package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Image
 */
public class Image {

    @JsonProperty("tag")
    private Object tag;

    @JsonProperty("physical_dimension")
    private SizeF physicalDimension;

    @JsonProperty("size")
    private Size size;

    @JsonProperty("width")
    private Integer width;

    @JsonProperty("height")
    private Integer height;

    @JsonProperty("horizontal_resolution")
    private Float horizontalResolution;

    @JsonProperty("vertical_resolution")
    private Float verticalResolution;

    @JsonProperty("flags")
    private Integer flags;

    @JsonProperty("raw_format")
    private ImageFormat rawFormat;

    @JsonProperty("pixel_format")
    private PixelFormat pixelFormat;

    @JsonProperty("property_id_list")
    private List<Integer> propertyIdList;

    @JsonProperty("property_items")
    private List<PropertyItem> propertyItems;

    @JsonProperty("palette")
    private ColorPalette palette;

    @JsonProperty("frame_dimensions_list")
    private List<String> frameDimensionsList;

    public Object getTag() { return tag; }
    public void setTag(Object tag) { this.tag = tag; }

    public SizeF getPhysicalDimension() { return physicalDimension; }
    public void setPhysicalDimension(SizeF physicalDimension) { this.physicalDimension = physicalDimension; }

    public Size getSize() { return size; }
    public void setSize(Size size) { this.size = size; }

    public Integer getWidth() { return width; }
    public void setWidth(Integer width) { this.width = width; }

    public Integer getHeight() { return height; }
    public void setHeight(Integer height) { this.height = height; }

    public Float getHorizontalResolution() { return horizontalResolution; }
    public void setHorizontalResolution(Float horizontalResolution) { this.horizontalResolution = horizontalResolution; }

    public Float getVerticalResolution() { return verticalResolution; }
    public void setVerticalResolution(Float verticalResolution) { this.verticalResolution = verticalResolution; }

    public Integer getFlags() { return flags; }
    public void setFlags(Integer flags) { this.flags = flags; }

    public ImageFormat getRawFormat() { return rawFormat; }
    public void setRawFormat(ImageFormat rawFormat) { this.rawFormat = rawFormat; }

    public PixelFormat getPixelFormat() { return pixelFormat; }
    public void setPixelFormat(PixelFormat pixelFormat) { this.pixelFormat = pixelFormat; }

    public List<Integer> getPropertyIdList() { return propertyIdList; }
    public void setPropertyIdList(List<Integer> propertyIdList) { this.propertyIdList = propertyIdList; }

    public List<PropertyItem> getPropertyItems() { return propertyItems; }
    public void setPropertyItems(List<PropertyItem> propertyItems) { this.propertyItems = propertyItems; }

    public ColorPalette getPalette() { return palette; }
    public void setPalette(ColorPalette palette) { this.palette = palette; }

    public List<String> getFrameDimensionsList() { return frameDimensionsList; }
    public void setFrameDimensionsList(List<String> frameDimensionsList) { this.frameDimensionsList = frameDimensionsList; }
}
