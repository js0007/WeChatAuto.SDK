using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>判断群聊是否为自有群。</summary>
    public Task<string?> IsGroupOwnerAsync(string clientId, string groupName, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/groups/{Escape(groupName)}/is-owner", cancellationToken: cancellationToken);

    /// <summary>获取群主昵称。</summary>
    public Task<string?> GetGroupOwnerAsync(string clientId, string groupName, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/groups/{Escape(groupName)}/owner", cancellationToken: cancellationToken);

    /// <summary>获取群聊成员列表。</summary>
    /// <param name="groupName">群名称，为空时作用于当前焦点群聊。</param>
    public Task<string?> GetGroupMembersAsync(string clientId, string? groupName = null, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/groups/members",
            BuildQuery(("groupName", groupName)), cancellationToken: cancellationToken);

    /// <summary>添加群聊成员（适用于自有群）。</summary>
    public Task<string?> AddGroupMembersAsync(string clientId, GroupMemberRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/groups/members", body: request, cancellationToken: cancellationToken);

    /// <summary>移除群聊成员（适用于自有群）。</summary>
    public Task<string?> RemoveGroupMembersAsync(string clientId, GroupMemberRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Delete, $"/api/v1/clients/{Escape(clientId)}/groups/members", body: request, cancellationToken: cancellationToken);

    /// <summary>退出群聊。</summary>
    /// <param name="clearHistory">是否清除历史记录，默认 true。</param>
    public Task<string?> ExitGroupAsync(string clientId, string groupName, bool clearHistory = true, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Delete, $"/api/v1/clients/{Escape(clientId)}/groups/{Escape(groupName)}",
            BuildQuery(("clearHistory", clearHistory ? "true" : "false")), cancellationToken: cancellationToken);

    /// <summary>创建群聊，已存在则打开群聊。</summary>
    public Task<string?> CreateGroupAsync(string clientId, CreateGroupRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/groups", body: request, cancellationToken: cancellationToken);

    /// <summary>修改群名（适用于自有群）。</summary>
    public Task<string?> RenameGroupAsync(string clientId, RenameGroupRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Put, $"/api/v1/clients/{Escape(clientId)}/groups/name", body: request, cancellationToken: cancellationToken);

    /// <summary>修改自己在群中的昵称。</summary>
    public Task<string?> ChangeGroupNickNameAsync(string clientId, ChangeGroupNickNameRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Put, $"/api/v1/clients/{Escape(clientId)}/groups/nickname", body: request, cancellationToken: cancellationToken);

    /// <summary>修改群备注（仅自己可见）。</summary>
    public Task<string?> ChangeGroupMemoAsync(string clientId, ChangeGroupMemoRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Put, $"/api/v1/clients/{Escape(clientId)}/groups/memo", body: request, cancellationToken: cancellationToken);

    /// <summary>更新群公告（仅适用于自有群）。</summary>
    public Task<string?> UpdateGroupNoticeAsync(string clientId, UpdateGroupNoticeRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Put, $"/api/v1/clients/{Escape(clientId)}/groups/notice", body: request, cancellationToken: cancellationToken);

    /// <summary>邀请群聊成员（适用于外部群）。</summary>
    public Task<string?> InviteGroupMembersAsync(string clientId, InviteGroupMemberRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/groups/invite", body: request, cancellationToken: cancellationToken);

    /// <summary>把群聊成员加为自己的好友（适用于外部群，注意微信风控）。</summary>
    public Task<string?> AddGroupMembersToFriendsAsync(string clientId, AddGroupMemberToFriendsRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/groups/add-members-to-friends", body: request, cancellationToken: cancellationToken);
}
