using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class PostCallbackApprovalTransactionResponseValue
    {
        [JsonProperty("responseStatus")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("response")]
        public string Response { get; set; }
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
        [JsonProperty("utrNo")]
        public string UtrNo { get; set; }
        
    }
}
