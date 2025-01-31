using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class PeriodCodeViewModel
    {
        // PeriodCode
        public short PrmKey { get; set; }

        public Guid PeriodCodeId { get; set; }

        public byte FinancialCyclePrmKey { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(3)]
        public string PeriodCodeStatus { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PeriodCodeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short PeriodCodePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }
    }
}
