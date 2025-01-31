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
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFLoanSchemeParameterRepository : ILoanSchemeParameterRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;


        public EFLoanSchemeParameterRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_loanSchemeParameterViewModel, StringLiteralValue.Amend);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _loanSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _loanSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _loanSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _loanSchemeParameterViewModel.EnableEmailServiceParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _loanSchemeParameterViewModel.EnableNoticeScheduleParameter = false;

                // Mapping 
                // LoanSchemeParameter
                LoanSchemeParameter loanSchemeParameter = Mapper.Map<LoanSchemeParameter>(_loanSchemeParameterViewModel);
                LoanSchemeParameterMakerChecker loanSchemeParameterMakerChecker = Mapper.Map<LoanSchemeParameterMakerChecker>(_loanSchemeParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                loanSchemeParameter.PrmKey = _loanSchemeParameterViewModel.LoanSchemeParameterPrmKey;

                // LoanSchemeParameterMakerChecker
                context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerChecker);
                context.Entry(loanSchemeParameterMakerChecker).State = EntityState.Added;
                loanSchemeParameter.LoanSchemeParameterMakerCheckers.Add(loanSchemeParameterMakerChecker);

                // LoanSchemeParameter
                context.LoanSchemeParameters.Attach(loanSchemeParameter);
                context.Entry(loanSchemeParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                _loanSchemeParameterViewModel.EntryDateTime = DateTime.Now;
                _loanSchemeParameterViewModel.UserAction = StringLiteralValue.Delete;
                _loanSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                LoanSchemeParameterMakerChecker loanSchemeParameterMakerChecker = Mapper.Map<LoanSchemeParameterMakerChecker>(_loanSchemeParameterViewModel);

                // DepositSchemeParameterMakerChecker
                context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerChecker);
                context.Entry(loanSchemeParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<LoanSchemeParameterViewModel> GetActiveEntry()
        {
            try
            {
                var a = await context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public LoanSchemeParameterViewModel GetActiveEntry1()
        {
            try
            {
                var tt = context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();
                return tt;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public LoanSchemeParameterViewModel ClearModelStateGetActiveEntry()
        {
            try
            {
                var tt = context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();
                return tt;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<LoanSchemeParameterViewModel>> GetLoanSchemeParameterIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<LoanSchemeParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<LoanSchemeParameterViewModel> GetUnVerifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<LoanSchemeParameterViewModel>("SELECT * FROM dbo.GetLoanSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            //check waiting for response and rejected entries count
            int count = await context.LoanSchemeParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                _loanSchemeParameterViewModel.EntryDateTime = DateTime.Now;
                _loanSchemeParameterViewModel.UserAction = StringLiteralValue.Reject;
                _loanSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                LoanSchemeParameterMakerChecker loanSchemeParameterMakerChecker = Mapper.Map<LoanSchemeParameterMakerChecker>(_loanSchemeParameterViewModel);

                // LoanSchemeParameterMakerChecker
                context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerChecker);
                context.Entry(loanSchemeParameterMakerChecker).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_loanSchemeParameterViewModel, StringLiteralValue.Create);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _loanSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _loanSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _loanSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _loanSchemeParameterViewModel.EnableEmailServiceParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _loanSchemeParameterViewModel.EnableNoticeScheduleParameter = false;

                // Mapping
                LoanSchemeParameter loanSchemeParameter = Mapper.Map<LoanSchemeParameter>(_loanSchemeParameterViewModel);
                LoanSchemeParameterMakerChecker loanSchemeParameterMakerChecker = Mapper.Map<LoanSchemeParameterMakerChecker>(_loanSchemeParameterViewModel);

                // LoanSchemeParameterMakerChecker
                context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerChecker);
                context.Entry(loanSchemeParameterMakerChecker).State = EntityState.Added;
                loanSchemeParameter.LoanSchemeParameterMakerCheckers.Add(loanSchemeParameterMakerChecker);

                // LoanSchemeParameter
                context.LoanSchemeParameters.Attach(loanSchemeParameter);
                context.Entry(loanSchemeParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            try
            {
                // Modify Old Record      
                LoanSchemeParameterViewModel loanSchemeParameterViewModelOfOldEntry = await GetActiveEntry();

                if (loanSchemeParameterViewModelOfOldEntry != null)
                {
                    // Set Default Value
                    loanSchemeParameterViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                    loanSchemeParameterViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                    loanSchemeParameterViewModelOfOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    LoanSchemeParameterMakerChecker loanSchemeParameterMakerCheckerForModify = Mapper.Map<LoanSchemeParameterMakerChecker>(loanSchemeParameterViewModelOfOldEntry);

                    // LoanSchemeParameterMakerCheckers
                    context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerCheckerForModify);
                    context.Entry(loanSchemeParameterMakerCheckerForModify).State = EntityState.Added;
                }

                // Verify Record
                // Set Default Value
                _loanSchemeParameterViewModel.EntryDateTime = DateTime.Now;
                _loanSchemeParameterViewModel.UserAction = StringLiteralValue.Verify;
                _loanSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                LoanSchemeParameterMakerChecker loanSchemeParameterMakerChecker = Mapper.Map<LoanSchemeParameterMakerChecker>(_loanSchemeParameterViewModel);

                // LoanSchemeParameterMakerChecker
                context.LoanSchemeParameterMakerCheckers.Attach(loanSchemeParameterMakerChecker);
                context.Entry(loanSchemeParameterMakerChecker).State = EntityState.Added;

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
