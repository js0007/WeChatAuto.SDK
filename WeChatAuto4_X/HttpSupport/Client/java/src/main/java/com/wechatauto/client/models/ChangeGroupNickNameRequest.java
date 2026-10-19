package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 修改自己在群中的昵称。
 */
public class ChangeGroupNickNameRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("nick_name")
    private String nickName;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public String getNickName() { return nickName; }
    public void setNickName(String nickName) { this.nickName = nickName; }
}
