using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Customer
{
    public class Request
    {
        [JsonPropertyName("customer")]
        public Details Customer { get; set; }

        [JsonPropertyName("subscription")]
        public Subscriptions.Details Subscription { get; set; }
    }
}
