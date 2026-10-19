using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeChatAuto.Client.Models;

/// <summary>增加朋友选项。</summary>
public sealed class AddFriendsOptions
{
    /// <summary>间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点。</summary>
    [JsonPropertyName("interval_time")]
    public int IntervalTime { get; set; }

    /// <summary>是否关闭增加朋友窗口,默认关闭，可以设置为 false 不关闭。</summary>
    [JsonPropertyName("is_close_win")]
    public bool IsCloseWin { get; set; }

    /// <summary>加好友时打招呼内容,如果为空，则保持微信默认。</summary>
    [JsonPropertyName("say_hi")]
    public string? SayHi { get; set; }

    /// <summary>备注后缀，如：设置 suffix 为 "test",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类。</summary>
    [JsonPropertyName("suffix")]
    public string? Suffix { get; set; }

    /// <summary>设置标签。</summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }
}

/// <summary>通过手机号码或微信号添加好友。</summary>
public sealed class AddFriendsRequest
{
    /// <summary>好友手机号或微信号列表。</summary>
    [JsonPropertyName("friends")]
    public List<string>? Friends { get; set; }

    /// <summary>添加好友选项。</summary>
    [JsonPropertyName("options")]
    public AddFriendsOptions? Options { get; set; }
}

/// <summary>从群聊中添加好友。</summary>
public sealed class AddGroupMemberToFriendsRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>群成员列表。</summary>
    [JsonPropertyName("members")]
    public List<string>? Members { get; set; }

    /// <summary>添加好友选项。</summary>
    [JsonPropertyName("options")]
    public AddFriendsOptions? Options { get; set; }
}

/// <summary>修改群备注。</summary>
public sealed class ChangeGroupMemoRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>新的群备注。</summary>
    [JsonPropertyName("new_memo")]
    public string? NewMemo { get; set; }
}

/// <summary>修改自己在群中的昵称。</summary>
public sealed class ChangeGroupNickNameRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>新的群昵称。</summary>
    [JsonPropertyName("nick_name")]
    public string? NickName { get; set; }
}

/// <summary>消息引用。</summary>
public sealed class ChatRefer
{
    /// <summary>日期,如果不设置则不进行日期筛选（格式：date）。</summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>被引用的消息。</summary>
    [JsonPropertyName("message")]
    public ChatSimpleMessage? Message { get; set; }

    /// <summary>是否关闭查找窗口，默认是关闭，如果设置为 false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口。</summary>
    [JsonPropertyName("is_close_search_win")]
    public bool IsCloseSearchWin { get; set; }
}

/// <summary>简单聊天消息。</summary>
public sealed class ChatSimpleMessage
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>消息。</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>消息日期。</summary>
    [JsonPropertyName("send_date_time")]
    public string? SendDateTime { get; set; }

    /// <summary>消息日期,日期时间格式（date-time）。</summary>
    [JsonPropertyName("date_time")]
    public string? DateTime { get; set; }

    /// <summary>唯一字符串。</summary>
    [JsonPropertyName("unique_string")]
    public string? UniqueString { get; set; }
}

/// <summary>颜色。</summary>
public sealed class Color
{
    /// <summary>红色分量（只读）。</summary>
    [JsonPropertyName("r")]
    public int R { get; set; }

    /// <summary>绿色分量（只读）。</summary>
    [JsonPropertyName("g")]
    public int G { get; set; }

    /// <summary>蓝色分量（只读）。</summary>
    [JsonPropertyName("b")]
    public int B { get; set; }

    /// <summary>透明度分量（只读）。</summary>
    [JsonPropertyName("a")]
    public int A { get; set; }

    /// <summary>是否为已知颜色（只读）。</summary>
    [JsonPropertyName("is_known_color")]
    public bool IsKnownColor { get; set; }

    /// <summary>是否为空颜色（只读）。</summary>
    [JsonPropertyName("is_empty")]
    public bool IsEmpty { get; set; }

    /// <summary>是否为命名颜色（只读）。</summary>
    [JsonPropertyName("is_named_color")]
    public bool IsNamedColor { get; set; }

    /// <summary>是否为系统颜色（只读）。</summary>
    [JsonPropertyName("is_system_color")]
    public bool IsSystemColor { get; set; }

    /// <summary>颜色名称（只读）。</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

/// <summary>调色板。</summary>
public sealed class ColorPalette
{
    /// <summary>标志位（只读）。</summary>
    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    /// <summary>颜色条目列表（只读）。</summary>
    [JsonPropertyName("entries")]
    public List<Color>? Entries { get; set; }
}

/// <summary>创建群聊。</summary>
public sealed class CreateGroupRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>首个群成员。</summary>
    [JsonPropertyName("first_who")]
    public string? FirstWho { get; set; }

    /// <summary>群成员列表。</summary>
    [JsonPropertyName("members")]
    public List<string>? Members { get; set; }
}

/// <summary>个人信息。</summary>
public sealed class FriendInfo
{
    /// <summary>昵称。</summary>
    [JsonPropertyName("nick_name")]
    public string? NickName { get; set; }

    /// <summary>备注名。</summary>
    [JsonPropertyName("memo_name")]
    public string? MemoName { get; set; }

    /// <summary>地区,建议仅供参考。</summary>
    [JsonPropertyName("area")]
    public string? Area { get; set; }

    /// <summary>标签（注意：schema 中的字段名为 lable）。</summary>
    [JsonPropertyName("lable")]
    public List<string>? Lable { get; set; }

    /// <summary>共同群数量。</summary>
    [JsonPropertyName("same_group_number")]
    public string? SameGroupNumber { get; set; }

    /// <summary>个性签名。</summary>
    [JsonPropertyName("signature")]
    public string? Signature { get; set; }

    /// <summary>来源。</summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>微信ID。</summary>
    [JsonPropertyName("wx_id")]
    public string? WxId { get; set; }

    /// <summary>头像路径。</summary>
    [JsonPropertyName("avatar_path")]
    public string? AvatarPath { get; set; }

    /// <summary>头像图片。</summary>
    [JsonPropertyName("avatar_image")]
    public Image? AvatarImage { get; set; }

    /// <summary>头像图片 base64 字符串。</summary>
    [JsonPropertyName("avatar_image_base64")]
    public string? AvatarImageBase64 { get; set; }

    /// <summary>通过 wxid / 手机号查询结果。</summary>
    [JsonPropertyName("friend_search_result")]
    public FriendSearchResultEnums? FriendSearchResult { get; set; }

    /// <summary>聊天类型。</summary>
    [JsonPropertyName("chat_type")]
    public ChatType? ChatType { get; set; }

    /// <summary>添加好友时间，&gt;4.x 版本才有此属性。</summary>
    [JsonPropertyName("add_date_time")]
    public string? AddDateTime { get; set; }

    /// <summary>微信中使用的名称，本质是：如果昵称不等于备注名，则以备注名为主（只读）。</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

/// <summary>添加/移除群成员。group_name 为空时作用于当前焦点群聊。</summary>
public sealed class GroupMemberRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>群成员列表。</summary>
    [JsonPropertyName("members")]
    public List<string>? Members { get; set; }
}

/// <summary>图片信息。</summary>
public sealed class Image
{
    /// <summary>标签（schema 中未定义具体类型，任意值）。</summary>
    [JsonPropertyName("tag")]
    public object? Tag { get; set; }

    /// <summary>物理尺寸。</summary>
    [JsonPropertyName("physical_dimension")]
    public SizeF? PhysicalDimension { get; set; }

    /// <summary>像素尺寸。</summary>
    [JsonPropertyName("size")]
    public Size? Size { get; set; }

    /// <summary>宽度（只读）。</summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>高度（只读）。</summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>水平分辨率（只读）。</summary>
    [JsonPropertyName("horizontal_resolution")]
    public float HorizontalResolution { get; set; }

    /// <summary>垂直分辨率（只读）。</summary>
    [JsonPropertyName("vertical_resolution")]
    public float VerticalResolution { get; set; }

    /// <summary>标志位（只读）。</summary>
    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    /// <summary>原始格式。</summary>
    [JsonPropertyName("raw_format")]
    public ImageFormat? RawFormat { get; set; }

    /// <summary>像素格式。</summary>
    [JsonPropertyName("pixel_format")]
    public PixelFormat? PixelFormat { get; set; }

    /// <summary>属性 ID 列表（只读）。</summary>
    [JsonPropertyName("property_id_list")]
    public List<int>? PropertyIdList { get; set; }

    /// <summary>属性条目列表（只读）。</summary>
    [JsonPropertyName("property_items")]
    public List<PropertyItem>? PropertyItems { get; set; }

    /// <summary>调色板。</summary>
    [JsonPropertyName("palette")]
    public ColorPalette? Palette { get; set; }

    /// <summary>帧尺寸 GUID 列表（只读）。</summary>
    [JsonPropertyName("frame_dimensions_list")]
    public List<string>? FrameDimensionsList { get; set; }
}

/// <summary>图片格式（GUID）。</summary>
public sealed class ImageFormat
{
    /// <summary>格式 GUID（uuid）。</summary>
    [JsonPropertyName("guid")]
    public string? Guid { get; set; }
}

/// <summary>邀请群成员（外部群）。</summary>
public sealed class InviteGroupMemberRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>被邀请成员列表。</summary>
    [JsonPropertyName("members")]
    public List<string>? Members { get; set; }

    /// <summary>邀请原因。</summary>
    [JsonPropertyName("invite_reason")]
    public string? InviteReason { get; set; }
}

/// <summary>定位会话。</summary>
public sealed class LocateConversationRequest
{
    /// <summary>会话标题。</summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

/// <summary>点击任务栏微信图标。index 与 wechat_name 二选一。</summary>
public sealed class NotifyIconClickRequest
{
    /// <summary>任务栏图标索引。</summary>
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    /// <summary>微信名称。</summary>
    [JsonPropertyName("wechat_name")]
    public string? WechatName { get; set; }
}

/// <summary>图片属性条目。</summary>
public sealed class PropertyItem
{
    /// <summary>属性 ID。</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>长度。</summary>
    [JsonPropertyName("len")]
    public int Len { get; set; }

    /// <summary>类型。</summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>值（format: byte）。</summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

/// <summary>引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。</summary>
public sealed class ReferenceRequest
{
    /// <summary>被引用的消息。</summary>
    [JsonPropertyName("message")]
    public ChatSimpleMessage? Message { get; set; }

    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>被引用消息内容。</summary>
    [JsonPropertyName("message_content")]
    public string? MessageContent { get; set; }

    /// <summary>上一滚动序号。</summary>
    [JsonPropertyName("prev_scroll_number")]
    public int PrevScrollNumber { get; set; }
}

/// <summary>修改群名。</summary>
public sealed class RenameGroupRequest
{
    /// <summary>原群名称。</summary>
    [JsonPropertyName("old_group_name")]
    public string? OldGroupName { get; set; }

    /// <summary>新群名称。</summary>
    [JsonPropertyName("new_group_name")]
    public string? NewGroupName { get; set; }
}

/// <summary>保存头像到指定路径。</summary>
public sealed class SaveAvatarRequest
{
    /// <summary>保存路径。</summary>
    [JsonPropertyName("path")]
    public string? Path { get; set; }
}

/// <summary>搜索会话。</summary>
public sealed class SearchConversationRequest
{
    /// <summary>搜索关键字（微信名称）。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }
}

/// <summary>发送表情。emoji 与 index 二选一（优先使用 index）。</summary>
public sealed class SendEmojiRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>表情名称或描述。</summary>
    [JsonPropertyName("emoji")]
    public string? Emoji { get; set; }

    /// <summary>表情索引。</summary>
    [JsonPropertyName("index")]
    public int? Index { get; set; }

    /// <summary>被 @ 的好友列表。</summary>
    [JsonPropertyName("at_users")]
    public List<string>? AtUsers { get; set; }
}

/// <summary>发送文件。who 为空时发送给当前焦点聊天窗口。</summary>
public sealed class SendFileRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>文件路径列表。</summary>
    [JsonPropertyName("files")]
    public List<string>? Files { get; set; }
}

/// <summary>发送文本消息。who 为空时发送给当前焦点聊天窗口。</summary>
public sealed class SendMessageRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>消息内容。</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>被 @ 的好友列表。</summary>
    [JsonPropertyName("at_users")]
    public List<string>? AtUsers { get; set; }

    /// <summary>消息引用。</summary>
    [JsonPropertyName("refer")]
    public ChatRefer? Refer { get; set; }
}

/// <summary>发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。</summary>
public sealed class SendVoiceChatsRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>参与成员列表。</summary>
    [JsonPropertyName("partner")]
    public List<string>? Partner { get; set; }
}

/// <summary>发送语音消息。who 为空时发送给当前焦点聊天窗口。</summary>
public sealed class SendVoiceMessageRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>语音文件路径。</summary>
    [JsonPropertyName("file_path")]
    public string? FilePath { get; set; }
}

/// <summary>文字转语音发送。</summary>
public sealed class SendVoiceMessageTtsRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>TTS API Key。</summary>
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; }

    /// <summary>要合成的文本。</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>语音合成选项。</summary>
    [JsonPropertyName("options")]
    public VoiceOptions? Options { get; set; }

    /// <summary>是否在发送前用 LLM 优化文本。</summary>
    [JsonPropertyName("optimize_with_llm")]
    public bool OptimizeWithLlm { get; set; }
}

/// <summary>设置会话免打扰。who 为空时作用于当前窗口。</summary>
public sealed class SetDoNotDisturbRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>是否免打扰。</summary>
    [JsonPropertyName("setting")]
    public bool Setting { get; set; }
}

/// <summary>设置会话置顶。who 为空时作用于当前窗口。</summary>
public sealed class SetTopMostRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>是否置顶。</summary>
    [JsonPropertyName("setting")]
    public bool Setting { get; set; }
}

/// <summary>像素尺寸（整型）。</summary>
public sealed class Size
{
    /// <summary>是否为空尺寸（只读）。</summary>
    [JsonPropertyName("is_empty")]
    public bool IsEmpty { get; set; }

    /// <summary>宽度。</summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>高度。</summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
}

/// <summary>物理尺寸（浮点）。</summary>
public sealed class SizeF
{
    /// <summary>是否为空尺寸（只读）。</summary>
    [JsonPropertyName("is_empty")]
    public bool IsEmpty { get; set; }

    /// <summary>宽度。</summary>
    [JsonPropertyName("width")]
    public float Width { get; set; }

    /// <summary>高度。</summary>
    [JsonPropertyName("height")]
    public float Height { get; set; }
}

/// <summary>打开/关闭指定好友或群的子窗口。</summary>
public sealed class SubWindowRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }
}

/// <summary>切换/关闭导航栏窗口。</summary>
public sealed class SwitchNavigationRequest
{
    /// <summary>导航栏类型。</summary>
    [JsonPropertyName("navigation_type")]
    public NavigationType? NavigationType { get; set; }
}

/// <summary>拍一拍。</summary>
public sealed class TapRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }

    /// <summary>上一滚动序号。</summary>
    [JsonPropertyName("prev_scroll_number")]
    public int PrevScrollNumber { get; set; }
}

/// <summary>更新群公告。</summary>
public sealed class UpdateGroupNoticeRequest
{
    /// <summary>群名称。</summary>
    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; }

    /// <summary>群公告内容。</summary>
    [JsonPropertyName("group_notice")]
    public string? GroupNotice { get; set; }
}

/// <summary>语音合成选项。</summary>
public sealed class VoiceOptions
{
    /// <summary>音色。系统音色，或者自定义音色（克隆的音色或者自定义生成的音色）。</summary>
    [JsonPropertyName("voice")]
    public string? Voice { get; set; }

    /// <summary>语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。</summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。</summary>
    [JsonPropertyName("language_type")]
    public string? LanguageType { get; set; }

    /// <summary>指令控制文本。</summary>
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }

    /// <summary>音量。可选，默认 50，范围 [0, 100]。为 null 时不发送。</summary>
    [JsonPropertyName("volume")]
    public int? Volume { get; set; }

    /// <summary>语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。</summary>
    [JsonPropertyName("rate")]
    public float? Rate { get; set; }

    /// <summary>音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。</summary>
    [JsonPropertyName("pitch")]
    public float? Pitch { get; set; }

    /// <summary>随机种子。可选，范围 [0, 65535]。</summary>
    [JsonPropertyName("seed")]
    public int? Seed { get; set; }

    /// <summary>是否在发送前用 LLM 进行口语化校核。</summary>
    [JsonPropertyName("optimize_instructions")]
    public bool OptimizeInstructions { get; set; }

    /// <summary>是否流式输出。</summary>
    [JsonPropertyName("is_stream")]
    public bool IsStream { get; set; }
}

/// <summary>仅携带 who（可为空，作用于当前焦点窗口）的请求。</summary>
public sealed class WhoRequest
{
    /// <summary>微信名称。</summary>
    [JsonPropertyName("who")]
    public string? Who { get; set; }
}
