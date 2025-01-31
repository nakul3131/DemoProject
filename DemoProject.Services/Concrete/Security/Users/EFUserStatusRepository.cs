using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserStatusRepository : IUserStatusRepository
    {
        private readonly EFDbContext context;

        public EFUserStatusRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
        public async Task<IEnumerable<UserStatusViewModel>> GetUserStatusIndex(string UserProfileStatus, DateTime EffectiveDate)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserStatusViewModel>("SELECT * FROM dbo.GetUserStatusList(@UserStatus, @EffectiveDate)", new SqlParameter("@UserStatus", UserProfileStatus), new SqlParameter("@EffectiveDate", EffectiveDate)).ToListAsync();
                return a;
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }
    }
}
