using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositInstallmentParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }
        
        public byte MinimumInstallment { get; set; }

        public byte InstallmentMultipleOf { get; set; }

        public byte MaximumInstallment { get; set; }

        public bool IsAllowPartialInstallment { get; set; }

        public bool IsAllowAdvanceInstallment { get; set; }

        public bool EnableInstallmentAlteration { get; set; }

        public short DuesInstallmentForDefault { get; set; }

        public bool EnableGracePeriodForDuesInstallment { get; set; }

        public byte NumberOfOverdueInstallmentRecoveryFromLinkedAccount { get; set; }

        public bool EnableIPenaltyInterestOnOverdues { get; set; }

        public decimal FixedPenaltyAmount { get; set; }

        public decimal PenaltyAmountPerHunderd { get; set; }

        public short DuesInstallmentForInactivityOfAccount { get; set; }

        public short RevivePeriodForInactivityAccount { get; set; }

        public bool EnableAutoClosureOfInactivityOfAccount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositInstallmentParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositInstallmentParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Dropdown

        public Guid TimePeriodUnitId { get; set; }

        [StringLength(100)]
        public string NameOfTimePeriodUnit { get; set; }

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
