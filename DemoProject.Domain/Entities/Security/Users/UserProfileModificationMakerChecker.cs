using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileModificationMakerChecker")]
    public partial class UserProfileModificationMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short UserProfileModificationPrmKey { get; set; }

        public short UserProfileCreatorPrmKey { get; set; } 

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual UserProfileModification UserProfileModification { get; set; }
    }
}
