"""Data models for the WECHATAUTO.SDK HTTP client.

Auto-generated from swagger.json. Every schema under ``components.schemas`` is
represented here as a dataclass or ``str``-based enum. Field names are kept
exactly as they appear on the wire (snake_case). All fields are optional and
default to ``None``; ``to_dict()`` omits ``None`` values so optional fields are
not sent over the wire.
"""

from __future__ import annotations

from dataclasses import dataclass, fields
from enum import Enum
from typing import Any, List


def _to_dict_value(value: Any) -> Any:
    if value is None:
        return None
    if isinstance(value, Enum):
        return value.value
    if isinstance(value, list):
        return [_to_dict_value(item) for item in value]
    if isinstance(value, dict):
        return {key: _to_dict_value(val) for key, val in value.items()}
    if hasattr(value, "to_dict"):
        return value.to_dict()
    return value


class _BaseModel:
    """Shared ``to_dict()`` implementation that omits ``None`` values."""

    def to_dict(self) -> dict:
        result: dict = {}
        for field in fields(self):
            value = getattr(self, field.name)
            if value is None:
                continue
            result[field.name] = _to_dict_value(value)
        return result


# ---------------------------------------------------------------------------
# Enums
# ---------------------------------------------------------------------------


class ChatType(str, Enum):
    """聊天类型 (chat type)."""

    FRIEND = "好友"
    ENTERPRISE_WECHAT = "企业微信"
    GROUP = "群聊"
    OFFICIAL_ACCOUNT = "公众号"
    SUBSCRIPTION_ACCOUNT = "订阅号"
    SERVICE_NOTIFICATION = "服务通知"
    TENCENT_NEWS = "腾讯新闻"
    WECHAT_TEAM = "微信团队"
    FILE_TRANSFER_HELPER = "文件传输助手"
    OTHER = "其他"


class FriendSearchResultEnums(str, Enum):
    """通过wxid查询，或者通过手机号查询结果枚举."""

    FRIEND = "Friend"
    NO_FIND = "No_Find"
    NOT_FRIEND = "NotFriend"


class NavigationType(str, Enum):
    """导航栏类型."""

    CHATS = "聊天"
    CONTACTS = "通讯录"
    FAVORITES = "收藏"
    CHAT_FILES = "聊天文件"
    MOMENTS = "朋友圈"
    SEARCH = "搜一搜"
    CHANNELS = "视频号"
    TOP_STORIES = "看一看"
    MINI_PROGRAM_PANEL = "小程序面板"
    PHONE = "手机"
    SETTINGS = "设置及其他"
    WECHAT = "微信"
    GAME_CENTER = "游戏中心"
    MORE = "更多"


class PixelFormat(str, Enum):
    DONT_CARE = "DontCare"
    MAX = "Max"
    INDEXED = "Indexed"
    GDI = "Gdi"
    FORMAT16BPP_RGB555 = "Format16bppRgb555"
    FORMAT16BPP_RGB565 = "Format16bppRgb565"
    FORMAT24BPP_RGB = "Format24bppRgb"
    FORMAT32BPP_RGB = "Format32bppRgb"
    FORMAT1BPP_INDEXED = "Format1bppIndexed"
    FORMAT4BPP_INDEXED = "Format4bppIndexed"
    FORMAT8BPP_INDEXED = "Format8bppIndexed"
    ALPHA = "Alpha"
    FORMAT16BPP_ARGB1555 = "Format16bppArgb1555"
    P_ALPHA = "PAlpha"
    FORMAT32BPP_P_ARGB = "Format32bppPArgb"
    EXTENDED = "Extended"
    FORMAT16BPP_GRAY_SCALE = "Format16bppGrayScale"
    FORMAT48BPP_RGB = "Format48bppRgb"
    FORMAT64BPP_P_ARGB = "Format64bppPArgb"
    CANONICAL = "Canonical"
    FORMAT32BPP_ARGB = "Format32bppArgb"
    FORMAT64BPP_ARGB = "Format64bppArgb"


# ---------------------------------------------------------------------------
# Models
# ---------------------------------------------------------------------------


@dataclass
class AddFriendsOptions(_BaseModel):
    """增加朋友选项."""

    interval_time: int = None
    is_close_win: bool = None
    say_hi: str = None
    suffix: str = None
    label: str = None


@dataclass
class AddFriendsRequest(_BaseModel):
    """通过手机号码或微信号添加好友。"""

    friends: List[str] = None
    options: AddFriendsOptions = None


@dataclass
class AddGroupMemberToFriendsRequest(_BaseModel):
    """从群聊中添加好友。"""

    group_name: str = None
    members: List[str] = None
    options: AddFriendsOptions = None


@dataclass
class ChangeGroupMemoRequest(_BaseModel):
    """修改群备注。"""

    group_name: str = None
    new_memo: str = None


@dataclass
class ChangeGroupNickNameRequest(_BaseModel):
    """修改自己在群中的昵称。"""

    group_name: str = None
    nick_name: str = None


@dataclass
class ChatRefer(_BaseModel):
    """消息引用."""

    date: str = None
    message: ChatSimpleMessage = None
    is_close_search_win: bool = None


@dataclass
class ChatSimpleMessage(_BaseModel):
    who: str = None
    message: str = None
    send_date_time: str = None
    date_time: str = None
    unique_string: str = None


@dataclass
class Color(_BaseModel):
    r: int = None
    g: int = None
    b: int = None
    a: int = None
    is_known_color: bool = None
    is_empty: bool = None
    is_named_color: bool = None
    is_system_color: bool = None
    name: str = None


@dataclass
class ColorPalette(_BaseModel):
    flags: int = None
    entries: List[Color] = None


@dataclass
class CreateGroupRequest(_BaseModel):
    """创建群聊。"""

    group_name: str = None
    first_who: str = None
    members: List[str] = None


@dataclass
class FriendInfo(_BaseModel):
    """个人信息."""

    nick_name: str = None
    memo_name: str = None
    area: str = None
    lable: List[str] = None
    same_group_number: str = None
    signature: str = None
    source: str = None
    wx_id: str = None
    avatar_path: str = None
    avatar_image: Image = None
    avatar_image_base64: str = None
    friend_search_result: FriendSearchResultEnums = None
    chat_type: ChatType = None
    add_date_time: str = None
    name: str = None


@dataclass
class GroupMemberRequest(_BaseModel):
    """添加/移除群成员。group_name 为空时作用于当前焦点群聊。"""

    group_name: str = None
    members: List[str] = None


@dataclass
class Image(_BaseModel):
    tag: Any = None
    physical_dimension: SizeF = None
    size: Size = None
    width: int = None
    height: int = None
    horizontal_resolution: float = None
    vertical_resolution: float = None
    flags: int = None
    raw_format: ImageFormat = None
    pixel_format: PixelFormat = None
    property_id_list: List[int] = None
    property_items: List[PropertyItem] = None
    palette: ColorPalette = None
    frame_dimensions_list: List[str] = None


@dataclass
class ImageFormat(_BaseModel):
    guid: str = None


@dataclass
class InviteGroupMemberRequest(_BaseModel):
    """邀请群成员（外部群）。"""

    group_name: str = None
    members: List[str] = None
    invite_reason: str = None


@dataclass
class LocateConversationRequest(_BaseModel):
    """定位会话。"""

    title: str = None


@dataclass
class NotifyIconClickRequest(_BaseModel):
    """点击任务栏微信图标。index 与 wechat_name 二选一。"""

    index: int = None
    wechat_name: str = None


@dataclass
class PropertyItem(_BaseModel):
    id: int = None
    len: int = None
    type: int = None
    value: str = None


@dataclass
class ReferenceRequest(_BaseModel):
    """引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。"""

    message: ChatSimpleMessage = None
    who: str = None
    message_content: str = None
    prev_scroll_number: int = None


@dataclass
class RenameGroupRequest(_BaseModel):
    """修改群名。"""

    old_group_name: str = None
    new_group_name: str = None


@dataclass
class SaveAvatarRequest(_BaseModel):
    """保存头像到指定路径。"""

    path: str = None


@dataclass
class SearchConversationRequest(_BaseModel):
    """搜索会话。"""

    who: str = None


@dataclass
class SendEmojiRequest(_BaseModel):
    """发送表情。emoji 与 index 二选一（优先使用 index）。"""

    who: str = None
    emoji: str = None
    index: int = None
    at_users: List[str] = None


@dataclass
class SendFileRequest(_BaseModel):
    """发送文件。who 为空时发送给当前焦点聊天窗口。"""

    who: str = None
    files: List[str] = None


@dataclass
class SendMessageRequest(_BaseModel):
    """发送文本消息。who 为空时发送给当前焦点聊天窗口。"""

    who: str = None
    message: str = None
    at_users: List[str] = None
    refer: ChatRefer = None


@dataclass
class SendVoiceChatsRequest(_BaseModel):
    """发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。"""

    who: str = None
    partner: List[str] = None


@dataclass
class SendVoiceMessageRequest(_BaseModel):
    """发送语音消息。who 为空时发送给当前焦点聊天窗口。"""

    who: str = None
    file_path: str = None


@dataclass
class SendVoiceMessageTtsRequest(_BaseModel):
    """文字转语音发送。"""

    who: str = None
    api_key: str = None
    message: str = None
    options: VoiceOptions = None
    optimize_with_llm: bool = None


@dataclass
class SetDoNotDisturbRequest(_BaseModel):
    """设置会话免打扰。who 为空时作用于当前窗口。"""

    who: str = None
    setting: bool = None


@dataclass
class SetTopMostRequest(_BaseModel):
    """设置会话置顶。who 为空时作用于当前窗口。"""

    who: str = None
    setting: bool = None


@dataclass
class Size(_BaseModel):
    is_empty: bool = None
    width: int = None
    height: int = None


@dataclass
class SizeF(_BaseModel):
    is_empty: bool = None
    width: float = None
    height: float = None


@dataclass
class SubWindowRequest(_BaseModel):
    """打开/关闭指定好友或群的子窗口。"""

    who: str = None


@dataclass
class SwitchNavigationRequest(_BaseModel):
    """切换/关闭导航栏窗口。"""

    navigation_type: NavigationType = None


@dataclass
class TapRequest(_BaseModel):
    """拍一拍。"""

    who: str = None
    prev_scroll_number: int = None


@dataclass
class UpdateGroupNoticeRequest(_BaseModel):
    """更新群公告。"""

    group_name: str = None
    group_notice: str = None


@dataclass
class VoiceOptions(_BaseModel):
    voice: str = None
    model: str = None
    language_type: str = None
    instructions: str = None
    volume: int = None
    rate: float = None
    pitch: float = None
    seed: int = None
    optimize_instructions: bool = None
    is_stream: bool = None


@dataclass
class WhoRequest(_BaseModel):
    """仅携带 who（可为空，作用于当前焦点窗口）的请求。"""

    who: str = None
