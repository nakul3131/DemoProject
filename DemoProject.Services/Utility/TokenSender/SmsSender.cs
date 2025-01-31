using System;
using System.Net;
using System.Web;
using System.Collections.Specialized;
using System.Web.Mvc;
using DemoProject.Domain.CustomEntities;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Abstract.SMS;
using DemoProject.Services.Abstract.Account.Customer;

namespace DemoProject.Services.Utility.SmsSender
{
    public class SmsSender
    {
        private readonly IAuthProviderRepository authProviderRepository;
        private readonly ICustomerDetailRepository customerDetailRepository;
        private readonly ISMSDetailRepository smsDetailRepository;
        private readonly IUserAuthenticationTokenRepository userAuthenticationTokenRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly ISMSRepository smsRepository;
        private readonly short userProfilePrmKey;

        SmsAccountCredential smsAccountCredential = new SmsAccountCredential();

        SmsResponse smsResponse = new SmsResponse();

        public SmsSender()
        {
            authProviderRepository = DependencyResolver.Current.GetService<IAuthProviderRepository>();
            customerDetailRepository = DependencyResolver.Current.GetService<ICustomerDetailRepository>();
            securityDetailRepository = DependencyResolver.Current.GetService<ISecurityDetailRepository>();
            smsRepository = DependencyResolver.Current.GetService<ISMSRepository>();
            smsDetailRepository = DependencyResolver.Current.GetService<ISMSDetailRepository>();
            userAuthenticationTokenRepository = DependencyResolver.Current.GetService<IUserAuthenticationTokenRepository>();

            userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
        }

        public SmsResponse SendAccountOpeningSms(long _customerAccountPrmKey)
        {
            // Get Account Credential For Proper Notice Type - AuthinticationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("AccountOpening");

            string mobileNumber = customerDetailRepository.GetCustomerRegisterdMobileNumber(_customerAccountPrmKey);

            string msg = smsDetailRepository.GetSmsForAccountOpening(_customerAccountPrmKey);

            try
            {
                string result = HitSMSApi(mobileNumber, msg);

                SmsResponse smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);

                // Save Message Log
                smsDetailRepository.SaveMessageLog(_customerAccountPrmKey.ToString(), smsResponse, mobileNumber, msg, smsAccountCredential.SenderId);

                return smsResponse;
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                return GetResponse(errorResult, smsAccountCredential.NameOfProvider);
            }
        }

        public SmsResponse SendMembershipApprovalSms(long _customerAccountPrmKey)
        {
            // Get Account Credential For Proper Notice Type - AuthinticationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("MembershipApproval");

            string mobileNumber = customerDetailRepository.GetCustomerRegisterdMobileNumber(_customerAccountPrmKey);

            string msg = smsDetailRepository.GetSmsForMembershipApproval(_customerAccountPrmKey);

            try
            {
                string result = HitSMSApi(mobileNumber, msg);

                SmsResponse smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);

                // Save Message Log
                smsDetailRepository.SaveMessageLog(_customerAccountPrmKey.ToString(), smsResponse, mobileNumber, msg, smsAccountCredential.SenderId);

                return smsResponse;
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                return GetResponse(errorResult, smsAccountCredential.NameOfProvider);
            }
        }

        public SmsResponse SendMembershipRejectionSms(long _customerAccountPrmKey)
        {
            // Get Account Credential For Proper Notice Type - AuthinticationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("MembershipRejection");

            string mobileNumber = customerDetailRepository.GetCustomerRegisterdMobileNumber(_customerAccountPrmKey);

            string msg = smsDetailRepository.GetSmsForMembershipApproval(_customerAccountPrmKey);

            try
            {
                string result = HitSMSApi(mobileNumber, msg);

                SmsResponse smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);

                // Save Message Log
                smsDetailRepository.SaveMessageLog(_customerAccountPrmKey.ToString(), smsResponse, mobileNumber, msg, smsAccountCredential.SenderId);

                return smsResponse;
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                return GetResponse(errorResult, smsAccountCredential.NameOfProvider);
            }
        }

        public SmsResponse SendTokenSms(int _userAuthenticationTokenPrmKey)
        {
            // Get Account Credential For Proper Notice Type - AuthinticationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("AuthenticationOTP");

            string mobileNumber = securityDetailRepository.GetUserProfileFieldByPrmKey("MobileNumber", userProfilePrmKey);

            string msg = smsDetailRepository.GetSmsForUserAuthenticationToken(_userAuthenticationTokenPrmKey);

            try
            {
                string result = HitSMSApi(mobileNumber, msg);

                SmsResponse smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);

                // Save Message Log
                smsDetailRepository.SaveMessageLog(_userAuthenticationTokenPrmKey.ToString(), smsResponse, mobileNumber, msg, smsAccountCredential.SenderId);

                return smsResponse;
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                return GetResponse(errorResult, smsAccountCredential.NameOfProvider);
            }
        }

        public SmsResponse ResendTokenSms (string _typeOfToken)
        {
            int userAuthenticationTokenPrmKey = 0;
            string msg = "";

            // Get Account Credential For Proper Notice Type - AuthinticationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("AuthenticationOTP");

            if (_typeOfToken == "Authentication")
            {
                userAuthenticationTokenPrmKey = userAuthenticationTokenRepository.GetRecentUserAuthenticationTokenPrmKey((short)HttpContext.Current.Session["UserProfilePrmKey"]);
                msg = smsDetailRepository.GetSmsForUserAuthenticationToken(userAuthenticationTokenPrmKey);
            }
            string mobileNumber = securityDetailRepository.GetUserProfileFieldByPrmKey("MobileNumber", (short)HttpContext.Current.Session["UserProfilePrmKey"]);
           
            try
            {
                string result = HitSMSApi(mobileNumber, msg);

                smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);
                // Save Sms Log
                smsDetailRepository.SaveSmsUserAuthenticationToken(userAuthenticationTokenPrmKey, smsResponse);

                return smsResponse;
                
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                smsResponse = GetResponse(errorResult, smsAccountCredential.NameOfProvider);
                return smsResponse;
            }
        }

        public string ResendTokenSmsWithResult(string _typeOfToken)
        {
            int userAuthenticationTokenPrmKey = 0;
            string msg = "";
            string returnResult = "";
            if (_typeOfToken == "Authentication")
            {
                userAuthenticationTokenPrmKey = userAuthenticationTokenRepository.GetRecentUserAuthenticationTokenPrmKey(userProfilePrmKey);

                if (userAuthenticationTokenPrmKey > 0)
                {
                    msg = smsDetailRepository.GetSmsForUserAuthenticationToken(userAuthenticationTokenPrmKey);

                    string mobileNumber = securityDetailRepository.GetUserProfileFieldByPrmKey("MobileNumber", userProfilePrmKey);

                    try
                    {
                        string result = HitSMSApi(mobileNumber, msg);

                        smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);
                        // Save Sms Log
                        smsDetailRepository.SaveSmsUserAuthenticationToken(userAuthenticationTokenPrmKey, smsResponse);

                        returnResult = smsResponse.Result;

                    }
                    catch (Exception e)
                    {
                        string s = e.Message;
                        returnResult = "Error, Error, Error, Error";
                    }
                }
                else
                {
                    returnResult = "Error, Error, Error, Error";
                    authProviderRepository.LogOut();
                    // Redirect To Page Token Expired

                }
            }
            return returnResult;
        }

        public SmsResponse SendTeleVerificationSMSToken(int _teleVerificationTokenPrmKey, string _mobileNumber)
        {
            // Get Account Credential For Proper Notice Type - TeleVerificationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("TeleVerificationOTP");

            string msg = smsDetailRepository.GetSmsForTeleVerificationToken(_teleVerificationTokenPrmKey);

            try
            {
                string result = HitSMSApi(_mobileNumber, msg);

                SmsResponse smsResponse =  GetResponse(result, smsAccountCredential.NameOfProvider);

                // Save Message Log
                smsDetailRepository.SaveMessageLog(_teleVerificationTokenPrmKey.ToString(), smsResponse, _mobileNumber, msg, smsAccountCredential.SenderId);

                return smsResponse;
            }
            catch (Exception e)
            {
                string s = e.Message;
                string errorResult = "Error, Error, Error, Error";
                return GetResponse(errorResult, smsAccountCredential.NameOfProvider);
            }

        }

        public string ResendTeleVerificationSMSToken(string _mobileNumber)
        {
            int teleVerificationTokenPrmKey = 0;
            string msg = "";
            string returnResult = "";

            // Get Account Credential For Proper Notice Type - TeleVerificationOTP In NoticeType Table
            smsAccountCredential = smsDetailRepository.GetSmsAccountCredentialsByNoticeType("TeleVerificationOTP");

            teleVerificationTokenPrmKey = smsRepository.GetRecentTeleVerificationTokenPrmKey(_mobileNumber);

            if (teleVerificationTokenPrmKey > 0)
            {
                msg = smsDetailRepository.GetSmsForTeleVerificationToken(teleVerificationTokenPrmKey);

                string mobileNumber = _mobileNumber;

                try
                {
                    string result = HitSMSApi(mobileNumber, msg);

                    smsResponse = GetResponse(result, smsAccountCredential.NameOfProvider);

                    smsDetailRepository.SaveMessageLog(teleVerificationTokenPrmKey.ToString(), smsResponse, _mobileNumber, msg, smsAccountCredential.SenderId);

                    returnResult = smsResponse.Result;

                }
                catch (Exception e)
                {
                    string s = e.Message;
                    returnResult = "Error, Error, Error, Error";
                }
            }
            else
            {
                returnResult = "Error, Error, Error, Error";

                // Redirect To Page Token Expired

            }

            return returnResult;
        }

        private string HitSMSApi(string _mobileNumber, string _msg)
        {
            using (var wb = new WebClient())
            {
                string result = "";
                
                // Weplus Technology
                if (smsAccountCredential.NameOfProvider == "WePlus Technology")
                {
                    byte[] response = wb.UploadValues("http://smsservice.weplustechnology.in/submitsms.jsp", new NameValueCollection()
                                    {
                                        {"user",  smsAccountCredential.UserID.ToString()},
                                        {"key",   smsAccountCredential.AuthenticationKey.ToString()},
                                        {"mobile", _mobileNumber},
                                        {"message", _msg},
                                        {"senderid", smsAccountCredential.SenderId},
                                        {"accusage", "1"},
                                    });

                    result = System.Text.Encoding.UTF8.GetString(response);
                }

                // VND SMS SERVICE
                if (smsAccountCredential.NameOfProvider == "VND SMS Services")
                {
                    byte[] response = wb.UploadValues("http://sms.vndsms.com/vendorsms/pushsms.aspx?", new NameValueCollection()
                                    {
                                        {"user",  smsAccountCredential.UserID.ToString()},
                                        {"password",   smsAccountCredential.UserPassword.ToString()},
                                        {"msisdn", _mobileNumber},
                                        {"sid", smsAccountCredential.SenderId},
                                        {"msg", _msg},
                                        {"fl", "0"},
                                        {"gwid", "2"},
                                    });

                    result = System.Text.Encoding.UTF8.GetString(response);
                }

                // Sattva Organization
                if (smsAccountCredential.NameOfProvider == "Sattva Organization")
                {
                    byte[] response = wb.UploadValues("http://sms.sattvaorganization.com/submitsms.jsp", new NameValueCollection()
                                    {
                                        {"user",  smsAccountCredential.UserID.ToString()},
                                        {"key",   smsAccountCredential.AuthenticationKey.ToString()},
                                        {"mobile", _mobileNumber},
                                        {"message", _msg},
                                        {"senderid", smsAccountCredential.SenderId},
                                        {"accusage", "1"},
                                        {"entityid", smsAccountCredential.PrincipalEntityId.ToString()},
                                        {"tempid", smsAccountCredential.TemplateId.ToString()},
                                    });

                    // Get SMS Result
                    result = System.Text.Encoding.UTF8.GetString(response);
                }

                return result;
            }
        }
                  
        public SmsResponse GetResponse(string _response, string _smsProvider)
        {
            SmsResponse smsResponse = new SmsResponse();            
            
            // Weplus Technology
            if (_smsProvider == "WePlus Technology")
            {
                //Replace All Special Character Like New Line \n, \r \t From Response   

                _response = _response.Replace("\n", String.Empty);
                _response = _response.Replace("\r", String.Empty);
                _response = _response.Replace("\t", String.Empty);

                string[] responseArray = _response.Split(',');
                if (responseArray.GetUpperBound(0) > 3)
                {
                    smsResponse.Status = responseArray[0].ToString();
                    smsResponse.Result = responseArray[1].ToString();
                    smsResponse.MessageId = responseArray[2].ToString();
                    smsResponse.ClientId = responseArray[3].ToString();
                }
                else
                {
                    smsResponse.Status = "Unknown";
                    smsResponse.Result = "Unknown";
                    smsResponse.MessageId = "Unknown";
                    smsResponse.ClientId = "Unknown";
                }
            }

            // VND SMS Services
            if (_smsProvider == "VND SMS Services")
            {
                //Replace All Special Character Like New Line \n, \r \t From Response   

                _response = _response.Replace("\n", String.Empty);
                _response = _response.Replace("\r", String.Empty);
                _response = _response.Replace("\t", String.Empty);
                _response = _response.Replace("\"", String.Empty);
                _response = _response.Replace("{", String.Empty);
                _response = _response.Replace("ErrorCode:", String.Empty);
                _response = _response.Replace("ErrorMessage:", String.Empty);
                _response = _response.Replace("JobId:", String.Empty);

                string[] responseArray = _response.Split(',');
                if (responseArray.GetUpperBound(0) > 3)
                {
                    smsResponse.Status = responseArray[0].ToString();
                    smsResponse.Result = responseArray[1].ToString();
                    smsResponse.MessageId = responseArray[2].ToString();
                    smsResponse.ClientId = responseArray[3].ToString();
                }
                else
                {
                    smsResponse.Status = "Unknown";
                    smsResponse.Result = "Unknown";
                    smsResponse.MessageId = "Unknown";
                    smsResponse.ClientId = "Unknown";
                }
            }

            // Sattva Organization
            if (_smsProvider == "Sattva Organization")
            {
                //Replace All Special Character Like New Line \n, \r \t From Response   

                _response = _response.Replace("\n", String.Empty);
                _response = _response.Replace("\r", String.Empty);
                _response = _response.Replace("\t", String.Empty);

                string[] responseArray = _response.Split(',');

                if (responseArray.GetUpperBound(0) > 3)
                {
                    smsResponse.Status = responseArray[0].ToString();
                    smsResponse.Result = responseArray[1].ToString();
                    smsResponse.MessageId = responseArray[2].ToString();
                    smsResponse.ClientId = responseArray[3].ToString();
                }
                else
                {
                    smsResponse.Status = "Unknown";
                    smsResponse.Result = "Unknown";
                    smsResponse.MessageId = "Unknown";
                    smsResponse.ClientId = "Unknown";
                }
            }

            return smsResponse;
        }
    }
}

