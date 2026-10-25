using System.ComponentModel;

namespace WeAutoCommon.Enums
{

    /// <summary>
    /// 上传类型
    /// </summary>
    public enum UploadType
    {
        [Description("图片")]
        Image = 0,
        [Description("视频")]
        Vedio = 1,
        [Description("文件")]
        File = 2,
    }


}