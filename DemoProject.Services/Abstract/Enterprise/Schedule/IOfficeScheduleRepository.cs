using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Schedule;

namespace DemoProject.Services.Abstract.Enterprise.Schedule
{
    public interface IOfficeScheduleRepository
    {
        // Amend OfficeSchedule Delete Entry - If Entry Rejected
        Task<bool> Amend(OfficeScheduleViewModel _OfficeScheduleViewModel);

        // Delete OfficeSchedule - Only For Rejected Entry
        Task<bool> Delete(OfficeScheduleViewModel _OfficeScheduleViewModel);

        // Return Rejected Entries
        Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From OfficeSchedule Table Which Are Not Authorized
        Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From OfficeSchedule Table For Modification
        Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<OfficeScheduleViewModel> GetRejectedEntry(Guid _OfficeScheduleId);

        bool GetUniqueOfficeScheduleName(string _nameOfOfficeSchedule);

        Guid GetOfficeScheduleIdByPrmKey(int _prmKey);

        // Return Record From OfficeSchedule Table By Given Parameter (i.e. OfficeScheduleId)
        Task<OfficeScheduleViewModel> GetUnVerifiedEntry(Guid _OfficeScheduleId);

        // Return Record From OfficeSchedule Table By Given Parameter (i.e. OfficeScheduleId)
        Task<OfficeScheduleViewModel> GetVerifiedEntry(Guid _OfficeScheduleId);

        // Reject OfficeSchedule Entry
        Task<bool> Reject(OfficeScheduleViewModel _OfficeScheduleViewModel);

        // Save OfficeSchedule New Entry
        Task<bool> Save(OfficeScheduleViewModel _OfficeScheduleViewModel);

        // Save OfficeSchedule Modification New Entry
        Task<bool> Modify(OfficeScheduleViewModel _OfficeScheduleViewModel);

        // Authorize OfficeSchedule Entry
        Task<bool> Verify(OfficeScheduleViewModel _OfficeScheduleViewModel);

    }
}
