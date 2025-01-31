using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileIndexViewModel
    {
        public short PrmKey { get; set; }

        public Guid UserProfileId { get; set; }

        [StringLength(100)]
        public string NameOfUserProfile { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

    }
}
