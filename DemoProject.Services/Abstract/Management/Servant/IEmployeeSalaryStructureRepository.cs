using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeSalaryStructureRepository
    {
        // Return EmployeeSalaryStructures Rejected Entries
        Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetRejectedEntries(int _employeePrmKey);

        // Return Valid List From EmployeeSalaryStructures Table Which Are Not Authorized
        Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetUnverifiedEntries(int _employeePrmKey);

        // Return Valid List From EmployeeSalaryStructures Table Which Are Authorized
        Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetVerifiedEntries(int _employeePrmKey);
    }
}
