using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IAgentRepository
    {
        // Area Type Dropdown
        List<SelectListItem> AgentDropdownList { get; }

        // Get PrmKey By Id
        int GetPrmKeyById(Guid _agentId);
    }
}
