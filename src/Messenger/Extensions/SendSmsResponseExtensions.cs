using System.Linq;
using System.Net.Http;
using Messenger.Dto;

namespace Messenger.Extensions;

public static class SendSmsResponseExtensions
{
    public static SendSmsResponse FromHttpResponseMessage(this SendSmsResponse response, HttpResponseMessage httpResponseMessage)
    {
        response.Status = (int)httpResponseMessage.StatusCode;
        response.Headers = httpResponseMessage.Headers.ToDictionary(header => header.Key, header => string.Join(";", header.Value));
        response.Content = httpResponseMessage.Content.ReadAsStringAsync().Result;

        return response;
    }
}