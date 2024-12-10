using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Misc
{
    public class Response
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}
