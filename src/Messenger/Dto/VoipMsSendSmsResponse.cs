using System.Text.Json.Serialization;

namespace Messenger.Dto;

public class VoipMsSendSmsResponse
{
    [JsonPropertyName("Status")]
    public string Status { get; set; }
    
    [JsonPropertyName("Sms")]
    public string Sms { get; set; }
}