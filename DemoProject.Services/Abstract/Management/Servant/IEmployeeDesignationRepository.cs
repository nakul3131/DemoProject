using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeDesignationRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeeDesignationViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeeDesignationViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeeDesignationViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
