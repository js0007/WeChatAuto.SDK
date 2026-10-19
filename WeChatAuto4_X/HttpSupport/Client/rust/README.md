# wechatauto-client

Auto-generated Rust client SDK for the **WECHATAUTO.SDK** WeChat automation HTTP API.

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：
> `HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
> (This client is auto-generated from swagger.json and has NOT been thoroughly tested.
> For authoritative API details, refer to `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.)

## Overview

- Default base URL: `http://localhost:5000` (the port comes from `App.json` -> `Http.Port`).
- Optional auth: `X-API-Key` header (from `App.json` -> `Http.ApiKey`). When the key is empty or
  unset, no header is sent and the server does not validate it.
- Every endpoint is under `/api/v1`.
- The OpenAPI document declares **no response schemas**, so every method returns
  `serde_json::Value` (the raw response body).
- Wire property names are `snake_case`; `Option<T>` fields are omitted from the JSON body when `None`.

## Usage

```rust
use wechatauto_client::models::{SendMessageRequest, ChatRefer, ChatSimpleMessage};
use wechatauto_client::WeChatAutoClient;

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    // No API key
    let client = WeChatAutoClient::new("http://localhost:5000");

    // Or with an API key / builder
    let client = WeChatAutoClient::builder()
        .base_url("http://localhost:5000")
        .api_key("your-api-key")
        .build();

    let clients: serde_json::Value = client.get_clients().await?;

    let body = SendMessageRequest {
        who: Some("张三".to_string()),
        message: Some("你好".to_string()),
        at_users: None,
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
    let result: serde_json::Value = client.send_message("clientId", &body).await?;

    Ok(())
}
```

All request/response models live in `wechatauto_client::models`. All request methods are `async`
and return `Result<serde_json::Value, wechatauto_client::Error>`.

## Building & testing

```sh
cargo build
cargo test
```

The unit tests are fully offline: they assert URL construction, query-string serialization
(including default values such as `fromCache=true` / `clearHistory=true`), path-segment
percent-encoding, `X-API-Key` header injection, and JSON body serialization without requiring a
live server.
