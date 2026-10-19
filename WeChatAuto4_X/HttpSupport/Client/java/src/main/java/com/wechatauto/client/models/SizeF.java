package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * SizeF
 */
public class SizeF {

    @JsonProperty("is_empty")
    private Boolean isEmpty;

    @JsonProperty("width")
    private Float width;

    @JsonProperty("height")
    private Float height;

    public Boolean getIsEmpty() { return isEmpty; }
    public void setIsEmpty(Boolean isEmpty) { this.isEmpty = isEmpty; }

    public Float getWidth() { return width; }
    public void setWidth(Float width) { this.width = width; }

    public Float getHeight() { return height; }
    public void setHeight(Float height) { this.height = height; }
}
