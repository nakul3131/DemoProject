using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DemoProject.Services.ViewModel.Management.Servant;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(EmployeeViewModel _employeeViewModel);

        Task<IEnumerable<EmployeeIndexViewModel>> GetEmployeeIndex(string _entryType);

        Task<EmployeeViewModel> GetEmployeeEntry(Guid _employeeId, string _entryType);

        Task<bool> GetSessionValues(int _employeePrmKey, string _entryType);

        //  Save New Entry
        Task<bool> Save(EmployeeViewModel _employeeViewModel);

        // Verify Entry
        Task<bool> VerifyRejectDelete(EmployeeViewModel _employeeViewModel, string _entryType);

    }
}