using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Management
{
    public interface IManagementDetailRepository
    {
        short GetBoardOfDirectorPrmKeyById(Guid _BoardOfDirectorId);

        byte GetCommunicationMediaPrmKeyById(Guid _communicationMediaId);

        byte GetDaysOfMonthPrmKeyById(Guid _daysOfMonthId);

        byte GetDaysOfWeekPrmKeyById(Guid _daysOfWeekId);

        short GetDepartmentPrmKeyById(Guid _departmentId);

        short GetDesignationPrmKeyById(Guid _designationId);

        int GetEmployeePrmKeyById(Guid _employeeId);

        byte GetEmployeeTypePrmKeyById(Guid _employeeTypeId);

        byte GetEmployerNaturePrmKeyById(Guid _employerNatureId);

        byte GetEmploymentTypePrmKeyById(Guid _employmentTypeId);

        byte GetMeetingTypePrmKeyById(Guid _meetingTypeId);

        short GetEventTypePrmKeyById(Guid _eventTypeId);

        short GetNoticeMediaPrmKeyById(Guid _noticeMediaId);

        short GetNoticeTypePrmKeyById(Guid _noticeTypeId);

        short GetSalaryBreakupPrmKeyById(Guid _salaryBreakupId);

        short GetSchedulePrmKeyById(Guid _scheduleId);

        byte GetScheduleTypePrmKeyById(Guid _scheduleTypeId);

        short GetSocialMediaPrmKeyById(Guid _socialMediaId);

        byte GetWorkingSchedulePrmKeyById(Guid _workingScheduleId);

        List<SelectListItem> BoardOfDirectorDropdownList { get; }

        List<SelectListItem> BoardOfDirectorDesignationDropdownList { get; }

        List<SelectListItem> CommunicationMediaDropdownList { get; }

        List<SelectListItem> CustomerSharesCapitalAccountDropdownList { get; }

        List<SelectListItem> DaysOfMonthDropdownList { get; }

        List<SelectListItem> DaysOfWeekDropdownList { get; }

        List<SelectListItem> DepartmentDropdownList { get; }

        List<SelectListItem> EmployeeDropdownList { get; }

        List<SelectListItem> EmployeeTypeDropdownList { get; }

        List<SelectListItem> InvestigationOfficerDropdownList { get; }

        List<SelectListItem> EmployeeDesignationDropdownList { get; }

        List<SelectListItem> EmployerNatureDropdownList { get; }

        List<SelectListItem> EmploymentTypeDropdownList { get; }

        List<SelectListItem> EventTypeDropdownList { get; }

        List<SelectListItem> MeetingTypeDropdownList { get; }

        List<SelectListItem> NoticeMediaDropdownList { get; }

        List<SelectListItem> NoticeTypeDropdownList { get; }

        List<SelectListItem> SharesNoticeTypeDropdownList { get; }

        List<SelectListItem> SalaryBreakupDropdownList { get; }

        List<SelectListItem> ScheduleTypeDropdownList { get; }

        List<SelectListItem> SocialMediaDropdownList { get; }
    }
}
