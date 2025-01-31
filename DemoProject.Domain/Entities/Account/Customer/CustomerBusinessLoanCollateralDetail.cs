using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerBusinessLoanCollateralDetail")]
    public partial class CustomerBusinessLoanCollateralDetail
    {
        public CustomerBusinessLoanCollateralDetail()
        {
            CustomerBusinessLoanCollateralDetailMakerCheckers = new HashSet<CustomerBusinessLoanCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte TotalBusinessExperience { get; set; }

        public decimal AnnualTurnOver { get; set; }

        public decimal PreviousYearProfit1 { get; set; }

        public decimal PreviousYearProfit2 { get; set; }

        public decimal PreviousYearProfit3 { get; set; }

        public decimal PreviousYearProfit4 { get; set; }

        public decimal PreviousYearProfit5 { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerBusinessLoanCollateralDetailMakerChecker> CustomerBusinessLoanCollateralDetailMakerCheckers { get; set; }
    }
}
