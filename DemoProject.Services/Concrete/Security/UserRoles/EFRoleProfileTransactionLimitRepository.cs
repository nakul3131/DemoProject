using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.UserRoles
{
    public class EFRoleProfileTransactionLimitRepository : IRoleProfileTransactionLimitRepository
    {

        private readonly EFDbContext context;

        public EFRoleProfileTransactionLimitRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetRejectedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileTransactionLimitViewModel>("SELECT * FROM dbo.GetRoleProfileTransactionLimitEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileTransactionLimitViewModel>("SELECT * FROM dbo.GetRoleProfileTransactionLimitEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetVerifiedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileTransactionLimitViewModel>("SELECT * FROM dbo.GetRoleProfileTransactionLimitEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public short GetPastDaysPermissionForTransaction(short _roleProfilePrmKey)
        {
            return context.RoleProfileTransactionLimits
                    .Where(l => l.RoleProfilePrmKey.Equals(_roleProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumNumberOfBackDaysForTransaction).FirstOrDefault();
        }

        public short GetPastDaysPermissionForVerification(short _roleProfilePrmKey)
        {
            return context.RoleProfileTransactionLimits
                    .Where(l => l.RoleProfilePrmKey.Equals(_roleProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumNumberOfBackDaysForVerification).FirstOrDefault();
        }

        public short GetPastDaysPermissionForAutoVerification(short _roleProfilePrmKey)
        {
            return context.RoleProfileTransactionLimits
                    .Where(l => l.RoleProfilePrmKey.Equals(_roleProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumNumberOfBackDaysForAutoVerification).FirstOrDefault();
        }

    }
}
