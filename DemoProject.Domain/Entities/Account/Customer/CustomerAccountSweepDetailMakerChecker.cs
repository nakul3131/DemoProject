using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountSweepDetailMakerChecker")]
    public partial class CustomerAccountSweepDetailMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountSweepDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual CustomerAccountSweepDetail CustomerAccountSweepDetail { get; set; }
    }
}
