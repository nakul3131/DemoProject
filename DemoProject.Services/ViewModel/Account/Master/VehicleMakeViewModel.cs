using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;

namespace DemoProject.Services.ViewModel.Account.Master
{
    public class VehicleMakeViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
 
        public VehicleMakeViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // VehicleMake

        public short PrmKey { get; set; }

        public Guid VehicleMakeId { get; set; }

        [StringLength(100)]
        public string NameOfVehicleMake { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short CenterPrmKey { get; set; }

         public Guid CenterId { get; set; }

        public short EstablishedYear { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // VehicleMakeMakerchecker 

        public DateTime EntryDateTime { get; set; }

        public short VehicleMakePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // VehicleMakeModification

        public Guid VehicleMakeModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // VehicleMakeModificationMakerChecker

        public short VehicleMakeModificationPrmKey { get; set; }

        // VehicleMakeTranslation

        public Guid VehicleMakeTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfVehicleMake { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // VehicleMakeTranslationMakerChecker

        public short VehicleMakeTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfVehicleMakeInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Vehicle Make");
            }
        }

        [StringLength(100)]
        public string NameOfVehicleMakePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Vehicle Make");
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
        public Guid VehicleMakeType { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

    }
}
