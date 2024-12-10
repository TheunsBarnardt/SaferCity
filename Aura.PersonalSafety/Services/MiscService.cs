using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Misc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aura.PersonalSafety.Services
{
    public class MiscService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MiscService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public MiscService(IHttpClientFactory httpClientFactory, ILogger<MiscService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/misc/";
        }

        /// <summary>
        /// Fetches status of the API or miscellaneous information.
        /// </summary>
        public async Task<Response> GetStatusAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetAsync($"{_baseUrl}{endpoint}", cancellationToken);

                response.EnsureSuccessStatusCode();
                return await response.ReadJsonAsync<Response>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStatusAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
