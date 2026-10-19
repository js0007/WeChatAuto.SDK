package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 从群聊中添加好友。
 */
public class AddGroupMemberToFriendsRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("members")
    private List<String> members;

    @JsonProperty("options")
    private AddFriendsOptions options;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public List<String> getMembers() { return members; }
    public void setMembers(List<String> members) { this.members = members; }

    public AddFriendsOptions getOptions() { return options; }
    public void setOptions(AddFriendsOptions options) { this.options = options; }
}
