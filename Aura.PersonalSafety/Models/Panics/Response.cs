using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Panics
{
    public class Response
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("panic")]
        public Details Panic { get; set; }
    }
}
