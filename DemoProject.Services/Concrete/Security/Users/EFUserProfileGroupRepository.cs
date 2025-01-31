using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileGroupRepository : IUserProfileGroupRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileGroupRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<UserProfileGroupViewModel> GetRejectedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileGroupViewModel>("SELECT * FROM dbo.GetUserProfileGroupEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileGroupViewModel> GetUnVerifiedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileGroupViewModel>("SELECT * FROM dbo.GetUserProfileGroupEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileGroupViewModel> GetVerifiedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileGroupViewModel>("SELECT * FROM dbo.GetUserProfileGroupEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
