using DemoProject.Services.Abstract.Master.General.Notice;
using DemoProject.Services.ViewModel.Master.General.Notice;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Master.General.Notice
{
    public class EFWeekMonthDayScheduleRepository : IWeekMonthDayScheduleRepository
    {
        private readonly EFDbContext context;
        
        public EFWeekMonthDayScheduleRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
        
        public async Task<IEnumerable<MonthScheduleViewModel>> GetMonthRejectedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<MonthScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfMonthEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "R")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MonthScheduleViewModel>> GetMonthUnverifiedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<MonthScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfMonthEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "U")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<MonthScheduleViewModel>> GetMonthVerifiedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<MonthScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfMonthEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "V")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<WeekScheduleViewModel>> GetWeekRejectedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<WeekScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfWeekEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "R")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<WeekScheduleViewModel>> GetWeekUnverifiedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<WeekScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfWeekEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "U")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<WeekScheduleViewModel>> GetWeekVerifiedEntries(short _noticeSchedulePrmkey)
        {
            try
            {
                return await context.Database.SqlQuery<WeekScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDaysOfWeekEntriesByNoticeSchedulePrmKey (@UserProfilePrmKey, @NoticeSchedulePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeSchedulePrmkey", _noticeSchedulePrmkey), new SqlParameter("EntriesType", "V")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DayScheduleViewModel>> GetDayRejectedEntries(short _noticeScheduleOnDatePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<DayScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDateTimeEntriesByNoticeScheduleOnDatePrmKey (@UserProfilePrmKey, @NoticeScheduleOnDatePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeScheduleOnDatePrmKey", _noticeScheduleOnDatePrmKey), new SqlParameter("EntriesType", "R")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DayScheduleViewModel>> GetDayUnverifiedEntries(short _noticeScheduleOnDatePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<DayScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDateTimeEntriesByNoticeScheduleOnDatePrmKey (@UserProfilePrmKey, @NoticeScheduleOnDatePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeScheduleOnDatePrmKey", _noticeScheduleOnDatePrmKey), new SqlParameter("EntriesType", "U")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DayScheduleViewModel>> GetDayVerifiedEntries(short _noticeScheduleOnDatePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<DayScheduleViewModel>("SELECT * FROM dbo.GetNoticeScheduleOnDateTimeEntriesByNoticeScheduleOnDatePrmKey (@UserProfilePrmKey, @NoticeScheduleOnDatePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@NoticeScheduleOnDatePrmKey", _noticeScheduleOnDatePrmKey), new SqlParameter("EntriesType", "V")).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

    }
}
