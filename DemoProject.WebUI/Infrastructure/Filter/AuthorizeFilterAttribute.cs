using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Security.Log;
using DemoProject.Services.Abstract.Security.Users;

namespace DemoProject.WebUI.Infrastructure.Filter
{
    //
    // Summary:
    //     Specifies that access to a controller or action method is restricted to users
    //     who meet the authorization requirement.
    public class AuthorizeFilterAttribute : AuthorizeAttribute 
    {
        private string httpMethod;
        private string controllerName;
        private string actionName;
        private int menuPrmKey;

        private IConfigurationDetailRepository configurationDetailRepository;
        private IUserProfileMenuRepository userProfileMenuRepository;
        private IActivityLogRepository activityLogRepository; 

        //AuthorizeFilterAttribute(IUserProfileMenuRepository _userProfileMenuRepository)
        //{
        //    userProfileMenuRepository = _userProfileMenuRepository;
        //}

        //
        // Summary:
        //     Initializes a new instance of the System.Web.Mvc.AuthorizeAttribute class.
        //public AuthorizeFilterAttribute(string _actionName)
        //{
        //    actionName = _actionName;
        //}

        //
        // Summary:
        //     It provides an entry point for custom authorization checks.
        //
        // Parameters:
        //   httpContext:
        //     The HTTP context, which encapsulates all HTTP-specific information about an individual
        //     HTTP request.
        //
        // Returns:
        //     true if the user is authorized; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The httpContext parameter is null.
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            userProfileMenuRepository = DependencyResolver.Current.GetService<IUserProfileMenuRepository>();
            activityLogRepository = DependencyResolver.Current.GetService<IActivityLogRepository>();

            var routeValues = httpContext.Request.RequestContext.RouteData.Values;

            httpMethod = httpContext.Request.HttpMethod;
            controllerName = (string)routeValues["controller"];
            actionName = (string)routeValues["action"];

            menuPrmKey = configurationDetailRepository.GetMenuPrmKey(controllerName, actionName);

            HttpContext.Current.Session["IsRedirectToSamePage"] = userProfileMenuRepository.IsRedirectedToSamePage(menuPrmKey);

            // Save Activity Log 
            if (actionName != "SaveContact" && actionName != "SaveDataTables")
            {
                activityLogRepository.SaveActivityLog(menuPrmKey, httpMethod);
            }

            //
            // Summary:
            //          Firstly Check Whether is Logged In Or Not?
            //          Then It checks that whether user has access to requested page.
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (httpMethod == "GET" & actionName != "Amend" & actionName != "Cancel")
                {
                    return userProfileMenuRepository.HasUserPermission(actionName, controllerName);
                }
                else
                {
                    return true;                    
                }
            }
            else
            {
                return false;
            }
        }

        //
        // Summary:
        //     Processes HTTP requests that fail authorization.
        //
        // Parameters:
        //   filterContext:
        //     Encapsulates the information for using System.Web.Mvc.AuthorizeAttribute. The
        //     filterContext object contains the controller, HTTP context, request context,
        //     action result, and route data.
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult
            ( 
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "UnAuthorizedAccess" }
                }
            );
        }
    }
}