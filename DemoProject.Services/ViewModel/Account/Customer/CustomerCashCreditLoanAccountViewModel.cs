using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerCashCreditLoanAccountViewModel
    {
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

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        //CustomerCashCreditLoanAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerCashCreditLoanAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Others
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
