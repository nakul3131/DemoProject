using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonViewModel 
    {
        // Person
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonInformationNumber { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string MothersMaidenName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfBirthOnDocument { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        public bool EnableGSTRegistrationDetails { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }
        
        // PersonMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // PersonModification
        //public Guid PersonModificationId { get; set; }
        
        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // PersonModificationMakerChecker
        public long PersonModificationPrmKey { get; set; }

        // PersonTranslation
        //public Guid PersonTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransFirstName { get; set; }

        [StringLength(50)]
        public string TransMiddleName { get; set; }

        [StringLength(50)]
        public string TransLastName { get; set; }

        [StringLength(150)]
        public string TransFullName { get; set; }

        [StringLength(50)]
        public string TransMotherName { get; set; }

        [StringLength(50)]
        public string TransMothersMaidenName { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // PersonTranslationMakerChecker
        public long PersonTranslationPrmKey { get; set; }

        // PersonPrefixViewModel
        public PersonPrefixViewModel PersonPrefixViewModel { get; set; }

        // PersonEmploymentDetailViewModel
        public PersonEmploymentDetailViewModel PersonEmploymentDetailViewModel { get; set; }

        // ForeignerViewModel
        public ForeignerViewModel ForeignerViewModel { get; set; }

        // GuardianPersonViewModel
        public GuardianPersonViewModel GuardianPersonViewModel { get; set; }

        // PersonAdditionalDetailViewModel
        public PersonAdditionalDetailViewModel PersonAdditionalDetailViewModel { get; set; }

        // PersonAdditionalIncomeDetailViewModel
        public PersonAdditionalIncomeDetailViewModel PersonAdditionalIncomeDetailViewModel { get; set; }

        // PersonAddressViewModel
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        // PersonAgricultureAssetViewModel
        public PersonAgricultureAssetViewModel PersonAgricultureAssetViewModel { get; set; }

        // PersonBankDetailViewModel
        public PersonBankDetailViewModel PersonBankDetailViewModel { get; set; }

        // PersonBoardOfDirectorRelationViewModel
        public PersonBoardOfDirectorRelationViewModel PersonBoardOfDirectorRelationViewModel { get; set; }

        // PersonBorrowingDetailViewModel
        public PersonBorrowingDetailViewModel PersonBorrowingDetailViewModel { get; set; }

        // PersonChronicDiseaseViewModel
        public PersonChronicDiseaseViewModel PersonChronicDiseaseViewModel { get; set; }

        // PersonCommoditiesAssetViewModel
        public PersonCommoditiesAssetViewModel PersonCommoditiesAssetViewModel { get; set; }

        // PersonContactDetailViewModel
        public PersonContactDetailViewModel PersonContactDetailViewModel { get; set; }

        // PersonCourtCaseViewModel
        public PersonCourtCaseViewModel PersonCourtCaseViewModel { get; set; }

        // PersonCreditRatingViewModel
        public PersonCreditRatingViewModel PersonCreditRatingViewModel { get; set; }

        // PersonCreditRatingViewModel
        public PersonGroupViewModel PersonGroupViewModel { get; set; }

        // PersonFamilyDetailViewModel
        public PersonFamilyDetailViewModel PersonFamilyDetailViewModel { get; set; }

        // PersonFinancialAssetViewModel
        public PersonFinancialAssetViewModel PersonFinancialAssetViewModel { get; set; }

        // PersonGSTRegistrationDetailViewModel
        public PersonGSTRegistrationDetailViewModel PersonGSTRegistrationDetailViewModel { get; set; }
        
        // PersonImmovableAssetViewModel
        public PersonImmovableAssetViewModel PersonImmovableAssetViewModel { get; set; }

        // PersonIncomeTaxDetailViewModel 
        public PersonIncomeTaxDetailViewModel PersonIncomeTaxDetailViewModel { get; set; }

        // PersonInsuranceDetailViewModel
        public PersonInsuranceDetailViewModel PersonInsuranceDetailViewModel { get; set; }

        // PersonKYCDocumentViewModel
        public PersonKYCDocumentViewModel PersonKYCDocumentViewModel { get; set; }

        // PersonMovableAssetViewModel
        public PersonMovableAssetViewModel PersonMovableAssetViewModel { get; set; }

        // PersonMachineryAssetViewModel
        public PersonMachineryAssetViewModel PersonMachineryAssetViewModel { get; set; }
        
        // PersonPhotoSignViewModel
        public PersonPhotoSignViewModel PersonPhotoSignViewModel { get; set; }

        // PersonSMSAlertViewModel
        public PersonSMSAlertViewModel PersonSMSAlertViewModel { get; set; }

        // PersonSocialMediaViewModel
        public PersonSocialMediaViewModel PersonSocialMediaViewModel { get; set; }

        // PersonHomeBranchViewModel
        public PersonHomeBranchViewModel PersonHomeBranchViewModel { get; set; }

        public PersonGroupAuthorizedSignatoryViewModel PersonGroupAuthorizedSignatoryViewModel { get; set; }

        // Other
        public Guid LanguageId { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

    }
}
