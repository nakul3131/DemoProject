using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.Services.Wrapper;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Management.Conference
{
    public class EFMeetingInviteeMemberRepository : IMeetingInviteeMemberRepository
    {
        private readonly EFDbContext context;

        public EFMeetingInviteeMemberRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public int GetPrmKeyById(Guid _meetingId)
        {
            return context.Meetings
                    .Where(c => c.MeetingId == _meetingId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> MeetingInviteeMemberDropdownList 
        {
            get
            {
                var s = (from e in context.Agendas

                         select new SelectListItem
                         {
                             Value = e.AgendaId.ToString(),
                             Text = e.NameOfAgenda
                         }).ToList();
                return s;
            }
        }

        public async Task<IEnumerable<MeetingInviteeMemberViewModel>> GetRejectedEntries(int _meetingPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingInviteeMemberViewModel>("SELECT * FROM dbo.GetMeetingInviteeMemberEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingInviteeMemberViewModel>> GetUnverifiedEntries(int _meetingPrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<MeetingInviteeMemberViewModel>("SELECT * FROM dbo.GetMeetingInviteeMemberEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingInviteeMemberViewModel>> GetVerifiedEntries(int _meetingPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingInviteeMemberViewModel>("SELECT * FROM dbo.GetMeetingInviteeMemberEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
