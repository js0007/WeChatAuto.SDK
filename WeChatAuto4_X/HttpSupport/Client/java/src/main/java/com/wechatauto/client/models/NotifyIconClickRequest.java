package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 点击任务栏微信图标。index 与 wechat_name 二选一。
 */
public class NotifyIconClickRequest {

    @JsonProperty("index")
    private Integer index;

    @JsonProperty("wechat_name")
    private String wechatName;

    public Integer getIndex() { return index; }
    public void setIndex(Integer index) { this.index = index; }

    public String getWechatName() { return wechatName; }
    public void setWechatName(String wechatName) { this.wechatName = wechatName; }
}
