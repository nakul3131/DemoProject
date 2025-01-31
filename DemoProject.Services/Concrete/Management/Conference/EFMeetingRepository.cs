using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Domain.Entities.Management.Conference;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.Services.Concrete.Management.Conference
{
    public class EFMeetingRepository : IMeetingRepository
    {
        private readonly EFDbContext context;

        //private readonly IMeetingRepository meetingRepository;
        private readonly IMeetingAgendaRepository meetingAgendaRepository;
        private readonly IMeetingInviteeBoardOfDirectorRepository meetingInviteeBoardOfDirectorRepository;
        private readonly IMeetingInviteeMemberRepository meetingInviteeMemberRepository;
        private readonly IMeetingNoticeRepository meetingNoticeRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IAgendaRepository agendaRepository;
        private readonly IBoardOfDirectorRepository boardOfDirectorRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ISharesCapitalCustomerAccountRepository sharesCapitalCustomerAccountRepository;
        public EFMeetingRepository(RepositoryConnection _connection, IMeetingAgendaRepository _meetingAgendaRepository, IMeetingInviteeBoardOfDirectorRepository _meetingInviteeBoardOfDirectorRepository,
                                   IMeetingInviteeMemberRepository _meetingInviteeMemberRepository, IMeetingNoticeRepository _meetingNoticeRepository, IManagementDetailRepository _managementDetailRepository,
                                   IAgendaRepository _agendaRepository, IBoardOfDirectorRepository _boardOfDirectorRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ISharesCapitalCustomerAccountRepository _sharesCapitalCustomerAccountRepository)
        {
            context = _connection.EFDbContext;
            meetingAgendaRepository = _meetingAgendaRepository;
            meetingInviteeBoardOfDirectorRepository = _meetingInviteeBoardOfDirectorRepository;
            meetingInviteeMemberRepository = _meetingInviteeMemberRepository;
            meetingNoticeRepository = _meetingNoticeRepository;
            managementDetailRepository = _managementDetailRepository;
            agendaRepository = _agendaRepository;
            boardOfDirectorRepository = _boardOfDirectorRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            sharesCapitalCustomerAccountRepository = _sharesCapitalCustomerAccountRepository;
        }

        public async Task<bool> Amend(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.EntryStatus = StringLiteralValue.Amend;
                _meetingViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingViewModel.ReasonForModification = "None";
                _meetingViewModel.TransReasonForModification = "None";
                _meetingViewModel.UserAction = StringLiteralValue.Amend;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _meetingViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingViewModel.MeetingTypeId);

                // Mapping 
                // Meeting 
                Meeting meeting = Mapper.Map<Meeting>(_meetingViewModel);
                MeetingMakerChecker meetingMakerChecker = Mapper.Map<MeetingMakerChecker>(_meetingViewModel);

                // MeetingModification
                MeetingModification meetingModification = Mapper.Map<MeetingModification>(_meetingViewModel);
                MeetingModificationMakerChecker meetingModificationMakerChecker = Mapper.Map<MeetingModificationMakerChecker>(_meetingViewModel);

                // MeetingTranslation
                MeetingTranslation meetingTranslation = Mapper.Map<MeetingTranslation>(_meetingViewModel);
                MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                // Set ReferenceKey As PrmKey To Every Object 
                meeting.PrmKey = _meetingViewModel.MeetingPrmKey;
                meetingModification.PrmKey = _meetingViewModel.MeetingModificationPrmKey;
                meetingTranslation.PrmKey = _meetingViewModel.MeetingTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table

                if (_meetingViewModel.MeetingModificationPrmKey == 0)
                {
                    // Meeting
                    context.MeetingMakerCheckers.Attach(meetingMakerChecker);
                    context.Entry(meetingMakerChecker).State = EntityState.Added;
                    meeting.MeetingMakerCheckers.Add(meetingMakerChecker);

                    context.Meetings.Attach(meeting);
                    context.Entry(meeting).State = EntityState.Modified;

                }
                else
                {
                    // MeetingModification
                    context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerChecker);
                    context.Entry(meetingModificationMakerChecker).State = EntityState.Added;
                    meetingModification.MeetingModificationMakerCheckers.Add(meetingModificationMakerChecker);

                    context.MeetingModifications.Attach(meetingModification);
                    context.Entry(meetingModification).State = EntityState.Modified;
                }

                // MeetingTranslation
                context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;
                meetingTranslation.MeetingTranslationMakerCheckers.Add(meetingTranslationMakerChecker);

                context.MeetingTranslations.Attach(meetingTranslation);
                context.Entry(meetingTranslation).State = EntityState.Modified;

                // MeetingAgenda - Amend Old Record
                IEnumerable<MeetingAgendaViewModel> meetingAgendaViewModelForAmend = await meetingAgendaRepository.GetRejectedEntries(_meetingViewModel.MeetingPrmKey);

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelForAmend)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingAgendaMakerChecker meetingAgendaMakerCheckerForAmend = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerCheckerForAmend);
                    context.Entry(meetingAgendaMakerCheckerForAmend).State = EntityState.Added;
                }

                // MeetingAgenda - Add New Amended Entry, Get MeetingAgenda Details From Session Object
                List<MeetingAgendaViewModel> meetingAgendaViewModelList = new List<MeetingAgendaViewModel>();

                meetingAgendaViewModelList = (List<MeetingAgendaViewModel>)HttpContext.Current.Session["MeetingAgenda"];

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.MeetingAgendaPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.AgendaPrmKey = agendaRepository.GetPrmKeyById(viewModel.AgendaId);

                    // MeetingAgenda
                    MeetingAgenda meetingAgenda = Mapper.Map<MeetingAgenda>(viewModel);
                    MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                    context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                    meetingAgenda.MeetingAgendaMakerCheckers.Add(meetingAgendaMakerChecker);

                    context.MeetingAgendas.Attach(meetingAgenda);
                    context.Entry(meetingAgenda).State = EntityState.Added;
                }

                // MeetingInviteeBoardOfDirector - Amend Old Record
                IEnumerable<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelForAmend = await meetingInviteeBoardOfDirectorRepository.GetRejectedEntries(_meetingViewModel.MeetingPrmKey);

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelForAmend)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerCheckerForAmend = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerCheckerForAmend);
                    context.Entry(meetingInviteeBoardOfDirectorMakerCheckerForAmend).State = EntityState.Added;
                }

                // MeetingInviteeBoardOfDirector - Add New Amended Entry, Get MeetingInviteeBoardOfDirector Details From Session Object
                List<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = new List<MeetingInviteeBoardOfDirectorViewModel>();

                meetingInviteeBoardOfDirectorViewModelList = (List<MeetingInviteeBoardOfDirectorViewModel>)HttpContext.Current.Session["MeetingInviteeBoardOfDirector"];

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.MeetingInviteeBoardOfDirectorPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = 3;
                    //viewModel.BoardOfDirectorPrmKey = boardOfDirectorRepository.GetPrmKeyById(viewModel.BoardOfDirectorId);

                    // MeetingInviteeBoardOfDirector
                    MeetingInviteeBoardOfDirector meetingInviteeBoardOfDirector = Mapper.Map<MeetingInviteeBoardOfDirector>(viewModel);
                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                    context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                    meetingInviteeBoardOfDirector.MeetingInviteeBoardOfDirectorMakerCheckers.Add(meetingInviteeBoardOfDirectorMakerChecker);

                    context.MeetingInviteeBoardOfDirectors.Attach(meetingInviteeBoardOfDirector);
                    context.Entry(meetingInviteeBoardOfDirector).State = EntityState.Added;
                }

                // MeetingInviteeMember - Amend Old Record
                IEnumerable<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelForAmend = await meetingInviteeMemberRepository.GetRejectedEntries(_meetingViewModel.MeetingPrmKey);

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelForAmend)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerCheckerForAmend = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerCheckerForAmend);
                    context.Entry(meetingInviteeMemberMakerCheckerForAmend).State = EntityState.Added;
                }

                // MeetingInviteeMember - Add New Amended Entry, Get BusinessOfficeSpecialPermission Details From Session Object
                List<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = new List<MeetingInviteeMemberViewModel>();

                meetingInviteeMemberViewModelList = (List<MeetingInviteeMemberViewModel>)HttpContext.Current.Session["MeetingInviteeMember"];

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.MeetingInviteeMemberPrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.CustomerSharesCapitalAccountPrmKey = 4;
                    //viewModel.SharesCapitalCustomerAccountPrmKey = sharesCapitalCustomerAccountRepository.GetPrmKeyById(viewModel.SharesCapitalCustomerAccountId);

                    // MeetingInviteeMember
                    MeetingInviteeMember meetingInviteeMember = Mapper.Map<MeetingInviteeMember>(viewModel);
                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                    context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                    meetingInviteeMember.MeetingInviteeMemberMakerCheckers.Add(meetingInviteeMemberMakerChecker);

                    context.MeetingInviteeMembers.Attach(meetingInviteeMember);
                    context.Entry(meetingInviteeMember).State = EntityState.Added;
                }

                // MeetingNotice - Amend Old Record
                IEnumerable<MeetingNoticeViewModel> meetingNoticeViewModelForAmend = await meetingNoticeRepository.GetRejectedEntries(_meetingViewModel.MeetingPrmKey);

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelForAmend)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingNoticeMakerChecker meetingNoticeMakerCheckerForAmend = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerCheckerForAmend);
                    context.Entry(meetingNoticeMakerCheckerForAmend).State = EntityState.Added;
                }

                // MeetingNotice - Add New Amended Entry, Get BusinessOfficeTransactionLimit Details From Session Object
                List<MeetingNoticeViewModel> meetingNoticeViewModelList = new List<MeetingNoticeViewModel>();

                meetingNoticeViewModelList = (List<MeetingNoticeViewModel>)HttpContext.Current.Session["MeetingNotice"];

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.MeetingNoticePrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns 
                    viewModel.NoticeMediaPrmKey = managementDetailRepository.GetNoticeMediaPrmKeyById(viewModel.NoticeMediaId);
                    viewModel.SchedulePrmKey = enterpriseDetailRepository.GetSchedulePrmKeyById(viewModel.ScheduleId);
                    viewModel.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(viewModel.MenuId);

                    // MeetingNotice 
                    MeetingNotice meetingNotice = Mapper.Map<MeetingNotice>(viewModel);
                    MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                    context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
                    meetingNotice.MeetingNoticeMakerCheckers.Add(meetingNoticeMakerChecker);

                    context.MeetingNotices.Attach(meetingNotice);
                    context.Entry(meetingNotice).State = EntityState.Added;
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

        public async Task<bool> Delete(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.UserAction = StringLiteralValue.Delete;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                MeetingMakerChecker meetingMakerChecker = Mapper.Map<MeetingMakerChecker>(_meetingViewModel);
                MeetingModificationMakerChecker meetingModificationMakerChecker = Mapper.Map<MeetingModificationMakerChecker>(_meetingViewModel);
                MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_meetingViewModel.MeetingModificationPrmKey == 0)
                {
                    // Meeting
                    context.MeetingMakerCheckers.Attach(meetingMakerChecker);
                    context.Entry(meetingMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // MeetingModification
                    context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerChecker);
                    context.Entry(meetingModificationMakerChecker).State = EntityState.Added;
                }

                // MeetingTranslation 
                context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;

                // MeetingAgenda
                List<MeetingAgendaViewModel> meetingAgendaViewModelList = new List<MeetingAgendaViewModel>();

                meetingAgendaViewModelList = (List<MeetingAgendaViewModel>)HttpContext.Current.Session["MeetingAgenda"];

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                    context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                }

                // MeetingInviteeBoardOfDirector
                List<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = new List<MeetingInviteeBoardOfDirectorViewModel>();

                meetingInviteeBoardOfDirectorViewModelList = (List<MeetingInviteeBoardOfDirectorViewModel>)HttpContext.Current.Session["MeetingInviteeBoardOfDirector"];

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                    context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                }

                // MeetingInviteeMember
                List<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = new List<MeetingInviteeMemberViewModel>();

                meetingInviteeMemberViewModelList = (List<MeetingInviteeMemberViewModel>)HttpContext.Current.Session["MeetingInviteeMember"];

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                    context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                }

                // MeetingNotice
                List<MeetingNoticeViewModel> meetingNoticeViewModelList = new List<MeetingNoticeViewModel>();

                meetingNoticeViewModelList = (List<MeetingNoticeViewModel>)HttpContext.Current.Session["MeetingNotice"];

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                    context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<MeetingViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public int GetPrmKeyById(Guid _meetingId)
        {
            return context.Meetings
                    .Where(c => c.MeetingId == _meetingId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> MeetingDropdownList
        {
            get
            {
                return (from e in context.Meetings

                        select new SelectListItem
                        {
                            Value = e.MeetingId.ToString(),
                            Text = e.Title
                        }).ToList();
            }
        }

        public async Task<MeetingViewModel> GetRejectedEntry(Guid _meetingId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntry (@MeetingId, @EntriesType)", new SqlParameter("@MeetingId", _meetingId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MeetingViewModel> GetUnVerifiedEntry(Guid _meetingId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntry (@MeetingId, @EntriesType)", new SqlParameter("@MeetingId", _meetingId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<MeetingViewModel> GetVerifiedEntry(Guid _meetingId)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingViewModel>("SELECT * FROM dbo.GetMeetingEntry (@MeetingId, @EntriesType)", new SqlParameter("@MeetingId", _meetingId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.EntryStatus = StringLiteralValue.Create;
                _meetingViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingViewModel.Remark = "None";
                _meetingViewModel.UserAction = StringLiteralValue.Create;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Get PrmKey By Id
                _meetingViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingViewModel.MeetingTypeId);

                // MeetingModification
                MeetingModification meetingModification = Mapper.Map<MeetingModification>(_meetingViewModel);
                MeetingModificationMakerChecker meetingModificationMakerChecker = Mapper.Map<MeetingModificationMakerChecker>(_meetingViewModel);

                // MeetingTranslation
                MeetingTranslation meetingTranslation = Mapper.Map<MeetingTranslation>(_meetingViewModel);
                MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                // MeetingAgenda 
                List<MeetingAgendaViewModel> meetingAgendaViewModelList = new List<MeetingAgendaViewModel>();

                meetingAgendaViewModelList = (List<MeetingAgendaViewModel>)HttpContext.Current.Session["MeetingAgenda"];

                List<MeetingAgenda> meetingAgendaList = new List<MeetingAgenda>();

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.AgendaPrmKey = agendaRepository.GetPrmKeyById(viewModel.AgendaId);

                    MeetingAgenda meetingAgenda = Mapper.Map<MeetingAgenda>(viewModel);
                    MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                    context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                    meetingAgenda.MeetingAgendaMakerCheckers.Add(meetingAgendaMakerChecker);

                    context.MeetingAgendas.Attach(meetingAgenda);
                    context.Entry(meetingAgenda).State = EntityState.Added;
                }

                // MeetingInviteeBoardOfDirector
                List<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = new List<MeetingInviteeBoardOfDirectorViewModel>();

                meetingInviteeBoardOfDirectorViewModelList = (List<MeetingInviteeBoardOfDirectorViewModel>)HttpContext.Current.Session["MeetingInviteeBoardOfDirector"];

                List<MeetingInviteeBoardOfDirector> meetingInviteeBoardOfDirectorList = new List<MeetingInviteeBoardOfDirector>();

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = 3;
                    //viewModel.BoardOfDirectorPrmKey = boardOfDirectorRepository.GetPrmKeyById(viewModel.BoardOfDirectorId);

                    MeetingInviteeBoardOfDirector meetingInviteeBoardOfDirector = Mapper.Map<MeetingInviteeBoardOfDirector>(viewModel);
                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                    context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                    meetingInviteeBoardOfDirector.MeetingInviteeBoardOfDirectorMakerCheckers.Add(meetingInviteeBoardOfDirectorMakerChecker);

                    context.MeetingInviteeBoardOfDirectors.Attach(meetingInviteeBoardOfDirector);
                    context.Entry(meetingInviteeBoardOfDirector).State = EntityState.Added;
                }

                // MeetingInviteeMember
                List<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = new List<MeetingInviteeMemberViewModel>();

                meetingInviteeMemberViewModelList = (List<MeetingInviteeMemberViewModel>)HttpContext.Current.Session["MeetingInviteeMember"];

                List<MeetingInviteeMember> meetingInviteeMemberList = new List<MeetingInviteeMember>();

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.CustomerSharesCapitalAccountPrmKey = 4;
                    //viewModel.SharesCapitalCustomerAccountPrmKey = sharesCapitalCustomerAccountRepository.GetPrmKeyById(viewModel.SharesCapitalCustomerAccountId);

                    MeetingInviteeMember meetingInviteeMember = Mapper.Map<MeetingInviteeMember>(viewModel);
                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                    context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                    meetingInviteeMember.MeetingInviteeMemberMakerCheckers.Add(meetingInviteeMemberMakerChecker);

                    context.MeetingInviteeMembers.Attach(meetingInviteeMember);
                    context.Entry(meetingInviteeMember).State = EntityState.Added;
                }

                // MeetingNotice
                List<MeetingNoticeViewModel> meetingNoticeViewModelList = new List<MeetingNoticeViewModel>();

                meetingNoticeViewModelList = (List<MeetingNoticeViewModel>)HttpContext.Current.Session["MeetingNotice"];

                List<MeetingNotice> meetingNoticeList = new List<MeetingNotice>();

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.NoticeMediaPrmKey = managementDetailRepository.GetNoticeMediaPrmKeyById(viewModel.NoticeMediaId);
                    viewModel.SchedulePrmKey = enterpriseDetailRepository.GetSchedulePrmKeyById(viewModel.ScheduleId);
                    viewModel.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(viewModel.MenuId);

                    MeetingNotice meetingNotice = Mapper.Map<MeetingNotice>(viewModel);
                    MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                    context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
                    meetingNotice.MeetingNoticeMakerCheckers.Add(meetingNoticeMakerChecker);

                    context.MeetingNotices.Attach(meetingNotice);
                    context.Entry(meetingNotice).State = EntityState.Added;
                }

                // MeetingModification
                context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerChecker);
                context.Entry(meetingModificationMakerChecker).State = EntityState.Added;
                meetingModification.MeetingModificationMakerCheckers.Add(meetingModificationMakerChecker);

                context.MeetingModifications.Attach(meetingModification);
                context.Entry(meetingModification).State = EntityState.Added;

                // MeetingTranslation
                context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;
                meetingTranslation.MeetingTranslationMakerCheckers.Add(meetingTranslationMakerChecker);

                context.MeetingTranslations.Attach(meetingTranslation);
                context.Entry(meetingTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.UserAction = StringLiteralValue.Reject;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                MeetingMakerChecker meetingMakerChecker = Mapper.Map<MeetingMakerChecker>(_meetingViewModel);
                MeetingModificationMakerChecker meetingModificationMakerChecker = Mapper.Map<MeetingModificationMakerChecker>(_meetingViewModel);
                MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table

                if (_meetingViewModel.MeetingModificationPrmKey == 0)
                {
                    // Meeting
                    context.MeetingMakerCheckers.Attach(meetingMakerChecker);
                    context.Entry(meetingMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // MeetingModification
                    context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerChecker);
                    context.Entry(meetingModificationMakerChecker).State = EntityState.Added;
                }

                // MeetingTranslation 
                context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;

                // MeetingAgenda
                List<MeetingAgendaViewModel> meetingAgendaViewModelList = new List<MeetingAgendaViewModel>();

                meetingAgendaViewModelList = (List<MeetingAgendaViewModel>)HttpContext.Current.Session["MeetingAgenda"];

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                    context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                }

                // MeetingInviteeBoardOfDirector
                List<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = new List<MeetingInviteeBoardOfDirectorViewModel>();

                meetingInviteeBoardOfDirectorViewModelList = (List<MeetingInviteeBoardOfDirectorViewModel>)HttpContext.Current.Session["MeetingInviteeBoardOfDirector"];

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                    context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                }

                // MeetingInviteeMember
                List<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = new List<MeetingInviteeMemberViewModel>();

                meetingInviteeMemberViewModelList = (List<MeetingInviteeMemberViewModel>)HttpContext.Current.Session["MeetingInviteeMember"];

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                    context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                }

                // MeetingNotice
                List<MeetingNoticeViewModel> meetingNoticeViewModelList = new List<MeetingNoticeViewModel>();

                meetingNoticeViewModelList = (List<MeetingNoticeViewModel>)HttpContext.Current.Session["MeetingNotice"];

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                {
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _meetingViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                    context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
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
        public async Task<bool> Save(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.EntryStatus = StringLiteralValue.Create;
                _meetingViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _meetingViewModel.ReasonForModification = "None";
                _meetingViewModel.Remark = "None";
                _meetingViewModel.TransReasonForModification = "None";
                _meetingViewModel.UserAction = StringLiteralValue.Create;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns
                _meetingViewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(_meetingViewModel.MeetingTypeId);

                // Meeting
                Meeting meeting = Mapper.Map<Meeting>(_meetingViewModel);
                MeetingMakerChecker meetingMakerChecker = Mapper.Map<MeetingMakerChecker>(_meetingViewModel);

                // MeetingTranslation
                MeetingTranslation meetingTranslation = Mapper.Map<MeetingTranslation>(_meetingViewModel);
                MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                // MeetingAgenda
                List<MeetingAgendaViewModel> meetingAgendaViewModelList = (List<MeetingAgendaViewModel>)HttpContext.Current.Session["MeetingAgenda"];

                List<MeetingAgenda> meetingAgendaList = new List<MeetingAgenda>();

                foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.AgendaPrmKey = agendaRepository.GetPrmKeyById(viewModel.AgendaId);

                    MeetingAgenda meetingAgenda = Mapper.Map<MeetingAgenda>(viewModel);
                    MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                    context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                    context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                    meetingAgenda.MeetingAgendaMakerCheckers.Add(meetingAgendaMakerChecker);

                    context.MeetingAgendas.Attach(meetingAgenda);
                    context.Entry(meetingAgenda).State = EntityState.Added;

                    meeting.MeetingAgendas.Add(meetingAgenda);
                }

                // MeetingInviteeBoardOfDirector 
                List<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = new List<MeetingInviteeBoardOfDirectorViewModel>();

                meetingInviteeBoardOfDirectorViewModelList = (List<MeetingInviteeBoardOfDirectorViewModel>)HttpContext.Current.Session["MeetingInviteeBoardOfDirector"];

                List<MeetingInviteeBoardOfDirector> meetingInviteeBoardOfDirectorList = new List<MeetingInviteeBoardOfDirector>();

                foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;
                    viewModel.AttendanceStatus = "sde";
                    viewModel.NoticeStatus = "fde";

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.BoardOfDirectorPrmKey = 3;
                    //viewModel.BoardOfDirectorPrmKey = boardOfDirectorRepository.GetPrmKeyById(viewModel.BoardOfDirectorId);

                    MeetingInviteeBoardOfDirector meetingInviteeBoardOfDirector = Mapper.Map<MeetingInviteeBoardOfDirector>(viewModel);
                    MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                    context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                    context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                    meetingInviteeBoardOfDirector.MeetingInviteeBoardOfDirectorMakerCheckers.Add(meetingInviteeBoardOfDirectorMakerChecker);

                    context.MeetingInviteeBoardOfDirectors.Attach(meetingInviteeBoardOfDirector);
                    context.Entry(meetingInviteeBoardOfDirector).State = EntityState.Added;

                    meeting.MeetingInviteeBoardOfDirectors.Add(meetingInviteeBoardOfDirector);
                }

                // MeetingInviteeMember
                List<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = new List<MeetingInviteeMemberViewModel>();

                meetingInviteeMemberViewModelList = (List<MeetingInviteeMemberViewModel>)HttpContext.Current.Session["MeetingInviteeMember"];

                List<MeetingInviteeMember> meetingInviteeMemberList = new List<MeetingInviteeMember>();

                foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                {
                    // Set Default Value 
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;
                    viewModel.AttendanceStatus = "ABC";
                    viewModel.NoticeStatus = "xyz";

                    // Get PrmKey By Id Of All Dropdowns
                    viewModel.CustomerSharesCapitalAccountPrmKey = 4;
                    //viewModel.SharesCapitalCustomerAccountPrmKey = sharesCapitalCustomerAccountRepository.GetPrmKeyById(viewModel.SharesCapitalCustomerAccountId);

                    MeetingInviteeMember meetingInviteeMember = Mapper.Map<MeetingInviteeMember>(viewModel);
                    MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                    context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                    context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                    meetingInviteeMember.MeetingInviteeMemberMakerCheckers.Add(meetingInviteeMemberMakerChecker);

                    context.MeetingInviteeMembers.Attach(meetingInviteeMember);
                    context.Entry(meetingInviteeMember).State = EntityState.Added;

                    meeting.MeetingInviteeMembers.Add(meetingInviteeMember);
                }

                // MeetingNotice
                List<MeetingNoticeViewModel> meetingNoticeViewModelList = new List<MeetingNoticeViewModel>();

                meetingNoticeViewModelList = (List<MeetingNoticeViewModel>)HttpContext.Current.Session["MeetingNotice"];

                List<MeetingNotice> meetingNoticeList = new List<MeetingNotice>();

                foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = "None";
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.MeetingPrmKey = _meetingViewModel.MeetingPrmKey;

                    // Get PrmKey By Id Of All Dropdowns  
                    viewModel.NoticeMediaPrmKey = managementDetailRepository.GetNoticeMediaPrmKeyById(viewModel.NoticeMediaId);
                    viewModel.SchedulePrmKey = enterpriseDetailRepository.GetSchedulePrmKeyById(viewModel.ScheduleId);
                    viewModel.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(viewModel.MenuId);

                    MeetingNotice meetingNotice = Mapper.Map<MeetingNotice>(viewModel);
                    MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                    context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                    context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
                    meetingNotice.MeetingNoticeMakerCheckers.Add(meetingNoticeMakerChecker);

                    context.MeetingNotices.Attach(meetingNotice);
                    context.Entry(meetingNotice).State = EntityState.Added;

                    meeting.MeetingNotices.Add(meetingNotice);
                }

                // Meeting
                context.MeetingMakerCheckers.Attach(meetingMakerChecker);
                context.Entry(meetingMakerChecker).State = EntityState.Added;
                meeting.MeetingMakerCheckers.Add(meetingMakerChecker);

                context.Meetings.Attach(meeting);
                context.Entry(meeting).State = EntityState.Added;

                // MeetingTranslation
                context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;
                meetingTranslation.MeetingTranslationMakerCheckers.Add(meetingTranslationMakerChecker);

                context.MeetingTranslations.Attach(meetingTranslation);
                context.Entry(meetingTranslation).State = EntityState.Added;
                meeting.MeetingTranslations.Add(meetingTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(MeetingViewModel _meetingViewModel)
        {
            try
            {
                // Set Default Value
                _meetingViewModel.EntryDateTime = DateTime.Now;
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_meetingViewModel.MeetingModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    MeetingViewModel meetingViewModelOldEntry = await GetVerifiedEntry(_meetingViewModel.MeetingId);

                    // Set Default Value
                    meetingViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    meetingViewModelOldEntry.UserProfilePrmKey = _meetingViewModel.UserProfilePrmKey;

                    // Mapping
                    MeetingTranslationMakerChecker meetingTranslationMakerCheckerForModify = Mapper.Map<MeetingTranslationMakerChecker>(meetingViewModelOldEntry);

                    // MeetingTranslation
                    context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerCheckerForModify);
                    context.Entry(meetingTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Modify (i.e. Old Verified Entries)
                    // MeetingAgenda
                    IEnumerable<MeetingAgendaViewModel> meetingAgendaViewModelListForModify = await meetingAgendaRepository.GetVerifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingAgendaMakerChecker meetingAgendaMakerCheckerForModify = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                        context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerCheckerForModify);
                        context.Entry(meetingAgendaMakerCheckerForModify).State = EntityState.Added;
                    }

                    // MeetingInviteeBoardOfDirector
                    IEnumerable<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelListForModify = await meetingInviteeBoardOfDirectorRepository.GetVerifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerCheckerForModify = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                        context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerCheckerForModify);
                        context.Entry(meetingInviteeBoardOfDirectorMakerCheckerForModify).State = EntityState.Added;
                    }

                    // MeetingInviteeMember
                    IEnumerable<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelListForModify = await meetingInviteeMemberRepository.GetVerifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerCheckerForModify = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                        context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerCheckerForModify);
                        context.Entry(meetingInviteeMemberMakerCheckerForModify).State = EntityState.Added;
                    }

                    // MeetingNotice
                    IEnumerable<MeetingNoticeViewModel> meetingNoticeViewModelListForModify = await meetingNoticeRepository.GetVerifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelListForModify)
                    {
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingNoticeMakerChecker meetingNoticeMakerCheckerForModify = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                        context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerCheckerForModify);
                        context.Entry(meetingNoticeMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table

                    if (meetingViewModelOldEntry.IsModified == true)
                    {
                        MeetingModificationMakerChecker meetingModificationMakerCheckerForModify = Mapper.Map<MeetingModificationMakerChecker>(meetingViewModelOldEntry);

                        context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerCheckerForModify);
                        context.Entry(meetingModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _meetingViewModel.EntryDateTime = DateTime.Now;
                    _meetingViewModel.UserAction = StringLiteralValue.Verify;
                    _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    MeetingModificationMakerChecker meetingModificationMakerChecker = Mapper.Map<MeetingModificationMakerChecker>(_meetingViewModel);
                    MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                    // MeetingModification
                    context.MeetingModificationMakerCheckers.Attach(meetingModificationMakerChecker);
                    context.Entry(meetingModificationMakerChecker).State = EntityState.Added;

                    // MeetingTranslation
                    context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                    context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;

                    // MeetingAgenda
                    IEnumerable<MeetingAgendaViewModel> meetingAgendaViewModelsList = await meetingAgendaRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelsList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                        context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                        context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                    }

                    // MeetingInviteeBoardOfDirector
                    IEnumerable<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelsList = await meetingInviteeBoardOfDirectorRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelsList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                        context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                        context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                    }

                    // MeetingInviteeMember
                    IEnumerable<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelsList = await meetingInviteeMemberRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelsList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                        context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                        context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                    }

                    // MeetingNotice
                    IEnumerable<MeetingNoticeViewModel> meetingNoticeViewModelsList = await meetingNoticeRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelsList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryStatus = StringLiteralValue.Verify;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                        context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                        context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
                    }
                }
                else
                {
                    // Set Default Value
                    _meetingViewModel.UserAction = StringLiteralValue.Verify;

                    // Mapping
                    MeetingMakerChecker meetingMakerChecker = Mapper.Map<MeetingMakerChecker>(_meetingViewModel);
                    MeetingTranslationMakerChecker meetingTranslationMakerChecker = Mapper.Map<MeetingTranslationMakerChecker>(_meetingViewModel);

                    // Meeting
                    context.MeetingMakerCheckers.Attach(meetingMakerChecker);
                    context.Entry(meetingMakerChecker).State = EntityState.Added;

                    // MeetingTranslation
                    context.MeetingTranslationMakerCheckers.Attach(meetingTranslationMakerChecker);
                    context.Entry(meetingTranslationMakerChecker).State = EntityState.Added;

                    // MeetingAgenda
                    IEnumerable<MeetingAgendaViewModel> meetingAgendaViewModelList = await meetingAgendaRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingAgendaViewModel viewModel in meetingAgendaViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingAgendaMakerChecker meetingAgendaMakerChecker = Mapper.Map<MeetingAgendaMakerChecker>(viewModel);

                        context.MeetingAgendaMakerCheckers.Attach(meetingAgendaMakerChecker);
                        context.Entry(meetingAgendaMakerChecker).State = EntityState.Added;
                    }

                    // MeetingInviteeBoardOfDirector
                    IEnumerable<MeetingInviteeBoardOfDirectorViewModel> meetingInviteeBoardOfDirectorViewModelList = await meetingInviteeBoardOfDirectorRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeBoardOfDirectorViewModel viewModel in meetingInviteeBoardOfDirectorViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeBoardOfDirectorMakerChecker meetingInviteeBoardOfDirectorMakerChecker = Mapper.Map<MeetingInviteeBoardOfDirectorMakerChecker>(viewModel);

                        context.MeetingInviteeBoardOfDirectorMakerCheckers.Attach(meetingInviteeBoardOfDirectorMakerChecker);
                        context.Entry(meetingInviteeBoardOfDirectorMakerChecker).State = EntityState.Added;
                    }

                    // MeetingInviteeMember
                    IEnumerable<MeetingInviteeMemberViewModel> meetingInviteeMemberViewModelList = await meetingInviteeMemberRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingInviteeMemberViewModel viewModel in meetingInviteeMemberViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingInviteeMemberMakerChecker meetingInviteeMemberMakerChecker = Mapper.Map<MeetingInviteeMemberMakerChecker>(viewModel);

                        context.MeetingInviteeMemberMakerCheckers.Attach(meetingInviteeMemberMakerChecker);
                        context.Entry(meetingInviteeMemberMakerChecker).State = EntityState.Added;
                    }

                    // MeetingNotice
                    IEnumerable<MeetingNoticeViewModel> meetingNoticeViewModelList = await meetingNoticeRepository.GetUnverifiedEntries(_meetingViewModel.MeetingPrmKey);

                    foreach (MeetingNoticeViewModel viewModel in meetingNoticeViewModelList)
                    {
                        viewModel.PrmKey = 0;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.Remark = _meetingViewModel.Remark;
                        viewModel.UserAction = StringLiteralValue.Verify;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        MeetingNoticeMakerChecker meetingNoticeMakerChecker = Mapper.Map<MeetingNoticeMakerChecker>(viewModel);

                        context.MeetingNoticeMakerCheckers.Attach(meetingNoticeMakerChecker);
                        context.Entry(meetingNoticeMakerChecker).State = EntityState.Added;
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
