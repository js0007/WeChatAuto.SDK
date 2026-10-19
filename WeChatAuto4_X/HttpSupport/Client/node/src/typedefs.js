/**
 * JSDoc type definitions and enum constants for the WECHATAUTO.SDK HTTP API.
 *
 * All request/response bodies are plain JavaScript objects. The wire property
 * names are snake_case and MUST be preserved exactly as documented here.
 *
 * Enums are exported both as JSDoc string unions (@typedef) and as frozen
 * const objects so you can use `ChatType.Friend` etc. in code.
 */

/* ------------------------------------------------------------------------- *
 * Enums
 * ------------------------------------------------------------------------- */

/** 聊天类型 (ChatType). @typedef {typeof ChatType[keyof typeof ChatType]} ChatType */
export const ChatType = Object.freeze({
  Friend: '好友',
  EnterpriseWeChat: '企业微信',
  Group: '群聊',
  OfficialAccount: '公众号',
  Subscription: '订阅号',
  ServiceNotification: '服务通知',
  TencentNews: '腾讯新闻',
  WeChatTeam: '微信团队',
  FileTransfer: '文件传输助手',
  Other: '其他',
});

/** 通过wxid查询，或者通过手机号查询结果枚举 (FriendSearchResultEnums).
 *  @typedef {typeof FriendSearchResultEnums[keyof typeof FriendSearchResultEnums]} FriendSearchResultEnums */
export const FriendSearchResultEnums = Object.freeze({
  Friend: 'Friend',
  NoFind: 'No_Find',
  NotFriend: 'NotFriend',
});

/** 导航栏类型 (NavigationType). @typedef {typeof NavigationType[keyof typeof NavigationType]} NavigationType */
export const NavigationType = Object.freeze({
  Chat: '聊天',
  Contacts: '通讯录',
  Favorites: '收藏',
  ChatFiles: '聊天文件',
  Moments: '朋友圈',
  Search: '搜一搜',
  Channels: '视频号',
  TopStories: '看一看',
  MiniProgramPanel: '小程序面板',
  Phone: '手机',
  SettingsAndMore: '设置及其他',
  WeChat: '微信',
  GameCenter: '游戏中心',
  More: '更多',
});

/** 像素格式 (PixelFormat). @typedef {typeof PixelFormat[keyof typeof PixelFormat]} PixelFormat */
export const PixelFormat = Object.freeze({
  DontCare: 'DontCare',
  Max: 'Max',
  Indexed: 'Indexed',
  Gdi: 'Gdi',
  Format16bppRgb555: 'Format16bppRgb555',
  Format16bppRgb565: 'Format16bppRgb565',
  Format24bppRgb: 'Format24bppRgb',
  Format32bppRgb: 'Format32bppRgb',
  Format1bppIndexed: 'Format1bppIndexed',
  Format4bppIndexed: 'Format4bppIndexed',
  Format8bppIndexed: 'Format8bppIndexed',
  Alpha: 'Alpha',
  Format16bppArgb1555: 'Format16bppArgb1555',
  PAlpha: 'PAlpha',
  Format32bppPArgb: 'Format32bppPArgb',
  Extended: 'Extended',
  Format16bppGrayScale: 'Format16bppGrayScale',
  Format48bppRgb: 'Format48bppRgb',
  Format64bppPArgb: 'Format64bppPArgb',
  Canonical: 'Canonical',
  Format32bppArgb: 'Format32bppArgb',
  Format64bppArgb: 'Format64bppArgb',
});

/* ------------------------------------------------------------------------- *
 * Request / response object schemas
 * ------------------------------------------------------------------------- */

/**
 * 增加朋友选项。
 * @typedef {object} AddFriendsOptions
 * @property {number} [interval_time] 间隔时间,以秒为单位，默认为三秒，如果担心风控，可以把此时间设置长一点
 * @property {boolean} [is_close_win] 是否关闭增加朋友窗口,默认关闭，可以设置为false不关闭
 * @property {string|null} [say_hi] 加好友时打招呼内容,如果为空，则保持微信默认
 * @property {string|null} [suffix] 备注后缀，如：设置suffix为"test",则备注为:xxxx_test,可以未来以备注的后缀来区分不同的好友分类
 * @property {string|null} [label] 设置标签
 */

/**
 * 通过手机号码或微信号添加好友。
 * @typedef {object} AddFriendsRequest
 * @property {string[]|null} [friends]
 * @property {AddFriendsOptions} [options]
 */

/**
 * 从群聊中添加好友。
 * @typedef {object} AddGroupMemberToFriendsRequest
 * @property {string|null} [group_name]
 * @property {string[]|null} [members]
 * @property {AddFriendsOptions} [options]
 */

/**
 * 修改群备注。
 * @typedef {object} ChangeGroupMemoRequest
 * @property {string|null} [group_name]
 * @property {string|null} [new_memo]
 */

/**
 * 修改自己在群中的昵称。
 * @typedef {object} ChangeGroupNickNameRequest
 * @property {string|null} [group_name]
 * @property {string|null} [nick_name]
 */

/**
 * 消息引用。
 * @typedef {object} ChatRefer
 * @property {string} [date] 日期,如果不设置则不进行日期筛选
 * @property {ChatSimpleMessage} [message]
 * @property {boolean} [is_close_search_win] 是否关闭查找窗口，默认是关闭，如果设置为false,则不关闭查找窗口，速度会略快，但需要自行关闭查找窗口.
 */

/**
 * 简单聊天消息。
 * @typedef {object} ChatSimpleMessage
 * @property {string|null} [who] 微信名称
 * @property {string|null} [message] 消息
 * @property {string|null} [send_date_time] 消息日期
 * @property {string} [date_time] 消息日期,日期时间格式
 * @property {string|null} [unique_string] 唯一字符串
 */

/**
 * 颜色（只读响应对象）。
 * @typedef {object} Color
 * @property {number} [r] (read-only)
 * @property {number} [g] (read-only)
 * @property {number} [b] (read-only)
 * @property {number} [a] (read-only)
 * @property {boolean} [is_known_color] (read-only)
 * @property {boolean} [is_empty] (read-only)
 * @property {boolean} [is_named_color] (read-only)
 * @property {boolean} [is_system_color] (read-only)
 * @property {string|null} [name] (read-only)
 */

/**
 * 调色板（只读响应对象）。
 * @typedef {object} ColorPalette
 * @property {number} [flags] (read-only)
 * @property {Color[]|null} [entries] (read-only)
 */

/**
 * 创建群聊。
 * @typedef {object} CreateGroupRequest
 * @property {string|null} [group_name]
 * @property {string|null} [first_who]
 * @property {string[]|null} [members]
 */

/**
 * 个人信息。
 * @typedef {object} FriendInfo
 * @property {string|null} [nick_name] 昵称
 * @property {string|null} [memo_name] 备注名
 * @property {string|null} [area] 地区,建议仅供参考
 * @property {string[]|null} [lable] 标签
 * @property {string|null} [same_group_number] 共同群数量
 * @property {string|null} [signature] 个性签名
 * @property {string|null} [source] 来源
 * @property {string|null} [wx_id] 微信ID
 * @property {string|null} [avatar_path] 头像路径
 * @property {Image} [avatar_image]
 * @property {string|null} [avatar_image_base64]
 * @property {FriendSearchResultEnums} [friend_search_result]
 * @property {ChatType} [chat_type]
 * @property {string|null} [add_date_time] 添加好友时间，>4.x版本才有此属性
 * @property {string|null} [name] 微信中使用的名称 本质是: 如果昵称不等于备注名，则以备注名不为主. (read-only)
 */

/**
 * 添加/移除群成员。group_name 为空时作用于当前焦点群聊。
 * @typedef {object} GroupMemberRequest
 * @property {string|null} [group_name]
 * @property {string[]|null} [members]
 */

/**
 * 图像对象（大部分字段为只读响应字段）。
 * @typedef {object} Image
 * @property {*} [tag]
 * @property {SizeF} [physical_dimension]
 * @property {Size} [size]
 * @property {number} [width] (read-only)
 * @property {number} [height] (read-only)
 * @property {number} [horizontal_resolution] (read-only)
 * @property {number} [vertical_resolution] (read-only)
 * @property {number} [flags] (read-only)
 * @property {ImageFormat} [raw_format]
 * @property {PixelFormat} [pixel_format]
 * @property {number[]|null} [property_id_list] (read-only)
 * @property {PropertyItem[]|null} [property_items] (read-only)
 * @property {ColorPalette} [palette]
 * @property {string[]|null} [frame_dimensions_list] (read-only)
 */

/**
 * 图像格式。
 * @typedef {object} ImageFormat
 * @property {string} [guid]
 */

/**
 * 邀请群成员（外部群）。
 * @typedef {object} InviteGroupMemberRequest
 * @property {string|null} [group_name]
 * @property {string[]|null} [members]
 * @property {string|null} [invite_reason]
 */

/**
 * 定位会话。
 * @typedef {object} LocateConversationRequest
 * @property {string|null} [title]
 */

/**
 * 点击任务栏微信图标。index 与 wechat_name 二选一。
 * @typedef {object} NotifyIconClickRequest
 * @property {number|null} [index]
 * @property {string|null} [wechat_name]
 */

/**
 * 属性项。
 * @typedef {object} PropertyItem
 * @property {number} [id]
 * @property {number} [len]
 * @property {number} [type]
 * @property {string|null} [value]
 */

/**
 * 引用消息。优先使用 message（ChatSimpleMessage），否则使用 who + message_content。
 * @typedef {object} ReferenceRequest
 * @property {ChatSimpleMessage} [message]
 * @property {string|null} [who]
 * @property {string|null} [message_content]
 * @property {number} [prev_scroll_number]
 */

/**
 * 修改群名。
 * @typedef {object} RenameGroupRequest
 * @property {string|null} [old_group_name]
 * @property {string|null} [new_group_name]
 */

/**
 * 保存头像到指定路径。
 * @typedef {object} SaveAvatarRequest
 * @property {string|null} [path]
 */

/**
 * 搜索会话。
 * @typedef {object} SearchConversationRequest
 * @property {string|null} [who]
 */

/**
 * 发送表情。emoji 与 index 二选一（优先使用 index）。
 * @typedef {object} SendEmojiRequest
 * @property {string|null} [who]
 * @property {string|null} [emoji] 表情名称或描述。
 * @property {number|null} [index] 表情索引。
 * @property {string[]|null} [at_users]
 */

/**
 * 发送文件。who 为空时发送给当前焦点聊天窗口。
 * @typedef {object} SendFileRequest
 * @property {string|null} [who]
 * @property {string[]|null} [files]
 */

/**
 * 发送文本消息。who 为空时发送给当前焦点聊天窗口。
 * @typedef {object} SendMessageRequest
 * @property {string|null} [who]
 * @property {string|null} [message]
 * @property {string[]|null} [at_users] 被 @ 的好友列表。
 * @property {ChatRefer} [refer]
 */

/**
 * 发起多人语音聊天。who 为空时作用于当前焦点聊天窗口。
 * @typedef {object} SendVoiceChatsRequest
 * @property {string|null} [who]
 * @property {string[]|null} [partner]
 */

/**
 * 发送语音消息。who 为空时发送给当前焦点聊天窗口。
 * @typedef {object} SendVoiceMessageRequest
 * @property {string|null} [who]
 * @property {string|null} [file_path]
 */

/**
 * 文字转语音发送。
 * @typedef {object} SendVoiceMessageTtsRequest
 * @property {string|null} [who]
 * @property {string|null} [api_key]
 * @property {string|null} [message]
 * @property {VoiceOptions} [options]
 * @property {boolean} [optimize_with_llm]
 */

/**
 * 设置会话免打扰。who 为空时作用于当前窗口。
 * @typedef {object} SetDoNotDisturbRequest
 * @property {string|null} [who]
 * @property {boolean} [setting]
 */

/**
 * 设置会话置顶。who 为空时作用于当前窗口。
 * @typedef {object} SetTopMostRequest
 * @property {string|null} [who]
 * @property {boolean} [setting]
 */

/**
 * 尺寸（整数）。
 * @typedef {object} Size
 * @property {boolean} [is_empty] (read-only)
 * @property {number} [width]
 * @property {number} [height]
 */

/**
 * 尺寸（浮点）。
 * @typedef {object} SizeF
 * @property {boolean} [is_empty] (read-only)
 * @property {number} [width]
 * @property {number} [height]
 */

/**
 * 打开/关闭指定好友或群的子窗口。
 * @typedef {object} SubWindowRequest
 * @property {string|null} [who]
 */

/**
 * 切换/关闭导航栏窗口。
 * @typedef {object} SwitchNavigationRequest
 * @property {NavigationType} [navigation_type]
 */

/**
 * 拍一拍。
 * @typedef {object} TapRequest
 * @property {string|null} [who]
 * @property {number} [prev_scroll_number]
 */

/**
 * 更新群公告。
 * @typedef {object} UpdateGroupNoticeRequest
 * @property {string|null} [group_name]
 * @property {string|null} [group_notice]
 */

/**
 * 语音合成选项。
 * @typedef {object} VoiceOptions
 * @property {string|null} [voice] 音色 系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
 * @property {string|null} [model] 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。 可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。
 * @property {string|null} [language_type] 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。
 * @property {string|null} [instructions] 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。 系统音色与声音复刻音色均可输入任意指令。
 * @property {number|null} [volume] 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。
 * @property {number|null} [rate] 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制， 避免与指令中的语速描述冲突。
 * @property {number|null} [pitch] 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。
 * @property {number|null} [seed] 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。
 * @property {boolean} [optimize_instructions] 是否在发送前用LLM进行口语化校核
 * @property {boolean} [is_stream]
 */

/**
 * 仅携带 who（可为空，作用于当前焦点窗口）的请求。
 * @typedef {object} WhoRequest
 * @property {string|null} [who]
 */

export {};
