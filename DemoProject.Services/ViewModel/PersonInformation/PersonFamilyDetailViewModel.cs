using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonFamilyDetailViewModel
    {
        //PersonFamilyDetail
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        [StringLength(150)]
        public string FullNameOfFamilyMember { get; set; }

        public byte RelationPrmKey { get; set; }

        public DateTime BirthDate { get; set; }

        public short OccupationPrmKey { get; set; }

        public decimal Income { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonFamilyDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonFamilyDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonFamilyDetailTranslation
        //public Guid PersonFamilyDetailTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }
        
        [StringLength(150)]
        public string TransFullNameOfFamilyMember { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //PersonFamilyDetailTranslationMakerChecker

        public long PersonFamilyDetailTranslationPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        public string PersonInformationNumberText { get; set; }

        //Other

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

        public Guid RelationId { get; set; }

        public Guid OccupationId { get; set; }

        [StringLength(100)]
        public string NameOfRelation { get; set; }

        [StringLength(100)]
        public string NameOfOccupation { get; set; }

    }
}
