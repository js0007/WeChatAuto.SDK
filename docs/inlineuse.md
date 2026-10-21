!> 内嵌方式（Embedded）即：在你的 .NET 应用里直接引用 **NuGet 包 / DLL**，在**自己的进程内**调用 SDK 的 API。类型安全、能力最全、可深度定制，尤其适合需要**事件回调、低延迟、把微信能力深度集成进自己业务**的场景。本文档覆盖社区版 + VIP 版全部 API，标 💎 的为 **VIP 专有能力**。

> 如果你不是 .NET、不想管理 SDK 生命周期，或需要跨语言/远程调用，请改用 [HTTP WebAPI 方式](https://scottfly189.github.io/WeChatAuto.SDK/main.html#/httpserveruse)，官方推荐。

---

## 一、适用场景（先选对方式）

| | 内嵌方式（本文档） | HTTP WebAPI 方式 |
|---|---|---|
| 适合 | .NET / Windows 原生应用，需要事件回调、低延迟、完整 API | 后端/云服务、跨语言、多系统共享、快速集成 |
| 入口 | `WeAutomation.Initialize(...)` → `WeChatClientFactory` → `WeChatClient` | 启动 Server，REST + Swagger |
| 特点 | 类型安全、能力最全、深度定制 | 语言无关、进程隔离、可远程、开箱即用 |

!> 绝大多数场景推荐 HTTP WebAPI 方式；需要 DLL/NuGet 包、深度回调（消息监听、好友监听）、或要把 SDK 编译进自己应用时，选内嵌方式。

---

## 二、环境要求与准备

### 2.1 基础要求

- **操作系统**：仅支持 **Windows**（Win10 及以上），不支持 Linux / macOS；
- **.NET**：`net6.0-windows` / `net7.0-windows` / `net8.0-windows` / `net9.0-windows` / `net10.0-windows` 任选其一；
- **微信版本**：`>= 4.1.9.30`，推荐使用最新版微信；
- **微信状态**：先打开微信并登录，**保持微信窗口为打开（非关闭）状态**，支持多开多个微信。

### 2.2 UI Tree（最关键的前置条件）

SDK 通过 Windows UI Automation（FlaUI/UIA3）驱动微信，前提是微信暴露 **UI Tree**。

- 如果最新版微信没有 UI Tree，SDK 支持**强开 UI Tree**：初始化时设置 `IsForceOpenUITree = true`；
- 参考 [我的最新版微信没有 UI Tree，这个怎么办？](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)；
- 用 `inspect.exe` / `FlaUInspect` 等工具可查看 UI Tree（[工具下载](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/Tools)）。

### 2.3 OCR 模型（部分能力必需）

若启用 OCR（`EnableOCR = true`），需把 OCR 模型文件 `models` 文件夹放到**程序运行目录**下：

- 开发环境（VS 运行）：`bin\Debug\netx.x-windows\models`
- 模型文件下载：[Tools/models](https://github.com/scottfly189/WeChatAuto.SDK/tree/master/Tools)

> 部分能力（如消息监听中的昵称识别）依赖 OCR，建议直接开启。

### 2.4 虚拟声卡（仅发语音消息需要）

`SendVoiceMessage` / TTS 语音依赖虚拟声卡 **Cable input / Cable output**：

- 在 `声音 → 设置 → 输入设备` 改成 `Cable output`；
- 未安装请在 [VBCABLE_Driver_Pack45.zip](https://github.com/alexzhao189/wechatautosdk/blob/main/Resources/VBCABLE_Driver_Pack45.zip) 下载。

---

## 三、安装

### 3.1 通过 NuGet 安装

```bash
dotnet add package WeChatAuto4x.SDK
```

或使用 Visual Studio / Rider 的 NuGet 包管理器搜索 `WeChatAuto4x.SDK`。

### 3.2 修改目标框架

SDK 面向 Windows 目标框架，把 `.csproj` 的 `TargetFramework` 改成带 `-windows` 后缀：

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

### 3.3 直接引用 DLL（可选）

也可以不通过 NuGet，直接把编译产物 `WeChatAuto.dll`（及依赖）拷贝到项目并添加引用。

---

## 四、快速开始（5 分钟跑通）

!> 最小示例为一个控制台.net应用，所以首先保证

一个最小可运行示例：初始化 → 拿客户端 → 发一条消息。

```csharp
using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

// 1. 初始化（内部依赖注入，适用于普通控制台程序）
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.EnableOCR = true;         // 部分能力依赖 OCR，建议开启（需提前放置 models 目录）
    options.IsForceOpenUITree = true; // 微信未公开 UI Tree 时自动强开；确认已开启可设为 false
    options.DebugMode = false;        // 生产环境建议关闭（开启会在获得焦点时高亮边框）
});

// 2. 获取客户端工厂（单例，务必记得释放）
using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();

// 3. 查看电脑上打开了几个微信
var names = clientFactory.GetWeChatClientNames();
Console.WriteLine($"检测到 {names.Count} 个微信：{string.Join(", ", names)}");

// 4. 拿到第一个微信客户端
var client = clientFactory.GetWeChatClient(names.First());

// 5. 给好友发消息（把 "某好友" 换成你自己的好友 / 群聊昵称）
await client.SendMessage("某好友", "hello world!!");
```

!> 运行前提：微信已登录且窗口可见；若报 `WechatNotOpenedException` / `WechatNotSupportUITreeException`，请回到 [2.2 UI Tree](#22-ui-tree最关键的前置条件) 排查。

---

## 五、核心概念（务必先读）

### 5.1 对象模型

```text
WeAutomation.Initialize(...)        // 初始化入口（全局唯一）
        └── WeChatClientFactory     // 多微信实例管理（单例，IDisposable）
                └── WeChatClient    // 单个微信客户端（IDisposable）
                        ├── Conversations    会话列表
                        ├── ChatContent      聊天内容 / 发送
                        ├── AddressBookList  通讯录
                        ├── MessageMonitor   消息监听 💎
                        ├── NewFriendMonitor 新好友监听 💎
                        ├── Moments          朋友圈 💎
                        ├── OwnerGroup       自有群
                        ├── OuterGroup       外部群
                        ├── Search           搜索 / 加好友
                        ├── NotifyIcon       任务栏图标
                        └── CacheManager     好友缓存
```

大部分能力都直接挂在 `WeChatClient` 上，即 `client.SendMessage(...)`、`client.AddMessageListener(...)`，无需深入子对象。

### 5.2 `who` 命名约定（很重要）

- 几乎所有方法的 `who` 参数都用**肉眼可见的昵称 / 备注名**定位好友/群聊，**不是 wxid**；
- `who` **可以为空**，空表示作用于**当前焦点所在的聊天窗口**（有状态，调用前请确保焦点在目标会话上）；
- 昵称可能重复，`wxid` 永不重复：需要精确区分时，建议给好友设置备注，或使用缓存中基于 `wxid` 的查询方法（如 `GetFriendWithWxIDFromCache`）。

### 5.3 异步 + 单 UI 线程模型

- 所有 API 都是 `async Task`，请 `await`；
- SDK 内部所有 UI 动作串行跑在**一个专用线程** `wechatauto.sdk` 上，**监听与发送互不抢鼠标/窗口**——这是刻意设计，业务上多线程调用最终也会排队，无需手动加锁；
- 消息监听等回调会在后台多线程触发，但**真正操作 UI 仍要回到微信单线程执行**。

### 5.4 两种依赖注入模式（二选一，互斥）

| 模式 | 方法 | 返回值 | 适用 |
|---|---|---|---|
| 内部 DI | `WeAutomation.Initialize(Action<WeChatConfig>)` | `IServiceProvider` | 裸控制台 / 无 DI 框架 |
| 外部 DI | `WeAutomation.Initialize(IServiceCollection, Action<WeChatConfig>)` | `IServiceCollection` | ASP.NET Core / Generic Host 已有 DI |

!> 两种 `Initialize` **不能混用**，混用会抛 `InvalidOperationException`。宿主已有 DI 用外部重载；否则用内部重载。

---

## 六、初始化与配置

### 6.1 内部 DI（普通控制台）

```csharp
var serviceProvider = WeAutomation.Initialize(options =>
{
    options.EnableOCR = true;
    options.IsForceOpenUITree = true;
});

using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();
var client = factory.GetWeChatClient(factory.GetWeChatClientNames().First());
```

### 6.2 外部 DI（ASP.NET Core / Generic Host）

```csharp
var builder = WebApplication.CreateBuilder(args);

WeAutomation.Initialize(builder.Services, options =>
{
    options.EnableOCR = true;
});

var app = builder.Build();

app.MapGet("/send", async (string who, string message, IServiceProvider sp) =>
{
    var factory = sp.GetRequiredService<WeChatClientFactory>();
    var client = factory.GetWeChatClient(factory.GetWeChatClientNames().First());
    await client.SendMessage(who, message);
    return Results.Ok();
});

app.Run();
```

### 6.3 常用配置项（`WeChatConfig`）

| 配置项 | 默认 | 说明 |
|---|---|---|
| `EnableOCR` | `false` | 是否启用 OCR（部分能力必需，需放置 models 目录） |
| `IsForceOpenUITree` | `false` | 微信未公开 UI Tree 时是否强开 |
| `IsOptimizeMainWindowLayout` | `true` | 启动时是否自动排列/优化主窗口布局 |
| `DebugMode` | `false` | 调试模式，开启后获得焦点时高亮边框 |
| `InitAdressBook` | `false` | 是否启动即初始化通讯录缓存，**以 wxid 为核心业务强烈建议开启** |
| `DefaultSavePath` | `wxauto_download` | 下载文件/图片默认保存路径 |
| `EnableRecordVideo` / `TargetVideoPath` | `false` | 是否录制视频及保存路径（依赖 ffmpeg） |
| `EnableMouseKeyboardSimulator` | `false` | 是否启用硬件键鼠模拟器（需购买设备） |
| `ProcessDpiAwareness` | `1` | DPI 感知级别（应用已设置 DPI 感知则此参数无效） |
| `ListenInterval` 等 | 见源码 | 各类监听的时间间隔（秒） |

> 完整配置项见 `WeAutoCommon.Configs.WeChatConfig`，多数字段同时支持 snake_case 的 JSON 序列化名。

---

## 七、API 使用指南

> 以下示例均假设已拿到 `client`（`WeChatClient`）与 `factory`（`WeChatClientFactory`），并按需引入命名空间：`WeChatAuto.Services`、`WeChatAuto.Components`、`WeChatAuto.Models`、`WeChatAuto.Options`、`WeAutoCommon.Enums`。社区版 / VIP 版能力差异见 [社区版 vs VIP 版](./ability.md)。

### 7.1 获取微信客户端

```csharp
// 列出所有已打开微信的昵称
var names = factory.GetWeChatClientNames();

// 按昵称取客户端（不存在会抛 WechatClientNotExistException）
var client = factory.GetWeChatClient("某昵称");

// 或一次性取全部（Dictionary<string, WeChatClient>，key 为昵称）
var all = factory.GetWeChatClientList();
```

### 7.2 个人信息与窗口

```csharp
// 本微信个人信息（昵称 / wxid / 头像路径）
var info = client.GetOwerInfo();
Console.WriteLine($"昵称={info.NickName}, wxid={info.WxId}");

// 窗口控制
await client.Focus();          // 使主窗口获得焦点
await client.Max();            // 最大化
await client.Restore();        // 还原
await client.Pinned();         // 置顶
await client.UnPinned();       // 取消置顶

// 句柄 / 进程 ID
nint hwnd = client.GetHandler();
nint pid  = client.GetProcessId();

// 打开 / 关闭子窗口（好友、群聊的聊天子窗口）
await client.OpenSubWin("某好友");
await client.CloseSubWin("某好友");

// 导航
await client.SwitchNavigation(NavigationType.朋友圈);
```

### 7.3 发送消息

#### 发送文本

```csharp
// 给指定好友/群聊发文本
await client.SendMessage("某好友", "你好");

// who 传空 → 发给当前焦点窗口
await client.SendMessage("你好");

// @某人（群聊，支持多个）
await client.SendMessage("某群", "开会了", atUser: new[] { "张三", "李四" });

// 引用某条历史消息后回复
await client.SendMessage("某好友", "好的", refer: new ChatRefer
{
    Message = new ChatSimpleMessage { Who = "某好友", Message = "明天有空吗？" }
});
```

#### 发送文件 / 表情

```csharp
// 发文件（多个）
await client.SendFile("某好友", new[] { @"C:\report.pdf", @"C:\photo.jpg" });

// 发表情（按名称或索引）
await client.SendEmoji("某好友", "[微笑]");
await client.SendEmoji("某好友", 1);
```

#### 语音 / 视频通话

```csharp
await client.SendVoiceChat("某好友");            // 单人语音通话
await client.SendVedioChat("某好友");            // 单人视频通话
await client.SendVoiceChats("某群", new[] { "张三", "李四" }); // 群多人语音
```

#### 发送语音消息（需虚拟声卡）

```csharp
await client.SendVoiceMessage("某好友", @"C:\audio\hello.wav");
```

!> `SendVoiceMessage` 依赖虚拟声卡 Cable input/output，见 [2.4 虚拟声卡](#24-虚拟声卡仅发语音消息需要)。

#### 文字转语音（TTS，💎）

```csharp
// 系统默认走阿里千问 Qwen-Audio-TTS
await client.SendVoiceMessageWithTTS(
    "某好友",
    "sk-你的阿里云百炼APIKey",   // 申请：https://bailian.console.aliyun.com
    "你好，欢迎使用我们的服务");

// 自定义音色 / 指令控制
await client.SendVoiceMessageWithTTS(
    "某好友", "sk-xxx", "你好",
    new VoiceOptions
    {
        Voice = "你的音色",
        Instructions = "用温柔的语气说，语速稍慢"
    });
```

### 7.4 获取消息与聊天历史

```csharp
// 主动拉取（轮询），适用于 wechat bot 定时取最新消息
var msgs = await client.PullMessages("某好友", maxFetchNumber: 20);
foreach (var m in msgs)
    Console.WriteLine(m);   // 格式：类型 - 时间 - 谁: 内容

// 按日期获取聊天历史
var today = await client.GetChatHistory("某好友", DateTime.Today);
var range = await client.GetChatHistory("某好友", new DateTime(2026, 10, 1), new DateTime(2026, 10, 20));

// 拍一拍 / 引用
await client.TapWho("某好友");
await client.ReferencedMessage("某好友", "某条消息内容");
```

### 7.5 会话管理

```csharp
// 所有会话标题（仅名称列表，高效）
var titles = await client.GetAllConversations();

// 可见会话（含名称/未读数/头像等）
var visible = await client.GetVisibleConversations();

// 搜索 / 定位会话
bool found = await client.SearchFriend("某好友");
bool ok = await client.LocateConversation("某好友");

// 会话设置
await client.SetTopMost("某好友", true);       // 置顶
await client.SetDoNotDisturb("某好友", true);  // 免打扰
```

### 7.6 好友与通讯录

```csharp
// 获取所有好友（fromCache=true 从缓存读，快；false 重新刷通讯录）
var friends = await client.GetAllFriends(fromCache: true);
foreach (var f in friends)
    Console.WriteLine($"{f.Name}  wxid={f.WxId}");

// 好友名称列表
var names = await client.GetAllFriendNames();

// 缓存查询（昵称可能重复，wxid 唯一）
var f1 = client.GetFriendFromCache("某好友");
var f2 = client.GetFriendWithWxIDFromCache("wxid_xxx");
var f3 = client.GetFriendsFromCache("某好友");   // 重名时返回多个

// 手动维护缓存
client.AddOrUpdateFriendFromCache(f1);
client.RemoveFriendFromCache("某好友");

// 加好友（手机号 / 微信号）
var results = await client.AddFriends(
    new[] { "13800138000", "wxid_xxxx" },
    new AddFriendsOptions
    {
        SayHi = "你好，我是 xx",      // 打招呼内容（空则微信默认）
        Suffix = "_新客户",           // 备注后缀
        IntervalTime = 5              // 间隔秒数，担心风控可调大
    });
foreach (var kv in results)
    Console.WriteLine($"{kv.Key}: {kv.Value}");   // FriendAddResult 枚举

// 删除好友
await client.RemoveFriend("某好友");
```

### 7.7 群管理

```csharp
// 自有群
await client.CreateOwnerChatGroup("测试群", "某好友", new[] { "张三", "李四" });
await client.GetChatGroupMemberList("测试群");
await client.AddOwnerChatGroupMember("测试群", "王五");
await client.RemoveOwnerChatGroupMember("测试群", "王五");
await client.ChangeOwnerChatGroupName("测试群", "新群名");
await client.ChangeChatGroupMemo("测试群", "这是备注");
await client.UpdateGroupNotice("测试群", "大家好");
var owner = await client.GetGroupOwner("测试群");
await client.QuitChatGroup("测试群");

// 外部群
await client.InviteChatGroupMember("某外部群", new List<string> { "张三", "李四" });

// 群内加好友（微信严风控操作，谨慎分批）
await client.AddChatGroupMemberToFriends("某外部群", new List<string> { "张三" });
```

!> 群内加好友是**严风控**操作，建议分批、一次不超过 20~30 个，时间跨度拉长到 4 小时或一天后。

### 7.8 消息监听（事件驱动，💎）

监听以**事件回调**方式触发：来消息时调用你的回调，`MessageContext` 里带有新消息、历史消息、`Sender`（回复当前窗口）、`Client`（完整能力）。

```csharp
// 固定监听某个好友/群
await client.AddMessageListener("某好友", ctx =>
{
    foreach (var m in ctx.NewMessages)
        Console.WriteLine($"{m.Who}: {m.Message}");

    // 回复当前监听窗口（Sender 只能给当前窗口发）
    ctx.Sender.SendMessage("已收到").GetAwaiter().GetResult();
});

// 监听多个
await client.AddMessageListener(new[] { "好友A", "群B" }, ctx => { /* ... */ });

// 开放式监听：不固定对象，所有"未开免打扰"的好友/群都监听
await client.AddMessageListener((string)null, ctx => { /* ... */ }, IsOpenMonitor: true);

// 固定时间段监听（9:00~18:00 生效）
await client.AddMessageListener(
    "某好友", ctx => { /* ... */ },
    new TimeOnly(9, 0), new TimeOnly(18, 0));

// 子窗口监听（弹窗式，返回更及时，回调为 async）
await client.AddMessageListenerWithSubWin("某好友", async ctx =>
{
    foreach (var m in ctx.NewMessages)
        await ctx.Sender.SendMessage($"收到：{m.Message}");
});

// 群聊系统消息监听（如：群主邀请好友后发欢迎语）
await client.AddGroupSystemMessageListener("某群", async ctx =>
{
    foreach (var m in ctx.NewMessages)
        Console.WriteLine($"[{ctx.FromWho}] 系统消息: {m}");
});

// 暂停 / 恢复 / 增减监听对象
await client.PauseMessageListener();
await client.ResumeMessageListener();
await client.AddListeningFriend("某好友");
await client.RemoveListeningFriend("某好友");
```

> **监听规则**（务必注意）：只能监听"允许消息通知"的好友/群聊，请勿给被监听对象设置"消息免打扰"；不监听的对象建议设免打扰以提升性能；建议减少"置顶"数量以降低滚动。

> `MessageMonitorOptions` 可精细控制：是否抓图片/附件/视频（`FetchImage`/`FetchAttachment`/`FetchVideo`）、是否点红包（`ClickRedEnvelope`）、风控预防（`IsRiskPrevention` + `RiskPreventionAction`）、数据库写入（`DbType`/`ConnectionString`/`WriteDataBaseCallback`）等。

> `UIInvoker` 参数（`Action<string>`）用于把微信嵌入 UI 的场景（如多微信切换 Tab 页），SDK 会把微信名称注入给你的调度器。

### 7.9 新好友自动通过（💎）

```csharp
var options = new FriendRequestAutoAcceptOptions
{
    KeyWord = new List<string> { "合作", "商务" },  // 只有打招呼命中关键词才通过
    Suffix = "_商务",                               // 通过后备注加后缀
    Label = "客户",                                 // 通过后打标签
    PassedDelete = true,                            // 通过后删除申请记录
    PassedCallBack = async (passedList, wxClient, sp) =>
    {
        foreach (var item in passedList)
            await wxClient.SendMessage(item.Who, "您好，欢迎添加！");
    }
};

await client.AddFriendRequestAutoAcceptListener(options);

// 暂停 / 恢复 / 停止
await client.PauseNewFriendListener();
await client.ResumeNewFriendListener();
await client.StopNewFriendListener();   // 停止后可换配置重新启动
```

### 7.10 朋友圈（💎）

```csharp
// 发朋友圈（图文，多图需在同一目录）
await client.AddMoments(
    new List<string> { @"C:\img\a.jpg", @"C:\img\b.jpg" },
    "今天天气不错",
    new MomentsOptions { Labels = new List<string> { "朋友" } });

// 删除自己发的朋友圈（按文字内容）
await client.RemoveMoments("今天天气不错");
```

### 7.11 消息转发（💎）

```csharp
// 转发多条（默认最后 5 条）
await client.ForwardMultipleMessage("某好友", "某群", rowCount: 5);

// 转发单条
await client.ForwardSingleMessage("某好友", "某条消息内容", "某群");
```

### 7.12 声音复刻（💎）

```csharp
// 上传参考音频克隆音色，返回 voice_id，可用于后续 TTS
var voiceId = await client.CloneVoice(
    "sk-你的千问APIKey",
    "https://example.com/ref.wav");   // 参考音频公网 URL

// 后续用它发语音
await client.SendVoiceMessageWithTTS("某好友", "sk-xxx", "你好",
    new VoiceOptions { Voice = voiceId });
```

!> 复刻的 `targetModel` 必须与合成时的 `model` 一致，不能跨模型使用。

---

## 八、多微信实例

`WeChatClientFactory` 一个进程可同时管理多个微信客户端：

```csharp
using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();

foreach (var name in factory.GetWeChatClientNames())
{
    var c = factory.GetWeChatClient(name);
    await c.SendMessage("某好友", $"来自 {name} 的问候");
}
```

> 多实例共用一个 UI 动作线程，操作会被串行调度，天然避免多微信之间抢鼠标。

---

## 九、生命周期与资源释放

- `WeChatClientFactory` 是单例且 `IDisposable`，建议 `using var factory = ...` 或手动 `factory.Dispose()`；
- `WeChatClient` 由工厂统一创建与管理，**不要手动 `Dispose` 单个 client**——工厂 `Dispose` 时会统一释放所有 client、监听器、录制器与 UI 线程；
- 释放后 SDK 会停止消息监听、取消相关 `CancellationToken`。

```csharp
using var factory = serviceProvider.GetRequiredService<WeChatClientFactory>();
// ... 业务逻辑 ...
// using 结束会自动释放，无需额外处理
```

---

## 十、风控与最佳实践

!> SDK 驱动的是**真实微信客户端**（UI 自动化），非幂等纯 CRUD。加好友、邀请群成员、群内加好友等属于微信**严风控**操作。

- **控制频率**：批量加好友/拉群建议分批、拉长时间间隔（几小时到一天）；
- **随机延时**：SDK 内置随机延时与点击偏移，可在配置中调整（如 `AddFriendsOptions.IntervalTime`）；
- **键鼠模拟器**：高敏感场景（如群内加好友）强烈建议搭配硬件键鼠模拟器，输入等同真人，显著降低风控误判；
- **监听风控预防**：`MessageMonitorOptions.IsRiskPrevention = true` 会在运行一段时间后自动暂停监听、随机等候（可自定义 `RiskPreventionAction`）；
- **合规**：请遵守微信使用条款，仅用于合法用途。

---

## 十一、常见问题（FAQ）

- **运行自动化不了微信**：确认微信已登录且窗口可见、系统为 Win10+、微信版本 >= 4.1.9.30；最新版微信若无 UI Tree 请开启 `IsForceOpenUITree` 或参考 [issue#3](https://github.com/scottfly189/WeChatAuto.SDK/issues/3)。
- **提示 `Det模型文件不存在`**：未放置 OCR 模型，把 `models` 文件夹拷到程序运行目录（见 [2.3 OCR 模型](#23-ocr-模型部分能力必需)）。
- **提示 `获取微信窗口失败` / `微信客户端不存在`**：微信未打开、窗口隐藏，或昵称写错（区分大小写、精确匹配）。
- **提示 `不支持UI Tree`**：微信未公开 UI Tree，开启 `IsForceOpenUITree`，或用工具确认 UI Tree。
- **两种 `Initialize` 混用报错**：外部 DI 与内部 DI 只能二选一，见 [5.4 两种依赖注入模式](#54-两种依赖注入模式二选一互斥)。
- **消息监听不回调**：被监听对象被设了"消息免打扰"；或未 `await AddMessageListener`。只有"允许消息通知"的对象才能监听。
- **发语音失败**：需安装虚拟声卡 Cable input/output（见 [2.4 虚拟声卡](#24-虚拟声卡仅发语音消息需要)）。
- **远程桌面最小化后自动化异常 / 发不了消息**：见 [FAQ](https://github.com/scottfly189/WeChatAuto.SDK/blob/master/docs/faq.md) 中的 RDP 与剪贴板设置。
- **更多问题**：参见 [完整 FAQ](./faq.md)，或提交 [Issues](https://github.com/scottfly189/WeChatAuto.SDK/issues)。
