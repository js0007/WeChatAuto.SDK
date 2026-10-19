package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 修改群名。
 */
public class RenameGroupRequest {

    @JsonProperty("old_group_name")
    private String oldGroupName;

    @JsonProperty("new_group_name")
    private String newGroupName;

    public String getOldGroupName() { return oldGroupName; }
    public void setOldGroupName(String oldGroupName) { this.oldGroupName = oldGroupName; }

    public String getNewGroupName() { return newGroupName; }
    public void setNewGroupName(String newGroupName) { this.newGroupName = newGroupName; }
}
