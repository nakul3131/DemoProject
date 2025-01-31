using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccountFunderDetail")]
    public partial class CustomerLoanAccountFunderDetail
    {
        public CustomerLoanAccountFunderDetail()
        {
            CustomerLoanAccountFunderDetailMakerCheckers = new HashSet<CustomerLoanAccountFunderDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessRole { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal RateOfInterest { get; set; }

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
        public virtual ICollection<CustomerLoanAccountFunderDetailMakerChecker> CustomerLoanAccountFunderDetailMakerCheckers { get; set; }
    }
}
