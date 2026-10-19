use wechatauto_client::models::{SetDoNotDisturbRequest, SendEmojiRequest};
use wechatauto_client::WeChatAutoClient;

#[test]
fn default_client_uses_localhost_5000() {
    let client = WeChatAutoClient::default();
    assert_eq!(client.base_url(), "http://localhost:5000");
    assert_eq!(client.api_key(), None);
}

#[test]
fn builder_configures_base_url_and_api_key() {
    let client = WeChatAutoClient::builder()
        .base_url("http://localhost:9000")
        .api_key("abc123")
        .build();
    assert_eq!(client.base_url(), "http://localhost:9000");
    assert_eq!(client.api_key(), Some("abc123"));
}

#[test]
fn request_models_serialize_snake_case() {
    let req = SetDoNotDisturbRequest {
        who: Some("张三".to_string()),
        setting: true,
    };
    let value = serde_json::to_value(req).expect("serialize");
    assert_eq!(value["who"], "张三");
    assert_eq!(value["setting"], true);

    let emoji = SendEmojiRequest {
        who: None,
        emoji: None,
        index: Some(3),
        at_users: None,
    };
    let value = serde_json::to_value(emoji).expect("serialize");
    assert_eq!(value["index"], 3);
    assert!(value.get("who").is_none());
    assert!(value.get("at_users").is_none());
}
