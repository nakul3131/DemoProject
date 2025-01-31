using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Enterprise.Schedule;

namespace DemoProject.Services.Abstract.Enterprise.Schedule
{
    public interface IWorkingScheduleRepository
    {
        // Amend WorkingSchedule Delete Entry - If Entry Rejected
        Task<bool> Amend(WorkingScheduleViewModel _workingScheduleViewModel);

        // Delete WorkingSchedule - Only For Rejected Entry
        Task<bool> Delete(WorkingScheduleViewModel _workingScheduleViewModel);

        // Return Rejected Entries
        Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From WorkingSchedule Table Which Are Not Authorized
        Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From WorkingSchedule Table For Modification
        Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfVerifiedEntries();

        byte GetPrmKeyById(Guid _officeScheduleId);

        // Return Rejected Entry
        Task<WorkingScheduleViewModel> GetRejectedEntry(Guid _workingScheduleId);

        bool GetUniqueWorkingScheduleName(string _nameOfWorkingSchedule);

        // Return Record From WorkingSchedule Table By Given Parameter (i.e. WorkingScheduleId)
        Task<WorkingScheduleViewModel> GetUnVerifiedEntry(Guid _workingScheduleId);

        // Return Record From WorkingSchedule Table By Given Parameter (i.e. WorkingScheduleId)
        Task<WorkingScheduleViewModel> GetVerifiedEntry(Guid _workingScheduleId);

        // Reject WorkingSchedule Entry
        Task<bool> Reject(WorkingScheduleViewModel _workingScheduleViewModel);

        // Save WorkingSchedule New Entry
        Task<bool> Save(WorkingScheduleViewModel _workingScheduleViewModel);

        // Save WorkingSchedule Modification New Entry
        Task<bool> Modify(WorkingScheduleViewModel _workingScheduleViewModel);

        // Authorize WorkingSchedule Entry
        Task<bool> Verify(WorkingScheduleViewModel _workingScheduleViewModel);

        // Droupdown List Values
        List<SelectListItem> WorkingSchedules { get; }

    }
}
