using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Leads
{
    public class Response
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("leadId")]
        public string LeadId { get; set; }
    }
}
