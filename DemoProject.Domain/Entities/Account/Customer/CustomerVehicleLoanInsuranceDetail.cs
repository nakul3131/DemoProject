using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerVehicleLoanInsuranceDetail")]
    public partial class CustomerVehicleLoanInsuranceDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerVehicleLoanInsuranceDetail()
        {
            CustomerVehicleLoanInsuranceDetailMakerCheckers = new HashSet<CustomerVehicleLoanInsuranceDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short InsuranceCompanyPrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; }

        public DateTime CommencementDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        public string TypeOfCoverage { get; set; }

        public bool HasAddedZeroDepreciation { get; set; }

        public bool HasAddedEngineProtection { get; set; }

        public bool HasAddedReturnToInvoice { get; set; }

        public bool HasAddedRoadsideAssistance { get; set; }

        public decimal PolicyPremium { get; set; }

        public decimal SumInsured { get; set; }

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
        public virtual ICollection<CustomerVehicleLoanInsuranceDetailMakerChecker> CustomerVehicleLoanInsuranceDetailMakerCheckers { get; set; }
    }
}
