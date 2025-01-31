using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("GeneralLedgerModification")]
    public partial class GeneralLedgerModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralLedgerModification()
        {
            GeneralLedgerModificationMakerCheckers = new HashSet<GeneralLedgerModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string GLCode { get; set; }

        [Required]
        [StringLength(20)]
        public string ExistingGLNumber { get; set; }

        public short GLNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfGL { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public bool EnableCashTransactionFromDemandDepositAccount { get; set; }

        public bool HasCustomerAccounts { get; set; }

        [Required]
        [StringLength(1)]
        public string BusinessOfficeAccess { get; set; }

        [Required]
        [StringLength(1)]
        public string CurrencyAccess { get; set; }

        [Required]
        [StringLength(1)]
        public string TransactionTypeAccess { get; set; }

        [Required]
        [StringLength(1)]
        public string CustomerTypeAccess { get; set; }

        public short ParentGLPrmKey { get; set; }

        [Required]
        [StringLength(3000)]
        public string ParentGLDescription { get; set; }

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

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerModificationMakerChecker> GeneralLedgerModificationMakerCheckers { get; set; }
    }
}
