using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class EventMasterViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public EventMasterViewModel()
        {
            mlDetailRepository       = DependencyResolver.Current.GetService<IMLDetailRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        // EventMaster

        public short PrmKey { get; set; }

        public Guid EventMasterId { get; set; }

        public short EventTypePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfEvent { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(1500)]
        public string EventDescription { get; set; }

        public DateTime ScheduledFrom { get; set; }

        public DateTime ScheduledTo { get; set; }

        public DateTime TriggeredAt { get; set; }

        public bool IsFullDayEvent { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemGenerated { get; set; }

        [StringLength(2048)]
        public string RedirectUrl { get; set; }

        public bool EnableReminder { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        // EventMasterTranslation

        public Guid EventMasterTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(100)]
        public string TransNameOfEvent { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [StringLength(100)]
        public string NameOfEventType { get; set; }

        public List<SelectListItem> EventTypeDropdownList
        {
            get
            {
                return managementDetailRepository.EventTypeDropdownList;
            }
        }
        
        
    }
}
