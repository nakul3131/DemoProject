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
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Enterprise.Establishment;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFByLawsLoanScheduleParameterRepository : IByLawsLoanScheduleParameterRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFByLawsLoanScheduleParameterRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_byLawsLoanScheduleParameterViewModel, StringLiteralValue.Amend);

                // Mapping 
                // ByLawsLoanScheduleParameter
                ByLawsLoanScheduleParameter byLawsLoanScheduleParameter = Mapper.Map<ByLawsLoanScheduleParameter>(_byLawsLoanScheduleParameterViewModel);
                ByLawsLoanScheduleParameterMakerChecker byLawsLoanScheduleParameterMakerChecker = Mapper.Map<ByLawsLoanScheduleParameterMakerChecker>(_byLawsLoanScheduleParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                byLawsLoanScheduleParameter.PrmKey = _byLawsLoanScheduleParameterViewModel.ByLawsLoanScheduleParameterPrmKey;

                // ByLawsLoanScheduleParameterMakerChecker
                context.ByLawsLoanScheduleParameterMakerCheckers.Attach(byLawsLoanScheduleParameterMakerChecker);
                context.Entry(byLawsLoanScheduleParameterMakerChecker).State = EntityState.Added;
                byLawsLoanScheduleParameter.ByLawsLoanScheduleParameterMakerCheckers.Add(byLawsLoanScheduleParameterMakerChecker);

                // ByLawsLoanScheduleParameter
                context.ByLawsLoanScheduleParameters.Attach(byLawsLoanScheduleParameter);
                context.Entry(byLawsLoanScheduleParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public ByLawsLoanScheduleParameterViewModel ClearModelStateGetActiveEntry()
        {
            try
            {
                var tt = context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();
                return tt;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetActiveEntry()
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public ByLawsLoanScheduleParameterViewModel GetActiveEntry1()
        {
            try
            {
                var tt = context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();
                return tt;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetOldVerifyEntriesByLoanTypePrmKey(byte _loanTypePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@LoanTypePrmKey, @EntryType)", new SqlParameter("@LoanTypePrmKey", _loanTypePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetRejectedEntry(byte _loanTypePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@LoanTypePrmKey, @EntryType)", new SqlParameter("@LoanTypePrmKey", _loanTypePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetUnVerifiedEntry()
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<ByLawsLoanScheduleParameterViewModel> GetVerifiedEntry(Guid loanTypeId)
        {
            try
            {
                return await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@UserProfilePrmKey, @LoanTypePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@LoanTypePrmKey", loanTypeId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ByLawsLoanScheduleParameterViewModel>> GetIndexOfModify()
        {
            try
            {
                return await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.[GetOrganizationLoanTypeEntries] (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<bool> IsAnyAuthorizationPending(byte loanTypePrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.ByLawsLoanScheduleParameters
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject) && u.LoanTypePrmKey == loanTypePrmKey)
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

        public async Task<bool> Save(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_byLawsLoanScheduleParameterViewModel, StringLiteralValue.Create);

                // Mapping
                ByLawsLoanScheduleParameter byLawsLoanScheduleParameter = Mapper.Map<ByLawsLoanScheduleParameter>(_byLawsLoanScheduleParameterViewModel);
                ByLawsLoanScheduleParameterMakerChecker byLawsLoanScheduleParameterMakerChecker = Mapper.Map<ByLawsLoanScheduleParameterMakerChecker>(_byLawsLoanScheduleParameterViewModel);

                // ByLawsLoanScheduleParameterMakerChecker
                context.ByLawsLoanScheduleParameterMakerCheckers.Attach(byLawsLoanScheduleParameterMakerChecker);
                context.Entry(byLawsLoanScheduleParameterMakerChecker).State = EntityState.Added;
                byLawsLoanScheduleParameter.ByLawsLoanScheduleParameterMakerCheckers.Add(byLawsLoanScheduleParameterMakerChecker);
                //byLawsLoanScheduleParameter.LoanDisbursementMethod = "BNS";
                // ByLawsLoanScheduleParameter
                context.ByLawsLoanScheduleParameters.Attach(byLawsLoanScheduleParameter);
                context.Entry(byLawsLoanScheduleParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel, string _entryType)
        {
            try
            {
                //ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModelOfOldEntry = await GetActiveEntry();

                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject || _entryType == StringLiteralValue.Delete)
                {
                    ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModelOfOldEntry = await GetOldVerifyEntriesByLoanTypePrmKey(_byLawsLoanScheduleParameterViewModel.LoanTypePrmKey);


                    //Modify Old Records
                    if (byLawsLoanScheduleParameterViewModelOfOldEntry != null)
                    {
                        // Set Default Value
                        byLawsLoanScheduleParameterViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                        byLawsLoanScheduleParameterViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                        byLawsLoanScheduleParameterViewModelOfOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        // Mapping
                        ByLawsLoanScheduleParameterMakerChecker byLawsLoanScheduleParameterMakerCheckerForModify = Mapper.Map<ByLawsLoanScheduleParameterMakerChecker>(byLawsLoanScheduleParameterViewModelOfOldEntry);

                        // ByLawsLoanScheduleParameterMakerCheckers
                        context.ByLawsLoanScheduleParameterMakerCheckers.Attach(byLawsLoanScheduleParameterMakerCheckerForModify);
                        context.Entry(byLawsLoanScheduleParameterMakerCheckerForModify).State = EntityState.Added;
                    }

                    _byLawsLoanScheduleParameterViewModel.UserAction = _entryType;
                    _byLawsLoanScheduleParameterViewModel.EntryDateTime = DateTime.Now;
                    _byLawsLoanScheduleParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    ByLawsLoanScheduleParameterMakerChecker byLawsLoanScheduleParameterMakerChecker = Mapper.Map<ByLawsLoanScheduleParameterMakerChecker>(_byLawsLoanScheduleParameterViewModel);

                    // ByLawsLoanScheduleParameterMakerChecker
                    context.ByLawsLoanScheduleParameterMakerCheckers.Attach(byLawsLoanScheduleParameterMakerChecker);
                    context.Entry(byLawsLoanScheduleParameterMakerChecker).State = EntityState.Added;

                    await context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}

