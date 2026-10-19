// ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）
package client

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"net/url"
	"strconv"
	"strings"
)

const apiPathPrefix = "/api/v1"

// Client is a generated HTTP client for the WECHATAUTO.SDK API.
//
// BaseURL is the server root (e.g. "http://localhost:5000"); the client appends
// the /api/v1 prefix and each endpoint path automatically. APIKey is the value
// of App.json -> Http.ApiKey; when non-empty it is sent as the X-API-Key header.
// HTTPClient may be injected for custom transports/timeouts.
type Client struct {
	BaseURL    string
	APIKey     string
	HTTPClient *http.Client
}

// NewClient constructs a Client. If httpClient is nil, http.DefaultClient is used.
func NewClient(baseURL, apiKey string, httpClient *http.Client) *Client {
	if httpClient == nil {
		httpClient = http.DefaultClient
	}
	return &Client{
		BaseURL:    strings.TrimRight(baseURL, "/"),
		APIKey:     apiKey,
		HTTPClient: httpClient,
	}
}

// do performs the request and returns the raw response body.
func (c *Client) do(method, path string, body any, query url.Values) (json.RawMessage, error) {
	var bodyReader io.Reader
	if body != nil {
		b, err := json.Marshal(body)
		if err != nil {
			return nil, fmt.Errorf("marshal request body: %w", err)
		}
		bodyReader = bytes.NewReader(b)
	}

	u := c.BaseURL + path
	if len(query) > 0 {
		u += "?" + query.Encode()
	}

	req, err := http.NewRequest(method, u, bodyReader)
	if err != nil {
		return nil, fmt.Errorf("build request: %w", err)
	}
	if body != nil {
		req.Header.Set("Content-Type", "application/json")
	}
	if c.APIKey != "" {
		req.Header.Set("X-API-Key", c.APIKey)
	}

	resp, err := c.HTTPClient.Do(req)
	if err != nil {
		return nil, fmt.Errorf("do request: %w", err)
	}
	defer resp.Body.Close()

	data, err := io.ReadAll(resp.Body)
	if err != nil {
		return nil, fmt.Errorf("read response body: %w", err)
	}
	if resp.StatusCode < 200 || resp.StatusCode > 299 {
		return nil, fmt.Errorf("unexpected status %d: %s", resp.StatusCode, string(data))
	}
	return json.RawMessage(data), nil
}

// ---------------------------------------------------------------------------
// Clients
// ---------------------------------------------------------------------------

// ListClients 获取全部微信客户端的概要信息列表。
func (c *Client) ListClients() (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients", nil, nil)
}

// GetClient 获取指定微信客户端的概要信息。
func (c *Client) GetClient(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID), nil, nil)
}

// GetClientOwnerInfo 获取本微信的个人信息。
func (c *Client) GetClientOwnerInfo(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/owner-info", nil, nil)
}

// SaveClientAvatar 保存本微信头像到指定路径。
func (c *Client) SaveClientAvatar(clientID string, req SaveAvatarRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/avatar", req, nil)
}

// ClickNotifyIcon 点击任务栏微信图标，按索引或微信名称定位。
func (c *Client) ClickNotifyIcon(clientID string, req NotifyIconClickRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/notify-icon/click", req, nil)
}

// ---------------------------------------------------------------------------
// Conversations
// ---------------------------------------------------------------------------

// ListConversations 获取会话列表所有会话的标题。
func (c *Client) ListConversations(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations", nil, nil)
}

// ListVisibleConversationTitles 获取会话列表可见会话的标题。
func (c *Client) ListVisibleConversationTitles(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/visible-titles", nil, nil)
}

// ListVisibleConversations 获取可见会话列表。
func (c *Client) ListVisibleConversations(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/visible", nil, nil)
}

// SearchConversation 搜索好友/群聊。
func (c *Client) SearchConversation(clientID string, req SearchConversationRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/search", req, nil)
}

// LocateConversation 定位会话，滚动会话列表使指定会话可见。
func (c *Client) LocateConversation(clientID string, req LocateConversationRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/locate", req, nil)
}

// SetConversationDoNotDisturb 设置会话消息免打扰。
func (c *Client) SetConversationDoNotDisturb(clientID string, req SetDoNotDisturbRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/set-do-not-disturb", req, nil)
}

// SetConversationTopMost 设置会话置顶。
func (c *Client) SetConversationTopMost(clientID string, req SetTopMostRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/conversations/set-top-most", req, nil)
}

// ---------------------------------------------------------------------------
// Friends
// ---------------------------------------------------------------------------

// OpenAddFriendWindow 打开新增朋友窗口。
func (c *Client) OpenAddFriendWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/add-window/open", nil, nil)
}

// CloseAddFriendWindow 关闭新增朋友窗口。
func (c *Client) CloseAddFriendWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/add-window/close", nil, nil)
}

// AddFriends 通过手机号码/微信号查找并添加好友。
func (c *Client) AddFriends(clientID string, req AddFriendsRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/add", req, nil)
}

// ListFriends 获取所有好友的信息列表（默认优先从缓存读取）。
func (c *Client) ListFriends(clientID string, fromCache ...bool) (json.RawMessage, error) {
	cache := true
	if len(fromCache) > 0 {
		cache = fromCache[0]
	}
	q := url.Values{}
	q.Set("fromCache", strconv.FormatBool(cache))
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends", nil, q)
}

// ListFriendNames 获取所有好友名称列表（有备注优先备注名）。
func (c *Client) ListFriendNames(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/names", nil, nil)
}

// DeleteFriend 从通讯录移除好友。
func (c *Client) DeleteFriend(clientID, nickName string) (json.RawMessage, error) {
	return c.do(http.MethodDelete, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/"+url.PathEscape(nickName), nil, nil)
}

// ListFriendsCache 获取缓存中的全部好友信息列表。
func (c *Client) ListFriendsCache(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache", nil, nil)
}

// UpsertFriendCache 手动新增或修改缓存中的好友对象。
func (c *Client) UpsertFriendCache(clientID string, friend FriendInfo) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache", friend, nil)
}

// ListFriendsCacheByName 按名称从缓存获取好友列表（适用于昵称重复场景）。
func (c *Client) ListFriendsCacheByName(clientID, who string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache/by-name/"+url.PathEscape(who), nil, nil)
}

// GetFriendCacheByWxid 按 wxid 从缓存获取好友信息。
func (c *Client) GetFriendCacheByWxid(clientID, wxid string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache/by-wxid/"+url.PathEscape(wxid), nil, nil)
}

// DeleteFriendCacheByWxid 按 wxid 从缓存移除好友。
func (c *Client) DeleteFriendCacheByWxid(clientID, wxid string) (json.RawMessage, error) {
	return c.do(http.MethodDelete, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache/by-wxid/"+url.PathEscape(wxid), nil, nil)
}

// GetFriendCache 按名称从缓存获取单个好友信息。
func (c *Client) GetFriendCache(clientID, who string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache/"+url.PathEscape(who), nil, nil)
}

// DeleteFriendCache 按名称从缓存移除好友。
func (c *Client) DeleteFriendCache(clientID, who string) (json.RawMessage, error) {
	return c.do(http.MethodDelete, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/friends/cache/"+url.PathEscape(who), nil, nil)
}

// ---------------------------------------------------------------------------
// Groups
// ---------------------------------------------------------------------------

// IsGroupOwner 判断群聊是否为自有群。
func (c *Client) IsGroupOwner(clientID, groupName string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/"+url.PathEscape(groupName)+"/is-owner", nil, nil)
}

// GetGroupOwner 获取群主昵称。
func (c *Client) GetGroupOwner(clientID, groupName string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/"+url.PathEscape(groupName)+"/owner", nil, nil)
}

// ListGroupMembers 获取群聊成员列表。
func (c *Client) ListGroupMembers(clientID string, groupName ...string) (json.RawMessage, error) {
	q := url.Values{}
	if len(groupName) > 0 && groupName[0] != "" {
		q.Set("groupName", groupName[0])
	}
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/members", nil, q)
}

// AddGroupMembers 添加群聊成员（适用于自有群）。
func (c *Client) AddGroupMembers(clientID string, req GroupMemberRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/members", req, nil)
}

// RemoveGroupMembers 移除群聊成员（适用于自有群）。
func (c *Client) RemoveGroupMembers(clientID string, req GroupMemberRequest) (json.RawMessage, error) {
	return c.do(http.MethodDelete, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/members", req, nil)
}

// QuitGroup 退出群聊。
func (c *Client) QuitGroup(clientID, groupName string, clearHistory ...bool) (json.RawMessage, error) {
	clear := true
	if len(clearHistory) > 0 {
		clear = clearHistory[0]
	}
	q := url.Values{}
	q.Set("clearHistory", strconv.FormatBool(clear))
	return c.do(http.MethodDelete, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/"+url.PathEscape(groupName), nil, q)
}

// CreateGroup 创建群聊，已存在则打开群聊。
func (c *Client) CreateGroup(clientID string, req CreateGroupRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups", req, nil)
}

// RenameGroup 修改群名（适用于自有群）。
func (c *Client) RenameGroup(clientID string, req RenameGroupRequest) (json.RawMessage, error) {
	return c.do(http.MethodPut, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/name", req, nil)
}

// ChangeGroupNickName 修改自己在群中的昵称。
func (c *Client) ChangeGroupNickName(clientID string, req ChangeGroupNickNameRequest) (json.RawMessage, error) {
	return c.do(http.MethodPut, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/nickname", req, nil)
}

// ChangeGroupMemo 修改群备注（仅自己可见）。
func (c *Client) ChangeGroupMemo(clientID string, req ChangeGroupMemoRequest) (json.RawMessage, error) {
	return c.do(http.MethodPut, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/memo", req, nil)
}

// UpdateGroupNotice 更新群公告（仅适用于自有群）。
func (c *Client) UpdateGroupNotice(clientID string, req UpdateGroupNoticeRequest) (json.RawMessage, error) {
	return c.do(http.MethodPut, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/notice", req, nil)
}

// InviteGroupMembers 邀请群聊成员（适用于外部群）。
func (c *Client) InviteGroupMembers(clientID string, req InviteGroupMemberRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/invite", req, nil)
}

// AddGroupMembersToFriends 把群聊成员加为自己的好友（适用于外部群）。
func (c *Client) AddGroupMembersToFriends(clientID string, req AddGroupMemberToFriendsRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/groups/add-members-to-friends", req, nil)
}

// ---------------------------------------------------------------------------
// Messages
// ---------------------------------------------------------------------------

// GetMessageTitle 获取当前聊天窗口的标题对象。
func (c *Client) GetMessageTitle(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/title", nil, nil)
}

// GetMessageOnlyTitle 获取当前聊天窗口的标题名称。
func (c *Client) GetMessageOnlyTitle(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/only-title", nil, nil)
}

// FocusMessageInput 聚焦当前窗口的消息输入区。
func (c *Client) FocusMessageInput(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/focus-input", nil, nil)
}

// SendMessage 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
func (c *Client) SendMessage(clientID string, req SendMessageRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send", req, nil)
}

// SendFile 发送文件。
func (c *Client) SendFile(clientID string, req SendFileRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-file", req, nil)
}

// SendEmoji 发送表情（按名称/描述/索引）。
func (c *Client) SendEmoji(clientID string, req SendEmojiRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-emoji", req, nil)
}

// SendVoiceChat 发起单人语音聊天。
func (c *Client) SendVoiceChat(clientID string, req WhoRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-voice-chat", req, nil)
}

// SendVideoChat 发起单人视频聊天。
func (c *Client) SendVideoChat(clientID string, req WhoRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-video-chat", req, nil)
}

// SendVoiceChats 发起多人语音聊天（群聊）。
func (c *Client) SendVoiceChats(clientID string, req SendVoiceChatsRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-voice-chats", req, nil)
}

// SendVoiceMessage 发送语音消息（需虚拟声卡 Cable input/Cable output）。
func (c *Client) SendVoiceMessage(clientID string, req SendVoiceMessageRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-voice-message", req, nil)
}

// SendVoiceMessageTts 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
func (c *Client) SendVoiceMessageTts(clientID string, req SendVoiceMessageTtsRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/send-voice-message-tts", req, nil)
}

// GetMessageHistory 获取聊天历史（支持按日期或时间段查询）。
func (c *Client) GetMessageHistory(clientID, who, date, startDate, endDate string) (json.RawMessage, error) {
	q := url.Values{}
	if who != "" {
		q.Set("who", who)
	}
	if date != "" {
		q.Set("date", date)
	}
	if startDate != "" {
		q.Set("start_date", startDate)
	}
	if endDate != "" {
		q.Set("end_date", endDate)
	}
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/history", nil, q)
}

// Tap 拍一拍当前聊天窗口的好友。
func (c *Client) Tap(clientID string, req TapRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/tap", req, nil)
}

// ReferenceMessage 引用当前窗口的好友消息。
func (c *Client) ReferenceMessage(clientID string, req ReferenceRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/reference", req, nil)
}

// ReferenceLastMessage 引用当前窗口最后一条消息。
func (c *Client) ReferenceLastMessage(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/messages/reference-last", nil, nil)
}

// ---------------------------------------------------------------------------
// Navigation
// ---------------------------------------------------------------------------

// SwitchNavigation 切换导航栏。
func (c *Client) SwitchNavigation(clientID string, req SwitchNavigationRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/navigation/switch", req, nil)
}

// CloseNavigation 关闭通过导航栏打开的窗口。
func (c *Client) CloseNavigation(clientID string, req SwitchNavigationRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/navigation/close", req, nil)
}

// ---------------------------------------------------------------------------
// Window
// ---------------------------------------------------------------------------

// MaximizeWindow 最大化微信窗口。
func (c *Client) MaximizeWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/maximize", nil, nil)
}

// RestoreWindow 还原微信窗口。
func (c *Client) RestoreWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/restore", nil, nil)
}

// PinWindow 置顶微信窗口。
func (c *Client) PinWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/pin", nil, nil)
}

// UnpinWindow 取消置顶微信窗口。
func (c *Client) UnpinWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/unpin", nil, nil)
}

// FocusWindow 使主窗口获取焦点。
func (c *Client) FocusWindow(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/focus", nil, nil)
}

// CloseSearchWindow 关闭查询窗口（已打开则关闭）。
func (c *Client) CloseSearchWindow(clientID string, req SubWindowRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/close-search-window", req, nil)
}

// OpenSubWindow 打开指定对象的子窗口。
func (c *Client) OpenSubWindow(clientID string, req SubWindowRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/open-sub-window", req, nil)
}

// CloseSubWindow 关闭指定对象的子窗口（已存在则关闭）。
func (c *Client) CloseSubWindow(clientID string, req SubWindowRequest) (json.RawMessage, error) {
	return c.do(http.MethodPost, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/close-sub-window", req, nil)
}

// GetWindowHandler 获取本微信窗口句柄。
func (c *Client) GetWindowHandler(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/handler", nil, nil)
}

// GetWindowProcessId 获取本微信窗口进程 ID。
func (c *Client) GetWindowProcessId(clientID string) (json.RawMessage, error) {
	return c.do(http.MethodGet, apiPathPrefix+"/clients/"+url.PathEscape(clientID)+"/window/process-id", nil, nil)
}
