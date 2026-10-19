package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
 */
public class SendVoiceChatsRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("partner")
    private List<String> partner;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public List<String> getPartner() { return partner; }
    public void setPartner(List<String> partner) { this.partner = partner; }
}
