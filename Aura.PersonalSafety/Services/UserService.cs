using System.Net;
using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Customer;
using Aura.PersonalSafety.Models.Status;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneOf;

namespace Aura.PersonalSafety.Services
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/customers/";
        }

        /// <summary>
        /// Registers a new customer.
        /// </summary>
        public async Task<OneOf<Response, BadRequestResponse>> RegisterCustomerAsync(Request request, string token, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                _logger.LogError("Error in RegisterCustomerAsync: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Fetches a customer by ID.
        /// </summary>
        public async Task<Response> GetCustomerByIdAsync(string customerId, string token, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"{_baseUrl}{customerId}", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetCustomerByIdAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
