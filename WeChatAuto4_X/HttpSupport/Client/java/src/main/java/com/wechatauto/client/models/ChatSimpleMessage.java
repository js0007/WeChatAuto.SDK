package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * ChatSimpleMessage
 */
public class ChatSimpleMessage {

    /** 微信名称 */
    @JsonProperty("who")
    private String who;

    /** 消息 */
    @JsonProperty("message")
    private String message;

    /** 消息日期 */
    @JsonProperty("send_date_time")
    private String sendDateTime;

    /** 消息日期,日期时间格式 */
    @JsonProperty("date_time")
    private String dateTime;

    /** 唯一字符串 */
    @JsonProperty("unique_string")
    private String uniqueString;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getMessage() { return message; }
    public void setMessage(String message) { this.message = message; }

    public String getSendDateTime() { return sendDateTime; }
    public void setSendDateTime(String sendDateTime) { this.sendDateTime = sendDateTime; }

    public String getDateTime() { return dateTime; }
    public void setDateTime(String dateTime) { this.dateTime = dateTime; }

    public String getUniqueString() { return uniqueString; }
    public void setUniqueString(String uniqueString) { this.uniqueString = uniqueString; }
}
