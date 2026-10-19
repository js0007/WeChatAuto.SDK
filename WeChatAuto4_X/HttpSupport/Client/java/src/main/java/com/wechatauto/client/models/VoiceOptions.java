package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * VoiceOptions
 */
public class VoiceOptions {

    /** 音色系统音色，或者 自定义音色（克隆的音色或者自定义生成的音色） */
    @JsonProperty("voice")
    private String voice;

    /** 语音合成模型。Qwen-Audio-TTS 系列均支持指令控制、声音复刻与流式输出。可选值：qwen-audio-3.1-tts-flash（默认，系统音色最丰富）、qwen-audio-3.0-tts-flash、qwen-audio-3.0-tts-plus（官方推荐，适合克隆/设计音色）。 */
    @JsonProperty("model")
    private String model;

    /** 语言类型。Qwen-Audio-TTS 已不再使用该参数（由音色自身或指令控制决定），保留仅为向后兼容。 */
    @JsonProperty("language_type")
    private String languageType;

    /** 指令控制文本。Qwen-Audio-TTS 通过 instruction 参数以自然语言控制方言、情感、语速、音色等合成效果。系统音色与声音复刻音色均可输入任意指令。 */
    @JsonProperty("instructions")
    private String instructions;

    /** 音量。可选，默认 50，范围 [0, 100]。为 null 时不发送，由 API 使用默认值。 */
    @JsonProperty("volume")
    private Integer volume;

    /** 语速。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送，交由 instruction 控制，避免与指令中的语速描述冲突。 */
    @JsonProperty("rate")
    private Float rate;

    /** 音调。可选，默认 1.0，范围 [0.5, 2.0]。为 null 时不发送。 */
    @JsonProperty("pitch")
    private Float pitch;

    /** 随机种子。可选，范围 [0, 65535]。相同文本、音色与参数下，相同 seed 可复现相同合成结果。 */
    @JsonProperty("seed")
    private Integer seed;

    /** 是否在发送前用LLM进行口语化校核 */
    @JsonProperty("optimize_instructions")
    private Boolean optimizeInstructions;

    @JsonProperty("is_stream")
    private Boolean isStream;

    public String getVoice() { return voice; }
    public void setVoice(String voice) { this.voice = voice; }

    public String getModel() { return model; }
    public void setModel(String model) { this.model = model; }

    public String getLanguageType() { return languageType; }
    public void setLanguageType(String languageType) { this.languageType = languageType; }

    public String getInstructions() { return instructions; }
    public void setInstructions(String instructions) { this.instructions = instructions; }

    public Integer getVolume() { return volume; }
    public void setVolume(Integer volume) { this.volume = volume; }

    public Float getRate() { return rate; }
    public void setRate(Float rate) { this.rate = rate; }

    public Float getPitch() { return pitch; }
    public void setPitch(Float pitch) { this.pitch = pitch; }

    public Integer getSeed() { return seed; }
    public void setSeed(Integer seed) { this.seed = seed; }

    public Boolean getOptimizeInstructions() { return optimizeInstructions; }
    public void setOptimizeInstructions(Boolean optimizeInstructions) { this.optimizeInstructions = optimizeInstructions; }

    public Boolean getIsStream() { return isStream; }
    public void setIsStream(Boolean isStream) { this.isStream = isStream; }
}
