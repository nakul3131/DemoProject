using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileAccessibilityViewModel
    {
        public short PrmKey { get; set; }

        public Guid UserProfileAccessibilityId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(3)]
        public string LoginVia { get; set; }

        public byte SessionTimeOut { get; set; }

        public short ScreenSaverTheme { get; set; }

        public short ApplicationTheme { get; set; }

        [StringLength(3)]
        public string TokenDeliveryChannel { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
    }
}
