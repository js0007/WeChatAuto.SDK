// ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。
// （This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）

using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeChatAuto.Client.Models;

namespace WeChatAuto.Client;

/// <summary>
/// WECHATAUTO.SDK HTTP API 客户端。
/// <para>
/// ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。
/// （This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）
/// </para>
/// </summary>
public sealed partial class WeChatAutoClient
{
    /// <summary>默认服务地址。</summary>
    public const string DefaultBaseUrl = "http://localhost:5000";

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        // null 属性不输出，与服务器约定一致（例如 VoiceOptions 中“为 null 时不发送”）。
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
    };

    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    /// <summary>X-API-Key 鉴权头（为空时客户端不发送该头）。</summary>
    public string? ApiKey { get; }

    /// <summary>服务基地址（不含末尾斜杠）。</summary>
    public string BaseUrl => _baseUrl;

    /// <summary>
    /// 创建客户端，使用默认（或指定）基地址与可选的 API Key。
    /// </summary>
    /// <param name="baseUrl">服务基地址，默认 <see cref="DefaultBaseUrl"/>（http://localhost:5000，端口来自 App.json 的 Http.Port）。</param>
    /// <param name="apiKey">可选 API Key（对应 App.json 的 Http.ApiKey）。为空/空字符串时不发送 X-API-Key 头。</param>
    public WeChatAutoClient(string? baseUrl = null, string? apiKey = null)
    {
        _baseUrl = NormalizeBaseUrl(baseUrl);
        ApiKey = apiKey;
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// 创建客户端，并注入自定义 <see cref="HttpMessageHandler"/>（便于测试或自定义请求管道）。
    /// </summary>
    /// <param name="baseUrl">服务基地址；为空时使用默认值。</param>
    /// <param name="apiKey">可选 API Key。</param>
    /// <param name="handler">自定义消息处理器。</param>
    public WeChatAutoClient(string? baseUrl, string? apiKey, HttpMessageHandler handler)
    {
        if (handler is null)
            throw new ArgumentNullException(nameof(handler));

        _baseUrl = NormalizeBaseUrl(baseUrl);
        ApiKey = apiKey;
        _httpClient = new HttpClient(handler);
    }

    /// <summary>
    /// 使用现有的 <see cref="HttpClient"/> 创建客户端（客户端不接管其生命周期）。
    /// </summary>
    /// <param name="httpClient">现有 HttpClient。</param>
    /// <param name="baseUrl">服务基地址；为空时使用默认值。</param>
    /// <param name="apiKey">可选 API Key。</param>
    public WeChatAutoClient(HttpClient httpClient, string? baseUrl = null, string? apiKey = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = NormalizeBaseUrl(baseUrl);
        ApiKey = apiKey;
    }

    private static string NormalizeBaseUrl(string? baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            return DefaultBaseUrl;
        return baseUrl.TrimEnd('/');
    }

    private static string Escape(string segment) => Uri.EscapeDataString(segment);

    private async Task<string?> SendAsync(
        HttpMethod method,
        string path,
        string? query = null,
        object? body = null,
        CancellationToken cancellationToken = default)
    {
        var url = _baseUrl + path;
        if (!string.IsNullOrEmpty(query))
            url += "?" + query.TrimStart('?');

        using var request = new HttpRequestMessage(method, url);

        if (!string.IsNullOrEmpty(ApiKey))
            request.Headers.TryAddWithoutValidation("X-API-Key", ApiKey);

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, body.GetType(), SerializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        return string.IsNullOrEmpty(content) ? null : content;
    }

    private static string BuildQuery(params (string Key, string? Value)[] parameters)
    {
        var parts = new List<string>();
        foreach (var (key, value) in parameters)
        {
            if (value is null)
                continue;
            parts.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
        }
        return string.Join("&", parts);
    }
}
