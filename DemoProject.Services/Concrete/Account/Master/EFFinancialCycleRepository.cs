using AutoMapper;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Concrete.Account.Master
{
    public class EFFinancialCycleRepository : IFinancialCycleRepository
    {
        private readonly EFDbContext context;

        private readonly IPeriodCodeRepository periodCodeRepository;


        public EFFinancialCycleRepository(RepositoryConnection _connection, IPeriodCodeRepository _periodCodeRepository)
        {
            context = _connection.EFDbContext;

            periodCodeRepository = _periodCodeRepository;
        }

        public async Task<bool> Amend(FinancialCycleViewModel _financialCycleViewModel)
        {
            try
            {
                // Set Default Value
                _financialCycleViewModel.ActivationStatus = StringLiteralValue.Active;
                _financialCycleViewModel.EntryDateTime = DateTime.Now;
                _financialCycleViewModel.EntryStatus = StringLiteralValue.Amend;
                _financialCycleViewModel.Remark = "None";
                _financialCycleViewModel.UserAction = StringLiteralValue.Amend;
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // FinancialCycle
                FinancialCycle financialCycle = Mapper.Map<FinancialCycle>(_financialCycleViewModel);
                FinancialCycleMakerChecker financialCycleMakerChecker = Mapper.Map<FinancialCycleMakerChecker>(_financialCycleViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                financialCycle.PrmKey = _financialCycleViewModel.FinancialCyclePrmKey;

                // FinancialCycleMakerChecker
                context.FinancialCycleMakerCheckers.Attach(financialCycleMakerChecker);
                context.Entry(financialCycleMakerChecker).State = EntityState.Added;
                financialCycle.FinancialCycleMakerCheckers.Add(financialCycleMakerChecker);

                context.FinancialCycles.Attach(financialCycle);
                context.Entry(financialCycle).State = EntityState.Modified;

                // PeriodCodeViewModel - Amend Old Record
                IEnumerable<PeriodCodeViewModel> periodCodeViewModelForAmend = await periodCodeRepository.GetRejectedEntries(_financialCycleViewModel.FinancialCyclePrmKey);

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelForAmend)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    PeriodCodeMakerChecker periodCodeMakerCheckerForAmend = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    context.PeriodCodeMakerCheckers.Attach(periodCodeMakerCheckerForAmend);
                    context.Entry(periodCodeMakerCheckerForAmend).State = EntityState.Added;
                }

                // PeriodCodeViewModel - Add New Amended Entry, Get PeriodCodeViewModel Details From Session Object
                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();

                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    //viewModel.PeriodCodePrmKey = 0;
                    viewModel.FinancialCyclePrmKey = _financialCycleViewModel.FinancialCyclePrmKey;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _financialCycleViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    PeriodCode periodCode = Mapper.Map<PeriodCode>(viewModel);

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

        public async Task<bool> Delete(FinancialCycleViewModel _financialCycleViewModel)
        {
            try
            {
                // Set Default Value
                _financialCycleViewModel.EntryDateTime = DateTime.Now;
                _financialCycleViewModel.Remark = "None";
                _financialCycleViewModel.UserAction = StringLiteralValue.Delete;
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                FinancialCycleMakerChecker financialCycleMakerChecker = Mapper.Map<FinancialCycleMakerChecker>(_financialCycleViewModel);

                // FinancialCycleMakerChecker
                context.FinancialCycleMakerCheckers.Attach(financialCycleMakerChecker);
                context.Entry(financialCycleMakerChecker).State = EntityState.Added;

                // PeriodCode Data Table
                List<PeriodCodeViewModel> periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    // PeriodCode
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    // PeriodCodeMakerChecker
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

        public async Task<IEnumerable<FinancialCycleViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<FinancialCycleViewModel>("SELECT * FROM dbo.GetFinancialCycleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<FinancialCycleViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<FinancialCycleViewModel>("SELECT * FROM dbo.GetFinancialCycleEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<FinancialCycleViewModel> GetRejectedEntry(Guid _financialCycleId)
        {
            try
            {
                return await context.Database.SqlQuery<FinancialCycleViewModel>("SELECT * FROM dbo.GetFinancialCycleEntry (@FinancialCycleId, @EntriesType)", new SqlParameter("@FinancialCycleId", _financialCycleId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<FinancialCycleViewModel> GetUnVerifiedEntry(Guid _financialCycleId)
        {
            try
            {
                return await context.Database.SqlQuery<FinancialCycleViewModel>("SELECT * FROM dbo.GetFinancialCycleEntry (@FinancialCycleId, @EntriesType)", new SqlParameter("@FinancialCycleId", _financialCycleId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(FinancialCycleViewModel _financialCycleViewModel)
        {
            try
            {
                // Set Default Value
                _financialCycleViewModel.EntryDateTime = DateTime.Now;
                _financialCycleViewModel.UserAction = StringLiteralValue.Reject;
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // FinancialCycle
                FinancialCycleMakerChecker financialCycleMakerChecker = Mapper.Map<FinancialCycleMakerChecker>(_financialCycleViewModel);

                // FinancialCycleMakerChecker
                context.FinancialCycleMakerCheckers.Attach(financialCycleMakerChecker);
                context.Entry(financialCycleMakerChecker).State = EntityState.Added;
               
                List<PeriodCodeViewModel> periodCodeViewModelViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];


                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelViewModelList)
                {
                    // Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _financialCycleViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
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

        public async Task<bool> Save(FinancialCycleViewModel _financialCycleViewModel)
        {
            try
            {
                // Set Default Value
                _financialCycleViewModel.ActivationStatus = StringLiteralValue.Active;
                _financialCycleViewModel.EntryDateTime = DateTime.Now;
                _financialCycleViewModel.EntryStatus = StringLiteralValue.Create;
                _financialCycleViewModel.Remark = "None";
                _financialCycleViewModel.UserAction = StringLiteralValue.Create;
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // FinancialCycle
                FinancialCycle financialCycle = Mapper.Map<FinancialCycle>(_financialCycleViewModel);
                FinancialCycleMakerChecker financialCycleMakerChecker = Mapper.Map<FinancialCycleMakerChecker>(_financialCycleViewModel);

                // FinancialCycleMakerChecker
                context.FinancialCycleMakerCheckers.Attach(financialCycleMakerChecker);
                context.Entry(financialCycleMakerChecker).State = EntityState.Added;
                financialCycle.FinancialCycleMakerCheckers.Add(financialCycleMakerChecker);

                context.FinancialCycles.Attach(financialCycle);
                context.Entry(financialCycle).State = EntityState.Added;

                // PeriodCode Data Table
                List<PeriodCodeViewModel> periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];

                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.FinancialCyclePrmKey = _financialCycleViewModel.FinancialCyclePrmKey;
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    // PeriodCode
                    PeriodCode periodCode = Mapper.Map<PeriodCode>(viewModel);
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    // PeriodCodes
                    context.PeriodCodes.Attach(periodCode);
                    context.Entry(periodCode).State = EntityState.Added;

                    // PeriodCodeMakerChecker
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

        public async Task<bool> Verify(FinancialCycleViewModel _financialCycleViewModel)
        {
            try
            {
                // Set Default Value
                _financialCycleViewModel.EntryDateTime = DateTime.Now;
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _financialCycleViewModel.UserAction = StringLiteralValue.Verify;

                // Mapping
                // FinancialCycle
                FinancialCycleMakerChecker financialCycleMakerChecker = Mapper.Map<FinancialCycleMakerChecker>(_financialCycleViewModel);

                // FinancialCycleMakerChecker
                context.FinancialCycleMakerCheckers.Attach(financialCycleMakerChecker);
                context.Entry(financialCycleMakerChecker).State = EntityState.Added;

                // PeriodCode Data Table
                List<PeriodCodeViewModel> periodCodeViewModelList = new List<PeriodCodeViewModel>();
                periodCodeViewModelList = (List<PeriodCodeViewModel>)HttpContext.Current.Session["PeriodCode"];
                
                foreach (PeriodCodeViewModel viewModel in periodCodeViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.Remark = _financialCycleViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    // PeriodCode
                    PeriodCodeMakerChecker periodCodeMakerChecker = Mapper.Map<PeriodCodeMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    // PeriodCodeMakerChecker
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
