package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;

/**
 * Pixel format (GDI+ / System.Drawing PixelFormat values).
 */
public enum PixelFormat {

    DONT_CARE("DontCare"),
    MAX("Max"),
    INDEXED("Indexed"),
    GDI("Gdi"),
    FORMAT16_BPP_RGB555("Format16bppRgb555"),
    FORMAT16_BPP_RGB565("Format16bppRgb565"),
    FORMAT24_BPP_RGB("Format24bppRgb"),
    FORMAT32_BPP_RGB("Format32bppRgb"),
    FORMAT1_BPP_INDEXED("Format1bppIndexed"),
    FORMAT4_BPP_INDEXED("Format4bppIndexed"),
    FORMAT8_BPP_INDEXED("Format8bppIndexed"),
    ALPHA("Alpha"),
    FORMAT16_BPP_ARGB1555("Format16bppArgb1555"),
    P_ALPHA("PAlpha"),
    FORMAT32_BPP_P_ARGB("Format32bppPArgb"),
    EXTENDED("Extended"),
    FORMAT16_BPP_GRAY_SCALE("Format16bppGrayScale"),
    FORMAT48_BPP_RGB("Format48bppRgb"),
    FORMAT64_BPP_P_ARGB("Format64bppPArgb"),
    CANONICAL("Canonical"),
    FORMAT32_BPP_ARGB("Format32bppArgb"),
    FORMAT64_BPP_ARGB("Format64bppArgb");

    private final String value;

    PixelFormat(String value) {
        this.value = value;
    }

    @JsonValue
    public String getValue() {
        return value;
    }

    @JsonCreator
    public static PixelFormat fromValue(String value) {
        if (value == null) {
            return null;
        }
        for (PixelFormat t : values()) {
            if (t.value.equals(value)) {
                return t;
            }
        }
        throw new IllegalArgumentException("Unknown PixelFormat value: " + value);
    }
}
