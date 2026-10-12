namespace WeChatAuto.Options
{
    public class VoiceOptions
    {
        /// <summary>
        /// 音色
        /// 系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
        /// </summary>
        public string Voice { get; set; } = "Cherry";
        /// <summary>
        /// 语言类型,默认"Chinese",可选值有: 'Auto', 'Chinese', 'English', 'German', 'Italian', 'Portuguese', 'Spanish', 'Japanese', 'Korean', 'French' or 'Russian'
        /// </summary>
        public string LanguageType { get; set; } = "Auto";
        /// <summary>
        /// 应用于qwen3-tts-instruct-flash模型
        /// </summary>
        public string Instructions { get; set; } = "";
        /// <summary>
        /// 是否在发送前用LLM进行口语化校核
        /// </summary>
        public bool OptimizeInstructions { get; set; } = false;
        public bool IsStream { get; set; } = false;
    }
}