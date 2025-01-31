using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class ProcessingFeeCalculationResponse
    {
        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("results")]
        public ProcessingFeeCalculationResponseResult ProcessingFeeCalculationResponseResult { get; set; }
    }
}
