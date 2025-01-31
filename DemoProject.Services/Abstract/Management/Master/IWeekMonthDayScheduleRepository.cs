using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Master.General.Notice;

namespace DemoProject.Services.Abstract.Master.General.Notice
{
    public interface IWeekMonthDayScheduleRepository
    {
        // Return Record From MonthNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<MonthScheduleViewModel>> GetMonthRejectedEntries(short _noticeSchedulePrmkey);

        // Return Record From MonthNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<MonthScheduleViewModel>> GetMonthUnverifiedEntries(short _noticeSchedulePrmkey);

        // Return Record From MonthNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<MonthScheduleViewModel>> GetMonthVerifiedEntries(short _noticeSchedulePrmkey);


        // Return Record From WeekNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<WeekScheduleViewModel>> GetWeekRejectedEntries(short _noticeSchedulePrmkey);

        // Return Record From WeekNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<WeekScheduleViewModel>> GetWeekUnverifiedEntries(short _noticeSchedulePrmkey);

        // Return Record From WeekNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<WeekScheduleViewModel>> GetWeekVerifiedEntries(short _noticeSchedulePrmkey);


        // Return Record From DayNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<DayScheduleViewModel>> GetDayRejectedEntries(short _noticeScheduleOnDatePrmKey);

        // Return Record From DayNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<DayScheduleViewModel>> GetDayUnverifiedEntries(short _noticeScheduleOnDatePrmKey);

        // Return Record From DayNoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<IEnumerable<DayScheduleViewModel>> GetDayVerifiedEntries(short _noticeScheduleOnDatePrmKey);

    }
}
