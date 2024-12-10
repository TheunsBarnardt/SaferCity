using Aura.PersonalSafety.Models.Panics;
using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Callout
{
    public class Response
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
