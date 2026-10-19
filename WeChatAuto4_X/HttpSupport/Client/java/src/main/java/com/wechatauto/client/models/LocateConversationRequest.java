package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 定位会话。
 */
public class LocateConversationRequest {

    @JsonProperty("title")
    private String title;

    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
}
