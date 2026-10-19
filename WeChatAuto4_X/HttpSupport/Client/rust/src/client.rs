//! Async HTTP client for the WECHATAUTO.SDK API.
//!
//! The client targets `http://localhost:5000` by default, applies the optional
//! `X-API-Key` header when configured, and always returns the raw response body
//! as [`serde_json::Value`] (the OpenAPI document declares no response schemas).

use reqwest::Method;
use serde::Serialize;
use thiserror::Error;

/// Errors produced by [`WeChatAutoClient`].
#[derive(Debug, Error)]
pub enum Error {
    /// The underlying HTTP request failed (network, serialization, etc.).
    #[error("request failed: {0}")]
    Request(#[from] reqwest::Error),

    /// The server returned a non-2xx status code.
    #[error("HTTP {status}: {body}")]
    Http { status: u16, body: String },

    /// The response body was not valid JSON.
    #[error("failed to parse JSON response: {source}; body: {body}")]
    Json { source: serde_json::Error, body: String },
}

/// Builder for [`WeChatAutoClient`].
#[derive(Debug, Clone, Default)]
pub struct WeChatAutoClientBuilder {
    base_url: String,
    api_key: Option<String>,
    client: Option<reqwest::Client>,
}

impl WeChatAutoClientBuilder {
    /// Set the base URL (defaults to `http://localhost:5000`).
    pub fn base_url(mut self, base_url: impl Into<String>) -> Self {
        self.base_url = base_url.into();
        self
    }

    /// Set the `X-API-Key` value.
    pub fn api_key(mut self, api_key: impl Into<String>) -> Self {
        self.api_key = Some(api_key.into());
        self
    }

    /// Provide a pre-configured [`reqwest::Client`].
    pub fn client(mut self, client: reqwest::Client) -> Self {
        self.client = Some(client);
        self
    }

    /// Build the client.
    pub fn build(self) -> WeChatAutoClient {
        WeChatAutoClient {
            base_url: if self.base_url.is_empty() {
                "http://localhost:5000".to_string()
            } else {
                self.base_url
            },
            api_key: self.api_key,
            http: self.client.unwrap_or_default(),
        }
    }
}

/// Client for the WECHATAUTO.SDK WeChat automation API.
#[derive(Debug, Clone)]
pub struct WeChatAutoClient {
    base_url: String,
    api_key: Option<String>,
    http: reqwest::Client,
}

impl Default for WeChatAutoClient {
    fn default() -> Self {
        Self::new("http://localhost:5000")
    }
}

impl WeChatAutoClient {
    /// Create a client for the given base URL with no API key.
    pub fn new(base_url: impl Into<String>) -> Self {
        Self {
            base_url: base_url.into(),
            api_key: None,
            http: reqwest::Client::new(),
        }
    }

    /// Create a client for the given base URL with an `X-API-Key` value.
    pub fn with_api_key(base_url: impl Into<String>, api_key: impl Into<String>) -> Self {
        Self {
            base_url: base_url.into(),
            api_key: Some(api_key.into()),
            http: reqwest::Client::new(),
        }
    }

    /// Return a [`WeChatAutoClientBuilder`].
    pub fn builder() -> WeChatAutoClientBuilder {
        WeChatAutoClientBuilder::default()
    }

    /// The configured base URL.
    pub fn base_url(&self) -> &str {
        &self.base_url
    }

    /// The configured `X-API-Key`, if any.
    pub fn api_key(&self) -> Option<&str> {
        self.api_key.as_deref()
    }

    // ------------------------------------------------------------------
    // Request plumbing
    // ------------------------------------------------------------------

    /// Percent-encode a single path segment.
    fn encode_path_segment(segment: &str) -> String {
        let mut out = String::with_capacity(segment.len());
        for byte in segment.bytes() {
            match byte {
                b'A'..=b'Z' | b'a'..=b'z' | b'0'..=b'9' | b'-' | b'.' | b'_' | b'~' => {
                    out.push(byte as char)
                }
                _ => out.push_str(&format!("%{:02X}", byte)),
            }
        }
        out
    }

    /// Join the base URL with the given (already encoded) path.
    fn build_url(&self, path: &str) -> String {
        format!("{}{}", self.base_url.trim_end_matches('/'), path)
    }

    /// Build a request without sending it. Kept separate so URL construction,
    /// query serialization, header injection and body serialization can all be
    /// asserted in unit tests without a live server.
    fn build_request<T: Serialize + ?Sized>(
        &self,
        method: Method,
        path: &str,
        query: &[(&'static str, String)],
        body: Option<&T>,
    ) -> Result<reqwest::Request, Error> {
        let mut req = self.http.request(method, self.build_url(path));
        if !query.is_empty() {
            let pairs: Vec<(&str, &str)> = query
                .iter()
                .map(|(key, value)| (*key, value.as_str()))
                .collect();
            req = req.query(&pairs);
        }
        if let Some(key) = self.api_key.as_deref() {
            if !key.is_empty() {
                req = req.header("X-API-Key", key);
            }
        }
        if let Some(body) = body {
            req = req.json(body);
        }
        req.build().map_err(Error::Request)
    }

    /// Execute a built request and decode the response body as JSON.
    async fn send<T: Serialize + ?Sized>(
        &self,
        method: Method,
        path: String,
        query: Vec<(&'static str, String)>,
        body: Option<&T>,
    ) -> Result<serde_json::Value, Error> {
        let request = self.build_request(method, &path, &query, body)?;
        let response = self.http.execute(request).await.map_err(Error::Request)?;
        let status = response.status();
        let text = response.text().await.map_err(Error::Request)?;
        if !status.is_success() {
            return Err(Error::Http {
                status: status.as_u16(),
                body: text,
            });
        }
        if text.trim().is_empty() {
            return Ok(serde_json::Value::Null);
        }
        serde_json::from_str(&text).map_err(|source| Error::Json { source, body: text })
    }

    // ------------------------------------------------------------------
    // Clients
    // ------------------------------------------------------------------

    /// 获取全部微信客户端的概要信息列表。
    pub async fn get_clients(&self) -> Result<serde_json::Value, Error> {
        self.send(Method::GET, "/api/v1/clients".to_string(), vec![], None::<&()>)
            .await
    }

    /// 获取指定微信客户端的概要信息（昵称、wxid、头像路径、进程ID、窗口句柄、任务栏索引）。
    pub async fn get_client(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取本微信的个人信息（头像文件位置、wxid、昵称）。
    pub async fn get_client_owner_info(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/owner-info",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 保存本微信头像到指定路径。
    pub async fn save_avatar(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SaveAvatarRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/avatar",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 点击任务栏微信图标，按索引或微信名称定位。
    pub async fn notify_icon_click(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::NotifyIconClickRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/notify-icon/click",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    // ------------------------------------------------------------------
    // Conversations
    // ------------------------------------------------------------------

    /// 获取会话列表所有会话的标题。
    pub async fn get_conversations(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取会话列表可见会话的标题。
    pub async fn get_visible_conversation_titles(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/visible-titles",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取可见会话列表（含名称、未读消息数、头像等）。
    pub async fn get_visible_conversations(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/visible",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 搜索好友/群聊。
    pub async fn search_conversation(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SearchConversationRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/search",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 定位会话，滚动会话列表使指定会话可见。
    pub async fn locate_conversation(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::LocateConversationRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/locate",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 设置会话消息免打扰。
    pub async fn set_do_not_disturb(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SetDoNotDisturbRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/set-do-not-disturb",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 设置会话置顶。
    pub async fn set_top_most(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SetTopMostRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/conversations/set-top-most",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    // ------------------------------------------------------------------
    // Friends
    // ------------------------------------------------------------------

    /// 打开新增朋友窗口。
    pub async fn open_add_friend_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/add-window/open",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 关闭新增朋友窗口。
    pub async fn close_add_friend_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/add-window/close",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 通过手机号码/微信号查找并添加好友。
    pub async fn add_friends(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::AddFriendsRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/add",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 获取所有好友的信息列表（默认优先从缓存读取）。
    pub async fn get_friends(
        &self,
        client_id: impl Into<String>,
        from_cache: Option<bool>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends",
            Self::encode_path_segment(&client_id.into())
        );
        let query = vec![("fromCache", from_cache.unwrap_or(true).to_string())];
        self.send(Method::GET, path, query, None::<&()>).await
    }

    /// 获取所有好友名称列表（有备注优先备注名）。
    pub async fn get_friend_names(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/names",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 从通讯录移除好友。
    pub async fn delete_friend(
        &self,
        client_id: impl Into<String>,
        nick_name: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&nick_name.into())
        );
        self.send(Method::DELETE, path, vec![], None::<&()>).await
    }

    /// 获取缓存中的全部好友信息列表。
    pub async fn get_friends_cache(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 手动新增或修改缓存中的好友对象。
    pub async fn set_friend_cache(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::FriendInfo,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 按名称从缓存获取好友列表（适用于昵称重复场景）。
    pub async fn get_friends_cache_by_name(
        &self,
        client_id: impl Into<String>,
        who: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache/by-name/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&who.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 按 wxid 从缓存获取好友信息。
    pub async fn get_friend_cache_by_wxid(
        &self,
        client_id: impl Into<String>,
        wxid: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache/by-wxid/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&wxid.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 按 wxid 从缓存移除好友。
    pub async fn delete_friend_cache_by_wxid(
        &self,
        client_id: impl Into<String>,
        wxid: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache/by-wxid/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&wxid.into())
        );
        self.send(Method::DELETE, path, vec![], None::<&()>).await
    }

    /// 按名称从缓存获取单个好友信息。
    pub async fn get_friend_cache(
        &self,
        client_id: impl Into<String>,
        who: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&who.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 按名称从缓存移除好友。
    pub async fn delete_friend_cache(
        &self,
        client_id: impl Into<String>,
        who: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/friends/cache/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&who.into())
        );
        self.send(Method::DELETE, path, vec![], None::<&()>).await
    }

    // ------------------------------------------------------------------
    // Groups
    // ------------------------------------------------------------------

    /// 判断群聊是否为自有群。
    pub async fn group_is_owner(
        &self,
        client_id: impl Into<String>,
        group_name: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/{}/is-owner",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&group_name.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取群主昵称。
    pub async fn get_group_owner(
        &self,
        client_id: impl Into<String>,
        group_name: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/{}/owner",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&group_name.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取群聊成员列表。
    pub async fn get_group_members(
        &self,
        client_id: impl Into<String>,
        group_name: Option<&str>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/members",
            Self::encode_path_segment(&client_id.into())
        );
        let mut query = Vec::new();
        if let Some(name) = group_name {
            query.push(("groupName", name.to_string()));
        }
        self.send(Method::GET, path, query, None::<&()>).await
    }

    /// 添加群聊成员（适用于自有群）。
    pub async fn add_group_members(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::GroupMemberRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/members",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 移除群聊成员（适用于自有群）。
    pub async fn remove_group_members(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::GroupMemberRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/members",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::DELETE, path, vec![], Some(body)).await
    }

    /// 退出群聊。
    pub async fn exit_group(
        &self,
        client_id: impl Into<String>,
        group_name: impl Into<String>,
        clear_history: Option<bool>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/{}",
            Self::encode_path_segment(&client_id.into()),
            Self::encode_path_segment(&group_name.into())
        );
        let query = vec![(
            "clearHistory",
            clear_history.unwrap_or(true).to_string(),
        )];
        self.send(Method::DELETE, path, query, None::<&()>).await
    }

    /// 创建群聊，已存在则打开群聊。
    pub async fn create_group(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::CreateGroupRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 修改群名（适用于自有群）。
    pub async fn rename_group(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::RenameGroupRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/name",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::PUT, path, vec![], Some(body)).await
    }

    /// 修改自己在群中的昵称。
    pub async fn change_group_nickname(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::ChangeGroupNickNameRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/nickname",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::PUT, path, vec![], Some(body)).await
    }

    /// 修改群备注（仅自己可见）。
    pub async fn change_group_memo(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::ChangeGroupMemoRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/memo",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::PUT, path, vec![], Some(body)).await
    }

    /// 更新群公告（仅适用于自有群）。
    pub async fn update_group_notice(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::UpdateGroupNoticeRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/notice",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::PUT, path, vec![], Some(body)).await
    }

    /// 邀请群聊成员（适用于外部群）。
    pub async fn invite_group_members(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::InviteGroupMemberRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/invite",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 把群聊成员加为自己的好友（适用于外部群，注意微信风控）。
    pub async fn add_group_members_to_friends(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::AddGroupMemberToFriendsRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/groups/add-members-to-friends",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    // ------------------------------------------------------------------
    // Messages
    // ------------------------------------------------------------------

    /// 获取当前聊天窗口的标题对象。
    pub async fn get_message_title(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/title",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取当前聊天窗口的标题名称。
    pub async fn get_message_only_title(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/only-title",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 聚焦当前窗口的消息输入区。
    pub async fn focus_message_input(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/focus-input",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 发送文本消息（指定对象或当前聊天窗口，支持 @ 与引用）。
    pub async fn send_message(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendMessageRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发送文件。
    pub async fn send_file(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendFileRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-file",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发送表情（按名称/描述/索引）。
    pub async fn send_emoji(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendEmojiRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-emoji",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发起单人语音聊天。
    pub async fn send_voice_chat(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::WhoRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-voice-chat",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发起单人视频聊天。
    pub async fn send_video_chat(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::WhoRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-video-chat",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发起多人语音聊天（群聊）。
    pub async fn send_voice_chats(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendVoiceChatsRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-voice-chats",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 发送语音消息（需虚拟声卡 Cable input/Cable output）。
    pub async fn send_voice_message(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendVoiceMessageRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-voice-message",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 文字转语音发送（默认阿里千问 Qwen-Audio-TTS）。
    pub async fn send_voice_message_tts(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SendVoiceMessageTtsRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/send-voice-message-tts",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 获取聊天历史（支持按日期或时间段查询）。
    #[allow(clippy::too_many_arguments)]
    pub async fn get_message_history(
        &self,
        client_id: impl Into<String>,
        who: Option<&str>,
        date: Option<&str>,
        start_date: Option<&str>,
        end_date: Option<&str>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/history",
            Self::encode_path_segment(&client_id.into())
        );
        let mut query = Vec::new();
        if let Some(value) = who {
            query.push(("who", value.to_string()));
        }
        if let Some(value) = date {
            query.push(("date", value.to_string()));
        }
        if let Some(value) = start_date {
            query.push(("start_date", value.to_string()));
        }
        if let Some(value) = end_date {
            query.push(("end_date", value.to_string()));
        }
        self.send(Method::GET, path, query, None::<&()>).await
    }

    /// 拍一拍当前聊天窗口的好友。
    pub async fn tap(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::TapRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/tap",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 引用当前窗口的好友消息。
    pub async fn reference_message(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::ReferenceRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/reference",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 引用当前窗口最后一条消息。
    pub async fn reference_last_message(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/messages/reference-last",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    // ------------------------------------------------------------------
    // Navigation
    // ------------------------------------------------------------------

    /// 切换导航栏。
    pub async fn switch_navigation(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SwitchNavigationRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/navigation/switch",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 关闭通过导航栏打开的窗口（聊天文件、朋友圈、视频号等）。
    pub async fn close_navigation(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SwitchNavigationRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/navigation/close",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    // ------------------------------------------------------------------
    // Window
    // ------------------------------------------------------------------

    /// 最大化微信窗口。
    pub async fn maximize_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/maximize",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 还原微信窗口。
    pub async fn restore_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/restore",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 置顶微信窗口。
    pub async fn pin_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/pin",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 取消置顶微信窗口。
    pub async fn unpin_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/unpin",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 使主窗口获取焦点。
    pub async fn focus_window(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/focus",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], None::<&()>).await
    }

    /// 关闭查询窗口（已打开则关闭）。
    pub async fn close_search_window(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SubWindowRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/close-search-window",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 打开指定对象的子窗口。
    pub async fn open_sub_window(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SubWindowRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/open-sub-window",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 关闭指定对象的子窗口（已存在则关闭）。
    pub async fn close_sub_window(
        &self,
        client_id: impl Into<String>,
        body: &crate::models::SubWindowRequest,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/close-sub-window",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::POST, path, vec![], Some(body)).await
    }

    /// 获取本微信窗口句柄。
    pub async fn get_window_handler(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/handler",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }

    /// 获取本微信窗口进程 ID。
    pub async fn get_window_process_id(
        &self,
        client_id: impl Into<String>,
    ) -> Result<serde_json::Value, Error> {
        let path = format!(
            "/api/v1/clients/{}/window/process-id",
            Self::encode_path_segment(&client_id.into())
        );
        self.send(Method::GET, path, vec![], None::<&()>).await
    }
}

#[cfg(test)]
mod tests {
    use super::WeChatAutoClient;
    use crate::models::*;
    use reqwest::Method;

    fn client() -> WeChatAutoClient {
        WeChatAutoClient::new("http://localhost:5000")
    }

    fn request_url(client: &WeChatAutoClient, method: Method, path: &str) -> String {
        let req = client
            .build_request(method, path, &[], None::<&()>)
            .expect("build request");
        req.url().to_string()
    }

    #[test]
    fn builds_base_path() {
        let url = request_url(&client(), Method::GET, "/api/v1/clients");
        assert_eq!(url, "http://localhost:5000/api/v1/clients");
    }

    #[test]
    fn encodes_path_segments() {
        // Chinese nick name and a value needing percent-encoding.
        assert_eq!(
            WeChatAutoClient::encode_path_segment("张三/群"),
            "%E5%BC%A0%E4%B8%89%2F%E7%BE%A4"
        );
        assert_eq!(
            WeChatAutoClient::encode_path_segment("a b"),
            "a%20b"
        );
        let url = request_url(
            &client(),
            Method::DELETE,
            "/api/v1/clients/c1/friends/%E5%BC%A0%E4%B8%89",
        );
        assert!(url.ends_with("/api/v1/clients/c1/friends/%E5%BC%A0%E4%B8%89"), "{url}");
    }

    #[test]
    fn friends_query_uses_default_true() {
        let req = client()
            .build_request(
                Method::GET,
                "/api/v1/clients/c1/friends",
                &[("fromCache", true.to_string())],
                None::<&()>,
            )
            .expect("build request");
        assert_eq!(
            req.url().as_str(),
            "http://localhost:5000/api/v1/clients/c1/friends?fromCache=true"
        );
    }

    #[test]
    fn friends_query_uses_explicit_false() {
        let req = client()
            .build_request(
                Method::GET,
                "/api/v1/clients/c1/friends",
                &[("fromCache", false.to_string())],
                None::<&()>,
            )
            .expect("build request");
        assert_eq!(
            req.url().as_str(),
            "http://localhost:5000/api/v1/clients/c1/friends?fromCache=false"
        );
    }

    #[test]
    fn exit_group_clear_history_default_true() {
        let req = client()
            .build_request(
                Method::DELETE,
                "/api/v1/clients/c1/groups/g1",
                &[("clearHistory", true.to_string())],
                None::<&()>,
            )
            .expect("build request");
        assert_eq!(
            req.url().as_str(),
            "http://localhost:5000/api/v1/clients/c1/groups/g1?clearHistory=true"
        );
    }

    #[test]
    fn group_members_query_serializes_group_name() {
        let req = client()
            .build_request(
                Method::GET,
                "/api/v1/clients/c1/groups/members",
                &[("groupName", "我的群".to_string())],
                None::<&()>,
            )
            .expect("build request");
        let url = req.url().to_string();
        assert!(url.starts_with("http://localhost:5000/api/v1/clients/c1/groups/members?"), "{url}");
        assert!(url.contains("groupName=%E6%88%91%E7%9A%84%E7%BE%A4"), "{url}");
    }

    #[test]
    fn message_history_serializes_multiple_query_params() {
        let query = vec![
            ("who", "张三".to_string()),
            ("date", "2026-10-19".to_string()),
            ("start_date", "2026-10-01".to_string()),
            ("end_date", "2026-10-19".to_string()),
        ];
        let req = client()
            .build_request(Method::GET, "/api/v1/clients/c1/messages/history", &query, None::<&()>)
            .expect("build request");
        let url = req.url().to_string();
        assert!(url.contains("who=%E5%BC%A0%E4%B8%89"), "{url}");
        assert!(url.contains("date=2026-10-19"), "{url}");
        assert!(url.contains("start_date=2026-10-01"), "{url}");
        assert!(url.contains("end_date=2026-10-19"), "{url}");
    }

    #[test]
    fn api_key_header_set_only_when_present_and_non_empty() {
        let with_key = WeChatAutoClient::with_api_key("http://localhost:5000", "secret");
        let req = with_key
            .build_request(Method::GET, "/api/v1/clients", &[], None::<&()>)
            .expect("build request");
        assert_eq!(req.headers().get("X-API-Key").unwrap(), "secret");

        let empty_key = WeChatAutoClient::with_api_key("http://localhost:5000", "");
        let req = empty_key
            .build_request(Method::GET, "/api/v1/clients", &[], None::<&()>)
            .expect("build request");
        assert!(req.headers().get("X-API-Key").is_none());

        let no_key = client();
        let req = no_key
            .build_request(Method::GET, "/api/v1/clients", &[], None::<&()>)
            .expect("build request");
        assert!(req.headers().get("X-API-Key").is_none());
    }

    #[test]
    fn snake_case_keys_and_none_omission() {
        let msg = SendMessageRequest {
            who: Some("张三".to_string()),
            message: Some("你好".to_string()),
            at_users: Some(vec!["李四".to_string()]),
            refer: ChatRefer {
                date: "2026-10-19".to_string(),
                message: ChatSimpleMessage {
                    who: None,
                    message: None,
                    send_date_time: None,
                    date_time: "2026-10-19T00:00:00".to_string(),
                    unique_string: None,
                },
                is_close_search_win: true,
            },
        };
        let value = serde_json::to_value(&msg).expect("serialize");
        let obj = value.as_object().expect("object");
        assert!(obj.contains_key("who"));
        assert!(obj.contains_key("message"));
        assert!(obj.contains_key("at_users"));
        assert!(obj.contains_key("refer"));
        assert!(!obj.contains_key("Who"));
        assert!(!obj.contains_key("atUsers"));

        // None fields omitted from nested message
        let refer = obj["refer"].as_object().expect("refer object");
        let message = refer["message"].as_object().expect("nested message");
        assert!(message.contains_key("date_time"));
        assert!(!message.contains_key("who"));
        assert!(!message.contains_key("unique_string"));
    }

    #[test]
    fn enum_string_values() {
        assert_eq!(
            serde_json::to_value(ChatType::GroupChat).unwrap(),
            serde_json::json!("群聊")
        );
        assert_eq!(
            serde_json::to_value(NavigationType::Chat).unwrap(),
            serde_json::json!("聊天")
        );
        assert_eq!(
            serde_json::to_value(NavigationType::Moments).unwrap(),
            serde_json::json!("朋友圈")
        );
        assert_eq!(
            serde_json::to_value(FriendSearchResultEnums::NoFind).unwrap(),
            serde_json::json!("No_Find")
        );
        assert_eq!(
            serde_json::to_value(PixelFormat::Format32bppArgb).unwrap(),
            serde_json::json!("Format32bppArgb")
        );
    }

    #[test]
    fn request_body_json_round_trips() {
        let body = SendFileRequest {
            who: Some("张三".to_string()),
            files: Some(vec!["C:\\a.txt".to_string()]),
        };
        let client = client();
        let req = client
            .build_request(Method::POST, "/api/v1/clients/c1/messages/send-file", &[], Some(&body))
            .expect("build request");
        assert_eq!(req.method().as_str(), "POST");
        let bytes = req.body().and_then(|b| b.as_bytes()).expect("body bytes");
        let json: serde_json::Value = serde_json::from_slice(bytes).expect("json");
        assert_eq!(json["who"], "张三");
        assert_eq!(json["files"][0], "C:\\a.txt");
    }
}
