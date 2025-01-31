using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeTenureViewModel
    {
        // SchemeTenure

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short MinimumTenure { get; set; }

        public short MultiplesOf { get; set; }

        public short MaximumTenure { get; set; }

        public short DefaultTenure { get; set; }

        public byte TimePeriodUnitPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeTenureMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeTenurePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList

        public Guid TimePeriodUnitId { get; set; }

        [StringLength(100)]
        public string NameOfUnit { get; set; }

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
