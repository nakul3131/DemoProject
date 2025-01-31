using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileIdentityViewModel
    {
        // UserProfileIdentity

        public int PrmKey { get; set; }

        public Guid UserProfileIdentityId { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
