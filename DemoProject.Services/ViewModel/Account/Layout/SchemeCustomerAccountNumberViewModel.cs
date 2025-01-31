using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeCustomerAccountNumberViewModel
    {
        // SchemeCustomerAccountNumber

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableAccountNumberBranchwise { get; set; }

        [StringLength(25)]
        public string AccountNumberMask { get; set; }

        public long StartAccountNumber { get; set; }

        public long EndAccountNumber { get; set; }

        public int AccountNumberIncrementBy { get; set; }

        public bool EnableRandomAccountNumber { get; set; }

        public bool EnableAutoAccountNumber { get; set; }

        public bool EnableCustomizeAccountNumber { get; set; }

        public bool EnableReGenerateUnusedAccountNumber { get; set; }

        public bool EnableDigitalCodeForAccountNumber { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeCustomerAccountNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeCustomerAccountNumberPrmKey { get; set; }

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

        // Dropdown

        public string[] MaskTypeCharacterForAccountId { get; set; }

        public string[] MaskTypeCharacterForAccount { get; set; }

        public string MaskCharacter { get; set; }
    }
}
