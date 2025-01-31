using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFPeriodCodeRepository : IPeriodCodeRepository
    {
        private readonly EFDbContext context;

        public EFPeriodCodeRepository(RepositoryConnection _connection) 
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(PeriodCodeViewModel _periodCodeViewModel) 
        {
            try
            {
                IEnumerable<PeriodCodeViewModel> periodCodeViewModelViewModelList = await GetRejectedEntries(_periodCodeViewModel.FinancialCyclePrmKey);

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelViewModelList) 
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    //PeriodCodeMakerChecker
                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
                }

                // Get Trading Entity Details From Session Object
                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();

                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _periodCodeViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    // viewModel.FundContributionFrequencyPrmKey = fundContributionFrequencyRepository.GetPrmKeyById(viewModel.FundContributionFrequencyId);

                    PeriodCode periodCode = Mapper.Map<PeriodCode>(viewModel);
                    //periodCode.GeneralLedgerPrmKey = _periodCodeViewModel.GeneralLedgerPrmKey;

                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
                    periodCode.PeriodCodeMakerCheckers.Add(periodCodeMakerChecker);

                    context.PeriodCodes.Attach(periodCode);
                    context.Entry(periodCode).State = EntityState.Added;
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

        public async Task<bool> Delete(PeriodCodeViewModel _periodCodeViewModel)
        {
            try
            {

                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();

                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value 
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    PeriodCodeMakerChecker centerTradingDetailMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    //PeriodCodeMakerChecker
                    context.PeriodCodeMakerCheckers.Attach(centerTradingDetailMakerChecker);
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

        public async Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForFundContributionFrequencyCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForFundContributionFrequencyCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForFundContributionFrequencyCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntriesForFundContributionFrequencyCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPrmKeyByFinancialCycleId(Guid _financialCycleId)  
        {
            return context.FinancialCycles
                    .Where(c => c.FinancialCycleId == _financialCycleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetRejectedEntries(short _financialCyclePrmKey)
        {
            try
            {
               var a=  await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetUnverifiedEntries(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PeriodCodeViewModel>> GetVerifiedEntries(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PeriodCodeViewModel> GetViewModelForCreate(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PeriodCodeViewModel> GetViewModelForReject(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PeriodCodeViewModel> GetViewModelForUnverified(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PeriodCodeViewModel> GetViewModelForVerified(short _financialCyclePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PeriodCodeViewModel>("SELECT * FROM dbo.GetPeriodCodeEntriesByFinancialCyclePrmKey (@UserProfilePrmKey, @FinancialCyclePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@FinancialCyclePrmKey", _financialCyclePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(PeriodCodeViewModel _periodCodeViewModel)
        {
            try
            {
                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();

                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Dafault Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    //PeriodCodeMakerChecker
                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(PeriodCodeViewModel _periodCodeViewModel)
        {
            try
            {
                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();

                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    //viewModel.GeneralLedgerPrmKey = _periodCodeViewModel.GeneralLedgerPrmKey;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.FundContributionFrequencyPrmKey = fundContributionFrequencyRepository.GetPrmKeyById(viewModel.FundContributionFrequencyId);

                    //Mapping
                    PeriodCode periodCode = Mapper.Map<PeriodCode>(viewModel);
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    //PeriodCode
                    context.PeriodCodes.Attach(periodCode);
                    context.Entry(periodCode).State = EntityState.Added;

                    //PeriodCodeMakerChecker
                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
                    periodCode.PeriodCodeMakerCheckers.Add(periodCodeMakerChecker);

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

        public async Task<bool> Verify(PeriodCodeViewModel _periodCodeViewModel)
        {
            try
            {
                IEnumerable<PeriodCodeViewModel> periodCodeDetailViewModelList = await GetVerifiedEntries(_periodCodeViewModel.FinancialCyclePrmKey);

                foreach (PeriodCodeViewModel viewModel in periodCodeDetailViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
                }

                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();
                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerChecker);
                    context.Entry(periodCodeMakerChecker).State = EntityState.Added;
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
