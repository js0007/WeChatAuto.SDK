using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>获取全部微信客户端的概要信息列表。</summary>
    public Task<string?> GetClientsAsync(CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, "/api/v1/clients", cancellationToken: cancellationToken);

    /// <summary>获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。</summary>
    public Task<string?> GetClientAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}", cancellationToken: cancellationToken);

    /// <summary>获取本微信的个人信息（头像文件位置、wxid、昵称）。</summary>
    public Task<string?> GetOwnerInfoAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/owner-info", cancellationToken: cancellationToken);

    /// <summary>保存本微信头像到指定路径。</summary>
    public Task<string?> SaveAvatarAsync(string clientId, SaveAvatarRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/avatar", body: request, cancellationToken: cancellationToken);

    /// <summary>点击任务栏微信图标，按索引或微信名称定位。</summary>
    public Task<string?> ClickNotifyIconAsync(string clientId, NotifyIconClickRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/notify-icon/click", body: request, cancellationToken: cancellationToken);
}
