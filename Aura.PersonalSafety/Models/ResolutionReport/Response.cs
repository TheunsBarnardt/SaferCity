using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.ResolutionReport
{
    public class Response
    {
        [JsonPropertyName("calloutId")]
        public string CalloutId { get; set; }

        [JsonPropertyName("resolution")]
        public string Resolution { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("resolvedAt")]
        public string ResolvedAt { get; set; }
    }
}
