package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Size
 */
public class Size {

    @JsonProperty("is_empty")
    private Boolean isEmpty;

    @JsonProperty("width")
    private Integer width;

    @JsonProperty("height")
    private Integer height;

    public Boolean getIsEmpty() { return isEmpty; }
    public void setIsEmpty(Boolean isEmpty) { this.isEmpty = isEmpty; }

    public Integer getWidth() { return width; }
    public void setWidth(Integer width) { this.width = width; }

    public Integer getHeight() { return height; }
    public void setHeight(Integer height) { this.height = height; }
}
