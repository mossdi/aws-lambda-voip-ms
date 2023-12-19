using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Messenger.Dto;
using Messenger.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Messenger
{

    public class Function
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return await serviceProvider.GetService<App>()
                .Run(JsonSerializer.Deserialize<SendSmsRequest>(request.Body));
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<App>()    
                .AddTransient<HttpService>()
                .AddTransient<VoipMsService>();

            serviceCollection
                .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());

            serviceCollection
                .AddHttpClient("client")
                .AddHttpMessageHandler<HttpService>();
        }
    }

    public class App
    {

        private VoipMsService voipMsService;

        public App(VoipMsService voipMsService)
        {
            this.voipMsService = voipMsService;
        }

        public async Task<APIGatewayProxyResponse> Run(SendSmsRequest request)
        {
            SendSmsResponse response = await voipMsService.SendSms(request);

            return new APIGatewayProxyResponse
            {
                StatusCode = response.Status,
                Headers = response.Headers,
                Body = response.Content,
            };
        }
    }
}
