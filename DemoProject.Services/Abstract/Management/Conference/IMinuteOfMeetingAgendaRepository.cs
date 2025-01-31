using DemoProject.Services.ViewModel.Management.Conference;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMinuteOfMeetingAgendaRepository
    {
        // Amend MinuteOfMeetingAgenda Delete Entry - If Entry Rejected
        Task<bool> Amend(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);

        // Delete MinuteOfMeetingAgenda - Only For Rejected Entry
        Task<bool> Delete(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);

        // Return Rejected Entries
        Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From MinuteOfMeetingAgenda Table Which Are Not Authorized
        Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From MinuteOfMeetingAgenda Table For Modification
        Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfVerifiedEntries();

        // Return Valid List From MinuteOfMeetingAgenda Table Which Are Not Authorized
        Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfCreate();

        //int GetPrmKeyById(Guid _minuteOfMeetingId);

        // Return Rejected Entry
        Task<MinuteOfMeetingAgendaViewModel> GetRejectedEntry(Guid _meetingAgendaId);

        // Return Record From MinuteOfMeetingAgenda Table By Given Parameter (i.e. MeetingAgendaId)
        Task<MinuteOfMeetingAgendaViewModel> GetUnVerifiedEntry(Guid _meetingAgendaId);

        // Return Record From MinuteOfMeetingAgenda Table By Given Parameter (i.e. MeetingAgendaId)
        Task<MinuteOfMeetingAgendaViewModel> GetVerifiedEntry(Guid _meetingAgendaId); 

        // Reject MinuteOfMeetingAgenda Entry
        Task<bool> Reject(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);

        // Save MinuteOfMeetingAgenda New Entry
        Task<bool> Save(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);

        // Save MinuteOfMeetingAgenda Modification New Entry
        Task<bool> Modify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);

        // Authorize MinuteOfMeetingAgenda Entry
        Task<bool> Verify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel);  
    }
}
