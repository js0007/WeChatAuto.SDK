# wechatauto-client

Auto-generated Node.js (ES modules) HTTP client SDK for the **WECHATAUTO.SDK** WeChat automation API.

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：`HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
> (This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.)

## Requirements

- Node.js 18+ (uses the global `fetch`, no runtime dependencies)

## Install / usage

```js
import { WeChatAutoClient } from 'wechatauto-client';

const client = new WeChatAutoClient('http://localhost:5000', 'your-api-key');

const clients = await client.getClients();
await client.sendMessage('clientId', {
  who: '张三',
  message: 'hello',
  at_users: ['李四'],
});
```

The `clientId` can be any value from `GET /clients` (e.g. an index, `wxid`, or name) — it is sent as-is and URL-encoded.

### Constructor

```js
new WeChatAutoClient(baseUrl = 'http://localhost:5000', apiKey = '', fetchImpl = fetch)
```

- `baseUrl` — server base URL. The client appends `/api/v1` to every request.
- `apiKey` — value of the `X-API-Key` header (from `App.json -> Http.ApiKey`). When empty (the default) the header is omitted entirely.
- `fetchImpl` — injectable fetch implementation (useful for tests or a custom HTTP layer).

### Methods

One method per operation, 65 in total, grouped by tag (Clients, Conversations, Friends, Groups, Messages, Navigation, Window). Path parameters are positional; optional query parameters are optional arguments with spec defaults:

```js
await client.getFriends('clientId');              // fromCache defaults to true
await client.getFriends('clientId', false);
await client.leaveGroup('clientId', 'groupName'); // clearHistory defaults to true
await client.getMessagesHistory('clientId', who, date, startDate, endDate);
```

Request bodies are plain objects; pass them through as-is with the exact `snake_case` field names (`interval_time`, `is_close_win`, `say_hi`, `wx_id`, `nick_name`, `group_name`, `at_users`, `message`, `who`, ...). See `src/typedefs.js` for the full JSDoc typedefs and enum constants.

### Return values

Every method returns `Promise` of the parsed JSON response body (`res.json()`) — the OpenAPI spec declares no response schemas, so no response types are invented. On a non-2xx response the promise rejects with an `Error` whose message contains the HTTP status and the response body text.

### Enums

Exported constants: `ChatType`, `FriendSearchResultEnums`, `NavigationType`, `PixelFormat`.

```js
import { NavigationType } from 'wechatauto-client';
await client.switchNavigation('clientId', { navigation_type: NavigationType.Chat });
```

## Tests

```sh
npm test   # runs node --test
```

No dependencies — tests use `node:test` + `node:assert` with an injected fake `fetch`.
