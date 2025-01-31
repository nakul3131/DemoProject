using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeMemberNumberViewModel
    {
        // BusinessOfficeMemberNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoMemberNumber { get; set; }

        
        [StringLength(25)]
        public string MemberNumberMask { get; set; }

        public int StartMemberNumber { get; set; }

        public int EndMemberNumber { get; set; }

        public int MemberNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedMemberNumber { get; set; }

        public bool EnableRandomMemberNumber { get; set; }

        public bool EnableCustomizeMemberNumber { get; set; }

        public bool EnableDigitalCodeForMemberNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeMemberNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeMemberNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Mask
        public string[] MaskTypeCharacterForMemberId { get; set; }

        public string[] MaskTypeCharacterForMember { get; set; }
    }
}
