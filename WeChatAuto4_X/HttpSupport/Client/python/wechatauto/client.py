"""⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）"""

from urllib.parse import quote

import requests


class WeChatAutoError(Exception):
    """Raised when the server returns a non-2xx status code."""

    def __init__(self, status_code, body_text):
        self.status_code = status_code
        self.body_text = body_text
        super().__init__(f"HTTP {status_code}: {body_text}")


def _quote(value):
    return quote(str(value), safe="")


def _to_body(body):
    """Convert a dataclass body to a dict; pass dicts (and everything else) through."""
    if body is None:
        return None
    if isinstance(body, dict):
        return body
    if hasattr(body, "to_dict"):
        return body.to_dict()
    return body


class WeChatAutoClient:
    """Synchronous HTTP client for the WECHATAUTO.SDK API.

    :param base_url: Server base URL (default ``http://localhost:5000``).
    :param api_key: Value of the ``X-API-Key`` header. When empty, the header is
        omitted (the server does not validate in that case).
    :param session: Optional ``requests.Session`` to inject (useful for testing).
    """

    def __init__(self, base_url="http://localhost:5000", api_key="", session=None):
        self.base_url = (base_url or "http://localhost:5000").rstrip("/")
        self.api_key = api_key or ""
        self._session = session if session is not None else requests.Session()

    # -- internals ---------------------------------------------------------

    def _headers(self):
        if self.api_key:
            return {"X-API-Key": self.api_key}
        return {}

    def _request(self, method, path, params=None, json=None):
        url = self.base_url + path
        response = self._session.request(
            method, url, params=params, json=json, headers=self._headers()
        )
        if not 200 <= response.status_code < 300:
            raise WeChatAutoError(response.status_code, response.text)
        try:
            return response.json()
        except ValueError:
            return response.text

    # -- Clients -----------------------------------------------------------

    def get_clients(self):
        return self._request("GET", "/api/v1/clients")

    def get_client(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}")

    def get_client_owner_info(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/owner-info")

    def save_client_avatar(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/avatar",
            json=_to_body(body),
        )

    def click_notify_icon(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/notify-icon/click",
            json=_to_body(body),
        )

    # -- Conversations -----------------------------------------------------

    def get_conversations(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/conversations")

    def get_conversations_visible_titles(self, client_id):
        return self._request(
            "GET", f"/api/v1/clients/{_quote(client_id)}/conversations/visible-titles"
        )

    def get_conversations_visible(self, client_id):
        return self._request(
            "GET", f"/api/v1/clients/{_quote(client_id)}/conversations/visible"
        )

    def search_conversations(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/conversations/search",
            json=_to_body(body),
        )

    def locate_conversation(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/conversations/locate",
            json=_to_body(body),
        )

    def set_conversation_do_not_disturb(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/conversations/set-do-not-disturb",
            json=_to_body(body),
        )

    def set_conversation_top_most(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/conversations/set-top-most",
            json=_to_body(body),
        )

    # -- Friends -----------------------------------------------------------

    def open_add_friend_window(self, client_id):
        return self._request(
            "POST", f"/api/v1/clients/{_quote(client_id)}/friends/add-window/open"
        )

    def close_add_friend_window(self, client_id):
        return self._request(
            "POST", f"/api/v1/clients/{_quote(client_id)}/friends/add-window/close"
        )

    def add_friends(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/friends/add",
            json=_to_body(body),
        )

    def get_friends(self, client_id, from_cache=True):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/friends",
            params={"fromCache": from_cache},
        )

    def get_friend_names(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/friends/names")

    def delete_friend(self, client_id, nick_name):
        return self._request(
            "DELETE", f"/api/v1/clients/{_quote(client_id)}/friends/{_quote(nick_name)}"
        )

    def get_friends_cache(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/friends/cache")

    def add_or_update_friends_cache(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache",
            json=_to_body(body),
        )

    def get_friends_cache_by_name(self, client_id, who):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache/by-name/{_quote(who)}",
        )

    def get_friends_cache_by_wxid(self, client_id, wxid):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache/by-wxid/{_quote(wxid)}",
        )

    def delete_friends_cache_by_wxid(self, client_id, wxid):
        return self._request(
            "DELETE",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache/by-wxid/{_quote(wxid)}",
        )

    def get_friends_cache_who(self, client_id, who):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache/{_quote(who)}",
        )

    def delete_friends_cache_who(self, client_id, who):
        return self._request(
            "DELETE",
            f"/api/v1/clients/{_quote(client_id)}/friends/cache/{_quote(who)}",
        )

    # -- Groups ------------------------------------------------------------

    def is_group_owner(self, client_id, group_name):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/groups/{_quote(group_name)}/is-owner",
        )

    def get_group_owner(self, client_id, group_name):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/groups/{_quote(group_name)}/owner",
        )

    def get_group_members(self, client_id, group_name=None):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/groups/members",
            params={"groupName": group_name},
        )

    def add_group_members(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/groups/members",
            json=_to_body(body),
        )

    def remove_group_members(self, client_id, body):
        return self._request(
            "DELETE",
            f"/api/v1/clients/{_quote(client_id)}/groups/members",
            json=_to_body(body),
        )

    def leave_group(self, client_id, group_name, clear_history=True):
        return self._request(
            "DELETE",
            f"/api/v1/clients/{_quote(client_id)}/groups/{_quote(group_name)}",
            params={"clearHistory": clear_history},
        )

    def create_group(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/groups",
            json=_to_body(body),
        )

    def rename_group(self, client_id, body):
        return self._request(
            "PUT",
            f"/api/v1/clients/{_quote(client_id)}/groups/name",
            json=_to_body(body),
        )

    def change_group_nickname(self, client_id, body):
        return self._request(
            "PUT",
            f"/api/v1/clients/{_quote(client_id)}/groups/nickname",
            json=_to_body(body),
        )

    def change_group_memo(self, client_id, body):
        return self._request(
            "PUT",
            f"/api/v1/clients/{_quote(client_id)}/groups/memo",
            json=_to_body(body),
        )

    def update_group_notice(self, client_id, body):
        return self._request(
            "PUT",
            f"/api/v1/clients/{_quote(client_id)}/groups/notice",
            json=_to_body(body),
        )

    def invite_group_members(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/groups/invite",
            json=_to_body(body),
        )

    def add_group_members_to_friends(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/groups/add-members-to-friends",
            json=_to_body(body),
        )

    # -- Messages ----------------------------------------------------------

    def get_message_title(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/messages/title")

    def get_message_only_title(self, client_id):
        return self._request(
            "GET", f"/api/v1/clients/{_quote(client_id)}/messages/only-title"
        )

    def focus_message_input(self, client_id):
        return self._request(
            "POST", f"/api/v1/clients/{_quote(client_id)}/messages/focus-input"
        )

    def send_message(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send",
            json=_to_body(body),
        )

    def send_file(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-file",
            json=_to_body(body),
        )

    def send_emoji(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-emoji",
            json=_to_body(body),
        )

    def send_voice_chat(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-voice-chat",
            json=_to_body(body),
        )

    def send_video_chat(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-video-chat",
            json=_to_body(body),
        )

    def send_voice_chats(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-voice-chats",
            json=_to_body(body),
        )

    def send_voice_message(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-voice-message",
            json=_to_body(body),
        )

    def send_voice_message_tts(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/send-voice-message-tts",
            json=_to_body(body),
        )

    def get_message_history(self, client_id, who=None, date=None, start_date=None, end_date=None):
        return self._request(
            "GET",
            f"/api/v1/clients/{_quote(client_id)}/messages/history",
            params={
                "who": who,
                "date": date,
                "start_date": start_date,
                "end_date": end_date,
            },
        )

    def tap_message(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/tap",
            json=_to_body(body),
        )

    def reference_message(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/messages/reference",
            json=_to_body(body),
        )

    def reference_last_message(self, client_id):
        return self._request(
            "POST", f"/api/v1/clients/{_quote(client_id)}/messages/reference-last"
        )

    # -- Navigation --------------------------------------------------------

    def switch_navigation(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/navigation/switch",
            json=_to_body(body),
        )

    def close_navigation(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/navigation/close",
            json=_to_body(body),
        )

    # -- Window ------------------------------------------------------------

    def maximize_window(self, client_id):
        return self._request("POST", f"/api/v1/clients/{_quote(client_id)}/window/maximize")

    def restore_window(self, client_id):
        return self._request("POST", f"/api/v1/clients/{_quote(client_id)}/window/restore")

    def pin_window(self, client_id):
        return self._request("POST", f"/api/v1/clients/{_quote(client_id)}/window/pin")

    def unpin_window(self, client_id):
        return self._request("POST", f"/api/v1/clients/{_quote(client_id)}/window/unpin")

    def focus_window(self, client_id):
        return self._request("POST", f"/api/v1/clients/{_quote(client_id)}/window/focus")

    def close_search_window(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/window/close-search-window",
            json=_to_body(body),
        )

    def open_sub_window(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/window/open-sub-window",
            json=_to_body(body),
        )

    def close_sub_window(self, client_id, body):
        return self._request(
            "POST",
            f"/api/v1/clients/{_quote(client_id)}/window/close-sub-window",
            json=_to_body(body),
        )

    def get_window_handler(self, client_id):
        return self._request("GET", f"/api/v1/clients/{_quote(client_id)}/window/handler")

    def get_window_process_id(self, client_id):
        return self._request(
            "GET", f"/api/v1/clients/{_quote(client_id)}/window/process-id"
        )
