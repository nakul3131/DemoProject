using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFDepositSchemeParameterRepository : IDepositSchemeParameterRepository
    {
        private readonly EFDbContext context;

        private readonly IConfigurationDetailRepository configurationDetailRepository;
        public EFDepositSchemeParameterRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value               
                configurationDetailRepository.SetDefaultValues(_depositSchemeParameterViewModel, StringLiteralValue.Amend);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _depositSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _depositSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _depositSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _depositSchemeParameterViewModel.EnableEmailServiceParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _depositSchemeParameterViewModel.EnableNoticeScheduleParameter = false;


                //Mapping
                DepositSchemeParameter depositSchemeParameter = Mapper.Map<DepositSchemeParameter>(_depositSchemeParameterViewModel);
                DepositSchemeParameterMakerChecker depositSchemeParameterMakerChecker = Mapper.Map<DepositSchemeParameterMakerChecker>(_depositSchemeParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                depositSchemeParameter.PrmKey = _depositSchemeParameterViewModel.DepositSchemeParameterPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                //DepositSchemeParameter
                context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerChecker);
                context.Entry(depositSchemeParameterMakerChecker).State = EntityState.Added;
                depositSchemeParameter.DepositSchemeParameterMakerCheckers.Add(depositSchemeParameterMakerChecker);

                context.DepositSchemeParameters.Attach(depositSchemeParameter);
                context.Entry(depositSchemeParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_depositSchemeParameterViewModel, StringLiteralValue.Delete);

                //Mapping
                DepositSchemeParameterMakerChecker depositSchemeParameterMakerChecker = Mapper.Map<DepositSchemeParameterMakerChecker>(_depositSchemeParameterViewModel);

                //DepositSchemeParameter
                context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerChecker);
                context.Entry(depositSchemeParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<DepositSchemeParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<DepositSchemeParameterViewModel>("SELECT * FROM dbo.GetDepositSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<DepositSchemeParameterViewModel>> GetDepositSchemeParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<DepositSchemeParameterViewModel>("SELECT * FROM dbo.GetDepositSchemeParameterEntries (@UserProfilePrmKey,@EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DepositSchemeParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<DepositSchemeParameterViewModel>("SELECT * FROM dbo.GetDepositSchemeParameterEntry (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<DepositSchemeParameterViewModel> GetUnverifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<DepositSchemeParameterViewModel>("SELECT * FROM dbo.GetDepositSchemeParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            // Check Created, Amended and rejected entries count
            int prmKey = await context.DepositSchemeParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).FirstOrDefaultAsync();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> IsAnyDepositSchemeAuthorizationPending()
        {
            // Check Created, Amended and rejected entries count
            short prmKey = await (from s in context.Schemes
                                  join d in context.SchemeDepositAccountParameters .Where(d => d.EntryStatus == StringLiteralValue.Amend || d.EntryStatus == StringLiteralValue.Create || d.EntryStatus == StringLiteralValue.Reject) on s.PrmKey equals d.SchemePrmKey into sd
                                  from d in sd.DefaultIfEmpty()
                                  where (s.EntryStatus == StringLiteralValue.Amend || s.EntryStatus == StringLiteralValue.Create || d.EntryStatus == StringLiteralValue.Reject)
                                  select (s.PrmKey)).FirstOrDefaultAsync();
            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> Reject(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_depositSchemeParameterViewModel, StringLiteralValue.Reject);

                //Mapping
                DepositSchemeParameterMakerChecker depositSchemeParameterMakerChecker = Mapper.Map<DepositSchemeParameterMakerChecker>(_depositSchemeParameterViewModel);

                //DepositSchemeParameter
                context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerChecker);
                context.Entry(depositSchemeParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_depositSchemeParameterViewModel, StringLiteralValue.Create);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _depositSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _depositSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _depositSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _depositSchemeParameterViewModel.EnableEmailServiceParameter = false;

                 if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _depositSchemeParameterViewModel.EnableNoticeScheduleParameter = false;

                    //Mapping
                    DepositSchemeParameter depositSchemeParameter = Mapper.Map<DepositSchemeParameter>(_depositSchemeParameterViewModel);
                DepositSchemeParameterMakerChecker depositSchemeParameterMakerChecker = Mapper.Map<DepositSchemeParameterMakerChecker>(_depositSchemeParameterViewModel);

                //DepositSchemeParameter
                context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerChecker);
                context.Entry(depositSchemeParameterMakerChecker).State = EntityState.Added;
                depositSchemeParameter.DepositSchemeParameterMakerCheckers.Add(depositSchemeParameterMakerChecker);

                context.DepositSchemeParameters.Attach(depositSchemeParameter);
                context.Entry(depositSchemeParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                DepositSchemeParameterViewModel depositSchemeParameterViewModelOldEntry = await GetActiveEntry();

                if(depositSchemeParameterViewModelOldEntry != null)
                {
                    if (depositSchemeParameterViewModelOldEntry.PrmKey > 0)
                    {
                        // Set Default Value
                        configurationDetailRepository.SetDefaultValues(depositSchemeParameterViewModelOldEntry, StringLiteralValue.Modify);

                        //Mapping
                        DepositSchemeParameterMakerChecker depositSchemeParameterMakerCheckerForModify = Mapper.Map<DepositSchemeParameterMakerChecker>(depositSchemeParameterViewModelOldEntry);

                        // DepositSchemeParameterMakerChecker
                        context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerCheckerForModify);
                        context.Entry(depositSchemeParameterMakerCheckerForModify).State = EntityState.Added;
                    }
                }

                // Verify Record
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_depositSchemeParameterViewModel, StringLiteralValue.Verify);

                //Mapping
                DepositSchemeParameterMakerChecker depositSchemeParameterMakerChecker = Mapper.Map<DepositSchemeParameterMakerChecker>(_depositSchemeParameterViewModel);

                // DepositSchemeParameterMakerChecker
                context.DepositSchemeParameterMakerCheckers.Attach(depositSchemeParameterMakerChecker);
                context.Entry(depositSchemeParameterMakerChecker).State = EntityState.Added;

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
