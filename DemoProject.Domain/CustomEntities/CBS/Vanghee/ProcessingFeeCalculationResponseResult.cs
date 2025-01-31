using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class ProcessingFeeCalculationResponseResult
    {
        [JsonProperty("payMode")]
        public string PayMode { get; set; }
        [JsonProperty("compId")]
        public string CompanyId { get; set; }
        [JsonProperty("payAmount")]
        public string PayAmount { get; set; }
        [JsonProperty("processingFee")]
        public string ProcessingFee { get; set; }
    }
}
