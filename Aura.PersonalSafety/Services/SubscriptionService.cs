using System.Net;
using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Status;
using Aura.PersonalSafety.Models.Subscriptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneOf;

namespace Aura.PersonalSafety.Services
{
    public class SubscriptionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SubscriptionService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public SubscriptionService(IHttpClientFactory httpClientFactory, ILogger<SubscriptionService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/subscriptions/";
        }

        /// <summary>
        /// Creates or updates a subscription.
        /// </summary>
        public async Task<OneOf<Response, BadRequestResponse>> CreateOrUpdateSubscriptionAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_baseUrl, content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.ReadJsonAsync<Response>();
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return await response.ReadJsonAsync<BadRequestResponse>();
                }

                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CreateOrUpdateSubscriptionAsync: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Cancels a subscription.
        /// </summary>
        public async Task<bool> CancelSubscriptionAsync(string subscriptionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();


                var response = await httpClient.DeleteAsync($"{_baseUrl}{subscriptionId}", cancellationToken);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CancelSubscriptionAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
