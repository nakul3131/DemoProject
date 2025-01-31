using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.Entities.Management.SystemEntity;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFScheduleRepository : IScheduleRepository
    {
        private readonly EFDbContext context;
        private readonly IScheduleFrequencyRepository scheduleFrequencyRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public EFScheduleRepository(RepositoryConnection _connection, IScheduleFrequencyRepository _scheduleFrequencyRepository,IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            scheduleFrequencyRepository = _scheduleFrequencyRepository;
            managementDetailRepository = _managementDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.ActivationStatus = StringLiteralValue.Active;
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.EntryStatus = StringLiteralValue.Amend;
                _scheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _scheduleViewModel.ReasonForModification = "None";
                _scheduleViewModel.Remark = _scheduleViewModel.Remark;
                _scheduleViewModel.TransReasonForModification = "None";
                _scheduleViewModel.UserAction = StringLiteralValue.Amend;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // Schedule
                Schedule scheduleForAmend = Mapper.Map<Schedule>(_scheduleViewModel);
                ScheduleMakerChecker scheduleMakerCheckerForAmend = Mapper.Map<ScheduleMakerChecker>(_scheduleViewModel);

                // ScheduleModification
                ScheduleModification scheduleModificationForAmend = Mapper.Map<ScheduleModification>(_scheduleViewModel);
                ScheduleModificationMakerChecker scheduleModificationMakerCheckerForAmend = Mapper.Map<ScheduleModificationMakerChecker>(_scheduleViewModel);

                // ScheduleTranslation
                ScheduleTranslation scheduleTranslationForAmend = Mapper.Map<ScheduleTranslation>(_scheduleViewModel);
                ScheduleTranslationMakerChecker scheduleTranslationMakerCheckerForAmend = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                scheduleForAmend.PrmKey = _scheduleViewModel.SchedulePrmKey;
                scheduleModificationForAmend.PrmKey = _scheduleViewModel.ScheduleModificationPrmKey;
                scheduleTranslationForAmend.PrmKey = _scheduleViewModel.ScheduleTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_scheduleViewModel.ScheduleModificationPrmKey == 0)
                {
                    // Schedule
                    context.ScheduleMakerCheckers.Attach(scheduleMakerCheckerForAmend);
                    context.Entry(scheduleMakerCheckerForAmend).State = EntityState.Added;
                    scheduleForAmend.ScheduleMakerCheckers.Add(scheduleMakerCheckerForAmend);

                    context.Schedules.Attach(scheduleForAmend);
                    context.Entry(scheduleForAmend).State = EntityState.Modified;
                }
                else
                {
                    // Schedule Modification 
                    context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerCheckerForAmend);
                    context.Entry(scheduleModificationMakerCheckerForAmend).State = EntityState.Added;
                    scheduleModificationForAmend.ScheduleModificationMakerCheckers.Add(scheduleModificationMakerCheckerForAmend);

                    context.ScheduleModifications.Attach(scheduleModificationForAmend);
                    context.Entry(scheduleModificationForAmend).State = EntityState.Modified;
                }

                // ScheduleTranslation
                context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerCheckerForAmend);
                context.Entry(scheduleTranslationMakerCheckerForAmend).State = EntityState.Added;
                scheduleTranslationForAmend.ScheduleTranslationMakerCheckers.Add(scheduleTranslationMakerCheckerForAmend);

                context.ScheduleTranslations.Attach(scheduleTranslationForAmend);
                context.Entry(scheduleTranslationForAmend).State = EntityState.Modified;

                // ScheduleTradingEntity - Amend Old Record
                IEnumerable<ScheduleFrequencyViewModel> scheduleFrequencyViewModelForAmend = await scheduleFrequencyRepository.GetRejectedEntries(_scheduleViewModel.SchedulePrmKey);

                foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelForAmend)
                {
                    viewModel.EntryStatus = StringLiteralValue.Amend;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerCheckerForAmend = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerCheckerForAmend);
                    context.Entry(scheduleFrequencyMakerCheckerForAmend).State = EntityState.Added;
                }

                // ScheduleTradingEntity - Add New Amended Entry, Get ScheduleTradingEntity Details From Session Object
                List<ScheduleFrequencyViewModel> scheduleFrequencyViewModelList = new List<ScheduleFrequencyViewModel>();

                scheduleFrequencyViewModelList = (List<ScheduleFrequencyViewModel>)HttpContext.Current.Session["ScheduleFrequency"];

                foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelList)
                {
                    viewModel.ScheduleFrequencyPrmKey = 0;
                    viewModel.SchedulePrmKey = _scheduleViewModel.SchedulePrmKey;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _scheduleViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get Prmkey By Id
                    viewModel.ScheduleTypePrmKey = managementDetailRepository.GetScheduleTypePrmKeyById(viewModel.ScheduleTypeId);
                    viewModel.DaysOfWeekPrmKey = managementDetailRepository.GetDaysOfWeekPrmKeyById(viewModel.DaysOfWeekId);
                    viewModel.DaysOfMonthPrmKey = managementDetailRepository.GetDaysOfMonthPrmKeyById(viewModel.DaysOfMonthId);

                    ScheduleFrequency scheduleFrequency = Mapper.Map<ScheduleFrequency>(viewModel);
                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                    context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
                    scheduleFrequency.ScheduleFrequencyMakerCheckers.Add(scheduleFrequencyMakerChecker);

                    context.ScheduleFrequencies.Attach(scheduleFrequency);
                    context.Entry(scheduleFrequency).State = EntityState.Added;
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

        public async Task<bool> Delete(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.ReasonForModification = "None";
                _scheduleViewModel.Remark = "None";
                _scheduleViewModel.UserAction = StringLiteralValue.Delete;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ScheduleMakerChecker scheduleMakerChecker = Mapper.Map<ScheduleMakerChecker>(_scheduleViewModel);
                ScheduleModificationMakerChecker scheduleModificationMakerChecker = Mapper.Map<ScheduleModificationMakerChecker>(_scheduleViewModel);
                ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                if (_scheduleViewModel.ScheduleModificationPrmKey == 0)
                {
                    // Schedule
                    context.ScheduleMakerCheckers.Attach(scheduleMakerChecker);
                    context.Entry(scheduleMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ScheduleModification  
                    context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerChecker);
                    context.Entry(scheduleModificationMakerChecker).State = EntityState.Added;
                }

                //ScheduleTranslation
                context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;

                //ScheduleFrequency
                List<ScheduleFrequencyViewModel> scheduleFrequencyViewModelList = new List<ScheduleFrequencyViewModel>();
                scheduleFrequencyViewModelList = (List<ScheduleFrequencyViewModel>)HttpContext.Current.Session["ScheduleFrequency"];

                foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                    context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
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

        public byte GetlistofScheduleType(Guid ScheduleTypeId)
        {
            return context.ScheduleTypes.Where(c => c.ScheduleTypeId == ScheduleTypeId).Select(c => c.PrmKey).FirstOrDefault();
        }
        
        public async Task<IEnumerable<ScheduleViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ScheduleViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ScheduleViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ScheduleViewModel> GetRejectedEntry(Guid _scheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntry (@ScheduleId, @EntriesType)", new SqlParameter("@ScheduleId", _scheduleId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueScheduleName(string _nameOfSchedule)
        {
            bool status;
            if (context.Schedules.Where(p => p.NameOfSchedule == _nameOfSchedule).Select(p => p.PrmKey).FirstOrDefault() > 0)
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

        public async Task<ScheduleViewModel> GetUnVerifiedEntry(Guid _scheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntry (@ScheduleId, @EntriesType)", new SqlParameter("@ScheduleId", _scheduleId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ScheduleViewModel> GetVerifiedEntry(Guid _scheduleId)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleViewModel>("SELECT * FROM dbo.GetScheduleEntry (@ScheduleId, @EntriesType)", new SqlParameter("@ScheduleId", _scheduleId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.ScheduleTranslationPrmKey = 0;
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _scheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _scheduleViewModel.Remark = "None";
                _scheduleViewModel.UserAction = StringLiteralValue.Create;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // ScheduleModification
                ScheduleModification scheduleModification = Mapper.Map<ScheduleModification>(_scheduleViewModel);
                ScheduleModificationMakerChecker scheduleModificationMakerChecker = Mapper.Map<ScheduleModificationMakerChecker>(_scheduleViewModel);

                // ScheduleTranslation
                ScheduleTranslation scheduleTranslation = Mapper.Map<ScheduleTranslation>(_scheduleViewModel);
                ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                // ScheduleModification
                context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerChecker);
                context.Entry(scheduleModificationMakerChecker).State = EntityState.Added;
                scheduleModification.ScheduleModificationMakerCheckers.Add(scheduleModificationMakerChecker);

                context.ScheduleModifications.Attach(scheduleModification);
                context.Entry(scheduleModification).State = EntityState.Added;

                // ScheduleTranslation
                context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;
                scheduleTranslation.ScheduleTranslationMakerCheckers.Add(scheduleTranslationMakerChecker);

                context.ScheduleTranslations.Attach(scheduleTranslation);
                context.Entry(scheduleTranslation).State = EntityState.Added;

                //Get ScheduleFrequency From Session Object New Added Record / Updated Record
                List<ScheduleFrequencyViewModel> ScheduleFrequencyViewModelList = (List<ScheduleFrequencyViewModel>)HttpContext.Current.Session["ScheduleFrequency"];

                foreach (ScheduleFrequencyViewModel viewModel in ScheduleFrequencyViewModelList)
                {
                    // Set Deafult Value
                    viewModel.SchedulePrmKey = _scheduleViewModel.SchedulePrmKey;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.Remark = "None";

                    // Get Prmkey By Id
                    viewModel.ScheduleTypePrmKey = managementDetailRepository.GetScheduleTypePrmKeyById(viewModel.ScheduleTypeId);
                    viewModel.DaysOfWeekPrmKey = managementDetailRepository.GetDaysOfWeekPrmKeyById(viewModel.DaysOfWeekId);
                    viewModel.DaysOfMonthPrmKey = managementDetailRepository.GetDaysOfMonthPrmKeyById(viewModel.DaysOfMonthId);

                    ScheduleFrequency scheduleFrequency = Mapper.Map<ScheduleFrequency>(viewModel);
                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                    context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
                    scheduleFrequency.ScheduleFrequencyMakerCheckers.Add(scheduleFrequencyMakerChecker);

                    context.ScheduleFrequencies.Attach(scheduleFrequency);
                    context.Entry(scheduleFrequency).State = EntityState.Added;
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

        public async Task<bool> Reject(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.UserAction = StringLiteralValue.Reject;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                ScheduleMakerChecker scheduleMakerChecker = Mapper.Map<ScheduleMakerChecker>(_scheduleViewModel);
                ScheduleModificationMakerChecker scheduleModificationMakerChecker = Mapper.Map<ScheduleModificationMakerChecker>(_scheduleViewModel);
                ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_scheduleViewModel.ScheduleModificationPrmKey == 0)
                {
                    // ScheduleMakerChecker
                    context.ScheduleMakerCheckers.Attach(scheduleMakerChecker);
                    context.Entry(scheduleMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // ScheduleModificationMakerChecker
                    context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerChecker);
                    context.Entry(scheduleModificationMakerChecker).State = EntityState.Added;
                }

                // ScheduleTranslationMakerChecker
                context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;

                //ScheduleFrequency
                List<ScheduleFrequencyViewModel> scheduleFrequencyViewModelList = new List<ScheduleFrequencyViewModel>();
                scheduleFrequencyViewModelList = (List<ScheduleFrequencyViewModel>)HttpContext.Current.Session["ScheduleFrequency"];

                foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _scheduleViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                    context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
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

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.ActivationStatus = StringLiteralValue.Active;
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.EntryStatus = StringLiteralValue.Create;
                _scheduleViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _scheduleViewModel.ReasonForModification = "None";
                _scheduleViewModel.Remark = "None";
                _scheduleViewModel.TransReasonForModification = "None";
                _scheduleViewModel.UserAction = StringLiteralValue.Create;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Schedule
                Schedule schedule = Mapper.Map<Schedule>(_scheduleViewModel);
                ScheduleMakerChecker scheduleMakerChecker = Mapper.Map<ScheduleMakerChecker>(_scheduleViewModel);

                // ScheduleTranslation
                ScheduleTranslation scheduleTranslation = Mapper.Map<ScheduleTranslation>(_scheduleViewModel);
                ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                // ScheduleMakerChecker
                context.ScheduleMakerCheckers.Attach(scheduleMakerChecker);
                context.Entry(scheduleMakerChecker).State = EntityState.Added;
                schedule.ScheduleMakerCheckers.Add(scheduleMakerChecker);

                context.Schedules.Attach(schedule);
                context.Entry(schedule).State = EntityState.Added;

                // ScheduleTranslationMakerChecker
                context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;
                scheduleTranslation.ScheduleTranslationMakerCheckers.Add(scheduleTranslationMakerChecker);

                context.ScheduleTranslations.Attach(scheduleTranslation);
                context.Entry(scheduleTranslation).State = EntityState.Added;
                schedule.ScheduleTranslations.Add(scheduleTranslation);

                //Get ScheduleFrequency From Session Object New Added Record / Updated Record
                List<ScheduleFrequencyViewModel> ScheduleFrequencyViewModelList = (List<ScheduleFrequencyViewModel>)HttpContext.Current.Session["ScheduleFrequency"];

                foreach (ScheduleFrequencyViewModel viewModel in ScheduleFrequencyViewModelList)
                {
                    // Set Deafult Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.Remark = "None";

                    // Get Prmkey By Id
                    viewModel.ScheduleTypePrmKey = managementDetailRepository.GetScheduleTypePrmKeyById(viewModel.ScheduleTypeId);
                    viewModel.DaysOfWeekPrmKey = managementDetailRepository.GetDaysOfWeekPrmKeyById(viewModel.DaysOfWeekId);
                    viewModel.DaysOfMonthPrmKey = managementDetailRepository.GetDaysOfMonthPrmKeyById(viewModel.DaysOfMonthId);

                    //Mapping
                    ScheduleFrequency scheduleFrequency = Mapper.Map<ScheduleFrequency>(viewModel);
                    ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);
                    
                    //ScheduleFrequency
                    context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                    context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
                    scheduleFrequency.ScheduleFrequencyMakerCheckers.Add(scheduleFrequencyMakerChecker);

                    context.ScheduleFrequencies.Attach(scheduleFrequency);
                    context.Entry(scheduleFrequency).State = EntityState.Added;
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

        public async Task<bool> Verify(ScheduleViewModel _scheduleViewModel)
        {
            try
            {
                // Set Default Value
                _scheduleViewModel.EntryDateTime = DateTime.Now;
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_scheduleViewModel.ScheduleModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    ScheduleViewModel scheduleViewModelForModify = await GetVerifiedEntry(_scheduleViewModel.ScheduleId);

                    // Set Default Value
                    scheduleViewModelForModify.UserAction = StringLiteralValue.Modify;

                    // ScheduleTranslation
                    ScheduleTranslationMakerChecker scheduleTranslationMakerCheckerForModify = Mapper.Map<ScheduleTranslationMakerChecker>(scheduleViewModelForModify);

                    context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerCheckerForModify);
                    context.Entry(scheduleTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (scheduleViewModelForModify.IsModified == true)
                    {
                        ScheduleModificationMakerChecker scheduleModificationMakerCheckerForModify = Mapper.Map<ScheduleModificationMakerChecker>(scheduleViewModelForModify);

                        context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerCheckerForModify);
                        context.Entry(scheduleModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Modify Old ScheduleFrequency
                    IEnumerable<ScheduleFrequencyViewModel> scheduleFrequencyViewModelListForModify = await scheduleFrequencyRepository.GetVerifiedEntries(_scheduleViewModel.SchedulePrmKey);

                    foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        ScheduleFrequencyMakerChecker scheduleFrequencyMakerCheckerForModify = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                        context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerCheckerForModify);
                        context.Entry(scheduleFrequencyMakerCheckerForModify).State = EntityState.Added;
                    }
                    

                    // Verify New Record
                    // Set Default Value
                    _scheduleViewModel.UserAction = StringLiteralValue.Verify;

                    ScheduleModificationMakerChecker scheduleModificationMakerChecker = Mapper.Map<ScheduleModificationMakerChecker>(_scheduleViewModel);
                    ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                    // ScheduleModificationMakerChecker
                    context.ScheduleModificationMakerCheckers.Attach(scheduleModificationMakerChecker);
                    context.Entry(scheduleModificationMakerChecker).State = EntityState.Added;

                    // ScheduleTranslationMakerChecker
                    context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                    context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;


                    // ScheduleFrequencyViewModel
                    IEnumerable<ScheduleFrequencyViewModel> scheduleFrequencyViewModelList = await scheduleFrequencyRepository.GetUnverifiedEntries(_scheduleViewModel.SchedulePrmKey);

                    foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);

                        context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                        context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
                    }
                }
                else
                {
                    _scheduleViewModel.UserAction = StringLiteralValue.Verify;

                    ScheduleMakerChecker scheduleMakerChecker = Mapper.Map<ScheduleMakerChecker>(_scheduleViewModel);
                    ScheduleTranslationMakerChecker scheduleTranslationMakerChecker = Mapper.Map<ScheduleTranslationMakerChecker>(_scheduleViewModel);

                    // ScheduleMakerChecker
                    context.ScheduleMakerCheckers.Attach(scheduleMakerChecker);
                    context.Entry(scheduleMakerChecker).State = EntityState.Added;

                    // ScheduleTranslationMakerChecker
                    context.ScheduleTranslationMakerCheckers.Attach(scheduleTranslationMakerChecker);
                    context.Entry(scheduleTranslationMakerChecker).State = EntityState.Added;
                    
                    // Verify ScheduleFrequency
                    IEnumerable<ScheduleFrequencyViewModel> scheduleFrequencyViewModelList = await scheduleFrequencyRepository.GetUnverifiedEntries(_scheduleViewModel.SchedulePrmKey);

                    foreach (ScheduleFrequencyViewModel viewModel in scheduleFrequencyViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _scheduleViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        // Get Prmkey By Id
                        viewModel.ScheduleTypePrmKey = managementDetailRepository.GetScheduleTypePrmKeyById(viewModel.ScheduleTypeId);
                        viewModel.DaysOfWeekPrmKey = managementDetailRepository.GetDaysOfWeekPrmKeyById(viewModel.DaysOfWeekId);
                        viewModel.DaysOfMonthPrmKey = managementDetailRepository.GetDaysOfMonthPrmKeyById(viewModel.DaysOfMonthId);
                        
                        //Mapping
                        ScheduleFrequencyMakerChecker scheduleFrequencyMakerChecker = Mapper.Map<ScheduleFrequencyMakerChecker>(viewModel);
                        
                        //ScheduleFrequency
                        context.ScheduleFrequencyMakerCheckers.Attach(scheduleFrequencyMakerChecker);
                        context.Entry(scheduleFrequencyMakerChecker).State = EntityState.Added;
                    }
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
