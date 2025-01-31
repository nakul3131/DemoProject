using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class HeaderPartialModel
    {
        public string title;
        public string path;

        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public HeaderPartialModel(string _menuCode)
        {
            configurationDetailRepository = DependencyResolver.Current.GetService<IConfigurationDetailRepository>();
            title = configurationDetailRepository.GetNameOfMenuByMenuId(_menuCode);
            path = configurationDetailRepository.GetMenuPath(_menuCode);
        }
    }
}
