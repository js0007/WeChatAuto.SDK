# WECHATAUTO.SDK Java Client

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）

Java 11+ HTTP client for the WECHATAUTO.SDK WeChat automation API.

## Features

- One method per API operation (65 operations).
- Returns the raw response body as a `String` (responses have no schema in the OpenAPI spec).
- Optional `X-API-Key` header (only sent when a non-empty key is configured).
- `snake_case` JSON wire names preserved via Jackson `@JsonProperty`.
- Enums (`ChatType`, `FriendSearchResultEnums`, `NavigationType`, `PixelFormat`) serialize to their exact string values via `@JsonValue`.
- Throws `WeChatAutoException` on non-2xx responses (includes HTTP status and body).

## Installation / Build

Requires JDK 11+ and Maven.

```
mvn clean test
mvn clean install
```

## Quick start

```java
import com.wechatauto.client.WeChatAutoClient;
import com.wechatauto.client.models.SendMessageRequest;

// Default base URL http://localhost:5000, no API key.
WeChatAutoClient client = new WeChatAutoClient();

// With a custom base URL and API key.
WeChatAutoClient client = new WeChatAutoClient("http://localhost:5000", "your-api-key");

// Send a text message and read the raw JSON response.
SendMessageRequest req = new SendMessageRequest();
req.setWho("张三");
req.setMessage("hello");
String rawJson = client.sendMessage("client-1", req);
```

## Default base URL

The default base URL is `http://localhost:5000`. The actual port comes from `App.json` -> `Http.Port`; the API key comes from `App.json` -> `Http.ApiKey` (empty means the server does not validate, in which case the `X-API-Key` header is not sent).

## Project layout

- `com.wechatauto.client.WeChatAutoClient` — the client (all 65 operations).
- `com.wechatauto.client.WeChatAutoException` — thrown on non-2xx responses and transport errors.
- `com.wechatauto.client.models.*` — 42 POJO/enum classes, one per `components.schemas` entry.
