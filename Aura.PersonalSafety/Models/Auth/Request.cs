using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Auth
{
    public class Request
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
