package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 切换/关闭导航栏窗口。
 */
public class SwitchNavigationRequest {

    @JsonProperty("navigation_type")
    private NavigationType navigationType;

    public NavigationType getNavigationType() { return navigationType; }
    public void setNavigationType(NavigationType navigationType) { this.navigationType = navigationType; }
}
