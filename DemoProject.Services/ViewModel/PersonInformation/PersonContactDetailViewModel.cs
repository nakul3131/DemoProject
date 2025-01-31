using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonContactDetailViewModel
    {
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte ContactTypePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string FieldValue { get; set; }

        public bool IsVerified { get; set; }

        [Required]
        [StringLength(50)]
        public string VerificationCode { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //Maker Checker

        public DateTime EntryDateTime { get; set; }

        public long PersonContactDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //Other
        public long CustomerAccountContactDetailPrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

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

        public bool IsUpdated { get; set; }

        public Guid ContactTypeId { get; set; }

        [StringLength(100)]
        public string NameOfContactType { get; set; }

    }
}
