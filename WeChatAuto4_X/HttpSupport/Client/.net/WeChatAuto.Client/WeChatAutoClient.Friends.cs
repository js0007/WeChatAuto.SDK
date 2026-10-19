using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>打开新增朋友窗口。</summary>
    public Task<string?> OpenAddFriendWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/friends/add-window/open", cancellationToken: cancellationToken);

    /// <summary>关闭新增朋友窗口。</summary>
    public Task<string?> CloseAddFriendWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/friends/add-window/close", cancellationToken: cancellationToken);

    /// <summary>通过手机号码/微信号查找并添加好友。</summary>
    public Task<string?> AddFriendsAsync(string clientId, AddFriendsRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/friends/add", body: request, cancellationToken: cancellationToken);

    /// <summary>获取所有好友的信息列表（默认优先从缓存读取）。</summary>
    /// <param name="fromCache">是否从缓存读取，默认 true。</param>
    public Task<string?> GetFriendsAsync(string clientId, bool fromCache = true, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends",
            BuildQuery(("fromCache", fromCache ? "true" : "false")), cancellationToken: cancellationToken);

    /// <summary>获取所有好友名称列表（有备注优先备注名）。</summary>
    public Task<string?> GetFriendNamesAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends/names", cancellationToken: cancellationToken);

    /// <summary>从通讯录移除好友。</summary>
    public Task<string?> DeleteFriendAsync(string clientId, string nickName, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Delete, $"/api/v1/clients/{Escape(clientId)}/friends/{Escape(nickName)}", cancellationToken: cancellationToken);

    /// <summary>获取缓存中的全部好友信息列表。</summary>
    public Task<string?> GetFriendsCacheAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends/cache", cancellationToken: cancellationToken);

    /// <summary>手动新增或修改缓存中的好友对象。</summary>
    public Task<string?> AddOrUpdateFriendCacheAsync(string clientId, FriendInfo friend, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/friends/cache", body: friend, cancellationToken: cancellationToken);

    /// <summary>按名称从缓存获取好友列表（适用于昵称重复场景）。</summary>
    public Task<string?> GetFriendsCacheByNameAsync(string clientId, string who, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends/cache/by-name/{Escape(who)}", cancellationToken: cancellationToken);

    /// <summary>按 wxid 从缓存获取好友信息。</summary>
    public Task<string?> GetFriendCacheByWxIdAsync(string clientId, string wxid, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends/cache/by-wxid/{Escape(wxid)}", cancellationToken: cancellationToken);

    /// <summary>按 wxid 从缓存移除好友。</summary>
    public Task<string?> DeleteFriendCacheByWxIdAsync(string clientId, string wxid, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Delete, $"/api/v1/clients/{Escape(clientId)}/friends/cache/by-wxid/{Escape(wxid)}", cancellationToken: cancellationToken);

    /// <summary>按名称从缓存获取单个好友信息。</summary>
    public Task<string?> GetFriendCacheAsync(string clientId, string who, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/friends/cache/{Escape(who)}", cancellationToken: cancellationToken);

    /// <summary>按名称从缓存移除好友。</summary>
    public Task<string?> DeleteFriendCacheAsync(string clientId, string who, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Delete, $"/api/v1/clients/{Escape(clientId)}/friends/cache/{Escape(who)}", cancellationToken: cancellationToken);
}
