using System.Web.Mvc;
using DemoProject.WebUI.Utility;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Infrastructure.Filter
{
    public class RequestValidityActionFilterAttribute : ActionFilterAttribute 
    {
        private PageRequest pageRequest = new PageRequest();
        private Subscription subscription = new Subscription();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (subscription.IsValid() == false)
            {
                throw new SubscriptionExpiredException();
            }

            if (pageRequest.IsHumanRequest() == false)
            {
                throw new UnusualTrafficException();
            }
            // Assign Default value which is used in forgot password link
            filterContext.Controller.TempData["IsUserVAlid"] = false;
        }
    }
}