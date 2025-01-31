using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileMakerChecker")]
    public partial class UserProfileMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short UserProfileCreatorPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
