using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.UserRoles
{
    public class EFRoleProfileMenuRepository : IRoleProfileMenuRepository
    {
        private readonly EFDbContext context;

        public EFRoleProfileMenuRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<RoleProfileMenuViewModel>> GetRejectedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileMenuViewModel>("SELECT * FROM dbo.GetRoleProfileMenuEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileMenuViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileMenuViewModel>("SELECT * FROM dbo.GetRoleProfileMenuEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileMenuViewModel>> GetVerifiedEntries(short _roleProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileMenuViewModel>("SELECT * FROM dbo.GetRoleProfileMenuEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
