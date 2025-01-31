using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccountDebtToIncomeRatio")]
    public partial class CustomerLoanAccountDebtToIncomeRatio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanAccountDebtToIncomeRatio()
        {
            CustomerLoanAccountDebtToIncomeRatioMakerCheckers = new HashSet<CustomerLoanAccountDebtToIncomeRatioMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public decimal MonthlyIncome { get; set; }

        public decimal MonthlyRentPayments { get; set; }

        public decimal MonthlyExpenseForTaxes { get; set; }

        public decimal MonthlyExpenseForInsurance { get; set; }

        public decimal EducationalLoanEMI { get; set; }

        public decimal PersonalLoanEMI { get; set; }

        public decimal CoSignedLoanEMI { get; set; }

        public decimal VehicleLoanEMI { get; set; }

        public decimal MinimumCreditCardPayments { get; set; }

        public decimal MonthlyCarPayments { get; set; }

        public decimal MonthlyTimeSharePayments { get; set; }

        public decimal MonthlyChildSupportPayment { get; set; }

        public decimal MonthlyAlimonyPayment { get; set; }

        public decimal DebtToIncomeRatio { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountDebtToIncomeRatioMakerChecker> CustomerLoanAccountDebtToIncomeRatioMakerCheckers { get; set; }
    }
}
