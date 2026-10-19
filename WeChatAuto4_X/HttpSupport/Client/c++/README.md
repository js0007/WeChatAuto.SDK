# WECHATAUTO.SDK — C++ Client

A C++17, header-only HTTP client for the **WECHATAUTO.SDK** WeChat automation API.

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：
> `HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
> (This client is auto-generated from swagger.json and has NOT been thoroughly tested.
> For authoritative API details, refer to `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.)

## Layout

```
c++/
├── include/wechatauto/wechatauto_client.hpp   # the entire library (header-only)
├── tests/test_client.cpp                       # unit tests (no live server, no test framework)
├── CMakeLists.txt
└── README.md
```

## Dependencies

- **C++17** compiler.
- **nlohmann/json** — single header (`nlohmann/json.hpp`). Required.
  - Download `json.hpp` from <https://github.com/nlohmann/json/releases> and place it on your
    include path, or install the `nlohmann_json` CMake package.
- **libcurl** — *optional*. Only needed to use the built-in `CurlTransport`.
  Define `WECHATAUTO_HAS_CURL` and link libcurl to enable it. Without it the header still
  compiles; you must supply your own `HttpTransport`.

## Quick start

```cpp
#include <wechatauto/wechatauto_client.hpp>

int main() {
    wechatauto::WeChatAutoClient client(
        "http://localhost:5000",   // base URL (from App.json -> Http.Port)
        "your-api-key"             // X-API-Key (from App.json -> Http.ApiKey; empty => no auth)
    );

    // Raw response body as std::string (responses have no schema; parse it yourself).
    std::string clients = client.getClients();

    wechatauto::SendMessageRequest req;
    req.who = "alice";
    req.message = "hello";
    std::string result = client.sendMessage("your-client-id", req);
}
```

## Custom transport

The client is transport-agnostic. Subclass `HttpTransport` to use any HTTP stack:

```cpp
class MyTransport : public wechatauto::HttpTransport {
public:
    wechatauto::HttpResponse request(const std::string& method, const std::string& url,
                                     const std::map<std::string, std::string>& headers,
                                     const std::string& body) override {
        // ... issue the HTTP request and return {status_code, body}
    }
};

wechatauto::WeChatAutoClient client(
    "http://localhost:5000", "", std::make_unique<MyTransport>());
```

When `WECHATAUTO_HAS_CURL` is defined, a `CurlTransport` is used automatically if no
transport is supplied.

## Building the tests

```sh
cmake -S . -B build -DWECHATAUTO_BUILD_TESTS=ON
cmake --build build
ctest --test-dir build
```

Or compile the single test translation unit directly (no libcurl, no test framework):

```sh
c++ -std=c++17 -I include -I path/to/nlohmann tests/test_client.cpp -o test_client
./test_client
```

## Notes / assumptions

- Every endpoint is prefixed with `/api/v1`.
- All responses are returned as the raw body `std::string` — no response structs are generated.
- Wire property names are `snake_case` and preserved exactly (`interval_time`, `is_close_win`,
  `say_hi`, `wx_id`, `nick_name`, `group_name`, `at_users`, `message`, `who`, ...).
- Nullable fields are `std::optional<T>` and are omitted from the JSON body when unset.
  Nested object fields (e.g. `options`, `refer`, `message`) are also `std::optional` and omitted
  when unset; non-nullable scalar fields are always serialized.
- Path and query values are URL-encoded.
- The header contains Chinese string literals (enum wire values, disclaimer). When compiling with
  MSVC, pass `/utf-8` so UTF-8 source is decoded correctly (GCC/Clang treat source as UTF-8 by default).
