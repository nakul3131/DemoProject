using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileBusinessOfficeMakerChecker")]
    public partial class UserProfileBusinessOfficeMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int UserProfileBusinessOfficePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }
    
        public virtual UserProfileBusinessOffice UserProfileBusinessOffice { get; set; }
    }
}
