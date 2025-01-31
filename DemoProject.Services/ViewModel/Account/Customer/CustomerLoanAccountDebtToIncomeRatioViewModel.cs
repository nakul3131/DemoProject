using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
   public class CustomerLoanAccountDebtToIncomeRatioViewModel
    {
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
     
        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerLoanAccountDebtToIncomeRatioMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAccountDebtToIncomeRatioPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
        

    }
}
