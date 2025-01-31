using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationContactDetailViewModel
    {
        //OrganizationContactDetail
        public short PrmKey { get; set; }

        public Guid OrganizationContactDetailId { get; set; }
        
        public Guid ContactTypeId { get; set; }
        
        public Guid ContactGroupId { get; set; }

        public byte ContactTypePrmKey { get; set; }
        
        [StringLength(50)]
        public string FieldValue { get; set; }

        public byte ContactGroupPrmKey { get; set; }

        public bool IsOpen { get; set; }

        [StringLength(100)]
        public string NameOfContactType { get; set; }

        [StringLength(100)]
        public string NameOfContactGroup { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //MakerChecker

        public DateTime EntryDateTime { get; set; }

        public short OrganizationContactDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
