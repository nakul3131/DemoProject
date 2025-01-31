using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Customer;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("GoldLoanRate")]
    public partial class GoldLoanRate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldLoanRate()
        {
            CustomerGoldLoanCollateralDetails = new HashSet<CustomerGoldLoanCollateralDetail>();
            GoldLoanRateMakerCheckers = new HashSet<GoldLoanRateMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid GoldLoanRateId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string Purity { get; set; }

        public decimal MarketRatePerGram { get; set; }

        public decimal LoanAmountPerGram { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanCollateralDetail> CustomerGoldLoanCollateralDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldLoanRateMakerChecker> GoldLoanRateMakerCheckers { get; set; }
    }
}
