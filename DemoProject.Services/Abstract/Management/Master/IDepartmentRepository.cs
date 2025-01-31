using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IDepartmentRepository
    {
        // Amend Department Delete Entry - If Entry Rejected
        Task<bool> Amend(DepartmentViewModel _departmentViewModel);

        // Delete Department - Only For Rejected Entry
        Task<bool> Delete(DepartmentViewModel _departmentViewModel);

        // Return Rejected Entries
        Task<IEnumerable<DepartmentViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Department Table Which Are Not Authorized
        Task<IEnumerable<DepartmentViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Department Table For Modification
        Task<IEnumerable<DepartmentViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<DepartmentViewModel> GetRejectedEntry(Guid _departmentId);

        bool GetUniqueDepartmentName(string _nameOfDepartment);

        Guid GetDepartmentIdByPrmKey(int _prmKey);

        // Return Record From Department Table By Given Parameter (i.e. _departmentId)
        Task<DepartmentViewModel> GetUnVerifiedEntry(Guid _departmentId);

        // Return Record From Department Table By Given Parameter (i.e. _departmentId)
        Task<DepartmentViewModel> GetVerifiedEntry(Guid _departmentId);

        // Save Department Modification New Entry
        Task<bool> Modify(DepartmentViewModel _departmentViewModel);

        // Reject Department Entry
        Task<bool> Reject(DepartmentViewModel _departmentViewModel);

        // Save Department New Entry
        Task<bool> Save(DepartmentViewModel _departmentViewModel);

        // Authorize Department Entry
        Task<bool> Verify(DepartmentViewModel _departmentViewModel);
    }
}
