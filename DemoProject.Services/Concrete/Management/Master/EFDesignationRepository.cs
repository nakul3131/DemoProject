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
    public class EFDesignationRepository : IDesignationRepository
    {
        private readonly EFDbContext context;

        public EFDesignationRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.ActivationStatus = StringLiteralValue.Active;
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.EntryStatus = StringLiteralValue.Amend;
                _designationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _designationViewModel.ReasonForModification = "None";
                _designationViewModel.Remark = "None";
                _designationViewModel.TransReasonForModification = "None";
                _designationViewModel.UserAction = StringLiteralValue.Amend;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // Designation
                Designation designationForAmend = Mapper.Map<Designation>(_designationViewModel);
                DesignationMakerChecker designationMakerCheckerForAmend = Mapper.Map<DesignationMakerChecker>(_designationViewModel);

                // DesignationModification
                DesignationModification designationModificationForAmend = Mapper.Map<DesignationModification>(_designationViewModel);
                DesignationModificationMakerChecker designationModificationMakerCheckerForAmend = Mapper.Map<DesignationModificationMakerChecker>(_designationViewModel);

                DesignationTranslation designationTranslationForAmend = Mapper.Map<DesignationTranslation>(_designationViewModel);
                DesignationTranslationMakerChecker designationTranslationMakerCheckerForAmend = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                designationForAmend.PrmKey = _designationViewModel.DesignationPrmKey;
                designationModificationForAmend.PrmKey = _designationViewModel.DesignationModificationPrmKey;
                designationTranslationForAmend.PrmKey = _designationViewModel.DesignationTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_designationViewModel.DesignationModificationPrmKey == 0)
                {
                    // Designation
                    context.DesignationMakerCheckers.Attach(designationMakerCheckerForAmend);
                    context.Entry(designationMakerCheckerForAmend).State = EntityState.Added;
                    designationForAmend.DesignationMakerCheckers.Add(designationMakerCheckerForAmend);

                    context.Designations.Attach(designationForAmend);
                    context.Entry(designationForAmend).State = EntityState.Modified;
                }
                else
                {
                    // Designation Modification 
                    context.DesignationModificationMakerCheckers.Attach(designationModificationMakerCheckerForAmend);
                    context.Entry(designationModificationMakerCheckerForAmend).State = EntityState.Added;
                    designationModificationForAmend.DesignationModificationMakerCheckers.Add(designationModificationMakerCheckerForAmend);

                    context.DesignationModifications.Attach(designationModificationForAmend);
                    context.Entry(designationModificationForAmend).State = EntityState.Modified;
                }

                // DesignationTranslation
                context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerCheckerForAmend);
                context.Entry(designationTranslationMakerCheckerForAmend).State = EntityState.Added;
                designationTranslationForAmend.DesignationTranslationMakerCheckers.Add(designationTranslationMakerCheckerForAmend);

                context.DesignationTranslations.Attach(designationTranslationForAmend);
                context.Entry(designationTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.ReasonForModification = "None";
                _designationViewModel.Remark = "None";
                _designationViewModel.UserAction = StringLiteralValue.Delete;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                DesignationMakerChecker designationMakerChecker = Mapper.Map<DesignationMakerChecker>(_designationViewModel);
                DesignationModificationMakerChecker designationModificationMakerChecker = Mapper.Map<DesignationModificationMakerChecker>(_designationViewModel);
                DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                if (_designationViewModel.DesignationModificationPrmKey == 0)
                {
                    // Designation
                    context.DesignationMakerCheckers.Attach(designationMakerChecker);
                    context.Entry(designationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // DesignationModification  
                    context.DesignationModificationMakerCheckers.Attach(designationModificationMakerChecker);
                    context.Entry(designationModificationMakerChecker).State = EntityState.Added;
                }

                // DesignationTranslation
                context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                context.Entry(designationTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<DesignationViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DesignationViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DesignationViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DesignationViewModel> GetRejectedEntry(Guid _designationId)
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntry (@DesignationId, @EntriesType)", new SqlParameter("@DesignationId", _designationId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueDesignationName(string _nameOfDesignation)
        {
            bool status;
            if (context.Designations.Where(p => p.NameOfDesignation == _nameOfDesignation).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public Guid GetDesignationIdByPrmKey(int _prmKey)
        {
            return context.Designations
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.DesignationId).FirstOrDefault();
        }

        public async Task<DesignationViewModel> GetUnVerifiedEntry(Guid _designationId)
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntry (@DesignationId, @EntriesType)", new SqlParameter("@DesignationId", _designationId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DesignationViewModel> GetVerifiedEntry(Guid _designationId)
        {
            try
            {
                return await context.Database.SqlQuery<DesignationViewModel>("SELECT * FROM dbo.GetDesignationEntry (@DesignationId, @EntriesType)", new SqlParameter("@DesignationId", _designationId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.DesignationModificationPrmKey = 0;
                _designationViewModel.DesignationTranslationPrmKey = 0;
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.EntryStatus = StringLiteralValue.Create;
                _designationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _designationViewModel.Remark = "None";
                _designationViewModel.UserAction = StringLiteralValue.Create;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // DesignationModification
                DesignationModification designationModification = Mapper.Map<DesignationModification>(_designationViewModel);
                DesignationModificationMakerChecker designationModificationMakerChecker = Mapper.Map<DesignationModificationMakerChecker>(_designationViewModel);

                // DesignationTranslation
                DesignationTranslation designationTranslation = Mapper.Map<DesignationTranslation>(_designationViewModel);
                DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                // DesignationModification
                context.DesignationModificationMakerCheckers.Attach(designationModificationMakerChecker);
                context.Entry(designationModificationMakerChecker).State = EntityState.Added;
                designationModification.DesignationModificationMakerCheckers.Add(designationModificationMakerChecker);

                context.DesignationModifications.Attach(designationModification);
                context.Entry(designationModification).State = EntityState.Added;

                // DesignationTranslation
                context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                context.Entry(designationTranslationMakerChecker).State = EntityState.Added;
                designationTranslation.DesignationTranslationMakerCheckers.Add(designationTranslationMakerChecker);

                context.DesignationTranslations.Attach(designationTranslation);
                context.Entry(designationTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.UserAction = StringLiteralValue.Reject;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                DesignationMakerChecker designationMakerChecker = Mapper.Map<DesignationMakerChecker>(_designationViewModel);
                DesignationModificationMakerChecker designationModificationMakerChecker = Mapper.Map<DesignationModificationMakerChecker>(_designationViewModel);
                DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_designationViewModel.DesignationModificationPrmKey == 0)
                {
                    // DesignationMakerChecker
                    context.DesignationMakerCheckers.Attach(designationMakerChecker);
                    context.Entry(designationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // DesignationModificationMakerChecker
                    context.DesignationModificationMakerCheckers.Attach(designationModificationMakerChecker);
                    context.Entry(designationModificationMakerChecker).State = EntityState.Added;
                }

                // DesignationTranslationMakerChecker
                context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                context.Entry(designationTranslationMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.ActivationStatus = StringLiteralValue.Active;
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.EntryStatus = StringLiteralValue.Create;
                _designationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _designationViewModel.ReasonForModification = "None";
                _designationViewModel.Remark = "None";
                _designationViewModel.TransReasonForModification = "None";
                _designationViewModel.UserAction = StringLiteralValue.Create;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Designation
                Designation designation = Mapper.Map<Designation>(_designationViewModel);
                DesignationMakerChecker designationMakerChecker = Mapper.Map<DesignationMakerChecker>(_designationViewModel);

                // DesignationTranslation
                DesignationTranslation designationTranslation = Mapper.Map<DesignationTranslation>(_designationViewModel);
                DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                // DesignationMakerChecker
                context.DesignationMakerCheckers.Attach(designationMakerChecker);
                context.Entry(designationMakerChecker).State = EntityState.Added;
                designation.DesignationMakerCheckers.Add(designationMakerChecker);

                context.Designations.Attach(designation);
                context.Entry(designation).State = EntityState.Added;

                // DesignationTranslationMakerChecker
                context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                context.Entry(designationTranslationMakerChecker).State = EntityState.Added;
                designationTranslation.DesignationTranslationMakerCheckers.Add(designationTranslationMakerChecker);

                context.DesignationTranslations.Attach(designationTranslation);
                context.Entry(designationTranslation).State = EntityState.Added;
                designation.DesignationTranslations.Add(designationTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(DesignationViewModel _designationViewModel)
        {
            try
            {
                // Set Default Value
                _designationViewModel.EntryDateTime = DateTime.Now;
                _designationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _designationViewModel.DesignationId = GetDesignationIdByPrmKey(_designationViewModel.DesignationPrmKey);

                if (_designationViewModel.DesignationModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    DesignationViewModel designationViewModelForModify = await GetVerifiedEntry(_designationViewModel.DesignationId);

                    // Set Default Value
                    designationViewModelForModify.UserAction = StringLiteralValue.Modify;
                    designationViewModelForModify.UserProfilePrmKey = _designationViewModel.UserProfilePrmKey;

                    // DesignationTranslation
                    DesignationTranslationMakerChecker designationTranslationMakerCheckerForModify = Mapper.Map<DesignationTranslationMakerChecker>(designationViewModelForModify);

                    context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerCheckerForModify);
                    context.Entry(designationTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (designationViewModelForModify.IsModified == true)
                    {
                        DesignationModificationMakerChecker designationModificationMakerCheckerForModify = Mapper.Map<DesignationModificationMakerChecker>(designationViewModelForModify);

                        context.DesignationModificationMakerCheckers.Attach(designationModificationMakerCheckerForModify);
                        context.Entry(designationModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _designationViewModel.UserAction = StringLiteralValue.Verify;

                    DesignationModificationMakerChecker designationModificationMakerChecker = Mapper.Map<DesignationModificationMakerChecker>(_designationViewModel);
                    DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                    // DesignationModificationMakerChecker
                    context.DesignationModificationMakerCheckers.Attach(designationModificationMakerChecker);
                    context.Entry(designationModificationMakerChecker).State = EntityState.Added;

                    // DesignationTranslationMakerChecker
                    context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                    context.Entry(designationTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _designationViewModel.UserAction = StringLiteralValue.Verify;

                    DesignationMakerChecker designationMakerChecker = Mapper.Map<DesignationMakerChecker>(_designationViewModel);
                    DesignationTranslationMakerChecker designationTranslationMakerChecker = Mapper.Map<DesignationTranslationMakerChecker>(_designationViewModel);

                    // DesignationMakerChecker
                    context.DesignationMakerCheckers.Attach(designationMakerChecker);
                    context.Entry(designationMakerChecker).State = EntityState.Added;

                    // DesignationTranslationMakerChecker
                    context.DesignationTranslationMakerCheckers.Attach(designationTranslationMakerChecker);
                    context.Entry(designationTranslationMakerChecker).State = EntityState.Added;
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
