package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 发送文件。who 为空时发送给当前焦点聊天窗口。
 */
public class SendFileRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("files")
    private List<String> files;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public List<String> getFiles() { return files; }
    public void setFiles(List<String> files) { this.files = files; }
}
