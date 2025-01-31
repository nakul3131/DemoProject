using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IScheduleFrequencyRepository
    {
        short GetPrmKeyById(Guid _scheduleFrequencyId);

        // Return Rejected ScheduleFrequency Entries
        Task<IEnumerable<ScheduleFrequencyViewModel>> GetRejectedEntries(short _schedulePrmKey);

        // Return UnVerified ScheduleFrequency Entries
        Task<IEnumerable<ScheduleFrequencyViewModel>> GetUnverifiedEntries(short _schedulePrmKey);

        // Return Verified ScheduleFrequency Entries
        Task<IEnumerable<ScheduleFrequencyViewModel>> GetVerifiedEntries(short _schedulePrmKey);

    }
}
