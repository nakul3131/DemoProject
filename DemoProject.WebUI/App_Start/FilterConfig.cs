using System.Web;
using System.Web.Mvc;
using DemoProject.WebUI.Infrastructure.Filter;
namespace DemoProject.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilterAttribute());
            filters.Add(new AuthorizeFilterAttribute());
            filters.Add(new RecaptchaFilterAttribute());
        }
    }
}
