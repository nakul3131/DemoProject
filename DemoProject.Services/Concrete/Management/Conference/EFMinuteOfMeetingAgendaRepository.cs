using AutoMapper;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Domain.Entities.Management.Conference;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management;

namespace DemoProject.Services.Concrete.Management.Conference
{
    public class EFMinuteOfMeetingAgendaRepository : IMinuteOfMeetingAgendaRepository
    {
        private readonly EFDbContext context;

        private readonly IMinuteOfMeetingAgendaSpokespersonRepository minuteOfMeetingAgendaSpokespersonRepository; 
        private readonly IManagementDetailRepository managementDetailRepository;

        public EFMinuteOfMeetingAgendaRepository(RepositoryConnection _connection, IMinuteOfMeetingAgendaSpokespersonRepository _minuteOfMeetingAgendaSpokespersonRepository, IManagementDetailRepository _managementDetailRepository)
        { 
            context = _connection.EFDbContext;
            minuteOfMeetingAgendaSpokespersonRepository = _minuteOfMeetingAgendaSpokespersonRepository;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryStatus = StringLiteralValue.Amend;
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.EntryStatus = StringLiteralValue.Amend;
                _minuteOfMeetingAgendaViewModel.ReasonForModification = "None";
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Amend;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                
                // Mapping 
                // MinuteOfMeetingAgenda
                MinuteOfMeetingAgenda minuteOfMeetingAgendaForAmend = Mapper.Map<MinuteOfMeetingAgenda>(_minuteOfMeetingAgendaViewModel);
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerCheckerForAmend = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                minuteOfMeetingAgendaForAmend.PrmKey = _minuteOfMeetingAgendaViewModel.MinuteOfMeetingAgendaPrmKey;
                
                // Save Data In Appropriate Tables By Entity Framework Methods            

                // MinuteOfMeetingAgendaMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerCheckerForAmend);
                context.Entry(minuteOfMeetingAgendaMakerCheckerForAmend).State = EntityState.Added;
                minuteOfMeetingAgendaForAmend.MinuteOfMeetingAgendaMakerCheckers.Add(minuteOfMeetingAgendaMakerCheckerForAmend);

                context.MinuteOfMeetingAgendas.Attach(minuteOfMeetingAgendaForAmend);
                context.Entry(minuteOfMeetingAgendaForAmend).State = EntityState.Modified;

                // MinuteOfMeetingAgendaSpokesperson - Amend Old Record
                IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelForAmend = await minuteOfMeetingAgendaSpokespersonRepository.GetRejectedEntries(_minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey);

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelForAmend)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // MinuteOfMeetingSpokespersonMakerChecker
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
                }

                // MinuteOfMeetingAgendaSpokesperson - Add New Amended Entry, Get MinuteOfMeetingAgenda Details From Session Object
                List<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = (List<MinuteOfMeetingAgendaSpokespersonViewModel>)HttpContext.Current.Session["MinuteOfMeetingAgendaSpokesperson"];

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.MeetingAgendaPrmKey = _minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey;
                    viewModel.MinuteOfMeetingAgendaSpokespersonPrmKey = 0;
                    viewModel.MinuteOfMeetingAgendaSpokespersonTranslationPrmKey = 0;
                    viewModel.Remark = _minuteOfMeetingAgendaViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(viewModel.BoardOfDirectorId);

                    // MinuteOfMeetingSpokesperson
                    MinuteOfMeetingAgendaSpokesperson minuteOfMeetingAgendaSpokesperson = Mapper.Map<MinuteOfMeetingAgendaSpokesperson>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslation
                    MinuteOfMeetingAgendaSpokespersonTranslation minuteOfMeetingAgendaSpokespersonTranslation = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslation>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokespersonTranslation.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespersonTranslations.Attach(minuteOfMeetingAgendaSpokespersonTranslation);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslation).State = EntityState.Added;

                    // MinuteOfMeetingSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokesperson.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespeople.Attach(minuteOfMeetingAgendaSpokesperson);
                    context.Entry(minuteOfMeetingAgendaSpokesperson).State = EntityState.Added;
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

        public async Task<bool> Delete(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Delete;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerChecker = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // MinuteOfMeetingAgendaMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerChecker);
                context.Entry(minuteOfMeetingAgendaMakerChecker).State = EntityState.Added;

                // MinuteOfMeetingAgendaSpokesperson
                IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = await minuteOfMeetingAgendaSpokespersonRepository.GetRejectedEntries(_minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey);

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _minuteOfMeetingAgendaViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    
                    // MinuteOfMeetingAgendaSpokesperson
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);
                    
                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;

                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Modify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaViewModel>> GetIndexOfCreate()
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //public int GetPrmKeyById(Guid _minuteOfMeetingId)
        //{
        //    var a= context.MeetingAgendas
        //            .Where(c => c.MeetingAgendaId == _minuteOfMeetingId)
        //            .Select(c => c.PrmKey).FirstOrDefault();
        //    return a;
        //}

        public async Task<MinuteOfMeetingAgendaViewModel> GetRejectedEntry(Guid _meetingAgendaId)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntry (@MeetingAgendaId, @EntriesType)", new SqlParameter("@MeetingAgendaId", _meetingAgendaId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MinuteOfMeetingAgendaViewModel> GetUnVerifiedEntry(Guid _meetingAgendaId)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntry (@MeetingAgendaId, @EntriesType)", new SqlParameter("@MeetingAgendaId", _meetingAgendaId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MinuteOfMeetingAgendaViewModel> GetVerifiedEntry(Guid _meetingAgendaId)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingAgendaEntry (@MeetingAgendaId, @EntriesType)", new SqlParameter("@MeetingAgendaId", _meetingAgendaId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.EntryStatus = StringLiteralValue.Create;
                _minuteOfMeetingAgendaViewModel.MinuteOfMeetingAgendaPrmKey = 0;
                _minuteOfMeetingAgendaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _minuteOfMeetingAgendaViewModel.Remark = "None";
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Create;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id

                // MinuteOfMeetingAgenda
                MinuteOfMeetingAgenda minuteOfMeetingAgenda = Mapper.Map<MinuteOfMeetingAgenda>(_minuteOfMeetingAgendaViewModel);
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerChecker = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // MinuteOfMeetingAgendaSpokesperson 
                List<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = new List<MinuteOfMeetingAgendaSpokespersonViewModel>();

                minuteOfMeetingAgendaSpokespersonViewModelList = (List<MinuteOfMeetingAgendaSpokespersonViewModel>)HttpContext.Current.Session["MinuteOfMeetingAgendaSpokesperson"];

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.MinuteOfMeetingAgendaSpokespersonTranslationPrmKey = 0;
                    viewModel.MeetingAgendaPrmKey = _minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(viewModel.BoardOfDirectorId);
                    
                    // MinuteOfMeetingSpokesperson
                    MinuteOfMeetingAgendaSpokesperson minuteOfMeetingAgendaSpokesperson = Mapper.Map<MinuteOfMeetingAgendaSpokesperson>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslation
                    MinuteOfMeetingAgendaSpokespersonTranslation minuteOfMeetingAgendaSpokespersonTranslation = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslation>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokespersonTranslation.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespersonTranslations.Attach(minuteOfMeetingAgendaSpokespersonTranslation);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslation).State = EntityState.Added;
                    
                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokesperson.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespeople.Attach(minuteOfMeetingAgendaSpokesperson);
                    context.Entry(minuteOfMeetingAgendaSpokesperson).State = EntityState.Added; 
                }

                // MinuteOfMeetingAgendaMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerChecker);
                context.Entry(minuteOfMeetingAgendaMakerChecker).State = EntityState.Added;
                minuteOfMeetingAgenda.MinuteOfMeetingAgendaMakerCheckers.Add(minuteOfMeetingAgendaMakerChecker);

                context.MinuteOfMeetingAgendas.Attach(minuteOfMeetingAgenda);
                context.Entry(minuteOfMeetingAgenda).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            } 
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Reject;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // MinuteOfMeetingAgendaMakerChecker
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerChecker = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // MinuteOfMeetingAgendaMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerChecker);
                context.Entry(minuteOfMeetingAgendaMakerChecker).State = EntityState.Added;

                // MinuteOfMeetingAgendaSpokesperson
                IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = await minuteOfMeetingAgendaSpokespersonRepository.GetUnverifiedEntries(_minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey);

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _minuteOfMeetingAgendaViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    
                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;

                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;
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
        public async Task<bool> Save(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.EntryStatus = StringLiteralValue.Create;
                _minuteOfMeetingAgendaViewModel.ReasonForModification = "None";
                _minuteOfMeetingAgendaViewModel.Remark = "None";
                _minuteOfMeetingAgendaViewModel.Note = "None";
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Create;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey = _minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey;

                // Get PrmKey By Id Of All Dropdowns

                // MinuteOfMeetingAgenda
                MinuteOfMeetingAgenda minuteOfMeetingAgenda = Mapper.Map<MinuteOfMeetingAgenda>(_minuteOfMeetingAgendaViewModel);
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerChecker = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // MinuteOfMeetingAgendaSpokesperson
                List<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = new List<MinuteOfMeetingAgendaSpokespersonViewModel>();

                minuteOfMeetingAgendaSpokespersonViewModelList = (List<MinuteOfMeetingAgendaSpokespersonViewModel>)HttpContext.Current.Session["MinuteOfMeetingAgendaSpokesperson"];

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingAgendaPrmKey = _minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = managementDetailRepository.GetBoardOfDirectorPrmKeyById(viewModel.BoardOfDirectorId);

                    // MinuteOfMeetingAgendaSpokesperson
                    MinuteOfMeetingAgendaSpokesperson minuteOfMeetingAgendaSpokesperson = Mapper.Map<MinuteOfMeetingAgendaSpokesperson>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslation
                    MinuteOfMeetingAgendaSpokespersonTranslation minuteOfMeetingAgendaSpokespersonTranslation = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslation>(viewModel);
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokespersonTranslation.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespersonTranslations.Attach(minuteOfMeetingAgendaSpokespersonTranslation);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslation).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokesperson.MinuteOfMeetingAgendaSpokespersonTranslations.Add(minuteOfMeetingAgendaSpokespersonTranslation);

                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;
                    minuteOfMeetingAgendaSpokesperson.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Add(minuteOfMeetingAgendaSpokespersonMakerChecker);

                    context.MinuteOfMeetingAgendaSpokespeople.Attach(minuteOfMeetingAgendaSpokesperson);
                    context.Entry(minuteOfMeetingAgendaSpokesperson).State = EntityState.Added;
                }

                // MinuteOfMeetingAgendaMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerChecker);
                context.Entry(minuteOfMeetingAgendaMakerChecker).State = EntityState.Added;
                minuteOfMeetingAgenda.MinuteOfMeetingAgendaMakerCheckers.Add(minuteOfMeetingAgendaMakerChecker);

                context.MinuteOfMeetingAgendas.Attach(minuteOfMeetingAgenda);
                context.Entry(minuteOfMeetingAgenda).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            try
            {
                // Modify Old Record      
                MinuteOfMeetingAgendaViewModel minuteOfMeetingAgendaViewModelOfOldEntry = await GetVerifiedEntry(_minuteOfMeetingAgendaViewModel.MeetingAgendaId);

                if (minuteOfMeetingAgendaViewModelOfOldEntry != null)
                {
                    if (minuteOfMeetingAgendaViewModelOfOldEntry.PrmKey > 0)
                    {
                        // Set Default Value
                        minuteOfMeetingAgendaViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                        minuteOfMeetingAgendaViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                        minuteOfMeetingAgendaViewModelOfOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        // Mapping
                        MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerCheckerForModify = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(minuteOfMeetingAgendaViewModelOfOldEntry);

                        //MinuteOfMeetingAgendaMakerChecker
                        context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerCheckerForModify);
                        context.Entry(minuteOfMeetingAgendaMakerCheckerForModify).State = EntityState.Added;

                        // Modify (i.e. Old Verified Entries)
                        // MinuteOfMeetingSpokesperson
                        IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingSpokespersonViewModelListModify = await minuteOfMeetingAgendaSpokespersonRepository.GetVerifiedEntries(_minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey);

                        foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingSpokespersonViewModelListModify)
                        {
                            viewModel.EntryDateTime = DateTime.Now;
                            viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                            viewModel.UserAction = StringLiteralValue.Modify;
                            viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                            
                            // MinuteOfMeetingSpokespersonMakerChecker
                            MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                            // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                            MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                            // MinuteOfMeetingAgendaSpokespersonMakerChecker
                            context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                            context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;

                            // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                            context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                            context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
                        }
                    }
                }

                // Verify Record
                //Set Default Value
                _minuteOfMeetingAgendaViewModel.EntryDateTime = DateTime.Now;
                _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Verify;
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                // MinuteOfMeetingAgendaMakerChecker
                MinuteOfMeetingAgendaMakerChecker minuteOfMeetingAgendaMakerChecker = Mapper.Map<MinuteOfMeetingAgendaMakerChecker>(_minuteOfMeetingAgendaViewModel);

                // MinuteOfMeetingMakerChecker
                context.MinuteOfMeetingAgendaMakerCheckers.Attach(minuteOfMeetingAgendaMakerChecker);
                context.Entry(minuteOfMeetingAgendaMakerChecker).State = EntityState.Added;

                // MinuteOfMeetingSpokesperson
                IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel> minuteOfMeetingAgendaSpokespersonViewModelList = await minuteOfMeetingAgendaSpokespersonRepository.GetUnverifiedEntries(_minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey);

                foreach (MinuteOfMeetingAgendaSpokespersonViewModel viewModel in minuteOfMeetingAgendaSpokespersonViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Remark = _minuteOfMeetingAgendaViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // MinuteOfMeetingSpokespersonMakerChecker
                    MinuteOfMeetingAgendaSpokespersonMakerChecker minuteOfMeetingAgendaSpokespersonMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker minuteOfMeetingAgendaSpokespersonTranslationMakerChecker = Mapper.Map<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>(viewModel);

                    // MinuteOfMeetingAgendaSpokespersonMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonMakerChecker).State = EntityState.Added;

                    // MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker
                    context.MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers.Attach(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker);
                    context.Entry(minuteOfMeetingAgendaSpokespersonTranslationMakerChecker).State = EntityState.Added;
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
