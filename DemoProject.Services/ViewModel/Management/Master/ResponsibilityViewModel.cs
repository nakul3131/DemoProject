using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ResponsibilityViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IResponsibilityRepository responsibilityRepository;

        public ResponsibilityViewModel()
        {
            responsibilityRepository = DependencyResolver.Current.GetService<IResponsibilityRepository>();
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        public short PrmKey { get; set; }

        public Guid ResponsibilityId { get; set; }
         
        [StringLength(100)]
        public string NameOfResponsibility { get; set; }
         
        [StringLength(10)]
        public string AliasName { get; set; }
         
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short SequenceNumber { get; set; }
         
        [StringLength(50)]
        public string SequenceText { get; set; }

        public short ParentFunctionPrmKey { get; set; }

        public bool IsTitle { get; set; }

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

        //ResponsibilityMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short ResponsibilityPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
         
        [StringLength(3)]
        public string UserAction { get; set; }
         
        [StringLength(1500)]
        public string Remark { get; set; }

        //ResponsibilityModification

        public Guid ResponsibilityModificationId { get; set; }
         
        public byte ModificationNumber { get; set; }
            
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //ResponsibilityModificationMakerChecker

        public short ResponsibilityModificationPrmKey { get; set; }

        //ResponsibilityTranslation

        public Guid ResponsibilityTranslationId { get; set; }
         
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }
         
        [StringLength(100)]
        public string TransNameOfResponsibility { get; set; }
         
        [StringLength(10)]
        public string TransAliasName { get; set; }
         
        [StringLength(100)]
        public string TransNameOnReport { get; set; }
         
        [StringLength(50)]
        public string TransSequenceText { get; set; }
         
        [StringLength(1500)]
        public string TransNote { get; set; }
         
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //ResponsibilityTranslationMakerChecker

        public short ResponsibilityTranslationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfResponsibilityInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Responsibility");
            }
        }

        [StringLength(100)]
        public string NameOfResponsibilityPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Responsibility");
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
        public string SequenceNumberTextInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Sequence Number Text");
            }
        }

        [StringLength(100)]
        public string SequenceNumberTextPlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Sequence Number Text");
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

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // For SelectListItem

        public Guid ParentFunctionId { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> ResponsibilityDropdownList
        {
            get
            {
                return responsibilityRepository.ResponsibilityDropdownList;
            }
        }

    }
}
