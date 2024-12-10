using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aura.PersonalSafety.Services
{
    public class ChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChatService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public ChatService(IHttpClientFactory httpClientFactory, ILogger<ChatService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/panics/";
        }


        // Send a Chat Message to Control Centre
        public async Task<Models.Chat.Response> SendChatMessageAsync(Models.Chat.Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}chat/send", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Chat.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SendChatMessageAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Receive a Chat Message from Control Centre
        public async Task<Models.Chat.Response> ReceiveChatMessageAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}{calloutId}/chat/receive", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Chat.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ReceiveChatMessageAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
