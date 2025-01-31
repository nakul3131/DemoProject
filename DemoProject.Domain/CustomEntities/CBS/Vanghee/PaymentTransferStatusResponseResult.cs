using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class PaymentTransferStatusResponseResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("txnId")]
        public string TransactionId { get; set; }
        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }
        [JsonProperty("UTR")]
        public string UtrNo { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
}
