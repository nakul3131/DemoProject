using AutoMapper;
using DemoProject.Domain.Entities.Management.Conference;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Management.Conference
{
    public class EFMeetingAllowanceRepository : IMeetingAllowanceRepository
    {
        private readonly EFDbContext context;
        private readonly IManagementDetailRepository managementDetailRepository;

        public EFMeetingAllowanceRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.ActivationStatus = StringLiteralValue.Active;
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.EntryStatus = StringLiteralValue.Amend;
                _meetingAllowanceViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingAllowanceViewModel.UserAction = StringLiteralValue.Amend;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _meetingAllowanceViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingAllowanceViewModel.MeetingTypeId);

                // Mapping 
                // MeetingAllowance 
                MeetingAllowance meetingAllowanceForAmend = Mapper.Map<MeetingAllowance>(_meetingAllowanceViewModel);
                MeetingAllowanceMakerChecker meetingAllowanceMakerCheckerForAmend = Mapper.Map<MeetingAllowanceMakerChecker>(_meetingAllowanceViewModel);

                // MeetingAllowanceModification 
                MeetingAllowanceModification meetingAllowanceModificationForAmend = Mapper.Map<MeetingAllowanceModification>(_meetingAllowanceViewModel);
                MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerCheckerForAmend = Mapper.Map<MeetingAllowanceModificationMakerChecker>(_meetingAllowanceViewModel);

                MeetingAllowanceTranslation meetingAllowanceTranslationForAmend = Mapper.Map<MeetingAllowanceTranslation>(_meetingAllowanceViewModel);
                MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerCheckerForAmend = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                meetingAllowanceForAmend.PrmKey = _meetingAllowanceViewModel.MeetingAllowancePrmKey;
                meetingAllowanceModificationForAmend.PrmKey = _meetingAllowanceViewModel.MeetingAllowanceModificationPrmKey;
                meetingAllowanceTranslationForAmend.PrmKey = _meetingAllowanceViewModel.MeetingAllowanceTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_meetingAllowanceViewModel.MeetingAllowanceModificationPrmKey == 0)
                {
                    // MeetingAllowance
                    context.MeetingAllowanceMakerCheckers.Attach(meetingAllowanceMakerCheckerForAmend);
                    context.Entry(meetingAllowanceMakerCheckerForAmend).State = EntityState.Added;
                    meetingAllowanceForAmend.MeetingAllowanceMakerCheckers.Add(meetingAllowanceMakerCheckerForAmend);

                    context.MeetingAllowances.Attach(meetingAllowanceForAmend);
                    context.Entry(meetingAllowanceForAmend).State = EntityState.Modified;
                }
                else
                {
                    // MeetingAllowance Modification 
                    context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerCheckerForAmend);
                    context.Entry(meetingAllowanceModificationMakerCheckerForAmend).State = EntityState.Added;
                    meetingAllowanceModificationForAmend.MeetingAllowanceModificationMakerCheckers.Add(meetingAllowanceModificationMakerCheckerForAmend);

                    context.MeetingAllowanceModifications.Attach(meetingAllowanceModificationForAmend);
                    context.Entry(meetingAllowanceModificationForAmend).State = EntityState.Modified;
                }

                // MeetingAllowanceTranslation
                context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerCheckerForAmend);
                context.Entry(meetingAllowanceTranslationMakerCheckerForAmend).State = EntityState.Added;
                meetingAllowanceTranslationForAmend.MeetingAllowanceTranslationMakerCheckers.Add(meetingAllowanceTranslationMakerCheckerForAmend);

                context.MeetingAllowanceTranslations.Attach(meetingAllowanceTranslationForAmend);
                context.Entry(meetingAllowanceTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.ReasonForModification = "None";
                _meetingAllowanceViewModel.UserAction = StringLiteralValue.Delete;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                MeetingAllowanceMakerChecker meetingAllowanceMakerChecker = Mapper.Map<MeetingAllowanceMakerChecker>(_meetingAllowanceViewModel);
                MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerChecker = Mapper.Map<MeetingAllowanceModificationMakerChecker>(_meetingAllowanceViewModel);
                MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                if (_meetingAllowanceViewModel.MeetingAllowanceModificationPrmKey == 0)
                {
                    // MeetingAllowance
                    context.MeetingAllowanceMakerCheckers.Attach(meetingAllowanceMakerChecker);
                    context.Entry(meetingAllowanceMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // MeetingAllowanceModification  
                    context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerChecker);
                    context.Entry(meetingAllowanceModificationMakerChecker).State = EntityState.Added;
                }

                // MeetingAllowanceTranslation
                context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingAllowanceViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MeetingAllowanceViewModel> GetRejectedEntry(Guid _meetingAllowanceId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntry (@MeetingAllowanceId, @EntriesType)", new SqlParameter("@MeetingAllowanceId", _meetingAllowanceId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MeetingAllowanceViewModel> GetUnVerifiedEntry(Guid _meetingAllowanceId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntry (@MeetingAllowanceId, @EntriesType)", new SqlParameter("@MeetingAllowanceId", _meetingAllowanceId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MeetingAllowanceViewModel> GetVerifiedEntry(Guid _meetingAllowanceId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAllowanceViewModel>("SELECT * FROM dbo.GetMeetingAllowanceEntry (@MeetingAllowanceId, @EntriesType)", new SqlParameter("@MeetingAllowanceId", _meetingAllowanceId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.MeetingAllowanceTranslationPrmKey = 0;
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.EntryStatus = StringLiteralValue.Create;
                _meetingAllowanceViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingAllowanceViewModel.Remark = "None";
                _meetingAllowanceViewModel.UserAction = StringLiteralValue.Create;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _meetingAllowanceViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingAllowanceViewModel.MeetingTypeId);

                // Mapping
                // MeetingAllowanceModification
                MeetingAllowanceModification meetingAllowanceModification = Mapper.Map<MeetingAllowanceModification>(_meetingAllowanceViewModel);
                MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerChecker = Mapper.Map<MeetingAllowanceModificationMakerChecker>(_meetingAllowanceViewModel);

                // MeetingAllowanceTranslation
                MeetingAllowanceTranslation meetingAllowanceTranslation = Mapper.Map<MeetingAllowanceTranslation>(_meetingAllowanceViewModel);
                MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                // MeetingAllowanceModification
                context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerChecker);
                context.Entry(meetingAllowanceModificationMakerChecker).State = EntityState.Added;
                meetingAllowanceModification.MeetingAllowanceModificationMakerCheckers.Add(meetingAllowanceModificationMakerChecker);

                context.MeetingAllowanceModifications.Attach(meetingAllowanceModification);
                context.Entry(meetingAllowanceModification).State = EntityState.Added;

                // MeetingAllowanceTranslation
                context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;
                meetingAllowanceTranslation.MeetingAllowanceTranslationMakerCheckers.Add(meetingAllowanceTranslationMakerChecker);

                context.MeetingAllowanceTranslations.Attach(meetingAllowanceTranslation);
                context.Entry(meetingAllowanceTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.UserAction = StringLiteralValue.Reject;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                MeetingAllowanceMakerChecker meetingAllowanceMakerChecker = Mapper.Map<MeetingAllowanceMakerChecker>(_meetingAllowanceViewModel);
                MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerChecker = Mapper.Map<MeetingAllowanceModificationMakerChecker>(_meetingAllowanceViewModel);
                MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_meetingAllowanceViewModel.MeetingAllowanceModificationPrmKey == 0)
                {
                    // MeetingAllowanceMakerChecker
                    context.MeetingAllowanceMakerCheckers.Attach(meetingAllowanceMakerChecker);
                    context.Entry(meetingAllowanceMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // MeetingAllowanceModificationMakerChecker
                    context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerChecker);
                    context.Entry(meetingAllowanceModificationMakerChecker).State = EntityState.Added;
                }

                // MeetingAllowanceTranslationMakerChecker
                context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.ActivationStatus = StringLiteralValue.Active;
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.EntryStatus = StringLiteralValue.Create;
                _meetingAllowanceViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingAllowanceViewModel.ReasonForModification = "None";
                _meetingAllowanceViewModel.Remark = "None";
                _meetingAllowanceViewModel.TransReasonForModification = "None";
                _meetingAllowanceViewModel.UserAction = StringLiteralValue.Create;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _meetingAllowanceViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingAllowanceViewModel.MeetingTypeId);

                // Mapping
                // MeetingAllowance
                MeetingAllowance meetingAllowance = Mapper.Map<MeetingAllowance>(_meetingAllowanceViewModel);
                MeetingAllowanceMakerChecker meetingAllowanceMakerChecker = Mapper.Map<MeetingAllowanceMakerChecker>(_meetingAllowanceViewModel);

                // MeetingAllowanceTranslation
                MeetingAllowanceTranslation meetingAllowanceTranslation = Mapper.Map<MeetingAllowanceTranslation>(_meetingAllowanceViewModel);
                MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                // MeetingAllowanceMakerChecker
                context.MeetingAllowanceMakerCheckers.Attach(meetingAllowanceMakerChecker);
                context.Entry(meetingAllowanceMakerChecker).State = EntityState.Added;
                meetingAllowance.MeetingAllowanceMakerCheckers.Add(meetingAllowanceMakerChecker);

                context.MeetingAllowances.Attach(meetingAllowance);
                context.Entry(meetingAllowance).State = EntityState.Added;

                // MeetingAllowanceTranslationMakerChecker
                context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;
                meetingAllowanceTranslation.MeetingAllowanceTranslationMakerCheckers.Add(meetingAllowanceTranslationMakerChecker);

                context.MeetingAllowanceTranslations.Attach(meetingAllowanceTranslation);
                context.Entry(meetingAllowanceTranslation).State = EntityState.Added;
                meetingAllowance.MeetingAllowanceTranslations.Add(meetingAllowanceTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            try
            {
                // Set Default Value
                _meetingAllowanceViewModel.EntryDateTime = DateTime.Now;
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_meetingAllowanceViewModel.MeetingAllowanceModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    MeetingAllowanceViewModel meetingAllowanceViewModelForModify = await GetVerifiedEntry(_meetingAllowanceViewModel.MeetingAllowanceId);

                    // Set Default Value
                    meetingAllowanceViewModelForModify.UserAction = StringLiteralValue.Modify;
                    meetingAllowanceViewModelForModify.UserProfilePrmKey = _meetingAllowanceViewModel.UserProfilePrmKey;

                    // MeetingAllowanceTranslation
                    MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerCheckerForModify = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(meetingAllowanceViewModelForModify);

                    context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerCheckerForModify);
                    context.Entry(meetingAllowanceTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (meetingAllowanceViewModelForModify.IsModified == true)
                    {
                        MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerCheckerForModify = Mapper.Map<MeetingAllowanceModificationMakerChecker>(meetingAllowanceViewModelForModify);

                        context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerCheckerForModify);
                        context.Entry(meetingAllowanceModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _meetingAllowanceViewModel.UserAction = StringLiteralValue.Verify;

                    MeetingAllowanceModificationMakerChecker meetingAllowanceModificationMakerChecker = Mapper.Map<MeetingAllowanceModificationMakerChecker>(_meetingAllowanceViewModel);
                    MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                    // MeetingAllowanceModificationMakerChecker
                    context.MeetingAllowanceModificationMakerCheckers.Attach(meetingAllowanceModificationMakerChecker);
                    context.Entry(meetingAllowanceModificationMakerChecker).State = EntityState.Added;

                    // MeetingAllowanceTranslationMakerChecker
                    context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                    context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _meetingAllowanceViewModel.UserAction = StringLiteralValue.Verify;

                    MeetingAllowanceMakerChecker meetingAllowanceMakerChecker = Mapper.Map<MeetingAllowanceMakerChecker>(_meetingAllowanceViewModel);
                    MeetingAllowanceTranslationMakerChecker meetingAllowanceTranslationMakerChecker = Mapper.Map<MeetingAllowanceTranslationMakerChecker>(_meetingAllowanceViewModel);

                    // MeetingAllowanceMakerChecker
                    context.MeetingAllowanceMakerCheckers.Attach(meetingAllowanceMakerChecker);
                    context.Entry(meetingAllowanceMakerChecker).State = EntityState.Added;

                    // MeetingAllowanceTranslationMakerChecker
                    context.MeetingAllowanceTranslationMakerCheckers.Attach(meetingAllowanceTranslationMakerChecker);
                    context.Entry(meetingAllowanceTranslationMakerChecker).State = EntityState.Added;
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
