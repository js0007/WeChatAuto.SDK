package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 创建群聊。
 */
public class CreateGroupRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("first_who")
    private String firstWho;

    @JsonProperty("members")
    private List<String> members;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public String getFirstWho() { return firstWho; }
    public void setFirstWho(String firstWho) { this.firstWho = firstWho; }

    public List<String> getMembers() { return members; }
    public void setMembers(List<String> members) { this.members = members; }
}
