using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.GL
{
    [Table("GeneralLedger")]
    public partial class GeneralLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralLedger()
        {
            GeneralLedgerBusinessOffices = new HashSet<GeneralLedgerBusinessOffice>();
            GeneralLedgerCurrencies = new HashSet<GeneralLedgerCurrency>();
            GeneralLedgerCustomerTypes = new HashSet<GeneralLedgerCustomerType>();
            GeneralLedgerMakerCheckers = new HashSet<GeneralLedgerMakerChecker>();
            GeneralLedgerModifications = new HashSet<GeneralLedgerModification>();
            GeneralLedgerTransactionTypes = new HashSet<GeneralLedgerTransactionType>();
            GeneralLedgerTranslations = new HashSet<GeneralLedgerTranslation>();
            RevenueGeneralLedgerParameters = new HashSet<RevenueGeneralLedgerParameter>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [Required]
        [StringLength(20)]
        public string GLCode { get; set; }

        [Required]
        [StringLength(20)]
        public string ExistingGLNumber { get; set; }

        public int GLNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfGL { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short AccountClassPrmKey { get; set; }

        public bool HasCustomerAccounts { get; set; }

        public bool IsApplicableForTax { get; set; }

        public bool EnableCashTransactionFromDemandDepositAccount { get; set; }

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
        [StringLength(1500)]
        public string ParentGLDescription { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerBusinessOffice> GeneralLedgerBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerCurrency> GeneralLedgerCurrencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerCustomerType> GeneralLedgerCustomerTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerMakerChecker> GeneralLedgerMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerModification> GeneralLedgerModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerTransactionType> GeneralLedgerTransactionTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GeneralLedgerTranslation> GeneralLedgerTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueGeneralLedgerParameter> RevenueGeneralLedgerParameters { get; set; }
    }
}
