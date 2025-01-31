using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class AgentViewModel
    {
        private readonly IAgentRepository agentRepository;

        public AgentViewModel()
        {
            agentRepository = DependencyResolver.Current.GetService<IAgentRepository>();
        }

        //Agent
        public int PrmKey { get; set; }

        public Guid AgentId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //AgentMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int AgentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Dropdown
        public List<SelectListItem> AgentDropdownList
        {
            get
            {
                return agentRepository.AgentDropdownList;
            }
        }
    }
}
