using DemoProject.Services.ViewModel.Management.Servant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IServantDetailRepository
    {
        Task<IEnumerable<EmployeeDocumentViewModel>> GetDocumentEntries(int _employeePrmKey, string _entryType);

        Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetSalaryStructureEntries(int _employeePrmKey, string _entryType);

        Task<EmployeeDepartmentViewModel> GetDepartmentEntry(int _employeePrmKey, string _entryType);

        Task<EmployeeDesignationViewModel> GetDesignationEntry(int _employeePrmKey, string _entryType);

        Task<EmployeeDetailViewModel> GetDetailEntry(int _employeePrmKey, string _entryType);

        Task<EmployeePerformanceRatingViewModel> GetPerformanceRatingEntry(int _employeePrmKey, string _entryType);

        Task<EmployeePhotoViewModel> GetPhotoEntry(int _employeePrmKey, string _entryType);

        Task<EmployeeWorkingScheduleViewModel> GetWorkingScheduleEntry(int _employeePrmKey, string _entryType);

        void GetEmployeeAllDefaultValues(EmployeeViewModel _employeeViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeDefaultValues(EmployeeViewModel _employeeViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeDocumentDefaultValues(EmployeeDocumentViewModel _employeeDocumentViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeSalaryStructureDefaultValues(EmployeeSalaryStructureViewModel _employeeSalaryStructureViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeDepartmentDefaultValues(EmployeeDepartmentViewModel _employeeDepartmentViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeDesignationDefaultValues(EmployeeDesignationViewModel _employeeDesignationViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeDetailDefaultValues(EmployeeDetailViewModel _employeeDetailViewModel, bool _isModify, string _entryStatus);

        void GetEmployeePerformanceRatingDefaultValues(EmployeePerformanceRatingViewModel _employeePerformanceRatingViewModel, bool _isModify, string _entryStatus);

        void GetEmployeePhotoDefaultValues(EmployeePhotoViewModel _employeePhotoViewModel, bool _isModify, string _entryStatus);

        void GetEmployeeWorkingScheduleDefaultValues(EmployeeWorkingScheduleViewModel _employeeWorkingScheduleViewModel, bool _isModify, string _entryStatus);

    }
}
