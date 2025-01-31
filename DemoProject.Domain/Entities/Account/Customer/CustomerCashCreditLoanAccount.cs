using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerCashCreditLoanAccount")]
    public partial class CustomerCashCreditLoanAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public CustomerCashCreditLoanAccount()
        {
            CustomerCashCreditLoanAccountMakerCheckers = new HashSet<CustomerCashCreditLoanAccountMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public decimal PercentageOfOwnershipHeldByApplicant { get; set; }

        public decimal AnnualTurnOver { get; set; }

        public decimal CurrentYearProjectedTurnover { get; set; }

        public bool IsProfitableAsPerLatestPLStatement { get; set; }                  

        public decimal ProfitLossAmount { get; set; }

        public decimal PreviousYearTurnOver { get; set; }

        public decimal PreviousSecondYearTurnOver { get; set; }

        public decimal PreviousThirdYearTurnOver { get; set; }

        public decimal PreviousYearGrossProfitMargin { get; set; }

        public decimal PreviousSecondYearGrossProfitMargin { get; set; }

        public decimal PreviousThirdYearGrossProfitMargin { get; set; }

        public decimal PreviousYearNetProfitMargin { get; set; }

        public decimal PreviousSecondYearNetProfitMargin { get; set; }

        public decimal PreviousThirdYearNetProfitMargin { get; set; }

        public decimal DebtEquityRatio { get; set; }

        public short WorkingCapitalCycle { get; set; }

        public decimal ValueOfCollateral { get; set; }

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
        public virtual ICollection<CustomerCashCreditLoanAccountMakerChecker> CustomerCashCreditLoanAccountMakerCheckers { get; set; }

    }
}
