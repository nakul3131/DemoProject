using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositAccountParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [StringLength(3)]
        public string DepositType { get; set; }

        public bool IsRequiredOrdinaryMembership { get; set; }

        public bool IsRequiredNominalMembership { get; set; }

        public bool IsRequiredSavingAccount { get; set; }

        public bool EnableOverrideMaturedAmount { get; set; }

        public bool EnableLockinPeriod { get; set; }

        public bool EnableRenewal { get; set; }

        public bool IsAvailablePledgeLoan { get; set; }

        public bool EnableNumberOfTransactionLimit { get; set; }

        public bool EnableTransactionAmountLimit { get; set; }

        public bool EnableBankingChannel { get; set; }

        public bool EnableAgent { get; set; }

        public bool EnableAgentIncentiveParameter { get; set; }

        public bool IsUnderCGAS { get; set; }

        public bool IsApplicableForTaxExempt { get; set; }

        public decimal MaximumTaxExemptAmount { get; set; }

        public bool EnableTDSDeductionOnInterest { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositAccountParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositAccountParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

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
