package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 发送文本消息。who 为空时发送给当前焦点聊天窗口。
 */
public class SendMessageRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("message")
    private String message;

    /** 被 @ 的好友列表。 */
    @JsonProperty("at_users")
    private List<String> atUsers;

    @JsonProperty("refer")
    private ChatRefer refer;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getMessage() { return message; }
    public void setMessage(String message) { this.message = message; }

    public List<String> getAtUsers() { return atUsers; }
    public void setAtUsers(List<String> atUsers) { this.atUsers = atUsers; }

    public ChatRefer getRefer() { return refer; }
    public void setRefer(ChatRefer refer) { this.refer = refer; }
}
