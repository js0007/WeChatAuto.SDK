/*
注意：首次运行应该将ocr的所有文件拷贝到运行根目录的models目录
如：\bin\Debug\net10.0-windows\models下面
*/

using WeChatAuto.Services;
using WeChatAuto.Components;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = WeAutomation.Initialize(options =>
{
    options.DebugMode = false;   // 可选，在生产环境建议关闭
    options.EnableOCR = true;    // 必须打开OCR
    options.IsForceOpenUITree = true; //是否强开微信的UI Tree,如果确认自己的微信UI Tree是开启的，则可以设置为False    
});

using var clientFactory = serviceProvider.GetRequiredService<WeChatClientFactory>();
//检查打开有几个微信
var wechat_list = clientFactory.GetWeChatClientNames();
Console.WriteLine($"电脑上打开微信{wechat_list.Count()}个: {string.Join(',',wechat_list)}");

// 打开第一个微信，发送消息
//var client = clientFactory.GetWeChatClient(wechat_list.First());
// Alex是你的微信的名字
var client = clientFactory.GetWeChatClient("Alex");
// 给好友 AI.Net 发送文本消息,这里应该改成你自己的微信名称.
await client.SendMessage("AI.Net","hello world222!!");
