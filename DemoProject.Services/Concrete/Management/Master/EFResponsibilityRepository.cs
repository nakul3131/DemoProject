using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFResponsibilityRepository : IResponsibilityRepository
    {
        private readonly EFDbContext context;

        public EFResponsibilityRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;

        }

        public async Task<bool> Amend(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.ActivationStatus = StringLiteralValue.Active;
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.EntryStatus = StringLiteralValue.Amend;
                _responsibilityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _responsibilityViewModel.ReasonForModification = "None";
                _responsibilityViewModel.TransReasonForModification = "None";
                _responsibilityViewModel.UserAction = StringLiteralValue.Amend;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _responsibilityViewModel.ParentFunctionPrmKey = GetResponsibilityPrmKeyById(_responsibilityViewModel.ParentFunctionId);

                // Mapping 
                // Responsibility
                Responsibility responsibilityForAmend = Mapper.Map<Responsibility>(_responsibilityViewModel);
                ResponsibilityMakerChecker responsibilityMakerCheckerForAmend = Mapper.Map<ResponsibilityMakerChecker>(_responsibilityViewModel);

                // ResponsibilityModification
                ResponsibilityModification responsibilityModificationForAmend = Mapper.Map<ResponsibilityModification>(_responsibilityViewModel);
                ResponsibilityModificationMakerChecker responsibilityModificationMakerCheckerForAmend = Mapper.Map<ResponsibilityModificationMakerChecker>(_responsibilityViewModel);

                ResponsibilityTranslation responsibilityTranslationForAmend = Mapper.Map<ResponsibilityTranslation>(_responsibilityViewModel);
                ResponsibilityTranslationMakerChecker responsibilityTranslationMakerCheckerForAmend = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                responsibilityForAmend.PrmKey = _responsibilityViewModel.ResponsibilityPrmKey;
                responsibilityModificationForAmend.PrmKey = _responsibilityViewModel.ResponsibilityModificationPrmKey;
                responsibilityTranslationForAmend.PrmKey = _responsibilityViewModel.ResponsibilityTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_responsibilityViewModel.ResponsibilityModificationPrmKey == 0)
                {
                    // Responsibility
                    context.ResponsibilityMakerCheckers.Attach(responsibilityMakerCheckerForAmend);
                    context.Entry(responsibilityMakerCheckerForAmend).State = EntityState.Added;
                    responsibilityForAmend.ResponsibilityMakerCheckers.Add(responsibilityMakerCheckerForAmend);

                    context.Responsibilities.Attach(responsibilityForAmend);
                    context.Entry(responsibilityForAmend).State = EntityState.Modified;
                }
                else
                {
                    // Responsibility Modification 
                    context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerCheckerForAmend);
                    context.Entry(responsibilityModificationMakerCheckerForAmend).State = EntityState.Added;
                    responsibilityModificationForAmend.ResponsibilityModificationMakerCheckers.Add(responsibilityModificationMakerCheckerForAmend);

                    context.ResponsibilityModifications.Attach(responsibilityModificationForAmend);
                    context.Entry(responsibilityModificationForAmend).State = EntityState.Modified;
                }

                // ResponsibilityTranslation
                context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerCheckerForAmend);
                context.Entry(responsibilityTranslationMakerCheckerForAmend).State = EntityState.Added;
                responsibilityTranslationForAmend.ResponsibilityTranslationMakerCheckers.Add(responsibilityTranslationMakerCheckerForAmend);

                context.ResponsibilityTranslations.Attach(responsibilityTranslationForAmend);
                context.Entry(responsibilityTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.ReasonForModification = "None";
                _responsibilityViewModel.Remark = "None";
                _responsibilityViewModel.UserAction = StringLiteralValue.Delete;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ResponsibilityMakerChecker responsibilityMakerChecker = Mapper.Map<ResponsibilityMakerChecker>(_responsibilityViewModel);
                ResponsibilityModificationMakerChecker responsibilityModificationMakerChecker = Mapper.Map<ResponsibilityModificationMakerChecker>(_responsibilityViewModel);
                ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                if (_responsibilityViewModel.ResponsibilityModificationPrmKey == 0)
                {
                    // Responsibility
                    context.ResponsibilityMakerCheckers.Attach(responsibilityMakerChecker);
                    context.Entry(responsibilityMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ResponsibilityModification  
                    context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerChecker);
                    context.Entry(responsibilityModificationMakerChecker).State = EntityState.Added;
                }

                // ResponsibilityTranslation
                context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public List<SelectListItem> ResponsibilityDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from r in context.Responsibilities
                            join mf in context.ResponsibilityModifications .Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on r.PrmKey equals mf.ResponsibilityPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.ResponsibilityTranslations .Where(t => t.EntryStatus == StringLiteralValue.Verify) on r.PrmKey equals t.ResponsibilityPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (r.EntryStatus.Equals(StringLiteralValue.Verify))
                            &&    (r.ActivationStatus.Equals(StringLiteralValue.Active))
                            &&    (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby r.NameOfResponsibility
                            select new SelectListItem
                            {
                                Value = r.ResponsibilityId.ToString(),
                                Text = ((mf.NameOfResponsibility.Equals(null)) ? r.NameOfResponsibility.Trim() + " ---> " + (t.TransNameOfResponsibility.Equals(null) ? " " : t.TransNameOfResponsibility.Trim()) : mf.NameOfResponsibility + " ---> " + (t.TransNameOfResponsibility.Equals(null) ? " " : t.TransNameOfResponsibility.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.Responsibilities
                        join mf in context.ResponsibilityModifications .Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on r.PrmKey equals mf.ResponsibilityPrmKey into rm
                        from mf in rm.DefaultIfEmpty()
                        where (r.EntryStatus.Equals(StringLiteralValue.Verify)) 
                        &&    (r.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = r.ResponsibilityId.ToString(),
                            Text = ((mf.NameOfResponsibility.Equals(null)) ? r.NameOfResponsibility.Trim() : mf.NameOfResponsibility)
                        }).ToList();
            }
        }

        public async Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetResponsibilityPrmKeyById(Guid _responsibilityId)
        {
            return context.Responsibilities
                    .Where(c => c.ResponsibilityId == _responsibilityId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<ResponsibilityViewModel> GetRejectedEntry(Guid _responsibilityId)
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntry (@ResponsibilityId, @EntriesType)", new SqlParameter("@ResponsibilityId", _responsibilityId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueResponsibilityName(string _nameOfResponsibility)
        {
            bool status;
            if (context.Responsibilities.Where(p => p.NameOfResponsibility == _nameOfResponsibility).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public async Task<ResponsibilityViewModel> GetUnVerifiedEntry(Guid _responsibilityId)
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntry (@ResponsibilityId, @EntriesType)", new SqlParameter("@ResponsibilityId", _responsibilityId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ResponsibilityViewModel> GetVerifiedEntry(Guid _responsibilityId)
        {
            try
            {
                return await context.Database.SqlQuery<ResponsibilityViewModel>("SELECT * FROM dbo.GetResponsibilityEntry (@ResponsibilityId, @EntriesType)", new SqlParameter("@ResponsibilityId", _responsibilityId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.ResponsibilityTranslationPrmKey = 0;
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.EntryStatus = StringLiteralValue.Create;
                _responsibilityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _responsibilityViewModel.Remark = "None";
                _responsibilityViewModel.UserAction = StringLiteralValue.Create;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _responsibilityViewModel.ParentFunctionPrmKey = GetResponsibilityPrmKeyById(_responsibilityViewModel.ParentFunctionId);

                // Mapping
                // ResponsibilityModification
                ResponsibilityModification responsibilityModification = Mapper.Map<ResponsibilityModification>(_responsibilityViewModel);
                ResponsibilityModificationMakerChecker responsibilityModificationMakerChecker = Mapper.Map<ResponsibilityModificationMakerChecker>(_responsibilityViewModel);

                // ResponsibilityTranslation
                ResponsibilityTranslation responsibilityTranslation = Mapper.Map<ResponsibilityTranslation>(_responsibilityViewModel);
                ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                // ResponsibilityModification
                context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerChecker);
                context.Entry(responsibilityModificationMakerChecker).State = EntityState.Added;
                responsibilityModification.ResponsibilityModificationMakerCheckers.Add(responsibilityModificationMakerChecker);

                context.ResponsibilityModifications.Attach(responsibilityModification);
                context.Entry(responsibilityModification).State = EntityState.Added;

                // ResponsibilityTranslation
                context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;
                responsibilityTranslation.ResponsibilityTranslationMakerCheckers.Add(responsibilityTranslationMakerChecker);

                context.ResponsibilityTranslations.Attach(responsibilityTranslation);
                context.Entry(responsibilityTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.UserAction = StringLiteralValue.Reject;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ResponsibilityMakerChecker responsibilityMakerChecker = Mapper.Map<ResponsibilityMakerChecker>(_responsibilityViewModel);
                ResponsibilityModificationMakerChecker responsibilityModificationMakerChecker = Mapper.Map<ResponsibilityModificationMakerChecker>(_responsibilityViewModel);
                ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_responsibilityViewModel.ResponsibilityModificationPrmKey == 0)
                {
                    // ResponsibilityMakerChecker
                    context.ResponsibilityMakerCheckers.Attach(responsibilityMakerChecker);
                    context.Entry(responsibilityMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ResponsibilityModificationMakerChecker
                    context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerChecker);
                    context.Entry(responsibilityModificationMakerChecker).State = EntityState.Added;
                }

                // ResponsibilityTranslationMakerChecker
                context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.ActivationStatus = StringLiteralValue.Active;
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.EntryStatus = StringLiteralValue.Create;
                _responsibilityViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _responsibilityViewModel.ReasonForModification = "None";
                _responsibilityViewModel.Remark = "None";
                _responsibilityViewModel.TransReasonForModification = "None";
                _responsibilityViewModel.UserAction = StringLiteralValue.Create;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _responsibilityViewModel.ParentFunctionPrmKey = GetResponsibilityPrmKeyById(_responsibilityViewModel.ParentFunctionId);

                // Mapping
                // Responsibility
                Responsibility responsibility = Mapper.Map<Responsibility>(_responsibilityViewModel);
                ResponsibilityMakerChecker responsibilityMakerChecker = Mapper.Map<ResponsibilityMakerChecker>(_responsibilityViewModel);

                // ResponsibilityTranslation
                ResponsibilityTranslation responsibilityTranslation = Mapper.Map<ResponsibilityTranslation>(_responsibilityViewModel);
                ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                // ResponsibilityMakerChecker
                context.ResponsibilityMakerCheckers.Attach(responsibilityMakerChecker);
                context.Entry(responsibilityMakerChecker).State = EntityState.Added;
                responsibility.ResponsibilityMakerCheckers.Add(responsibilityMakerChecker);

                context.Responsibilities.Attach(responsibility);
                context.Entry(responsibility).State = EntityState.Added;

                // ResponsibilityTranslationMakerChecker
                context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;
                responsibilityTranslation.ResponsibilityTranslationMakerCheckers.Add(responsibilityTranslationMakerChecker);

                context.ResponsibilityTranslations.Attach(responsibilityTranslation);
                context.Entry(responsibilityTranslation).State = EntityState.Added;
                responsibility.ResponsibilityTranslations.Add(responsibilityTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(ResponsibilityViewModel _responsibilityViewModel)
        {
            try
            {
                // Set Default Value
                _responsibilityViewModel.EntryDateTime = DateTime.Now;
                _responsibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_responsibilityViewModel.ResponsibilityModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    ResponsibilityViewModel responsibilityViewModelForModify = await GetVerifiedEntry(_responsibilityViewModel.ResponsibilityId);

                    // Set Default Value
                    responsibilityViewModelForModify.UserAction = StringLiteralValue.Modify;
                    responsibilityViewModelForModify.UserProfilePrmKey = _responsibilityViewModel.UserProfilePrmKey;

                    // ResponsibilityTranslation
                    ResponsibilityTranslationMakerChecker responsibilityTranslationMakerCheckerForModify = Mapper.Map<ResponsibilityTranslationMakerChecker>(responsibilityViewModelForModify);

                    context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerCheckerForModify);
                    context.Entry(responsibilityTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (responsibilityViewModelForModify.IsModified == true)
                    {
                        ResponsibilityModificationMakerChecker responsibilityModificationMakerCheckerForModify = Mapper.Map<ResponsibilityModificationMakerChecker>(responsibilityViewModelForModify);

                        context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerCheckerForModify);
                        context.Entry(responsibilityModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _responsibilityViewModel.UserAction = StringLiteralValue.Verify;

                    ResponsibilityModificationMakerChecker responsibilityModificationMakerChecker = Mapper.Map<ResponsibilityModificationMakerChecker>(_responsibilityViewModel);
                    ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                    // ResponsibilityModificationMakerChecker
                    context.ResponsibilityModificationMakerCheckers.Attach(responsibilityModificationMakerChecker);
                    context.Entry(responsibilityModificationMakerChecker).State = EntityState.Added;

                    // ResponsibilityTranslationMakerChecker
                    context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                    context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _responsibilityViewModel.UserAction = StringLiteralValue.Verify;

                    ResponsibilityMakerChecker responsibilityMakerChecker = Mapper.Map<ResponsibilityMakerChecker>(_responsibilityViewModel);
                    ResponsibilityTranslationMakerChecker responsibilityTranslationMakerChecker = Mapper.Map<ResponsibilityTranslationMakerChecker>(_responsibilityViewModel);

                    // ResponsibilityMakerChecker
                    context.ResponsibilityMakerCheckers.Attach(responsibilityMakerChecker);
                    context.Entry(responsibilityMakerChecker).State = EntityState.Added;

                    // ResponsibilityTranslationMakerChecker
                    context.ResponsibilityTranslationMakerCheckers.Attach(responsibilityTranslationMakerChecker);
                    context.Entry(responsibilityTranslationMakerChecker).State = EntityState.Added;
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
