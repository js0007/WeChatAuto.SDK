package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 发送表情。emoji 与 index 二选一（优先使用 index）。
 */
public class SendEmojiRequest {

    @JsonProperty("who")
    private String who;

    /** 表情名称或描述。 */
    @JsonProperty("emoji")
    private String emoji;

    /** 表情索引。 */
    @JsonProperty("index")
    private Integer index;

    @JsonProperty("at_users")
    private List<String> atUsers;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getEmoji() { return emoji; }
    public void setEmoji(String emoji) { this.emoji = emoji; }

    public Integer getIndex() { return index; }
    public void setIndex(Integer index) { this.index = index; }

    public List<String> getAtUsers() { return atUsers; }
    public void setAtUsers(List<String> atUsers) { this.atUsers = atUsers; }
}
