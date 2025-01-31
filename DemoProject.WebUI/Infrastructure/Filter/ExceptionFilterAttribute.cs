using System.Web.Mvc;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Infrastructure.Filter
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            // Handle Subscription Expired Exception
            if (filterContext.Exception is SubscriptionExpiredException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_SubscriptionExpired"
                };
            }

            // Handle Unusual Traffic Exception (BOT behavior)
            if (filterContext.Exception is UnusualTrafficException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_Recaptcha"
                };
            }

            // Handle User Locked Exception
            if (filterContext.Exception is UserLockedException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_UserLocked"
                };
            }

            // Handle User Already Exception
            if (filterContext.Exception is UserAlreadyLoggedInException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_ClearRecentSession"
                };
            }

            // Database Error
            if (filterContext.Exception is DatabaseException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_DatabaseError"
                };
            }

            // Exceed Boundary Limit Error
            if (filterContext.Exception is ExceedBoundaryLimitException)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_ExceedBoundaryLimit"
                };
            }

            filterContext.ExceptionHandled = true;
        }
    }
}