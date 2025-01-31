using DemoProject.Services.ViewModel.Master.General.Notice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Master
{
    public interface INoticeScheduleRepository
    {
        short GetPrmKeyById(Guid _NoticeScheduleId);

        List<SelectListItem> NoticeScheduleDropdownList { get; }

        // Amend NoticeSchedule Delete Entry - If Entry Rejected
        Task<bool> Amend(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Amend NoticeSchedule Modification Entry - If Entry Rejected
        Task<bool> AmendModification(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Delete NoticeSchedule - Only For Rejected Entry
        Task<bool> Delete(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Delete NoticeSchedule Modification Entry - Only For Rejected Entry
        Task<bool> DeleteModification(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Return Rejected Entries
        Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From NoticeSchedule Table Which Are Not Authorized
        Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From NoticeSchedule Table For Modification
        Task<IEnumerable<NoticeScheduleViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<NoticeScheduleViewModel> GetRejectedEntry(Guid _noticeScheduleId);

        // Return Record From NoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<NoticeScheduleViewModel> GetUnVerifiedEntry(Guid _noticeScheduleId);

        // Return Record From NoticeSchedule Table By Given Parameter (i.e. NoticeScheduleId)
        Task<NoticeScheduleViewModel> GetVerifiedEntry(Guid _noticeScheduleId);

        // Reject NoticeSchedule Entry
        Task<bool> Reject(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Reject NoticeSchedule Modification Entry
        Task<bool> RejectModification(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Save NoticeSchedule New Entry
        Task<bool> Save(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Save NoticeSchedule Modification New Entry
        Task<bool> SaveModification(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Authorize NoticeSchedule Entry
        Task<bool> Verify(NoticeScheduleViewModel _noticeScheduleViewModel);

        // Authorize NoticeSchedule Modification Entry
        Task<bool> VerifyModification(NoticeScheduleViewModel _noticeScheduleViewModel);
    }
}
