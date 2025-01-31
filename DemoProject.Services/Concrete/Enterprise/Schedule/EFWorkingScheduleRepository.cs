using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Enterprise.Schedule;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Schedule;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.Enterprise.Schedule
{
    public class EFWorkingScheduleRepository : IWorkingScheduleRepository
    {
        private readonly EFDbContext context;

        private readonly IManagementDetailRepository managementDetailRepository;

        public EFWorkingScheduleRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;

            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _workingScheduleViewModel.EntryStatus = StringLiteralValue.Amend;
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _workingScheduleViewModel.ReasonForModification = "None";
                _workingScheduleViewModel.TransReasonForModification = "None";
                _workingScheduleViewModel.UserAction = StringLiteralValue.Amend;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _workingScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday1Id);
                _workingScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday2Id);

                //mapping
                //WorkingSchedule
                WorkingSchedule workingSchedule = Mapper.Map<WorkingSchedule>(_workingScheduleViewModel);
                WorkingScheduleMakerChecker workingScheduleMakerChecker = Mapper.Map<WorkingScheduleMakerChecker>(_workingScheduleViewModel);

                //WorkingScheduleModification
                WorkingScheduleModification workingScheduleModification = Mapper.Map<WorkingScheduleModification>(_workingScheduleViewModel);
                WorkingScheduleModificationMakerChecker workingScheduleModificationMakerChecker = Mapper.Map<WorkingScheduleModificationMakerChecker>(_workingScheduleViewModel);

                //WorkingScheduleTranslation
                WorkingScheduleTranslation workingScheduleTranslation = Mapper.Map<WorkingScheduleTranslation>(_workingScheduleViewModel);
                WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                workingSchedule.PrmKey = _workingScheduleViewModel.WorkingSchedulePrmKey;
                workingScheduleModification.PrmKey = _workingScheduleViewModel.WorkingScheduleModificationPrmKey;
                workingScheduleTranslation.PrmKey = _workingScheduleViewModel.WorkingScheduleTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_workingScheduleViewModel.WorkingScheduleModificationPrmKey == 0)
                {
                    // WorkingSchedule
                    context.WorkingSchedules.Attach(workingSchedule);
                    context.Entry(workingSchedule).State = EntityState.Modified;

                    context.WorkingScheduleMakerCheckers.Attach(workingScheduleMakerChecker);
                    context.Entry(workingScheduleMakerChecker).State = EntityState.Added;
                    workingSchedule.WorkingScheduleMakerCheckers.Add(workingScheduleMakerChecker);

                }
                else
                {
                    // WorkingScheduleModification
                    context.WorkingScheduleModifications.Attach(workingScheduleModification);
                    context.Entry(workingScheduleModification).State = EntityState.Modified;

                    context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerChecker);
                    context.Entry(workingScheduleModificationMakerChecker).State = EntityState.Added;
                    workingScheduleModification.WorkingScheduleModificationMakerCheckers.Add(workingScheduleModificationMakerChecker);

                }

                //WorkingScheduleTranslation
                context.WorkingScheduleTranslations.Attach(workingScheduleTranslation);
                context.Entry(workingScheduleTranslation).State = EntityState.Modified;

                context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;
                workingScheduleTranslation.WorkingScheduleTranslationMakerCheckers.Add(workingScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.Remark = "None";
                _workingScheduleViewModel.UserAction = StringLiteralValue.Delete;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                WorkingScheduleMakerChecker workingScheduleMakerChecker = Mapper.Map<WorkingScheduleMakerChecker>(_workingScheduleViewModel);
                WorkingScheduleModificationMakerChecker workingScheduleModificationMakerChecker = Mapper.Map<WorkingScheduleModificationMakerChecker>(_workingScheduleViewModel);
                WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                if (_workingScheduleViewModel.WorkingScheduleModificationPrmKey == 0)
                {
                    // WorkingSchedule
                    context.WorkingScheduleMakerCheckers.Attach(workingScheduleMakerChecker);
                    context.Entry(workingScheduleMakerChecker).State = EntityState.Added;

                }
                else
                {
                    // WorkingScheduleModification  
                    context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerChecker);
                    context.Entry(workingScheduleModificationMakerChecker).State = EntityState.Added;
                }

                //WorkingScheduleTranslation
                context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<WorkingScheduleViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public byte GetPrmKeyById(Guid _workingScheduleId)
        {
            return context.WorkingSchedules
                    .Where(c => c.WorkingScheduleId == _workingScheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<WorkingScheduleViewModel> GetRejectedEntry(Guid _WorkingScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntry (@WorkingScheduleId, @EntriesType)", new SqlParameter("@WorkingScheduleId", _WorkingScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueWorkingScheduleName(string _nameOfWorkingSchedule)
        {
            bool status;
            if (context.WorkingSchedules.Where(p => p.NameOfSchedule == _nameOfWorkingSchedule).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public Guid GetWorkingScheduleIdByPrmKey(int _prmKey)
        {
            return context.WorkingSchedules
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.WorkingScheduleId).FirstOrDefault();
        }

        public async Task<WorkingScheduleViewModel> GetUnVerifiedEntry(Guid _WorkingScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntry (@WorkingScheduleId, @EntriesType)", new SqlParameter("@WorkingScheduleId", _WorkingScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<WorkingScheduleViewModel> GetVerifiedEntry(Guid _WorkingScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<WorkingScheduleViewModel>("SELECT * FROM dbo.GetWorkingScheduleEntry (@WorkingScheduleId, @EntriesType)", new SqlParameter("@WorkingScheduleId", _WorkingScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _workingScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _workingScheduleViewModel.Remark = "None";
                _workingScheduleViewModel.UserAction = StringLiteralValue.Create;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _workingScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday1Id);
                _workingScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday2Id);

                //map
                //Mapping
                //WorkingScheduleModification
                WorkingScheduleModification workingScheduleModification = Mapper.Map<WorkingScheduleModification>(_workingScheduleViewModel);
                WorkingScheduleModificationMakerChecker workingScheduleModificationMakerChecker = Mapper.Map<WorkingScheduleModificationMakerChecker>(_workingScheduleViewModel);

                //WorkingScheduleTranslation
                WorkingScheduleTranslation workingScheduleTranslation = Mapper.Map<WorkingScheduleTranslation>(_workingScheduleViewModel);
                WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //WorkingScheduleModification
                context.WorkingScheduleModifications.Attach(workingScheduleModification);
                context.Entry(workingScheduleModification).State = EntityState.Added;

                context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerChecker);
                context.Entry(workingScheduleModificationMakerChecker).State = EntityState.Added;
                workingScheduleModification.WorkingScheduleModificationMakerCheckers.Add(workingScheduleModificationMakerChecker);

                //WorkingScheduleTranslation
                context.WorkingScheduleTranslations.Attach(workingScheduleTranslation);
                context.Entry(workingScheduleTranslation).State = EntityState.Added;

                context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;
                workingScheduleTranslation.WorkingScheduleTranslationMakerCheckers.Add(workingScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.UserAction = StringLiteralValue.Reject;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                WorkingScheduleMakerChecker workingScheduleMakerChecker = Mapper.Map<WorkingScheduleMakerChecker>(_workingScheduleViewModel);
                WorkingScheduleModificationMakerChecker workingScheduleModificationMakerChecker = Mapper.Map<WorkingScheduleModificationMakerChecker>(_workingScheduleViewModel);
                WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                if (_workingScheduleViewModel.WorkingScheduleModificationPrmKey == 0)
                {
                    // WorkingSchedule
                    context.WorkingScheduleMakerCheckers.Attach(workingScheduleMakerChecker);
                    context.Entry(workingScheduleMakerChecker).State = EntityState.Added;

                }
                else
                {
                    // WorkingScheduleModification  
                    context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerChecker);
                    context.Entry(workingScheduleModificationMakerChecker).State = EntityState.Added;
                }

                //WorkingScheduleTranslation
                context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _workingScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _workingScheduleViewModel.ReasonForModification = "None";
                _workingScheduleViewModel.Remark = "None";
                _workingScheduleViewModel.TransReasonForModification = "None";
                _workingScheduleViewModel.UserAction = StringLiteralValue.Create;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _workingScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday1Id);
                _workingScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_workingScheduleViewModel.WeeklyHoliday2Id);

                //Mapping
                //WorkingSchedule
                WorkingSchedule workingSchedule = Mapper.Map<WorkingSchedule>(_workingScheduleViewModel);
                WorkingScheduleMakerChecker workingScheduleMakerChecker = Mapper.Map<WorkingScheduleMakerChecker>(_workingScheduleViewModel);

                //WorkingScheduleTranslation
                WorkingScheduleTranslation workingScheduleTranslation = Mapper.Map<WorkingScheduleTranslation>(_workingScheduleViewModel);
                WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //WorkingSchedule
                context.WorkingSchedules.Attach(workingSchedule);
                context.Entry(workingSchedule).State = EntityState.Added;

                context.WorkingScheduleMakerCheckers.Attach(workingScheduleMakerChecker);
                context.Entry(workingScheduleMakerChecker).State = EntityState.Added;
                workingSchedule.WorkingScheduleMakerCheckers.Add(workingScheduleMakerChecker);

                //WorkingScheduleTranslation
                context.WorkingScheduleTranslations.Attach(workingScheduleTranslation);
                context.Entry(workingScheduleTranslation).State = EntityState.Added;
                workingSchedule.WorkingScheduleTranslations.Add(workingScheduleTranslation);

                context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;
                workingScheduleTranslation.WorkingScheduleTranslationMakerCheckers.Add(workingScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(WorkingScheduleViewModel _workingScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _workingScheduleViewModel.EntryDateTime = DateTime.Now;
                _workingScheduleViewModel.UserAction = StringLiteralValue.Verify;
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _workingScheduleViewModel.WorkingScheduleId = GetWorkingScheduleIdByPrmKey(_workingScheduleViewModel.WorkingSchedulePrmKey);

                if (_workingScheduleViewModel.WorkingScheduleModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    WorkingScheduleViewModel workingScheduleViewModelForModify = await GetVerifiedEntry(_workingScheduleViewModel.WorkingScheduleId);

                    // Set Default Value
                    workingScheduleViewModelForModify.UserAction = StringLiteralValue.Modify;
                    workingScheduleViewModelForModify.UserProfilePrmKey = _workingScheduleViewModel.UserProfilePrmKey;
                    // WorkingScheduleTranslation
                    WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerCheckerForModify = Mapper.Map<WorkingScheduleTranslationMakerChecker>(workingScheduleViewModelForModify);

                    context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerCheckerForModify);
                    context.Entry(workingScheduleTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Check Entry Existance In Modification Table Or Main Table
                    if (workingScheduleViewModelForModify.IsModified == true)
                    {
                        WorkingScheduleModificationMakerChecker workingScheduleModificationMakerCheckerForModify = Mapper.Map<WorkingScheduleModificationMakerChecker>(workingScheduleViewModelForModify);

                        context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerCheckerForModify);
                        context.Entry(workingScheduleModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _workingScheduleViewModel.UserAction = StringLiteralValue.Verify;

                    WorkingScheduleModificationMakerChecker workingScheduleModificationMakerChecker = Mapper.Map<WorkingScheduleModificationMakerChecker>(_workingScheduleViewModel);
                    WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                    // WorkingScheduleModificationMakerChecker
                    context.WorkingScheduleModificationMakerCheckers.Attach(workingScheduleModificationMakerChecker);
                    context.Entry(workingScheduleModificationMakerChecker).State = EntityState.Added;

                    // WorkingScheduleTranslationMakerChecker
                    context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                    context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _workingScheduleViewModel.UserAction = StringLiteralValue.Verify;

                    WorkingScheduleMakerChecker workingScheduleMakerChecker = Mapper.Map<WorkingScheduleMakerChecker>(_workingScheduleViewModel);
                    WorkingScheduleTranslationMakerChecker workingScheduleTranslationMakerChecker = Mapper.Map<WorkingScheduleTranslationMakerChecker>(_workingScheduleViewModel);

                    // WorkingScheduleMakerChecker
                    context.WorkingScheduleMakerCheckers.Attach(workingScheduleMakerChecker);
                    context.Entry(workingScheduleMakerChecker).State = EntityState.Added;

                    // WorkingScheduleTranslationMakerChecker
                    context.WorkingScheduleTranslationMakerCheckers.Attach(workingScheduleTranslationMakerChecker);
                    context.Entry(workingScheduleTranslationMakerChecker).State = EntityState.Added;
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

        public List<SelectListItem> WorkingSchedules
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.WorkingSchedules
                            join mf in context.WorkingScheduleModifications on o.PrmKey equals mf.WorkingSchedulePrmKey into om
                            from mf in om.DefaultIfEmpty()
                            join t in context.WorkingScheduleTranslations on o.PrmKey equals t.WorkingSchedulePrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.WorkingScheduleId.ToString(),
                                Text = ((mf.NameOfSchedule.Equals(null)) ? o.NameOfSchedule.Trim() + " ---> " + (t.TransNameOfSchedule.Equals(null) ? " " : t.TransNameOfSchedule.Trim()) : mf.NameOfSchedule + " ---> " + (t.TransNameOfSchedule.Equals(null) ? " " : t.TransNameOfSchedule.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.WorkingSchedules
                        join mf in context.WorkingScheduleModifications on o.PrmKey equals mf.WorkingSchedulePrmKey into om
                        from mf in om.DefaultIfEmpty()
                        where (o.EntryStatus.Equals(StringLiteralValue.Verify) && o.ActivationStatus.Equals(StringLiteralValue.Active)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = o.WorkingScheduleId.ToString(),
                            Text = ((mf.NameOfSchedule.Equals(null)) ? o.NameOfSchedule.Trim() : mf.NameOfSchedule)
                        }).ToList();

            }
        }

    }
}
