using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Conference;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingNoticeRepository
    {
        int GetPrmKeyById(Guid _meetingId);

        List<SelectListItem> MeetingAgendaDropdownList { get; }

        // Return Rejected MeetingNotice Entries
        Task<IEnumerable<MeetingNoticeViewModel>> GetRejectedEntries(int _meetingPrmKey);

        // Return UnVerified MeetingNotice Entries
        Task<IEnumerable<MeetingNoticeViewModel>> GetUnverifiedEntries(int _meetingPrmKey);

        // Return Verified MeetingNotice Entries 
        Task<IEnumerable<MeetingNoticeViewModel>> GetVerifiedEntries(int _meetingPrmKey);
    }
}
