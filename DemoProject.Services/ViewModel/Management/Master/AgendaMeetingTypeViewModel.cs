using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.Services.ViewModel.Management.Master
{
    public class AgendaMeetingTypeViewModel
    {       
        private readonly IAgendaRepository agendaRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public AgendaMeetingTypeViewModel ()
        {
            agendaRepository = DependencyResolver.Current.GetService<IAgendaRepository>();
            managementDetailRepository = DependencyResolver.Current.GetService<IManagementDetailRepository>();
        }

        //AgendaMeetingType

        public int PrmKey { get; set; }

        public Guid AgendaMeetingTypeId { get; set; }

        public int AgendaPrmKey { get; set; }

        public byte MeetingTypePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }
        
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int AgendaMeetingTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem

        public Guid AgendaId { get; set; }

        public Guid MeetingTypeId { get; set; }

        [StringLength(100)]
        public string NameOfAgenda { get; set; }
        
        [StringLength(100)]
        public string NameOfMeetingType { get; set; }
        
        // List<SelectListItem> For Dropdownlist

        public List<SelectListItem> AgendaDropdownList
        {
            get
            {
                return agendaRepository.AgendaDropdownList;
            }

        }

        public List<SelectListItem> MeetingTypeDropdownList
        {
            get
            {
                return managementDetailRepository.MeetingTypeDropdownList;
            }

        }


    }
}
