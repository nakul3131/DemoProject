using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class ConfigDetailViewModel
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public ConfigDetailViewModel()
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
        }
    }
}
