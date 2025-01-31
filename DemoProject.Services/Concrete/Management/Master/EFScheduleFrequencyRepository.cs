using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFScheduleFrequencyRepository : IScheduleFrequencyRepository
    {
        private readonly EFDbContext context;

        public EFScheduleFrequencyRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public short GetPrmKeyById(Guid _scheduleFrequencyId)
        {
            return context.ScheduleFrequencies
                    .Where(c => c.ScheduleFrequencyId == _scheduleFrequencyId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<IEnumerable<ScheduleFrequencyViewModel>> GetRejectedEntries(short _schedulePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleFrequencyViewModel>("SELECT * FROM dbo.GetScheduleFrequencyEntriesBySchedulePrmKey (@UserProfilePrmKey, @SchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SchedulePrmkey", _schedulePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ScheduleFrequencyViewModel>> GetUnverifiedEntries(short _schedulePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleFrequencyViewModel>("SELECT * FROM dbo.GetScheduleFrequencyEntriesBySchedulePrmKey (@UserProfilePrmKey, @SchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SchedulePrmkey", _schedulePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<ScheduleFrequencyViewModel>> GetVerifiedEntries(short _schedulePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<ScheduleFrequencyViewModel>("SELECT * FROM dbo.GetScheduleFrequencyEntriesBySchedulePrmKey (@UserProfilePrmKey, @SchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SchedulePrmkey", _schedulePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
