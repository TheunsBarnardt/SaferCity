using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.ServerToken
{
    public class OkResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; }
    }
}
