using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("RevenueGeneralLedgerTaxParameterMakerChecker")]
    public partial class RevenueGeneralLedgerTaxParameterMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short RevenueGeneralLedgerTaxParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual RevenueGeneralLedgerTaxParameter RevenueGeneralLedgerTaxParameter { get; set; }
    }
}
