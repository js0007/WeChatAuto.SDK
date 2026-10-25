using System.Diagnostics;
using FlaUI.Core.Capturing;
using WeChatAuto.Services;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using Dm.util;

namespace WeChatAuto.Utils
{
    public class WeChatRecordVideo
    {
        public string TargetPath { get; set; }
        public VideoRecorder _VideoRecorder { get; set; } = null;
        private bool isRunning = false;

        public WeChatRecordVideo(string path)
        {
            TargetPath = path;
        }

        /// <summary>
        /// 全局录制录制视频
        /// 录制视频前，需要先下载ffmpeg(系统会自动下载ffmpeg,但是第一次会下载比较慢，请耐心等待)，并保存到当前目录下的ffmpeg文件夹
        /// </summary>
        /// <param name="fileName">文件名,默认不指定,如果不指定,则使用RecordVideo_20251109_123456.mp4作为文件名</param>
        /// <returns>视频文件路径</returns>
        public async Task<string> GlobalRecordVideo(string fileName = "")
        {
            if (isRunning)
            {
                return null;
            }
            isRunning = true;
            var _fileName = fileName;
            var rootPath = Path.Combine(WeAutomation.Config.TargetVideoPath);
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            Trace.WriteLine($"开始录制视频,保存目录: {rootPath}");
            if (string.IsNullOrWhiteSpace(fileName))
            {
                _fileName = $"RecordVideo_{DateTime.Now.ToString("yyyy_MM_dd_HHmmss")}.mp4";
            }
            var filePath = Path.Combine(rootPath, _fileName);
            Trace.WriteLine($"开始录制视频,保存文件: {filePath}");
            Trace.WriteLine($"如果是首次录制视频，会自动下载ffmpeg,请打开VPN,并且所需要时间比较长，请耐心等待...");
            var ffmpegPath = await VideoRecorder.DownloadFFMpeg(Path.Combine(Directory.GetCurrentDirectory(), "ffmpeg")).ConfigureAwait(false);
            Trace.WriteLine($"ffmpeg路径: {ffmpegPath}");
            Trace.WriteLine($"ffmpeg下载或者校验完成,开始录制视频...");
            _VideoRecorder = new VideoRecorder(new VideoRecorderSettings { VideoQuality = 26, ffmpegPath = ffmpegPath, TargetVideoPath = filePath }, r =>
                {
                    var img = Capture.Screen();
                    var infoOverlay = new InfoOverlay(img);
                    var mouseOverlay = new MouseOverlay(img);
                    img.ApplyOverlays(mouseOverlay, infoOverlay);
                    return img;
                });
            return filePath;
        }

        public void StopRecordVideo()
        {
            if (_VideoRecorder != null)
            {
                Trace.WriteLine($"停止录制视频!");
                _VideoRecorder.Stop();
            }
        }

        /// <summary>
        /// 开始指定元素位置录屏.
        /// 注意：StartRecord 方法 最好不要与 GlobalRecordVideo方法混用.
        /// </summary>
        /// <param name="element">指定</param>
        /// <returns></returns>
        public async Task<string> StartRecord(AutomationElement element)
        {
            if (!Directory.Exists(WeAutomation.Config.TargetVideoPath))
            {
                Directory.CreateDirectory(WeAutomation.Config.TargetVideoPath);
            }
            var fileName = Path.Combine(WeAutomation.Config.TargetVideoPath, $"{Guid.NewGuid().ToString("N")}.mp4");
            Trace.WriteLine($"开始录制视频,保存文件: {fileName}");

            var ffmpegPath = await VideoRecorder.DownloadFFMpeg(Path.Combine(Directory.GetCurrentDirectory(), "ffmpeg"));
            _VideoRecorder = new VideoRecorder(new VideoRecorderSettings { VideoQuality = 26, ffmpegPath = ffmpegPath, TargetVideoPath = fileName }, r =>
            {
                var img = Capture.ElementRectangle(element, new System.Drawing.Rectangle(
                    element.BoundingRectangle.X - 20,
                    element.BoundingRectangle.Y - 20,
                    element.BoundingRectangle.Width + 40,
                    element.BoundingRectangle.Height + 40
                ));
                var infoOverlay = new InfoOverlay(img);
                var mouseOverlay = new MouseOverlay(img);
                img.ApplyOverlays(mouseOverlay, infoOverlay);
                return img;
            });
            return fileName;
        }
    }
}