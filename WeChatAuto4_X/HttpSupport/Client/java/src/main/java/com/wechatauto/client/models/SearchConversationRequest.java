package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 搜索会话。
 */
public class SearchConversationRequest {

    @JsonProperty("who")
    private String who;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }
}
