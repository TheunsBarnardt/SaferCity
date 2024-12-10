using System.Text.Json.Serialization;
using Aura.PersonalSafety.Models.Panics;

namespace Aura.PersonalSafety.Models.ImageMap
{
    public class Response
    {
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("customerLocation")]
        public Location CustomerLocation { get; set; }

        [JsonPropertyName("responderLocation")]
        public Location ResponderLocation { get; set; }
    }
}
