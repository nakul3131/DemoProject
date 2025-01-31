using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfilePasswordPolicyMakerChecker")]
    public partial class UserProfilePasswordPolicyMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePasswordPolicyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }
    
        public virtual UserProfilePasswordPolicy UserProfilePasswordPolicy { get; set; }
    }
}
