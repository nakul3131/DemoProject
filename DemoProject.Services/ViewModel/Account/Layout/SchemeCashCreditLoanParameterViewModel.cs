using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeCashCreditLoanParameterViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool IsRequiredDemandDepositAccount { get; set; }

        public bool EnableFixedDepositAsCollateral { get; set; }

        public bool EnableRealEstateAsCollateral { get; set; }

        public bool EnableExtraCollateral { get; set; }

        public byte SanctionLoanAndTurnOverProportion { get; set; }

        public decimal MarginBetweenStockAndWithdrawal { get; set; }

        public bool EnableFineInterestAfterMaturity { get; set; }

        public bool IsRequiredStockList { get; set; }

        public byte BalanceConfirmationCertificateTimePeriod { get; set; }

        public byte PastFinancialYearStatements { get; set; }

        public byte PastIncomeTaxReturnStatements { get; set; }

        public byte PastAssesmentOrders { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string DebtEquityRatio { get; set; }

        [Required]
        [StringLength(1)]
        public string WorkingCapitalCycle { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeCashCreditLoanParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
