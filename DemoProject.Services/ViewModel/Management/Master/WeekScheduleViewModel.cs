using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Master.General.Notice
{
    public class WeekScheduleViewModel
    {
        private IManagementDetailRepository managementDetailRepository;

        public WeekScheduleViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        //NoticeScheduleOnDaysOfWeek

        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfWeekId { get; set; }

        public int DayOfWeekPrmKey { get; set; }

        public bool IsEveryWeek { get; set; }

        public short WeekInterval { get; set; }

        //NoticeScheduleOnDaysOfWeekMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short NoticeScheduleOnDaysOfWeekPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //NoticeScheduleOnDaysOfWeekTime

        public Guid NoticeScheduleOnDaysOfWeekTimeId { get; set; }

        public TimeSpan WeekScheduleTime { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //NoticeScheduleOnDaysOfWeekTimeMakerChecker

        public short NoticeScheduleOnDaysOfWeekTimePrmKey { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(100)]
        public string NameOfWeekDay { get; set; }
        
        public Guid WeekDayId { get; set; }

        public List<SelectListItem> DaysOfWeekDropdownList
        {
            get
            {
                return managementDetailRepository.DaysOfWeekDropdownList;
            }
        }
    }
}
