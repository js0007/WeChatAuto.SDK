package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
 */
public class ReferenceRequest {

    @JsonProperty("message")
    private ChatSimpleMessage message;

    @JsonProperty("who")
    private String who;

    @JsonProperty("message_content")
    private String messageContent;

    @JsonProperty("prev_scroll_number")
    private Integer prevScrollNumber;

    public ChatSimpleMessage getMessage() { return message; }
    public void setMessage(ChatSimpleMessage message) { this.message = message; }

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getMessageContent() { return messageContent; }
    public void setMessageContent(String messageContent) { this.messageContent = messageContent; }

    public Integer getPrevScrollNumber() { return prevScrollNumber; }
    public void setPrevScrollNumber(Integer prevScrollNumber) { this.prevScrollNumber = prevScrollNumber; }
}
