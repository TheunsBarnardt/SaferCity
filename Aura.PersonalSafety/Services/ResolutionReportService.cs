using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.ResolutionReport;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aura.PersonalSafety.Services
{
    public class ResolutionReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ResolutionReportService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public ResolutionReportService(IHttpClientFactory httpClientFactory, ILogger<ResolutionReportService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/panics/";
        }

        // Generate a Resolution Report
        public async Task<Response> GenerateResolutionReportAsync(string calloutId, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_baseUrl}{calloutId}/resolution-report", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GenerateResolutionReportAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
