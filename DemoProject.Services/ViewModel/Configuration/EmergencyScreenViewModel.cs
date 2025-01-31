using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class EmergencyScreenViewModel
    {
        [StringLength(1000)]
        public string EmergencyScreenHeaderText { get; set; }

        [StringLength(4000)]
        public string EmergencyScreenBodyText { get; set; }

        [StringLength(2000)]
        public string EmergencyScreenFooterText { get; set; }

        public void Getdata()
        {

        }
    }
}
