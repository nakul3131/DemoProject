using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeTargetGroupViewModel
    {
        // SchemeTargetGroup

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte TargetGroupPrmKey { get; set; }

        [StringLength(3)]
        public string RequiredMembership { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeTargetGroupMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeTargetGroupPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // SchemeTargetGroupGender
        public byte GenderPrmKey { get; set; }

        // SchemeTargetGroupGenderMakerChecker
        public short SchemeTargetGroupGenderPrmKey { get; set; }

        // SchemeTargetGroupOccupation


        private short _setString;

        public short SetString
        {
            get
            {
                return _setString;
            }
            set
            {
                OccupationPrmKey += value;
                _setString = value;
            }
        }
        public short OccupationPrmKey { get; set; }

        // SchemeTargetGroupOccupationMakerChecker
        public short SchemeTargetGroupOccupationPrmKey { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid TargetGroupId { get; set; }

        [StringLength(100)]
        public string NameOfTargetGroup { get; set; }

        public Guid GenderId { get; set; }

        [StringLength(100)]
        public string NameOfGender { get; set; }

        public Guid OccupationId { get; set; }

        [StringLength(100)]
        public string NameOfOccupation { get; set; }

        [StringLength(30)]
        public string RequiredMembershipText { get; set; }
    }
}
