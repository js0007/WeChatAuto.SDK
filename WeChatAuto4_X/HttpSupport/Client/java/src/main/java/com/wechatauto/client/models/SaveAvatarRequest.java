package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 保存头像到指定路径。
 */
public class SaveAvatarRequest {

    @JsonProperty("path")
    private String path;

    public String getPath() { return path; }
    public void setPath(String path) { this.path = path; }
}
