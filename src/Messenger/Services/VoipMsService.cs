using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Messenger.Dto;
using Messenger.Extensions;

namespace Messenger.Services;

public class VoipMsService
{

    private readonly HttpClient client;

    public VoipMsService(IHttpClientFactory httpClientFactory)
    {
        client = httpClientFactory.CreateClient("client");
    }

    public async Task<SendSmsResponse> SendSms(SendSmsRequest sendSmsRequest)
    {
        var request = new
        {
            did = sendSmsRequest.OriginationNumber,
            dst = sendSmsRequest.DestinationNumber,
            message = sendSmsRequest.Message
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("?method=sendSMS", request);
        
        return new SendSmsResponse().FromHttpResponseMessage(response); 
    }
}