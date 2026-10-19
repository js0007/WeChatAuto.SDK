# wechatauto (Python)

Python HTTP client for the **WECHATAUTO.SDK** WeChat automation API, auto-generated
from `swagger.json`.

> ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：
> `HttpSupport/Server/Docs/api-site/swagger.json` 与 `API接入指南.md`。
> (This client is auto-generated from swagger.json and has NOT been thoroughly
> tested. For authoritative API details, refer to
> `HttpSupport/Server/Docs/api-site/swagger.json` and `API接入指南.md`.)

## Install

```bash
pip install -e .          # runtime dependency: requests
pip install -e .[dev]     # adds pytest
```

## Usage

```python
from wechatauto import WeChatAutoClient
from wechatauto import models

client = WeChatAutoClient(
    base_url="http://localhost:5000",  # port from App.json -> Http.Port
    api_key="",                        # App.json -> Http.ApiKey; empty -> header omitted
)

# Simple GET
clients = client.get_clients()

# Path params are positional
info = client.get_client("wxid_xxx")

# POST with a dataclass body (or a plain dict)
client.send_message("wxid_xxx", models.SendMessageRequest(who="friend", message="hello"))
client.send_message("wxid_xxx", {"who": "friend", "message": "hello"})
```

- Every endpoint is under `/api/v1` (handled automatically).
- Request bodies accept either a dataclass (serialized via `to_dict()`, which
  omits `None` values) or a plain `dict` (passed through unchanged).
- Methods return parsed JSON (`dict` / `list` / `str`). Responses have no schema,
  so no response types are constructed.
- On a non-2xx status, a `WeChatAutoError` is raised (with `.status_code` and
  `.body_text`).

## Layout

```
python/
  wechatauto/
    __init__.py   # exports WeChatAutoClient, WeChatAutoError, models
    client.py     # WeChatAutoClient
    models.py     # dataclasses + enums for every schema in components.schemas
  tests/test_client.py
  pyproject.toml
  README.md
```

## Run tests

```bash
python -m pytest tests/ -q
```

Tests use `unittest.mock`-style fake sessions (no live server).
