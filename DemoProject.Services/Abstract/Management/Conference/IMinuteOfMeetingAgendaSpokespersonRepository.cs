using DemoProject.Services.ViewModel.Management.Conference;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMinuteOfMeetingAgendaSpokespersonRepository
    {
        // Droupdown List Values
        List<SelectListItem> MeetingAgendaDropdownList { get; }

        // Return Rejected MinuteOfMeetingAgendaSpokesperson Entries
        Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetRejectedEntries(int _meetingAgendaPrmKey);

        // Return UnVerified MinuteOfMeetingAgendaSpokesperson Entries
        Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetUnverifiedEntries(int _meetingAgendaPrmKey);

        // Return Verified MinuteOfMeetingAgendaSpokesperson Entries
        Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetVerifiedEntries(int _meetingAgendaPrmKey); 
    }
}
