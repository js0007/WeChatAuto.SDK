package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 邀请群成员（外部群）。
 */
public class InviteGroupMemberRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("members")
    private List<String> members;

    @JsonProperty("invite_reason")
    private String inviteReason;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public List<String> getMembers() { return members; }
    public void setMembers(List<String> members) { this.members = members; }

    public String getInviteReason() { return inviteReason; }
    public void setInviteReason(String inviteReason) { this.inviteReason = inviteReason; }
}
