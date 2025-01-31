using DemoProject.Services.Abstract.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Master.General.Notice
{
    public class MonthScheduleViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;

        public MonthScheduleViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        //NoticeScheduleOnDaysOfMonth

        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfMonthId { get; set; }

        public int MonthPrmKey { get; set; }

        public int DayOfMonthPrmKey { get; set; }

        public bool IsEveryMonth { get; set; }

        public short MonthInterval { get; set; }

        //NoticeScheduleOnDaysOfMonthMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public short NoticeScheduleOnDaysOfMonthPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //NoticeScheduleOnDaysOfMonthTime

        public Guid NoticeScheduleOnDaysOfMonthTimeId { get; set; }

        public TimeSpan MonthScheduleTime { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //NoticeScheduleOnDaysOfMonthTimeMakerChecker

        public short NoticeScheduleOnDaysOfMonthTimePrmKey { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(100)]
        public string NameOfMonth { get; set; }

        [StringLength(100)]
        public string NameOfMonthDay { get; set; }
        
        public Guid MonthId { get; set; }
        
        public Guid MonthDayId { get; set; }

        public List<SelectListItem> Months
        {
            get
            {
                return managementDetailRepository.DaysOfMonthDropdownList;
            }
        }

        public List<SelectListItem> MonthDays
        {
            get
            {
                return managementDetailRepository.DaysOfMonthDropdownList;
            }
        }

    }
}
