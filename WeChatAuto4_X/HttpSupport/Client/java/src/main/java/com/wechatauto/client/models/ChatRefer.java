package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 消息引用
 */
public class ChatRefer {

    /** 日期,如果不设置则不进行日期筛选 */
    @JsonProperty("date")
    private String date;

    @JsonProperty("message")
    private ChatSimpleMessage message;

    /** 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口. */
    @JsonProperty("is_close_search_win")
    private Boolean isCloseSearchWin;

    public String getDate() { return date; }
    public void setDate(String date) { this.date = date; }

    public ChatSimpleMessage getMessage() { return message; }
    public void setMessage(ChatSimpleMessage message) { this.message = message; }

    public Boolean getIsCloseSearchWin() { return isCloseSearchWin; }
    public void setIsCloseSearchWin(Boolean isCloseSearchWin) { this.isCloseSearchWin = isCloseSearchWin; }
}
