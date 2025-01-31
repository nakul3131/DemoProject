using DemoProject.Services.ViewModel.Management.Conference;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Management.Conference
{
    public interface IMeetingAllowanceRepository
    {
        // Amend MeetingAllowance Delete Entry - If Entry Rejected
        Task<bool> Amend(MeetingAllowanceViewModel _meetingAllowanceViewModel);

        // Delete MeetingAllowance - Only For Rejected Entry
        Task<bool> Delete(MeetingAllowanceViewModel _meetingAllowanceViewModel);

        //List<SelectListItem> DesignationDropdownList { get; }

        //List<SelectListItem> DesignationDropdownListForEmployeeCatgory { get; }

        // Return Rejected Entries
        Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From MeetingAllowance Table Which Are Not Authorized
        Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From MeetingAllowance Table For Modification
        Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfVerifiedEntries();

        //short GetPrmKeyById(Guid _DesignationId);

        // Return Rejected Entry
        Task<MeetingAllowanceViewModel> GetRejectedEntry(Guid _meetingAllowanceId);

        //bool GetUniqueDesignationName(string _nameOfDesignation);

        // Return Record From MeetingAllowance Table By Given Parameter (i.e. MeetingAllowanceId)
        Task<MeetingAllowanceViewModel> GetUnVerifiedEntry(Guid _meetingAllowanceId);

        // Return Record From MeetingAllowance Table By Given Parameter (i.e. MeetingAllowanceId)
        Task<MeetingAllowanceViewModel> GetVerifiedEntry(Guid _meetingAllowanceId);
         
        // Save MeetingAllowance Modification New Entry
        Task<bool> Modify(MeetingAllowanceViewModel _meetingAllowanceViewModel);

        // Reject MeetingAllowance Entry
        Task<bool> Reject(MeetingAllowanceViewModel _meetingAllowanceViewModel);

        // Save MeetingAllowance New Entry
        Task<bool> Save(MeetingAllowanceViewModel _meetingAllowanceViewModel);

        // Authorize MeetingAllowance Entry
        Task<bool> Verify(MeetingAllowanceViewModel _meetingAllowanceViewModel); 
    }
}
