using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Customer
{
    public class Response
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("customer")]
        public Details Customer { get; set; }
    }
}
