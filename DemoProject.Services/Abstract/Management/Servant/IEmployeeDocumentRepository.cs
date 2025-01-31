using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeDocumentRepository
    {
        //List<SelectListItem> EmployeeTypeDropdownList { get; }

        //int GetPrmKeyById(Guid _employeeTypeId);

        // Return EmployeeDocuments Rejected Entries
        Task<IEnumerable<EmployeeDocumentViewModel>> GetRejectedEntries(int _employeePrmKey);

        // Return Valid List From EmployeeDocuments Table Which Are Not Authorized
        Task<IEnumerable<EmployeeDocumentViewModel>> GetUnverifiedEntries(int _employeePrmKey);

        // Return Valid List From EmployeeDocuments Table Which Are Authorized
        Task<IEnumerable<EmployeeDocumentViewModel>> GetVerifiedEntries(int _employeePrmKey);
    }
}
