using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ScheduleViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public ScheduleViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();

        }

        //Schedule

        public short PrmKey { get; set; }

        public Guid ScheduleId { get; set; }

        [StringLength(100)]
        public string NameOfSchedule { get; set; }
        
        [StringLength(10)]
        public string AliasName { get; set; }
        
        [StringLength(100)]
        public string NameOnReport { get; set; }

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

        //ScheduleMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //ScheduleModification
        public Guid ScheduleModificationId { get; set; }
        
        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //ScheduleModificationMakerChecker
        public short ScheduleModificationPrmKey { get; set; }

        //ScheduleTranslation
        public Guid ScheduleTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }
        
        [StringLength(100)]
        public string TransNameOfSchedule { get; set; }
        
        [StringLength(10)]
        public string TransAliasName { get; set; }
        
        [StringLength(100)]
        public string TransNameOnReport { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //ScheduleTranslationMakerChecker

        public short ScheduleTranslationPrmKey { get; set; }

        // ScheduleFrequency
        public Guid ScheduleFrequencyId { get; set; }
        
        public byte ScheduleTypePrmKey { get; set; }

        public short NumberOfDays { get; set; }

        public byte DaysOfWeekPrmKey { get; set; }

        public byte DaysOfMonthPrmKey { get; set; }

        public DateTime SpecifiedDate { get; set; }

        public short Recur { get; set; }

        public bool IsEvery { get; set; }
        
        //ScheduleFrequencyMakerChecker
        
        public short ScheduleFrequencyPrmKey { get; set; }
        
        //ScheduleFrequencyTiming

        public Guid ScheduleFrequencyTimingId { get; set; }
        
        public TimeSpan ScheduleTime { get; set; }

        //ScheduleFrequencyTimingMakerChecker
        public short ScheduleFrequencyTimingPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfScheduleInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Schedule");
            }
        }

        [StringLength(100)]
        public string NameOfSchedulePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Schedule");
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
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem

        public Guid ScheduleTypeId { get; set; }

        public Guid DaysOfWeekId { get; set; }

        public Guid DaysOfMonthId { get; set; }

        [StringLength(100)]
        public string NameOfScheduleType { get; set; }

        [StringLength(100)]
        public string NameOfWeekDay { get; set; }

        [StringLength(100)]
        public string NameOfMonthDay { get; set; }


        // List<SelectListItem> For Dropdownlist

        public List<SelectListItem> ScheduleTypeDropdownList
        {
            get
            {
                return managementDetailRepository.ScheduleTypeDropdownList;
            }

        }

        public List<SelectListItem> DaysOfWeekDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfWeekDropdownList;
            }

        }

        public List<SelectListItem> DaysOfMonthDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfMonthDropdownList;
            }

        }

    }
}
