package com.wechatauto.client.models;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * 文字转语音发送。
 */
public class SendVoiceMessageTtsRequest {

    @JsonProperty("who")
    private String who;

    @JsonProperty("api_key")
    private String apiKey;

    @JsonProperty("message")
    private String message;

    @JsonProperty("options")
    private VoiceOptions options;

    @JsonProperty("optimize_with_llm")
    private Boolean optimizeWithLlm;

    public String getWho() { return who; }
    public void setWho(String who) { this.who = who; }

    public String getApiKey() { return apiKey; }
    public void setApiKey(String apiKey) { this.apiKey = apiKey; }

    public String getMessage() { return message; }
    public void setMessage(String message) { this.message = message; }

    public VoiceOptions getOptions() { return options; }
    public void setOptions(VoiceOptions options) { this.options = options; }

    public Boolean getOptimizeWithLlm() { return optimizeWithLlm; }
    public void setOptimizeWithLlm(Boolean optimizeWithLlm) { this.optimizeWithLlm = optimizeWithLlm; }
}
