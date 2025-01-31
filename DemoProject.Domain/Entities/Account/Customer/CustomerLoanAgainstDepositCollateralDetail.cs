using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAgainstDepositCollateralDetail")]
    public partial class CustomerLoanAgainstDepositCollateralDetail
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanAgainstDepositCollateralDetail()
        {
            CustomerLoanAgainstDepositCollateralDetailMakerCheckers = new HashSet<CustomerLoanAgainstDepositCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public decimal MortgageAmount { get; set; }

        public bool IsLoanClosed { get; set; }

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
        
        public virtual CustomerDepositAccount CustomerDepositAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAgainstDepositCollateralDetailMakerChecker> CustomerLoanAgainstDepositCollateralDetailMakerCheckers { get; set; }

    }
}
