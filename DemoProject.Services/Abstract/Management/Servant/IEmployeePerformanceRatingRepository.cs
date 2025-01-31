using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeePerformanceRatingRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeePerformanceRatingViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeePerformanceRatingViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeePerformanceRatingViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
