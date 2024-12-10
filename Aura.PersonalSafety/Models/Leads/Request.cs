using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Leads
{
    public class Request
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
