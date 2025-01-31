using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoProject.WebUI.Utility
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }


        public List<int> count { get; set; }
    }
}