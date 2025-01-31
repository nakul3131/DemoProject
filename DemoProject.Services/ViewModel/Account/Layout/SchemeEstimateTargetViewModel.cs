using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeEstimateTargetViewModel
    {
        // SchemeEstimateTarget

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal NumberOfAccount { get; set; }

        [StringLength(1)]
        public string NumberOfAccountUnit { get; set; }

        public decimal TurnOverAmount { get; set; }

        [StringLength(1)]
        public string TurnOverUnit { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeEstimateTargetMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeEstimateTargetPrmKey { get; set; }

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
