package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * PropertyItem
 */
public class PropertyItem {

    @JsonProperty("id")
    private Integer id;

    @JsonProperty("len")
    private Integer len;

    @JsonProperty("type")
    private Integer type;

    /** Value as a base64-encoded string (spec format: byte). */
    @JsonProperty("value")
    private String value;

    public Integer getId() { return id; }
    public void setId(Integer id) { this.id = id; }

    public Integer getLen() { return len; }
    public void setLen(Integer len) { this.len = len; }

    public Integer getType() { return type; }
    public void setType(Integer type) { this.type = type; }

    public String getValue() { return value; }
    public void setValue(String value) { this.value = value; }
}
