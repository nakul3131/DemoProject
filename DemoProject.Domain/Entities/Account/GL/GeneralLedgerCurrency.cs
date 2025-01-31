using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("GeneralLedgerCurrency")]
    public partial class GeneralLedgerCurrency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralLedgerCurrency()
        {
            GeneralLedgerCurrencyMakerCheckers = new HashSet<GeneralLedgerCurrencyMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

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
        public virtual ICollection<GeneralLedgerCurrencyMakerChecker> GeneralLedgerCurrencyMakerCheckers { get; set; }
    }
}
