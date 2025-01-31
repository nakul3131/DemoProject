using DemoProject.Services.ViewModel.Management.Servant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Management.Servant
{
    public interface IEmployeeDbContextRepository
    {
        bool AttachEmployeeData(EmployeeViewModel _employeeViewModel, string _entryType);

        bool AttachEmployeeDepartmentData(EmployeeDepartmentViewModel _employeeDepartmentViewModel, string _entryType);

        bool AttachEmployeeDesignationData(EmployeeDesignationViewModel _employeeDesignationViewModel, string _entryType);

        bool AttachEmployeeDetailData(EmployeeDetailViewModel _employeeDetailViewModel, string _entryType);

        bool AttachEmployeePerformanceRatingData(EmployeePerformanceRatingViewModel _employeePerformanceRatingViewModel, string _entryType);

        bool AttachEmployeePhotoData(EmployeePhotoViewModel _employeePhotoViewModel, string _entryType);

        bool AttachPhotoDocumentInLocalStorage(EmployeePhotoViewModel _employeePhotoViewModel, string _photoDocumentLocalStoragePath, EmployeePhotoViewModel employeePhotoViewModel, string _entryType);

        bool AttachEmployeeSalaryStructureData(EmployeeSalaryStructureViewModel _employeeSalaryStructureViewModel, string _entryType);

        bool AttachEmployeeWorkingScheduleData(EmployeeWorkingScheduleViewModel _employeeWorkingScheduleViewModel, string _entryType);

        bool AttachEmployeeDocumentData(EmployeeDocumentViewModel _employeeDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachEmployeeDocumentInLocalStorage(EmployeeDocumentViewModel _employeeDocumentViewModel, string _kYCDocumentLocalStoragePath, IEnumerable<EmployeeDocumentViewModel> _employeeDocumentViewModelList, string _entryType);

        string GetFullFilePath(string _fullPath, string _nameOfFile);

        bool FileExist(string _fullFilePath);

        Task<bool> SaveData();

    }
}
