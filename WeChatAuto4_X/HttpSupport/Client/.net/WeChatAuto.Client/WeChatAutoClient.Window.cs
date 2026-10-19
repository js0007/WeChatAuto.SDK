using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>最大化微信窗口。</summary>
    public Task<string?> MaximizeWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/maximize", cancellationToken: cancellationToken);

    /// <summary>还原微信窗口。</summary>
    public Task<string?> RestoreWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/restore", cancellationToken: cancellationToken);

    /// <summary>置顶微信窗口。</summary>
    public Task<string?> PinWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/pin", cancellationToken: cancellationToken);

    /// <summary>取消置顶微信窗口。</summary>
    public Task<string?> UnpinWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/unpin", cancellationToken: cancellationToken);

    /// <summary>使主窗口获取焦点。</summary>
    public Task<string?> FocusWindowAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/focus", cancellationToken: cancellationToken);

    /// <summary>关闭查询窗口（已打开则关闭）。</summary>
    public Task<string?> CloseSearchWindowAsync(string clientId, SubWindowRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/close-search-window", body: request, cancellationToken: cancellationToken);

    /// <summary>打开指定对象的子窗口。</summary>
    public Task<string?> OpenSubWindowAsync(string clientId, SubWindowRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/open-sub-window", body: request, cancellationToken: cancellationToken);

    /// <summary>关闭指定对象的子窗口（已存在则关闭）。</summary>
    public Task<string?> CloseSubWindowAsync(string clientId, SubWindowRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/window/close-sub-window", body: request, cancellationToken: cancellationToken);

    /// <summary>获取本微信窗口句柄。</summary>
    public Task<string?> GetWindowHandlerAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/window/handler", cancellationToken: cancellationToken);

    /// <summary>获取本微信窗口进程 ID。</summary>
    public Task<string?> GetWindowProcessIdAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/window/process-id", cancellationToken: cancellationToken);
}
