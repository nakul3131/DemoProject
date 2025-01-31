using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class ScheduleFrequencyViewModel
    {
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMLDetailRepository mlDetailRepository;

        public ScheduleFrequencyViewModel()
        {
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
        }

        // ScheduleFrequency

        public short PrmKey { get; set; }

        public Guid ScheduleFrequencyId { get; set; }

        public short SchedulePrmKey { get; set; }

        public byte ScheduleTypePrmKey { get; set; }

        public short NumberOfDays { get; set; }

        public byte DaysOfWeekPrmKey { get; set; }

        public byte DaysOfMonthPrmKey { get; set; }

        public DateTime SpecifiedDate { get; set; }

        public TimeSpan ScheduleTime { get; set; }

        public short Recur { get; set; }

        public bool IsEvery { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
        
        //ScheduleFrequencyMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public short ScheduleFrequencyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
       
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }
        
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
