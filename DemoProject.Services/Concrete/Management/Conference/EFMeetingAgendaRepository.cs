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
    public class EFMeetingAgendaRepository : IMeetingAgendaRepository
    {
        private readonly EFDbContext context;

        public EFMeetingAgendaRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public int GetPrmKeyById(Guid _meetingAgendaId)
        {
            return context.MeetingAgendas
                    .Where(c => c.MeetingAgendaId == _meetingAgendaId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> MeetingAgendaDropdownList 
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from b in context.Agendas
                             join mf in context.AgendaModifications on b.PrmKey equals mf.AgendaPrmKey into bm
                             from mf in bm.DefaultIfEmpty()
                             join t in context.AgendaTranslations on b.PrmKey equals t.AgendaPrmKey into bt
                             from t in bt.DefaultIfEmpty()
                             join ma in context.MeetingAgendas on b.PrmKey equals ma.AgendaPrmKey into mat
                             from ma in mat.DefaultIfEmpty() 

                             where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                                     && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                     && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                     && (ma.EntryStatus.Equals(StringLiteralValue.Verify) || ma.EntryStatus.Equals(null))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     || (b.EntryStatus == StringLiteralValue.Verify)
                                     && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                     && (ma.EntryStatus.Equals(StringLiteralValue.Verify) || ma.EntryStatus.Equals(null))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     && (b.IsModified.Equals(false))
                             orderby b.NameOfAgenda
                             select new SelectListItem
                             {
                                 Value = ma.MeetingAgendaId.ToString(),
                                 Text = ((mf.NameOfAgenda.Equals(null)) ? b.NameOfAgenda.Trim() + " ---> " + (t.TransNameOfAgenda.Equals(null) ? " " : t.TransNameOfAgenda.Trim()) : mf.NameOfAgenda + " ---> " + (t.TransNameOfAgenda.Equals(null) ? " " : t.TransNameOfAgenda.Trim()))
                             }).ToList();
                    return a;
                }

                // Default List In Default Language (i.e. English)
                var aa= (from b in context.Agendas
                        join mf in context.AgendaModifications on b.PrmKey equals mf.AgendaPrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        join t in context.AgendaTranslations on b.PrmKey equals t.AgendaPrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        join ma in context.MeetingAgendas on b.PrmKey equals ma.AgendaPrmKey into mat
                        from ma in mat.DefaultIfEmpty()
                        where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                 && (ma.EntryStatus.Equals(StringLiteralValue.Verify) || ma.EntryStatus.Equals(null))
                                || (b.EntryStatus == StringLiteralValue.Verify)
                                && (b.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (ma.EntryStatus.Equals(StringLiteralValue.Verify) || ma.EntryStatus.Equals(null))
                         orderby b.NameOfAgenda
                        select new SelectListItem
                        {
                            Value = ma.MeetingAgendaId.ToString(),
                            Text = ((mf.NameOfAgenda.Equals(null)) ? b.NameOfAgenda.Trim() : mf.NameOfAgenda.Trim())
                        }).ToList();
                return aa;

                //var x= (from a in context.Agenda
                //        join m in context.MeetingAgendas on a.PrmKey equals m.AgendaPrmKey into mt
                //        from m in mt.DefaultIfEmpty()
                //        where ((a.EntryStatus == StringLiteralValue.Verify))

                //        select new SelectListItem
                //        {
                //            Value = m.MeetingAgendaId.ToString(),
                //            Text = (a.NameOfAgenda.Trim())
                //        }).ToList();

                //return x;
            }
        }

        public async Task<IEnumerable<MeetingAgendaViewModel>> GetRejectedEntries(int _meetingPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAgendaViewModel>("SELECT * FROM dbo.GetMeetingAgendaEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingAgendaViewModel>> GetUnverifiedEntries(int _meetingPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<MeetingAgendaViewModel>("SELECT * FROM dbo.GetMeetingAgendaEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MeetingAgendaViewModel>> GetVerifiedEntries(int _meetingPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<MeetingAgendaViewModel>("SELECT * FROM dbo.GetMeetingAgendaEntriesByMeetingPrmKey (@UserProfilePrmKey, @MeetingPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@MeetingPrmkey", _meetingPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
