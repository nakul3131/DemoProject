using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountSweepDetailViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerAccountSweepDetailId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public decimal MinimumBalanceToTriggerSweepIn { get; set; }

        public decimal MaximumAmountToTriggerSweep { get; set; }

        public decimal MinimumTermDepositAmount { get; set; }

        public decimal MaximumTermDepositAmount { get; set; }

        public short MinimumTermDepositTenure { get; set; }

        public short MaximumTermDepositTenure { get; set; }

        public short DefaultTermDepositTenure { get; set; }

        public short MaximumNumberOfSweepOut { get; set; }

        public bool EnableAutoRenew { get; set; }

        public byte SweepOutFrequencyPrmKey { get; set; }

        public bool EnableOnBeginingOfDay { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //CustomerAccountSweepDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountSweepDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public Guid SweepOutFrequencyId { get; set; }



    }
}
