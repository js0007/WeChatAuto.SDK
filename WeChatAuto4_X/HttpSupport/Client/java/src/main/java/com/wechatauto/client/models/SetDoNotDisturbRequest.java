package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 设置会话免打扰。who 为空时作用于当前窗口。
 */
public class SetDoNotDisturbRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("setting")
    private Boolean setting;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public Boolean getSetting() { return setting; }
    public void setSetting(Boolean setting) { this.setting = setting; }
}
