using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFDepartmentRepository : IDepartmentRepository
    {
        private readonly EFDbContext context;

        public EFDepartmentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.ActivationStatus = StringLiteralValue.Active;
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.EntryStatus = StringLiteralValue.Amend;
                _departmentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _departmentViewModel.ReasonForModification = "None";
                _departmentViewModel.Remark = "None";
                _departmentViewModel.TransReasonForModification = "None";
                _departmentViewModel.UserAction = StringLiteralValue.Amend;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // Department
                Department departmentForAmend = Mapper.Map<Department>(_departmentViewModel);
                DepartmentMakerChecker departmentMakerCheckerForAmend = Mapper.Map<DepartmentMakerChecker>(_departmentViewModel);

                // DepartmentModification
                DepartmentModification departmentModificationForAmend = Mapper.Map<DepartmentModification>(_departmentViewModel);
                DepartmentModificationMakerChecker departmentModificationMakerCheckerForAmend = Mapper.Map<DepartmentModificationMakerChecker>(_departmentViewModel);

                DepartmentTranslation departmentTranslationForAmend = Mapper.Map<DepartmentTranslation>(_departmentViewModel);
                DepartmentTranslationMakerChecker departmentTranslationMakerCheckerForAmend = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                departmentForAmend.PrmKey = _departmentViewModel.DepartmentPrmKey;
                departmentModificationForAmend.PrmKey = _departmentViewModel.DepartmentModificationPrmKey;
                departmentTranslationForAmend.PrmKey = _departmentViewModel.DepartmentTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_departmentViewModel.DepartmentModificationPrmKey == 0)
                {
                    // Department
                    context.DepartmentMakerCheckers.Attach(departmentMakerCheckerForAmend);
                    context.Entry(departmentMakerCheckerForAmend).State = EntityState.Added;
                    departmentForAmend.DepartmentMakerCheckers.Add(departmentMakerCheckerForAmend);

                    context.Departments.Attach(departmentForAmend);
                    context.Entry(departmentForAmend).State = EntityState.Modified;
                }
                else
                {
                    // Department Modification 
                    context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerCheckerForAmend);
                    context.Entry(departmentModificationMakerCheckerForAmend).State = EntityState.Added;
                    departmentModificationForAmend.DepartmentModificationMakerCheckers.Add(departmentModificationMakerCheckerForAmend);

                    context.DepartmentModifications.Attach(departmentModificationForAmend);
                    context.Entry(departmentModificationForAmend).State = EntityState.Modified;
                }

                // DepartmentTranslation
                context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerCheckerForAmend);
                context.Entry(departmentTranslationMakerCheckerForAmend).State = EntityState.Added;
                departmentTranslationForAmend.DepartmentTranslationMakerCheckers.Add(departmentTranslationMakerCheckerForAmend);

                context.DepartmentTranslations.Attach(departmentTranslationForAmend);
                context.Entry(departmentTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.ReasonForModification = "None";
                _departmentViewModel.Remark = "None";
                _departmentViewModel.UserAction = StringLiteralValue.Delete;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                DepartmentMakerChecker departmentMakerChecker = Mapper.Map<DepartmentMakerChecker>(_departmentViewModel);
                DepartmentModificationMakerChecker departmentModificationMakerChecker = Mapper.Map<DepartmentModificationMakerChecker>(_departmentViewModel);
                DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                if (_departmentViewModel.DepartmentModificationPrmKey == 0)
                {
                    // Department
                    context.DepartmentMakerCheckers.Attach(departmentMakerChecker);
                    context.Entry(departmentMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // DepartmentModification  
                    context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerChecker);
                    context.Entry(departmentModificationMakerChecker).State = EntityState.Added;
                }

                // DepartmentTranslation
                context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DepartmentViewModel> GetRejectedEntry(Guid _departmentId)
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntry (@DepartmentId, @EntriesType)", new SqlParameter("@DepartmentId", _departmentId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueDepartmentName(string _nameOfDepartment)
        {
            bool status;
            if (context.Departments.Where(p => p.NameOfDepartment == _nameOfDepartment).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public Guid GetDepartmentIdByPrmKey(int _prmKey)
        {
            return context.Departments
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.DepartmentId).FirstOrDefault();
        }

        public async Task<DepartmentViewModel> GetUnVerifiedEntry(Guid _departmentId)
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntry (@DepartmentId, @EntriesType)", new SqlParameter("@DepartmentId", _departmentId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DepartmentViewModel> GetVerifiedEntry(Guid _departmentId)
        {
            try
            {
                return await context.Database.SqlQuery<DepartmentViewModel>("SELECT * FROM dbo.GetDepartmentEntry (@DepartmentId, @EntriesType)", new SqlParameter("@DepartmentId", _departmentId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.DepartmentTranslationPrmKey = 0;
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.EntryStatus = StringLiteralValue.Create;
                _departmentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _departmentViewModel.Remark = "None";
                _departmentViewModel.UserAction = StringLiteralValue.Create;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // DepartmentModification
                DepartmentModification departmentModification = Mapper.Map<DepartmentModification>(_departmentViewModel);
                DepartmentModificationMakerChecker departmentModificationMakerChecker = Mapper.Map<DepartmentModificationMakerChecker>(_departmentViewModel);

                // DepartmentTranslation
                DepartmentTranslation departmentTranslation = Mapper.Map<DepartmentTranslation>(_departmentViewModel);
                DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                // DepartmentModification
                context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerChecker);
                context.Entry(departmentModificationMakerChecker).State = EntityState.Added;
                departmentModification.DepartmentModificationMakerCheckers.Add(departmentModificationMakerChecker);

                context.DepartmentModifications.Attach(departmentModification);
                context.Entry(departmentModification).State = EntityState.Added;

                // DepartmentTranslation
                context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;
                departmentTranslation.DepartmentTranslationMakerCheckers.Add(departmentTranslationMakerChecker);

                context.DepartmentTranslations.Attach(departmentTranslation);
                context.Entry(departmentTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.UserAction = StringLiteralValue.Reject;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                DepartmentMakerChecker departmentMakerChecker = Mapper.Map<DepartmentMakerChecker>(_departmentViewModel);
                DepartmentModificationMakerChecker departmentModificationMakerChecker = Mapper.Map<DepartmentModificationMakerChecker>(_departmentViewModel);
                DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_departmentViewModel.DepartmentModificationPrmKey == 0)
                {
                    // DepartmentMakerChecker
                    context.DepartmentMakerCheckers.Attach(departmentMakerChecker);
                    context.Entry(departmentMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // DepartmentModificationMakerChecker
                    context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerChecker);
                    context.Entry(departmentModificationMakerChecker).State = EntityState.Added;
                }

                // DepartmentTranslationMakerChecker
                context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.ActivationStatus = StringLiteralValue.Active;
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.EntryStatus = StringLiteralValue.Create;
                _departmentViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _departmentViewModel.ReasonForModification = "None";
                _departmentViewModel.Remark = "None";
                _departmentViewModel.TransReasonForModification = "None";
                _departmentViewModel.UserAction = StringLiteralValue.Create;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Department
                Department department = Mapper.Map<Department>(_departmentViewModel);
                DepartmentMakerChecker departmentMakerChecker = Mapper.Map<DepartmentMakerChecker>(_departmentViewModel);

                // DepartmentTranslation
                DepartmentTranslation departmentTranslation = Mapper.Map<DepartmentTranslation>(_departmentViewModel);
                DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                // DepartmentMakerChecker
                context.DepartmentMakerCheckers.Attach(departmentMakerChecker);
                context.Entry(departmentMakerChecker).State = EntityState.Added;
                department.DepartmentMakerCheckers.Add(departmentMakerChecker);

                context.Departments.Attach(department);
                context.Entry(department).State = EntityState.Added;

                // DepartmentTranslationMakerChecker
                context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;
                departmentTranslation.DepartmentTranslationMakerCheckers.Add(departmentTranslationMakerChecker);

                context.DepartmentTranslations.Attach(departmentTranslation);
                context.Entry(departmentTranslation).State = EntityState.Added;
                department.DepartmentTranslations.Add(departmentTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(DepartmentViewModel _departmentViewModel)
        {
            try
            {
                // Set Default Value
                _departmentViewModel.EntryDateTime = DateTime.Now;
                _departmentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _departmentViewModel.DepartmentId = GetDepartmentIdByPrmKey(_departmentViewModel.DepartmentPrmKey);

                if (_departmentViewModel.DepartmentModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    DepartmentViewModel departmentViewModelForModify = await GetVerifiedEntry(_departmentViewModel.DepartmentId);

                    // Set Default Value
                    departmentViewModelForModify.UserAction = StringLiteralValue.Modify;
                    departmentViewModelForModify.UserProfilePrmKey = _departmentViewModel.UserProfilePrmKey;

                    // DepartmentTranslation
                    DepartmentTranslationMakerChecker departmentTranslationMakerCheckerForModify = Mapper.Map<DepartmentTranslationMakerChecker>(departmentViewModelForModify);

                    context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerCheckerForModify);
                    context.Entry(departmentTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (departmentViewModelForModify.IsModified == true)
                    {
                        DepartmentModificationMakerChecker departmentModificationMakerCheckerForModify = Mapper.Map<DepartmentModificationMakerChecker>(departmentViewModelForModify);

                        context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerCheckerForModify);
                        context.Entry(departmentModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _departmentViewModel.UserAction = StringLiteralValue.Verify;

                    DepartmentModificationMakerChecker departmentModificationMakerChecker = Mapper.Map<DepartmentModificationMakerChecker>(_departmentViewModel);
                    DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                    // DepartmentModificationMakerChecker
                    context.DepartmentModificationMakerCheckers.Attach(departmentModificationMakerChecker);
                    context.Entry(departmentModificationMakerChecker).State = EntityState.Added;

                    // DepartmentTranslationMakerChecker
                    context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                    context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _departmentViewModel.UserAction = StringLiteralValue.Verify;

                    DepartmentMakerChecker departmentMakerChecker = Mapper.Map<DepartmentMakerChecker>(_departmentViewModel);
                    DepartmentTranslationMakerChecker departmentTranslationMakerChecker = Mapper.Map<DepartmentTranslationMakerChecker>(_departmentViewModel);

                    // DepartmentMakerChecker
                    context.DepartmentMakerCheckers.Attach(departmentMakerChecker);
                    context.Entry(departmentMakerChecker).State = EntityState.Added;

                    // DepartmentTranslationMakerChecker
                    context.DepartmentTranslationMakerCheckers.Attach(departmentTranslationMakerChecker);
                    context.Entry(departmentTranslationMakerChecker).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}