using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanInterestProvisionParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestCalculationFrequencyPrmKey { get; set; }

        public bool EnableCapitalization { get; set; }

        public bool EnableDueInterestCapitalization { get; set; }

        public bool EnableOverdueAccountInterestCalculation { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //MakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanInterestProvisionParameterPrmKey { get; set; }

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

        public Guid InterestCalculationFrequencyId { get; set; }

        [StringLength(100)]
        public string NameOfInterestCalculationFrequency { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGeneralLedger { get; set; }

    }
}
