package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 拍一拍。
 */
public class TapRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("prev_scroll_number")
    private Integer prevScrollNumber;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public Integer getPrevScrollNumber() { return prevScrollNumber; }
    public void setPrevScrollNumber(Integer prevScrollNumber) { this.prevScrollNumber = prevScrollNumber; }
}
