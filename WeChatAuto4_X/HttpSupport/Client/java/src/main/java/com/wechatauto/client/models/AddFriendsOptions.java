package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 增加朋友选项
 */
public class AddFriendsOptions {

    /** 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点 */
    @JsonProperty("interval_time")
    private Integer intervalTime;

    /** 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭 */
    @JsonProperty("is_close_win")
    private Boolean isCloseWin;

    /** 加好友时打招呼内容,如果为空，则保持微信默认 */
    @JsonProperty("say_hi")
    private String sayHi;

    /** 备注后缀，如：设置suffix为"test",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类 */
    @JsonProperty("suffix")
    private String suffix;

    /** 设置标签 */
    @JsonProperty("label")
    private String label;

    public Integer getIntervalTime() { return intervalTime; }
    public void setIntervalTime(Integer intervalTime) { this.intervalTime = intervalTime; }

    public Boolean getIsCloseWin() { return isCloseWin; }
    public void setIsCloseWin(Boolean isCloseWin) { this.isCloseWin = isCloseWin; }

    public String getSayHi() { return sayHi; }
    public void setSayHi(String sayHi) { this.sayHi = sayHi; }

    public String getSuffix() { return suffix; }
    public void setSuffix(String suffix) { this.suffix = suffix; }

    public String getLabel() { return label; }
    public void setLabel(String label) { this.label = label; }
}
