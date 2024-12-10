using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Panics
{
    public class Request
    {
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("panicReason")]
        public string PanicReason { get; set; }
    }
}
