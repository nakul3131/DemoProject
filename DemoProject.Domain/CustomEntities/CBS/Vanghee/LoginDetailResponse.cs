using Newtonsoft.Json;
namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    
    public class LoginDetailResponse
    {
        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }
        [JsonProperty("successOrErrorMsg")]
        public string SuccessOrErrorMsg { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("results")]
        public LoginDetailTokenResponse loginDetailTokenResponse{ get; set; }
    }
}
