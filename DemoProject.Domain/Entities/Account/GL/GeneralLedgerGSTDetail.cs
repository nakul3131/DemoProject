using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("GeneralLedgerGSTDetail")]
    public partial class GeneralLedgerGSTDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralLedgerGSTDetail()
        {
            GeneralLedgerGSTDetailMakerCheckers = new HashSet<GeneralLedgerGSTDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool IsService { get; set; }

        [Required]
        [StringLength(20)]
        public string HsnSacCode { get; set; }

        public bool IsAllowReverseChargeMechanism { get; set; }

        public bool IsEligibleForInputTaxCredit { get; set; }

        public decimal TaxRate { get; set; }

        public decimal CGSTRate { get; set; }

        public decimal SGSTRate { get; set; }

        public decimal IGSTRate { get; set; }

        public decimal CessRate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerGSTDetailMakerChecker> GeneralLedgerGSTDetailMakerCheckers { get; set; }
    }
}
