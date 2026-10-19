/*
⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）
*/

import {
  ChatType,
  FriendSearchResultEnums,
  NavigationType,
  PixelFormat,
} from './typedefs.js';

export {
  ChatType,
  FriendSearchResultEnums,
  NavigationType,
  PixelFormat,
} from './typedefs.js';

const DEFAULT_BASE_URL = 'http://localhost:5000';

/**
 * HTTP client for the WECHATAUTO.SDK WeChat automation API.
 *
 * Every method returns a `Promise` of the parsed JSON response body
 * (object, array or primitive). Response bodies have no schema in the
 * OpenAPI spec, so no response types are invented here.
 *
 * @example
 * import { WeChatAutoClient } from 'wechatauto-client';
 * const client = new WeChatAutoClient('http://localhost:5000', 'my-api-key');
 * const clients = await client.getClients();
 */
export class WeChatAutoClient {
  /**
   * @param {string} [baseUrl='http://localhost:5000'] Server base URL (without /api/v1).
   * @param {string} [apiKey=''] X-API-Key value. When empty the header is omitted.
   * @param {typeof fetch} [fetchImpl=fetch] Injectable fetch implementation (Node 18+ global fetch by default).
   */
  constructor(baseUrl = DEFAULT_BASE_URL, apiKey = '', fetchImpl = fetch) {
    this.baseUrl = String(baseUrl || DEFAULT_BASE_URL).replace(/\/+$/, '');
    this.apiKey = apiKey || '';
    this._fetch = fetchImpl;
  }

  /**
   * Build a URL from a path template (path params already encoded).
   * @param {string} path Path beginning with "/".
   * @param {Record<string, string|number|boolean|undefined|null>} [query]
   * @returns {string}
   */
  _url(path, query) {
    let url = `${this.baseUrl}/api/v1${path}`;
    if (query) {
      const parts = [];
      for (const [key, value] of Object.entries(query)) {
        if (value === undefined || value === null) continue;
        parts.push(`${encodeURIComponent(key)}=${encodeURIComponent(String(value))}`);
      }
      if (parts.length > 0) url += `?${parts.join('&')}`;
    }
    return url;
  }

  /**
   * Perform a request.
   * @param {string} method HTTP verb.
   * @param {string} path Path template.
   * @param {object} [options]
   * @param {Record<string, string|number|boolean|undefined|null>} [options.query]
   * @param {object} [options.body] Request body (plain object, JSON-serialized).
   * @returns {Promise<any>} Parsed JSON response body.
   */
  async _request(method, path, { query, body } = {}) {
    const headers = {};
    if (this.apiKey) headers['X-API-Key'] = this.apiKey;

    const init = { method, headers };
    if (body !== undefined) {
      headers['Content-Type'] = 'application/json';
      init.body = JSON.stringify(body);
    }

    const res = await this._fetch(this._url(path, query), init);
    if (!res.ok) {
      const text = await res.text().catch(() => '');
      throw new Error(`HTTP ${res.status} ${res.statusText}: ${text}`);
    }
    return res.json();
  }

  /* ------------------------------------------------------------------ *
   * Clients
   * ------------------------------------------------------------------ */

  /** 获取全部微信客户端的概要信息列表。 */
  getClients() {
    return this._request('GET', '/clients');
  }

  /** 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。 */
  getClient(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}`);
  }

  /** 获取本微信的个人信息（头像文件位置、wxid、昵称）。 */
  getClientOwnerInfo(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/owner-info`);
  }

  /** 保存本微信头像到指定路径。 @param {import('./typedefs.js').SaveAvatarRequest} body */
  saveAvatar(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/avatar`, { body });
  }

  /** 点击任务栏微信图标，按索引或微信名称定位。 @param {import('./typedefs.js').NotifyIconClickRequest} body */
  clickNotifyIcon(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/notify-icon/click`, { body });
  }

  /* ------------------------------------------------------------------ *
   * Conversations
   * ------------------------------------------------------------------ */

  /** 获取会话列表所有会话的标题。 */
  getConversations(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/conversations`);
  }

  /** 获取会话列表可见会话的标题。 */
  getConversationVisibleTitles(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/conversations/visible-titles`);
  }

  /** 获取可见会话列表（含名称、未读消息数、头像等）。 */
  getVisibleConversations(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/conversations/visible`);
  }

  /** 搜索好友/群聊。 @param {import('./typedefs.js').SearchConversationRequest} body */
  searchConversation(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/conversations/search`, { body });
  }

  /** 定位会话，滚动会话列表使指定会话可见。 @param {import('./typedefs.js').LocateConversationRequest} body */
  locateConversation(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/conversations/locate`, { body });
  }

  /** 设置会话消息免打扰。 @param {import('./typedefs.js').SetDoNotDisturbRequest} body */
  setConversationDoNotDisturb(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/conversations/set-do-not-disturb`, { body });
  }

  /** 设置会话置顶。 @param {import('./typedefs.js').SetTopMostRequest} body */
  setConversationTopMost(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/conversations/set-top-most`, { body });
  }

  /* ------------------------------------------------------------------ *
   * Friends
   * ------------------------------------------------------------------ */

  /** 打开新增朋友窗口。 */
  openAddFriendWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/friends/add-window/open`);
  }

  /** 关闭新增朋友窗口。 */
  closeAddFriendWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/friends/add-window/close`);
  }

  /** 通过手机号码/微信号查找并添加好友。 @param {import('./typedefs.js').AddFriendsRequest} body */
  addFriends(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/friends/add`, { body });
  }

  /** 获取所有好友的信息列表（默认优先从缓存读取）。 @param {boolean} [fromCache=true] */
  getFriends(clientId, fromCache = true) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends`, { query: { fromCache } });
  }

  /** 获取所有好友名称列表（有备注优先备注名）。 */
  getFriendNames(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends/names`);
  }

  /** 从通讯录移除好友。 */
  deleteFriend(clientId, nickName) {
    return this._request('DELETE', `/clients/${encodeURIComponent(clientId)}/friends/${encodeURIComponent(nickName)}`);
  }

  /** 获取缓存中的全部好友信息列表。 */
  getFriendsCache(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends/cache`);
  }

  /** 手动新增或修改缓存中的好友对象。 @param {import('./typedefs.js').FriendInfo} body */
  upsertFriendCache(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/friends/cache`, { body });
  }

  /** 按名称从缓存获取好友列表（适用于昵称重复场景）。 */
  getFriendsCacheByName(clientId, who) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends/cache/by-name/${encodeURIComponent(who)}`);
  }

  /** 按 wxid 从缓存获取好友信息。 */
  getFriendCacheByWxid(clientId, wxid) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends/cache/by-wxid/${encodeURIComponent(wxid)}`);
  }

  /** 按 wxid 从缓存移除好友。 */
  deleteFriendCacheByWxid(clientId, wxid) {
    return this._request('DELETE', `/clients/${encodeURIComponent(clientId)}/friends/cache/by-wxid/${encodeURIComponent(wxid)}`);
  }

  /** 按名称从缓存获取单个好友信息。 */
  getFriendCache(clientId, who) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/friends/cache/${encodeURIComponent(who)}`);
  }

  /** 按名称从缓存移除好友。 */
  deleteFriendCache(clientId, who) {
    return this._request('DELETE', `/clients/${encodeURIComponent(clientId)}/friends/cache/${encodeURIComponent(who)}`);
  }

  /* ------------------------------------------------------------------ *
   * Groups
   * ------------------------------------------------------------------ */

  /** 判断群聊是否为自有群。 */
  isGroupOwner(clientId, groupName) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/groups/${encodeURIComponent(groupName)}/is-owner`);
  }

  /** 获取群主昵称。 */
  getGroupOwner(clientId, groupName) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/groups/${encodeURIComponent(groupName)}/owner`);
  }

  /** 获取群聊成员列表。 @param {string} [groupName] */
  getGroupMembers(clientId, groupName) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/groups/members`, { query: { groupName } });
  }

  /** 添加群聊成员（适用于自有群）。 @param {import('./typedefs.js').GroupMemberRequest} body */
  addGroupMembers(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/groups/members`, { body });
  }

  /** 移除群聊成员（适用于自有群）。 @param {import('./typedefs.js').GroupMemberRequest} body */
  removeGroupMembers(clientId, body) {
    return this._request('DELETE', `/clients/${encodeURIComponent(clientId)}/groups/members`, { body });
  }

  /** 退出群聊。 @param {boolean} [clearHistory=true] */
  leaveGroup(clientId, groupName, clearHistory = true) {
    return this._request('DELETE', `/clients/${encodeURIComponent(clientId)}/groups/${encodeURIComponent(groupName)}`, { query: { clearHistory } });
  }

  /** 创建群聊，已存在则打开群聊。 @param {import('./typedefs.js').CreateGroupRequest} body */
  createGroup(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/groups`, { body });
  }

  /** 修改群名（适用于自有群）。 @param {import('./typedefs.js').RenameGroupRequest} body */
  renameGroup(clientId, body) {
    return this._request('PUT', `/clients/${encodeURIComponent(clientId)}/groups/name`, { body });
  }

  /** 修改自己在群中的昵称。 @param {import('./typedefs.js').ChangeGroupNickNameRequest} body */
  changeGroupNickName(clientId, body) {
    return this._request('PUT', `/clients/${encodeURIComponent(clientId)}/groups/nickname`, { body });
  }

  /** 修改群备注（仅自己可见）。 @param {import('./typedefs.js').ChangeGroupMemoRequest} body */
  changeGroupMemo(clientId, body) {
    return this._request('PUT', `/clients/${encodeURIComponent(clientId)}/groups/memo`, { body });
  }

  /** 更新群公告（仅适用于自有群）。 @param {import('./typedefs.js').UpdateGroupNoticeRequest} body */
  updateGroupNotice(clientId, body) {
    return this._request('PUT', `/clients/${encodeURIComponent(clientId)}/groups/notice`, { body });
  }

  /** 邀请群聊成员（适用于外部群）。 @param {import('./typedefs.js').InviteGroupMemberRequest} body */
  inviteGroupMembers(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/groups/invite`, { body });
  }

  /** 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。 @param {import('./typedefs.js').AddGroupMemberToFriendsRequest} body */
  addGroupMembersToFriends(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/groups/add-members-to-friends`, { body });
  }

  /* ------------------------------------------------------------------ *
   * Messages
   * ------------------------------------------------------------------ */

  /** 获取当前聊天窗口的标题对象。 */
  getMessagesTitle(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/messages/title`);
  }

  /** 获取当前聊天窗口的标题名称。 */
  getMessagesOnlyTitle(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/messages/only-title`);
  }

  /** 聚焦当前窗口的消息输入区。 */
  focusMessageInput(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/focus-input`);
  }

  /** 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。 @param {import('./typedefs.js').SendMessageRequest} body */
  sendMessage(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send`, { body });
  }

  /** 发送文件。 @param {import('./typedefs.js').SendFileRequest} body */
  sendFile(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-file`, { body });
  }

  /** 发送表情（按名称/描述/索引）。 @param {import('./typedefs.js').SendEmojiRequest} body */
  sendEmoji(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-emoji`, { body });
  }

  /** 发起单人语音聊天。 @param {import('./typedefs.js').WhoRequest} body */
  sendVoiceChat(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-voice-chat`, { body });
  }

  /** 发起单人视频聊天。 @param {import('./typedefs.js').WhoRequest} body */
  sendVideoChat(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-video-chat`, { body });
  }

  /** 发起多人语音聊天（群聊）。 @param {import('./typedefs.js').SendVoiceChatsRequest} body */
  sendVoiceChats(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-voice-chats`, { body });
  }

  /** 发送语音消息（需虚拟声卡 Cable input/Cable output）。 @param {import('./typedefs.js').SendVoiceMessageRequest} body */
  sendVoiceMessage(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-voice-message`, { body });
  }

  /** 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。 @param {import('./typedefs.js').SendVoiceMessageTtsRequest} body */
  sendVoiceMessageTts(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/send-voice-message-tts`, { body });
  }

  /** 获取聊天历史（支持按日期或时间段查询）。
   * @param {string} [who]
   * @param {string} [date]
   * @param {string} [startDate]
   * @param {string} [endDate]
   */
  getMessagesHistory(clientId, who, date, startDate, endDate) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/messages/history`, {
      query: { who, date, start_date: startDate, end_date: endDate },
    });
  }

  /** 拍一拍当前聊天窗口的好友。 @param {import('./typedefs.js').TapRequest} body */
  tapMessage(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/tap`, { body });
  }

  /** 引用当前窗口的好友消息。 @param {import('./typedefs.js').ReferenceRequest} body */
  referenceMessage(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/reference`, { body });
  }

  /** 引用当前窗口最后一条消息。 */
  referenceLastMessage(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/messages/reference-last`);
  }

  /* ------------------------------------------------------------------ *
   * Navigation
   * ------------------------------------------------------------------ */

  /** 切换导航栏。 @param {import('./typedefs.js').SwitchNavigationRequest} body */
  switchNavigation(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/navigation/switch`, { body });
  }

  /** 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。 @param {import('./typedefs.js').SwitchNavigationRequest} body */
  closeNavigation(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/navigation/close`, { body });
  }

  /* ------------------------------------------------------------------ *
   * Window
   * ------------------------------------------------------------------ */

  /** 最大化微信窗口。 */
  maximizeWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/maximize`);
  }

  /** 还原微信窗口。 */
  restoreWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/restore`);
  }

  /** 置顶微信窗口。 */
  pinWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/pin`);
  }

  /** 取消置顶微信窗口。 */
  unpinWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/unpin`);
  }

  /** 使主窗口获取焦点。 */
  focusWindow(clientId) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/focus`);
  }

  /** 关闭查询窗口（已打开则关闭）。 @param {import('./typedefs.js').SubWindowRequest} body */
  closeSearchWindow(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/close-search-window`, { body });
  }

  /** 打开指定对象的子窗口。 @param {import('./typedefs.js').SubWindowRequest} body */
  openSubWindow(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/open-sub-window`, { body });
  }

  /** 关闭指定对象的子窗口（已存在则关闭）。 @param {import('./typedefs.js').SubWindowRequest} body */
  closeSubWindow(clientId, body) {
    return this._request('POST', `/clients/${encodeURIComponent(clientId)}/window/close-sub-window`, { body });
  }

  /** 获取本微信窗口句柄。 */
  getWindowHandler(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/window/handler`);
  }

  /** 获取本微信窗口进程 ID。 */
  getWindowProcessId(clientId) {
    return this._request('GET', `/clients/${encodeURIComponent(clientId)}/window/process-id`);
  }
}

export default WeChatAutoClient;
