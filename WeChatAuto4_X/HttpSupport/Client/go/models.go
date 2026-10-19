// Package client provides a generated HTTP client for the WECHATAUTO.SDK API.
package client

// ChatType 聊天类型。
type ChatType string

const (
	ChatTypeFriend          ChatType = "好友"
	ChatTypeEnterprise      ChatType = "企业微信"
	ChatTypeGroup           ChatType = "群聊"
	ChatTypeOfficialAccount ChatType = "公众号"
	ChatTypeSubscription    ChatType = "订阅号"
	ChatTypeServiceNotice   ChatType = "服务通知"
	ChatTypeTencentNews     ChatType = "腾讯新闻"
	ChatTypeWeChatTeam      ChatType = "微信团队"
	ChatTypeFileTransfer    ChatType = "文件传输助手"
	ChatTypeOther           ChatType = "其他"
)

// FriendSearchResultEnums 通过 wxid 查询，或者通过手机号查询结果枚举。
type FriendSearchResultEnums string

const (
	FriendSearchResultFriend    FriendSearchResultEnums = "Friend"
	FriendSearchResultNoFind    FriendSearchResultEnums = "No_Find"
	FriendSearchResultNotFriend FriendSearchResultEnums = "NotFriend"
)

// NavigationType 导航栏类型。
type NavigationType string

const (
	NavigationChat             NavigationType = "聊天"
	NavigationContacts         NavigationType = "通讯录"
	NavigationFavorites        NavigationType = "收藏"
	NavigationChatFiles        NavigationType = "聊天文件"
	NavigationMoments          NavigationType = "朋友圈"
	NavigationSearch           NavigationType = "搜一搜"
	NavigationChannels         NavigationType = "视频号"
	NavigationLook             NavigationType = "看一看"
	NavigationMiniProgramPanel NavigationType = "小程序面板"
	NavigationPhone            NavigationType = "手机"
	NavigationSettings         NavigationType = "设置及其他"
	NavigationWeChat           NavigationType = "微信"
	NavigationGameCenter       NavigationType = "游戏中心"
	NavigationMore             NavigationType = "更多"
)

// PixelFormat 像素格式。
type PixelFormat string

const (
	PixelFormatDontCare       PixelFormat = "DontCare"
	PixelFormatMax            PixelFormat = "Max"
	PixelFormatIndexed        PixelFormat = "Indexed"
	PixelFormatGdi            PixelFormat = "Gdi"
	PixelFormat16bppRgb555    PixelFormat = "Format16bppRgb555"
	PixelFormat16bppRgb565    PixelFormat = "Format16bppRgb565"
	PixelFormat24bppRgb       PixelFormat = "Format24bppRgb"
	PixelFormat32bppRgb       PixelFormat = "Format32bppRgb"
	PixelFormat1bppIndexed    PixelFormat = "Format1bppIndexed"
	PixelFormat4bppIndexed    PixelFormat = "Format4bppIndexed"
	PixelFormat8bppIndexed    PixelFormat = "Format8bppIndexed"
	PixelFormatAlpha          PixelFormat = "Alpha"
	PixelFormat16bppArgb1555  PixelFormat = "Format16bppArgb1555"
	PixelFormatPAlpha         PixelFormat = "PAlpha"
	PixelFormat32bppPArgb     PixelFormat = "Format32bppPArgb"
	PixelFormatExtended       PixelFormat = "Extended"
	PixelFormat16bppGrayScale PixelFormat = "Format16bppGrayScale"
	PixelFormat48bppRgb       PixelFormat = "Format48bppRgb"
	PixelFormat64bppPArgb     PixelFormat = "Format64bppPArgb"
	PixelFormatCanonical      PixelFormat = "Canonical"
	PixelFormat32bppArgb      PixelFormat = "Format32bppArgb"
	PixelFormat64bppArgb      PixelFormat = "Format64bppArgb"
)

// AddFriendsOptions 增加朋友选项。
type AddFriendsOptions struct {
	IntervalTime int     `json:"interval_time,omitempty"` // 间隔时间（秒），默认三秒。
	IsCloseWin   bool    `json:"is_close_win,omitempty"`  // 是否关闭添加朋友窗口。
	SayHi        *string `json:"say_hi,omitempty"`        // 加好友时打招呼内容。
	Suffix       *string `json:"suffix,omitempty"`        // 备注后缀。
	Label        *string `json:"label,omitempty"`         // 设置标签。
}

// AddFriendsRequest 通过手机号码或微信号添加好友。
type AddFriendsRequest struct {
	Friends []string           `json:"friends,omitempty"`
	Options *AddFriendsOptions `json:"options,omitempty"`
}

// AddGroupMemberToFriendsRequest 从群聊中添加好友。
type AddGroupMemberToFriendsRequest struct {
	GroupName *string            `json:"group_name,omitempty"`
	Members   []string           `json:"members,omitempty"`
	Options   *AddFriendsOptions `json:"options,omitempty"`
}

// ChangeGroupMemoRequest 修改群备注。
type ChangeGroupMemoRequest struct {
	GroupName *string `json:"group_name,omitempty"`
	NewMemo   *string `json:"new_memo,omitempty"`
}

// ChangeGroupNickNameRequest 修改自己在群中的昵称。
type ChangeGroupNickNameRequest struct {
	GroupName *string `json:"group_name,omitempty"`
	NickName  *string `json:"nick_name,omitempty"`
}

// ChatRefer 消息引用。
type ChatRefer struct {
	Date             string             `json:"date,omitempty"`
	Message          *ChatSimpleMessage `json:"message,omitempty"`
	IsCloseSearchWin bool               `json:"is_close_search_win,omitempty"`
}

// ChatSimpleMessage 简单聊天消息。
type ChatSimpleMessage struct {
	Who          *string `json:"who,omitempty"`
	Message      *string `json:"message,omitempty"`
	SendDateTime *string `json:"send_date_time,omitempty"`
	DateTime     string  `json:"date_time,omitempty"`
	UniqueString *string `json:"unique_string,omitempty"`
}

// Color 颜色。
type Color struct {
	R             int     `json:"r,omitempty"`
	G             int     `json:"g,omitempty"`
	B             int     `json:"b,omitempty"`
	A             int     `json:"a,omitempty"`
	IsKnownColor  bool    `json:"is_known_color,omitempty"`
	IsEmpty       bool    `json:"is_empty,omitempty"`
	IsNamedColor  bool    `json:"is_named_color,omitempty"`
	IsSystemColor bool    `json:"is_system_color,omitempty"`
	Name          *string `json:"name,omitempty"`
}

// ColorPalette 调色板。
type ColorPalette struct {
	Flags   int     `json:"flags,omitempty"`
	Entries []Color `json:"entries,omitempty"`
}

// CreateGroupRequest 创建群聊。
type CreateGroupRequest struct {
	GroupName *string  `json:"group_name,omitempty"`
	FirstWho  *string  `json:"first_who,omitempty"`
	Members   []string `json:"members,omitempty"`
}

// FriendInfo 个人信息。
type FriendInfo struct {
	NickName           *string                 `json:"nick_name,omitempty"`
	MemoName           *string                 `json:"memo_name,omitempty"`
	Area               *string                 `json:"area,omitempty"`
	Lable              []string                `json:"lable,omitempty"`
	SameGroupNumber    *string                 `json:"same_group_number,omitempty"`
	Signature          *string                 `json:"signature,omitempty"`
	Source             *string                 `json:"source,omitempty"`
	WxID               *string                 `json:"wx_id,omitempty"`
	AvatarPath         *string                 `json:"avatar_path,omitempty"`
	AvatarImage        *Image                  `json:"avatar_image,omitempty"`
	AvatarImageBase64  *string                 `json:"avatar_image_base64,omitempty"`
	FriendSearchResult FriendSearchResultEnums `json:"friend_search_result,omitempty"`
	ChatType           ChatType                `json:"chat_type,omitempty"`
	AddDateTime        *string                 `json:"add_date_time,omitempty"`
	Name               *string                 `json:"name,omitempty"`
}

// GroupMemberRequest 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
type GroupMemberRequest struct {
	GroupName *string  `json:"group_name,omitempty"`
	Members   []string `json:"members,omitempty"`
}

// Image 图像信息。
type Image struct {
	Tag                  any            `json:"tag,omitempty"`
	PhysicalDimension    *SizeF         `json:"physical_dimension,omitempty"`
	Size                 *Size          `json:"size,omitempty"`
	Width                int            `json:"width,omitempty"`
	Height               int            `json:"height,omitempty"`
	HorizontalResolution float64        `json:"horizontal_resolution,omitempty"`
	VerticalResolution   float64        `json:"vertical_resolution,omitempty"`
	Flags                int            `json:"flags,omitempty"`
	RawFormat            *ImageFormat   `json:"raw_format,omitempty"`
	PixelFormat          PixelFormat    `json:"pixel_format,omitempty"`
	PropertyIDList       []int          `json:"property_id_list,omitempty"`
	PropertyItems        []PropertyItem `json:"property_items,omitempty"`
	Palette              *ColorPalette  `json:"palette,omitempty"`
	FrameDimensionsList  []string       `json:"frame_dimensions_list,omitempty"`
}

// ImageFormat 图像格式。
type ImageFormat struct {
	GUID string `json:"guid,omitempty"`
}

// InviteGroupMemberRequest 邀请群成员（外部群）。
type InviteGroupMemberRequest struct {
	GroupName    *string  `json:"group_name,omitempty"`
	Members      []string `json:"members,omitempty"`
	InviteReason *string  `json:"invite_reason,omitempty"`
}

// LocateConversationRequest 定位会话。
type LocateConversationRequest struct {
	Title *string `json:"title,omitempty"`
}

// NotifyIconClickRequest 点击任务栏微信图标。index 与 wechat_name 二选一。
type NotifyIconClickRequest struct {
	Index      *int    `json:"index,omitempty"`
	WechatName *string `json:"wechat_name,omitempty"`
}

// PropertyItem 属性项。
type PropertyItem struct {
	ID    int     `json:"id,omitempty"`
	Len   int     `json:"len,omitempty"`
	Type  int     `json:"type,omitempty"`
	Value *string `json:"value,omitempty"`
}

// ReferenceRequest 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
type ReferenceRequest struct {
	Message          *ChatSimpleMessage `json:"message,omitempty"`
	Who              *string            `json:"who,omitempty"`
	MessageContent   *string            `json:"message_content,omitempty"`
	PrevScrollNumber int                `json:"prev_scroll_number,omitempty"`
}

// RenameGroupRequest 修改群名。
type RenameGroupRequest struct {
	OldGroupName *string `json:"old_group_name,omitempty"`
	NewGroupName *string `json:"new_group_name,omitempty"`
}

// SaveAvatarRequest 保存头像到指定路径。
type SaveAvatarRequest struct {
	Path *string `json:"path,omitempty"`
}

// SearchConversationRequest 搜索会话。
type SearchConversationRequest struct {
	Who *string `json:"who,omitempty"`
}

// SendEmojiRequest 发送表情。emoji 与 index 二选一（优先使用 index）。
type SendEmojiRequest struct {
	Who     *string  `json:"who,omitempty"`
	Emoji   *string  `json:"emoji,omitempty"`
	Index   *int     `json:"index,omitempty"`
	AtUsers []string `json:"at_users,omitempty"`
}

// SendFileRequest 发送文件。who 为空时发送给当前焦点聊天窗口。
type SendFileRequest struct {
	Who   *string  `json:"who,omitempty"`
	Files []string `json:"files,omitempty"`
}

// SendMessageRequest 发送文本消息。who 为空时发送给当前焦点聊天窗口。
type SendMessageRequest struct {
	Who     *string    `json:"who,omitempty"`
	Message *string    `json:"message,omitempty"`
	AtUsers []string   `json:"at_users,omitempty"`
	Refer   *ChatRefer `json:"refer,omitempty"`
}

// SendVoiceChatsRequest 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
type SendVoiceChatsRequest struct {
	Who     *string  `json:"who,omitempty"`
	Partner []string `json:"partner,omitempty"`
}

// SendVoiceMessageRequest 发送语音消息。who 为空时发送给当前焦点聊天窗口。
type SendVoiceMessageRequest struct {
	Who      *string `json:"who,omitempty"`
	FilePath *string `json:"file_path,omitempty"`
}

// SendVoiceMessageTtsRequest 文字转语音发送。
type SendVoiceMessageTtsRequest struct {
	Who             *string       `json:"who,omitempty"`
	APIKey          *string       `json:"api_key,omitempty"`
	Message         *string       `json:"message,omitempty"`
	Options         *VoiceOptions `json:"options,omitempty"`
	OptimizeWithLLM bool          `json:"optimize_with_llm,omitempty"`
}

// SetDoNotDisturbRequest 设置会话免打扰。who 为空时作用于当前窗口。
type SetDoNotDisturbRequest struct {
	Who     *string `json:"who,omitempty"`
	Setting bool    `json:"setting"`
}

// SetTopMostRequest 设置会话置顶。who 为空时作用于当前窗口。
type SetTopMostRequest struct {
	Who     *string `json:"who,omitempty"`
	Setting bool    `json:"setting"`
}

// Size 大小（整数）。
type Size struct {
	IsEmpty bool `json:"is_empty,omitempty"`
	Width   int  `json:"width,omitempty"`
	Height  int  `json:"height,omitempty"`
}

// SizeF 大小（浮点数）。
type SizeF struct {
	IsEmpty bool    `json:"is_empty,omitempty"`
	Width   float64 `json:"width,omitempty"`
	Height  float64 `json:"height,omitempty"`
}

// SubWindowRequest 打开/关闭指定好友或群的子窗口。
type SubWindowRequest struct {
	Who *string `json:"who,omitempty"`
}

// SwitchNavigationRequest 切换/关闭导航栏窗口。
type SwitchNavigationRequest struct {
	NavigationType NavigationType `json:"navigation_type,omitempty"`
}

// TapRequest 拍一拍。
type TapRequest struct {
	Who              *string `json:"who,omitempty"`
	PrevScrollNumber int     `json:"prev_scroll_number,omitempty"`
}

// UpdateGroupNoticeRequest 更新群公告。
type UpdateGroupNoticeRequest struct {
	GroupName   *string `json:"group_name,omitempty"`
	GroupNotice *string `json:"group_notice,omitempty"`
}

// VoiceOptions 语音合成选项。
type VoiceOptions struct {
	Voice                *string  `json:"voice,omitempty"`
	Model                *string  `json:"model,omitempty"`
	LanguageType         *string  `json:"language_type,omitempty"`
	Instructions         *string  `json:"instructions,omitempty"`
	Volume               *int     `json:"volume,omitempty"`
	Rate                 *float64 `json:"rate,omitempty"`
	Pitch                *float64 `json:"pitch,omitempty"`
	Seed                 *int     `json:"seed,omitempty"`
	OptimizeInstructions bool     `json:"optimize_instructions,omitempty"`
	IsStream             bool     `json:"is_stream,omitempty"`
}

// WhoRequest 仅携带 who（可为空，作用于当前焦点窗口）的请求。
type WhoRequest struct {
	Who *string `json:"who,omitempty"`
}
