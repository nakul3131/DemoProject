using System;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.WebUI.Utility;
using DemoProject.WebUI.Infrastructure.Filter;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using System.Web;

namespace DemoProject.WebUI.Controllers
{
    //
    // Summary:
    //     Provides methods that respond to HTTP requests that are made to an ASP.NET MVC Application
    [RoutePrefix("Employee")]
    public class AccountController : Controller
    {
        private readonly IAuthProviderRepository authProvider;
        private readonly IPasswordPolicyRepository passwordPolicy;
        private readonly IUserProfilePasswordRepository userProfilePasswordRepository;
        private short userProfilePrmKey;

        //
        // Summary:
        //     Initializes a new instance of the AccountController class.
        public AccountController(IAuthProviderRepository _authProvider, IPasswordPolicyRepository _passwordPolicy, IUserProfilePasswordRepository _userProfilePasswordRepository)
        {
            authProvider = _authProvider;
            passwordPolicy = _passwordPolicy;
            userProfilePasswordRepository = _userProfilePasswordRepository;
        }

        [HttpGet]     
        [AllowAnonymous]
        public ActionResult LandingPage()
        {
            // Set cache-control headers for the entire app
            HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

            // Clear All Cookies
            foreach (string cookie in Request.Cookies.AllKeys)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1); // Expire each cookie
            }

            return View();            
        }

        [AllowAnonymous]
        [RequestValidityActionFilter]
        [Route("PasswordBasedAuthentication")]
        public ActionResult Login()
        {

            MemoryCache cache = MemoryCache.Default;

            List<string> cacheKeys = cache.Select(c => c.Key).ToList();

            foreach (string cacheKey in cacheKeys)
            {
                cache.Remove(cacheKey);
            }

            Session["LoginTime"] = null;

            if (TempData["TransactionStatus"] != null)
            {
                if (TempData["TransactionStatus"].ToString() == StringLiteralValue.ResetPassword)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.ResetPassword;
                }
                else
                {
                    if (TempData["TransactionStatus"] != null)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.ClearRecentSession;
                    }

                    if (TempData["TransactionStatus"] != null)
                    {

                        TempData["TransactionStatus"] = StringLiteralValue.Unverified;
                    }
                }
            }
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("PasswordBasedAuthentication")]
        public ActionResult Login(LoginViewModel _loginViewModel, string _returnUrl )
        {
            if (ModelState.IsValid)
            {
                string userName = _loginViewModel.UserName;
                string password = _loginViewModel.Password;

                if (authProvider.IsAuthenticate(userName, password))
                {
                    authProvider.CreateAuthenticationTicket(userName);
                    userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                    DateTime? passwordExpiryDateTime = userProfilePasswordRepository.GetPasswordExpiryDate(userProfilePrmKey);
                    if (passwordExpiryDateTime != null)
                    {
                        DateTime? passwordExpiryDate = passwordExpiryDateTime.Value.Date;
                        if (password == "1" || passwordExpiryDate <= DateTime.Today)
                        {
                            if (password == "1")
                            {
                                TempData["Password"] = "1";
                            }
                            if (passwordExpiryDate <= DateTime.Today)
                            {
                                TempData["PasswordExpiryDate"] = DateTime.Today;
                            }
                            return Redirect(_returnUrl ?? Url.Action("CreateNewPasswordAfterReset"));
                        }
                    }

                    if (authProvider.IsTokenBasedAuthenticationEnabledForUser(userProfilePrmKey))
                    {
                        return Redirect(_returnUrl ?? Url.Action("TokenBasedMFA"));
                    }
                    else
                    {
                        if (Session["LoginTime"] == null)
                        {
                            DateTimeOffset now = DateTimeOffset.UtcNow;
                            long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

                            Session["LoginTime"]= unixTimeMilliseconds;                           
                        }
                        Session["Userimage"] = authProvider.IsAuthenticateImage(userProfilePrmKey);
                        Session["Username"] = userName;
                        Session["SessionTimeOut"] = authProvider.GetUserProfileSessionTimeOut(userProfilePrmKey);
                        authProvider.LoginLog(userProfilePrmKey, false, 1, "Home");
                        return Redirect(_returnUrl ?? Url.Action("DashBoard", "Home"));
                    }
                }
                else
                {
                    userProfilePrmKey = authProvider.GetUserProfilePrmKey(userName);

                    if (userProfilePrmKey > 0)
                    {
                        TempData["IsUserValid"] = true;
                        Session["UserProfilePrmKey"] = userProfilePrmKey;
                        bool isValidUserPassword = authProvider.IsValidUserPassword(userProfilePrmKey, password);

                        if (isValidUserPassword)
                        {
                            string userProfileStatus = authProvider.GetUserProfileStatus(userProfilePrmKey);

                            // Check Whether User Has Locked
                            if (userProfileStatus == StringLiteralValue.Locked)
                            {
                                return View("~/Views/Shared/_UserLocked.cshtml");
                                //throw new UserLockedException();
                            }

                            // Check Whether User Has Already Logged In
                            else if (userProfileStatus == StringLiteralValue.LoggedIn || userProfileStatus == StringLiteralValue.IncompleteLogin)
                            {
                                return View("~/Views/Shared/_ClearRecentSession.cshtml");
                                //throw new UserAlreadyLoggedInException();
                            }
                        }
                        else
                        {
                            ModelState.Clear();
                            authProvider.InvalidLoginLog(userName, password, userProfilePrmKey);
                            ModelState.AddModelError("InvalidAttempt", authProvider.GetInvalidAttemptErrorMessage(userProfilePrmKey));
                        }
                    }
                    else
                    {
                        authProvider.UpdateSuspiciousActivityCount(false);
                        ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
                    }
                }                
            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }

            return View(_loginViewModel);
        }

        [HttpGet]
        [SendTokenFilter]
        [Route("TokenBasedAuthentication")]
        public ActionResult TokenBasedMFA() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TokenBasedAuthentication")]
        public ActionResult TokenBasedMFA(MFAViewModel _mfaViewModel, string _returnUrl)
        {
            if (ModelState.IsValid)
            {
                short userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                string mobileOTP = _mfaViewModel.MobileOTP;
                string emailVCode = _mfaViewModel.EmailVCode;

                if (authProvider.IsTokenAuthenticate(userProfilePrmKey, mobileOTP, emailVCode, 1))
                {
                    authProvider.LoginLog(userProfilePrmKey, true, 2, TempData["Ip"].ToString());
                    ClearTempDataVariables();
                    return Redirect(_returnUrl ?? Url.Action("DashBoard", "Home"));
                }
                else
                {
                    string userProfileStatus = authProvider.GetUserProfileStatus(userProfilePrmKey);

                    // Check Whether User Has Locked
                    if (userProfileStatus == StringLiteralValue.Locked)
                    {
                        authProvider.LogOut();
                        return Redirect(_returnUrl ?? Url.Action("Login", "Account"));
                    }
                    else
                    {
                        authProvider.InvalidLoginLog(mobileOTP, emailVCode, userProfilePrmKey);
                        TempData["DeliveryChannel"] = authProvider.GetDeliveryChannelsOfTokenForUser(userProfilePrmKey);
                        ModelState.AddModelError("InvalidAttempt", authProvider.GetInvalidAttemptErrorMessage(userProfilePrmKey));
                    }
                }
            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }
            return View();
        }

        // GET: Recaptcha
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Recaptcha()
        {
            ViewBag.Notify = "Please Verify reCAPTCHA. We use reCAPTCHA to protects from spam and abuse.";
            return View();
        }

        // POST: Recaptcha
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SubmitRecaptcha()
        {
            if (IsRecaptchaInputValid())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("Recaptcha");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [SendTokenFilter]
        [Route("UnlockUserByToken")]
        public ActionResult UnlockUserViaToken()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("UnlockUserByToken")]
        public ActionResult UnlockUserViaToken(MFAViewModel _mfaViewModel,string Command)
        {
            if (ModelState.IsValid)
            {
                userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                string mobileOTP = _mfaViewModel.MobileOTP;
                string emailVCode = _mfaViewModel.EmailVCode;

                if (authProvider.IsTokenAuthenticate(userProfilePrmKey, _mfaViewModel.MobileOTP, _mfaViewModel.EmailVCode, 3))
                {
                    if (Command == StringLiteralValue.Unverified)
                    {
                        authProvider.ClearUserRecentSession(userProfilePrmKey, "Token");

                        TempData["TransactionStatus"] = StringLiteralValue.Unverified;
                    }

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    string userProfileStatus = authProvider.GetUserProfileStatus(userProfilePrmKey);

                    // Check Whether User Has Locked
                    if (userProfileStatus == StringLiteralValue.Locked)
                    {
                        return Redirect(Url.Action("Login", "Account"));
                    }
                    else
                    {
                        authProvider.InvalidLoginLog(mobileOTP, emailVCode, userProfilePrmKey);
                        TempData["DeliveryChannel"] = authProvider.GetDeliveryChannelsOfTokenForUser(userProfilePrmKey);
                        ModelState.AddModelError("InvalidAttempt", authProvider.GetInvalidAttemptErrorMessage(userProfilePrmKey));
                    }
                }

            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CreateNewPasswordAfterReset")]
        public ActionResult CreateNewPasswordAfterReset()
        {
            if (TempData["Password"] != null)
            {
                if (TempData["Password"].ToString() == "1")
                {
                    TempData["PasswordMsg"] = "If Your Password is 1 Then You Must Reset Your Password";
                }
            }

            if (TempData["PasswordExpiryDate"] != null)
            {
                if ((DateTime)TempData["PasswordExpiryDate"] == DateTime.Today)
                {
                    TempData["PasswordExpiryDateMsg"] = "If Date Is Expiry Date Then You Must Reset Your Password";
                }
            }
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("CreateNewPasswordAfterReset")]
        public ActionResult CreateNewPasswordAfterReset(ResetPasswordViaTokenViewModel _mfaViewModel, string _returnUrl)
        {
            if (ModelState.IsValid)
            {
                short userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                string userPassword = _mfaViewModel.UserPassword;
                authProvider.ResetUserPassword(userProfilePrmKey, userPassword);
                TempData["TransactionStatus"] = StringLiteralValue.ResetPassword;
                return Redirect(_returnUrl ?? Url.Action("Login", "Account"));
            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetPasswordPolicyValues(short _userProfilePrmKey)
        {
            var data = passwordPolicy.GetPasswordPolicyValues(_userProfilePrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        [SendTokenFilter]
        [Route("ResetPasswordByToken")]
        public ActionResult ResetPasswordViaToken()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ResetPasswordByToken")]
        public ActionResult ResetPasswordViaToken(ResetPasswordViaTokenViewModel _mfaViewModel, string _returnUrl)
        {
            if (ModelState.IsValid)
            {
                short userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                string mobileOTP = _mfaViewModel.MobileOTP;
                string emailVCode = _mfaViewModel.EmailVCode;
                string userPassword = _mfaViewModel.UserPassword;

                if (authProvider.IsTokenAuthenticate(userProfilePrmKey, mobileOTP, emailVCode, 2))
                {
                    authProvider.ResetUserPassword(userProfilePrmKey, userPassword);
                    authProvider.LoginLog(userProfilePrmKey, true, 1, TempData["Ip"].ToString());
                    ClearTempDataVariables();
                    TempData["TransactionStatus"] = StringLiteralValue.ResetPassword;
                    return Redirect(_returnUrl ?? Url.Action("Login", "Account"));
                }
                else
                {
                    authProvider.InvalidLoginLog(mobileOTP, emailVCode, userProfilePrmKey);
                    TempData["DeliveryChannel"] = authProvider.GetDeliveryChannelsOfTokenForUser(userProfilePrmKey);
                    ModelState.AddModelError("InvalidAttempt", authProvider.GetInvalidAttemptErrorMessage(userProfilePrmKey));
                }
            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        [SendTokenFilter]
        [Route("ClearRecentLoginSession")]
        public ActionResult ClearUserLoginSessionViaToken()
        {
            userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
            TempData["RegisteredEmailId"] = string.Concat("*******", authProvider.GetRegisteredEmailIdOfUser(userProfilePrmKey).Substring(5));
            TempData["RegisteredMobileNumber"] = string.Concat("*******", authProvider.GetRegisteredMobileNumberOfUser(userProfilePrmKey).Substring(7));
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ClearRecentLoginSession")]
        public ActionResult ClearUserLoginSessionViaToken(MFAViewModel _mfaViewModel,string Command)
        {
            if (ModelState.IsValid)
            {
                userProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                string mobileOTP = _mfaViewModel.MobileOTP;
                string emailVCode = _mfaViewModel.EmailVCode;

                if (authProvider.IsTokenAuthenticate(userProfilePrmKey, _mfaViewModel.MobileOTP, _mfaViewModel.EmailVCode, 4))
                {
                    if (Command == StringLiteralValue.ClearRecentSession)
                    {
                        authProvider.ClearUserRecentSession(userProfilePrmKey, "Token");

                        TempData["TransactionStatus"] = StringLiteralValue.ClearRecentSession;
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    string userProfileStatus = authProvider.GetUserProfileStatus(userProfilePrmKey);

                    // Check Whether User Has Locked
                    if (userProfileStatus == StringLiteralValue.Locked)
                    {
                        return Redirect(Url.Action("Login", "Account"));
                    }
                    else
                    {
                        authProvider.InvalidLoginLog(mobileOTP, emailVCode, userProfilePrmKey);
                        TempData["DeliveryChannel"] = authProvider.GetDeliveryChannelsOfTokenForUser(userProfilePrmKey);
                        TempData["RegisteredEmailId"] = string.Concat("*******", authProvider.GetRegisteredEmailIdOfUser(userProfilePrmKey).Substring(5));
                        TempData["RegisteredMobileNumber"] = string.Concat ("*******", authProvider.GetRegisteredMobileNumberOfUser(userProfilePrmKey).Substring(7));
                        ModelState.AddModelError("InvalidAttempt", authProvider.GetInvalidAttemptErrorMessage(userProfilePrmKey));
                    }
                }
            }
            else
            {
                authProvider.UpdateSuspiciousActivityCount(false);
                ModelState.AddModelError("InvalidAttempt", authProvider.GetSuspiciousActivityErrorMessage());
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [SendTokenFilter]
        [Route("AdministratorContactDetails")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("UnAuthorizedAccess")]
        public ActionResult UnAuthorizedAccess()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Logout")]
        public ActionResult LogOut()
        {
            var a = ControllerContext.RouteData;
            Session["LoginTime"] = null;
            authProvider.LogOut();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [NonAction]
        private bool IsRecaptchaInputValid()
        {
            var response = Request["g-recaptcha-response"]; //Validate Google recaptcha here.                                                                 
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response)); //Pass your Response and Secret Key here.  
            var obj = JObject.Parse(result); //Result In True or False.  
            var status = (bool)obj.SelectToken("success");
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(result);
            if (captchaResponse.ErrorCodes != null)
            {
                string inputResponse = captchaResponse.ErrorCodes[0].ToString();
            }            
            //check the status is true or not to identify you are a robot or not.  
            if (status == true) 
            {
                HttpContext.Response.Cookies["RepeatedRequestCountCookie"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies["PreviousRequestedPage"].Expires = DateTime.Now.AddDays(-1);
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();

                return true;
            }
            else
            {
                ViewBag.Notify = "Google reCaptcha Not Validated Successfully";

                return false;
            }
        }

        // Clear UnNecessary Temp Variables
        [NonAction]
        private void ClearTempDataVariables()
        {
            if (TempData.Keys.Count > 0)
            {
                foreach (var key in TempData.Keys)
                {
                    TempData.Remove(key);

                    if (TempData.Keys.Count == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}


// GET: GetUserProfilePhoto
//[HttpGet]
//[AllowAnonymous]
//public FileContentResult GetUserProfilePhoto ()
//{            
//    short userProfilePrmKey = (short)HttpContext.Session["TmpUserProfilePrmKey"];

//    UserProfilePhoto userProfilePhoto = authProvider.GetUserProfilePhoto(userProfilePrmKey);

//    if (userProfilePhoto != null)
//    {
//        return File(userProfilePhoto.Photo, userProfilePhoto.Extension);
//    }
//    else
//    {
//        return null;
//    }
//}