using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class GetBankIdResponseResult
    {
        [JsonProperty("companyId")]
        public string CompanyId { get; set; }
        [JsonProperty("bankId")]
        public string BankId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
