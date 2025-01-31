using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonAdditionalDetailViewModel
    {
        // PersonAdditionalDetail
        public long PrmKey { get; set; }
     
        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte PersonTypePrmKey { get; set; }

        public byte PersonCategoryPrmKey { get; set; }

        public short BirthCityPrmKey { get; set; }

        public bool IsResident { get; set; }

        public bool HasOtherCountryResidentStatus { get; set; }

        public byte GenderPrmKey { get; set; }

        public byte BloodGroupPrmKey { get; set; }

        public byte MaritalStatusPrmKey { get; set; }

        public DateTime? DateOfMarriage { get; set; }

        [StringLength(50)]
        public string LifePartnerName { get; set; }

        [StringLength(50)]
        public string LifePartnerMaidenName { get; set; }

        public short CastCategoryPrmKey { get; set; }

        public short EducationalQualificationPrmKey { get; set; }

        public short OccupationPrmKey { get; set; }

        public byte PhysicalStatusPrmKey { get; set; }

        public byte PovertyStatusPrmKey { get; set; }

        public bool IsEmployee { get; set; }

        public bool IsPolitician { get; set; }

        [StringLength(1500)]
        public string PoliticialBackgroundDetails { get; set; }

        public byte VIPRank { get; set; }

        [StringLength(1500)]
        public string VIPBackgroundDetails { get; set; }

        public bool IsSubmitedForm60 { get; set; }

        public bool IsIncomeTaxPayer { get; set; }

        public bool IsSubmitedForm15G { get; set; }

        public bool IsSubmitedForm15H { get; set; }

        public bool IsGSTTaxPayer { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonAdditionalDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonAdditionalDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonAdditionalDetailTranslation
               
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLifePartnerName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLifePartnerMaidenName { get; set; }
        
        [StringLength(1500)]
        public string TransPoliticialBackgroundDetails { get; set; }
        
        [StringLength(1500)]
        public string TransVIPBackgroundDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //PersonAdditionalDetailTranslationMakerChecker

        public long PersonAdditionalDetailTranslationPrmKey { get; set; }

        // Other
        public Guid PersonCategoryId { get; set; }

        //Other
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        public short ParentOccupationPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        // For SelectListItem

        public Guid PersonTypeId { get; set; }

        public Guid BirthCityId { get; set; }

        public Guid GenderId { get; set; }

        public Guid BloodGroupId { get; set; }

        public Guid MaritalStatusId { get; set; }

        public Guid PovertyStatusId { get; set; }

        public Guid PhysicalStatusId { get; set; }

        public Guid CastCategoryId { get; set; }

        public Guid EducationalQualificationId { get; set; }

        public Guid OccupationId { get; set; }

        public Guid CityId { get; set; }

        public Guid EmploymentTypeId { get; set; }

        public Guid NatureOfEmployerId { get;set;}

        public Guid DesignationId { get; set; }

    }
}
