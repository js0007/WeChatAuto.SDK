package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;

/**
 * 通过wxid查询，或者通过手机号查询结果枚举
 */
public enum FriendSearchResultEnums {

    FRIEND("Friend"),
    NO_FIND("No_Find"),
    NOT_FRIEND("NotFriend");

    private final String value;

    FriendSearchResultEnums(String value) {
        this.value = value;
    }

    @JsonValue
    public String getValue() {
        return value;
    }

    @JsonCreator
    public static FriendSearchResultEnums fromValue(String value) {
        if (value == null) {
            return null;
        }
        for (FriendSearchResultEnums t : values()) {
            if (t.value.equals(value)) {
                return t;
            }
        }
        throw new IllegalArgumentException("Unknown FriendSearchResultEnums value: " + value);
    }
}
