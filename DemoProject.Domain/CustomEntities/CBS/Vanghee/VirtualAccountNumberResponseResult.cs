using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class VirtualAccountNumberResponseResult
    {
        [JsonProperty("virAccNo")]
        public string AccountNumber { get; set; }
        [JsonProperty("virvpa")]
        public string VirtualPaymentAddress { get; set; }
        [JsonProperty("memberId")]
        public string MemberId { get; set; }
        [JsonProperty("custName")]
        public string CustomerName { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
