using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Security.Login;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class EmergencyScreenViewModel
    {
        private readonly IEmergencyScreenRepository emergencyScreenRepository;

        public EmergencyScreenViewModel()
        {
            emergencyScreenRepository = DependencyResolver.Current.GetService<IEmergencyScreenRepository>();
            BodyText = emergencyScreenRepository.BodyText();
            FooterText = emergencyScreenRepository.FooterText();
            HeaderText = emergencyScreenRepository.HeaderText();
        }

        public short PrmKey { get; set; }
        [StringLength(1000)]
        public string HeaderText { get; set; }

        [StringLength(4000)]
        public string BodyText { get; set; }

        [StringLength(2000)]
        public string FooterText { get; set; }
    }
}

