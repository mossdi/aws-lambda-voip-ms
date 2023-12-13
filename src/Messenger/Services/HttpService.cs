using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Messenger.Services;

public class HttpService: DelegatingHandler
{

    private readonly IConfiguration configuration;
    
    public HttpService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string uri = QueryHelpers.AddQueryString(request.RequestUri!.ToString(), new Dictionary<string, string>()
        {
            ["api_username"] = configuration.GetSection("VoipMsApi").GetSection("User").Value!,
            ["api_password"] = configuration.GetSection("VoipMsApi").GetSection("Password").Value!
        });

        request.RequestUri = new Uri(uri);

        return await base.SendAsync(request, cancellationToken);
    }
}