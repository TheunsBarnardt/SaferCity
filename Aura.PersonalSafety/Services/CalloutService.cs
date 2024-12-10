using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Panics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aura.PersonalSafety.Services
{
    public class CalloutService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CalloutService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public CalloutService(IHttpClientFactory httpClientFactory, ILogger<CalloutService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/panics/";
        }


        // Callout Created
        public async Task<Models.Callout.Response> CreateCalloutAsync(Models.Callout.Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}create", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CreateCalloutAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Responder Dispatched
        public async Task<Models.Callout.Response> DispatchResponderAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/dispatched", null, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in DispatchResponderAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Responder Arrived
        public async Task<Models.Callout.Response> ResponderArrivedAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/arrived", null, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ResponderArrivedAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Responder Completed
        public async Task<Models.Callout.Response> ResponderCompletedAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/completed", null, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ResponderCompletedAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Callout Closed
        public async Task<Models.Callout.Response> CloseCalloutAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/close", null, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CloseCalloutAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Update Callout Location
        public async Task<Models.Callout.Response> UpdateCalloutLocationAsync(string calloutId, Location request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/location", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in UpdateCalloutLocationAsync: {Message}", ex.Message);
                throw;
            }
        }



        // 'Soft Cancel' a Callout
        public async Task<Models.Callout.Response> SoftCancelCalloutAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PutAsync($"{_baseUrl}{calloutId}/soft-cancel", null, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SoftCancelCalloutAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Return a Specific Callout
        public async Task<Models.Callout.Response> GetCalloutAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}{calloutId}", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.Callout.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetCalloutAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Return All Active Callouts for a Customer
        public async Task<List<Models.Callout.Response>> GetActiveCalloutsAsync(string customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}customer/{customerId}/active", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<List<Models.Callout.Response>>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetActiveCalloutsAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
