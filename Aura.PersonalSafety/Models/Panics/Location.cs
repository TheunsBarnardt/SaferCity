using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Panics
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
