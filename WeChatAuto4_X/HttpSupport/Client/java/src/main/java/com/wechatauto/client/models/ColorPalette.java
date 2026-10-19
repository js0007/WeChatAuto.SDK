package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * ColorPalette
 */
public class ColorPalette {

    @JsonProperty("flags")
    private Integer flags;

    @JsonProperty("entries")
    private List<Color> entries;

    public Integer getFlags() { return flags; }
    public void setFlags(Integer flags) { this.flags = flags; }

    public List<Color> getEntries() { return entries; }
    public void setEntries(List<Color> entries) { this.entries = entries; }
}
