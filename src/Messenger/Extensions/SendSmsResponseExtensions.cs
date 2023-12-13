using System.Net.Http;
using Messenger.Dto;

namespace Messenger.Extensions;

public static class SendSmsResponseExtensions
{
    public static SendSmsResponse FromHttpResponseMessage(this SendSmsResponse response, HttpResponseMessage httpResponseMessage)
    {
        response.Status = (int)httpResponseMessage.StatusCode;

        return response;
    }
}