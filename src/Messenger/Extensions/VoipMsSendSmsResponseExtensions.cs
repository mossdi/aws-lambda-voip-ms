using System.Text.Json;
using Messenger.Dto;

namespace Messenger.Extensions;

public static class VoipMsSendSmsResponseExtensions
{
    public static VoipMsSendSmsResponse FromSendSmsResponse(
        this VoipMsSendSmsResponse voipMsSendSmsResponse, 
        SendSmsResponse sendSmsResponse)
    {
        return JsonSerializer.Deserialize<VoipMsSendSmsResponse>(sendSmsResponse.Content)!;
    }
}