using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Domain.Entities.Account.Parameter;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFSharesCapitalByLawsParameterRepository : ISharesCapitalByLawsParameterRepository
    {
        private readonly EFDbContext context;

        public EFSharesCapitalByLawsParameterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameter)
        {
            try
            {
                // Set Default Value
                _sharesCapitalByLawsParameter.EntryDateTime = DateTime.Now;
                _sharesCapitalByLawsParameter.EntryStatus = StringLiteralValue.Amend;
                _sharesCapitalByLawsParameter.ReasonForModification = "None";
                _sharesCapitalByLawsParameter.UserAction = StringLiteralValue.Amend;
                _sharesCapitalByLawsParameter.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // SharesCapitalByLawsParameter
                SharesCapitalByLawsParameter sharesCapitalByLawsParameter = Mapper.Map<SharesCapitalByLawsParameter>(_sharesCapitalByLawsParameter);
                SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerChecker = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(_sharesCapitalByLawsParameter);

                // Set ReferenceKey As PrmKey To Every Object
                sharesCapitalByLawsParameter.PrmKey = _sharesCapitalByLawsParameter.SharesCapitalByLawsParameterPrmKey;

                // SharesCapitalByLawsParameter
                context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerChecker);
                context.Entry(sharesCapitalByLawsParameterMakerChecker).State = EntityState.Added;
                sharesCapitalByLawsParameter.SharesCapitalByLawsParameterMakerCheckers.Add(sharesCapitalByLawsParameterMakerChecker);

                context.SharesCapitalByLawsParameters.Attach(sharesCapitalByLawsParameter);
                context.Entry(sharesCapitalByLawsParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameter)
        {
            try
            {
                // Set Default Value
                _sharesCapitalByLawsParameter.EntryDateTime = DateTime.Now;
                _sharesCapitalByLawsParameter.UserAction = StringLiteralValue.Delete;
                _sharesCapitalByLawsParameter.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerChecker = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(_sharesCapitalByLawsParameter);

                // SharesCapitalByLawsParameter
                context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerChecker);
                context.Entry(sharesCapitalByLawsParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public SharesCapitalByLawsParameterViewModel GetActiveEntry()
        {
            try
            {
                return context.Database.SqlQuery<SharesCapitalByLawsParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalByLawsParameterEntry(@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesCapitalByLawsParameterViewModel>> GetSharesCapitalByLawsParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalByLawsParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalByLawsParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalByLawsParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalByLawsParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalByLawsParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalByLawsParameterViewModel> GetUnVerifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalByLawsParameterViewModel>("SELECT * FROM dbo.GetSharesCapitalByLawsParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            // check waiting for response and rejected entries count
            int count = await context.SharesCapitalByLawsParameters
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

        public async Task<bool> Reject(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameter)
        {
            try
            {
                // Set Default Value
                _sharesCapitalByLawsParameter.EntryDateTime = DateTime.Now;
                _sharesCapitalByLawsParameter.UserAction = StringLiteralValue.Reject;
                _sharesCapitalByLawsParameter.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerChecker = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(_sharesCapitalByLawsParameter);

                // SharesCapitalByLawsParameter
                context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerChecker);
                context.Entry(sharesCapitalByLawsParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameter)
        {
            try
            {
                // Set Default Value
                _sharesCapitalByLawsParameter.EntryDateTime = DateTime.Now;
                _sharesCapitalByLawsParameter.EntryStatus = StringLiteralValue.Create;
                _sharesCapitalByLawsParameter.Remark = "None";
                _sharesCapitalByLawsParameter.UserAction = StringLiteralValue.Create;
                _sharesCapitalByLawsParameter.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                SharesCapitalByLawsParameter sharesCapitalByLawsParameter = Mapper.Map<SharesCapitalByLawsParameter>(_sharesCapitalByLawsParameter);
                SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerChecker = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(_sharesCapitalByLawsParameter);

                // SharesCapitalByLawsParameter
                context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerChecker);
                context.Entry(sharesCapitalByLawsParameterMakerChecker).State = EntityState.Added;
                sharesCapitalByLawsParameter.SharesCapitalByLawsParameterMakerCheckers.Add(sharesCapitalByLawsParameterMakerChecker);

                context.SharesCapitalByLawsParameters.Attach(sharesCapitalByLawsParameter);
                context.Entry(sharesCapitalByLawsParameter).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameter)
        {
            try
            {
                // Modify Old Record      
                SharesCapitalByLawsParameterViewModel SharesCapitalByLawsParameterViewModelOfOldEntry =  GetActiveEntry();

                if (SharesCapitalByLawsParameterViewModelOfOldEntry.PrmKey > 0)
                {
                    // Set Default Value
                    SharesCapitalByLawsParameterViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                    SharesCapitalByLawsParameterViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                    SharesCapitalByLawsParameterViewModelOfOldEntry.UserProfilePrmKey = _sharesCapitalByLawsParameter.UserProfilePrmKey;

                    // Mapping
                    SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerCheckerForModify = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(SharesCapitalByLawsParameterViewModelOfOldEntry);

                    //SharesCapitalByLawsParameter
                    context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerCheckerForModify);
                    context.Entry(sharesCapitalByLawsParameterMakerCheckerForModify).State = EntityState.Added;
                }

                // Verify Record
                // Set Default Value
                _sharesCapitalByLawsParameter.EntryDateTime = DateTime.Now;
                _sharesCapitalByLawsParameter.UserAction = StringLiteralValue.Verify;
                _sharesCapitalByLawsParameter.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                SharesCapitalByLawsParameterMakerChecker sharesCapitalByLawsParameterMakerChecker = Mapper.Map<SharesCapitalByLawsParameterMakerChecker>(_sharesCapitalByLawsParameter);

                // SharesCapitalByLawsParameter
                context.SharesCapitalByLawsParameterMakerCheckers.Attach(sharesCapitalByLawsParameterMakerChecker);
                context.Entry(sharesCapitalByLawsParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public decimal GetSharesFaceValueAmount()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == EntryStatus.Verified)
                            .Select(s => s.SharesFaceValue).FirstOrDefault();
            return a;
        }

        public decimal AdmissionFeesForMembershipAmount()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == EntryStatus.Verified)
                            .Select(s => s.AdmissionFeesForMembership).FirstOrDefault();
            return a;
        }


        public decimal AdmissionFeesForNominalMember()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == EntryStatus.Verified)
                            .Select(s => s.AdmissionFeesForNominalMember).FirstOrDefault();
            return a;
        }


        public int MinimumNumberOfSharesHoldingForActiveMember()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == EntryStatus.Verified)
                            .Select(s => s.MinimumNumberOfSharesHoldingForActiveMember).FirstOrDefault();
            return a;
        }

        public decimal GetMaximumSharesHoldingLimitPercentage()
        {
            var a = context.SharesCapitalByLawsParameters
                            .Where(s => s.EntryStatus == EntryStatus.Verified)
                            .Select(s => s.MaximumSharesHolidingLimitPercentage).FirstOrDefault();
            return a;
        }
    }
}
