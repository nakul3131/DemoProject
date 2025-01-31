using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class FinancialCycleViewModel
    {

        // FinancialCycle

        public byte PrmKey { get; set; }

        public Guid FinancialCycleId { get; set; }

        [StringLength(9)]
        public string FinancialCycleCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(3)]
        public string FinancialCycleStatus { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // FinancialCycleMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte FinancialCyclePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // PeriodCode

        public Guid PeriodCodeId { get; set; }

        [StringLength(5)]
        public string Code { get; set; } 

        [StringLength(3)]
        public string PeriodCodeStatus { get; set; }

        // PeriodCodeMakerChecker

        public short PeriodCodePrmKey { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
