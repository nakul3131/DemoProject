using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerDepositAccount")]
    public partial class CustomerDepositAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerDepositAccount()
        {
            CustomerDepositAccountMakerCheckers = new HashSet<CustomerDepositAccountMakerChecker>();
            CustomerDepositAccountAgents = new HashSet<CustomerDepositAccountAgent>();
            CustomerTermDepositAccountDetails = new HashSet<CustomerTermDepositAccountDetail>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime? MaturityDate { get; set; }

        public bool EnableAutoCloseOnMaturity { get; set; }

        public byte AccountOperationModePrmKey { get; set; }

        public decimal DepositInstallmentAmount { get; set; }

        public byte InstallmentFrequencyPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDepositAccountMakerChecker> CustomerDepositAccountMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDepositAccountAgent> CustomerDepositAccountAgents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTermDepositAccountDetail> CustomerTermDepositAccountDetails { get; set; }
    }
}
