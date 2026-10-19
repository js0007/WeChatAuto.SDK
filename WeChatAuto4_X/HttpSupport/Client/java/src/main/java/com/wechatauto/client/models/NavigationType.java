package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;

/**
 * 导航栏类型
 */
public enum NavigationType {

    CHAT("聊天"),
    CONTACTS("通讯录"),
    FAVORITES("收藏"),
    CHAT_FILES("聊天文件"),
    MOMENTS("朋友圈"),
    SEARCH("搜一搜"),
    CHANNELS("视频号"),
    TOP_STORIES("看一看"),
    MINI_PROGRAM_PANEL("小程序面板"),
    MOBILE("手机"),
    SETTINGS("设置及其他"),
    WECHAT("微信"),
    GAME_CENTER("游戏中心"),
    MORE("更多");

    private final String value;

    NavigationType(String value) {
        this.value = value;
    }

    @JsonValue
    public String getValue() {
        return value;
    }

    @JsonCreator
    public static NavigationType fromValue(String value) {
        if (value == null) {
            return null;
        }
        for (NavigationType t : values()) {
            if (t.value.equals(value)) {
                return t;
            }
        }
        throw new IllegalArgumentException("Unknown NavigationType value: " + value);
    }
}
