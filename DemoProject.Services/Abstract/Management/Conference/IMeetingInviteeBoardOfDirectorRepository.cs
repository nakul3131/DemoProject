using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Conference;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingInviteeBoardOfDirectorRepository
    {
        int GetPrmKeyById(Guid _meetingId); 

        List<SelectListItem> MeetingInviteeBoardOfDirectorDropdownList { get; } 

        // Return Rejected MeetingInviteeBoardOfDirector Entries
        Task<IEnumerable<MeetingInviteeBoardOfDirectorViewModel>> GetRejectedEntries(int _meetingPrmKey);

        // Return UnVerified MeetingInviteeBoardOfDirector Entries
        Task<IEnumerable<MeetingInviteeBoardOfDirectorViewModel>> GetUnverifiedEntries(int _meetingPrmKey);

        // Return Verified MeetingInviteeBoardOfDirector Entries
        Task<IEnumerable<MeetingInviteeBoardOfDirectorViewModel>> GetVerifiedEntries(int _meetingPrmKey);
    }
}
