using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanFineInterestParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte NumberOfMissedInstallment { get; set; }

        public short FineDays { get; set; }

        public decimal RateOfFineInterest { get; set; }

        [StringLength(1)]
        public string RateOfFineInterestUnit { get; set; }

        public byte InterestRateChargedDurationPrmKey { get; set; }

        public byte DaysInYearPrmKey { get; set; }

        public byte LendingRepaymentsInterestCalculationPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //MakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanFineInterestParameterPrmKey { get; set; }

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

        public Guid InterestMethodId { get; set; }

        [StringLength(100)]
        public string NameOfInterestMethod { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGeneralLedger { get; set; }

        public Guid InterestRateChargedDurationId { get; set; }

        [StringLength(100)]
        public string NameOfInterestRateChargedDuration { get; set; }

        public Guid LendingRepaymentsInterestCalculationId { get; set; }

        [StringLength(500)]
        public string NameOfEvent { get; set; }

        public Guid DaysInYearId { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

    }
}
