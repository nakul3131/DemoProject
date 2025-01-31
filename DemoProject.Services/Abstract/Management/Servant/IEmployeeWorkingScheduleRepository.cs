using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeWorkingScheduleRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeeWorkingScheduleViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeeWorkingScheduleViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeeWorkingScheduleViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
