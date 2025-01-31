using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Abstract.Security;
using DemoProject.Domain.Entities.Management.Servant;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.ViewModel.Management.Servant;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;


namespace DemoProject.Services.Concrete.Management.Servant
{
    public class EFEmployeeDbContextRepository : IEmployeeDbContextRepository

    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;

        // private BusinessOffice businessOffice = new BusinessOffice();
        private EntityState entityState;
        int employeePrmKey;
        public EFEmployeeDbContextRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository, IAccountDetailRepository _accountDetailRepository, IManagementDetailRepository _managementDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
        }

        //TO STORE IMAGES IN LOCAL STORAGE
        // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
        // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.
        // Create List httpPostedFileBaseList For New Uploaded Files

        List<string> filePathList = new List<string>();
        List<string> localStorageFilePathList = new List<string>();
        List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();

        //TO STORE NEW IMAGES AND DELETE OLD IMAGES IN LOCAL STORAGE

        List<string> oldRecord = new List<string>();
        List<string> localStorageFilePathListForOldRecord = new List<string>();
        List<HttpPostedFileBase> httpPostedFileBaseListForOldRecord = new List<HttpPostedFileBase>();

        public bool AttachEmployeeData(EmployeeViewModel _employeeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeViewModel, _entryType);

                Employee employee = Mapper.Map<Employee>(_employeeViewModel);
                EmployeeMakerChecker employeeMakerChecker = Mapper.Map<EmployeeMakerChecker>(_employeeViewModel);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeePrmKey = _employeeViewModel.EmployeePrmKey;

                    context.Employees.Attach(employee);
                    context.Entry(employee).State = entityState;

                    context.EmployeeMakerCheckers.Attach(employeeMakerChecker);
                    context.Entry(employeeMakerChecker).State = EntityState.Added;
                    employee.EmployeeMakerCheckers.Add(employeeMakerChecker);
                }
                else
                {
                    context.EmployeeMakerCheckers.Attach(employeeMakerChecker);
                    context.Entry(employeeMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeDepartmentData(EmployeeDepartmentViewModel _employeeDepartmentViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeDepartmentViewModel, _entryType);

                EmployeeDepartment employeeDepartment = Mapper.Map<EmployeeDepartment>(_employeeDepartmentViewModel);
                EmployeeDepartmentMakerChecker employeeDepartmentMakerChecker = Mapper.Map<EmployeeDepartmentMakerChecker>(_employeeDepartmentViewModel);

                employeeDepartment.DepartmentPrmKey = managementDetailRepository.GetDepartmentPrmKeyById(_employeeDepartmentViewModel.DepartmentId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeeDepartment.EmployeePrmKey = employeePrmKey;
                    // EmployeeDepartment
                    context.EmployeeDepartments.Attach(employeeDepartment);
                    context.Entry(employeeDepartment).State = entityState;

                    // EmployeeDepartmentMakerChecker
                    context.EmployeeDepartmentMakerCheckers.Attach(employeeDepartmentMakerChecker);
                    context.Entry(employeeDepartmentMakerChecker).State = EntityState.Added;
                    employeeDepartment.EmployeeDepartmentMakerCheckers.Add(employeeDepartmentMakerChecker);

                }
                else
                {
                    context.EmployeeDepartmentMakerCheckers.Attach(employeeDepartmentMakerChecker);
                    context.Entry(employeeDepartmentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeDesignationData(EmployeeDesignationViewModel _employeeDesignationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeDesignationViewModel, _entryType);

                EmployeeDesignation employeeDesignation = Mapper.Map<EmployeeDesignation>(_employeeDesignationViewModel);
                EmployeeDesignationMakerChecker employeeDesignationMakerChecker = Mapper.Map<EmployeeDesignationMakerChecker>(_employeeDesignationViewModel);

                employeeDesignation.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_employeeDesignationViewModel.DesignationId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeeDesignation.EmployeePrmKey = employeePrmKey;

                    // EmployeeDesignation
                    context.EmployeeDesignations.Attach(employeeDesignation);
                    context.Entry(employeeDesignation).State = entityState;

                    // EmployeeDesignationMakerChecker
                    context.EmployeeDesignationMakerCheckers.Attach(employeeDesignationMakerChecker);
                    context.Entry(employeeDesignationMakerChecker).State = EntityState.Added;
                    employeeDesignation.EmployeeDesignationMakerCheckers.Add(employeeDesignationMakerChecker);



                }
                else
                {
                    context.EmployeeDesignationMakerCheckers.Attach(employeeDesignationMakerChecker);
                    context.Entry(employeeDesignationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeDetailData(EmployeeDetailViewModel _employeeDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeDetailViewModel, _entryType);

                EmployeeDetail employeeDetail = Mapper.Map<EmployeeDetail>(_employeeDetailViewModel);
                EmployeeDetailMakerChecker employeeDetailMakerChecker = Mapper.Map<EmployeeDetailMakerChecker>(_employeeDetailViewModel);

                employeeDetail.EmployeeTypePrmKey = managementDetailRepository.GetEmployeeTypePrmKeyById(_employeeDetailViewModel.EmployeeTypeId);
                employeeDetail.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_employeeDetailViewModel.PersonId);
                employeeDetail.PostingPlacePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_employeeDetailViewModel.PostingPlaceId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeeDetail.EmployeePrmKey = employeePrmKey;

                    // EmployeeDetail
                    context.EmployeeDetails.Attach(employeeDetail);
                    context.Entry(employeeDetail).State = entityState;

                    // EmployeeDetailMakerChecker
                    context.EmployeeDetailMakerCheckers.Attach(employeeDetailMakerChecker);
                    context.Entry(employeeDetailMakerChecker).State = EntityState.Added;
                    employeeDetail.EmployeeDetailMakerCheckers.Add(employeeDetailMakerChecker);



                }
                else
                {
                    context.EmployeeDetailMakerCheckers.Attach(employeeDetailMakerChecker);
                    context.Entry(employeeDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeePerformanceRatingData(EmployeePerformanceRatingViewModel _employeePerformanceRatingViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeePerformanceRatingViewModel, _entryType);

                EmployeePerformanceRating employeePerformanceRating = Mapper.Map<EmployeePerformanceRating>(_employeePerformanceRatingViewModel);
                EmployeePerformanceRatingMakerChecker employeePerformanceRatingMakerChecker = Mapper.Map<EmployeePerformanceRatingMakerChecker>(_employeePerformanceRatingViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeePerformanceRating.EmployeePrmKey = employeePrmKey;

                    // EmployeePerformanceRating
                    context.EmployeePerformanceRatings.Attach(employeePerformanceRating);
                    context.Entry(employeePerformanceRating).State = entityState;

                    // EmployeePerformanceRatingMakerChecker
                    context.EmployeePerformanceRatingMakerCheckers.Attach(employeePerformanceRatingMakerChecker);
                    context.Entry(employeePerformanceRatingMakerChecker).State = EntityState.Added;
                    employeePerformanceRating.EmployeePerformanceRatingMakerCheckers.Add(employeePerformanceRatingMakerChecker);


                }
                else
                {
                    context.EmployeePerformanceRatingMakerCheckers.Attach(employeePerformanceRatingMakerChecker);
                    context.Entry(employeePerformanceRatingMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeePhotoData(EmployeePhotoViewModel _employeePhotoViewModel, string _entryType)
        {
            try
            {
                int employeePhotoPrmKey = _employeePhotoViewModel.EmployeePhotoPrmKey;
                configurationDetailRepository.SetDefaultValues(_employeePhotoViewModel, _entryType);

                EmployeePhoto employeePhoto = Mapper.Map<EmployeePhoto>(_employeePhotoViewModel);
                EmployeePhotoMakerChecker employeePhotoMakerChecker = Mapper.Map<EmployeePhotoMakerChecker>(_employeePhotoViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeePhoto.EmployeePrmKey = employeePrmKey;
                    
                    string oldPhotoFileName = GetOldPhotoFileName(employeePhotoPrmKey);
                    string oldPhotoLocalStoragePath = GetOldPhotoLocalStoragePath(employeePhotoPrmKey);
                    
                    if (oldPhotoFileName != employeePhoto.NameOfFile && oldPhotoFileName !=null)
                    {
                        string serverDestinationPath = " ";
                        // Check if the destination path contains a tilde ('~') operator.
                        if (oldPhotoLocalStoragePath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(oldPhotoLocalStoragePath);
                        }

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(serverDestinationPath);
                        httpPostedFileBaseListForOldRecord.Add(_employeePhotoViewModel.PhotoPath);
                    }
                    

                    // EmployeePhoto
                    context.EmployeePhotos.Attach(employeePhoto);
                    context.Entry(employeePhoto).State = entityState;

                    // EmployeePhotoMakerChecker
                    context.EmployeePhotoMakerCheckers.Attach(employeePhotoMakerChecker);
                    context.Entry(employeePhotoMakerChecker).State = EntityState.Added;
                    employeePhoto.EmployeePhotoMakerCheckers.Add(employeePhotoMakerChecker);

                }
                else
                {
                    context.EmployeePhotoMakerCheckers.Attach(employeePhotoMakerChecker);
                    context.Entry(employeePhotoMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        //Get Old PhotoFileName
        string GetOldPhotoFileName(long _employeePhotoPrmKey)
        {
            return context.EmployeePhotos
                   .Where(c => c.PrmKey == _employeePhotoPrmKey)
                   .Select(c => c.NameOfFile).FirstOrDefault();
        }

        //Get Old PhotoLocalStoragePath
        string GetOldPhotoLocalStoragePath(long _employeePhotoPrmKey)
        {
            return context.EmployeePhotos
                   .Where(c => c.PrmKey == _employeePhotoPrmKey)
                   .Select(c => c.LocalStoragePath).FirstOrDefault();
        }

        public bool AttachPhotoDocumentInLocalStorage(EmployeePhotoViewModel _employeePhotoViewModel, string _photoDocumentLocalStoragePath, EmployeePhotoViewModel employeePhotoViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _employeePhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_employeePhotoViewModel.PhotoPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _photoDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _employeePhotoViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_employeePhotoViewModel.PhotoPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _employeePhotoViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _employeePhotoViewModel.LocalStoragePath = localStoragePath;
                }

                //else
                //{
                //    _employeePhotoViewModel.PhotoCopy = personPhotoSignViewModel.PhotoCopy;

                //    // Check Existance Of File 
                //    FileInfo file = new FileInfo(personPhotoSignViewModel.LocalStoragePath);
                //    File.Delete(personPhotoSignViewModel.LocalStoragePath);
                //    if (file.Exists)
                //    {
                //        // Encrypt Filename With Extension
                //        _employeePhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                //        // Combine Local Storage Path With File Name
                //        _employeePhotoViewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), personPhotoSignViewModel.NameOfFile);

                //        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                //        filePathList.Add(personPhotoSignViewModel.LocalStoragePath);

                //        // Add null In httpPostedFileBaseList (Because Of Old File)
                //        httpPostedFileBaseList.Add(null);

                //        // Add New Generated Local Storage Path Which Has Stored In Database.
                //        localStorageFilePathList.Add(_employeePhotoViewModel.LocalStoragePath);

                //    }

                //}

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeWorkingScheduleData(EmployeeWorkingScheduleViewModel _employeeWorkingScheduleViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeWorkingScheduleViewModel, _entryType);

                EmployeeWorkingSchedule employeeWorkingSchedule = Mapper.Map<EmployeeWorkingSchedule>(_employeeWorkingScheduleViewModel);
                EmployeeWorkingScheduleMakerChecker employeeWorkingScheduleMakerChecker = Mapper.Map<EmployeeWorkingScheduleMakerChecker>(_employeeWorkingScheduleViewModel);

                employeeWorkingSchedule.WorkingSchedulePrmKey = managementDetailRepository.GetWorkingSchedulePrmKeyById(_employeeWorkingScheduleViewModel.WorkingScheduleId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    employeeWorkingSchedule.EmployeePrmKey = employeePrmKey;

                    // EmployeeWorkingSchedule
                    context.EmployeeWorkingSchedules.Attach(employeeWorkingSchedule);
                    context.Entry(employeeWorkingSchedule).State = entityState;

                    // EmployeeWorkingScheduleMakerChecker
                    context.EmployeeWorkingScheduleMakerCheckers.Attach(employeeWorkingScheduleMakerChecker);
                    context.Entry(employeeWorkingScheduleMakerChecker).State = EntityState.Added;
                    employeeWorkingSchedule.EmployeeWorkingScheduleMakerCheckers.Add(employeeWorkingScheduleMakerChecker);


                }
                else
                {
                    context.EmployeeWorkingScheduleMakerCheckers.Attach(employeeWorkingScheduleMakerChecker);
                    context.Entry(employeeWorkingScheduleMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeDocumentData(EmployeeDocumentViewModel _employeeDocumentViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeDocumentViewModel, _entryType);

                EmployeeDocument employeeDocument = Mapper.Map<EmployeeDocument>(_employeeDocumentViewModel);
                EmployeeDocumentMakerChecker employeeDocumentMakerChecker = Mapper.Map<EmployeeDocumentMakerChecker>(_employeeDocumentViewModel);

                employeeDocument.DocumentPrmKey = personDetailRepository.GetDocumentPrmKeyById(_employeeDocumentViewModel.DocumentId);

                if (_entryType == StringLiteralValue.Create)
                {
                    employeeDocument.EmployeePrmKey = employeePrmKey;
                    
                    context.EmployeeDocuments.Attach(employeeDocument);
                    context.Entry(employeeDocument).State = EntityState.Added;

                    context.EmployeeDocumentMakerCheckers.Attach(employeeDocumentMakerChecker);
                    context.Entry(employeeDocumentMakerChecker).State = EntityState.Added;
                    employeeDocument.EmployeeDocumentMakerCheckers.Add(employeeDocumentMakerChecker);
                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image When PhotoUpload is Optional.
                    if (_oldFileName != _employeeDocumentViewModel.NameOfFile && _oldFileName != null)
                    {
                        string serverDestinationPath = " ";

                        // Get Destination Path From Database
                        string destinationPath = _localStoragePath;

                        // Check if the destination path contains a tilde ('~') operator.
                        if (destinationPath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                        }

                        // Combine Destination Path with the encrypted file name to get the Full Destination Path
                        _employeeDocumentViewModel.StoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                        oldRecord.Add("OldRecord");
                        localStorageFilePathListForOldRecord.Add(_employeeDocumentViewModel.StoragePath);
                        httpPostedFileBaseListForOldRecord.Add(_employeeDocumentViewModel.DocPath);
                    }

                }
                else
                {
                    context.EmployeeDocumentMakerCheckers.Attach(employeeDocumentMakerChecker);
                    context.Entry(employeeDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachEmployeeDocumentInLocalStorage(EmployeeDocumentViewModel _employeeDocumentViewModel, string _kYCDocumentLocalStoragePath, IEnumerable<EmployeeDocumentViewModel> _employeeDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _employeeDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_employeeDocumentViewModel.DocPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _kYCDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _employeeDocumentViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_employeeDocumentViewModel.DocPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _employeeDocumentViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _employeeDocumentViewModel.StoragePath = localStoragePath;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachEmployeeSalaryStructureData(EmployeeSalaryStructureViewModel _employeeSalaryStructureViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_employeeSalaryStructureViewModel, _entryType);


                EmployeeSalaryStructure employeeSalaryStructure = Mapper.Map<EmployeeSalaryStructure>(_employeeSalaryStructureViewModel);
                EmployeeSalaryStructureMakerChecker employeeSalaryStructureMakerChecker = Mapper.Map<EmployeeSalaryStructureMakerChecker>(_employeeSalaryStructureViewModel);

                employeeSalaryStructure.SalaryBreakupPrmKey = managementDetailRepository.GetSalaryBreakupPrmKeyById(_employeeSalaryStructureViewModel.SalaryBreakupId);

                if (_entryType == StringLiteralValue.Create)
                {
                    employeeSalaryStructure.EmployeePrmKey = employeePrmKey;

                    context.EmployeeSalaryStructures.Attach(employeeSalaryStructure);
                    context.Entry(employeeSalaryStructure).State = EntityState.Added;

                    context.EmployeeSalaryStructureMakerCheckers.Attach(employeeSalaryStructureMakerChecker);
                    context.Entry(employeeSalaryStructureMakerChecker).State = EntityState.Added;
                    employeeSalaryStructure.EmployeeSalaryStructureMakerCheckers.Add(employeeSalaryStructureMakerChecker);


                }
                else
                {
                    context.EmployeeSalaryStructureMakerCheckers.Attach(employeeSalaryStructureMakerChecker);
                    context.Entry(employeeSalaryStructureMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        private bool DeleteLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        if (File.Exists(localStorageFilePathList[i]))
                            File.Delete(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool SaveLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        //New Uploaded File Copy Uploaded File To Destination Folder
                        httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool ResetOldLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < oldRecord.Count; i++)
                {
                    //If New File Uploaded
                    if (oldRecord[i] == "OldRecord")
                    {
                        //New Uploaded File Copy Uploaded File To Destination Folder
                        httpPostedFileBaseListForOldRecord[i].SaveAs(localStorageFilePathListForOldRecord[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool DeleteOldLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < oldRecord.Count; i++)
                {
                    //If New File Uploaded
                    if (oldRecord[i] == "OldRecord")
                    {
                        if (File.Exists(localStorageFilePathListForOldRecord[i]))
                            File.Delete(localStorageFilePathListForOldRecord[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        public string GetFullFilePath(string _fullPath, string _nameOfFile)
        {
            string serverDestinationPath = " ";
            // Get Destination Path From Database
            string destinationPath = _fullPath;

            // Check if the destination path contains a tilde ('~') operator.
            if (destinationPath.IndexOf('~') > -1)
            {
                serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
            }

            return serverDestinationPath;
        }

        public bool FileExist(string _fullFilePath)
        {
            bool result = false;

            if (File.Exists(_fullFilePath))
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> SaveData()
        {
            try
            {
                SaveLocalStorageDocument();
                DeleteOldLocalStorageDocument();
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DeleteLocalStorageDocument();
                ResetOldLocalStorageDocument();
                return false;
            }
        }

    }
}
