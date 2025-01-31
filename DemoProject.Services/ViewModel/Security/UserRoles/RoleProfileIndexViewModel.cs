using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Security.UserRoles
{
    public class RoleProfileIndexViewModel
    {
        public short PrmKey { get; set; }

        public Guid RoleProfileId { get; set; }

        [StringLength(100)]
        public string NameOfRoleProfile { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

    }
}
