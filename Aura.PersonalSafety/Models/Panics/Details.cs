using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Panics
{
    public class Details
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
