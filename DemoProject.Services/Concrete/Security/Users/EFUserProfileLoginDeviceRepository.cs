using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileLoginDeviceRepository : IUserProfileLoginDeviceRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileLoginDeviceRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetRejectedUserProfileLoginDeviceEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileLoginDeviceViewModel>("SELECT * FROM dbo.GetUserProfileLoginDeviceEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetUnverifiedUserProfileLoginDeviceEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileLoginDeviceViewModel>("SELECT * FROM dbo.GetUserProfileLoginDeviceEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetVerifiedUserProfileLoginDeviceEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileLoginDeviceViewModel>("SELECT * FROM dbo.GetUserProfileLoginDeviceEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
