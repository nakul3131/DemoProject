using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Enterprise.Schedule;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Schedule;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.Enterprise.Schedule
{
    public class EFOfficeScheduleRepository : IOfficeScheduleRepository
    {
        private readonly EFDbContext context;

        private readonly IManagementDetailRepository managementDetailRepository;

        public EFOfficeScheduleRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository)
        {
            context             = _connection.EFDbContext;

            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _officeScheduleViewModel.EntryStatus = StringLiteralValue.Amend;
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _officeScheduleViewModel.ReasonForModification = "None";
                _officeScheduleViewModel.TransReasonForModification = "None";
                _officeScheduleViewModel.UserAction = StringLiteralValue.Amend;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _officeScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday1Id);
                _officeScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday2Id);

                //mapping
                //OfficeSchedule
                OfficeSchedule officeSchedule = Mapper.Map<OfficeSchedule>(_officeScheduleViewModel);
                OfficeScheduleMakerChecker officeScheduleMakerChecker = Mapper.Map<OfficeScheduleMakerChecker>(_officeScheduleViewModel);

                //OfficeScheduleModification
                OfficeScheduleModification officeScheduleModification = Mapper.Map<OfficeScheduleModification>(_officeScheduleViewModel);
                OfficeScheduleModificationMakerChecker officeScheduleModificationMakerChecker = Mapper.Map<OfficeScheduleModificationMakerChecker>(_officeScheduleViewModel);

                //OfficeScheduleTranslation
                OfficeScheduleTranslation officeScheduleTranslation = Mapper.Map<OfficeScheduleTranslation>(_officeScheduleViewModel);
                OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                officeSchedule.PrmKey = _officeScheduleViewModel.OfficeSchedulePrmKey;
                officeScheduleModification.PrmKey = _officeScheduleViewModel.OfficeScheduleModificationPrmKey;
                officeScheduleTranslation.PrmKey = _officeScheduleViewModel.OfficeScheduleTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_officeScheduleViewModel.OfficeScheduleModificationPrmKey == 0)
                {
                    // OfficeSchedule
                    context.OfficeSchedules.Attach(officeSchedule);
                    context.Entry(officeSchedule).State = EntityState.Modified;

                    context.OfficeScheduleMakerCheckers.Attach(officeScheduleMakerChecker);
                    context.Entry(officeScheduleMakerChecker).State = EntityState.Added;
                    officeSchedule.OfficeScheduleMakerCheckers.Add(officeScheduleMakerChecker);

                }
                else
                {
                    // OfficeScheduleModification
                    context.OfficeScheduleModifications.Attach(officeScheduleModification);
                    context.Entry(officeScheduleModification).State = EntityState.Modified;

                    context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerChecker);
                    context.Entry(officeScheduleModificationMakerChecker).State = EntityState.Added;
                    officeScheduleModification.OfficeScheduleModificationMakerCheckers.Add(officeScheduleModificationMakerChecker);

                }

                //OfficeScheduleTranslation
                context.OfficeScheduleTranslations.Attach(officeScheduleTranslation);
                context.Entry(officeScheduleTranslation).State = EntityState.Modified;

                context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;
                officeScheduleTranslation.OfficeScheduleTranslationMakerCheckers.Add(officeScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.Remark = "None";
                _officeScheduleViewModel.UserAction = StringLiteralValue.Delete;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                OfficeScheduleMakerChecker officeScheduleMakerChecker = Mapper.Map<OfficeScheduleMakerChecker>(_officeScheduleViewModel);
                OfficeScheduleModificationMakerChecker officeScheduleModificationMakerChecker = Mapper.Map<OfficeScheduleModificationMakerChecker>(_officeScheduleViewModel);
                OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                if (_officeScheduleViewModel.OfficeScheduleModificationPrmKey == 0)
                {
                    // OfficeSchedule
                    context.OfficeScheduleMakerCheckers.Attach(officeScheduleMakerChecker);
                    context.Entry(officeScheduleMakerChecker).State = EntityState.Added;

                }
                else
                {
                    // OfficeScheduleModification  
                    context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerChecker);
                    context.Entry(officeScheduleModificationMakerChecker).State = EntityState.Added;
                }

                //OfficeScheduleTranslation
                context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OfficeScheduleViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<OfficeScheduleViewModel> GetRejectedEntry(Guid _OfficeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntry (@OfficeScheduleId, @EntriesType)", new SqlParameter("@OfficeScheduleId", _OfficeScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueOfficeScheduleName(string _nameOfOfficeSchedule)
        {
            bool status;
            if (context.OfficeSchedules.Where(p => p.NameOfSchedule == _nameOfOfficeSchedule).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public Guid GetOfficeScheduleIdByPrmKey(int _prmKey)
        {
            return context.OfficeSchedules
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.OfficeScheduleId).FirstOrDefault();
        }

        public async Task<OfficeScheduleViewModel> GetUnVerifiedEntry(Guid _OfficeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntry (@OfficeScheduleId, @EntriesType)", new SqlParameter("@OfficeScheduleId", _OfficeScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<OfficeScheduleViewModel> GetVerifiedEntry(Guid _OfficeScheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<OfficeScheduleViewModel>("SELECT * FROM dbo.GetOfficeScheduleEntry (@OfficeScheduleId, @EntriesType)", new SqlParameter("@OfficeScheduleId", _OfficeScheduleId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _officeScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _officeScheduleViewModel.Remark = "None";
                _officeScheduleViewModel.UserAction = StringLiteralValue.Create;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _officeScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday1Id);
                _officeScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday2Id);

                //Mapping
                //OfficeScheduleModification
                OfficeScheduleModification officeScheduleModification = Mapper.Map<OfficeScheduleModification>(_officeScheduleViewModel);
                OfficeScheduleModificationMakerChecker officeScheduleModificationMakerChecker = Mapper.Map<OfficeScheduleModificationMakerChecker>(_officeScheduleViewModel);

                //OfficeScheduleTranslation
                OfficeScheduleTranslation officeScheduleTranslation = Mapper.Map<OfficeScheduleTranslation>(_officeScheduleViewModel);
                OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //OfficeScheduleModification
                context.OfficeScheduleModifications.Attach(officeScheduleModification);
                context.Entry(officeScheduleModification).State = EntityState.Added;

                context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerChecker);
                context.Entry(officeScheduleModificationMakerChecker).State = EntityState.Added;
                officeScheduleModification.OfficeScheduleModificationMakerCheckers.Add(officeScheduleModificationMakerChecker);

                //OfficeScheduleTranslation
                context.OfficeScheduleTranslations.Attach(officeScheduleTranslation);
                context.Entry(officeScheduleTranslation).State = EntityState.Added;

                context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;
                officeScheduleTranslation.OfficeScheduleTranslationMakerCheckers.Add(officeScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.UserAction = StringLiteralValue.Reject;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                OfficeScheduleMakerChecker officeScheduleMakerChecker = Mapper.Map<OfficeScheduleMakerChecker>(_officeScheduleViewModel);
                OfficeScheduleModificationMakerChecker officeScheduleModificationMakerChecker = Mapper.Map<OfficeScheduleModificationMakerChecker>(_officeScheduleViewModel);
                OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                if (_officeScheduleViewModel.OfficeScheduleModificationPrmKey == 0)
                {
                    // OfficeSchedule
                    context.OfficeScheduleMakerCheckers.Attach(officeScheduleMakerChecker);
                    context.Entry(officeScheduleMakerChecker).State = EntityState.Added;

                }
                else
                {
                    // OfficeScheduleModification  
                    context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerChecker);
                    context.Entry(officeScheduleModificationMakerChecker).State = EntityState.Added;
                }

                //OfficeScheduleTranslation
                context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _officeScheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _officeScheduleViewModel.ReasonForModification = "None";
                _officeScheduleViewModel.Remark = "None";
                _officeScheduleViewModel.TransReasonForModification = "None";
                _officeScheduleViewModel.UserAction = StringLiteralValue.Create;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _officeScheduleViewModel.WeeklyHoliday1 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday1Id);
                _officeScheduleViewModel.WeeklyHoliday2 = managementDetailRepository.GetDaysOfWeekPrmKeyById(_officeScheduleViewModel.WeeklyHoliday2Id);

                //Mapping
                //OfficeSchedule
                OfficeSchedule officeSchedule = Mapper.Map<OfficeSchedule>(_officeScheduleViewModel);
                OfficeScheduleMakerChecker officeScheduleMakerChecker = Mapper.Map<OfficeScheduleMakerChecker>(_officeScheduleViewModel);

                //OfficeScheduleTranslation
                OfficeScheduleTranslation officeScheduleTranslation = Mapper.Map<OfficeScheduleTranslation>(_officeScheduleViewModel);
                OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //OfficeSchedule
                context.OfficeSchedules.Attach(officeSchedule);
                context.Entry(officeSchedule).State = EntityState.Added;

                context.OfficeScheduleMakerCheckers.Attach(officeScheduleMakerChecker);
                context.Entry(officeScheduleMakerChecker).State = EntityState.Added;
                officeSchedule.OfficeScheduleMakerCheckers.Add(officeScheduleMakerChecker);

                //OfficeScheduleTranslation
                context.OfficeScheduleTranslations.Attach(officeScheduleTranslation);
                context.Entry(officeScheduleTranslation).State = EntityState.Added;
                officeSchedule.OfficeScheduleTranslations.Add(officeScheduleTranslation);

                context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;
                officeScheduleTranslation.OfficeScheduleTranslationMakerCheckers.Add(officeScheduleTranslationMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(OfficeScheduleViewModel _officeScheduleViewModel)
        {
            try
            {
                // Set Default Value
                _officeScheduleViewModel.EntryDateTime = DateTime.Now;
                _officeScheduleViewModel.UserAction = StringLiteralValue.Verify;
                _officeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _officeScheduleViewModel.OfficeScheduleId = GetOfficeScheduleIdByPrmKey(_officeScheduleViewModel.OfficeSchedulePrmKey);

                if (_officeScheduleViewModel.OfficeScheduleModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    OfficeScheduleViewModel officeScheduleViewModelForModify = await GetVerifiedEntry(_officeScheduleViewModel.OfficeScheduleId);

                    // Set Default Value
                    officeScheduleViewModelForModify.UserAction = StringLiteralValue.Modify;
                    officeScheduleViewModelForModify.UserProfilePrmKey = _officeScheduleViewModel.UserProfilePrmKey;
                    // OfficeScheduleTranslation
                    OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerCheckerForModify = Mapper.Map<OfficeScheduleTranslationMakerChecker>(officeScheduleViewModelForModify);

                    context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerCheckerForModify);
                    context.Entry(officeScheduleTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Check Entry Existance In Modification Table Or Main Table
                    if (officeScheduleViewModelForModify.IsModified == true)
                    {
                        OfficeScheduleModificationMakerChecker officeScheduleModificationMakerCheckerForModify = Mapper.Map<OfficeScheduleModificationMakerChecker>(officeScheduleViewModelForModify);

                        context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerCheckerForModify);
                        context.Entry(officeScheduleModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _officeScheduleViewModel.UserAction = StringLiteralValue.Verify;

                    OfficeScheduleModificationMakerChecker officeScheduleModificationMakerChecker = Mapper.Map<OfficeScheduleModificationMakerChecker>(_officeScheduleViewModel);
                    OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                    // OfficeScheduleModificationMakerChecker
                    context.OfficeScheduleModificationMakerCheckers.Attach(officeScheduleModificationMakerChecker);
                    context.Entry(officeScheduleModificationMakerChecker).State = EntityState.Added;

                    // OfficeScheduleTranslationMakerChecker
                    context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                    context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _officeScheduleViewModel.UserAction = StringLiteralValue.Verify;

                    OfficeScheduleMakerChecker officeScheduleMakerChecker = Mapper.Map<OfficeScheduleMakerChecker>(_officeScheduleViewModel);
                    OfficeScheduleTranslationMakerChecker officeScheduleTranslationMakerChecker = Mapper.Map<OfficeScheduleTranslationMakerChecker>(_officeScheduleViewModel);

                    // OfficeScheduleMakerChecker
                    context.OfficeScheduleMakerCheckers.Attach(officeScheduleMakerChecker);
                    context.Entry(officeScheduleMakerChecker).State = EntityState.Added;

                    // OfficeScheduleTranslationMakerChecker
                    context.OfficeScheduleTranslationMakerCheckers.Attach(officeScheduleTranslationMakerChecker);
                    context.Entry(officeScheduleTranslationMakerChecker).State = EntityState.Added;
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
