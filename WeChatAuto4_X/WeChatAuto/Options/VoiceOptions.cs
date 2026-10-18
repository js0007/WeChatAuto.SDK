namespace WeChatAuto.Options
{
    public class VoiceOptions
    {
        /// <summary>
        /// 默认语音合成模型。声音复刻的 target_model 与合成时的 model 都必须使用此值。
        /// 如需整体回退到 qwen-audio-3.0-tts-flash，改这里即可。
        /// </summary>
        public const string DefaultModel = "qwen-audio-3.1-tts-flash";

        /// <summary>
        /// 音色
        /// 系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色）
        /// </summary>
        public string Voice { get; set; } = "longanhuan_v3.1";
        /// <summary>
        /// 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。
        /// 可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。
        /// </summary>
        public string Model { get; set; } = DefaultModel;
        /// <summary>
        /// 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。
        /// </summary>
        public string LanguageType { get; set; } = "Auto";
        /// <summary>
        /// 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。
        /// 系统音色与声音复刻音色均可输入任意指令。
        /// </summary>
        public string Instructions { get; set; } = "";
        /// <summary>
        /// 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。
        /// </summary>
        public int? Volume { get; set; } = null;
        /// <summary>
        /// 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，
        /// 避免与指令中的语速描述冲突。
        /// </summary>
        public float? Rate { get; set; } = null;
        /// <summary>
        /// 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。
        /// </summary>
        public float? Pitch { get; set; } = null;
        /// <summary>
        /// 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。
        /// </summary>
        public int? Seed { get; set; } = null;
        /// <summary>
        /// 是否在发送前用LLM进行口语化校核
        /// </summary>
        public bool OptimizeInstructions { get; set; } = false;
        public bool IsStream { get; set; } = false;
    }
}