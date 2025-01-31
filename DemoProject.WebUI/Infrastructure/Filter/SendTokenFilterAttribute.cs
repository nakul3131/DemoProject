using System.Web.Mvc;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;

namespace DemoProject.WebUI.Infrastructure.Filter
{
    public class SendTokenFilterAttribute : ActionFilterAttribute
    {
        private IAuthProviderRepository authProviderRepository;
        private IUserAuthenticationTokenRepository userAuthenticationTokenRepository;        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            authProviderRepository = DependencyResolver.Current.GetService<IAuthProviderRepository>();
            userAuthenticationTokenRepository = DependencyResolver.Current.GetService<IUserAuthenticationTokenRepository>();

            string smsResponse="";
            string currentRequestAction = filterContext.ActionDescriptor.ActionName;

            short userProfilePrmKey = (short)filterContext.HttpContext.Session["UserProfilePrmKey"];
            filterContext.Controller.TempData["DeliveryChannel"] = authProviderRepository.GetDeliveryChannelsOfTokenForUser(userProfilePrmKey);
            filterContext.Controller.TempData["RegisteredMobileNumber"] = string.Concat("********", authProviderRepository.GetRegisteredMobileNumberOfUser(userProfilePrmKey).Substring(8));

            if (currentRequestAction == "TokenBasedMFA")
            {
                // Token For - 1 - Login User
                smsResponse = userAuthenticationTokenRepository.CreateUserAuthenticationToken(userProfilePrmKey, 1);
            }

            if (currentRequestAction == "ResetPasswordViaToken")
            {
                // Token For - 2 - ForgotPassword / Password Reset
                smsResponse = userAuthenticationTokenRepository.CreateUserAuthenticationToken(userProfilePrmKey, 2);
            }

            if (currentRequestAction == "UnlockUserViaToken")
            {
                // Token For - 3 - Unlock User
                smsResponse = userAuthenticationTokenRepository.CreateUserAuthenticationToken(userProfilePrmKey, 3);
            }

            if (currentRequestAction == "ClearUserLoginSessionViaToken")
            {
                // Token For - 4 - Clear User Login Session
                smsResponse = userAuthenticationTokenRepository.CreateUserAuthenticationToken(userProfilePrmKey, 4);
            }

            filterContext.Controller.TempData["smsResponseResult"] = smsResponse.ToLower().ToString();
            
        }
    }
}