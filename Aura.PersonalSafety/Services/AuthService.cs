using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Auth;
using Aura.PersonalSafety.Models.ServerToken;
using Aura.PersonalSafety.Models.Status;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneOf;
using Serilog;
using System.Net;
using System.Text;

namespace Aura.PersonalSafety.Services
{
    internal class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthService> _logger;
        private readonly string _baseUrl;
        private readonly ServiceOptions _serviceOptions;
        private string _cachedServerToken;
        private DateTime _tokenServerExpirationTime;
        private string _cachedCalloutToken;
        private DateTime _tokenCalloutExpirationTime;

        internal AuthService(IHttpClientFactory httpClientFactory, ILogger<AuthService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;

            _logger = logger;
            _serviceOptions = options.Value;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/oauth/";
        }

        internal async Task<string> GetServerTokenAsync()
        {

            if (_cachedServerToken == null || DateTime.UtcNow >= _tokenServerExpirationTime)
            {
                var response = await ServerTokenAsync(new Request()
                {
                    ClientId = _serviceOptions.ClientId,
                    ClientSecret = _serviceOptions.ClientSecret,
                });

                if (response.IsT0)
                {
                    _cachedServerToken = response.AsT0?.AccessToken;
                    _tokenServerExpirationTime = DateTime.UtcNow.AddSeconds(response.AsT0?.ExpiresIn ?? 0); // Assume token expiration time comes with the response
                }
                else
                {
                    Log.Error(response.AsT1.Error);
                }

            }

            return _cachedServerToken;
        }

        internal async Task<string> GetCalloutTokenAsync()
        {
            // If the token has expired, get a new one
            if (_cachedCalloutToken == null || DateTime.UtcNow >= _tokenCalloutExpirationTime)
            {
                var response = await CalloutTokenAsync(new Request()
                {
                    ClientId = _serviceOptions.ClientId,
                    ClientSecret = _serviceOptions.ClientSecret,
                });

                if (response.IsT0)
                {
                    _cachedCalloutToken = response.AsT0?.AccessToken;
                    _tokenCalloutExpirationTime = DateTime.UtcNow.AddSeconds(response.AsT0?.ExpiresIn ?? 0); // Assume token expiration time comes with the response
                }
                else
                {
                    Log.Error(response.AsT1.Error);
                }
            }

            return _cachedCalloutToken;
        }

        /// <summary>
        /// Fetches an authorization token using a password grant - Full access to AURA API.
        /// </summary>
        internal async Task<OneOf<OkResponse, BadRequestResponse>> ServerTokenAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}token", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.ReadJsonAsync<OkResponse>();
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return await response.ReadJsonAsync<BadRequestResponse>();
                }

                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ServerTokenAsync: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Fetches a callout token for limited access to panic-related endpoints.
        /// </summary>
        internal async Task<OneOf<OkResponse, BadRequestResponse>> CalloutTokenAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}calloutToken", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.ReadJsonAsync<OkResponse>();
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return await response.ReadJsonAsync<BadRequestResponse>();
                }

                throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CalloutTokenAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
