using System.Text;
using Aura.PersonalSafety.Config;
using Aura.PersonalSafety.Extensions;
using Aura.PersonalSafety.Models.Leads;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aura.PersonalSafety.Services
{
    public class LeadService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LeadService> _logger;
        private readonly ServiceOptions _serviceOptions;
        private readonly string _baseUrl;

        public LeadService(IHttpClientFactory httpClientFactory, ILogger<LeadService> logger, IOptions<ServiceOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _serviceOptions = options.Value;
            _logger = logger;
            _baseUrl = $"{_serviceOptions.BaseUrl}/v2/leads/";
        }

        /// <summary>
        /// Submits a new lead.
        /// </summary>
        public async Task<Response> SubmitLeadAsync(Request request, CancellationToken cancellationToken = default)
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
                _logger.LogError("Error in SubmitLeadAsync: {Message}", ex.Message);
                throw;
            }
        }
    }
}
