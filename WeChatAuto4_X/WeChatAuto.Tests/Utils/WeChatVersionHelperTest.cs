using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
using NAudio.Wave;
using OneOf;
using Xunit.Abstractions;
using NAudio.CoreAudioApi;
using WeChatAuto.Utils;


namespace WeChatAuto.Tests.Utils;

public class WeChatVersionHelperTest
{
    private readonly ITestOutputHelper _output;

    public WeChatVersionHelperTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "显示当前微信版本号")]
    public void TestName()
    {
        // Given
        var processes = Process.GetProcesses();
        var processId = 0;
        foreach(var process in processes)
        {
            if (process.ProcessName.Equals("Weixin"))
            {
                processId = process.Id;
                break;
            }
        }    
        // When
        var result = WeChatVersionHelper.GetRunnintVersion(processId);
        // Then
        _output.WriteLine($"当前微信版本:{result}");
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }


}