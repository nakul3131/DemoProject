using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class LoginDetailTokenResponse
    {

        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("id")]
        public string LoginId { get; set; }
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("expiry")]
        public string Expiry { get; set; }
        [JsonProperty("authKey")]
        public string AuthenticationKey { get; set; }
    }
}
