using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerTermDepositAccountDetail")]
    public partial class CustomerTermDepositAccountDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerTermDepositAccountDetail()
        {
            CustomerTermDepositAccountDetailMakerCheckers = new HashSet<CustomerTermDepositAccountDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int CertificateNumber { get; set; }

        public decimal DepositAmount { get; set; }

        [StringLength(3)]
        public string MaturityInstruction { get; set; }

        [StringLength(3)]
        public string InterestPayoutFrequency { get; set; }

        public decimal InterestPayoutAmount { get; set; }

        public byte InterestPayoutDay { get; set; }

        public decimal TotalInterestAmount { get; set; }

        public decimal MaturityAmount { get; set; }

        public short GracePeriodForRenewal { get; set; }

        public bool EnableAutoRenewOnMaturity { get; set; }

        public short AutoRenewWaitingTimePeriod { get; set; }

        public byte RenewTypePrmKey { get; set; }

        public decimal CustomRenewAmount { get; set; }

        public short RenewTenure { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerDepositAccount CustomerDepositAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTermDepositAccountDetailMakerChecker> CustomerTermDepositAccountDetailMakerCheckers { get; set; }
    }
}
