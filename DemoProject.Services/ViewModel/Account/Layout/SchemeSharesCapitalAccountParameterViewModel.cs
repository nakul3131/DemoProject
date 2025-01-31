using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeSharesCapitalAccountParameterViewModel
    {
        // SchemeSharesCapitalAccountParameter

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableMemberNumberBranchwise { get; set; }

        public bool EnableDigitalCodeForMemberNumber { get; set; }

        public bool IsVisibleMemberNumber { get; set; }

        [StringLength(25)]
        public string MemberNumberMask { get; set; }

        public bool EnableAutoMemberNumber { get; set; }

        public bool EnableCustomizeMemberNumber { get; set; }

        public int StartMemberNumber { get; set; }

        public int EndMemberNumber { get; set; }

        public int MemberNumberIncrementBy { get; set; }

        public bool EnableRandomMemberNumber { get; set; }

        public bool EnableReGenerateUnusedMemberNumber { get; set; }

        public short MinimumNumberOfShares { get; set; }

        public short MaximumNumberOfShares { get; set; }

        public short DefaultNumberOfShares { get; set; }

        public bool EnableDividend { get; set; }

        public bool EnableSharesTransferCharges { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeSharesCapitalAccountParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeSharesCapitalAccountParameterPrmKey { get; set; }

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

        public string[] MaskTypeCharacterForMember { get; set; }

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
