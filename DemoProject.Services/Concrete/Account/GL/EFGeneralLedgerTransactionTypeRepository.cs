using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Constants;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerTransactionTypeRepository : IGeneralLedgerTransactionTypeRepository
    {
        private readonly EFDbContext context;

        private readonly IAccountDetailRepository accountDetailRepository;

        public EFGeneralLedgerTransactionTypeRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
        }

        public async Task<bool> Amend(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {
                IEnumerable<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeDetailViewModelList = await GetRejectedEntries(_generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey);

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeDetailViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    //GeneralLedgerTransactionTypeMakerChecker
                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                }

                // Get General Ledger Transaction Type From Session Object
                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();

                generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _generalLedgerTransactionTypeViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(viewModel.TransactionTypeId);

                    GeneralLedgerTransactionType generalLedgerTransactionType = Mapper.Map<GeneralLedgerTransactionType>(viewModel);
                    generalLedgerTransactionType.GeneralLedgerPrmKey = _generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey;

                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                    generalLedgerTransactionType.GeneralLedgerTransactionTypeMakerCheckers.Add(generalLedgerTransactionTypeMakerChecker);

                    context.GeneralLedgerTransactionTypes.Attach(generalLedgerTransactionType);
                    context.Entry(generalLedgerTransactionType).State = EntityState.Added;
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

        public async Task<bool> Delete(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {

                List<GeneralLedgerTransactionTypeViewModel> centerTradingDetailViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();

                centerTradingDetailViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in centerTradingDetailViewModelList)
                {
                    // Set Default Value 
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    GeneralLedgerTransactionTypeMakerChecker centerTradingDetailMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    //GeneralLedgerTransactionTypeMakerChecker
                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(centerTradingDetailMakerChecker);
                    context.Entry(centerTradingDetailMakerChecker).State = EntityState.Added;
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

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForTransactionTypeCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForTransactionTypeCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForTransactionTypeCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForTransactionTypeCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetRejectedEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetUnverifiedEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetVerifiedEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForCreate(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForReject(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForUnverified(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForVerified(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerTransactionTypeViewModel>("SELECT * FROM dbo.GetGeneralLedgerTransactionTypeEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {
                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();

                generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                {
                    // Set Dafault Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    //GeneralLedgerTransactionTypeMakerChecker
                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {
                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();

                generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.GeneralLedgerPrmKey = _generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Get PrmKey By Id
                    viewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(viewModel.TransactionTypeId);

                    //Mapping
                    GeneralLedgerTransactionType generalLedgerTransactionType = Mapper.Map<GeneralLedgerTransactionType>(viewModel);
                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    //GeneralLedgerTransactionType
                    context.GeneralLedgerTransactionTypes.Attach(generalLedgerTransactionType);
                    context.Entry(generalLedgerTransactionType).State = EntityState.Added;

                    //GeneralLedgerTransactionTypeMakerChecker
                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                    generalLedgerTransactionType.GeneralLedgerTransactionTypeMakerCheckers.Add(generalLedgerTransactionTypeMakerChecker);

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

        public async Task<bool> Modify(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();

                generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = _generalLedgerTransactionTypeViewModel.ReasonForModification;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(viewModel.TransactionTypeId);

                    GeneralLedgerTransactionType generalLedgerTransactionType = Mapper.Map<GeneralLedgerTransactionType>(viewModel);
                    generalLedgerTransactionType.GeneralLedgerPrmKey = _generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey;

                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                    generalLedgerTransactionType.GeneralLedgerTransactionTypeMakerCheckers.Add(generalLedgerTransactionTypeMakerChecker);

                    context.GeneralLedgerTransactionTypes.Attach(generalLedgerTransactionType);
                    context.Entry(generalLedgerTransactionType).State = EntityState.Added;
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

        public async Task<bool> Verify(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel)
        {
            try
            {
                IEnumerable<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeDetailViewModelList = await GetVerifiedEntries(_generalLedgerTransactionTypeViewModel.GeneralLedgerPrmKey);

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeDetailViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                }

                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = new List<GeneralLedgerTransactionTypeViewModel>();
                generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
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