# WECHATAUTO.SDK Go Client

> ⚠️ 本客户端代码由 `swagger.json` 自动生成，未经充分测试。接口的准确行为请以官方文档为准：`HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
> (This client is auto-generated from `swagger.json` and has NOT been thoroughly tested. For authoritative API details, refer to `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.)

A minimal, dependency-free Go HTTP client for the WECHATAUTO.SDK WeChat automation API.
Only the Go standard library is used (`net/http`, `encoding/json`, `net/url`).

## Usage

```go
package main

import (
	"fmt"
	"log"

	wechatauto "wechatauto/client"
)

func main() {
	// baseURL defaults to http://localhost:5000 (App.json -> Http.Port).
	// apiKey is App.json -> Http.ApiKey; leave empty when the server does not validate.
	c := wechatauto.NewClient("http://localhost:5000", "your-api-key", nil)

	body, err := c.ListClients()
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(string(body)) // raw JSON body
}
```

Every method returns `(json.RawMessage, error)`. Response schemas are not declared in the
OpenAPI document (only `200 OK`), so the raw body is returned for you to decode as needed.

## Key behavior

- All endpoints are prefixed with `/api/v1`; you only pass `clientId` and other path params.
- `X-API-Key` is sent only when the configured `APIKey` is non-empty.
- `Content-Type: application/json` is set automatically for request bodies.
- Path segments and query values are URL-encoded (`url.PathEscape` / `url.Values`).
- Non-2xx responses return an error that includes the status code and response body.
- Optional query parameters use spec defaults (e.g. `fromCache=true`, `clearHistory=true`).

## Models

`models.go` contains a Go struct for every schema under `components.schemas`, with exact
`snake_case` JSON tags. Enums (`ChatType`, `FriendSearchResultEnums`, `NavigationType`,
`PixelFormat`) are typed string constants. Nullable/optional fields use `omitempty`
(pointer types for nullable scalars, pointer types for nested objects).

## Running tests

```sh
go test ./...
```
