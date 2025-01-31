﻿using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class UPICollectionInwardReportResponse
    {
        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("message")]
        public UPICollectionInwardReportResponseMIS UPICollectionInwardReportResponseMis { get; set; }
    }
}
