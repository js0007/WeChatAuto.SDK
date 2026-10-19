package com.wechatauto.client.models;

import java.util.List;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 个人信息
 */
public class FriendInfo {

    /** 昵称 */
    @JsonProperty("nick_name")
    private String nickName;

    /** 备注名 */
    @JsonProperty("memo_name")
    private String memoName;

    /** 地区,建议仅供参考 */
    @JsonProperty("area")
    private String area;

    /** 标签 */
    @JsonProperty("lable")
    private List<String> lable;

    /** 共同群数量 */
    @JsonProperty("same_group_number")
    private String sameGroupNumber;

    /** 个性签名 */
    @JsonProperty("signature")
    private String signature;

    /** 来源 */
    @JsonProperty("source")
    private String source;

    /** 微信ID */
    @JsonProperty("wx_id")
    private String wxId;

    /** 头像路径 */
    @JsonProperty("avatar_path")
    private String avatarPath;

    @JsonProperty("avatar_image")
    private Image avatarImage;

    @JsonProperty("avatar_image_base64")
    private String avatarImageBase64;

    @JsonProperty("friend_search_result")
    private FriendSearchResultEnums friendSearchResult;

    @JsonProperty("chat_type")
    private ChatType chatType;

    /** 添加好友时间，>4.x版本才有此属性 */
    @JsonProperty("add_date_time")
    private String addDateTime;

    /** 微信中使用的名称本质是: 如果昵称不等于备注名，则以备注名不为主. */
    @JsonProperty("name")
    private String name;

    public String getNickName() { return nickName; }
    public void setNickName(String nickName) { this.nickName = nickName; }

    public String getMemoName() { return memoName; }
    public void setMemoName(String memoName) { this.memoName = memoName; }

    public String getArea() { return area; }
    public void setArea(String area) { this.area = area; }

    public List<String> getLable() { return lable; }
    public void setLable(List<String> lable) { this.lable = lable; }

    public String getSameGroupNumber() { return sameGroupNumber; }
    public void setSameGroupNumber(String sameGroupNumber) { this.sameGroupNumber = sameGroupNumber; }

    public String getSignature() { return signature; }
    public void setSignature(String signature) { this.signature = signature; }

    public String getSource() { return source; }
    public void setSource(String source) { this.source = source; }

    public String getWxId() { return wxId; }
    public void setWxId(String wxId) { this.wxId = wxId; }

    public String getAvatarPath() { return avatarPath; }
    public void setAvatarPath(String avatarPath) { this.avatarPath = avatarPath; }

    public Image getAvatarImage() { return avatarImage; }
    public void setAvatarImage(Image avatarImage) { this.avatarImage = avatarImage; }

    public String getAvatarImageBase64() { return avatarImageBase64; }
    public void setAvatarImageBase64(String avatarImageBase64) { this.avatarImageBase64 = avatarImageBase64; }

    public FriendSearchResultEnums getFriendSearchResult() { return friendSearchResult; }
    public void setFriendSearchResult(FriendSearchResultEnums friendSearchResult) { this.friendSearchResult = friendSearchResult; }

    public ChatType getChatType() { return chatType; }
    public void setChatType(ChatType chatType) { this.chatType = chatType; }

    public String getAddDateTime() { return addDateTime; }
    public void setAddDateTime(String addDateTime) { this.addDateTime = addDateTime; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
}
