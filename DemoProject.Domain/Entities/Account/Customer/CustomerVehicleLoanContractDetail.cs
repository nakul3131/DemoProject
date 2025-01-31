using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerVehicleLoanContractDetail")]
    public partial class CustomerVehicleLoanContractDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerVehicleLoanContractDetail()
        {
            CustomerVehicleLoanContractDetailMakerCheckers = new HashSet<CustomerVehicleLoanContractDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string ContractNature { get; set; }

        [Required]
        [StringLength(100)]
        public string OtherContractNatureDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string ContractObligations { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(500)]
        public string CompanyDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetail { get; set; }

        [Required]
        [StringLength(500)]
        public string AddressDetails { get; set; }

        public decimal ContractMonthlyAmount { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentFrequency { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentMode { get; set; }

        public byte PaymentDay { get; set; }

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
        public virtual ICollection<CustomerVehicleLoanContractDetailMakerChecker> CustomerVehicleLoanContractDetailMakerCheckers { get; set; }

    }
}
