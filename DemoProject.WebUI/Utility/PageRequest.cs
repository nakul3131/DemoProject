using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.WebUI.Utility
{
    public class PageRequest
    {
        private readonly IMLDetailRepository mlDetailRepository;

        public PageRequest()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        public bool IsHumanRequest()
        {
            if (HttpContext.Current.Request.Cookies["RepeatedRequestCountCookie"] != null)
            {
                if (int.Parse(HttpContext.Current.Request.Cookies["RepeatedRequestCountCookie"].Value) > int.Parse(mlDetailRepository.GetMLConfigValue("Internet Bot", "Identify")))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}