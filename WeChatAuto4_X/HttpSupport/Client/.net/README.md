# WeChatAuto.Client (.NET)

WECHATAUTO.SDK HTTP API 的 .NET 客户端 SDK。

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：
> `HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
>
> （This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details,
> refer to `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.）

## 项目结构

- `WeChatAuto.Client/` —— 类库（net8.0），仅依赖 `System.Net.Http` 与 `System.Text.Json`。
  - `WeChatAutoClient.cs` —— 核心客户端（基地址、API Key、发送逻辑）。
  - `WeChatAutoClient.*.cs` —— 按 tag 分组的操作方法（Clients / Conversations / Friends / Groups / Messages / Navigation / Window）。
  - `Models/Enums.cs`、`Models/Models.cs` —— 请求模型与枚举（全部 `components.schemas`，共 42 个）。
- `WeChatAuto.Client.Tests/` —— xUnit 单元测试（使用假 HttpMessageHandler，不需要真实服务器）。
- `WeChatAuto.sln` —— 解决方案（可选）。

## 快速开始

```csharp
using WeChatAuto.Client;
using WeChatAuto.Client.Models;

var client = new WeChatAutoClient(
    baseUrl: "http://localhost:5000", // 默认值，端口来自 App.json 的 Http.Port
    apiKey: "your-api-key");          // 对应 App.json 的 Http.ApiKey；为空则不发送 X-API-Key 头

string? result = await client.GetClientsAsync();

await client.SendMessageAsync("wxid_xxx", new SendMessageRequest
{
    Who = "some_friend",
    Message = "hello",
});
```

## 说明

- 默认基地址：`http://localhost:5000`（端口来自 `App.json -> Http.Port`）。
- 可选鉴权：`X-API-Key` 头（来自 `App.json -> Http.ApiKey`；为空时服务端不校验，客户端也不发送）。
- 所有端点均以 `/api/v1` 开头。
- swagger 中响应没有 schema（仅 “200 OK”），因此所有方法返回**原始响应体**（`Task<string?>`），不构造任何响应 DTO。
- 请求体字段名严格保持 snake_case（`interval_time`、`is_close_win`、`say_hi`、`wx_id`、`nick_name`、`group_name`、`at_users`、`message`、`who` 等）。
- 枚举按字符串序列化（`JsonStringEnumConverter`）；`null` 属性在序列化时被省略。

## 构建与测试

```bash
dotnet build
dotnet test
```
