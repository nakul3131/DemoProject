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
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFEvaluationSectionRepository : IEvaluationSectionRepository
    {
        private readonly EFDbContext context;

        public EFEvaluationSectionRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.EntryStatus = StringLiteralValue.Amend;
                _evaluationSectionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _evaluationSectionViewModel.ReasonForModification = "None";
                _evaluationSectionViewModel.TransReasonForModification = "None";
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Amend;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //EvaluationSection
                EvaluationSection evaluationSection = Mapper.Map<EvaluationSection>(_evaluationSectionViewModel);
                EvaluationSectionMakerChecker evaluationSectionMakerChecker = Mapper.Map<EvaluationSectionMakerChecker>(_evaluationSectionViewModel);

                //EvaluationSectionTranslation
                EvaluationSectionTranslation evaluationSectionTranslation = Mapper.Map<EvaluationSectionTranslation>(_evaluationSectionViewModel);
                EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                //EvaluationSectionModification
                EvaluationSectionModification evaluationSectionModification = Mapper.Map<EvaluationSectionModification>(_evaluationSectionViewModel);
                EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerChecker = Mapper.Map<EvaluationSectionModificationMakerChecker>(_evaluationSectionViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                evaluationSection.PrmKey = _evaluationSectionViewModel.EvaluationSectionPrmKey;
                evaluationSectionTranslation.PrmKey = _evaluationSectionViewModel.EvaluationSectionTranslationPrmKey;
                evaluationSectionModification.PrmKey = _evaluationSectionViewModel.EvaluationSectionModificationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_evaluationSectionViewModel.EvaluationSectionModificationPrmKey == 0)
                {
                    // EvaluationSection
                    context.EvaluationSectionMakerCheckers.Attach(evaluationSectionMakerChecker);
                    context.Entry(evaluationSectionMakerChecker).State = EntityState.Added;
                    evaluationSection.EvaluationSectionMakerCheckers.Add(evaluationSectionMakerChecker);

                    context.EvaluationSections.Attach(evaluationSection);
                    context.Entry(evaluationSection).State = EntityState.Modified;
                }
                else
                {
                    // EvaluationSection Modification 
                    context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerChecker);
                    context.Entry(evaluationSectionModificationMakerChecker).State = EntityState.Added;
                    evaluationSectionModification.EvaluationSectionModificationMakerCheckers.Add(evaluationSectionModificationMakerChecker);

                    context.EvaluationSectionModifications.Attach(evaluationSectionModification);
                    context.Entry(evaluationSectionModification).State = EntityState.Modified;
                }

                //EvaluationSectionTranslation
                context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;
                evaluationSectionTranslation.EvaluationSectionTranslationMakerCheckers.Add(evaluationSectionTranslationMakerChecker);

                context.EvaluationSectionTranslations.Attach(evaluationSectionTranslation);
                context.Entry(evaluationSectionTranslation).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Delete;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                EvaluationSectionMakerChecker evaluationSectionMakerChecker = Mapper.Map<EvaluationSectionMakerChecker>(_evaluationSectionViewModel);
                EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerChecker = Mapper.Map<EvaluationSectionModificationMakerChecker>(_evaluationSectionViewModel);
                EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                if (_evaluationSectionViewModel.EvaluationSectionModificationPrmKey == 0)
                {
                    // EvaluationSection
                    context.EvaluationSectionMakerCheckers.Attach(evaluationSectionMakerChecker);
                    context.Entry(evaluationSectionMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // EvaluationSectionModification  
                    context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerChecker);
                    context.Entry(evaluationSectionModificationMakerChecker).State = EntityState.Added;
                }

                //EvaluationSectionTranslation
                context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public List<SelectListItem> EvaluationSectionDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.EvaluationSections
                            join mf in context.EvaluationSectionModifications on o.PrmKey equals mf.EvaluationSectionPrmKey into om
                            from mf in om.DefaultIfEmpty()
                            join t in context.EvaluationSectionTranslations on o.PrmKey equals t.EvaluationSectionPrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.EvaluationSectionId.ToString(),
                                Text = ((mf.NameOfEvaluationSection.Equals(null)) ? o.NameOfEvaluationSection.Trim() + " ---> " + (t.TransNameOfEvaluationSection.Equals(null) ? " " : t.TransNameOfEvaluationSection.Trim()) : mf.NameOfEvaluationSection + " ---> " + (t.TransNameOfEvaluationSection.Equals(null) ? " " : t.TransNameOfEvaluationSection.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.EvaluationSections
                        join mf in context.EvaluationSectionModifications on o.PrmKey equals mf.EvaluationSectionPrmKey into om
                        from mf in om.DefaultIfEmpty()
                        where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = o.EvaluationSectionId.ToString(),
                            Text = ((mf.NameOfEvaluationSection.Equals(null)) ? o.NameOfEvaluationSection.Trim() : mf.NameOfEvaluationSection)
                        }).ToList();

            }
        }

        public async Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EvaluationSectionViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPrmKeyById(Guid _EvaluationSectionId)
        {
            return context.EvaluationSections
                    .Where(c => c.EvaluationSectionId == _EvaluationSectionId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<EvaluationSectionViewModel> GetRejectedEntry(Guid _evaluationSectionId)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntry (@EvaluationSectionId, @EntriesType)", new SqlParameter("@EvaluationSectionId", _evaluationSectionId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EvaluationSectionViewModel> GetUnVerifiedEntry(Guid _evaluationSectionId)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntry (@EvaluationSectionId, @EntriesType)", new SqlParameter("@EvaluationSectionId", _evaluationSectionId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueEvaluationSectionName(string _nameOfEvaluationSection)
        {
            bool status;
            if (context.EvaluationSections.Where(p => p.NameOfEvaluationSection == _nameOfEvaluationSection).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public async Task<EvaluationSectionViewModel> GetVerifiedEntry(Guid _evaluationSectionId)
        {
            try
            {
                return await context.Database.SqlQuery<EvaluationSectionViewModel>("SELECT * FROM dbo.GetEvaluationSectionEntry (@EvaluationSectionId, @EntriesType)", new SqlParameter("@EvaluationSectionId", _evaluationSectionId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.EntryStatus = StringLiteralValue.Create;
                _evaluationSectionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _evaluationSectionViewModel.Remark = "None";
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Create;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //EvaluationSectionModification
                EvaluationSectionModification evaluationSectionModification = Mapper.Map<EvaluationSectionModification>(_evaluationSectionViewModel);
                EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerChecker = Mapper.Map<EvaluationSectionModificationMakerChecker>(_evaluationSectionViewModel);

                //EvaluationSectionTranslation
                EvaluationSectionTranslation evaluationSectionTranslation = Mapper.Map<EvaluationSectionTranslation>(_evaluationSectionViewModel);
                EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //EvaluationSectionModification
                context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerChecker);
                context.Entry(evaluationSectionModificationMakerChecker).State = EntityState.Added;
                evaluationSectionModification.EvaluationSectionModificationMakerCheckers.Add(evaluationSectionModificationMakerChecker);

                context.EvaluationSectionModifications.Attach(evaluationSectionModification);
                context.Entry(evaluationSectionModification).State = EntityState.Added;

                //EvaluationSectionTranslation
                context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;
                evaluationSectionTranslation.EvaluationSectionTranslationMakerCheckers.Add(evaluationSectionTranslationMakerChecker);

                context.EvaluationSectionTranslations.Attach(evaluationSectionTranslation);
                context.Entry(evaluationSectionTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Reject;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                EvaluationSectionMakerChecker evaluationSectionMakerChecker = Mapper.Map<EvaluationSectionMakerChecker>(_evaluationSectionViewModel);
                EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerChecker = Mapper.Map<EvaluationSectionModificationMakerChecker>(_evaluationSectionViewModel);
                EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                // Check Entry Existance In Modification Table Or Main Table
                if (_evaluationSectionViewModel.EvaluationSectionModificationPrmKey == 0)
                {
                    // EvaluationSectionMakerChecker
                    context.EvaluationSectionMakerCheckers.Attach(evaluationSectionMakerChecker);
                    context.Entry(evaluationSectionMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // EvaluationSectionModificationMakerChecker
                    context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerChecker);
                    context.Entry(evaluationSectionModificationMakerChecker).State = EntityState.Added;
                }

                //EvaluationSectionTranslationMakerChecker
                context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.ActivationStatus = StringLiteralValue.Active;
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.EntryStatus = StringLiteralValue.Create;
                _evaluationSectionViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _evaluationSectionViewModel.Remark = "None";
                _evaluationSectionViewModel.ReasonForModification = "None";
                _evaluationSectionViewModel.TransReasonForModification = "None";
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Create;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //EvaluationSection
                EvaluationSection evaluationSection = Mapper.Map<EvaluationSection>(_evaluationSectionViewModel);
                EvaluationSectionMakerChecker evaluationSectionMakerChecker = Mapper.Map<EvaluationSectionMakerChecker>(_evaluationSectionViewModel);

                //EvaluationSectionTranslation
                EvaluationSectionTranslation evaluationSectionTranslation = Mapper.Map<EvaluationSectionTranslation>(_evaluationSectionViewModel);
                EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //EvaluationSection
                context.EvaluationSectionMakerCheckers.Attach(evaluationSectionMakerChecker);
                context.Entry(evaluationSectionMakerChecker).State = EntityState.Added;
                evaluationSection.EvaluationSectionMakerCheckers.Add(evaluationSectionMakerChecker);

                context.EvaluationSections.Attach(evaluationSection);
                context.Entry(evaluationSection).State = EntityState.Added;

                //EvaluationSectionTranslation
                context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;
                evaluationSectionTranslation.EvaluationSectionTranslationMakerCheckers.Add(evaluationSectionTranslationMakerChecker);

                context.EvaluationSectionTranslations.Attach(evaluationSectionTranslation);
                context.Entry(evaluationSectionTranslation).State = EntityState.Added;
                evaluationSection.EvaluationSectionTranslations.Add(evaluationSectionTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            try
            {
                // Set Default Value
                _evaluationSectionViewModel.EntryDateTime = DateTime.Now;
                _evaluationSectionViewModel.UserAction = StringLiteralValue.Verify;
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_evaluationSectionViewModel.EvaluationSectionModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    EvaluationSectionViewModel evaluationSectionViewModelForModify = await GetVerifiedEntry(_evaluationSectionViewModel.EvaluationSectionId);

                    // Set Default Value
                    evaluationSectionViewModelForModify.UserAction = StringLiteralValue.Modify;
                    evaluationSectionViewModelForModify.UserProfilePrmKey = _evaluationSectionViewModel.UserProfilePrmKey;
                    // EvaluationSectionTranslation
                    EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerCheckerForModify = Mapper.Map<EvaluationSectionTranslationMakerChecker>(evaluationSectionViewModelForModify);

                    context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerCheckerForModify);
                    context.Entry(evaluationSectionTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Check Entry Existance In Modification Table Or Main Table
                    if (evaluationSectionViewModelForModify.IsModified == true)
                    {
                        EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerCheckerForModify = Mapper.Map<EvaluationSectionModificationMakerChecker>(evaluationSectionViewModelForModify);

                        context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerCheckerForModify);
                        context.Entry(evaluationSectionModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _evaluationSectionViewModel.UserAction = StringLiteralValue.Verify;

                    EvaluationSectionModificationMakerChecker evaluationSectionModificationMakerChecker = Mapper.Map<EvaluationSectionModificationMakerChecker>(_evaluationSectionViewModel);
                    EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                    // EvaluationSectionModificationMakerChecker
                    context.EvaluationSectionModificationMakerCheckers.Attach(evaluationSectionModificationMakerChecker);
                    context.Entry(evaluationSectionModificationMakerChecker).State = EntityState.Added;

                    // EvaluationSectionTranslationMakerChecker
                    context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                    context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _evaluationSectionViewModel.UserAction = StringLiteralValue.Verify;

                    EvaluationSectionMakerChecker evaluationSectionMakerChecker = Mapper.Map<EvaluationSectionMakerChecker>(_evaluationSectionViewModel);
                    EvaluationSectionTranslationMakerChecker evaluationSectionTranslationMakerChecker = Mapper.Map<EvaluationSectionTranslationMakerChecker>(_evaluationSectionViewModel);

                    // EvaluationSectionMakerChecker
                    context.EvaluationSectionMakerCheckers.Attach(evaluationSectionMakerChecker);
                    context.Entry(evaluationSectionMakerChecker).State = EntityState.Added;

                    // EvaluationSectionTranslationMakerChecker
                    context.EvaluationSectionTranslationMakerCheckers.Attach(evaluationSectionTranslationMakerChecker);
                    context.Entry(evaluationSectionTranslationMakerChecker).State = EntityState.Added;
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
