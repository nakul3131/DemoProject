using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonSocialMediaViewModel
    {
        // PersonSocialMedia
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short SocialMediaPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(500)]
        public string SocialMediaLink { get; set; }

        [StringLength(2500)]
        public string OtherDetails { get; set; }

        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PersonSocialMediaMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonSocialMediaPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid SocialMediaId { get; set; }

        [StringLength(100)]
        public string NameOfSocialMedia { get; set; }

    }
}