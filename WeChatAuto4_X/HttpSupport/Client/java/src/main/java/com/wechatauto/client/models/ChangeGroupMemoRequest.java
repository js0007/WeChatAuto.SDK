package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 修改群备注。
 */
public class ChangeGroupMemoRequest {

    @JsonProperty("group_name")
    private String groupName;

    @JsonProperty("new_memo")
    private String newMemo;

    public String getGroupName() { return groupName; }
    public void setGroupName(String groupName) { this.groupName = groupName; }

    public String getNewMemo() { return newMemo; }
    public void setNewMemo(String newMemo) { this.newMemo = newMemo; }
}
