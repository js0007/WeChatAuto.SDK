# API 文档站点（静态）

本目录是一份**纯静态**的 API 文档站点，可直接拷贝到任意静态服务器 / Nginx / GitHub Pages / OSS 上对外发布，**无需部署真实的微信自动化服务**。

## 目录结构

```
api-site/
├── index.html      # Swagger UI 页面（CDN 加载 swagger-ui，指向同目录 swagger.json）
├── swagger.json    # 离线生成的 OpenAPI 3.0 文档（含全部接口 + 中文注释 + ApiKey 安全方案）
└── README.md       # 本说明
```

## 如何发布

1. 把整个 `api-site/` 目录拷贝到服务器（如 `/var/www/wechatapi/`、Nginx 的 `root`、OSS bucket）。
2. 用浏览器打开 `index.html` 即可浏览全部接口。

> 提示：`index.html` 通过 `unpkg.com` 加载 Swagger UI 前端资源，**联网的访客**才能正常渲染。若服务器/内网无外网，请把 swagger-ui 的资源也下载到本目录并改为相对引用。

## 如何重新生成 swagger.json

`swagger.json` 是从 `Server` 程序集离线生成的，改动接口后需重新生成：

- 方式一：直接运行真实服务（`Server.exe`），访问 `http://localhost:5000/swagger/v1/swagger.json` 导出。
- 方式二：使用离线生成器（无需启动微信/服务），产物写入本目录。

## 与真实服务的差异

- 本页面**只读文档**，`Try it out` 调用的接口指向页面同源的 `/api/...`，静态服务器上**不会**有真实后端。
- 若要在线调试，需在部署了 `Server.exe` 的机器上访问其自带的 Swagger UI：`http://<服务器>:<Port>/swagger`。
- 鉴权：全局默认使用请求头 `X-API-Key`，值取自 `App.json` 的 `Http.ApiKey`（为空则不校验）。
