/*
 * ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。
 * （This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）
 */

#ifndef WECHATAUTO_CLIENT_HPP
#define WECHATAUTO_CLIENT_HPP

#include <cstdint>
#include <cctype>
#include <map>
#include <memory>
#include <optional>
#include <stdexcept>
#include <string>
#include <utility>
#include <vector>

#include <nlohmann/json.hpp>

namespace wechatauto {

// ---------------------------------------------------------------------------
// Enums
// ---------------------------------------------------------------------------

enum class ChatType {
    Friend,
    EnterpriseWeChat,
    Group,
    OfficialAccount,
    SubscriptionAccount,
    ServiceNotification,
    TencentNews,
    WeChatTeam,
    FileTransferHelper,
    Other
};

NLOHMANN_JSON_SERIALIZE_ENUM(ChatType, {
    {ChatType::Friend, "好友"},
    {ChatType::EnterpriseWeChat, "企业微信"},
    {ChatType::Group, "群聊"},
    {ChatType::OfficialAccount, "公众号"},
    {ChatType::SubscriptionAccount, "订阅号"},
    {ChatType::ServiceNotification, "服务通知"},
    {ChatType::TencentNews, "腾讯新闻"},
    {ChatType::WeChatTeam, "微信团队"},
    {ChatType::FileTransferHelper, "文件传输助手"},
    {ChatType::Other, "其他"},
})

enum class FriendSearchResult {
    Friend,
    NoFind,
    NotFriend
};

NLOHMANN_JSON_SERIALIZE_ENUM(FriendSearchResult, {
    {FriendSearchResult::Friend, "Friend"},
    {FriendSearchResult::NoFind, "No_Find"},
    {FriendSearchResult::NotFriend, "NotFriend"},
})

enum class NavigationType {
    Chat,
    Contacts,
    Favorites,
    ChatFiles,
    Moments,
    Search,
    Channels,
    TopStories,
    MiniProgramPanel,
    Phone,
    Settings,
    WeChat,
    GameCenter,
    More
};

NLOHMANN_JSON_SERIALIZE_ENUM(NavigationType, {
    {NavigationType::Chat, "聊天"},
    {NavigationType::Contacts, "通讯录"},
    {NavigationType::Favorites, "收藏"},
    {NavigationType::ChatFiles, "聊天文件"},
    {NavigationType::Moments, "朋友圈"},
    {NavigationType::Search, "搜一搜"},
    {NavigationType::Channels, "视频号"},
    {NavigationType::TopStories, "看一看"},
    {NavigationType::MiniProgramPanel, "小程序面板"},
    {NavigationType::Phone, "手机"},
    {NavigationType::Settings, "设置及其他"},
    {NavigationType::WeChat, "微信"},
    {NavigationType::GameCenter, "游戏中心"},
    {NavigationType::More, "更多"},
})

enum class PixelFormat {
    DontCare,
    Max,
    Indexed,
    Gdi,
    Format16bppRgb555,
    Format16bppRgb565,
    Format24bppRgb,
    Format32bppRgb,
    Format1bppIndexed,
    Format4bppIndexed,
    Format8bppIndexed,
    Alpha,
    Format16bppArgb1555,
    PAlpha,
    Format32bppPArgb,
    Extended,
    Format16bppGrayScale,
    Format48bppRgb,
    Format64bppPArgb,
    Canonical,
    Format32bppArgb,
    Format64bppArgb
};

NLOHMANN_JSON_SERIALIZE_ENUM(PixelFormat, {
    {PixelFormat::DontCare, "DontCare"},
    {PixelFormat::Max, "Max"},
    {PixelFormat::Indexed, "Indexed"},
    {PixelFormat::Gdi, "Gdi"},
    {PixelFormat::Format16bppRgb555, "Format16bppRgb555"},
    {PixelFormat::Format16bppRgb565, "Format16bppRgb565"},
    {PixelFormat::Format24bppRgb, "Format24bppRgb"},
    {PixelFormat::Format32bppRgb, "Format32bppRgb"},
    {PixelFormat::Format1bppIndexed, "Format1bppIndexed"},
    {PixelFormat::Format4bppIndexed, "Format4bppIndexed"},
    {PixelFormat::Format8bppIndexed, "Format8bppIndexed"},
    {PixelFormat::Alpha, "Alpha"},
    {PixelFormat::Format16bppArgb1555, "Format16bppArgb1555"},
    {PixelFormat::PAlpha, "PAlpha"},
    {PixelFormat::Format32bppPArgb, "Format32bppPArgb"},
    {PixelFormat::Extended, "Extended"},
    {PixelFormat::Format16bppGrayScale, "Format16bppGrayScale"},
    {PixelFormat::Format48bppRgb, "Format48bppRgb"},
    {PixelFormat::Format64bppPArgb, "Format64bppPArgb"},
    {PixelFormat::Canonical, "Canonical"},
    {PixelFormat::Format32bppArgb, "Format32bppArgb"},
    {PixelFormat::Format64bppArgb, "Format64bppArgb"},
})

// ---------------------------------------------------------------------------
// Models
// ---------------------------------------------------------------------------

namespace detail {

inline bool has(const nlohmann::json& j, const char* key) {
    return j.is_object() && j.contains(key) && !j.at(key).is_null();
}

template <typename T>
inline nlohmann::json to_json_value(const T& value) {
    nlohmann::json j;
    to_json(j, value);
    return j;
}

}  // namespace detail

struct Size {
    bool is_empty = false;
    std::int32_t width = 0;
    std::int32_t height = 0;
};

inline void to_json(nlohmann::json& j, const Size& m) {
    j = nlohmann::json::object();
    j["is_empty"] = m.is_empty;
    j["width"] = m.width;
    j["height"] = m.height;
}

inline void from_json(const nlohmann::json& j, Size& m) {
    if (j.is_object()) {
        if (j.contains("is_empty")) j.at("is_empty").get_to(m.is_empty);
        if (j.contains("width")) j.at("width").get_to(m.width);
        if (j.contains("height")) j.at("height").get_to(m.height);
    }
}

struct SizeF {
    bool is_empty = false;
    double width = 0.0;
    double height = 0.0;
};

inline void to_json(nlohmann::json& j, const SizeF& m) {
    j = nlohmann::json::object();
    j["is_empty"] = m.is_empty;
    j["width"] = m.width;
    j["height"] = m.height;
}

inline void from_json(const nlohmann::json& j, SizeF& m) {
    if (j.is_object()) {
        if (j.contains("is_empty")) j.at("is_empty").get_to(m.is_empty);
        if (j.contains("width")) j.at("width").get_to(m.width);
        if (j.contains("height")) j.at("height").get_to(m.height);
    }
}

struct Color {
    std::int32_t r = 0;
    std::int32_t g = 0;
    std::int32_t b = 0;
    std::int32_t a = 0;
    bool is_known_color = false;
    bool is_empty = false;
    bool is_named_color = false;
    bool is_system_color = false;
    std::optional<std::string> name;
};

inline void to_json(nlohmann::json& j, const Color& m) {
    j = nlohmann::json::object();
    j["r"] = m.r;
    j["g"] = m.g;
    j["b"] = m.b;
    j["a"] = m.a;
    j["is_known_color"] = m.is_known_color;
    j["is_empty"] = m.is_empty;
    j["is_named_color"] = m.is_named_color;
    j["is_system_color"] = m.is_system_color;
    if (m.name) j["name"] = *m.name;
}

inline void from_json(const nlohmann::json& j, Color& m) {
    if (j.is_object()) {
        if (j.contains("r")) j.at("r").get_to(m.r);
        if (j.contains("g")) j.at("g").get_to(m.g);
        if (j.contains("b")) j.at("b").get_to(m.b);
        if (j.contains("a")) j.at("a").get_to(m.a);
        if (j.contains("is_known_color")) j.at("is_known_color").get_to(m.is_known_color);
        if (j.contains("is_empty")) j.at("is_empty").get_to(m.is_empty);
        if (j.contains("is_named_color")) j.at("is_named_color").get_to(m.is_named_color);
        if (j.contains("is_system_color")) j.at("is_system_color").get_to(m.is_system_color);
        if (detail::has(j, "name")) m.name = j.at("name").get<std::string>();
    }
}

struct ColorPalette {
    std::int32_t flags = 0;
    std::optional<std::vector<Color>> entries;
};

inline void to_json(nlohmann::json& j, const ColorPalette& m) {
    j = nlohmann::json::object();
    j["flags"] = m.flags;
    if (m.entries) j["entries"] = *m.entries;
}

inline void from_json(const nlohmann::json& j, ColorPalette& m) {
    if (j.is_object()) {
        if (j.contains("flags")) j.at("flags").get_to(m.flags);
        if (detail::has(j, "entries")) m.entries = j.at("entries").get<std::vector<Color>>();
    }
}

struct PropertyItem {
    std::int32_t id = 0;
    std::int32_t len = 0;
    std::int32_t type = 0;
    std::optional<std::string> value;
};

inline void to_json(nlohmann::json& j, const PropertyItem& m) {
    j = nlohmann::json::object();
    j["id"] = m.id;
    j["len"] = m.len;
    j["type"] = m.type;
    if (m.value) j["value"] = *m.value;
}

inline void from_json(const nlohmann::json& j, PropertyItem& m) {
    if (j.is_object()) {
        if (j.contains("id")) j.at("id").get_to(m.id);
        if (j.contains("len")) j.at("len").get_to(m.len);
        if (j.contains("type")) j.at("type").get_to(m.type);
        if (detail::has(j, "value")) m.value = j.at("value").get<std::string>();
    }
}

struct ImageFormat {
    std::string guid;
};

inline void to_json(nlohmann::json& j, const ImageFormat& m) {
    j = nlohmann::json::object();
    j["guid"] = m.guid;
}

inline void from_json(const nlohmann::json& j, ImageFormat& m) {
    if (j.is_object()) {
        if (j.contains("guid")) j.at("guid").get_to(m.guid);
    }
}

struct Image {
    nlohmann::json tag;
    std::optional<SizeF> physical_dimension;
    std::optional<Size> size;
    std::int32_t width = 0;
    std::int32_t height = 0;
    double horizontal_resolution = 0.0;
    double vertical_resolution = 0.0;
    std::int32_t flags = 0;
    std::optional<ImageFormat> raw_format;
    std::optional<PixelFormat> pixel_format;
    std::optional<std::vector<std::int32_t>> property_id_list;
    std::optional<std::vector<PropertyItem>> property_items;
    std::optional<ColorPalette> palette;
    std::optional<std::vector<std::string>> frame_dimensions_list;
};

inline void to_json(nlohmann::json& j, const Image& m) {
    j = nlohmann::json::object();
    if (!m.tag.is_null()) j["tag"] = m.tag;
    if (m.physical_dimension) j["physical_dimension"] = *m.physical_dimension;
    if (m.size) j["size"] = *m.size;
    j["width"] = m.width;
    j["height"] = m.height;
    j["horizontal_resolution"] = m.horizontal_resolution;
    j["vertical_resolution"] = m.vertical_resolution;
    j["flags"] = m.flags;
    if (m.raw_format) j["raw_format"] = *m.raw_format;
    if (m.pixel_format) j["pixel_format"] = *m.pixel_format;
    if (m.property_id_list) j["property_id_list"] = *m.property_id_list;
    if (m.property_items) j["property_items"] = *m.property_items;
    if (m.palette) j["palette"] = *m.palette;
    if (m.frame_dimensions_list) j["frame_dimensions_list"] = *m.frame_dimensions_list;
}

inline void from_json(const nlohmann::json& j, Image& m) {
    if (j.is_object()) {
        if (j.contains("tag")) m.tag = j.at("tag");
        if (detail::has(j, "physical_dimension")) m.physical_dimension = j.at("physical_dimension").get<SizeF>();
        if (detail::has(j, "size")) m.size = j.at("size").get<Size>();
        if (j.contains("width")) j.at("width").get_to(m.width);
        if (j.contains("height")) j.at("height").get_to(m.height);
        if (j.contains("horizontal_resolution")) j.at("horizontal_resolution").get_to(m.horizontal_resolution);
        if (j.contains("vertical_resolution")) j.at("vertical_resolution").get_to(m.vertical_resolution);
        if (j.contains("flags")) j.at("flags").get_to(m.flags);
        if (detail::has(j, "raw_format")) m.raw_format = j.at("raw_format").get<ImageFormat>();
        if (detail::has(j, "pixel_format")) m.pixel_format = j.at("pixel_format").get<PixelFormat>();
        if (detail::has(j, "property_id_list")) m.property_id_list = j.at("property_id_list").get<std::vector<std::int32_t>>();
        if (detail::has(j, "property_items")) m.property_items = j.at("property_items").get<std::vector<PropertyItem>>();
        if (detail::has(j, "palette")) m.palette = j.at("palette").get<ColorPalette>();
        if (detail::has(j, "frame_dimensions_list")) m.frame_dimensions_list = j.at("frame_dimensions_list").get<std::vector<std::string>>();
    }
}

struct VoiceOptions {
    std::optional<std::string> voice;
    std::optional<std::string> model;
    std::optional<std::string> language_type;
    std::optional<std::string> instructions;
    std::optional<std::int32_t> volume;
    std::optional<double> rate;
    std::optional<double> pitch;
    std::optional<std::int32_t> seed;
    bool optimize_instructions = false;
    bool is_stream = false;
};

inline void to_json(nlohmann::json& j, const VoiceOptions& m) {
    j = nlohmann::json::object();
    if (m.voice) j["voice"] = *m.voice;
    if (m.model) j["model"] = *m.model;
    if (m.language_type) j["language_type"] = *m.language_type;
    if (m.instructions) j["instructions"] = *m.instructions;
    if (m.volume) j["volume"] = *m.volume;
    if (m.rate) j["rate"] = *m.rate;
    if (m.pitch) j["pitch"] = *m.pitch;
    if (m.seed) j["seed"] = *m.seed;
    j["optimize_instructions"] = m.optimize_instructions;
    j["is_stream"] = m.is_stream;
}

inline void from_json(const nlohmann::json& j, VoiceOptions& m) {
    if (j.is_object()) {
        if (detail::has(j, "voice")) m.voice = j.at("voice").get<std::string>();
        if (detail::has(j, "model")) m.model = j.at("model").get<std::string>();
        if (detail::has(j, "language_type")) m.language_type = j.at("language_type").get<std::string>();
        if (detail::has(j, "instructions")) m.instructions = j.at("instructions").get<std::string>();
        if (detail::has(j, "volume")) m.volume = j.at("volume").get<std::int32_t>();
        if (detail::has(j, "rate")) m.rate = j.at("rate").get<double>();
        if (detail::has(j, "pitch")) m.pitch = j.at("pitch").get<double>();
        if (detail::has(j, "seed")) m.seed = j.at("seed").get<std::int32_t>();
        if (j.contains("optimize_instructions")) j.at("optimize_instructions").get_to(m.optimize_instructions);
        if (j.contains("is_stream")) j.at("is_stream").get_to(m.is_stream);
    }
}

struct ChatSimpleMessage {
    std::optional<std::string> who;
    std::optional<std::string> message;
    std::optional<std::string> send_date_time;
    std::string date_time;
    std::optional<std::string> unique_string;
};

inline void to_json(nlohmann::json& j, const ChatSimpleMessage& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.message) j["message"] = *m.message;
    if (m.send_date_time) j["send_date_time"] = *m.send_date_time;
    j["date_time"] = m.date_time;
    if (m.unique_string) j["unique_string"] = *m.unique_string;
}

inline void from_json(const nlohmann::json& j, ChatSimpleMessage& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "message")) m.message = j.at("message").get<std::string>();
        if (detail::has(j, "send_date_time")) m.send_date_time = j.at("send_date_time").get<std::string>();
        if (j.contains("date_time")) j.at("date_time").get_to(m.date_time);
        if (detail::has(j, "unique_string")) m.unique_string = j.at("unique_string").get<std::string>();
    }
}

struct ChatRefer {
    std::string date;
    std::optional<ChatSimpleMessage> message;
    bool is_close_search_win = false;
};

inline void to_json(nlohmann::json& j, const ChatRefer& m) {
    j = nlohmann::json::object();
    j["date"] = m.date;
    if (m.message) j["message"] = *m.message;
    j["is_close_search_win"] = m.is_close_search_win;
}

inline void from_json(const nlohmann::json& j, ChatRefer& m) {
    if (j.is_object()) {
        if (j.contains("date")) j.at("date").get_to(m.date);
        if (detail::has(j, "message")) m.message = j.at("message").get<ChatSimpleMessage>();
        if (j.contains("is_close_search_win")) j.at("is_close_search_win").get_to(m.is_close_search_win);
    }
}

struct AddFriendsOptions {
    std::int32_t interval_time = 0;
    bool is_close_win = false;
    std::optional<std::string> say_hi;
    std::optional<std::string> suffix;
    std::optional<std::string> label;
};

inline void to_json(nlohmann::json& j, const AddFriendsOptions& m) {
    j = nlohmann::json::object();
    j["interval_time"] = m.interval_time;
    j["is_close_win"] = m.is_close_win;
    if (m.say_hi) j["say_hi"] = *m.say_hi;
    if (m.suffix) j["suffix"] = *m.suffix;
    if (m.label) j["label"] = *m.label;
}

inline void from_json(const nlohmann::json& j, AddFriendsOptions& m) {
    if (j.is_object()) {
        if (j.contains("interval_time")) j.at("interval_time").get_to(m.interval_time);
        if (j.contains("is_close_win")) j.at("is_close_win").get_to(m.is_close_win);
        if (detail::has(j, "say_hi")) m.say_hi = j.at("say_hi").get<std::string>();
        if (detail::has(j, "suffix")) m.suffix = j.at("suffix").get<std::string>();
        if (detail::has(j, "label")) m.label = j.at("label").get<std::string>();
    }
}

struct AddFriendsRequest {
    std::optional<std::vector<std::string>> friends;
    std::optional<AddFriendsOptions> options;
};

inline void to_json(nlohmann::json& j, const AddFriendsRequest& m) {
    j = nlohmann::json::object();
    if (m.friends) j["friends"] = *m.friends;
    if (m.options) j["options"] = *m.options;
}

inline void from_json(const nlohmann::json& j, AddFriendsRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "friends")) m.friends = j.at("friends").get<std::vector<std::string>>();
        if (detail::has(j, "options")) m.options = j.at("options").get<AddFriendsOptions>();
    }
}

struct AddGroupMemberToFriendsRequest {
    std::optional<std::string> group_name;
    std::optional<std::vector<std::string>> members;
    std::optional<AddFriendsOptions> options;
};

inline void to_json(nlohmann::json& j, const AddGroupMemberToFriendsRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.members) j["members"] = *m.members;
    if (m.options) j["options"] = *m.options;
}

inline void from_json(const nlohmann::json& j, AddGroupMemberToFriendsRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "members")) m.members = j.at("members").get<std::vector<std::string>>();
        if (detail::has(j, "options")) m.options = j.at("options").get<AddFriendsOptions>();
    }
}

struct ChangeGroupMemoRequest {
    std::optional<std::string> group_name;
    std::optional<std::string> new_memo;
};

inline void to_json(nlohmann::json& j, const ChangeGroupMemoRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.new_memo) j["new_memo"] = *m.new_memo;
}

inline void from_json(const nlohmann::json& j, ChangeGroupMemoRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "new_memo")) m.new_memo = j.at("new_memo").get<std::string>();
    }
}

struct ChangeGroupNickNameRequest {
    std::optional<std::string> group_name;
    std::optional<std::string> nick_name;
};

inline void to_json(nlohmann::json& j, const ChangeGroupNickNameRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.nick_name) j["nick_name"] = *m.nick_name;
}

inline void from_json(const nlohmann::json& j, ChangeGroupNickNameRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "nick_name")) m.nick_name = j.at("nick_name").get<std::string>();
    }
}

struct CreateGroupRequest {
    std::optional<std::string> group_name;
    std::optional<std::string> first_who;
    std::optional<std::vector<std::string>> members;
};

inline void to_json(nlohmann::json& j, const CreateGroupRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.first_who) j["first_who"] = *m.first_who;
    if (m.members) j["members"] = *m.members;
}

inline void from_json(const nlohmann::json& j, CreateGroupRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "first_who")) m.first_who = j.at("first_who").get<std::string>();
        if (detail::has(j, "members")) m.members = j.at("members").get<std::vector<std::string>>();
    }
}

struct FriendInfo {
    std::optional<std::string> nick_name;
    std::optional<std::string> memo_name;
    std::optional<std::string> area;
    std::optional<std::vector<std::string>> lable;
    std::optional<std::string> same_group_number;
    std::optional<std::string> signature;
    std::optional<std::string> source;
    std::optional<std::string> wx_id;
    std::optional<std::string> avatar_path;
    std::optional<Image> avatar_image;
    std::optional<std::string> avatar_image_base64;
    std::optional<FriendSearchResult> friend_search_result;
    std::optional<ChatType> chat_type;
    std::optional<std::string> add_date_time;
    std::optional<std::string> name;
};

inline void to_json(nlohmann::json& j, const FriendInfo& m) {
    j = nlohmann::json::object();
    if (m.nick_name) j["nick_name"] = *m.nick_name;
    if (m.memo_name) j["memo_name"] = *m.memo_name;
    if (m.area) j["area"] = *m.area;
    if (m.lable) j["lable"] = *m.lable;
    if (m.same_group_number) j["same_group_number"] = *m.same_group_number;
    if (m.signature) j["signature"] = *m.signature;
    if (m.source) j["source"] = *m.source;
    if (m.wx_id) j["wx_id"] = *m.wx_id;
    if (m.avatar_path) j["avatar_path"] = *m.avatar_path;
    if (m.avatar_image) j["avatar_image"] = *m.avatar_image;
    if (m.avatar_image_base64) j["avatar_image_base64"] = *m.avatar_image_base64;
    if (m.friend_search_result) j["friend_search_result"] = *m.friend_search_result;
    if (m.chat_type) j["chat_type"] = *m.chat_type;
    if (m.add_date_time) j["add_date_time"] = *m.add_date_time;
    if (m.name) j["name"] = *m.name;
}

inline void from_json(const nlohmann::json& j, FriendInfo& m) {
    if (j.is_object()) {
        if (detail::has(j, "nick_name")) m.nick_name = j.at("nick_name").get<std::string>();
        if (detail::has(j, "memo_name")) m.memo_name = j.at("memo_name").get<std::string>();
        if (detail::has(j, "area")) m.area = j.at("area").get<std::string>();
        if (detail::has(j, "lable")) m.lable = j.at("lable").get<std::vector<std::string>>();
        if (detail::has(j, "same_group_number")) m.same_group_number = j.at("same_group_number").get<std::string>();
        if (detail::has(j, "signature")) m.signature = j.at("signature").get<std::string>();
        if (detail::has(j, "source")) m.source = j.at("source").get<std::string>();
        if (detail::has(j, "wx_id")) m.wx_id = j.at("wx_id").get<std::string>();
        if (detail::has(j, "avatar_path")) m.avatar_path = j.at("avatar_path").get<std::string>();
        if (detail::has(j, "avatar_image")) m.avatar_image = j.at("avatar_image").get<Image>();
        if (detail::has(j, "avatar_image_base64")) m.avatar_image_base64 = j.at("avatar_image_base64").get<std::string>();
        if (detail::has(j, "friend_search_result")) m.friend_search_result = j.at("friend_search_result").get<FriendSearchResult>();
        if (detail::has(j, "chat_type")) m.chat_type = j.at("chat_type").get<ChatType>();
        if (detail::has(j, "add_date_time")) m.add_date_time = j.at("add_date_time").get<std::string>();
        if (detail::has(j, "name")) m.name = j.at("name").get<std::string>();
    }
}

struct GroupMemberRequest {
    std::optional<std::string> group_name;
    std::optional<std::vector<std::string>> members;
};

inline void to_json(nlohmann::json& j, const GroupMemberRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.members) j["members"] = *m.members;
}

inline void from_json(const nlohmann::json& j, GroupMemberRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "members")) m.members = j.at("members").get<std::vector<std::string>>();
    }
}

struct InviteGroupMemberRequest {
    std::optional<std::string> group_name;
    std::optional<std::vector<std::string>> members;
    std::optional<std::string> invite_reason;
};

inline void to_json(nlohmann::json& j, const InviteGroupMemberRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.members) j["members"] = *m.members;
    if (m.invite_reason) j["invite_reason"] = *m.invite_reason;
}

inline void from_json(const nlohmann::json& j, InviteGroupMemberRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "members")) m.members = j.at("members").get<std::vector<std::string>>();
        if (detail::has(j, "invite_reason")) m.invite_reason = j.at("invite_reason").get<std::string>();
    }
}

struct LocateConversationRequest {
    std::optional<std::string> title;
};

inline void to_json(nlohmann::json& j, const LocateConversationRequest& m) {
    j = nlohmann::json::object();
    if (m.title) j["title"] = *m.title;
}

inline void from_json(const nlohmann::json& j, LocateConversationRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "title")) m.title = j.at("title").get<std::string>();
    }
}

struct NotifyIconClickRequest {
    std::optional<std::int32_t> index;
    std::optional<std::string> wechat_name;
};

inline void to_json(nlohmann::json& j, const NotifyIconClickRequest& m) {
    j = nlohmann::json::object();
    if (m.index) j["index"] = *m.index;
    if (m.wechat_name) j["wechat_name"] = *m.wechat_name;
}

inline void from_json(const nlohmann::json& j, NotifyIconClickRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "index")) m.index = j.at("index").get<std::int32_t>();
        if (detail::has(j, "wechat_name")) m.wechat_name = j.at("wechat_name").get<std::string>();
    }
}

struct ReferenceRequest {
    std::optional<ChatSimpleMessage> message;
    std::optional<std::string> who;
    std::optional<std::string> message_content;
    std::int32_t prev_scroll_number = 0;
};

inline void to_json(nlohmann::json& j, const ReferenceRequest& m) {
    j = nlohmann::json::object();
    if (m.message) j["message"] = *m.message;
    if (m.who) j["who"] = *m.who;
    if (m.message_content) j["message_content"] = *m.message_content;
    j["prev_scroll_number"] = m.prev_scroll_number;
}

inline void from_json(const nlohmann::json& j, ReferenceRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "message")) m.message = j.at("message").get<ChatSimpleMessage>();
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "message_content")) m.message_content = j.at("message_content").get<std::string>();
        if (j.contains("prev_scroll_number")) j.at("prev_scroll_number").get_to(m.prev_scroll_number);
    }
}

struct RenameGroupRequest {
    std::optional<std::string> old_group_name;
    std::optional<std::string> new_group_name;
};

inline void to_json(nlohmann::json& j, const RenameGroupRequest& m) {
    j = nlohmann::json::object();
    if (m.old_group_name) j["old_group_name"] = *m.old_group_name;
    if (m.new_group_name) j["new_group_name"] = *m.new_group_name;
}

inline void from_json(const nlohmann::json& j, RenameGroupRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "old_group_name")) m.old_group_name = j.at("old_group_name").get<std::string>();
        if (detail::has(j, "new_group_name")) m.new_group_name = j.at("new_group_name").get<std::string>();
    }
}

struct SaveAvatarRequest {
    std::optional<std::string> path;
};

inline void to_json(nlohmann::json& j, const SaveAvatarRequest& m) {
    j = nlohmann::json::object();
    if (m.path) j["path"] = *m.path;
}

inline void from_json(const nlohmann::json& j, SaveAvatarRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "path")) m.path = j.at("path").get<std::string>();
    }
}

struct SearchConversationRequest {
    std::optional<std::string> who;
};

inline void to_json(nlohmann::json& j, const SearchConversationRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
}

inline void from_json(const nlohmann::json& j, SearchConversationRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
    }
}

struct SendEmojiRequest {
    std::optional<std::string> who;
    std::optional<std::string> emoji;
    std::optional<std::int32_t> index;
    std::optional<std::vector<std::string>> at_users;
};

inline void to_json(nlohmann::json& j, const SendEmojiRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.emoji) j["emoji"] = *m.emoji;
    if (m.index) j["index"] = *m.index;
    if (m.at_users) j["at_users"] = *m.at_users;
}

inline void from_json(const nlohmann::json& j, SendEmojiRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "emoji")) m.emoji = j.at("emoji").get<std::string>();
        if (detail::has(j, "index")) m.index = j.at("index").get<std::int32_t>();
        if (detail::has(j, "at_users")) m.at_users = j.at("at_users").get<std::vector<std::string>>();
    }
}

struct SendFileRequest {
    std::optional<std::string> who;
    std::optional<std::vector<std::string>> files;
};

inline void to_json(nlohmann::json& j, const SendFileRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.files) j["files"] = *m.files;
}

inline void from_json(const nlohmann::json& j, SendFileRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "files")) m.files = j.at("files").get<std::vector<std::string>>();
    }
}

struct SendMessageRequest {
    std::optional<std::string> who;
    std::optional<std::string> message;
    std::optional<std::vector<std::string>> at_users;
    std::optional<ChatRefer> refer;
};

inline void to_json(nlohmann::json& j, const SendMessageRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.message) j["message"] = *m.message;
    if (m.at_users) j["at_users"] = *m.at_users;
    if (m.refer) j["refer"] = *m.refer;
}

inline void from_json(const nlohmann::json& j, SendMessageRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "message")) m.message = j.at("message").get<std::string>();
        if (detail::has(j, "at_users")) m.at_users = j.at("at_users").get<std::vector<std::string>>();
        if (detail::has(j, "refer")) m.refer = j.at("refer").get<ChatRefer>();
    }
}

struct SendVoiceChatsRequest {
    std::optional<std::string> who;
    std::optional<std::vector<std::string>> partner;
};

inline void to_json(nlohmann::json& j, const SendVoiceChatsRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.partner) j["partner"] = *m.partner;
}

inline void from_json(const nlohmann::json& j, SendVoiceChatsRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "partner")) m.partner = j.at("partner").get<std::vector<std::string>>();
    }
}

struct SendVoiceMessageRequest {
    std::optional<std::string> who;
    std::optional<std::string> file_path;
};

inline void to_json(nlohmann::json& j, const SendVoiceMessageRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.file_path) j["file_path"] = *m.file_path;
}

inline void from_json(const nlohmann::json& j, SendVoiceMessageRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "file_path")) m.file_path = j.at("file_path").get<std::string>();
    }
}

struct SendVoiceMessageTtsRequest {
    std::optional<std::string> who;
    std::optional<std::string> api_key;
    std::optional<std::string> message;
    std::optional<VoiceOptions> options;
    bool optimize_with_llm = false;
};

inline void to_json(nlohmann::json& j, const SendVoiceMessageTtsRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    if (m.api_key) j["api_key"] = *m.api_key;
    if (m.message) j["message"] = *m.message;
    if (m.options) j["options"] = *m.options;
    j["optimize_with_llm"] = m.optimize_with_llm;
}

inline void from_json(const nlohmann::json& j, SendVoiceMessageTtsRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (detail::has(j, "api_key")) m.api_key = j.at("api_key").get<std::string>();
        if (detail::has(j, "message")) m.message = j.at("message").get<std::string>();
        if (detail::has(j, "options")) m.options = j.at("options").get<VoiceOptions>();
        if (j.contains("optimize_with_llm")) j.at("optimize_with_llm").get_to(m.optimize_with_llm);
    }
}

struct SetDoNotDisturbRequest {
    std::optional<std::string> who;
    bool setting = false;
};

inline void to_json(nlohmann::json& j, const SetDoNotDisturbRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    j["setting"] = m.setting;
}

inline void from_json(const nlohmann::json& j, SetDoNotDisturbRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (j.contains("setting")) j.at("setting").get_to(m.setting);
    }
}

struct SetTopMostRequest {
    std::optional<std::string> who;
    bool setting = false;
};

inline void to_json(nlohmann::json& j, const SetTopMostRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    j["setting"] = m.setting;
}

inline void from_json(const nlohmann::json& j, SetTopMostRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (j.contains("setting")) j.at("setting").get_to(m.setting);
    }
}

struct SubWindowRequest {
    std::optional<std::string> who;
};

inline void to_json(nlohmann::json& j, const SubWindowRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
}

inline void from_json(const nlohmann::json& j, SubWindowRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
    }
}

struct SwitchNavigationRequest {
    std::optional<NavigationType> navigation_type;
};

inline void to_json(nlohmann::json& j, const SwitchNavigationRequest& m) {
    j = nlohmann::json::object();
    if (m.navigation_type) j["navigation_type"] = *m.navigation_type;
}

inline void from_json(const nlohmann::json& j, SwitchNavigationRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "navigation_type")) m.navigation_type = j.at("navigation_type").get<NavigationType>();
    }
}

struct TapRequest {
    std::optional<std::string> who;
    std::int32_t prev_scroll_number = 0;
};

inline void to_json(nlohmann::json& j, const TapRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
    j["prev_scroll_number"] = m.prev_scroll_number;
}

inline void from_json(const nlohmann::json& j, TapRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
        if (j.contains("prev_scroll_number")) j.at("prev_scroll_number").get_to(m.prev_scroll_number);
    }
}

struct UpdateGroupNoticeRequest {
    std::optional<std::string> group_name;
    std::optional<std::string> group_notice;
};

inline void to_json(nlohmann::json& j, const UpdateGroupNoticeRequest& m) {
    j = nlohmann::json::object();
    if (m.group_name) j["group_name"] = *m.group_name;
    if (m.group_notice) j["group_notice"] = *m.group_notice;
}

inline void from_json(const nlohmann::json& j, UpdateGroupNoticeRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "group_name")) m.group_name = j.at("group_name").get<std::string>();
        if (detail::has(j, "group_notice")) m.group_notice = j.at("group_notice").get<std::string>();
    }
}

struct WhoRequest {
    std::optional<std::string> who;
};

inline void to_json(nlohmann::json& j, const WhoRequest& m) {
    j = nlohmann::json::object();
    if (m.who) j["who"] = *m.who;
}

inline void from_json(const nlohmann::json& j, WhoRequest& m) {
    if (j.is_object()) {
        if (detail::has(j, "who")) m.who = j.at("who").get<std::string>();
    }
}

// ---------------------------------------------------------------------------
// HTTP transport
// ---------------------------------------------------------------------------

struct HttpResponse {
    long status_code = 0;
    std::string body;
};

class HttpTransport {
public:
    virtual ~HttpTransport() = default;

    virtual HttpResponse request(const std::string& method,
                                 const std::string& url,
                                 const std::map<std::string, std::string>& headers,
                                 const std::string& body) = 0;
};

#if defined(WECHATAUTO_HAS_CURL)
#include <curl/curl.h>

class CurlTransport : public HttpTransport {
public:
    CurlTransport() = default;
    explicit CurlTransport(long timeout_ms) : timeout_ms_(timeout_ms) {}

    HttpResponse request(const std::string& method, const std::string& url,
                         const std::map<std::string, std::string>& headers,
                         const std::string& body) override {
        HttpResponse response;
        CURL* curl = curl_easy_init();
        if (curl == nullptr) {
            throw std::runtime_error("curl_easy_init failed");
        }

        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);
        curl_easy_setopt(curl, CURLOPT_TIMEOUT_MS, timeout_ms_);

        curl_slist* header_list = nullptr;
        for (const auto& h : headers) {
            const std::string line = h.first + ": " + h.second;
            header_list = curl_slist_append(header_list, line.c_str());
        }
        if (header_list != nullptr) {
            curl_easy_setopt(curl, CURLOPT_HTTPHEADER, header_list);
        }

        if (method == "POST") {
            curl_easy_setopt(curl, CURLOPT_POST, 1L);
        } else if (method == "PUT") {
            curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, "PUT");
        } else if (method == "DELETE") {
            curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, "DELETE");
        } else if (method == "GET") {
            curl_easy_setopt(curl, CURLOPT_HTTPGET, 1L);
        } else {
            curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, method.c_str());
        }

        if (!body.empty()) {
            curl_easy_setopt(curl, CURLOPT_POSTFIELDS, body.c_str());
            curl_easy_setopt(curl, CURLOPT_POSTFIELDSIZE, static_cast<long>(body.size()));
        }

        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, &CurlTransport::write_callback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response.body);

        const CURLcode res = curl_easy_perform(curl);
        if (res == CURLE_OK) {
            curl_easy_getinfo(curl, CURLINFO_RESPONSE_CODE, &response.status_code);
        }

        curl_slist_free_all(header_list);
        curl_easy_cleanup(curl);

        if (res != CURLE_OK) {
            throw std::runtime_error(std::string("curl_easy_perform failed: ") + curl_easy_strerror(res));
        }
        return response;
    }

private:
    static std::size_t write_callback(char* ptr, std::size_t size, std::size_t nmemb, void* userdata) {
        const std::size_t total = size * nmemb;
        static_cast<std::string*>(userdata)->append(ptr, total);
        return total;
    }

    long timeout_ms_ = 30000;
};
#endif  // WECHATAUTO_HAS_CURL

// ---------------------------------------------------------------------------
// Client
// ---------------------------------------------------------------------------

namespace detail {

inline std::string url_encode(const std::string& value) {
    static const char* hex = "0123456789ABCDEF";
    std::string out;
    out.reserve(value.size() * 3);
    for (unsigned char c : value) {
        if (std::isalnum(c) || c == '-' || c == '_' || c == '.' || c == '~') {
            out += static_cast<char>(c);
        } else {
            out += '%';
            out += hex[(c >> 4) & 0x0F];
            out += hex[c & 0x0F];
        }
    }
    return out;
}

inline void add_query(std::string& query, const std::string& key, const std::string& value) {
    query += (query.empty() ? "?" : "&");
    query += url_encode(key);
    query += "=";
    query += url_encode(value);
}

}  // namespace detail

class WeChatAutoClient {
public:
    WeChatAutoClient()
        : WeChatAutoClient("http://localhost:5000", "", nullptr) {}

    WeChatAutoClient(std::string base_url, std::string api_key = "",
                     std::unique_ptr<HttpTransport> transport = nullptr)
        : base_url_(base_url.empty() ? "http://localhost:5000" : std::move(base_url)),
          api_key_(std::move(api_key)),
          transport_(std::move(transport)) {
        strip_trailing_slash(base_url_);
#if defined(WECHATAUTO_HAS_CURL)
        if (!transport_) {
            transport_ = std::make_unique<CurlTransport>();
        }
#endif
    }

    void set_base_url(std::string base_url) {
        base_url_ = base_url.empty() ? "http://localhost:5000" : std::move(base_url);
        strip_trailing_slash(base_url_);
    }

    void set_api_key(std::string api_key) { api_key_ = std::move(api_key); }

    void set_transport(std::unique_ptr<HttpTransport> transport) { transport_ = std::move(transport); }

    // -----------------------------------------------------------------------
    // Clients
    // -----------------------------------------------------------------------

    std::string getClients() {
        return send("GET", "/api/v1/clients", "", nullptr);
    }

    std::string getClient(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId), "", nullptr);
    }

    std::string getOwnerInfo(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/owner-info", "", nullptr);
    }

    std::string saveAvatar(const std::string& clientId, const SaveAvatarRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/avatar", "",
                    &detail::to_json_value(request));
    }

    std::string notifyIconClick(const std::string& clientId, const NotifyIconClickRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/notify-icon/click", "",
                    &detail::to_json_value(request));
    }

    // -----------------------------------------------------------------------
    // Conversations
    // -----------------------------------------------------------------------

    std::string getConversations(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations", "", nullptr);
    }

    std::string getConversationVisibleTitles(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/visible-titles", "", nullptr);
    }

    std::string getVisibleConversations(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/visible", "", nullptr);
    }

    std::string searchConversation(const std::string& clientId, const SearchConversationRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/search", "",
                    &detail::to_json_value(request));
    }

    std::string locateConversation(const std::string& clientId, const LocateConversationRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/locate", "",
                    &detail::to_json_value(request));
    }

    std::string setDoNotDisturb(const std::string& clientId, const SetDoNotDisturbRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/set-do-not-disturb", "",
                    &detail::to_json_value(request));
    }

    std::string setTopMost(const std::string& clientId, const SetTopMostRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/conversations/set-top-most", "",
                    &detail::to_json_value(request));
    }

    // -----------------------------------------------------------------------
    // Friends
    // -----------------------------------------------------------------------

    std::string openAddFriendWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/add-window/open", "", nullptr);
    }

    std::string closeAddFriendWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/add-window/close", "", nullptr);
    }

    std::string addFriends(const std::string& clientId, const AddFriendsRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/add", "",
                    &detail::to_json_value(request));
    }

    std::string getFriends(const std::string& clientId, bool fromCache = true) {
        std::string query;
        detail::add_query(query, "fromCache", fromCache ? "true" : "false");
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends", query, nullptr);
    }

    std::string getFriendNames(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/names", "", nullptr);
    }

    std::string removeFriend(const std::string& clientId, const std::string& nickName) {
        return send("DELETE", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/" + detail::url_encode(nickName), "", nullptr);
    }

    std::string getFriendsCache(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache", "", nullptr);
    }

    std::string addOrUpdateFriendCache(const std::string& clientId, const FriendInfo& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache", "",
                    &detail::to_json_value(request));
    }

    std::string getFriendsCacheByName(const std::string& clientId, const std::string& who) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache/by-name/" + detail::url_encode(who), "", nullptr);
    }

    std::string getFriendCacheByWxid(const std::string& clientId, const std::string& wxid) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache/by-wxid/" + detail::url_encode(wxid), "", nullptr);
    }

    std::string removeFriendCacheByWxid(const std::string& clientId, const std::string& wxid) {
        return send("DELETE", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache/by-wxid/" + detail::url_encode(wxid), "", nullptr);
    }

    std::string getFriendCache(const std::string& clientId, const std::string& who) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache/" + detail::url_encode(who), "", nullptr);
    }

    std::string removeFriendCache(const std::string& clientId, const std::string& who) {
        return send("DELETE", "/api/v1/clients/" + detail::url_encode(clientId) + "/friends/cache/" + detail::url_encode(who), "", nullptr);
    }

    // -----------------------------------------------------------------------
    // Groups
    // -----------------------------------------------------------------------

    std::string isGroupOwner(const std::string& clientId, const std::string& groupName) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/" + detail::url_encode(groupName) + "/is-owner", "", nullptr);
    }

    std::string getGroupOwner(const std::string& clientId, const std::string& groupName) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/" + detail::url_encode(groupName) + "/owner", "", nullptr);
    }

    std::string getGroupMembers(const std::string& clientId, const std::optional<std::string>& groupName = std::nullopt) {
        std::string query;
        if (groupName) {
            detail::add_query(query, "groupName", *groupName);
        }
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/members", query, nullptr);
    }

    std::string addGroupMembers(const std::string& clientId, const GroupMemberRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/members", "",
                    &detail::to_json_value(request));
    }

    std::string removeGroupMembers(const std::string& clientId, const GroupMemberRequest& request) {
        return send("DELETE", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/members", "",
                    &detail::to_json_value(request));
    }

    std::string quitGroup(const std::string& clientId, const std::string& groupName, bool clearHistory = true) {
        std::string query;
        detail::add_query(query, "clearHistory", clearHistory ? "true" : "false");
        return send("DELETE", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/" + detail::url_encode(groupName), query, nullptr);
    }

    std::string createGroup(const std::string& clientId, const CreateGroupRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups", "",
                    &detail::to_json_value(request));
    }

    std::string renameGroup(const std::string& clientId, const RenameGroupRequest& request) {
        return send("PUT", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/name", "",
                    &detail::to_json_value(request));
    }

    std::string changeGroupNickName(const std::string& clientId, const ChangeGroupNickNameRequest& request) {
        return send("PUT", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/nickname", "",
                    &detail::to_json_value(request));
    }

    std::string changeGroupMemo(const std::string& clientId, const ChangeGroupMemoRequest& request) {
        return send("PUT", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/memo", "",
                    &detail::to_json_value(request));
    }

    std::string updateGroupNotice(const std::string& clientId, const UpdateGroupNoticeRequest& request) {
        return send("PUT", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/notice", "",
                    &detail::to_json_value(request));
    }

    std::string inviteGroupMembers(const std::string& clientId, const InviteGroupMemberRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/invite", "",
                    &detail::to_json_value(request));
    }

    std::string addGroupMembersToFriends(const std::string& clientId, const AddGroupMemberToFriendsRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/groups/add-members-to-friends", "",
                    &detail::to_json_value(request));
    }

    // -----------------------------------------------------------------------
    // Messages
    // -----------------------------------------------------------------------

    std::string getMessageTitle(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/title", "", nullptr);
    }

    std::string getMessageOnlyTitle(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/only-title", "", nullptr);
    }

    std::string focusMessageInput(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/focus-input", "", nullptr);
    }

    std::string sendMessage(const std::string& clientId, const SendMessageRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send", "",
                    &detail::to_json_value(request));
    }

    std::string sendFile(const std::string& clientId, const SendFileRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-file", "",
                    &detail::to_json_value(request));
    }

    std::string sendEmoji(const std::string& clientId, const SendEmojiRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-emoji", "",
                    &detail::to_json_value(request));
    }

    std::string sendVoiceChat(const std::string& clientId, const WhoRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-voice-chat", "",
                    &detail::to_json_value(request));
    }

    std::string sendVideoChat(const std::string& clientId, const WhoRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-video-chat", "",
                    &detail::to_json_value(request));
    }

    std::string sendVoiceChats(const std::string& clientId, const SendVoiceChatsRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-voice-chats", "",
                    &detail::to_json_value(request));
    }

    std::string sendVoiceMessage(const std::string& clientId, const SendVoiceMessageRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-voice-message", "",
                    &detail::to_json_value(request));
    }

    std::string sendVoiceMessageTts(const std::string& clientId, const SendVoiceMessageTtsRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/send-voice-message-tts", "",
                    &detail::to_json_value(request));
    }

    std::string getMessageHistory(const std::string& clientId,
                                  const std::optional<std::string>& who = std::nullopt,
                                  const std::optional<std::string>& date = std::nullopt,
                                  const std::optional<std::string>& startDate = std::nullopt,
                                  const std::optional<std::string>& endDate = std::nullopt) {
        std::string query;
        if (who) detail::add_query(query, "who", *who);
        if (date) detail::add_query(query, "date", *date);
        if (startDate) detail::add_query(query, "start_date", *startDate);
        if (endDate) detail::add_query(query, "end_date", *endDate);
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/history", query, nullptr);
    }

    std::string tapMessage(const std::string& clientId, const TapRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/tap", "",
                    &detail::to_json_value(request));
    }

    std::string referenceMessage(const std::string& clientId, const ReferenceRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/reference", "",
                    &detail::to_json_value(request));
    }

    std::string referenceLastMessage(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/messages/reference-last", "", nullptr);
    }

    // -----------------------------------------------------------------------
    // Navigation
    // -----------------------------------------------------------------------

    std::string switchNavigation(const std::string& clientId, const SwitchNavigationRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/navigation/switch", "",
                    &detail::to_json_value(request));
    }

    std::string closeNavigation(const std::string& clientId, const SwitchNavigationRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/navigation/close", "",
                    &detail::to_json_value(request));
    }

    // -----------------------------------------------------------------------
    // Window
    // -----------------------------------------------------------------------

    std::string maximizeWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/maximize", "", nullptr);
    }

    std::string restoreWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/restore", "", nullptr);
    }

    std::string pinWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/pin", "", nullptr);
    }

    std::string unpinWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/unpin", "", nullptr);
    }

    std::string focusWindow(const std::string& clientId) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/focus", "", nullptr);
    }

    std::string closeSearchWindow(const std::string& clientId, const SubWindowRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/close-search-window", "",
                    &detail::to_json_value(request));
    }

    std::string openSubWindow(const std::string& clientId, const SubWindowRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/open-sub-window", "",
                    &detail::to_json_value(request));
    }

    std::string closeSubWindow(const std::string& clientId, const SubWindowRequest& request) {
        return send("POST", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/close-sub-window", "",
                    &detail::to_json_value(request));
    }

    std::string getWindowHandler(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/handler", "", nullptr);
    }

    std::string getWindowProcessId(const std::string& clientId) {
        return send("GET", "/api/v1/clients/" + detail::url_encode(clientId) + "/window/process-id", "", nullptr);
    }

private:
    std::string send(const std::string& method, const std::string& path, const std::string& query,
                     const nlohmann::json* body) {
        if (!transport_) {
            throw std::runtime_error(
                "no HttpTransport available: provide one via constructor/set_transport, or define WECHATAUTO_HAS_CURL "
                "and link libcurl");
        }

        std::string url = base_url_ + path + query;

        std::map<std::string, std::string> headers;
        if (!api_key_.empty()) {
            headers["X-API-Key"] = api_key_;
        }

        std::string body_str;
        if (body != nullptr) {
            body_str = body->dump();
            headers["Content-Type"] = "application/json";
        }

        return transport_->request(method, url, headers, body_str).body;
    }

    static void strip_trailing_slash(std::string& s) {
        while (!s.empty() && s.back() == '/') {
            s.pop_back();
        }
    }

    std::string base_url_;
    std::string api_key_;
    std::unique_ptr<HttpTransport> transport_;
};

}  // namespace wechatauto

#endif  // WECHATAUTO_CLIENT_HPP
