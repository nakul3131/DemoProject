using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanAgreementNumberViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAgreementNumberBranchwise { get; set; }

        [StringLength(25)]
        public string AgreementNumberMask { get; set; }

        public int StartAgreementNumber { get; set; }

        public int EndAgreementNumber { get; set; }

        public int AgreementNumberIncrementBy { get; set; }

        public bool EnableRandomAgreementNumber { get; set; }

        public bool EnableAutoAgreementNumber { get; set; }

        public bool EnableCustomizeAgreementNumber { get; set; }

        public bool EnableReGenerateUnusedAgreementNumber { get; set; }

        public bool EnableDigitalCodeForAgreementNumber { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeLoanAgreementNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanAgreementNumberPrmKey { get; set; }

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

        // ****  For What Purpose This Property Is Added

        public string[] MaskTypeCharacterForAgreementId { get; set; }

        public string[] MaskTypeCharacterForAgreement { get; set; }

        public string MaskCharacter { get; set; }
    }
}
