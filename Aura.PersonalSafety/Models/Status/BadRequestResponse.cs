using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Status
{
    public class BadRequestResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
