using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class AddBeneficiaryResponseValueResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("benCode")]
        public string BeneficiaryCode { get; set; }
        [JsonProperty("corpUsrId")]
        public string CorporateUserId { get; set; }
    }
}
