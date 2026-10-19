package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * ImageFormat
 */
public class ImageFormat {

    @JsonProperty("guid")
    private String guid;

    public String getGuid() { return guid; }
    public void setGuid(String guid) { this.guid = guid; }
}
