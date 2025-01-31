using System.Web.Mvc;
using DemoProject.Services.Utility.SmsSender;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Abstract.SMS;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class SMSController : Controller
    {
        SmsSender sms = new SmsSender();

        private readonly IUserProfileAccessibilityRepository userProfileAccessibilityRepository;
        private readonly ISMSRepository smsRepository;

        public SMSController(IUserProfileAccessibilityRepository _userProfileAccessibilityRepository, ISMSRepository _smsRepository)
        {
            userProfileAccessibilityRepository = _userProfileAccessibilityRepository;
            smsRepository = _smsRepository;
        }

        public ActionResult ReSendUserAuthenticationToken()
        {
            string result = sms.ResendTokenSmsWithResult("Authentication");
            TempData["smsResponseResult"] = result;
            return Json(result, JsonRequestBehavior.AllowGet);           
        }

        public ActionResult ReSendTeleVerificataionToken(string MobileNumber)
        {
            string result = sms.ResendTeleVerificationSMSToken(MobileNumber);
            TempData["smsResponseResult"] = result;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendVerificationToken(string MobileNumber)
        {
            var data = smsRepository.CreateTeleVerificationSMSToken(MobileNumber);
            TempData["SmsResponseResult"] = data.ToString();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateVerificationToken(string MobileNumber, string Token)
        {
            bool data = smsRepository.IsValidMobileVerificationToken(MobileNumber, Token);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountOpeningWelcomeSMS(long _customerAccountPrmKey)
        {
            string response = sms.SendAccountOpeningSms(_customerAccountPrmKey).Result;
            TempData["SmsResponseResult"] = response.ToString();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MembershipApprovalSMS(long _customerAccountPrmKey)
        {
            string response = sms.SendMembershipApprovalSms(_customerAccountPrmKey).Result;
            TempData["SmsResponseResult"] = response.ToString();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}