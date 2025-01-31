using DemoProject.Services.ViewModel.Management.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IScheduleRepository
    {
        // Amend Schedule Delete Entry - If Entry Rejected
        Task<bool> Amend(ScheduleViewModel _scheduleViewModel);

        // Delete Schedule - Only For Rejected Entry
        Task<bool> Delete(ScheduleViewModel _scheduleViewModel);
        
        // Return Rejected Entries
        Task<IEnumerable<ScheduleViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Schedule Table Which Are Not Authorized
        Task<IEnumerable<ScheduleViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Schedule Table For Modification
        Task<IEnumerable<ScheduleViewModel>> GetIndexOfVerifiedEntries();

        byte GetlistofScheduleType(Guid ScheduleTypeId);

        // Return Rejected Entry
        Task<ScheduleViewModel> GetRejectedEntry(Guid _scheduleId);

        bool GetUniqueScheduleName(string _nameOfSchedule);

        // Return Record From Schedule Table By Given Parameter (i.e. ScheduleId)
        Task<ScheduleViewModel> GetUnVerifiedEntry(Guid _scheduleId);

        // Return Record From Schedule Table By Given Parameter (i.e. ScheduleId)
        Task<ScheduleViewModel> GetVerifiedEntry(Guid _scheduleId);

        // Save Schedule Modification New Entry
        Task<bool> Modify(ScheduleViewModel _scheduleViewModel);

        // Reject Schedule Entry
        Task<bool> Reject(ScheduleViewModel _scheduleViewModel);

        // Save Schedule New Entry
        Task<bool> Save(ScheduleViewModel _scheduleViewModel);

        // Authorize Schedule Entry
        Task<bool> Verify(ScheduleViewModel _scheduleViewModel);
    }
}
