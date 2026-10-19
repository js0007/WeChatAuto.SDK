package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 发送语音消息。who 为空时发送给当前焦点聊天窗口。
 */
public class SendVoiceMessageRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("file_path")
    private String filePath;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getFilePath() { return filePath; }
    public void setFilePath(String filePath) { this.filePath = filePath; }
}
