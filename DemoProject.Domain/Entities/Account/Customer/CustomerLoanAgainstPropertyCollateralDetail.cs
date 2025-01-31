using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAgainstPropertyCollateralDetail")]
    public partial class CustomerLoanAgainstPropertyCollateralDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public CustomerLoanAgainstPropertyCollateralDetail()
        {
            CustomerLoanAgainstPropertyCollateralDetailMakerCheckers = new HashSet<CustomerLoanAgainstPropertyCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short CenterPrmKey { get; set; }

        public byte PropertyAge { get; set; }

        public decimal EstimatedPropertyValue { get; set; }

        public decimal DownPaymentAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string PropertyCondition { get; set; }


        [Required]
        [StringLength(3)]
        public string PropertyUsage { get; set; }

        public bool HasExistingPropertyLiabilities { get; set; }

        public decimal OutstandingLoanAmount { get; set; }

        public short RemainingTerm { get; set; }

        public decimal MonthlyRepaymentAmount { get; set; }

        public bool IsPropertyLegallyRegistered { get; set; }

        public bool IsPropertyFreeOfAnyLegalDisputes { get; set; }

        public bool HasMortgageInsurance { get; set; }

        public decimal MortgageInsuranceAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string PropertyType { get; set; }

        [Required]
        [StringLength(500)]
        public string PropertyAddressLine1 { get; set; }

        [Required]
        [StringLength(500)]
        public string PropertyAddressLine2 { get; set; }

        [Required]
        [StringLength(500)]
        public string PropertyAddressLine3 { get; set; }

        [Required]
        [StringLength(1500)]
        public string PropertyProximityToKeyLandmarks { get; set; }

        [Required]
        [StringLength(3)]
        public string PropertyOwnershipStatus { get; set; }

        [Required]
        [StringLength(150)]
        public string LenderName { get; set; }

        [Required]
        [StringLength(1500)]
        public string AnyAdditionalLiens { get; set; }

        [Required]
        [StringLength(1500)]
        public string LegalDisputeDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string NeighborhoodInformation { get; set; }

        [Required]
        [StringLength(500)]
        public string TransportConnectivity { get; set; }

        [Required]
        [StringLength(500)]
        public string SecurityFeatures { get; set; }

        [Required]
        [StringLength(500)]
        public string UtilityAvailability { get; set; }

        [Required]
        [StringLength(30)]
        public string PropertyTypeOther { get; set; }

        [Required]
        [StringLength(30)]
        public string PropertyOwnershipStatusOther { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAgainstPropertyCollateralDetailMakerChecker> CustomerLoanAgainstPropertyCollateralDetailMakerCheckers { get; set; }
    }
}
