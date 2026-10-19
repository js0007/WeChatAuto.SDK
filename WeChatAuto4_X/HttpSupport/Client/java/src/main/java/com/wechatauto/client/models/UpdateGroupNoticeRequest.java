package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 更新群公告。
 */
public class UpdateGroupNoticeRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("group_notice")
    private String groupNotice;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public String getGroupNotice() { return groupNotice; }
    public void setGroupNotice(String groupNotice) { this.groupNotice = groupNotice; }
}
