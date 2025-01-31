using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.Security.Log;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Log;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.Services.Concrete.Security
{
    public class EFAuthProviderRepository : IAuthProviderRepository
    {
        //
        // Summary:
        //          This is repository class. It implements the IAuthProvider interface and uses an instance of EFDbContext
        //          to retrieve data from the database using the Entity Framework

        private readonly EFDbContext context;

        private readonly IAccountRecoveryLogRepository accountRecoveryLogRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IInvalidLoginLogRepository invalidLoginLogRepository;
        private readonly ILoginLogRepository loginLogRepository;
        private readonly IPasswordPolicyRepository passwordPolicy;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IUserAuthenticationTokenRepository userAuthenticationTokenRepository;
        private readonly IUserProfileAccessibilityRepository userProfileAccessibilityRepository;
        private readonly IUserProfileHomeBranchRepository userProfileHomeBranchRepository;
        private readonly IUserProfilePasswordRepository userProfilePasswordRepository;
        private readonly IUserProfilePhotoRepository userProfilePhotoRepository;
        private readonly IUserProfileRepository userProfileRepository;

        public EFAuthProviderRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IUserAuthenticationTokenRepository _userAuthenticationTokenRepository, 
                                        ILoginLogRepository _loginLogRepository, IInvalidLoginLogRepository _invalidLoginLogRepository, IUserProfileRepository _userProfileRepository, IUserProfileAccessibilityRepository _userProfileAccessibilityRepository, IUserProfilePasswordRepository _userProfilePasswordRepository, 
                                        IUserProfileHomeBranchRepository _userProfileHomeBranchRepository, IUserProfilePhotoRepository _userProfilePhotoRepository, ISecurityDetailRepository _securityDetailRepository, IAccountRecoveryLogRepository _accountRecoveryLogRepository, IPasswordPolicyRepository _passwordPolicy)
        {
            context = _connection.EFDbContext;

            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            userAuthenticationTokenRepository = _userAuthenticationTokenRepository;
            loginLogRepository = _loginLogRepository;
            invalidLoginLogRepository = _invalidLoginLogRepository;
            userProfileRepository = _userProfileRepository;
            userProfileAccessibilityRepository = _userProfileAccessibilityRepository;
            userProfilePasswordRepository = _userProfilePasswordRepository;
            userProfileHomeBranchRepository = _userProfileHomeBranchRepository;
            userProfilePhotoRepository = _userProfilePhotoRepository;
            securityDetailRepository = _securityDetailRepository;
            accountRecoveryLogRepository = _accountRecoveryLogRepository;
            passwordPolicy = _passwordPolicy;
        }

        public string IsAuthenticateImage(short _userProfilePrmKey)
        {
            string newsrc = string.Empty;

            try
            {
                var image = context.UserProfilePhotoes.Where(x => x.UserProfilePrmKey == _userProfilePrmKey).Select(x => x.Photo).FirstOrDefault();

                if (image != null)
                {
                    newsrc = Convert.ToBase64String(image);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;

            }

            return newsrc;
        }

        public bool IsAuthenticate(string _userName, string _password)
        {
            return userProfilePasswordRepository.IsValidUserCredentialsForAuthentication(_userName, _password);
        }

        public bool IsTokenAuthenticate(short _userProfilePrmKey, string _mobileOTP, string _emailVCode, byte _tokenFor)
        {
            return userAuthenticationTokenRepository.IsTokenAuthenticate(_userProfilePrmKey, _mobileOTP, _emailVCode, _tokenFor);            
        }

        public bool IsValidUserPassword(short _userProfilePrmKey, string _password)
        {
            bool result = userProfilePasswordRepository.IsValidUserPassword(_userProfilePrmKey, _password);

            // Create Session Variable For Login Trouble Shoot Problem
            if (result)
            {
                HttpContext.Current.Session.Add("IsValidUserPassword", true);
                HttpContext.Current.Session.Add("UserProfilePrmKey", _userProfilePrmKey);
            }
            else
            {
                HttpContext.Current.Session.Add("IsValidUserPassword", false);
            }

            return result;
        }

        public void CreateAuthenticationTicket(string _userName)
        {
            short userProfilePrmKey = securityDetailRepository.GetUserProfilePrmKeyByColumn("NameOfUserProfile", _userName);
            
            // Create User Profile PrmKey Session Variable
            HttpContext.Current.Session.Add("UserProfilePrmKey", userProfilePrmKey);

            short branchPrmKey = userProfileHomeBranchRepository.GetUserHomeBranchPrmKey(userProfilePrmKey);
            
            // Create UserHomeBranchPrmKey Session Variable
            HttpContext.Current.Session.Add("UserHomeBranchPrmKey", branchPrmKey);

            short regionalLanguagePrmKey = enterpriseDetailRepository.GetBusinessOfficeRegionalLanguagePrmKey();

            // Create RegionalLanguagePrmKey Session Variable
            HttpContext.Current.Session.Add("RegionalLanguagePrmKey", regionalLanguagePrmKey);

            string nameOfRegionalLanguage = configurationDetailRepository.GetNameOfRegionalLanguage(regionalLanguagePrmKey);

            // Create RegionalLanguage Session Variable            
            HttpContext.Current.Session.Add("RegionalLanguage", nameOfRegionalLanguage);

            string nameOfRegionalLanguageInEnglish = configurationDetailRepository.GetNameOfLanguage(regionalLanguagePrmKey);

            // Create RegionalLanguage In English Text Session Variable            
            HttpContext.Current.Session.Add("RegionalLanguageInEnglish", nameOfRegionalLanguageInEnglish);

            // Creates an authentication ticket for the supplied user name and adds it to the
            //     cookies collection of the response and 
            //     false to create a persistent cookie (that is not saved across browser sessions)
            FormsAuthentication.SetAuthCookie(_userName, false);
        }

        public bool IsTokenBasedAuthenticationEnabledForUser(short _userProfilePrmKey)
        {
            return userProfileAccessibilityRepository.IsUserAllowTokenBasedAuthentication(_userProfilePrmKey);
        }

        public void UpdateSuspiciousActivityCount(bool _isResetToZero)
        {
            if (_isResetToZero)
            {
                var suspiciousActivityCount = new HttpCookie("saCount")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };

                HttpContext.Current.Response.Cookies.Add(suspiciousActivityCount);
            }
            else
            {
                byte count = 0;

                if (HttpContext.Current.Request.Cookies["saCount"] != null)
                {
                    count = Convert.ToByte(HttpContext.Current.Request.Cookies["saCount"].Value);
                    count += 1;

                    var suspiciousActivityCount = new HttpCookie("saCount")
                    {
                        Expires = DateTime.Now.AddDays(365),
                        Value = count.ToString()
                    };

                    HttpContext.Current.Response.Cookies.Add(suspiciousActivityCount);
                }
                else
                {
                    var suspiciousActivityCount = new HttpCookie("saCount")
                    {
                        Expires = DateTime.Now.AddDays(365),
                        Value = "1"
                    };

                    HttpContext.Current.Response.Cookies.Add(suspiciousActivityCount);
                }
            }
        }

        public string GetInvalidAttemptErrorMessage(short _userProfilePrmKey)
        {           
            return context.Database.SqlQuery<string>("SELECT dbo.GetErrorMessageForInvalidAttempt (@UserProfilePrmKey)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey)).FirstOrDefault();
        }

        public string GetSuspiciousActivityErrorMessage()
        {
            string result = "";

            byte maxNumOfSuspiciousActivityAttempts = context.AppConfigs.Select(a => a.MaxNumOfSuspiciousActivityAttempts).FirstOrDefault();

            byte count = 2;//Convert.ToByte(HttpContext.Current.Request.Cookies["saCount"].Value);

            if (count != 0)
            {
                if (count < maxNumOfSuspiciousActivityAttempts)
                    result = "We Found Throttle Requests From You, If You Do Same Continuously, Your Device Will Be Blocked";
                else if (count + 1 == maxNumOfSuspiciousActivityAttempts)
                    result = "We Found Throttle Requests From You, If You Do Same One More Time, Your Device Will Be Blocked";
                else if (count >= maxNumOfSuspiciousActivityAttempts)
                    result = "We Found Continuously Throttle Requests From You, Your Device Is Blocked";
            }

            return result;
        }

        public short GetUserProfilePrmKey(string _nameOfUserProfile)
        {
            return securityDetailRepository.GetUserProfilePrmKeyByName(_nameOfUserProfile);
        }

        public string GetUserProfileStatus(short _userProfilePrmKey)
        {
            return securityDetailRepository.GetUserProfileStatus(_userProfilePrmKey);
        }

        public byte GetUserProfileSessionTimeOut(short _userProfilePrmKey)
        {
            return userProfileAccessibilityRepository.GetUserProfileSessionTimeOut(_userProfilePrmKey);
        }
        
        public string GetDeliveryChannelsOfTokenForUser(short _userProfilePrmKey)
        {
            return userProfileAccessibilityRepository.GetDeliveryChannelsOfTokenForUser(_userProfilePrmKey);
        }

        public string GetRegisteredEmailIdOfUser(short _userProfilePrmKey)
        {
            return securityDetailRepository.GetUserProfileRegisteredEmailId(_userProfilePrmKey);
        }

        public string GetRegisteredMobileNumberOfUser(short _userProfilePrmKey)
        {
            return securityDetailRepository.GetUserProfileRegisteredMobileNumber(_userProfilePrmKey);
        }

        public bool IsUserHasTrouble(string _userName, string _password)
        {
            bool result = userProfilePasswordRepository.IsValidUserPassword(_userName, _password);

            if (result)
            {
                short userProfilePrmKey = securityDetailRepository.GetUserProfilePrmKeyByColumn("NameOfUserProfile", _userName);
                string userStatus = securityDetailRepository.GetUserProfileStatus(userProfilePrmKey);

                if (userStatus == StringLiteralValue.Locked || userStatus == StringLiteralValue.IncompleteLogin)
                {
                    return true;
                }                
            }
            return false;
        }

        public void UnlockUser(short _userProfilePrmKey, string _method)
        {
            context.Database.ExecuteSqlCommand("Execute dbo.Usp_UnlockUser @p0", new SqlParameter("@p0", _userProfilePrmKey));
            // Account Recovery Token For - Unlock User (2)
            SaveAccountRecoveryLog(_userProfilePrmKey, 2, _method);
            LogOut();
        }

        public void ClearUserRecentSession(short _userProfilePrmKey, string _method)
        {
            context.Database.ExecuteSqlCommand("Execute dbo.Usp_ClearLoginSession @p0", new SqlParameter("@p0", _userProfilePrmKey));

            // Account Recovery Token For - 1 - For Password Recovery, 2 - For Unlock User, 3 - For Clear Login Session
            SaveAccountRecoveryLog(_userProfilePrmKey, 3, _method);

            if (!(_method == "Admin"))
            {
                LogOut();
            }
        }

        public void ChangeUserPassword()
        {
            //userProfilePasswordRepository.
        }

        public void LoginLog(short _userProfilePrmKey, bool _isMFAEnabled, byte _mfaMethodPrmKey, string _ip)
        {
            LoginLog loginLog = new LoginLog();

            DateTime now = DateTime.Now;

            loginLog.LoginDateTime = now;
            loginLog.UserProfilePrmKey = _userProfilePrmKey;
            loginLog.IsMFAEnabled = _isMFAEnabled;
            loginLog.MFAMethodPrmKey = _mfaMethodPrmKey;
            loginLog.ClientMachineName = HttpContext.Current.Server.MachineName;
            loginLog.ClientBrowser = HttpContext.Current.Request.Browser.Browser;
            loginLog.ClientIPAddress = _ip;
            loginLog.ClientLocation = "Local";
            loginLog.ClientApp = "Web";
            loginLog.ClientOperationSystem = HttpContext.Current.Request.Browser.Platform;
            loginLog.IsRiskDetected = false;
            loginLog.LogoutDateTime = null;

            // Save Log
            loginLogRepository.SaveLoginLog(loginLog);
        }

        public void InvalidLoginLog(string _input1, string _input2, short _userProfilePrmKey)
        {
            try
            {
                // Customise Class created For assigning or storing encrypted value issue
                CustomInvalidLoginLog customiseInvalidLoginLog = new CustomInvalidLoginLog();

                DateTime now = DateTime.Now;

                customiseInvalidLoginLog.EffectiveDateTime = now;
                customiseInvalidLoginLog.InputedUserName = _input1;
                customiseInvalidLoginLog.InputedPassword = _input2;
                customiseInvalidLoginLog.UserProfilePrmKey = _userProfilePrmKey;
                customiseInvalidLoginLog.ClientMachineName = HttpContext.Current.Server.MachineName;
                customiseInvalidLoginLog.ClientBrowser = HttpContext.Current.Request.Browser.Browser;
                customiseInvalidLoginLog.ClientIPAddress = HttpContext.Current.Request.UserHostAddress;
                customiseInvalidLoginLog.ClientLocation = "Local";
                customiseInvalidLoginLog.ClientApp = "Web";
                customiseInvalidLoginLog.ClientOperatingSystem = HttpContext.Current.Request.Browser.Platform;

                if (_userProfilePrmKey > 0)
                {
                    customiseInvalidLoginLog.MatchingRatioOfInputedPassword = 0; //userProfilePasswordRepository.GetPasswordMatchRatio(userPrmKey, filterContext.Controller.ValueProvider.GetValue("password").AttemptedValue);
                }
                else
                {
                    customiseInvalidLoginLog.MatchingRatioOfInputedPassword = 0;
                }

                // Save Invalid Login Log
                invalidLoginLogRepository.SaveInvalidLoginLog(customiseInvalidLoginLog);
            }
            catch (Exception e)
            {
                string m = e.Message;
            }
        }

        public void SaveAccountRecoveryLog(short _userProfilePrmKey, byte _accountRecoveryFor, string _recoveryMethod)
        {
            DateTime now = DateTime.Now;

            AccountRecoveryLog accountRecoveryLog = new AccountRecoveryLog()
            {
                EffectiveDateTime = DateTime.Now,
                UserProfilePrmKey = _userProfilePrmKey,
                AccountRecoveryMethodPrmKey = securityDetailRepository.GetAuthenticationMethodPrmKeyByName(_recoveryMethod),
                AccountRecoveryFor = _accountRecoveryFor,
            };
      
            accountRecoveryLogRepository.SaveAccountRecoveryLog(accountRecoveryLog);
        }

        public void ResetUserPassword(short _userProfilePrmKey, string _userPassword)
        {
            try
            {
                var data = passwordPolicy.GetPasswordPolicyValues(_userProfilePrmKey);

                UserProfilePassword userProfilePassword = new UserProfilePassword
                {
                    UserProfilePrmKey = _userProfilePrmKey,
                    UserPassword = Encoding.ASCII.GetBytes(_userPassword),
                    CreateDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(data.ForcePasswordChangeAfterDays),
                    ActivationStatus = "ACT"
                };

                
                //context.SmsUserAuthenticationTokens.Add(smsUserAuthenticationToken);
                context.UserProfilePasswords.Attach(userProfilePassword);
                context.Entry(userProfilePassword).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        public void LogOut()
        {
            context.Database.ExecuteSqlCommand("Execute dbo.Usp_LogOut @p0", new SqlParameter("@p0", (short)HttpContext.Current.Session["UserProfilePrmKey"]));
        } 
    }
}