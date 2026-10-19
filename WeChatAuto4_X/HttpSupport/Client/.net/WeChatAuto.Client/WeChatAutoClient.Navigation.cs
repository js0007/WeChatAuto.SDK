using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>切换导航栏。</summary>
    public Task<string?> SwitchNavigationAsync(string clientId, SwitchNavigationRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/navigation/switch", body: request, cancellationToken: cancellationToken);

    /// <summary>关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。</summary>
    public Task<string?> CloseNavigationAsync(string clientId, SwitchNavigationRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/navigation/close", body: request, cancellationToken: cancellationToken);
}
