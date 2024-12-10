using Aura.PersonalSafety.Models.Panics;
using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Callout
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
