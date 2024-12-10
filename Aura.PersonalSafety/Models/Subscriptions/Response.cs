using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Subscriptions
{
    public class Response
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("subscriptionTypeId")]
        public int SubscriptionTypeId { get; set; }

        [JsonPropertyName("validFrom")]
        public string ValidFrom { get; set; }

        [JsonPropertyName("validTo")]
        public string ValidTo { get; set; }
    }
}
