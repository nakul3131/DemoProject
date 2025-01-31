using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Management.Master;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFAgendaMeetingTypeRepository : IAgendaMeetingTypeRepository
    {
        private readonly EFDbContext context;
        private readonly IAgendaRepository agendaRepository;
        private readonly IManagementDetailRepository managementDetailRepository;

        public EFAgendaMeetingTypeRepository(RepositoryConnection _connection, IAgendaRepository _agendaRepository, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            agendaRepository = _agendaRepository;
            managementDetailRepository = _managementDetailRepository;
        }

        public async Task<bool> Amend(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                IEnumerable<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModelsList = await GetRejectedEntries(_agendaMeetingTypeViewModel.AgendaPrmKey);

                foreach (AgendaMeetingTypeViewModel viewModel in agendaMeetingTypeViewModelsList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);
                    
                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
                }

                // Get Agenda Meeting Type From Session Object
                List<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModelList = new List<AgendaMeetingTypeViewModel>();

                agendaMeetingTypeViewModelList = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in agendaMeetingTypeViewModelList)
                {
                    // Set Default Value    q
                    viewModel.ActivationStatus = StringLiteralValue.Inactive;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = _agendaMeetingTypeViewModel.Note;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.AgendaPrmKey = _agendaMeetingTypeViewModel.AgendaPrmKey;
                    viewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(viewModel.MeetingTypeId);

                    //Mapping
                    AgendaMeetingType agendaMeetingType = Mapper.Map<AgendaMeetingType>(viewModel);
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
                    agendaMeetingType.AgendaMeetingTypeMakerCheckers.Add(agendaMeetingTypeMakerChecker);

                    context.AgendaMeetingTypes.Attach(agendaMeetingType);
                    context.Entry(agendaMeetingType).State = EntityState.Added;

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

        public async Task<bool> Delete(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                List<AgendaMeetingTypeViewModel> evaluationSectorContentItemViewModels = new List<AgendaMeetingTypeViewModel>();

                evaluationSectorContentItemViewModels = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in evaluationSectorContentItemViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = "None";
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Closed(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                // Get Agenda Meeting Type From Session Object
                List<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModelList = new List<AgendaMeetingTypeViewModel>();

                agendaMeetingTypeViewModelList = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in agendaMeetingTypeViewModelList)
                {
                    // Set Default Value    
                    viewModel.ActivationStatus = StringLiteralValue.Inactive;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = _agendaMeetingTypeViewModel.Note;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.AgendaPrmKey = _agendaMeetingTypeViewModel.AgendaPrmKey;
                    viewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(viewModel.MeetingTypeId);

                    //Mapping
                    AgendaMeetingType agendaMeetingType = Mapper.Map<AgendaMeetingType>(viewModel);
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
                    agendaMeetingType.AgendaMeetingTypeMakerCheckers.Add(agendaMeetingTypeMakerChecker);

                    context.AgendaMeetingTypes.Attach(agendaMeetingType);
                    context.Entry(agendaMeetingType).State = EntityState.Added;

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

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaEntriesForAgendaMeetingTypeCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaEntriesForAgendaMeetingTypeCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaEntriesForAgendaMeetingTypeCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Closed)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaEntriesForAgendaMeetingTypeCRUDOperation  ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public List<SelectListItem> GetModelEntries(Guid agendaId)
        {
            List<SelectListItem> modelNames = new List<SelectListItem>();

            int AgendaPrmKey = agendaRepository.GetPrmKeyById(agendaId);
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from v in context.MeetingTypes
                        join t in context.MeetingTypeTranslations on v.PrmKey equals t.MeetingTypePrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        where ( v.ActivationStatus.Equals(StringLiteralValue.Active)
                                && t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = v.MeetingTypeId.ToString(),
                            Text = (v.NameOfMeetingType.Trim() + " --> " + (t.TransNameOfMeetingType.Equals(null) ? " " : t.TransNameOfMeetingType.Trim()))
                        }).ToList();

            }

            // Default List In Defaul Language (i.e. English)
            return (from v in context.MeetingTypes
                    where (!(from m in context.AgendaMeetingTypes
                             where m.AgendaPrmKey.Equals(AgendaPrmKey)
                             select m.MeetingTypePrmKey).Contains(v.PrmKey)
                             && v.ActivationStatus.Equals(StringLiteralValue.Active))
                    select new SelectListItem
                    {
                        Value = v.MeetingTypeId.ToString(),
                        Text = v.NameOfMeetingType.Trim()
                    }).ToList();

        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetRejectedEntries(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetUnverifiedEntries(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaMeetingTypeViewModel>> GetVerifiedEntries(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaMeetingTypeViewModel> GetViewModelForCreate(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @AgendaPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@AgendaPrmKey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaMeetingTypeViewModel> GetViewModelForReject(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaMeetingTypeViewModel> GetViewModelForUnverified(int _agendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaMeetingTypeViewModel>("SELECT * FROM dbo.GetAgendaMeetingTypeEntriesByAgendaPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _agendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                List<AgendaMeetingTypeViewModel> AgendaMeetingTypeViewModels = new List<AgendaMeetingTypeViewModel>();

                AgendaMeetingTypeViewModels = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in AgendaMeetingTypeViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.Remark = "None";
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                List<AgendaMeetingTypeViewModel> evaluationSectorContentItemViewModels = new List<AgendaMeetingTypeViewModel>();

                evaluationSectorContentItemViewModels = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in evaluationSectorContentItemViewModels)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Inactive;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CloseDate = new DateTime(2021, 01, 01);
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.Note = _agendaMeetingTypeViewModel.Note;

                    //Get PrmKey By Id
                    viewModel.AgendaPrmKey = _agendaMeetingTypeViewModel.AgendaPrmKey;
                    viewModel.MeetingTypePrmKey = managementDetailRepository.GetMeetingTypePrmKeyById(viewModel.MeetingTypeId);

                    //Mapping
                    AgendaMeetingType agendaMeetingType = Mapper.Map<AgendaMeetingType>(viewModel);
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
                    agendaMeetingType.AgendaMeetingTypeMakerCheckers.Add(agendaMeetingTypeMakerChecker);

                    context.AgendaMeetingTypes.Attach(agendaMeetingType);
                    context.Entry(agendaMeetingType).State = EntityState.Added;
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

        public async Task<bool> Verify(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            try
            {
                IEnumerable<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModelsList = await GetVerifiedEntries(_agendaMeetingTypeViewModel.AgendaPrmKey);

                foreach (AgendaMeetingTypeViewModel viewModel in agendaMeetingTypeViewModelsList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
                }

                List<AgendaMeetingTypeViewModel> evaluationSectorContentItemViewModels = new List<AgendaMeetingTypeViewModel>();

                evaluationSectorContentItemViewModels = (List<AgendaMeetingTypeViewModel>)HttpContext.Current.Session["AgendaMeetingType"];

                foreach (AgendaMeetingTypeViewModel viewModel in evaluationSectorContentItemViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.Remark = "None";
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    
                    //Mapping
                    AgendaMeetingTypeMakerChecker agendaMeetingTypeMakerChecker = Mapper.Map<AgendaMeetingTypeMakerChecker>(viewModel);
                    agendaMeetingTypeMakerChecker.PrmKey = 0;

                    //AgendaMeetingType
                    context.AgendaMeetingTypeMakerCheckers.Attach(agendaMeetingTypeMakerChecker);
                    context.Entry(agendaMeetingTypeMakerChecker).State = EntityState.Added;
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
