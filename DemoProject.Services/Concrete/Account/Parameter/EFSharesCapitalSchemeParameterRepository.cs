using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Account.Parameter;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFSharesCapitalSchemeParameterRepository : ISharesCapitalSchemeParameterRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFSharesCapitalSchemeParameterRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeParameterViewModel, StringLiteralValue.Amend);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _sharesCapitalSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableEmailServiceParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableNoticeScheduleParameter = false;

                //Mapping
                SharesCapitalSchemeParameter sharesCapitalSchemeParameter = Mapper.Map<SharesCapitalSchemeParameter>(_sharesCapitalSchemeParameterViewModel);
                SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerChecker = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(_sharesCapitalSchemeParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                sharesCapitalSchemeParameter.PrmKey = _sharesCapitalSchemeParameterViewModel.SharesCapitalSchemeParameterPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                //SharesCapitalSchemeParameter
                context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerChecker);
                context.Entry(sharesCapitalSchemeParameterMakerChecker).State = EntityState.Added;
                sharesCapitalSchemeParameter.SharesCapitalSchemeParameterMakerCheckers.Add(sharesCapitalSchemeParameterMakerChecker);

                context.SharesCapitalSchemeParameters.Attach(sharesCapitalSchemeParameter);
                context.Entry(sharesCapitalSchemeParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeParameterViewModel, StringLiteralValue.Delete);

                //Mapping
                SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerChecker = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(_sharesCapitalSchemeParameterViewModel);

                //SharesCapitalSchemeParameter
                context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerChecker);
                context.Entry(sharesCapitalSchemeParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<SharesCapitalSchemeParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalSchemeParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<SharesCapitalSchemeParameterViewModel>> GetSharesCapitalSchemeParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalSchemeParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeParameterEntries (@UserProfilePrmKey,@EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalSchemeParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalSchemeParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeParameterEntry (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalSchemeParameterViewModel> GetUnverifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalSchemeParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalSchemeParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

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
            byte prmKey = await context.SharesCapitalSchemeParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).FirstOrDefaultAsync();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> IsAnySharesCapitalSchemeAuthorizationPending()
        {
            // Check Created, Amended and rejected entries count
            short prmKey = await (from s in context.Schemes
                            join sh in context.SchemeSharesCapitalAccountParameters.Where(sh => sh.EntryStatus == StringLiteralValue.Amend || sh.EntryStatus == StringLiteralValue.Create || sh.EntryStatus == StringLiteralValue.Reject) on s.PrmKey equals sh.SchemePrmKey into ssh
                            from sh in ssh.DefaultIfEmpty()
                            where (s.EntryStatus == StringLiteralValue.Amend || s.EntryStatus == StringLiteralValue.Create || sh.EntryStatus == StringLiteralValue.Reject)
                            select (s.PrmKey)).FirstOrDefaultAsync();
            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> Reject(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeParameterViewModel, StringLiteralValue.Reject);

                //Mapping
                SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerChecker = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(_sharesCapitalSchemeParameterViewModel);

                //SharesCapitalSchemeParameter
                context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerChecker);
                context.Entry(sharesCapitalSchemeParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeParameterViewModel, StringLiteralValue.Create);

                // Set Values Based On Visibility
                if (configurationDetailRepository.GetNumberOfBranches() < 1)
                    _sharesCapitalSchemeParameterViewModel.EnableBusinessOfficeParameter = false;

                if (configurationDetailRepository.HasCoreBankingFeature() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableBankingChannelParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableSmsServiceParameter = false;

                if (configurationDetailRepository.IsEnabledEmailService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableEmailServiceParameter = false;

                if (configurationDetailRepository.IsEnabledSmsService() == false && configurationDetailRepository.IsEnabledEmailService() == false)
                    _sharesCapitalSchemeParameterViewModel.EnableNoticeScheduleParameter = false;


                    //Mapping
                SharesCapitalSchemeParameter sharesCapitalSchemeParameter = Mapper.Map<SharesCapitalSchemeParameter>(_sharesCapitalSchemeParameterViewModel);
                SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerChecker = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(_sharesCapitalSchemeParameterViewModel);

                //SharesCapitalSchemeParameter
                context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerChecker);
                context.Entry(sharesCapitalSchemeParameterMakerChecker).State = EntityState.Added;
                sharesCapitalSchemeParameter.SharesCapitalSchemeParameterMakerCheckers.Add(sharesCapitalSchemeParameterMakerChecker);

                context.SharesCapitalSchemeParameters.Attach(sharesCapitalSchemeParameter);
                context.Entry(sharesCapitalSchemeParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModelOldEntry = await GetActiveEntry();

                if(sharesCapitalSchemeParameterViewModelOldEntry != null)
                {
                    if (sharesCapitalSchemeParameterViewModelOldEntry.PrmKey > 0)
                    {
                        // Set Default Value
                        configurationDetailRepository.SetDefaultValues(sharesCapitalSchemeParameterViewModelOldEntry, StringLiteralValue.Modify);

                        //Mapping
                        SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerCheckerForModify = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(sharesCapitalSchemeParameterViewModelOldEntry);

                        //SharesCapitalSchemeParameter
                        context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerCheckerForModify);
                        context.Entry(sharesCapitalSchemeParameterMakerCheckerForModify).State = EntityState.Added;

                    }
                }

                // Verify Record
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesCapitalSchemeParameterViewModel, StringLiteralValue.Verify);
 
                //Mapping
                SharesCapitalSchemeParameterMakerChecker sharesCapitalSchemeParameterMakerChecker = Mapper.Map<SharesCapitalSchemeParameterMakerChecker>(_sharesCapitalSchemeParameterViewModel);

                //SharesCapitalSchemeParameter
                context.SharesCapitalSchemeParameterMakerCheckers.Attach(sharesCapitalSchemeParameterMakerChecker);
                context.Entry(sharesCapitalSchemeParameterMakerChecker).State = EntityState.Added;

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
