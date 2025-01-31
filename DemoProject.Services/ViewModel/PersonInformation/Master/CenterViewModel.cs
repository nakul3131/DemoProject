using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation.Master
{
    public class CenterViewModel
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public CenterViewModel()
        {
            personDetailRepository = DependencyResolver.Current.GetService<IPersonDetailRepository>();
            mlDetailRepository    = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        //Center
        public short PrmKey { get; set; }

        public Guid CenterId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfCenter { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string OtherNameAsParent { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public int CenterCategory { get; set; }

        public Guid CenterCategoryId { get; set; }

        public int Direction { get; set; }

        public Guid DirectionId { get; set; }

        public int LocalGovernment { get; set; }

        public Guid LocalGovernmentId { get; set; }

        public int PinCode { get; set; }

        public short ParentCenterPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CenterMakerCkecker

        public DateTime EntryDateTime { get; set; }

        public short CenterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CenterDemographicDetail

        public Guid CenterDemographicDetailId { get; set; }

        public Guid AreaTypeId { get; set; }

        public byte AreaTypePrmKey { get; set; }

        public long TotalPopulation { get; set; }

        public decimal PerCapitaIncome { get; set; }

        public Guid EducationalLevelId { get; set; }

        [Required]
        public int EducationalLevel { get; set; }

        public Guid FamilySystemId { get; set; }

        [Required]
        public int FamilySystem { get; set; }

        public long NumberOfResidentsOwningHomes { get; set; }

        // CenterDemographicDetailMakerChecker

        public short CenterDemographicDetailPrmKey { get; set; }

        // CenterISOCode 

        public Guid CenterISOCodeId { get; set; }

        [Required]
        [StringLength(2)]
        public string ISOAlphaNumericCode2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ISOAlphaNumericCode3 { get; set; }

        public short ISONumericCode { get; set; }

        [Required]
        [StringLength(20)]
        public string OtherCode { get; set; }

        // CenterISOCodeMakerChecker

        public short CenterISOCodePrmKey { get; set; }

        // CenterOccupation

        public Guid CenterOccupationId { get; set; }

        public Guid OccupationId { get; set; }

        public short OccupationPrmKey { get; set; }

        public Guid[] SelectedOccupationId { get; set; }

        // CenterOccupationMakerChecker

        public short CenterOccupationPrmKey { get; set; }

        // CenterTradingEntityDetail 

        public Guid CenterTradingEntityDetailId { get; set; }

        public Guid TradingEntityId { get; set; }

        public short TradingEntityPrmKey { get; set; }

        public long Volume { get; set; }

        // CenterTradingDetailMakerChecker

        public short CenterTradingEntityDetailPrmKey { get; set; }

        // CountryAdditionalDetail

        public Guid CountryAdditionalDetailId { get; set; }

        public Guid WorldWideTimeZoneId { get; set; }

        public Guid CurrencyId { get; set; }

        public byte MinorAge { get; set; }

        public short WorldWideTimeZonePrmKey { get; set; }

        public short InternationalDialingCodes { get; set; }

        public short CurrencyPrmKey { get; set; }

        // CountryAdditionalDetailMakerChecker

        public short CountryAdditionalDetailPrmKey { get; set; }

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
        public string TransOtherNameAsParent { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        // CenterTranslationMakerChecker

        public short CenterTranslationPrmKey { get; set; }

        // CenterModification

        public Guid CenterModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // CenterModificationMakerChecker

        public short CenterModificationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfCenterInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Center");
            }
        }

        [StringLength(100)]
        public string NameOfCenterPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Center");
            }
        }

        [StringLength(10)]
        public string AliasNameInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Alias Name");
            }
        }

        [StringLength(100)]
        public string AliasNamePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Alias Name");
            }
        }

        [StringLength(100)]
        public string OtherNameAsParentInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Other Name As Parent");
            }
        }

        [StringLength(100)]
        public string OtherNameAsParentPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Other Name As Parent");
            }
        }

        [StringLength(100)]
        public string NameOnReportInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name On Report");
            }
        }

        [StringLength(100)]
        public string NameOnReportPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name On Report");
            }
        }

        [StringLength(1500)]
        public string NoteInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Note");
            }
        }

        [StringLength(100)]
        public string NotePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Note");
            }
        }

        [StringLength(1500)]
        public string ReasonForModificationInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Reason For Modification");
            }
        }

        [StringLength(100)]
        public string ReasonForModificationPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Reason For Modification");
            }
        }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // List<SelectListItem> For Dropdownlist

        public List<SelectListItem> Centers
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> CenterCategories
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> TradingEntities
        {
            get
            {
                return personDetailRepository.CityDropdownList;
            }
        }

        public List<SelectListItem> WorldWideTimeZones
        {
            get
            {
                return personDetailRepository.CityDropdownList;
            }
        }

        public List<SelectListItem> Directions
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> LocalGovernments
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> EducationLevels
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }

        public List<SelectListItem> FamilySystems
        {
            get
            {
                return personDetailRepository.VillageTownCityDropdownList;
            }
        }
    }

    [Flags]
    public enum SelectedTables
    {
        CenterIsoCodeViewModel = 1,
        CenterDemographicDetail = 2
    };
}
