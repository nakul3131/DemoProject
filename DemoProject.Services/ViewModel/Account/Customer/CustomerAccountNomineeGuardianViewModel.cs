using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountNomineeGuardianViewModel
    {
        // CustomerAccountNomineeGuardian

        public int PrmKey { get; set; }

        public long CustomerAccountNomineePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        public long PersonInformationNumber { get; set; }

        public byte GuardianTypePrmKey { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(500)]
        public string FullAddress { get; set; }

        [StringLength(150)]
        public string ContactDetails { get; set; }

        [StringLength(3)]
        public string AgeProofSubmissionStatusOfTheMinor { get; set; }

        public DateTime AppointedDateOfContact { get; set; }

        public TimeSpan AppointedTimeOfContact { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // CustomerAccountNomineeGuardianMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountNomineeGuardianPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerAccountNomineeGuardianTranslation
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(150)]
        public string TransFullName { get; set; }

        [StringLength(500)]
        public string TransFullAddress { get; set; }

        [StringLength(150)]
        public string TransContactDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        // CustomerAccountNomineeGuardianTranslationMakerChecker
        public int CustomerAccountNomineeGuardianTranslationPrmKey { get; set; }

        // Other
        [StringLength(50)]
        public string NominationNumber { get; set; }

        [StringLength(30)]
        public string AgeProofSubmissionStatusOfTheMinorText { get; set; }

        public Guid GuardianTypeId { get; set; }

        public string NameOfGuardianType { get; set; }

        public string NameOfGuardianPersonInformationNumber { get; set; }
    }
}
