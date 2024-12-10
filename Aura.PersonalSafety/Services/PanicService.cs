
using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Panics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aura.PersonalSafety.Services
{
    public class PanicService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PanicService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public PanicService(IHttpClientFactory httpClientFactory, ILogger<PanicService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/panics/";
        }

        /// <summary>
        /// Initiates a panic.
        /// </summary>
        public async Task<Response> InitiatePanicAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_baseUrl, content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in InitiatePanicAsync: {Message}", ex.Message);
                throw;
            }
        }


        // Send a Test Panic - SECURITY
        public async Task<Response> SendTestPanicSecurityAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}test/security", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SendTestPanicSecurityAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Send a Real Panic - SECURITY
        public async Task<Response> SendRealPanicSecurityAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}real/security", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SendRealPanicSecurityAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Send a Test Panic - MEDICAL
        public async Task<Response> SendTestPanicMedicalAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}test/medical", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SendTestPanicMedicalAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Send a Real Panic - MEDICAL
        public async Task<Response> SendRealPanicMedicalAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_baseUrl}real/medical", content, cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SendRealPanicMedicalAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
