using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;
using DemoProject.Domain.CustomEntities.CBS;
using DemoProject.Domain.CustomEntities.CBS.Vanghee;
using DemoProject.Services.Utility.CyberSecurity;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Utility.APIRequester
{
    public class VangheeCBSAPIRequester
    {
        private ICoreTransactionDetailRepository coreTransactionDetailRepository;

        CBSAccountCredential cbsVangheeAccountCredential = new CBSAccountCredential();

        LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
        GetBankIdResponse getBankIdResponse = new GetBankIdResponse();
        GetBeneficiaryDetailResponse getBeneficiaryDetailResponse = new GetBeneficiaryDetailResponse();
        AddBeneficiaryDetailResponse addBeneficiaryDetailResponse = new AddBeneficiaryDetailResponse();
        ProcessingFeeCalculationResponse processingFeeCalculationResponse = new ProcessingFeeCalculationResponse();
        PaymentTransferStatusResponse paymentTransferStatusResponse = new PaymentTransferStatusResponse();
        PostCallbackApprovalTransactionResponse postCallbackApprovalTransactionResponse = new PostCallbackApprovalTransactionResponse();
        VirtualAccountNumberResponse virtualAccountNumberResponse = new VirtualAccountNumberResponse();
        ECollectionInwardReportResponse eCollectionInwardReportResponse = new ECollectionInwardReportResponse();
        UPICollectionInwardReportResponse upiCollectionInwardReportResponse = new UPICollectionInwardReportResponse();
        InitiateQRTransactionResponse initiateQRTransactionResponse = new InitiateQRTransactionResponse();
        GetBeneficiaryNameResponse getBeneficiaryNameResponse = new GetBeneficiaryNameResponse();

        

        WebClient webClient = new WebClient();

        dynamic jsonEncryptedResponse ="";
        
        private byte[] jsonRequestArray;
        private byte[] finalAPIRequestArray;
       

        string apiKey;

        string loginAPIChecksum;
        string loginAPIValue;

        string jsonAPIRequest;
        string requestedAPIEncryptedChecksum;
        string requestedAPIEncryptedValue;

        string encryptedResponse ="";
        string encryptedResponseValue ="";
        string decryptedResponse ="";


        public VangheeCBSAPIRequester()
        {
            coreTransactionDetailRepository = DependencyResolver.Current.GetService<ICoreTransactionDetailRepository>();
            cbsVangheeAccountCredential = coreTransactionDetailRepository.GetCBSAccountCredentials("Vanghee");

            apiKey = cbsVangheeAccountCredential.APIKey;
            loginAPIChecksum = coreTransactionDetailRepository.GetLoginRequestChecksum();
            loginAPIValue = coreTransactionDetailRepository.GetLoginRequestEncryptedValue();
        }

        // API Number 3 - Login
        public LoginDetailResponse Login()
        {
            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";

            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", loginAPIChecksum},
                { "value", loginAPIValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Login API
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/login", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            loginDetailResponse = JsonConvert.DeserializeObject<LoginDetailResponse>(decryptedResponse);

            return loginDetailResponse;
        }

        // API Number 4 Get BANKID API
        public GetBankIdResponse GetBankId(BankIdDetailViewModel _bankIdDetailViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For GetBankId API
            Dictionary<string, object> bankIdDetailDictionry = new Dictionary<string, object>
            {
                {"ifscCode", _bankIdDetailViewModel.IFSCCode},
                {"accNo",_bankIdDetailViewModel.AccountNumber},
                {"compId", cbsVangheeAccountCredential.CompanyNumericId}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(bankIdDetailDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of GetBankId API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Get BankId API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/bank/get-bankid",jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            getBankIdResponse = JsonConvert.DeserializeObject<GetBankIdResponse>(decryptedResponse);

            return getBankIdResponse;
        }

        // API Number 5 Get Beneficiary Details of the Customer
        public GetBeneficiaryDetailResponse GetBeneficiaryDetail(string _beneficiaryCode)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();
            
            
            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey",  loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For GetBeneficiaryDetailsOfTheCustomer API
            Dictionary<string, object> beneficiaryDetailDictionry = new Dictionary<string, object>
            {
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"benCode", _beneficiaryCode}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(beneficiaryDetailDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Get Beneficiary Deatail API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Get Beneficiary Details API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/get-benbycode", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);
            
            getBeneficiaryDetailResponse = JsonConvert.DeserializeObject<GetBeneficiaryDetailResponse>(decryptedResponse);

            return getBeneficiaryDetailResponse;
        }

        // API Number 6 Add Beneficiary Detail
        public AddBeneficiaryDetailResponse AddBeneficiaryDetail (BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Add Beneficiary Detail API
            Dictionary<string, object> addBeneficiaryDetailDictionry = new Dictionary<string, object>
            {
                {"accNo", _beneficiaryDetailViewModel.AccountNumber},
                {"corpUsrId", _beneficiaryDetailViewModel.CustomerNumber}, 
                {"urnno", loginDetailResponse.loginDetailTokenResponse.LoginId},
                {"email", _beneficiaryDetailViewModel.EmailId},
                {"ifscCode", _beneficiaryDetailViewModel.IfscCode},
                {"mobile", _beneficiaryDetailViewModel.MobileNumber},
                {"name", _beneficiaryDetailViewModel.NameOfBeneficiary},
                {"vpa", _beneficiaryDetailViewModel.VirtualPrivateAddress},
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"benCode", ""}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(addBeneficiaryDetailDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Add Beneficiary Deatail API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Add Beneficiary API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/add-ben", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            addBeneficiaryDetailResponse = JsonConvert.DeserializeObject<AddBeneficiaryDetailResponse>(decryptedResponse);

            return addBeneficiaryDetailResponse;
        }

        // API Number 7 Processing Fee Calculation API
        public ProcessingFeeCalculationResponse ProcessingFeeCalculation (string _payMode, decimal _paymentAmount)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Processing Fee Calculation API
            Dictionary<string, object> processingFeeCalculationDictionry = new Dictionary<string, object>
            {
                {"custId", loginDetailResponse.loginDetailTokenResponse.LoginId},
                {"payMode", _payMode},
                { "category","PAYMENTS"},
                { "compId", cbsVangheeAccountCredential.CompanyNumericId},
                { "payAmount", _paymentAmount}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(processingFeeCalculationDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Processing Fee Calculation API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Processing Fee Calculation API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/calc-process-fee", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            processingFeeCalculationResponse = JsonConvert.DeserializeObject<ProcessingFeeCalculationResponse>(decryptedResponse);

            return processingFeeCalculationResponse;
        }

        // API Number 8 Initiate Txn-Authentication Service(NEFT/IMPS/RTGS/UPI) FROM CBS to VANGHEE
        public PaymentTransferStatusResponse PaymentTransferStatusResponse(CoreTransactionViewModel _coreTransactionViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Initiate Txn-Authentication Service API
            Dictionary<string, object> coreTransactionDictionry = new Dictionary<string, object>
            {
                //{"custId", loginDetailResponse.loginDetailTokenResponse.Token},
                //{"compId", cbsVangheeAccountCredential.CompanyNumericId},
                //{"custName", _coreTransactionViewModel.CustomerName},
                //{"benName", _coreTransactionViewModel.BeneficiaryName},
                //{"amount", _coreTransactionViewModel.Amount},
                //{"processingFee", _coreTransactionViewModel.ProcessingFee}, 
                //{"payMode", _coreTransactionViewModel.PaymentMode},
                //{"bankId", cbsVangheeAccountCredential.BankNumericId},
                //{"benCode", _coreTransactionViewModel.BeneficiaryCode},
                //{"benAccNo", _coreTransactionViewModel.BeneficiaryAccountNumber},
                //{"remarks", _coreTransactionViewModel.Remark},
                //{"src","CBS001"},
                //{"recId", _coreTransactionViewModel.TransactionNumber},
                //{"memberMobile", _coreTransactionViewModel.MobileNumber},
                //{"callbackurl", "http://cbs-callbackurl.com/paymentverify"}
                {"custId", loginDetailResponse.loginDetailTokenResponse.LoginId},
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"custName", "Pradip Pratap Ghanwat"},
                {"benName", "Pradip Pratap Ghanwat"},
                {"amount", "10000.00"},
                {"processingFee", "2.36"},
                {"payMode", "IMPS"},
                {"bankId", cbsVangheeAccountCredential.BankNumericId},
                {"benCode", "7541176"},
                {"benAccNo", "140905501038"},
                {"remarks", "Trans"},
                {"src","CBS001"},
                {"recId", "P77777"},
                {"memberMobile", "9975952375"},
                {"callbackurl", "http://cbs-callbackurl.com/paymentverify"}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(coreTransactionDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Initiate Txn-Authentication Service API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Initiate Txn-Authentication Service API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/reqauth-payment", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            paymentTransferStatusResponse = JsonConvert.DeserializeObject<PaymentTransferStatusResponse>(decryptedResponse);

            return paymentTransferStatusResponse;
        }

        // API Number 9 Post Callback Approval FROM CBS to VANGHEE
        public PostCallbackApprovalTransactionResponse PostCallbackApprovalTransactionResponse (PostCallbackApprovalTransactionViewModel _postCallbackApprovalTransactionViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Post Callback Approval API
            Dictionary<string, object> postCallbackApprovalTransactionDictionry = new Dictionary<string, object>
            {
                //{"recId", _postCallbackApprovalTransactionViewModel.TransactionNumber},
                //{"uniqueId", _postCallbackApprovalTransactionViewModel.UniqueId},
                //{"amount", _postCallbackApprovalTransactionViewModel.TransactionAmount},
                //{"status", _postCallbackApprovalTransactionViewModel.Status},
                //{"approvalcode", _postCallbackApprovalTransactionViewModel.ApprovalCode},
                //{"compId", cbsVangheeAccountCredential.CompanyNumericId},
                //{"cryptoCode", "MTY0NF8xMfMjg5MTlfNTM1MMTY0NF8xMfMjg5MTlfNTM1MQ==="},
                //{"filler1", _postCallbackApprovalTransactionViewModel.Filler1},
                //{"filler2", _postCallbackApprovalTransactionViewModel.Filler2},
                //{"filler3", _postCallbackApprovalTransactionViewModel.Filler3},
                //{"filler4", _postCallbackApprovalTransactionViewModel.Filler4},
                //{"filler5", _postCallbackApprovalTransactionViewModel.Filler5},
                //{"filler6", _postCallbackApprovalTransactionViewModel.Filler6},

                {"recId", "P77777"},
                {"uniqueId", "6476410"},
                {"amount", "10000"},
                {"status", "PAY-APPROVED"},
                {"approvalcode","1001"},
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"cryptoCode", "MTY0NF8xMfMjg5MTlfNTM1MMTY0NF8xMfMjg5MTlfNTM1MQ==="},
                {"filler1"," FUTURE USE "},
                {"filler2"," FUTURE USE"},
                {"filler3"," FUTURE USE "},
                {"filler4"," FUTURE USE "},
                {"filler5"," FUTURE USE "},
                {"filler6"," FUTURE USE "}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(postCallbackApprovalTransactionDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Post Callback Approval API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Post Callback Approval API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/verifyauth-payment", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            postCallbackApprovalTransactionResponse = JsonConvert.DeserializeObject<PostCallbackApprovalTransactionResponse>(decryptedResponse);

            return postCallbackApprovalTransactionResponse;
        }

        // API Number 10 APPROVAL CALL BACK From Vanghee to CBS/ASP SYSTEM


        // API Number 11 Payment Transfer Status API
        public PaymentTransferStatusResponse PaymentTransferStatus(string _transactionId)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For PaymentTransferStatus API
            Dictionary<string, object> paymentTransferStatusDictionry = new Dictionary<string, object>
            {
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"txnId", _transactionId}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(paymentTransferStatusDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Payment Transfer Status Response API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Get PaymentTransferStatusResponse API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/pay/status", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            paymentTransferStatusResponse = JsonConvert.DeserializeObject<PaymentTransferStatusResponse>(decryptedResponse);

            return paymentTransferStatusResponse;
        }

        // API Number 12 Create a Virtual Account Number API
        public VirtualAccountNumberResponse CreateVirtualAccountNumber (VirtualAccountDetailViewModel _virtualAccountDetailViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Create a Virtual Account Number API
            Dictionary<string, object> virtualAccountNumberDictionry = new Dictionary<string, object>
            {
                {"accNo", "140905501011"},
                {"accStatus", "true"},
                {"accType", "CASA"},
                {"cbCustId", "P1111"},
                {"custName", "Ganesh Ravi Shinde"},
                {"memberId", "2"},
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"eCode", "SHRC"},
                {"memberStatus", "Active"},
                {"mobNo", "9975952375"}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(virtualAccountNumberDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Create a Virtual Account Number API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Create a Virtual Account Number API API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/cust/master", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            virtualAccountNumberResponse = JsonConvert.DeserializeObject<VirtualAccountNumberResponse>(decryptedResponse);

            return virtualAccountNumberResponse;

        }

        // API Number 13 Get Ecollection Inward Report
        public ECollectionInwardReportResponse ECollectionInwardReport(bool _isIncremental, DateTime _payDate)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Get Ecollection Inward Report API
            Dictionary<string, object> eCollectionInwardReportDictionry = new Dictionary<string, object>
            {
                //{"compId", cbsVangheeAccountCredential.CompanyNumericId},
                //{"incremental", _isIncremental},
                //{"payDate", _payDate},
                //{"urn", cbsVangheeAccountCredential.UniqueRegistrationNumber},
                //{"vaggrname", "VANGHE"},
                //{"code", cbsVangheeAccountCredential.ECollectionCode},

                //{"compId","115695"},
                //{"incremental","false"},
                //{"payDate","10/10/2022"},
                //{"urn",loginDetailResponse.loginDetailTokenResponse.LoginId},
                //{"vaggrname","VANGHE"},
                //{"code","SHRC" },

                {"compId","115695"},
                {"incremental","false"},
                {"payDate","22/09/2022"},
                {"urn","OTAwM18xMfOjAxMThfNzE4MA==="},
                {"vaggrname","VANGHE"},
                {"code","SHRC"},



            };

            jsonAPIRequest = JsonConvert.SerializeObject(eCollectionInwardReportDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Get Ecollection Inward Report API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Get ECollection Report API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/mis", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            eCollectionInwardReportResponse  = JsonConvert.DeserializeObject<ECollectionInwardReportResponse>(decryptedResponse);

            return eCollectionInwardReportResponse;
        }

        // API Number 14 Get UPI Collection Inward Report
        public UPICollectionInwardReportResponse UPICollectionInwardReport(UPICollectionInwardReportViewModel _upiCollectionInwardReportViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Get UPI Collection Inward Report API
            Dictionary<string, object> upiCollectionInwardReportDictionry = new Dictionary<string, object>
            {


                //{"compId", cbsVangheeAccountCredential.CompanyNumericId},
                //{"incremental", _upiCollectionInwardReportViewModel.IsIncremental},
                //{"payDate", _upiCollectionInwardReportViewModel.PayDate},
                //{"urn", cbsVangheeAccountCredential.UniqueRegistrationNumber},
                //{"vaggrname", "VANGHE"},
                //{"vpacode", _upiCollectionInwardReportViewModel.VirtualPaymentAddress},
                //{"filler1", _upiCollectionInwardReportViewModel.Filler1},
                //{"filler2", _upiCollectionInwardReportViewModel.Filler2},


                    {"compId","115695"},
                    {"incremental","false"},
                    {"payDate","22/09/2022"},
                    {"urn","OTE5M18xMfOjAxMThfMjkzNQ==="},
                    {"vaggrname","VANGHE"},
                    {"vpacode","SHRC.20000086@icici" },
                    {"filler1 ", " "}, /// Future use,
                    {"filler2 "," "} // Future Use
            };

            jsonAPIRequest = JsonConvert.SerializeObject(upiCollectionInwardReportDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Get UPI Collection Inward Report API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // UPI Collection API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/api/cb/misupicollection", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            upiCollectionInwardReportResponse = JsonConvert.DeserializeObject<UPICollectionInwardReportResponse>(decryptedResponse);

            return upiCollectionInwardReportResponse;
        }

        // API Number 15 Scan QR - Initiate Transaction
        public InitiateQRTransactionResponse InitiateQRTransaction(InitiateQRTransactionViewModel _initiateQRTransactionViewModel)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Scan QR - Initiate Transaction API
            Dictionary<string, object> initiateQRTransactionDictionry = new Dictionary<string, object>
            {
                //{"custId", loginDetailResponse.loginDetailTokenResponse.LoginId},
                //{"compId", cbsVangheeAccountCredential.CompanyNumericId},
                //{"custName", _initiateQRTransactionViewModel.CustomerName},
                //{"benName", _initiateQRTransactionViewModel.BeneficiaryName},
                //{"amount", _initiateQRTransactionViewModel.Amount},
                //{"processingFee", _initiateQRTransactionViewModel.ProcessingFee},
                //{"payMode", _initiateQRTransactionViewModel.PaymentMode},
                //{"bankId", cbsVangheeAccountCredential.BankNumericId},
                //{"vpa", _initiateQRTransactionViewModel.VirtualPaymentAddress},
                //{"remarks", _initiateQRTransactionViewModel.Remark},
                //{"src","CBS001"},
                //{"recId", _initiateQRTransactionViewModel.TransactionNumber},
                //{"memberMobile", _initiateQRTransactionViewModel.MobileNumber},
                //{"callbackurl", "http://cbs-callbackurl.com/paymentverify"}


                {"custId", loginDetailResponse.loginDetailTokenResponse.LoginId}, // Replace with Your Id received from Login token API
                {"compId", "115695"},
                {"custName", "Pradip Pratap Ghanwat"},
                {"benName", "Pradip Pratap Ghanwat"}, // Name Scanned from QR code
                {"amount", "10000.00"},
                {"processingFee","2.36"}, //Processing Fee are calculated against each Payment Slab shared with CBS.
                {"payMode", "UPI"}, //UPI
                {"bankId", "1815233"},
                {"vpa", "abc@ICIC"}, //VPA Address Scanned from QR code
                {"remarks", "test payment"},
                {"src","CBS001"},
                {"recId","U77777"}, // UNIQUE TXN ID GENERATED AT CBS END.
                {"memberMobile", "7012122525"}, // 10 Digit Mobile number Member
                {"callbackurl", "http://cbs-callbackurl.com/paymentverify"}

            };

            jsonAPIRequest = JsonConvert.SerializeObject(initiateQRTransactionDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Scan QR - Initiate Transaction API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Initiate QR Transaction API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/qr-reqauth-payment", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            initiateQRTransactionResponse  = JsonConvert.DeserializeObject<InitiateQRTransactionResponse>(decryptedResponse);

            return initiateQRTransactionResponse;
        }

        // API Number 16 Get The Beneficiary Name API
        public GetBeneficiaryNameResponse GetBeneficiaryName(string _virtualPaymentAddress)
        {
            // Required Login For Every Transaction To Get Login Id And token
            loginDetailResponse = Login();

            // Headers Of Request
            webClient.Headers["content-type"] = "application/json";
            webClient.Headers.Add("authKey", loginDetailResponse.loginDetailTokenResponse.AuthenticationKey.ToString());
            webClient.Headers.Add("Authorization", "Bearer " + loginDetailResponse.loginDetailTokenResponse.Token.ToString());

            // Required Inputs For Get The Beneficiary Name API API
            Dictionary<string, object> beneficiaryNameDictionry = new Dictionary<string, object>
            {
                {"compId", cbsVangheeAccountCredential.CompanyNumericId},
                {"bankId", cbsVangheeAccountCredential.BankNumericId},
                {"vpaAddress", _virtualPaymentAddress}
            };

            jsonAPIRequest = JsonConvert.SerializeObject(beneficiaryNameDictionry);

            requestedAPIEncryptedChecksum = VangheeCoreTransactionCrypto.CreateChecksum(jsonAPIRequest);

            requestedAPIEncryptedValue = VangheeCoreTransactionCrypto.EncryptAndEncode(jsonAPIRequest);

            // Parameters (api, checkSum, value) Of Get The Beneficiary Name API API
            Dictionary<string, object> jsonRequestDictionary = new Dictionary<string, object>
            {
                { "apiKey", apiKey},
                { "checkSum", requestedAPIEncryptedChecksum},
                { "value", requestedAPIEncryptedValue}
            };

            // array of tinyint i.e. byte[]
            jsonRequestArray = Encoding.Default.GetBytes(JsonConvert.SerializeObject(jsonRequestDictionary, Formatting.Indented));

            // Get Beneficiary Name API URL
            finalAPIRequestArray = webClient.UploadData("https://vangheeuat.com/aesvgnerp/upi-validation", jsonRequestArray);

            encryptedResponse = Encoding.Default.GetString(finalAPIRequestArray);

            jsonEncryptedResponse = JsonConvert.DeserializeObject<object>(encryptedResponse);

            encryptedResponseValue = jsonEncryptedResponse.value;

            decryptedResponse = VangheeCoreTransactionCrypto.DecodeAndDecrypt(encryptedResponseValue);

            getBeneficiaryNameResponse = JsonConvert.DeserializeObject<GetBeneficiaryNameResponse>(decryptedResponse);

            return getBeneficiaryNameResponse;
        }

    }
}
