//! ⚠️ 本客户端代码由 swagger.json 自动生成，未经充分测试。接口的准确行为请以官方文档为准：HttpSupport/Server/Docs/api-site/swagger.json 与 API接入指南.md。（This client is auto-generated from swagger.json and has NOT been thoroughly tested. For authoritative API details, refer to HttpSupport/Server/Docs/api-site/swagger.json and API接入指南.md.）

pub mod models;

mod client;

pub use client::{Error, WeChatAutoClient, WeChatAutoClientBuilder};
