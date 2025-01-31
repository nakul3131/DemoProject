
using System;
using Newtonsoft.Json;
namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class GetBeneficiaryDetailResponseResult
    {
        [JsonProperty("benCode")]
        public string BeneficiaryCode { get; set; }
        [JsonProperty("accNo")]
        public string AccountNumber { get; set; }
        [JsonProperty("ifsc")]
        public string IFSCCode { get; set; }
        [JsonProperty("name")]
        public string CustomerName { get; set; }
        [JsonProperty("email")]
        public string EmailId { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }
        [JsonProperty("successOrErrorMsg")]
        public string SuccessOrErrorMsg { get; set; }
    }
}
