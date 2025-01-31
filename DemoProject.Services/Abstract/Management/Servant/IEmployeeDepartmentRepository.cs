using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeDepartmentRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeeDepartmentViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeeDepartmentViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeeDepartmentViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
