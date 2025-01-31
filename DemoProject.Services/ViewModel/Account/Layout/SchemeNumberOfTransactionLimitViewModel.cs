using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeNumberOfTransactionLimitViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public byte MinimumNumberOfTransaction { get; set; }

        public byte MaximumNumberOfTransaction { get; set; }

        public byte TimePeriodUnitPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeNumberOfTransactionLimitMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeNumberOfTransactionLimitPrmKey { get; set; }

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

        // Dropdown

        public Guid TransactionTypeId { get; set; }

        [StringLength(100)]
        public string NameOfTransactionType { get; set; }

        public Guid TimePeriodUnitId { get; set; }

        [StringLength(100)]
        public string NameOfTimePeriodUnit { get; set; }
    }
}
