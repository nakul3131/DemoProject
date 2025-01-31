using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Concrete.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Management.Servant
{
    public class EFServantDetailRepository : IServantDetailRepository
    {
        private readonly EFDbContext context;

        public EFServantDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<EmployeeDocumentViewModel>> GetDocumentEntries(int _employeePrmKey, string _entryType)
        {
            try
            {
                IEnumerable<EmployeeDocumentViewModel> employeeDocumentViewModels;
                employeeDocumentViewModels = await context.Database.SqlQuery<EmployeeDocumentViewModel>("SELECT * FROM dbo.GetEmployeeDocumentEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                foreach (EmployeeDocumentViewModel viewModel in employeeDocumentViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.DocumentCopy, viewModel.NameOfFile);

                    viewModel.DocPath = objFile;

                }
                return employeeDocumentViewModels;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetSalaryStructureEntries(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeSalaryStructureViewModel>("SELECT * FROM dbo.GetEmployeeSalaryStructureEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeeDepartmentViewModel> GetDepartmentEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeDepartmentViewModel>("SELECT * FROM dbo.GetEmployeeDepartmentEntryByEmployeePrmKey (@EmployeePrmkey, @EntriesType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeeDesignationViewModel> GetDesignationEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeDesignationViewModel>("SELECT * FROM dbo.GetEmployeeDesignationEntryByEmployeePrmKey (@EmployeePrmkey, @EntriesType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeeDetailViewModel> GetDetailEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeDetailViewModel>("SELECT * FROM dbo.GetEmployeeDetailEntryByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeePerformanceRatingViewModel> GetPerformanceRatingEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeePerformanceRatingViewModel>("SELECT * FROM dbo.GetEmployeePerformanceRatingEntryByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeePhotoViewModel> GetPhotoEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeePhotoViewModel>("SELECT * FROM dbo.GetEmployeePhotoEntryByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeeWorkingScheduleViewModel> GetWorkingScheduleEntry(int _employeePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeWorkingScheduleViewModel>("SELECT * FROM dbo.GetEmployeeWorkingScheduleEntryByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void GetEmployeeAllDefaultValues(EmployeeViewModel _employeeViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EntryStatus = entryStatus;
            _employeeViewModel.UserAction = entryStatus;

            // EmployeeDocument
            _employeeViewModel.EmployeeDocumentViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeDocumentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeDocumentViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeDocumentViewModel.UserAction = entryStatus;

            // EmployeeSalaryStructure
            _employeeViewModel.EmployeeSalaryStructureViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EmployeeSalaryStructureViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeSalaryStructureViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeSalaryStructureViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeSalaryStructureViewModel.UserAction = entryStatus;

            // EmployeeDepartment
            _employeeViewModel.EmployeeDepartmentViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EmployeeDepartmentViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeDepartmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeDepartmentViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeDepartmentViewModel.UserAction = entryStatus;

            // EmployeeDesignation
            _employeeViewModel.EmployeeDesignationViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EmployeeDesignationViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeDesignationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeDesignationViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeDesignationViewModel.UserAction = entryStatus;

            // EmployeeDetail
            _employeeViewModel.EmployeeDetailViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeDetailViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeDetailViewModel.UserAction = entryStatus;

            // EmployeePerformanceRating
            _employeeViewModel.EmployeePerformanceRatingViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EmployeePerformanceRatingViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeePerformanceRatingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeePerformanceRatingViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeePerformanceRatingViewModel.UserAction = entryStatus;

            // EmployeePhoto
            _employeeViewModel.EmployeePhotoViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeePhotoViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeePhotoViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeePhotoViewModel.UserAction = entryStatus;

            // EmployeeWorkingSchedule
            _employeeViewModel.EmployeeWorkingScheduleViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EmployeeWorkingScheduleViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.EmployeeWorkingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EmployeeWorkingScheduleViewModel.EntryStatus = entryStatus;
            _employeeViewModel.EmployeeWorkingScheduleViewModel.UserAction = entryStatus;

            

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeDepartmentViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeDesignationViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeDetailViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeePerformanceRatingViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeePhotoViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeWorkingScheduleViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeDocumentViewModel.ReasonForModification = "None";
                _employeeViewModel.EmployeeSalaryStructureViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeViewModel.Remark = "None";
                _employeeViewModel.EmployeeDepartmentViewModel.Remark = "None";
                _employeeViewModel.EmployeeDesignationViewModel.Remark = "None";
                _employeeViewModel.EmployeeDetailViewModel.Remark = "None";
                _employeeViewModel.EmployeePerformanceRatingViewModel.Remark = "None";
                _employeeViewModel.EmployeePhotoViewModel.Remark = "None";
                _employeeViewModel.EmployeeWorkingScheduleViewModel.Remark = "None";
                _employeeViewModel.EmployeeDocumentViewModel.Remark = "None";
                _employeeViewModel.EmployeeSalaryStructureViewModel.Remark = "None";
            }
            else
            {
                _employeeViewModel.EmployeeDepartmentViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeeDesignationViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeeDetailViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeePerformanceRatingViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeePhotoViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeeWorkingScheduleViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeeDocumentViewModel.Remark = _employeeViewModel.Remark;
                _employeeViewModel.EmployeeSalaryStructureViewModel.Remark = _employeeViewModel.Remark;
            }
        }

        public void GetEmployeeDefaultValues(EmployeeViewModel _employeeViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeViewModel.EntryDateTime = DateTime.Now;
            _employeeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeViewModel.EntryStatus = entryStatus;
            _employeeViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeViewModel.Remark = "None";
            }
        }

        public void GetEmployeeDocumentDefaultValues(EmployeeDocumentViewModel _employeeDocumentViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeDocumentViewModel.EntryDateTime = DateTime.Now;
            _employeeDocumentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeDocumentViewModel.EntryStatus = entryStatus;
            _employeeDocumentViewModel.UserAction = entryStatus;


            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeDocumentViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeDocumentViewModel.Remark = "None";
            }
        }

        public void GetEmployeeSalaryStructureDefaultValues(EmployeeSalaryStructureViewModel _employeeSalaryStructureViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeSalaryStructureViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeSalaryStructureViewModel.EntryDateTime = DateTime.Now;
            _employeeSalaryStructureViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeSalaryStructureViewModel.EntryStatus = entryStatus;
            _employeeSalaryStructureViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeSalaryStructureViewModel.ReasonForModification = "None";
                _employeeSalaryStructureViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeSalaryStructureViewModel.Remark = "None";
            }
        }

        public void GetEmployeeDepartmentDefaultValues(EmployeeDepartmentViewModel _employeeDepartmentViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeDepartmentViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeDepartmentViewModel.EntryDateTime = DateTime.Now;
            _employeeDepartmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeDepartmentViewModel.EntryStatus = entryStatus;
            _employeeDepartmentViewModel.UserAction = entryStatus;


            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeDepartmentViewModel.ReasonForModification = "None";
                _employeeDepartmentViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeDepartmentViewModel.Remark = "None";
            }
        }

        public void GetEmployeeDesignationDefaultValues(EmployeeDesignationViewModel _employeeDesignationViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeDesignationViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeDesignationViewModel.EntryDateTime = DateTime.Now;
            _employeeDesignationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeDesignationViewModel.EntryStatus = entryStatus;
            _employeeDesignationViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeDesignationViewModel.ReasonForModification = "None";
                _employeeDesignationViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeDesignationViewModel.Remark = "None";
            }
        }

        public void GetEmployeeDetailDefaultValues(EmployeeDetailViewModel _employeeDetailViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeDetailViewModel.EntryDateTime = DateTime.Now;
            _employeeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeDetailViewModel.EntryStatus = entryStatus;
            _employeeDetailViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeDetailViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeDetailViewModel.Remark = "None";
            }
        }

        public void GetEmployeePerformanceRatingDefaultValues(EmployeePerformanceRatingViewModel _employeePerformanceRatingViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeePerformanceRatingViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeePerformanceRatingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeePerformanceRatingViewModel.EntryDateTime = DateTime.Now;
            _employeePerformanceRatingViewModel.EntryStatus = entryStatus;
            _employeePerformanceRatingViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeePerformanceRatingViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeePerformanceRatingViewModel.Remark = "None";
            }
        }

        public void GetEmployeePhotoDefaultValues(EmployeePhotoViewModel _employeePhotoViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeePhotoViewModel.EntryDateTime = DateTime.Now;
            _employeePhotoViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeePhotoViewModel.EntryStatus = entryStatus;
            _employeePhotoViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeePhotoViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeePhotoViewModel.Remark = "None";
            }
        }

        public void GetEmployeeWorkingScheduleDefaultValues(EmployeeWorkingScheduleViewModel _employeeWorkingScheduleViewModel, bool _isModify, string _entryStatus)
        {
            string entryStatus;
            if (_entryStatus == StringLiteralValue.Modify && _isModify == false)
            {
                entryStatus = StringLiteralValue.Create;
            }
            else
            {
                entryStatus = _entryStatus;
            }

            _employeeWorkingScheduleViewModel.ActivationStatus = StringLiteralValue.Active;
            _employeeWorkingScheduleViewModel.EntryDateTime = DateTime.Now;
            _employeeWorkingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _employeeWorkingScheduleViewModel.EntryStatus = entryStatus;
            _employeeWorkingScheduleViewModel.UserAction = entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _employeeWorkingScheduleViewModel.ReasonForModification = "None";
                _employeeWorkingScheduleViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _employeeWorkingScheduleViewModel.Remark = "None";
            }
        }
        
    }
}
