using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;

namespace DemoProject.Services.Concrete.Management.Servant
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private readonly EFDbContext context;
        private readonly IServantDetailRepository servantDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personInformationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IEmployeeDbContextRepository employeeDbContextRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;

        public EFEmployeeRepository(RepositoryConnection _connection, IServantDetailRepository _servantDetailRepository, IManagementDetailRepository _managementDetailRepository, IPersonDetailRepository _personInformationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IEmployeeDbContextRepository _employeeDbContextRepository, IPersonInformationParameterRepository _personInformationParameterRepository)
        {
            context = _connection.EFDbContext;
            servantDetailRepository = _servantDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            employeeDbContextRepository = _employeeDbContextRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
        }

        public async Task<bool> Amend(EmployeeViewModel _employeeViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                bool result=true;

                result = employeeDbContextRepository.AttachEmployeeData(_employeeViewModel, StringLiteralValue.Amend);
                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDepartmentData(_employeeViewModel.EmployeeDepartmentViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDesignationData(_employeeViewModel.EmployeeDesignationViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDetailData(_employeeViewModel.EmployeeDetailViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeePerformanceRatingData(_employeeViewModel.EmployeePerformanceRatingViewModel, StringLiteralValue.Amend);
                }
                
                if (result)
                {

                    if (personInformationParameterViewModel.PhotoDocumentUpload != "D" )
                    {
                        EmployeePhotoViewModel employeePhotoViewModelForAmend = await servantDetailRepository.GetPhotoEntry(_employeeViewModel.PrmKey, StringLiteralValue.Reject);

                        if (_employeeViewModel.EmployeePhotoViewModel.PhotoPath != null)
                        {
                            employeePhotoViewModelForAmend.PhotoPath = _employeeViewModel.EmployeePhotoViewModel.PhotoPath;

                            //Photo
                            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                            {
                                if (_employeeViewModel.EmployeePhotoViewModel.PhotoPath != null)
                                    result = employeeDbContextRepository.AttachPhotoDocumentInLocalStorage(_employeeViewModel.EmployeePhotoViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Amend);
                                else
                                {
                                    _employeeViewModel.EmployeePhotoViewModel.NameOfFile = employeePhotoViewModelForAmend.NameOfFile;
                                    _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath = employeePhotoViewModelForAmend.LocalStoragePath;
                                }
                            }
                            else
                            {
                                _employeeViewModel.EmployeePhotoViewModel.NameOfFile = employeePhotoViewModelForAmend.NameOfFile;
                                _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath = employeePhotoViewModelForAmend.LocalStoragePath;
                            }

                            result = employeeDbContextRepository.AttachEmployeePhotoData(_employeeViewModel.EmployeePhotoViewModel, StringLiteralValue.Amend);

                        }
                        else
                            result = employeeDbContextRepository.AttachEmployeePhotoData(employeePhotoViewModelForAmend, StringLiteralValue.Amend);
                    }
                }


                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeWorkingScheduleData(_employeeViewModel.EmployeeWorkingScheduleViewModel, StringLiteralValue.Amend);
                }

                // EmployeeDocument Old Record For Amend
                if (result)
                {
                    IEnumerable<EmployeeDocumentViewModel> employeeDocumentViewModelList = await servantDetailRepository.GetDocumentEntries(_employeeViewModel.PrmKey, StringLiteralValue.Reject);

                    if (employeeDocumentViewModelList.Count() > 0)
                    {
                        foreach (EmployeeDocumentViewModel viewModel in employeeDocumentViewModelList)
                        {
                            result = employeeDbContextRepository.AttachEmployeeDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, viewModel.NameOfDocument, StringLiteralValue.Amend);
                        }
                    }
                }

                // EmployeeDocument New Record For Amend
                if (result)
                {
                    List<EmployeeDocumentViewModel> employeeDocumentViewModelList = (List<EmployeeDocumentViewModel>)HttpContext.Current.Session["Document"];

                    if (employeeDocumentViewModelList != null)
                    {
                        foreach (EmployeeDocumentViewModel viewModel in employeeDocumentViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.StoragePath;
                            string oldFileName = viewModel.NameOfFile;

                            if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage == true)
                            {
                                if (viewModel.DocPath != null)
                                {
                                    result = employeeDbContextRepository.AttachEmployeeDocumentInLocalStorage(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, null, StringLiteralValue.Create);

                                }
                                else
                                {
                                    viewModel.NameOfDocument = oldFileName;

                                    viewModel.StoragePath = oldLocalStoragePath;
                                }
                            }
                            result = employeeDbContextRepository.AttachEmployeeDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);

                        }
                    }

                }



                // EmployeeSalaryStructure Old Record For Amend
                if (result)
                {
                    IEnumerable<EmployeeSalaryStructureViewModel> employeeSalaryStructureViewModelList = await servantDetailRepository.GetSalaryStructureEntries(_employeeViewModel.PrmKey, StringLiteralValue.Reject);

                    if (employeeSalaryStructureViewModelList != null)
                    {
                        foreach (EmployeeSalaryStructureViewModel viewModel in employeeSalaryStructureViewModelList)
                        {
                            result = employeeDbContextRepository.AttachEmployeeSalaryStructureData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // EmployeeSalaryStructure New Record For Amend
                if (result)
                {
                    List<EmployeeSalaryStructureViewModel> employeeSalaryStructureViewModelList = (List<EmployeeSalaryStructureViewModel>)HttpContext.Current.Session["SalaryStructure"];

                    foreach (EmployeeSalaryStructureViewModel viewModel in employeeSalaryStructureViewModelList)
                    {
                        result = employeeDbContextRepository.AttachEmployeeSalaryStructureData(viewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                    result = await employeeDbContextRepository.SaveData();

                if (result)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<EmployeeIndexViewModel>> GetEmployeeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<EmployeeIndexViewModel>("SELECT * FROM dbo.GetEmployeeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeEntry(Guid _employeeId, string _entryType)
        {
            try
            {
                EmployeeViewModel employeeViewModel = await context.Database.SqlQuery<EmployeeViewModel>("SELECT * FROM dbo.GetEmployeeEntry (@EmployeeId, @EntriesType)", new SqlParameter("@EmployeeId", _employeeId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                employeeViewModel.EmployeeDepartmentViewModel = await servantDetailRepository.GetDepartmentEntry(employeeViewModel.PrmKey, _entryType);

                employeeViewModel.EmployeeDesignationViewModel = await servantDetailRepository.GetDesignationEntry(employeeViewModel.PrmKey, _entryType);

                employeeViewModel.EmployeeDetailViewModel = await servantDetailRepository.GetDetailEntry(employeeViewModel.PrmKey, _entryType);

                employeeViewModel.EmployeePerformanceRatingViewModel = await servantDetailRepository.GetPerformanceRatingEntry(employeeViewModel.PrmKey, _entryType);

                employeeViewModel.EmployeePhotoViewModel = await servantDetailRepository.GetPhotoEntry(employeeViewModel.PrmKey, _entryType);

                employeeViewModel.EmployeeWorkingScheduleViewModel = await servantDetailRepository.GetWorkingScheduleEntry(employeeViewModel.PrmKey, _entryType);

                return employeeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

        }

        public async Task<bool> GetSessionValues(int _employeePrmKey, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["Document"] = await servantDetailRepository.GetDocumentEntries(_employeePrmKey, _entryType);

                HttpContext.Current.Session["SalaryStructure"] = await servantDetailRepository.GetSalaryStructureEntries(_employeePrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(EmployeeViewModel _employeeViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                bool result = true;

                result = employeeDbContextRepository.AttachEmployeeData(_employeeViewModel, StringLiteralValue.Create);

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDepartmentData(_employeeViewModel.EmployeeDepartmentViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDesignationData(_employeeViewModel.EmployeeDesignationViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeDetailData(_employeeViewModel.EmployeeDetailViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeePerformanceRatingData(_employeeViewModel.EmployeePerformanceRatingViewModel, StringLiteralValue.Create);
                }

                
                if (result)
                {
                    if (personInformationParameterViewModel.PhotoDocumentUpload != "D")
                    {
                        if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                        {
                            if (_employeeViewModel.EmployeePhotoViewModel.PhotoPath != null)
                                result = employeeDbContextRepository.AttachPhotoDocumentInLocalStorage(_employeeViewModel.EmployeePhotoViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Create);
                            else
                            {
                                _employeeViewModel.EmployeePhotoViewModel.NameOfFile = "None";
                                _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath = "None";
                            }
                        }
                        else
                        {
                            _employeeViewModel.EmployeePhotoViewModel.NameOfFile = "None";
                            _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath = "None";
                        }
                        result = employeeDbContextRepository.AttachEmployeePhotoData(_employeeViewModel.EmployeePhotoViewModel, StringLiteralValue.Create);

                    }
                }

                if (result)
                {
                    result = employeeDbContextRepository.AttachEmployeeWorkingScheduleData(_employeeViewModel.EmployeeWorkingScheduleViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    List<EmployeeDocumentViewModel> employeeDocumentViewModelList = (List<EmployeeDocumentViewModel>)HttpContext.Current.Session["Document"];

                    if (employeeDocumentViewModelList != null)
                    {
                        foreach (EmployeeDocumentViewModel viewModel in employeeDocumentViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.StoragePath;
                            string oldFileName = viewModel.NameOfDocument;

                            if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage == true)
                            {
                                if (viewModel.DocPath != null)
                                {
                                    result = employeeDbContextRepository.AttachEmployeeDocumentInLocalStorage(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, null, StringLiteralValue.Create);

                                }
                                else
                                {
                                    viewModel.NameOfDocument = oldFileName;

                                    viewModel.StoragePath = oldLocalStoragePath;
                                }
                            }
                            result = employeeDbContextRepository.AttachEmployeeDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                        }
                    }
                       
                }

                if (result)
                {
                    List<EmployeeSalaryStructureViewModel> employeeSalaryStructureViewModelList = (List<EmployeeSalaryStructureViewModel>)HttpContext.Current.Session["SalaryStructure"];

                    foreach (EmployeeSalaryStructureViewModel viewModel in employeeSalaryStructureViewModelList)
                    {
                        result = employeeDbContextRepository.AttachEmployeeSalaryStructureData(viewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                {
                    result = await employeeDbContextRepository.SaveData();
                }

                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(EmployeeViewModel _employeeViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                {
                    entriesType = StringLiteralValue.Unverified;
                }
                else
                    entriesType = StringLiteralValue.Reject;


                bool result;

                result = employeeDbContextRepository.AttachEmployeeData(_employeeViewModel, _entryType);

                // EmployeeDepartment
                if (result)
                {
                    EmployeeDepartmentViewModel employeeDepartmentViewModel = await servantDetailRepository.GetDepartmentEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeeDepartmentViewModel != null)
                        result = employeeDbContextRepository.AttachEmployeeDepartmentData(employeeDepartmentViewModel, _entryType);
                }

                // EmployeeDesignation
                if (result)
                {
                    EmployeeDesignationViewModel employeeDesignationViewModel = await servantDetailRepository.GetDesignationEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeeDesignationViewModel != null)
                        result = employeeDbContextRepository.AttachEmployeeDesignationData(employeeDesignationViewModel, _entryType);
                }

                // EmployeeDetail
                if (result)
                {
                    EmployeeDetailViewModel employeeDetailViewModel = await servantDetailRepository.GetDetailEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeeDetailViewModel != null)
                        result = employeeDbContextRepository.AttachEmployeeDetailData(employeeDetailViewModel, _entryType);
                }

                // EmployeePerformanceRating
                if (result)
                {
                    EmployeePerformanceRatingViewModel employeePerformanceRatingViewModel = await servantDetailRepository.GetPerformanceRatingEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeePerformanceRatingViewModel != null)
                        result = employeeDbContextRepository.AttachEmployeePerformanceRatingData(employeePerformanceRatingViewModel, _entryType);
                }

                // EmployeePhoto
                if (result)
                {
                    EmployeePhotoViewModel employeePhotoViewModel = await servantDetailRepository.GetPhotoEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeePhotoViewModel != null)
                    {
                        result = employeeDbContextRepository.AttachEmployeePhotoData(employeePhotoViewModel, _entryType);
                    }
                }

                // EmployeeWorkingSchedule
                if (result)
                {
                    EmployeeWorkingScheduleViewModel employeeWorkingScheduleViewModel = await servantDetailRepository.GetWorkingScheduleEntry(_employeeViewModel.PrmKey, entriesType);

                    if (employeeWorkingScheduleViewModel != null)
                    {
                        result = employeeDbContextRepository.AttachEmployeeWorkingScheduleData(employeeWorkingScheduleViewModel, _entryType);
                    }
                }

                // EmployeeDocument
                if (result)
                {
                    IEnumerable<EmployeeDocumentViewModel> employeeDocumentViewModelList = await servantDetailRepository.GetDocumentEntries(_employeeViewModel.PrmKey, entriesType);

                    if (employeeDocumentViewModelList != null)
                    {
                        foreach (EmployeeDocumentViewModel viewModel in employeeDocumentViewModelList)
                        {
                            result = employeeDbContextRepository.AttachEmployeeDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, viewModel.NameOfDocument, _entryType);
                        }
                    }
                }

                // EmployeeSalaryStructure
                if (result)
                {
                    IEnumerable<EmployeeSalaryStructureViewModel> employeeSalaryStructureViewModelList = await servantDetailRepository.GetSalaryStructureEntries(_employeeViewModel.PrmKey, entriesType);

                    if (employeeSalaryStructureViewModelList != null)
                    {
                        foreach (EmployeeSalaryStructureViewModel viewModel in employeeSalaryStructureViewModelList)
                        {
                            result = employeeDbContextRepository.AttachEmployeeSalaryStructureData(viewModel, _entryType);
                        }
                    }
                }

                if (result)
                    result = await employeeDbContextRepository.SaveData();

                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}