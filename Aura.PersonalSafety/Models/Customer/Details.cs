using System.Text.Json.Serialization;

namespace Aura.PersonalSafety.Models.Customer
{
    public class Details
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("duressCode")]
        public string DuressCode { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("vehicleMake")]
        public string VehicleMake { get; set; }

        [JsonPropertyName("vehicleModel")]
        public string VehicleModel { get; set; }

        [JsonPropertyName("vehicleColour")]
        public string VehicleColour { get; set; }

        [JsonPropertyName("vehicleLicensePlate")]
        public string VehicleLicensePlate { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("businessName")]
        public string BusinessName { get; set; }

        [JsonPropertyName("hasMedicalAid")]
        public bool HasMedicalAid { get; set; }

        [JsonPropertyName("medicalConditions")]
        public string MedicalConditions { get; set; }

        [JsonPropertyName("medications")]
        public string Medications { get; set; }

        [JsonPropertyName("allergies")]
        public string Allergies { get; set; }

        [JsonPropertyName("medicalAidNumber")]
        public string MedicalAidNumber { get; set; }

        [JsonPropertyName("medicalAidScheme")]
        public string MedicalAidScheme { get; set; }

        [JsonPropertyName("nextOfKinName")]
        public string NextOfKinName { get; set; }

        [JsonPropertyName("nextOfKinNumber")]
        public string NextOfKinNumber { get; set; }

        [JsonPropertyName("bloodtype")]
        public string BloodType { get; set; }

        [JsonPropertyName("familyDoctorPhoneNumber")]
        public string FamilyDoctorPhoneNumber { get; set; }

        [JsonPropertyName("medicalOther")]
        public string MedicalOther { get; set; }
    }
}
