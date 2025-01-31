using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class VehicleVariantViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;

        public VehicleVariantViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // VehicleVariant

        public short PrmKey { get; set; }

        public Guid VehicleVariantId { get; set; }
 
        public short VehicleModelPrmKey { get; set; }

        [StringLength(100)]
        public string NameOfVariant { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(3)]
        public string EngineType { get; set; }

        public short EngineCapacity { get; set; }

        [StringLength(3)]
        public string Transmission { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // VehicleVariantMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short VehicleVariantPrmKey { get; set; }
        
        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // VehicleVariantModification

        public Guid VehicleVariantModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // VehicleVariantModificationMakerChecker

        public short VehicleVariantModificationPrmKey { get; set; }

        // VehicleVariantTranslation

        public Guid VehicleVariantTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfVariant { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        // VehicleVariantTranslationMakerChecker
        
        public short VehicleVariantTranslationPrmKey { get; set; }
        
        // Translation In Regional

        [StringLength(100)]
        public string NameOfVariantInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Variant");
            }
        }

        [StringLength(100)]
        public string NameOfVariantPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Variant");
            }
        }

        [StringLength(100)]
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

        [StringLength(100)]
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

        [StringLength(100)]
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
        public Guid VehicleBodyTypeId { get; set; }

        public Guid VehicleModelId { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

    }
}
