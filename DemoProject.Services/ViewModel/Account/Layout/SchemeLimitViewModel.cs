using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLimitViewModel
    {
        // SchemeLimit

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal CashDepositLimit { get; set; }

        public decimal CashWithdrawalLimit { get; set; }

        public decimal RetailAccountTurnOverLimit { get; set; }

        public decimal CorporateAccountTurnOverLimit { get; set; }

        public decimal RetailHoldingAmountProportionToTotalAmount { get; set; }

        public decimal CorporateHoldingAmountProportionToTotalAmount { get; set; }

        public decimal TurnOverLimit { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeLimitMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLimitPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

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
