using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountSweepDetail")]
    public partial class CustomerAccountSweepDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountSweepDetail()
        {
            CustomerAccountSweepDetailMakerCheckers = new HashSet<CustomerAccountSweepDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public decimal MinimumBalanceToTriggerSweepIn { get; set; }

        public decimal MaximumAmountToTriggerSweep { get; set; }

        public decimal MinimumTermDepositAmount { get; set; }

        public decimal MaximumTermDepositAmount { get; set; }

        public decimal MinimumTermDepositTenure { get; set; }

        public decimal MaximumTermDepositTenure { get; set; }

        public decimal DefaultTermDepositTenure { get; set; }

        public decimal MaximumNumberOfSweepOut { get; set; }

        public bool EnableAutoRenew { get; set; }

        public byte SweepOutFrequencyPrmKey { get; set; }

        public bool EnableOnBeginingOfDay { get; set; }

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

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual SweepOutFrequency SweepoutFrequency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountSweepDetailMakerChecker> CustomerAccountSweepDetailMakerCheckers { get; set; }
    }
}
