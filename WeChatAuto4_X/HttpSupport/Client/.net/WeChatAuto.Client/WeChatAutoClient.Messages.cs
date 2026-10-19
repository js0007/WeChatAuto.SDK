using System.Net.Http;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

public sealed partial class WeChatAutoClient
{
    /// <summary>获取当前聊天窗口的标题对象。</summary>
    public Task<string?> GetMessageTitleAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/messages/title", cancellationToken: cancellationToken);

    /// <summary>获取当前聊天窗口的标题名称。</summary>
    public Task<string?> GetMessageOnlyTitleAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/messages/only-title", cancellationToken: cancellationToken);

    /// <summary>聚焦当前窗口的消息输入区。</summary>
    public Task<string?> FocusMessageInputAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/focus-input", cancellationToken: cancellationToken);

    /// <summary>发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。</summary>
    public Task<string?> SendMessageAsync(string clientId, SendMessageRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send", body: request, cancellationToken: cancellationToken);

    /// <summary>发送文件。</summary>
    public Task<string?> SendFileAsync(string clientId, SendFileRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-file", body: request, cancellationToken: cancellationToken);

    /// <summary>发送表情（按名称/描述/索引）。</summary>
    public Task<string?> SendEmojiAsync(string clientId, SendEmojiRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-emoji", body: request, cancellationToken: cancellationToken);

    /// <summary>发起单人语音聊天。</summary>
    public Task<string?> SendVoiceChatAsync(string clientId, WhoRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-voice-chat", body: request, cancellationToken: cancellationToken);

    /// <summary>发起单人视频聊天。</summary>
    public Task<string?> SendVideoChatAsync(string clientId, WhoRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-video-chat", body: request, cancellationToken: cancellationToken);

    /// <summary>发起多人语音聊天（群聊）。</summary>
    public Task<string?> SendVoiceChatsAsync(string clientId, SendVoiceChatsRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-voice-chats", body: request, cancellationToken: cancellationToken);

    /// <summary>发送语音消息（需虚拟声卡 Cable input/Cable output）。</summary>
    public Task<string?> SendVoiceMessageAsync(string clientId, SendVoiceMessageRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-voice-message", body: request, cancellationToken: cancellationToken);

    /// <summary>文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。</summary>
    public Task<string?> SendVoiceMessageTtsAsync(string clientId, SendVoiceMessageTtsRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/send-voice-message-tts", body: request, cancellationToken: cancellationToken);

    /// <summary>获取聊天历史（支持按日期或时间段查询）。</summary>
    /// <param name="who">微信名称。</param>
    /// <param name="date">日期（date-time）。</param>
    /// <param name="startDate">开始日期（date-time）。</param>
    /// <param name="endDate">结束日期（date-time）。</param>
    public Task<string?> GetMessageHistoryAsync(
        string clientId,
        string? who = null,
        string? date = null,
        string? startDate = null,
        string? endDate = null,
        CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Get, $"/api/v1/clients/{Escape(clientId)}/messages/history",
            BuildQuery(
                ("who", who),
                ("date", date),
                ("start_date", startDate),
                ("end_date", endDate)),
            cancellationToken: cancellationToken);

    /// <summary>拍一拍当前聊天窗口的好友。</summary>
    public Task<string?> TapAsync(string clientId, TapRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/tap", body: request, cancellationToken: cancellationToken);

    /// <summary>引用当前窗口的好友消息。</summary>
    public Task<string?> ReferenceAsync(string clientId, ReferenceRequest request, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/reference", body: request, cancellationToken: cancellationToken);

    /// <summary>引用当前窗口最后一条消息。</summary>
    public Task<string?> ReferenceLastAsync(string clientId, CancellationToken cancellationToken = default)
        => SendAsync(HttpMethod.Post, $"/api/v1/clients/{Escape(clientId)}/messages/reference-last", cancellationToken: cancellationToken);
}
