using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Conference;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingAgendaRepository
    {
        int GetPrmKeyById(Guid _meetingAgendaId);  

        List<SelectListItem> MeetingAgendaDropdownList { get; } 

        // Return Rejected MeetingAgenda Entries
        Task<IEnumerable<MeetingAgendaViewModel>> GetRejectedEntries(int _meetingPrmKey);

        // Return UnVerified MeetingAgenda Entries
        Task<IEnumerable<MeetingAgendaViewModel>> GetUnverifiedEntries(int _meetingPrmKey);

        // Return Verified MeetingAgenda Entries
        Task<IEnumerable<MeetingAgendaViewModel>> GetVerifiedEntries(int _meetingPrmKey); 
    }
}
