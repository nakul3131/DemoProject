using Newtonsoft.Json;

namespace DemoProject.Domain.CustomEntities.CBS.Vanghee
{
    public class UPICollectionInwardReportResponseMISResult
    {

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("mis")]
        public UPICollectionInwardReportResponseMIS[] eCollectionInwardReportResponseMIS { get; set; }

    }

    public class UPICollectionInwardReportResponseMIS
    {
        [JsonProperty("receiptId")]
        public string ReceiptId { get; set; }
        [JsonProperty("mode")]
        public string PaymentMode { get; set; }
        [JsonProperty("amt")]
        public string Amount { get; set; }
        [JsonProperty("payeeAccNo")]
        public string PayeeAccountNumber { get; set; }
        [JsonProperty("payeeifsc")]
        public string PayeeIFSCCode { get; set; }
        [JsonProperty("payeeName")]
        public string PayeeName { get; set; }
        [JsonProperty("payeePayDate")]
        public string PayeePayDate { get; set; }
        [JsonProperty("payeeRemarks")]
        public string PayeeRemarks { get; set; }
        [JsonProperty("bankTxnno")]
        public string BankTransactionNumber { get; set; }
        [JsonProperty("uniqueRegistrationNumber")]
        public string UniqueRegistrationNumber { get; set; }
        [JsonProperty("VirtualACNo")]
        public string VirtualCustomerAccountNumber { get; set; }
        [JsonProperty("vaggrName")]
        public string VaggrName { get; set; }
        [JsonProperty("uniqueTransactionReference")]
        public string UniqueTransactionReference { get; set; }
        [JsonProperty("vaggrid")]
        public string Vaggrid { get; set; }
        [JsonProperty("CustomerAccountNo")]
        public string CustomerAccountNumber { get; set; }
    }
}
