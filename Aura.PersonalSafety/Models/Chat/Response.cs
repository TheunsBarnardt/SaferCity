using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Chat
{
    public class Response
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
    }
}
