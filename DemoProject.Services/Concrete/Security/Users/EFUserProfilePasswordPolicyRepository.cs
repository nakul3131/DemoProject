using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfilePasswordPolicyRepository : IUserProfilePasswordPolicyRepository
    {
        private readonly EFDbContext context;

        public EFUserProfilePasswordPolicyRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<UserProfilePasswordPolicyViewModel> GetRejectedUserProfilePasswordPolicyEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfilePasswordPolicyViewModel>("SELECT * FROM dbo.GetUserProfilePasswordPolicyEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfilePasswordPolicyViewModel> GetUnverifiedUserProfilePasswordPolicyEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfilePasswordPolicyViewModel>("SELECT * FROM dbo.GetUserProfilePasswordPolicyEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfilePasswordPolicyViewModel> GetVerifiedUserProfilePasswordPolicyEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfilePasswordPolicyViewModel>("SELECT * FROM dbo.GetUserProfilePasswordPolicyEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
