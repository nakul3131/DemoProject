using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Conference;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingInviteeMemberRepository
    {
        int GetPrmKeyById(Guid _meetingId);

        List<SelectListItem> MeetingInviteeMemberDropdownList { get; } 

        // Return Rejected MeetingInviteeMember Entries
        Task<IEnumerable<MeetingInviteeMemberViewModel>> GetRejectedEntries(int _meetingPrmKey);

        // Return UnVerified MeetingInviteeMember Entries
        Task<IEnumerable<MeetingInviteeMemberViewModel>> GetUnverifiedEntries(int _meetingPrmKey);

        // Return Verified MeetingInviteeMember Entries
        Task<IEnumerable<MeetingInviteeMemberViewModel>> GetVerifiedEntries(int _meetingPrmKey);
    }
}
