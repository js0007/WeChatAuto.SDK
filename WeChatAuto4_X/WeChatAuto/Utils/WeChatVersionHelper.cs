using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using OneOf.Types;
using WeChatAuto.Exceptions;
namespace WeChatAuto.Utils
{
    /// <summary>
    /// 微信版本帮助类.
    /// </summary>
    public static class WeChatVersionHelper
    {
        /// <summary>
        /// 获取微信版本号
        /// </summary>
        /// <returns></returns>
        public static string GetRunnintVersion()
        {
            var processes = Process.GetProcesses();
            var processId = 0;
            foreach (var process in processes)
            {
                if (process.ProcessName.Equals("Weixin"))
                {
                    processId = process.Id;
                    break;
                }
            }
            var result = WeChatVersionHelper.GetRunnintVersion(processId);
            return result;
        }
        /// <summary>
        /// 通过给定微信processId获取微信版本号.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        /// <exception cref="WechatNotOpenedException"></exception>
        public static string GetRunnintVersion(int processId)
        {
            var result = "";
            if (processId == 0)
                throw new WechatNotOpenedException("没有打开微信");
            try
            {
                var process = Process.GetProcessById(processId);
                var path = process.MainModule?.FileName;
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                    return result;
                var info = FileVersionInfo.GetVersionInfo(path);
                result = info.ProductVersion ?? info.FileVersion;
            }
            catch (Win32Exception)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return result;
        }
    }
}