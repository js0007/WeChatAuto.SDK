//! Data models for the WECHATAUTO.SDK HTTP API.
//!
//! Every type in this module mirrors a schema under `components.schemas` of
//! `swagger.json`. Wire property names are `snake_case` and `Option<T>` fields
//! are omitted from the JSON body when they are `None`.

use serde::{Deserialize, Serialize};

/// 聊天类型
#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
pub enum ChatType {
    #[serde(rename = "好友")]
    Friend,
    #[serde(rename = "企业微信")]
    EnterpriseWeChat,
    #[serde(rename = "群聊")]
    GroupChat,
    #[serde(rename = "公众号")]
    OfficialAccount,
    #[serde(rename = "订阅号")]
    Subscription,
    #[serde(rename = "服务通知")]
    ServiceNotice,
    #[serde(rename = "腾讯新闻")]
    TencentNews,
    #[serde(rename = "微信团队")]
    WeChatTeam,
    #[serde(rename = "文件传输助手")]
    FileTransfer,
    #[serde(rename = "其他")]
    Other,
}

/// 通过wxid查询，或者通过手机号查询结果枚举
#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
pub enum FriendSearchResultEnums {
    #[serde(rename = "Friend")]
    Friend,
    #[serde(rename = "No_Find")]
    NoFind,
    #[serde(rename = "NotFriend")]
    NotFriend,
}

/// 导航栏类型
#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
pub enum NavigationType {
    #[serde(rename = "聊天")]
    Chat,
    #[serde(rename = "通讯录")]
    Contacts,
    #[serde(rename = "收藏")]
    Favorites,
    #[serde(rename = "聊天文件")]
    ChatFiles,
    #[serde(rename = "朋友圈")]
    Moments,
    #[serde(rename = "搜一搜")]
    Search,
    #[serde(rename = "视频号")]
    Channels,
    #[serde(rename = "看一看")]
    Look,
    #[serde(rename = "小程序面板")]
    MiniProgramPanel,
    #[serde(rename = "手机")]
    Phone,
    #[serde(rename = "设置及其他")]
    Settings,
    #[serde(rename = "微信")]
    WeChat,
    #[serde(rename = "游戏中心")]
    GameCenter,
    #[serde(rename = "更多")]
    More,
}

/// System.Drawing pixel format values.
#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
pub enum PixelFormat {
    #[serde(rename = "DontCare")]
    DontCare,
    #[serde(rename = "Max")]
    Max,
    #[serde(rename = "Indexed")]
    Indexed,
    #[serde(rename = "Gdi")]
    Gdi,
    #[serde(rename = "Format16bppRgb555")]
    Format16bppRgb555,
    #[serde(rename = "Format16bppRgb565")]
    Format16bppRgb565,
    #[serde(rename = "Format24bppRgb")]
    Format24bppRgb,
    #[serde(rename = "Format32bppRgb")]
    Format32bppRgb,
    #[serde(rename = "Format1bppIndexed")]
    Format1bppIndexed,
    #[serde(rename = "Format4bppIndexed")]
    Format4bppIndexed,
    #[serde(rename = "Format8bppIndexed")]
    Format8bppIndexed,
    #[serde(rename = "Alpha")]
    Alpha,
    #[serde(rename = "Format16bppArgb1555")]
    Format16bppArgb1555,
    #[serde(rename = "PAlpha")]
    PAlpha,
    #[serde(rename = "Format32bppPArgb")]
    Format32bppPArgb,
    #[serde(rename = "Extended")]
    Extended,
    #[serde(rename = "Format16bppGrayScale")]
    Format16bppGrayScale,
    #[serde(rename = "Format48bppRgb")]
    Format48bppRgb,
    #[serde(rename = "Format64bppPArgb")]
    Format64bppPArgb,
    #[serde(rename = "Canonical")]
    Canonical,
    #[serde(rename = "Format32bppArgb")]
    Format32bppArgb,
    #[serde(rename = "Format64bppArgb")]
    Format64bppArgb,
}

/// 增加朋友选项
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct AddFriendsOptions {
    /// 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点
    pub interval_time: i32,
    /// 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭
    pub is_close_win: bool,
    /// 加好友时打招呼内容,如果为空，则保持微信默认
    #[serde(skip_serializing_if = "Option::is_none")]
    pub say_hi: Option<String>,
    /// 备注后缀，如：设置suffix为"test",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类
    #[serde(skip_serializing_if = "Option::is_none")]
    pub suffix: Option<String>,
    /// 设置标签
    #[serde(skip_serializing_if = "Option::is_none")]
    pub label: Option<String>,
}

/// 通过手机号码或微信号添加好友。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct AddFriendsRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub friends: Option<Vec<String>>,
    pub options: AddFriendsOptions,
}

/// 从群聊中添加好友。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct AddGroupMemberToFriendsRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub members: Option<Vec<String>>,
    pub options: AddFriendsOptions,
}

/// 修改群备注。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ChangeGroupMemoRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub new_memo: Option<String>,
}

/// 修改自己在群中的昵称。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ChangeGroupNickNameRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub nick_name: Option<String>,
}

/// 消息引用
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ChatRefer {
    /// 日期,如果不设置则不进行日期筛选
    pub date: String,
    pub message: ChatSimpleMessage,
    /// 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口.
    pub is_close_search_win: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ChatSimpleMessage {
    /// 微信名称
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    /// 消息
    #[serde(skip_serializing_if = "Option::is_none")]
    pub message: Option<String>,
    /// 消息日期
    #[serde(skip_serializing_if = "Option::is_none")]
    pub send_date_time: Option<String>,
    /// 消息日期,日期时间格式
    pub date_time: String,
    /// 唯一字符串
    #[serde(skip_serializing_if = "Option::is_none")]
    pub unique_string: Option<String>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct Color {
    pub r: i32,
    pub g: i32,
    pub b: i32,
    pub a: i32,
    pub is_known_color: bool,
    pub is_empty: bool,
    pub is_named_color: bool,
    pub is_system_color: bool,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub name: Option<String>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ColorPalette {
    pub flags: i32,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub entries: Option<Vec<Color>>,
}

/// 创建群聊。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct CreateGroupRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub first_who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub members: Option<Vec<String>>,
}

/// 个人信息
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct FriendInfo {
    /// 昵称
    #[serde(skip_serializing_if = "Option::is_none")]
    pub nick_name: Option<String>,
    /// 备注名
    #[serde(skip_serializing_if = "Option::is_none")]
    pub memo_name: Option<String>,
    /// 地区,建议仅供参考
    #[serde(skip_serializing_if = "Option::is_none")]
    pub area: Option<String>,
    /// 标签
    #[serde(skip_serializing_if = "Option::is_none")]
    pub lable: Option<Vec<String>>,
    /// 共同群数量
    #[serde(skip_serializing_if = "Option::is_none")]
    pub same_group_number: Option<String>,
    /// 个性签名
    #[serde(skip_serializing_if = "Option::is_none")]
    pub signature: Option<String>,
    /// 来源
    #[serde(skip_serializing_if = "Option::is_none")]
    pub source: Option<String>,
    /// 微信ID
    #[serde(skip_serializing_if = "Option::is_none")]
    pub wx_id: Option<String>,
    /// 头像路径
    #[serde(skip_serializing_if = "Option::is_none")]
    pub avatar_path: Option<String>,
    pub avatar_image: Image,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub avatar_image_base64: Option<String>,
    pub friend_search_result: FriendSearchResultEnums,
    pub chat_type: ChatType,
    /// 添加好友时间，>4.x版本才有此属性
    #[serde(skip_serializing_if = "Option::is_none")]
    pub add_date_time: Option<String>,
    /// 微信中使用的名称。本质是: 如果昵称不等于备注名，则以备注名不为主.
    #[serde(skip_serializing_if = "Option::is_none")]
    pub name: Option<String>,
}

/// 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct GroupMemberRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub members: Option<Vec<String>>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct Image {
    /// Untyped/opaque tag value (schema has no declared type).
    #[serde(skip_serializing_if = "Option::is_none")]
    pub tag: Option<serde_json::Value>,
    pub physical_dimension: SizeF,
    pub size: Size,
    pub width: i32,
    pub height: i32,
    pub horizontal_resolution: f32,
    pub vertical_resolution: f32,
    pub flags: i32,
    pub raw_format: ImageFormat,
    pub pixel_format: PixelFormat,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub property_id_list: Option<Vec<i32>>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub property_items: Option<Vec<PropertyItem>>,
    pub palette: ColorPalette,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub frame_dimensions_list: Option<Vec<String>>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ImageFormat {
    pub guid: String,
}

/// 邀请群成员（外部群）。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct InviteGroupMemberRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub members: Option<Vec<String>>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub invite_reason: Option<String>,
}

/// 定位会话。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct LocateConversationRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub title: Option<String>,
}

/// 点击任务栏微信图标。index 与 wechat_name 二选一。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct NotifyIconClickRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub index: Option<i32>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub wechat_name: Option<String>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct PropertyItem {
    pub id: i32,
    pub len: i32,
    #[serde(rename = "type")]
    pub r#type: i32,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub value: Option<String>,
}

/// 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct ReferenceRequest {
    pub message: ChatSimpleMessage,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub message_content: Option<String>,
    pub prev_scroll_number: i32,
}

/// 修改群名。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct RenameGroupRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub old_group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub new_group_name: Option<String>,
}

/// 保存头像到指定路径。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SaveAvatarRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub path: Option<String>,
}

/// 搜索会话。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SearchConversationRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
}

/// 发送表情。emoji 与 index 二选一（优先使用 index）。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendEmojiRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    /// 表情名称或描述。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub emoji: Option<String>,
    /// 表情索引。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub index: Option<i32>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub at_users: Option<Vec<String>>,
}

/// 发送文件。who 为空时发送给当前焦点聊天窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendFileRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub files: Option<Vec<String>>,
}

/// 发送文本消息。who 为空时发送给当前焦点聊天窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendMessageRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub message: Option<String>,
    /// 被 @ 的好友列表。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub at_users: Option<Vec<String>>,
    pub refer: ChatRefer,
}

/// 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendVoiceChatsRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub partner: Option<Vec<String>>,
}

/// 发送语音消息。who 为空时发送给当前焦点聊天窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendVoiceMessageRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub file_path: Option<String>,
}

/// 文字转语音发送。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SendVoiceMessageTtsRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub api_key: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub message: Option<String>,
    pub options: VoiceOptions,
    pub optimize_with_llm: bool,
}

/// 设置会话免打扰。who 为空时作用于当前窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SetDoNotDisturbRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    pub setting: bool,
}

/// 设置会话置顶。who 为空时作用于当前窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SetTopMostRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    pub setting: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct Size {
    pub is_empty: bool,
    pub width: i32,
    pub height: i32,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SizeF {
    pub is_empty: bool,
    pub width: f32,
    pub height: f32,
}

/// 打开/关闭指定好友或群的子窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SubWindowRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
}

/// 切换/关闭导航栏窗口。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct SwitchNavigationRequest {
    pub navigation_type: NavigationType,
}

/// 拍一拍。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct TapRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
    pub prev_scroll_number: i32,
}

/// 更新群公告。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct UpdateGroupNoticeRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_name: Option<String>,
    #[serde(skip_serializing_if = "Option::is_none")]
    pub group_notice: Option<String>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct VoiceOptions {
    /// 音色。系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
    #[serde(skip_serializing_if = "Option::is_none")]
    pub voice: Option<String>,
    /// 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub model: Option<String>,
    /// 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub language_type: Option<String>,
    /// 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。系统音色与声音复刻音色均可输入任意指令。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub instructions: Option<String>,
    /// 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub volume: Option<i32>,
    /// 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，避免与指令中的语速描述冲突。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub rate: Option<f32>,
    /// 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub pitch: Option<f32>,
    /// 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。
    #[serde(skip_serializing_if = "Option::is_none")]
    pub seed: Option<i32>,
    /// 是否在发送前用LLM进行口语化校核
    pub optimize_instructions: bool,
    pub is_stream: bool,
}

/// 仅携带 who（可为空，作用于当前焦点窗口）的请求。
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "snake_case")]
pub struct WhoRequest {
    #[serde(skip_serializing_if = "Option::is_none")]
    pub who: Option<String>,
}
