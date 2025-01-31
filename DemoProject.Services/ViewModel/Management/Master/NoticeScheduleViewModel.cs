using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Master.General.Notice
{
    public class NoticeScheduleViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public NoticeScheduleViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        //NoticeSchedule

        public short PrmKey { get; set; }

        public Guid NoticeScheduleId { get; set; }

        [StringLength(50)]
        public string NameOfNoticeSchedule { get; set; }
        
        [StringLength(10)]
        public string AliasName { get; set; }
        
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short StartAfterDays { get; set; }

        public bool IsEveryDay { get; set; }

        public short IntervalBetweenDay { get; set; }

        public TimeSpan ScheduleTime { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //NoticeScheduleMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //NoticeScheduleOnDate

        public Guid NoticeScheduleOnDateId { get; set; }

        public DateTime ScheduleDate { get; set; }

        //NoticeScheduleOnDateMakerChecker

        public short NoticeScheduleOnDatePrmKey { get; set; }

        //NoticeScheduleOnDateTime

        public Guid NoticeScheduleOnDateTimeId { get; set; }

        public TimeSpan DateScheduleTime { get; set; }

        //NoticeScheduleOnDateTimeMakerChecker

        public short NoticeScheduleOnDateTimePrmKey { get; set; }

        //NoticeScheduleOnDaysOfMonth

        public Guid NoticeScheduleOnDaysOfMonthId { get; set; }

        public int DayOfMonthPrmKey { get; set; }

        public int MonthPrmKey { get; set; }

        public bool IsEveryMonth { get; set; }

        public short MonthInterval { get; set; }

        //NoticeScheduleOnDaysOfMonthMakerChecker

        public short NoticeScheduleOnDaysOfMonthPrmKey { get; set; }

        //NoticeScheduleOnDaysOfMonthTime

        public Guid NoticeScheduleOnDaysOfMonthTimeId { get; set; }

        public TimeSpan MonthScheduleTime { get; set; }

        //NoticeScheduleOnDaysOfMonthTimeMakerChecker

        public short NoticeScheduleOnDaysOfMonthTimePrmKey { get; set; }

        //NoticeScheduleOnDaysOfWeek

        public Guid NoticeScheduleOnDaysOfWeekId { get; set; }

        public int DayOfWeekPrmKey { get; set; }

        public bool IsEveryWeek { get; set; }

        public short WeekInterval { get; set; }

        //NoticeScheduleOnDaysOfWeekMakerChecker

        public short NoticeScheduleOnDaysOfWeekPrmKey { get; set; }

        //NoticeScheduleOnDaysOfWeekTime

        public Guid NoticeScheduleOnDaysOfWeekTimeId { get; set; }

        public TimeSpan WeekScheduleTime { get; set; }

        //NoticeScheduleOnDaysOfWeekTimeMakerChecker

        public short NoticeScheduleOnDaysOfWeekTimePrmKey { get; set; }

        //NoticeScheduleTranslation

        public Guid NoticeScheduleTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfNoticeSchedule { get; set; }
        
        [StringLength(10)]
        public string TransAliasName { get; set; }
        
        [StringLength(100)]
        public string TransNameOnReport { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //NoticeScheduleTranslationMakerChecker

        public short NoticeScheduleTranslationPrmKey { get; set; }

        //NoticeScheduleModification

        public Guid NoticeScheduleModificationId { get; set; }
        
        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //NoticeScheduleModificationMakerChecker

        public short NoticeScheduleModificationPrmKey { get; set; }

        // Translation In Regional

        [StringLength(100)]
        public string NameOfNoticeScheduleInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Name Of Notice Schedule");
            }
        }

        [StringLength(100)]
        public string NameOfNoticeSchedulePlaceHolderInRegionalLanguage
        {
            get
            {
                return mlDetailRepository.TranslateInRegionalLanguage("Enter Name Of Notice Schedule");
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

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }
        
        public Guid MonthId { get; set; }
        
        public Guid MonthDayId { get; set; }
        
        public Guid WeekDayId { get; set; }

        public List<SelectListItem> Months
        {
            get
            {
                return managementDetailRepository.DaysOfMonthDropdownList;
            }
        }

        public List<SelectListItem> DaysOfMonthDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfMonthDropdownList;
            }
        }

        public List<SelectListItem> DaysOfWeekDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfWeekDropdownList;
            }
        }

    }
}
