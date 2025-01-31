using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerVehicleLoanPermitDetail")]
    public partial class CustomerVehicleLoanPermitDetail
    {
        public CustomerVehicleLoanPermitDetail()
        {
            CustomerVehicleLoanPermitDetailMakerCheckers = new HashSet<CustomerVehicleLoanPermitDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string PermitType { get; set; }

        [Required]
        [StringLength(500)]
        public string PermitDetails { get; set; }

        public DateTime PermitIssueDate { get; set; }

        public DateTime PermitExpiryDate { get; set; }

        [Required]
        [StringLength(150)]
        public string IssuingAuthority { get; set; }

        public decimal PermitAmountPerMonth { get; set; }

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
        public virtual ICollection<CustomerVehicleLoanPermitDetailMakerChecker> CustomerVehicleLoanPermitDetailMakerCheckers { get; set; }

    }
}
