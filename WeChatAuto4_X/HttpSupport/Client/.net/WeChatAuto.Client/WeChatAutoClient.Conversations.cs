using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>获取会话列表所有会话的标题。</summary>
    public Task<string?> GetConversationsAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/conversations", cancellationToken: cancellationToken);

    /// <summary>获取会话列表可见会话的标题。</summary>
    public Task<string?> GetVisibleConversationTitlesAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/conversations/visible-titles", cancellationToken: cancellationToken);

    /// <summary>获取可见会话列表（含名称、未读消息数、头像等）。</summary>
    public Task<string?> GetVisibleConversationsAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/conversations/visible", cancellationToken: cancellationToken);

    /// <summary>搜索好友/群聊。</summary>
    public Task<string?> SearchConversationAsync(string clientId, SearchConversationRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/conversations/search", body: request, cancellationToken: cancellationToken);

    /// <summary>定位会话，滚动会话列表使指定会话可见。</summary>
    public Task<string?> LocateConversationAsync(string clientId, LocateConversationRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/conversations/locate", body: request, cancellationToken: cancellationToken);

    /// <summary>设置会话消息免打扰。</summary>
    public Task<string?> SetConversationDoNotDisturbAsync(string clientId, SetDoNotDisturbRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/conversations/set-do-not-disturb", body: request, cancellationToken: cancellationToken);

    /// <summary>设置会话置顶。</summary>
    public Task<string?> SetConversationTopMostAsync(string clientId, SetTopMostRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/conversations/set-top-most", body: request, cancellationToken: cancellationToken);
}
