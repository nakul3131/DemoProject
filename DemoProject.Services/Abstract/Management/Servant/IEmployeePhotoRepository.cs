using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeePhotoRepository
    {
        // Return GetRejectedEntries Entry
        Task<EmployeePhotoViewModel> GetRejectedEntries(int _employeePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<EmployeePhotoViewModel> GetUnVerifiedEntries(int _employeePrmKey);

        // Return GetVerifiedEntries Entry
        Task<EmployeePhotoViewModel> GetVerifiedEntries(int _employeePrmKey);
    }
}
