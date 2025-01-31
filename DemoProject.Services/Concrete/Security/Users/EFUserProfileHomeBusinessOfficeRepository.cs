using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileHomeBusinessOfficeRepository : IUserProfileHomeBusinessOfficeRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileHomeBusinessOfficeRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<UserProfileHomeBusinessOfficeViewModel> GetRejectedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileHomeBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileHomeBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileHomeBusinessOfficeViewModel> GetUnVerifiedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileHomeBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileHomeBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileHomeBusinessOfficeViewModel> GetVerifiedEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileHomeBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileHomeBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
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
