using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aura.PersonalSafety.Services
{
    public class ImageMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ImageMapService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public ImageMapService(IHttpClientFactory httpClientFactory, ILogger<ImageMapService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/panics/";
        }


        // Generate a Resolution Report
        public async Task<Models.ResolutionReport.Response> GenerateResolutionReportAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}{calloutId}/resolution-report", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.ResolutionReport.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GenerateResolutionReportAsync: {Message}", ex.Message);
                throw;
            }
        }

        // Get Static Image Map with Customer and Responder Markers
        public async Task<Models.ImageMap.Response> GetStaticImageMapAsync(string customerId, string responderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}static-map/{customerId}/{responderId}", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Models.ImageMap.Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStaticImageMapAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
