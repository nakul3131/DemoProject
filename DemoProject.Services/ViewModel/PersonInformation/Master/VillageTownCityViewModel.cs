using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class VillageTownCityViewModel
    {
        // Center
        public short PrmKey { get; set; }

        public Guid CenterId { get; set; }

        public byte CenterCategoryPrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCenter { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short ParentCenterPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // CenterMakerCkecker
        public DateTime EntryDateTime { get; set; }

        public short CenterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CenterModification
        public Guid CenterModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // CenterModificationMakerChecker
        public short CenterModificationPrmKey { get; set; }

        // CenterTranslation 
        public Guid CenterTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCenter { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // CenterTranslationMakerChecker
        public short CenterTranslationPrmKey { get; set; }

        public CenterDemographicDetailViewModel CenterDemographicDetailViewModel { get; set; }

        public CenterIsoCodeViewModel CenterIsoCodeViewModel { get; set; }

        public CenterOccupationViewModel CenterOccupationViewModel { get; set; }

        public CenterTradingEntityDetailViewModel CenterTradingEntityDetailViewModel { get; set; }

        public TalukaViewModel TalukaViewModel { get; set; }

        // Other
        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public bool EnableDirection { get; set; }

        public bool EnableAreaType { get; set; }

        public bool EnablePopulation { get; set; }

        public bool EnablePerCapitaIncome { get; set; }

        public bool EnableEducationLevel { get; set; }

        public bool EnableFamilySystem { get; set; }

        public bool EnableNumberOfResidentsOwningHomes { get; set; }

        [StringLength(1)]
        public string IsMandatoryCenterISOCode { get; set; }

        public bool EnableISOAlphaNumericCode2 { get; set; }

        public bool EnableISOAlphaNumericCode3 { get; set; }

        public bool EnableISONumericCode { get; set; }

        public bool EnableOtherCode { get; set; }

        [StringLength(1)]
        public string IsMandatoryCenterOccupation { get; set; }

        [StringLength(1)]
        public string IsMandatoryCenterTradingDetail { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown
        public Guid AreaTypeId { get; set; }

        public Guid DirectionId { get; set; }

        public Guid EducationLevelId { get; set; }

        public Guid FamilySystemId { get; set; }

        public Guid LocalGovernmentId { get; set; }

        public Guid ParentCenterCountryId { get; set; }

        public Guid ParentCenterDistrictId { get; set; }

        public Guid ParentCenterDivisionId { get; set; }

        public Guid ParentCenterPostId { get; set; }

        public Guid ParentCenterStateId { get; set; }

        public Guid ParentCenterSubDivisionOfficeId { get; set; }

        public Guid ParentCenterTalukaId { get; set; }

        public Guid[] SelectedOccupationId { get; set; }

        public Guid TradingEntityId { get; set; }
    }
}