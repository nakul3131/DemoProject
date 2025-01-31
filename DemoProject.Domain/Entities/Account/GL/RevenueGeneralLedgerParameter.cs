using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("RevenueGeneralLedgerParameter")]
    public partial class RevenueGeneralLedgerParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RevenueGeneralLedgerParameter()
        {
            RevenueGeneralLedgerParameterMakerCheckers = new HashSet<RevenueGeneralLedgerParameterMakerChecker>();
            RevenueGeneralLedgerTaxParameters = new HashSet<RevenueGeneralLedgerTaxParameter>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid RevenueGeneralLedgerParameterId { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short ModificationNumber { get; set; }

        public bool IsIncome { get; set; }

        public bool IsOperational { get; set; }

        public bool IsGoodsRevenue { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsITCAvailable { get; set; }

        public bool IsUnderRCM { get; set; }

        [Required]
        [StringLength(50)]
        public string SACHSNCode { get; set; }

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

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueGeneralLedgerParameterMakerChecker> RevenueGeneralLedgerParameterMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueGeneralLedgerTaxParameter> RevenueGeneralLedgerTaxParameters { get; set; }
    }
}
