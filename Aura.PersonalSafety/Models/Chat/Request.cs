using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Chat
{
    public class Request
    {
        [JsonPropertyName("calloutId")]
        public string CalloutId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
