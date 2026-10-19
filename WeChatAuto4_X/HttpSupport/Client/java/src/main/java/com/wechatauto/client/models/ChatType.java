package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;

/**
 * 聊天类型
 */
public enum ChatType {

    FRIEND("好友"),
    WECHAT_WORK("企业微信"),
    GROUP_CHAT("群聊"),
    OFFICIAL_ACCOUNT("公众号"),
    SUBSCRIPTION_ACCOUNT("订阅号"),
    SERVICE_NOTIFICATION("服务通知"),
    TENCENT_NEWS("腾讯新闻"),
    WECHAT_TEAM("微信团队"),
    FILE_TRANSFER_HELPER("文件传输助手"),
    OTHER("其他");

    private final String value;

    ChatType(String value) {
        this.value = value;
    }

    @JsonValue
    public String getValue() {
        return value;
    }

    @JsonCreator
    public static ChatType fromValue(String value) {
        if (value == null) {
            return null;
        }
        for (ChatType t : values()) {
            if (t.value.equals(value)) {
                return t;
            }
        }
        throw new IllegalArgumentException("Unknown ChatType value: " + value);
    }
}
