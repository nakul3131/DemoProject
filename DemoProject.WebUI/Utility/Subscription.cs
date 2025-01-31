using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.WebUI.Utility
{
    public class Subscription
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public Subscription()
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }

        public bool IsValid()
        {
            if (HttpContext.Current.Session["UserHomeBranchPrmKey"] != null)
            {
                return configurationDetailRepository.IsActiveSoftwareSubscriptionStauts();
            }            
            else
            {
                return true;
            }
        }
    }
}