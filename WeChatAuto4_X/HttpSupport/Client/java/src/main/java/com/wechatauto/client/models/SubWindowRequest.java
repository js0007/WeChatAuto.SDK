package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 打开/关闭指定好友或群的子窗口。
 */
public class SubWindowRequest {

    @JsonProperty("who")
    private String who;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }
}
