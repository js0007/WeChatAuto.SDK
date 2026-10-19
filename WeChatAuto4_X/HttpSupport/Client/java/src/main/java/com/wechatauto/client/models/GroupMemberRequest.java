package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
 */
public class GroupMemberRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("members")
    private List<String> members;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public List<String> getMembers() { return members; }
    public void setMembers(List<String> members) { this.members = members; }
}
