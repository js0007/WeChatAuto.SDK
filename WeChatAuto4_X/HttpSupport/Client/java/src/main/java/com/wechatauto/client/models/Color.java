package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Color
 */
public class Color {

    @JsonProperty("r")
    private Integer r;

    @JsonProperty("g")
    private Integer g;

    @JsonProperty("b")
    private Integer b;

    @JsonProperty("a")
    private Integer a;

    @JsonProperty("is_known_color")
    private Boolean isKnownColor;

    @JsonProperty("is_empty")
    private Boolean isEmpty;

    @JsonProperty("is_named_color")
    private Boolean isNamedColor;

    @JsonProperty("is_system_color")
    private Boolean isSystemColor;

    @JsonProperty("name")
    private String name;

    public Integer getR() { return r; }
    public void setR(Integer r) { this.r = r; }

    public Integer getG() { return g; }
    public void setG(Integer g) { this.g = g; }

    public Integer getB() { return b; }
    public void setB(Integer b) { this.b = b; }

    public Integer getA() { return a; }
    public void setA(Integer a) { this.a = a; }

    public Boolean getIsKnownColor() { return isKnownColor; }
    public void setIsKnownColor(Boolean isKnownColor) { this.isKnownColor = isKnownColor; }

    public Boolean getIsEmpty() { return isEmpty; }
    public void setIsEmpty(Boolean isEmpty) { this.isEmpty = isEmpty; }

    public Boolean getIsNamedColor() { return isNamedColor; }
    public void setIsNamedColor(Boolean isNamedColor) { this.isNamedColor = isNamedColor; }

    public Boolean getIsSystemColor() { return isSystemColor; }
    public void setIsSystemColor(Boolean isSystemColor) { this.isSystemColor = isSystemColor; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
}
