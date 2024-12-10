namespace Aura.Services
{
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Aura.Config;
    using global::Aura.Extensions;
    using global::Aura.Models.Status;
    using global::Aura.Models.Users;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using OneOf;

    namespace Aura.Services
    {
        public class CustomerService
        {
            private readonly IHttpClientFactory _httpClientFactory;
            private readonly ILogger<CustomerService> _logger;
            private readonly ServiceOptions _serviceOptions;
            private readonly string _baseUrl;

            public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger,IOptions<ServiceOptions> options)
            {
                _httpClientFactory = httpClientFactory;
                _logger = logger;
                _serviceOptions = options.Value;
                _baseUrl = $"{_serviceOptions.BaseUrl}/v2/customers/";
            }

            /// <summary>
            /// Register a new customer with an active subscription
            /// </summary>
            public async Task<OneOf<Response, BadRequestResponse>> RegisterCustomerWithSubscriptionAsync(Request request,  CancellationToken cancellationToken = default)
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
                    _logger.LogError("Error in RegisterCustomerWithSubscriptionAsync: {Message}", ex.Message);
                    throw;
                }
            }

            /// <summary>
            /// Register a New Customer Without a Subscription
            /// </summary>
            public async Task<OneOf<Response, BadRequestResponse>> RegisterCustomerWithoutSubscriptionAsync(Details customer, CancellationToken cancellationToken = default)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();

                    var request = new { customer };
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
                    _logger.LogError("Error in RegisterCustomerWithoutSubscriptionAsync: {Message}", ex.Message);
                    throw;
                }
            }

            /// <summary>
            /// Edit an Existing Customer's Profile
            /// </summary>
            public async Task<OneOf<Response, BadRequestResponse>> UpdateCustomerAsync(string customerId, Details customer, CancellationToken cancellationToken = default)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();

                    var json = JsonConvert.SerializeObject(customer);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync($"{_baseUrl}{customerId}", content, cancellationToken);

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
                    _logger.LogError("Error in UpdateCustomerAsync: {Message}", ex.Message);
                    throw;
                }
            }

            /// <summary>
            /// Fetches a customer's profile by customerId.
            /// </summary>
            public async Task<Response> GetCustomerByIdAsync(string customerId, CancellationToken cancellationToken = default)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();

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

            /// <summary>
            /// Soft deletes a customer's profile by customerId.
            /// </summary>
            public async Task<bool> DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();

                    var response = await httpClient.DeleteAsync($"{_baseUrl}{customerId}", cancellationToken);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error in DeleteCustomerAsync: {Message}", ex.Message);
                    throw;
                }
            }
        }
    }

}
