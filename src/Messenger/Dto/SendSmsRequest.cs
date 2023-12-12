using System.Text.Json.Serialization;

namespace Messenger.Dto;

public class SendSmsRequest
{

    [JsonPropertyName("OriginationNumber")]
    public string OriginationNumber { get; set; }

    [JsonPropertyName("DestinationNumber")]
    public string DestinationNumber { get; set; }

    [JsonPropertyName("Message")]
    public string Message { get; set; }
}