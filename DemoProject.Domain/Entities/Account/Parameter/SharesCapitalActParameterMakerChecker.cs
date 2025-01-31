using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("SharesCapitalActParameterMakerChecker")]
    public partial class SharesCapitalActParameterMakerChecker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public byte SharesCapitalActParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual SharesCapitalActParameter SharesCapitalActParameter { get; set; }
    }
}
