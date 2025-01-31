using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Conference;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingRepository
    {
        // Amend Meeting Delete Entry - If Entry Rejected
        Task<bool> Amend(MeetingViewModel _meetingViewModel);

        // Delete Meeting - Only For Rejected Entry
        Task<bool> Delete(MeetingViewModel _meetingViewModel);

        // Return Rejected Entries
        Task<IEnumerable<MeetingViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Meeting Table Which Are Not Authorized
        Task<IEnumerable<MeetingViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Meeting Table For Modification
        Task<IEnumerable<MeetingViewModel>> GetIndexOfVerifiedEntries();

        int GetPrmKeyById(Guid _meetingId);

        // All Valid Meeting Droupdown List 
        List<SelectListItem> MeetingDropdownList { get; } 

        // Return Rejected Entry
        Task<MeetingViewModel> GetRejectedEntry(Guid _meetingId);
        
        // Return Record From Meeting Table By Given Parameter (i.e. MeetingId)
        Task<MeetingViewModel> GetUnVerifiedEntry(Guid _meetingId);

        // Return Record From Meeting Table By Given Parameter (i.e. MeetingId)
        Task<MeetingViewModel> GetVerifiedEntry(Guid _meetingId); 

        // Reject Meeting Entry
        Task<bool> Reject(MeetingViewModel _meetingViewModel);

        // Save Meeting New Entry
        Task<bool> Save(MeetingViewModel _meetingViewModel);

        // Save Meeting Modification New Entry
        Task<bool> Modify(MeetingViewModel _meetingViewModel);

        // Authorize Meeting Entry
        Task<bool> Verify(MeetingViewModel _meetingViewModel); 
    }
}
