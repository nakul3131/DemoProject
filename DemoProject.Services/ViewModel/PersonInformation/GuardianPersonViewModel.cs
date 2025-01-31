using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class GuardianPersonViewModel
    {
        public long PrmKey { get; set; }

        //public Guid GuardianPersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        [StringLength(150)]
        public string PersonInformationNumberText { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(150)]
        public string GuardianFullName { get; set; }

        [StringLength(500)]
        public string FullAddress { get; set; }

        public byte RelationPrmKey { get; set; }

        [StringLength(1)]
        public string AgeProofSubmissionStatusOfTheMinor { get; set; }

        public DateTime AppointedDateOfContact { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //GuardianPersonMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long GuardianPersonPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //GuardianPersonTranslation
       
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransGuardianFullName { get; set; }

        [StringLength(500)]
        public string TransFullAddress { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //GuardianPersonTranslationMakerChecker
        
        public long GuardianPersonTranslationPrmKey { get; set; }
        
        //Person

        [StringLength(50)]
        public string FirstName { get; set; }

        public Guid PersonId { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public string NameOfRelation { get; set; }

        // For SelectListItem
        
        public Guid RelationId { get; set; }
    }
}
