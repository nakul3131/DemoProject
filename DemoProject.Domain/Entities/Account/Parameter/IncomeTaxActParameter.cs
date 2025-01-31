using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Parameter
{
    [Table("IncomeTaxActParameter")]
    public partial class IncomeTaxActParameter
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal AcceptanceOfCashLoansOrDeposits { get; set; }

        public decimal RepaymentOfCashLoansOrDeposits { get; set; }

        public decimal CashTransactionLimitsPerDayForIncomeTaxReporting { get; set; }

        public decimal CashTransactionLimitsPerYearForIncomeTaxReporting { get; set; }

        public decimal CashWithdrawalTDSLimitForFilers { get; set; }

        public decimal FilersTDSPercentageForCashWithdrawal { get; set; }

        public decimal CashWithdrawalTDSLimitForNonFilersSlab1 { get; set; }

        public decimal NonFilersTDSPercentageForCashWithdrawalSlab1 { get; set; }

        public decimal CashWithdrawalTDSLimitForNonFilersSlab2 { get; set; }

        public decimal NonFilersTDSPercentageForCashWithdrawalSlab2 { get; set; }

        public decimal CashPaymentLimitForExpensesAndAssets { get; set; }

        public decimal DepositInterestThresholdLimitInFiscalYear { get; set; }

        public decimal DepositInterestThresholdLimitForSeniorCitizenInFiscalYear { get; set; }

        public decimal TDSRateOnDepositInterestForPANHolder { get; set; }

        public decimal TDSRateOnDepositInterestForNonPANHolder { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
