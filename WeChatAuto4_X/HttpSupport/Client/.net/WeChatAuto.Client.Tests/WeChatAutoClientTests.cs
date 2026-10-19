using System.Net;
using System.Text.Json;
using WeChatAuto.Client;
using WeChatAuto.Client.Models;
using Xunit;

namespace WeChatAuto.Client.Tests;

public class WeChatAutoClientTests
{
    private static (WeChatAutoClient Client, FakeHandler Handler) CreateClient(string? apiKey = null, string baseUrl = "http://localhost:5000")
    {
        var handler = new FakeHandler();
        var client = new WeChatAutoClient(baseUrl, apiKey, handler);
        return (client, handler);
    }

    [Fact]
    public async Task Get_NoParams_SendsGetToClientsPathWithoutQuery()
    {
        var (client, handler) = CreateClient();

        await client.GetClientsAsync();

        Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
        Assert.Equal("/api/v1/clients", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.Equal("", handler.LastRequest.RequestUri.Query);
        Assert.Null(handler.LastBody);
    }

    [Fact]
    public async Task Get_WithQuery_SerializesBooleanQueryParameter()
    {
        var (client, handler) = CreateClient();

        await client.GetFriendsAsync("abc", fromCache: false);

        Assert.Equal(HttpMethod.Get, handler.LastRequest!.Method);
        Assert.Equal("/api/v1/clients/abc/friends", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.Equal("?fromCache=false", handler.LastRequest.RequestUri.Query);
    }

    [Fact]
    public async Task Get_WithPathParam_UrlEncodesSegment()
    {
        var (client, handler) = CreateClient();

        await client.GetClientAsync("my client");

        Assert.Equal("/api/v1/clients/my%20client", handler.LastRequest!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Post_WithBody_SerializesSnakeCaseJsonBody()
    {
        var (client, handler) = CreateClient();

        await client.SendMessageAsync("abc", new SendMessageRequest
        {
            Who = "friend",
            Message = "hello",
            AtUsers = new List<string> { "a", "b" },
        });

        Assert.Equal(HttpMethod.Post, handler.LastRequest!.Method);
        Assert.Equal("/api/v1/clients/abc/messages/send", handler.LastRequest.RequestUri!.AbsolutePath);

        using var doc = JsonDocument.Parse(handler.LastBody!);
        var root = doc.RootElement;
        Assert.Equal("friend", root.GetProperty("who").GetString());
        Assert.Equal("hello", root.GetProperty("message").GetString());
        Assert.Equal("a", root.GetProperty("at_users")[0].GetString());
        Assert.Equal("b", root.GetProperty("at_users")[1].GetString());
    }

    [Fact]
    public async Task Put_WithBody_SendsPutWithSnakeCaseJsonBody()
    {
        var (client, handler) = CreateClient();

        await client.RenameGroupAsync("abc", new RenameGroupRequest
        {
            OldGroupName = "g1",
            NewGroupName = "g2",
        });

        Assert.Equal(HttpMethod.Put, handler.LastRequest!.Method);
        Assert.Equal("/api/v1/clients/abc/groups/name", handler.LastRequest.RequestUri!.AbsolutePath);

        using var doc = JsonDocument.Parse(handler.LastBody!);
        Assert.Equal("g1", doc.RootElement.GetProperty("old_group_name").GetString());
        Assert.Equal("g2", doc.RootElement.GetProperty("new_group_name").GetString());
    }

    [Fact]
    public async Task Delete_WithPathParam_SendsDelete()
    {
        var (client, handler) = CreateClient();

        await client.DeleteFriendAsync("abc", "bob");

        Assert.Equal(HttpMethod.Delete, handler.LastRequest!.Method);
        Assert.Equal("/api/v1/clients/abc/friends/bob", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task ApiKey_Empty_OmitsHeader()
    {
        var (client, handler) = CreateClient(apiKey: "");

        await client.GetClientsAsync();

        Assert.False(handler.LastRequest!.Headers.Contains("X-API-Key"));
    }

    [Fact]
    public async Task ApiKey_Null_OmitsHeader()
    {
        var (client, handler) = CreateClient(apiKey: null);

        await client.GetClientsAsync();

        Assert.False(handler.LastRequest!.Headers.Contains("X-API-Key"));
    }

    [Fact]
    public async Task ApiKey_Provided_SendsHeader()
    {
        var (client, handler) = CreateClient(apiKey: "secret");

        await client.GetClientsAsync();

        Assert.True(handler.LastRequest!.Headers.TryGetValues("X-API-Key", out var values));
        Assert.Equal("secret", Assert.Single(values));
    }

    [Fact]
    public async Task Enum_Property_SerializesAsString()
    {
        var (client, handler) = CreateClient();

        await client.SwitchNavigationAsync("abc", new SwitchNavigationRequest { NavigationType = NavigationType.Contacts });

        using var doc = JsonDocument.Parse(handler.LastBody!);
        Assert.Equal("通讯录", doc.RootElement.GetProperty("navigation_type").GetString());
    }

    private sealed class FakeHandler : HttpMessageHandler
    {
        public HttpRequestMessage? LastRequest { get; private set; }
        public string? LastBody { get; private set; }
        public string ResponseContent { get; set; } = "{}";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            LastBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(StatusCode) { Content = new StringContent(ResponseContent) };
        }
    }
}
