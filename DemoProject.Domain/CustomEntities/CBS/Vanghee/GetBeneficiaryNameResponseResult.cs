using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class GetBeneficiaryNameResponseResult
    {
        [JsonProperty("success")]
        public string Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("mobileAppData")]
        public string MobileAppData { get; set; }
        
    }
}
