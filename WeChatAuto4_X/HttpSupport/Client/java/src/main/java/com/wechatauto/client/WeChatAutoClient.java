/**
 * ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）
 */
package com.wechatauto.client;

import java.io.IOException;
import java.net.URI;
import java.net.URLEncoder;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.charset.StandardCharsets;
import java.util.LinkedHashMap;
import java.util.Map;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.wechatauto.client.models.AddFriendsRequest;
import com.wechatauto.client.models.AddGroupMemberToFriendsRequest;
import com.wechatauto.client.models.ChangeGroupMemoRequest;
import com.wechatauto.client.models.ChangeGroupNickNameRequest;
import com.wechatauto.client.models.CreateGroupRequest;
import com.wechatauto.client.models.FriendInfo;
import com.wechatauto.client.models.GroupMemberRequest;
import com.wechatauto.client.models.InviteGroupMemberRequest;
import com.wechatauto.client.models.LocateConversationRequest;
import com.wechatauto.client.models.NotifyIconClickRequest;
import com.wechatauto.client.models.ReferenceRequest;
import com.wechatauto.client.models.RenameGroupRequest;
import com.wechatauto.client.models.SaveAvatarRequest;
import com.wechatauto.client.models.SearchConversationRequest;
import com.wechatauto.client.models.SendEmojiRequest;
import com.wechatauto.client.models.SendFileRequest;
import com.wechatauto.client.models.SendMessageRequest;
import com.wechatauto.client.models.SendVoiceChatsRequest;
import com.wechatauto.client.models.SendVoiceMessageRequest;
import com.wechatauto.client.models.SendVoiceMessageTtsRequest;
import com.wechatauto.client.models.SetDoNotDisturbRequest;
import com.wechatauto.client.models.SetTopMostRequest;
import com.wechatauto.client.models.SubWindowRequest;
import com.wechatauto.client.models.SwitchNavigationRequest;
import com.wechatauto.client.models.TapRequest;
import com.wechatauto.client.models.UpdateGroupNoticeRequest;
import com.wechatauto.client.models.WhoRequest;

/**
 * Java HTTP client for the WECHATAUTO.SDK WeChat automation API.
 *
 * <p>All methods return the raw response body as a {@link String}. Response DTOs
 * are intentionally not modelled because the API responses have no schema.</p>
 *
 * <p>The optional {@code X-API-Key} header is only sent when a non-empty API key
 * is configured. The default base URL is {@code http://localhost:5000}.</p>
 */
public class WeChatAutoClient {

    public static final String DEFAULT_BASE_URL = "http://localhost:5000";
    public static final String API_KEY_HEADER = "X-API-Key";

    private final String baseUrl;
    private final String apiKey;
    private final HttpClient httpClient;
    private final ObjectMapper objectMapper = new ObjectMapper();

    /** Convenience constructor using the default base URL and no API key. */
    public WeChatAutoClient() {
        this(DEFAULT_BASE_URL, "");
    }

    /** @param baseUrl base URL of the API, e.g. {@code http://localhost:5000}. */
    public WeChatAutoClient(String baseUrl, String apiKey) {
        this(baseUrl, apiKey, HttpClient.newHttpClient());
    }

    /**
     * @param baseUrl    base URL of the API, e.g. {@code http://localhost:5000}.
     * @param apiKey     value of the {@code X-API-Key} header (empty disables the header).
     * @param httpClient injectable {@link HttpClient} (useful for testing/tuning).
     */
    public WeChatAutoClient(String baseUrl, String apiKey, HttpClient httpClient) {
        this.baseUrl = baseUrl == null ? DEFAULT_BASE_URL : trimTrailingSlash(baseUrl);
        this.apiKey = apiKey == null ? "" : apiKey;
        this.httpClient = httpClient == null ? HttpClient.newHttpClient() : httpClient;
    }

    // ------------------------------------------------------------------
    // Clients
    // ------------------------------------------------------------------

    /** 获取全部微信客户端的概要信息列表。 */
    public String getClients() {
        return execute("GET", "/api/v1/clients", null, null);
    }

    /** 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。 */
    public String getClient(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId), null, null);
    }

    /** 获取本微信的个人信息（头像文件位置、wxid、昵称）。 */
    public String getClientOwnerInfo(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/owner-info", null, null);
    }

    /** 保存本微信头像到指定路径。 */
    public String saveAvatar(String clientId, SaveAvatarRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/avatar", null, request);
    }

    /** 点击任务栏微信图标，按索引或微信名称定位。 */
    public String clickNotifyIcon(String clientId, NotifyIconClickRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/notify-icon/click", null, request);
    }

    // ------------------------------------------------------------------
    // Conversations
    // ------------------------------------------------------------------

    /** 获取会话列表所有会话的标题。 */
    public String getConversations(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/conversations", null, null);
    }

    /** 获取会话列表可见会话的标题。 */
    public String getVisibleConversationTitles(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/conversations/visible-titles", null, null);
    }

    /** 获取可见会话列表（含名称、未读消息数、头像等）。 */
    public String getVisibleConversations(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/conversations/visible", null, null);
    }

    /** 搜索好友/群聊。 */
    public String searchConversation(String clientId, SearchConversationRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/conversations/search", null, request);
    }

    /** 定位会话，滚动会话列表使指定会话可见。 */
    public String locateConversation(String clientId, LocateConversationRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/conversations/locate", null, request);
    }

    /** 设置会话消息免打扰。 */
    public String setConversationDoNotDisturb(String clientId, SetDoNotDisturbRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/conversations/set-do-not-disturb", null, request);
    }

    /** 设置会话置顶。 */
    public String setConversationTopMost(String clientId, SetTopMostRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/conversations/set-top-most", null, request);
    }

    // ------------------------------------------------------------------
    // Friends
    // ------------------------------------------------------------------

    /** 打开新增朋友窗口。 */
    public String openAddFriendWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/friends/add-window/open", null, null);
    }

    /** 关闭新增朋友窗口。 */
    public String closeAddFriendWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/friends/add-window/close", null, null);
    }

    /** 通过手机号码/微信号查找并添加好友。 */
    public String addFriends(String clientId, AddFriendsRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/friends/add", null, request);
    }

    /** 获取所有好友的信息列表（默认优先从缓存读取）。 */
    public String getFriends(String clientId) {
        return getFriends(clientId, true);
    }

    /**
     * 获取所有好友的信息列表。
     *
     * @param fromCache 是否优先从缓存读取（默认 true）。
     */
    public String getFriends(String clientId, boolean fromCache) {
        Map<String, String> query = new LinkedHashMap<>();
        query.put("fromCache", Boolean.toString(fromCache));
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends", query, null);
    }

    /** 获取所有好友名称列表（有备注优先备注名）。 */
    public String getFriendNames(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends/names", null, null);
    }

    /** 从通讯录移除好友。 */
    public String deleteFriend(String clientId, String nickName) {
        return execute("DELETE", "/api/v1/clients/" + encode(clientId) + "/friends/" + encode(nickName), null, null);
    }

    /** 获取缓存中的全部好友信息列表。 */
    public String getFriendsCache(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends/cache", null, null);
    }

    /** 手动新增或修改缓存中的好友对象。 */
    public String upsertFriendCache(String clientId, FriendInfo friend) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/friends/cache", null, friend);
    }

    /** 按名称从缓存获取好友列表（适用于昵称重复场景）。 */
    public String getFriendsCacheByName(String clientId, String who) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends/cache/by-name/" + encode(who), null, null);
    }

    /** 按 wxid 从缓存获取好友信息。 */
    public String getFriendCacheByWxid(String clientId, String wxid) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends/cache/by-wxid/" + encode(wxid), null, null);
    }

    /** 按 wxid 从缓存移除好友。 */
    public String deleteFriendCacheByWxid(String clientId, String wxid) {
        return execute("DELETE", "/api/v1/clients/" + encode(clientId) + "/friends/cache/by-wxid/" + encode(wxid), null, null);
    }

    /** 按名称从缓存获取单个好友信息。 */
    public String getFriendCache(String clientId, String who) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/friends/cache/" + encode(who), null, null);
    }

    /** 按名称从缓存移除好友。 */
    public String deleteFriendCache(String clientId, String who) {
        return execute("DELETE", "/api/v1/clients/" + encode(clientId) + "/friends/cache/" + encode(who), null, null);
    }

    // ------------------------------------------------------------------
    // Groups
    // ------------------------------------------------------------------

    /** 判断群聊是否为自有群。 */
    public String isGroupOwner(String clientId, String groupName) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/groups/" + encode(groupName) + "/is-owner", null, null);
    }

    /** 获取群主昵称。 */
    public String getGroupOwner(String clientId, String groupName) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/groups/" + encode(groupName) + "/owner", null, null);
    }

    /** 获取群聊成员列表。 */
    public String getGroupMembers(String clientId, String groupName) {
        Map<String, String> query = new LinkedHashMap<>();
        if (groupName != null) {
            query.put("groupName", groupName);
        }
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/groups/members", query, null);
    }

    /** 添加群聊成员（适用于自有群）。 */
    public String addGroupMembers(String clientId, GroupMemberRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/groups/members", null, request);
    }

    /** 移除群聊成员（适用于自有群）。 */
    public String removeGroupMembers(String clientId, GroupMemberRequest request) {
        return execute("DELETE", "/api/v1/clients/" + encode(clientId) + "/groups/members", null, request);
    }

    /** 退出群聊（默认清除聊天历史）。 */
    public String exitGroup(String clientId, String groupName) {
        return exitGroup(clientId, groupName, true);
    }

    /**
     * 退出群聊。
     *
     * @param clearHistory 是否清除聊天历史（默认 true）。
     */
    public String exitGroup(String clientId, String groupName, boolean clearHistory) {
        Map<String, String> query = new LinkedHashMap<>();
        query.put("clearHistory", Boolean.toString(clearHistory));
        return execute("DELETE", "/api/v1/clients/" + encode(clientId) + "/groups/" + encode(groupName), query, null);
    }

    /** 创建群聊，已存在则打开群聊。 */
    public String createGroup(String clientId, CreateGroupRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/groups", null, request);
    }

    /** 修改群名（适用于自有群）。 */
    public String renameGroup(String clientId, RenameGroupRequest request) {
        return execute("PUT", "/api/v1/clients/" + encode(clientId) + "/groups/name", null, request);
    }

    /** 修改自己在群中的昵称。 */
    public String changeGroupNickName(String clientId, ChangeGroupNickNameRequest request) {
        return execute("PUT", "/api/v1/clients/" + encode(clientId) + "/groups/nickname", null, request);
    }

    /** 修改群备注（仅自己可见）。 */
    public String changeGroupMemo(String clientId, ChangeGroupMemoRequest request) {
        return execute("PUT", "/api/v1/clients/" + encode(clientId) + "/groups/memo", null, request);
    }

    /** 更新群公告（仅适用于自有群）。 */
    public String updateGroupNotice(String clientId, UpdateGroupNoticeRequest request) {
        return execute("PUT", "/api/v1/clients/" + encode(clientId) + "/groups/notice", null, request);
    }

    /** 邀请群聊成员（适用于外部群）。 */
    public String inviteGroupMembers(String clientId, InviteGroupMemberRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/groups/invite", null, request);
    }

    /** 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。 */
    public String addGroupMembersToFriends(String clientId, AddGroupMemberToFriendsRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/groups/add-members-to-friends", null, request);
    }

    // ------------------------------------------------------------------
    // Messages
    // ------------------------------------------------------------------

    /** 获取当前聊天窗口的标题对象。 */
    public String getMessageTitle(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/messages/title", null, null);
    }

    /** 获取当前聊天窗口的标题名称。 */
    public String getMessageOnlyTitle(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/messages/only-title", null, null);
    }

    /** 聚焦当前窗口的消息输入区。 */
    public String focusMessageInput(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/focus-input", null, null);
    }

    /** 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。 */
    public String sendMessage(String clientId, SendMessageRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send", null, request);
    }

    /** 发送文件。 */
    public String sendFile(String clientId, SendFileRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-file", null, request);
    }

    /** 发送表情（按名称/描述/索引）。 */
    public String sendEmoji(String clientId, SendEmojiRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-emoji", null, request);
    }

    /** 发起单人语音聊天。 */
    public String sendVoiceChat(String clientId, WhoRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-voice-chat", null, request);
    }

    /** 发起单人视频聊天。 */
    public String sendVideoChat(String clientId, WhoRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-video-chat", null, request);
    }

    /** 发起多人语音聊天（群聊）。 */
    public String sendVoiceChats(String clientId, SendVoiceChatsRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-voice-chats", null, request);
    }

    /** 发送语音消息（需虚拟声卡 Cable input/Cable output）。 */
    public String sendVoiceMessage(String clientId, SendVoiceMessageRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-voice-message", null, request);
    }

    /** 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。 */
    public String sendVoiceMessageTts(String clientId, SendVoiceMessageTtsRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/send-voice-message-tts", null, request);
    }

    /** 获取聊天历史（支持按日期或时间段查询）。 */
    public String getMessageHistory(String clientId, String who, String date, String startDate, String endDate) {
        Map<String, String> query = new LinkedHashMap<>();
        if (who != null) {
            query.put("who", who);
        }
        if (date != null) {
            query.put("date", date);
        }
        if (startDate != null) {
            query.put("start_date", startDate);
        }
        if (endDate != null) {
            query.put("end_date", endDate);
        }
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/messages/history", query, null);
    }

    /** 拍一拍当前聊天窗口的好友。 */
    public String tap(String clientId, TapRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/tap", null, request);
    }

    /** 引用当前窗口的好友消息。 */
    public String reference(String clientId, ReferenceRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/reference", null, request);
    }

    /** 引用当前窗口最后一条消息。 */
    public String referenceLast(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/messages/reference-last", null, null);
    }

    // ------------------------------------------------------------------
    // Navigation
    // ------------------------------------------------------------------

    /** 切换导航栏。 */
    public String switchNavigation(String clientId, SwitchNavigationRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/navigation/switch", null, request);
    }

    /** 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。 */
    public String closeNavigation(String clientId, SwitchNavigationRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/navigation/close", null, request);
    }

    // ------------------------------------------------------------------
    // Window
    // ------------------------------------------------------------------

    /** 最大化微信窗口。 */
    public String maximizeWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/maximize", null, null);
    }

    /** 还原微信窗口。 */
    public String restoreWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/restore", null, null);
    }

    /** 置顶微信窗口。 */
    public String pinWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/pin", null, null);
    }

    /** 取消置顶微信窗口。 */
    public String unpinWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/unpin", null, null);
    }

    /** 使主窗口获取焦点。 */
    public String focusWindow(String clientId) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/focus", null, null);
    }

    /** 关闭查询窗口（已打开则关闭）。 */
    public String closeSearchWindow(String clientId, SubWindowRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/close-search-window", null, request);
    }

    /** 打开指定对象的子窗口。 */
    public String openSubWindow(String clientId, SubWindowRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/open-sub-window", null, request);
    }

    /** 关闭指定对象的子窗口（已存在则关闭）。 */
    public String closeSubWindow(String clientId, SubWindowRequest request) {
        return execute("POST", "/api/v1/clients/" + encode(clientId) + "/window/close-sub-window", null, request);
    }

    /** 获取本微信窗口句柄。 */
    public String getWindowHandler(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/window/handler", null, null);
    }

    /** 获取本微信窗口进程 ID。 */
    public String getWindowProcessId(String clientId) {
        return execute("GET", "/api/v1/clients/" + encode(clientId) + "/window/process-id", null, null);
    }

    // ------------------------------------------------------------------
    // Internal helpers
    // ------------------------------------------------------------------

    private static String trimTrailingSlash(String value) {
        String result = value;
        while (result.endsWith("/")) {
            result = result.substring(0, result.length() - 1);
        }
        return result;
    }

    /** URL-encode a single path segment (spaces become %20, not +). */
    private static String encode(String value) {
        return URLEncoder.encode(value, StandardCharsets.UTF_8).replace("+", "%20");
    }

    private String execute(String method, String path, Map<String, String> query, Object body) {
        String uri = buildUri(path, query);
        HttpRequest.Builder builder = HttpRequest.newBuilder().uri(URI.create(uri));
        builder.header("Accept", "application/json");
        if (apiKey != null && !apiKey.isEmpty()) {
            builder.header(API_KEY_HEADER, apiKey);
        }

        HttpRequest request;
        if (body == null) {
            request = builder.method(method, HttpRequest.BodyPublishers.noBody()).build();
        } else {
            builder.header("Content-Type", "application/json");
            request = builder.method(method, HttpRequest.BodyPublishers.ofByteArray(serialize(body))).build();
        }

        HttpResponse<String> response;
        try {
            response = httpClient.send(request, HttpResponse.BodyHandlers.ofString(StandardCharsets.UTF_8));
        } catch (IOException e) {
            throw new WeChatAutoException(0, "Request to " + uri + " failed: " + e.getMessage(), e);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            throw new WeChatAutoException(0, "Request to " + uri + " was interrupted", e);
        }

        int status = response.statusCode();
        String responseBody = response.body();
        if (status < 200 || status >= 300) {
            throw new WeChatAutoException(status, "HTTP " + status + " for " + uri + ": " + responseBody);
        }
        return responseBody;
    }

    private String buildUri(String path, Map<String, String> query) {
        StringBuilder sb = new StringBuilder(baseUrl).append(path);
        if (query != null && !query.isEmpty()) {
            boolean first = true;
            for (Map.Entry<String, String> entry : query.entrySet()) {
                if (entry.getValue() == null) {
                    continue;
                }
                sb.append(first ? '?' : '&');
                first = false;
                sb.append(URLEncoder.encode(entry.getKey(), StandardCharsets.UTF_8));
                sb.append('=');
                sb.append(URLEncoder.encode(entry.getValue(), StandardCharsets.UTF_8));
            }
        }
        return sb.toString();
    }

    private byte[] serialize(Object body) {
        try {
            return objectMapper.writeValueAsBytes(body);
        } catch (JsonProcessingException e) {
            throw new WeChatAutoException(0, "Failed to serialize request body: " + e.getMessage(), e);
        }
    }
}
