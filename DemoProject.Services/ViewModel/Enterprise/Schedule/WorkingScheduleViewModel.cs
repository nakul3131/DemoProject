using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Enterprise.Schedule
{
    public class WorkingScheduleViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public WorkingScheduleViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // WorkingSchedule
        public byte PrmKey { get; set; }

        public Guid WorkingScheduleId { get; set; }

        [StringLength(100)]
        public string NameOfSchedule { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan WorkingDuration { get; set; }

        public TimeSpan MorningTeaTime { get; set; }

        public TimeSpan MorningTeaTimeDuration { get; set; }

        public TimeSpan LunchTime { get; set; }

        public TimeSpan LunchTimeDuration { get; set; }

        public TimeSpan EveningTeaTime { get; set; }

        public TimeSpan EveningTeaTimeDuration { get; set; }

        public TimeSpan DinnerTime { get; set; }

        public TimeSpan DinnerTimeDuration { get; set; }

        public byte WeeklyHoliday1 { get; set; }

        [StringLength(3)]
        public string WeeklyHoliday1Occurance { get; set; }

        public byte WeeklyHoliday2 { get; set; }

        [StringLength(3)]
        public string WeeklyHoliday2Occurance { get; set; }

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

        // WorkingScheduleMakerChecker

        public DateTime EntryDateTime { get; set; }

        public byte WorkingSchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // WorkingScheduleModification

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // WorkingScheduleModificationMakerChecker
        public byte WorkingScheduleModificationPrmKey { get; set; }

        //WorkingSchedule Translation

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

        // WorkingScheduleTranslationMakerChecker
        public short WorkingScheduleTranslationPrmKey { get; set; }

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

        public Guid WeeklyHoliday1Id { get; set; }

        public Guid WeeklyHoliday2Id { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // List<SelectListItem> For Dropdownlist
        public List<SelectListItem> DaysOfWeekDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfWeekDropdownList;
            }

        }
    }
}
