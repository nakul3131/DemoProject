using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace DemoProject.Services.Concrete.Management.Conference
{
    public class EFMinuteOfMeetingAgendaSpokespersonRepository : IMinuteOfMeetingAgendaSpokespersonRepository
    {
        private readonly EFDbContext context;

        public EFMinuteOfMeetingAgendaSpokespersonRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public List<SelectListItem> MeetingAgendaDropdownList
        {
            get
            {
                return (from e in context.MeetingAgendas

                        select new SelectListItem
                        {
                            Value = e.MeetingAgendaId.ToString(),
                        }).ToList();
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetRejectedEntries(int _meetingAgendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaSpokespersonViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingSpokespersonEntriesByMeetingAgendaPrmKey (@UserProfilePrmKey, @MeetingAgendaPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingAgendaPrmKey", _meetingAgendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetUnverifiedEntries(int _meetingAgendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaSpokespersonViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingSpokespersonEntriesByMeetingAgendaPrmKey (@UserProfilePrmKey, @MeetingAgendaPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingAgendaPrmKey", _meetingAgendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MinuteOfMeetingAgendaSpokespersonViewModel>> GetVerifiedEntries(int _meetingAgendaPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MinuteOfMeetingAgendaSpokespersonViewModel>("SELECT * FROM dbo.GetMinuteOfMeetingSpokespersonEntriesByMeetingAgendaPrmKey (@UserProfilePrmKey, @MeetingAgendaPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingAgendaPrmKey", _meetingAgendaPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
