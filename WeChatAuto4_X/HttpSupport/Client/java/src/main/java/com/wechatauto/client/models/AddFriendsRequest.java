package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 通过手机号码或微信号添加好友。
 */
public class AddFriendsRequest {

    @JsonProperty("friends")
    private List<String> friends;

    @JsonProperty("options")
    private AddFriendsOptions options;

    public List<String> getFriends() { return friends; }
    public void setFriends(List<String> friends) { this.friends = friends; }

    public AddFriendsOptions getOptions() { return options; }
    public void setOptions(AddFriendsOptions options) { this.options = options; }
}
