package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 仅携带 who（可为空，作用于当前焦点窗口）的请求。
 */
public class WhoRequest {

    @JsonProperty("who")
    private String who;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }
}
