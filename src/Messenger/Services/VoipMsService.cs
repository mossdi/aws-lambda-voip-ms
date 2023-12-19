using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Messenger.Dto;
using Messenger.Extensions;
using Microsoft.Extensions.Configuration;

namespace Messenger.Services;

public class VoipMsService
{

    private readonly HttpClient client;

    public VoipMsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        client = httpClientFactory.CreateClient("client");
        client.BaseAddress = new Uri(configuration.GetSection("VoipMsApi").GetSection("BaseUri").Value!);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.GetSection("VoipMsApi").GetSection("Token")}");
    }

    public async Task<SendSmsResponse> SendSms(SendSmsRequest sendSmsRequest)
    {
        HttpResponseMessage response = await client
            .GetAsync($"/?method=sendSMS&did={sendSmsRequest.OriginationNumber}&dst={sendSmsRequest.DestinationNumber}&message={sendSmsRequest.Message}");
        
        return new SendSmsResponse().FromHttpResponseMessage(response); 
    }
}