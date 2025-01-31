using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositAccountClosureParameterViewModel
    {

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnablePrematureClosure { get; set; }

        public bool EnableIPenaltyInterestOnPreMatureClosing { get; set; }

        public bool EnableClosureInLockInPeriod { get; set; }

        public bool EnablePrematureClosureBeforeAdvanceDepositPeriod { get; set; }

        public bool EnableExtendMaturity { get; set; }

        public bool EnableInactiveAccountExtendMaturity { get; set; }

        public short MinimumExtendPeriod { get; set; }

        public short MaximumExtendPeriod { get; set; }

        public bool EnableAutoClosureOfInactivityOfAccount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositAccountClosureParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositAccountClosureParameterPrmKey { get; set; }

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
