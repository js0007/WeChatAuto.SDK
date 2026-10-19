using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeChatAuto.Client.Models;

/// <summary>
/// 聊天类型。
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ChatType
{
    /// <summary>好友</summary>
    [JsonPropertyName("好友")] Friend,

    /// <summary>企业微信</summary>
    [JsonPropertyName("企业微信")] EnterpriseWeChat,

    /// <summary>群聊</summary>
    [JsonPropertyName("群聊")] GroupChat,

    /// <summary>公众号</summary>
    [JsonPropertyName("公众号")] OfficialAccount,

    /// <summary>订阅号</summary>
    [JsonPropertyName("订阅号")] Subscription,

    /// <summary>服务通知</summary>
    [JsonPropertyName("服务通知")] ServiceNotification,

    /// <summary>腾讯新闻</summary>
    [JsonPropertyName("腾讯新闻")] TencentNews,

    /// <summary>微信团队</summary>
    [JsonPropertyName("微信团队")] WeChatTeam,

    /// <summary>文件传输助手</summary>
    [JsonPropertyName("文件传输助手")] FileTransferHelper,

    /// <summary>其他</summary>
    [JsonPropertyName("其他")] Other,
}

/// <summary>
/// 通过 wxid 查询，或者通过手机号查询结果枚举。
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum FriendSearchResultEnums
{
    /// <summary>Friend</summary>
    [JsonPropertyName("Friend")] Friend,

    /// <summary>No_Find</summary>
    [JsonPropertyName("No_Find")] NoFind,

    /// <summary>NotFriend</summary>
    [JsonPropertyName("NotFriend")] NotFriend,
}

/// <summary>
/// 导航栏类型。
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum NavigationType
{
    /// <summary>聊天</summary>
    [JsonPropertyName("聊天")] Chat,

    /// <summary>通讯录</summary>
    [JsonPropertyName("通讯录")] Contacts,

    /// <summary>收藏</summary>
    [JsonPropertyName("收藏")] Favorites,

    /// <summary>聊天文件</summary>
    [JsonPropertyName("聊天文件")] ChatFiles,

    /// <summary>朋友圈</summary>
    [JsonPropertyName("朋友圈")] Moments,

    /// <summary>搜一搜</summary>
    [JsonPropertyName("搜一搜")] Search,

    /// <summary>视频号</summary>
    [JsonPropertyName("视频号")] Channels,

    /// <summary>看一看</summary>
    [JsonPropertyName("看一看")] TopStories,

    /// <summary>小程序面板</summary>
    [JsonPropertyName("小程序面板")] MiniProgramsPanel,

    /// <summary>手机</summary>
    [JsonPropertyName("手机")] Phone,

    /// <summary>设置及其他</summary>
    [JsonPropertyName("设置及其他")] SettingsAndOthers,

    /// <summary>微信</summary>
    [JsonPropertyName("微信")] WeChat,

    /// <summary>游戏中心</summary>
    [JsonPropertyName("游戏中心")] GameCenter,

    /// <summary>更多</summary>
    [JsonPropertyName("更多")] More,
}

/// <summary>
/// 像素格式（System.Drawing 的 PixelFormat 枚举值）。
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum PixelFormat
{
    /// <summary>DontCare</summary>
    [JsonPropertyName("DontCare")] DontCare,

    /// <summary>Max</summary>
    [JsonPropertyName("Max")] Max,

    /// <summary>Indexed</summary>
    [JsonPropertyName("Indexed")] Indexed,

    /// <summary>Gdi</summary>
    [JsonPropertyName("Gdi")] Gdi,

    /// <summary>Format16bppRgb555</summary>
    [JsonPropertyName("Format16bppRgb555")] Format16bppRgb555,

    /// <summary>Format16bppRgb565</summary>
    [JsonPropertyName("Format16bppRgb565")] Format16bppRgb565,

    /// <summary>Format24bppRgb</summary>
    [JsonPropertyName("Format24bppRgb")] Format24bppRgb,

    /// <summary>Format32bppRgb</summary>
    [JsonPropertyName("Format32bppRgb")] Format32bppRgb,

    /// <summary>Format1bppIndexed</summary>
    [JsonPropertyName("Format1bppIndexed")] Format1bppIndexed,

    /// <summary>Format4bppIndexed</summary>
    [JsonPropertyName("Format4bppIndexed")] Format4bppIndexed,

    /// <summary>Format8bppIndexed</summary>
    [JsonPropertyName("Format8bppIndexed")] Format8bppIndexed,

    /// <summary>Alpha</summary>
    [JsonPropertyName("Alpha")] Alpha,

    /// <summary>Format16bppArgb1555</summary>
    [JsonPropertyName("Format16bppArgb1555")] Format16bppArgb1555,

    /// <summary>PAlpha</summary>
    [JsonPropertyName("PAlpha")] PAlpha,

    /// <summary>Format32bppPArgb</summary>
    [JsonPropertyName("Format32bppPArgb")] Format32bppPArgb,

    /// <summary>Extended</summary>
    [JsonPropertyName("Extended")] Extended,

    /// <summary>Format16bppGrayScale</summary>
    [JsonPropertyName("Format16bppGrayScale")] Format16bppGrayScale,

    /// <summary>Format48bppRgb</summary>
    [JsonPropertyName("Format48bppRgb")] Format48bppRgb,

    /// <summary>Format64bppPArgb</summary>
    [JsonPropertyName("Format64bppPArgb")] Format64bppPArgb,

    /// <summary>Canonical</summary>
    [JsonPropertyName("Canonical")] Canonical,

    /// <summary>Format32bppArgb</summary>
    [JsonPropertyName("Format32bppArgb")] Format32bppArgb,

    /// <summary>Format64bppArgb</summary>
    [JsonPropertyName("Format64bppArgb")] Format64bppArgb,
}

/// <summary>
/// 将枚举按字符串序列化，并使用成员上的 <see cref="JsonPropertyNameAttribute"/> 作为线格式值。
/// （.NET 8 的 <c>JsonStringEnumConverter</c> 不读取枚举成员上的 <c>[JsonPropertyName]</c>，故提供此转换器。）
/// </summary>
public sealed class JsonStringEnumMemberConverter : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

    /// <inheritdoc />
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(EnumMemberConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

internal sealed class EnumMemberConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    private static readonly Dictionary<string, T> ByName = new(StringComparer.Ordinal);
    private static readonly Dictionary<T, string> ByValue = new();

    static EnumMemberConverter()
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var value = (T)field.GetValue(null)!;
            var name = field.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? field.Name;
            ByName[name] = value;
            ByValue[value] = name;
        }
    }

    /// <inheritdoc />
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var text = reader.GetString();
            if (text is not null && ByName.TryGetValue(text, out var value))
                return value;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var number = reader.GetInt32();
            return (T)Enum.ToObject(typeof(T), number);
        }

        throw new JsonException($"无法将 JSON 值转换为 {typeof(T).Name}。");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (ByValue.TryGetValue(value, out var name))
            writer.WriteStringValue(name);
        else
            writer.WriteStringValue(value.ToString());
    }
}
