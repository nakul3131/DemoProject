using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeDetailRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeeDetailViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeeDetailViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeeDetailViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
