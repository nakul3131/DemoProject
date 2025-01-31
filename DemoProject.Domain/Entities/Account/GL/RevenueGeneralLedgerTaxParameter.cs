using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("RevenueGeneralLedgerTaxParameter")]
    public partial class RevenueGeneralLedgerTaxParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RevenueGeneralLedgerTaxParameter()
        {
            RevenueGeneralLedgerTaxParameterMakerCheckers = new HashSet<RevenueGeneralLedgerTaxParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short RevenueGeneralLedgerParameterPrmKey { get; set; }

        public short ModificationNumber { get; set; }

        public short TaxGeneralLedgerPrmKey { get; set; }

        [Required]
        [StringLength(1)]
        public string TaxAmountType { get; set; }

        public decimal TaxAmount { get; set; }

        public byte RoundTaxAmount { get; set; }

        public byte FloorTaxAmount { get; set; }

        public byte CeilingTaxAmount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual RevenueGeneralLedgerParameter RevenueGeneralLedgerParameter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueGeneralLedgerTaxParameterMakerChecker> RevenueGeneralLedgerTaxParameterMakerCheckers { get; set; }
    }
}
