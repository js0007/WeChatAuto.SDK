# HttpSupport 客户端 SDK（多语言）

本目录包含由服务端 `swagger.json` 自动生成的各语言客户端调用代码与单元测试。

> ⚠️ **重要声明**：以下所有客户端代码均由 `swagger.json` 自动生成，**未经充分测试**。
> 接口的准确行为请以官方文档为准：`HttpSupport/Server/Docs/api-site/swagger.json` 与 `HttpSupport/Server/Docs/API接入指南.md`。

## 公共约定

| 项目 | 值 |
| --- | --- |
| 默认 Base URL | `http://localhost:5000`（来自 `App.json` 的 `Http.Port`） |
| 鉴权 | 可选 `X-API-Key` 请求头（来自 `App.json` 的 `Http.ApiKey`，为空时服务端不校验） |
| 接口前缀 | 所有接口均在 `/api/v1/...` 下 |
| 接口数量 | 65 个（7 个分组：Clients / Conversations / Friends / Groups / Messages / Navigation / Window） |
| 数据模型 | 42 个 `components.schemas` |
| 响应类型 | swagger 中所有响应仅声明 `200 OK`、无 schema，因此各客户端统一返回原始 JSON，未臆造响应 DTO |

## 各语言目录

| 语言 | 目录 | 构建/测试 |
| --- | --- | --- |
| .NET (C#) | [`.net/`](.net/) | `dotnet build` / `dotnet test`（xUnit） |
| C++ | [`c++/`](c++/) | 头文件库 + CMake，测试用 `tests/test_client.cpp`（MSVC 需 `/utf-8`） |
| Go | [`go/`](go/) | `go test ./...` |
| Java | [`java/`](java/) | `mvn test`（Maven + JUnit 5） |
| Node.js | [`node/`](node/) | `npm test`（`node --test`） |
| Python | [`python/`](python/) | `python -m pytest tests/` |
| Rust | [`rust/`](rust/) | `cargo test` |

每个语言目录内均包含：
- **src**：客户端实现（统一入口类 `WeChatAutoClient` / 等价类型，65 个接口方法 + 42 个数据模型/枚举）；
- **test**：单元测试用例（通过 mock/fake 传输层验证 HTTP 方法、路径、查询参数、`X-API-Key` 头与 JSON 请求体，无需运行服务端）；
- **README.md**：该语言的使用说明与假设说明。

## 通用用法示例

各语言的客户端构造方式一致：传入 `baseUrl`（默认 `http://localhost:5000`）、`apiKey`（可选），以及可注入的底层 HTTP 客户端，便于在测试中替换为 mock。

以发送文本消息为例（各语言方法名一致，均为 `sendMessage(clientId, body)`）：

- C#：`var client = new WeChatAutoClient("http://localhost:5000", apiKey);`
- Python：`client = WeChatAutoClient(base_url="http://localhost:5000", api_key="...")`
- 其余语言同理，见各目录 README。

## 生成说明

这些代码由 `swagger.json`（`HttpSupport/Server/Docs/api-site/swagger.json`）驱动生成；字段名在传输层严格保持 swagger 中的 `snake_case`（含 `FriendInfo.lable` 等原样拼写），枚举值保留中文/英文原始字符串。
