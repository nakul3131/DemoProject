using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management
{
    public class EFManagementDetailRepository : IManagementDetailRepository
    {
        private readonly EFDbContext context;

        public EFManagementDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public short GetBoardOfDirectorPrmKeyById(Guid _BoardOfDirectorId)
        {
            return context.BoardOfDirectors
                    .Where(c => c.BoardOfDirectorId == _BoardOfDirectorId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetCommunicationMediaPrmKeyById(Guid _communicationMediaId)
        {
            var tt = context.CommunicationMedias
                    .Where(c => c.CommunicationMediaId == _communicationMediaId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return tt;
        }

        public byte GetDaysOfMonthPrmKeyById(Guid _daysOfMonthId)
        {
            return context.DaysOfMonths
                    .Where(c => c.DaysOfMonthId == _daysOfMonthId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetDaysOfWeekPrmKeyById(Guid _daysOfWeekId)
        {
            return context.DaysOfWeeks
                    .Where(c => c.DaysOfWeekId == _daysOfWeekId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDepartmentPrmKeyById(Guid _departmentId)
        {
            return context.Departments
                    .Where(c => c.DepartmentId == _departmentId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDesignationPrmKeyById(Guid _designationId)
        {
            return context.Designations
                    .Where(c => c.DesignationId == _designationId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public int GetEmployeePrmKeyById(Guid _employeeId)
        {
            return context.Employees
                    .Where(c => c.EmployeeId == _employeeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetEmployeeTypePrmKeyById(Guid _employeeTypeId)
        {
            return context.EmployeeTypes
                    .Where(c => c.EmployeeTypeId == _employeeTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetEmployerNaturePrmKeyById(Guid _employerNatureId)
        {
            return context.EmployerNatures
                    .Where(n => n.EmployerNatureId == _employerNatureId)
                    .Select(n => n.PrmKey).FirstOrDefault();
        }

        public byte GetEmploymentTypePrmKeyById(Guid _employmentTypeId)
        {
            return context.EmploymentTypes
                    .Where(e => e.EmploymentTypeId == _employmentTypeId)
                    .Select(e => e.PrmKey).FirstOrDefault();
        }

        public short GetEventTypePrmKeyById(Guid _eventTypeId)
        {
            return context.EventTypes
                    .Where(c => c.EventTypeId == _eventTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetMeetingTypePrmKeyById(Guid _meetingTypeId)
        {
            return context.MeetingTypes
                    .Where(c => c.MeetingTypeId == _meetingTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetNoticeMediaPrmKeyById(Guid _noticeMediaId)
        {
            var a = context.NoticeMedias
                    .Where(c => c.NoticeMediaId == _noticeMediaId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }

        public short GetNoticeTypePrmKeyById(Guid _noticeTypeId)
        {
            var yy = context.NoticeTypes
                    .Where(c => c.NoticeTypeId == _noticeTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return yy;
        }

        public short GetSalaryBreakupPrmKeyById(Guid _salaryBreakupId)
        {
            return context.SalaryBreakups
                    .Where(c => c.SalaryBreakupId == _salaryBreakupId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetSchedulePrmKeyById(Guid _scheduleId)
        {
            var tt = context.Schedules
                    .Where(c => c.ScheduleId == _scheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return tt;
        }

        public byte GetScheduleTypePrmKeyById(Guid _scheduleTypeId)
        {
            var tt = context.ScheduleTypes
                    .Where(c => c.ScheduleTypeId == _scheduleTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return tt;
        }

        public short GetSocialMediaPrmKeyById(Guid _socialMediaId)
        {
            return context.SocialMedias
                    .Where(d => d.SocialMediaId == _socialMediaId)
                    .Select(d => d.PrmKey).FirstOrDefault();
        }

        public byte GetWorkingSchedulePrmKeyById(Guid _workingScheduleId)
        {
            return context.WorkingSchedules
                    .Where(c => c.WorkingScheduleId == _workingScheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> BoardOfDirectorDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {

                    return (from b in context.BoardOfDirectors
                            join c in context.CustomerAccountDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on b.CustomerAccountPrmKey equals c.CustomerAccountPrmKey into ca
                            from c in ca.DefaultIfEmpty()
                            join p in context.People.Where(p => p.ActivationStatus == StringLiteralValue.Active && p.EntryStatus == StringLiteralValue.Verify) on c.PersonPrmKey equals p.PrmKey into cp
                            from p in cp.DefaultIfEmpty()
                            join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                            from mf in pm.DefaultIfEmpty()
                            join t in context.PersonTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals t.PersonPrmKey into pt
                            from t in pt.DefaultIfEmpty()
                            where (p.EntryStatus.Equals(StringLiteralValue.Verify))
                                   && (p.ActivationStatus.Equals(StringLiteralValue.Active))
                                   && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby p.FullName
                            select new SelectListItem
                            {
                                Value = b.BoardOfDirectorId.ToString(),
                                Text = ((mf.FullName.Equals(null)) ? p.FullName.Trim() + " ---> " + (t.TransFullName.Equals(null) ? " " : t.TransFullName.Trim()) : mf.FullName + " ---> " + (t.TransFullName.Equals(null) ? " " : t.TransFullName.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from b in context.BoardOfDirectors
                        join c in context.CustomerAccountDetails.Where(c => c.EntryStatus == StringLiteralValue.Verify) on b.CustomerAccountPrmKey equals c.CustomerAccountPrmKey into ca
                        from c in ca.DefaultIfEmpty()
                        join p in context.People.Where(p => p.ActivationStatus == StringLiteralValue.Active && p.EntryStatus == StringLiteralValue.Verify) on c.PersonPrmKey equals p.PrmKey into cp
                        from p in cp.DefaultIfEmpty()
                        join mf in context.PersonModifications.Where(m => m.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PersonPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (p.ActivationStatus.Equals(StringLiteralValue.Active))
                        orderby p.FullName
                        select new SelectListItem
                        {
                            Value = b.BoardOfDirectorId.ToString(),
                            Text = ((mf.FullName.Equals(null)) ? p.FullName.Trim() : mf.FullName)
                        }).ToList();
            }
        }

        public List<SelectListItem> BoardOfDirectorDesignationDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Designations
                            join mf in context.DesignationModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DesignationPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.DesignationTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals t.DesignationPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            && (d.DesignationCategory == "BOD")
                            orderby d.NameOfDesignation
                            select new SelectListItem
                            {
                                Value = d.DesignationId.ToString(),
                                Text = ((mf.NameOfDesignation.Equals(null)) ? d.NameOfDesignation.Trim() + " ---> " + (t.TransNameOfDesignation.Equals(null) ? " " : t.TransNameOfDesignation.Trim()) : mf.NameOfDesignation + " ---> " + (t.TransNameOfDesignation.Equals(null) ? " " : t.TransNameOfDesignation.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Designations
                        join mf in context.DesignationModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DesignationPrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                        && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        && (d.DesignationCategory == "BOD")
                        select new SelectListItem
                        {
                            Value = d.DesignationId.ToString(),
                            Text = ((mf.NameOfDesignation.Equals(null)) ? d.NameOfDesignation.Trim() : mf.NameOfDesignation)
                        }).ToList();
            }
        }

        public List<SelectListItem> CommunicationMediaDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from c in context.CommunicationMedias
                            join ct in context.CommunicationMediaTranslations on c.PrmKey equals ct.CommunicationMediaPrmKey
                            orderby c.NameOfCommunicationMedia
                            select new SelectListItem
                            {
                                Value = c.CommunicationMediaId.ToString(),
                                Text = c.NameOfCommunicationMedia.Trim() + " --> " + ct.TransNameOfCommunicationMedia.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from c in context.CommunicationMedias
                        orderby c.NameOfCommunicationMedia
                        select new SelectListItem
                        {
                            Value = c.CommunicationMediaId.ToString(),
                            Text = c.NameOfCommunicationMedia
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> CustomerSharesCapitalAccountDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.CustomerSharesCapitalAccounts
                            join d in context.CustomerAccountDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on r.CustomerAccountPrmKey equals d.CustomerAccountPrmKey into ed
                            from d in ed.DefaultIfEmpty()
                            join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active) on d.PersonPrmKey equals p.PrmKey into dt
                            from p in dt.DefaultIfEmpty()
                            where (r.EntryStatus.Equals(StringLiteralValue.Verify))
                            select new SelectListItem
                            {
                                Value = p.PersonId.ToString(),
                                Text = p.FullName.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.CommunicationMedias

                        select new SelectListItem
                        {
                            Value = e.CommunicationMediaId.ToString(),
                            Text = e.NameOfCommunicationMedia
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DaysOfMonthDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.DaysOfMonths
                            join rt in context.DaysOfMonthTranslations on r.PrmKey equals rt.DaysOfMonthPrmKey
                            select new SelectListItem
                            {
                                Value = r.DaysOfMonthId.ToString(),
                                Text = r.NameOfMonthDay.Trim() + " --> " + rt.TransNameOfDayOfMonth.Trim()
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.DaysOfMonths

                        select new SelectListItem
                        {
                            Value = e.DaysOfMonthId.ToString(),
                            Text = e.NameOfMonthDay
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DaysOfWeekDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.DaysOfWeeks
                            join rt in context.DaysOfWeekTranslations on r.PrmKey equals rt.DaysOfWeekPrmKey
                            select new SelectListItem
                            {
                                Value = r.DaysOfWeekId.ToString(),
                                Text = r.NameOfWeekDay.Trim() + " --> " + rt.TransNameOfDayOfWeek.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from e in context.DaysOfWeeks

                        select new SelectListItem
                        {
                            Value = e.DaysOfWeekId.ToString(),
                            Text = e.NameOfWeekDay
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> DepartmentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Departments
                            join mf in context.DepartmentModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DepartmentPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.DepartmentTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals t.DepartmentPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            orderby d.NameOfDepartment
                            select new SelectListItem
                            {
                                Value = d.DepartmentId.ToString(),
                                Text = ((mf.NameOfDepartment.Equals(null)) ? d.NameOfDepartment.Trim() + " ---> " + (t.TransNameOfDepartment.Equals(null) ? " " : t.TransNameOfDepartment.Trim()) : mf.NameOfDepartment + " ---> " + (t.TransNameOfDepartment.Equals(null) ? " " : t.TransNameOfDepartment.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Departments
                        join mf in context.DepartmentModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DepartmentPrmKey into dm
                        from mf in dm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                        && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = d.DepartmentId.ToString(),
                            Text = ((mf.NameOfDepartment.Equals(null)) ? d.NameOfDepartment.Trim() : mf.NameOfDepartment)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EmployeeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                return (from e in context.Employees
                        join d in context.EmployeeDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on e.PrmKey equals d.EmployeePrmKey into ed
                        from d in ed.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active) on d.PersonPrmKey equals p.PrmKey into dt
                        from p in dt.DefaultIfEmpty()
                        where (e.EntryStatus.Equals(StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = e.EmployeeId.ToString(),
                            Text = (p.FullName)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EmployeeTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from e in context.EmployeeTypes
                            join t in context.EmployeeTypeTranslations on e.PrmKey equals t.EmployeeTypePrmKey into et
                            from t in et.DefaultIfEmpty()
                            where (e.ActivationStatus.Equals(StringLiteralValue.Active))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = e.EmployeeTypeId.ToString(),
                                Text = (e.NameOfEmployeeType.Trim() + " ---> " + (t.TransNameOfEmployeeType.Equals(null) ? " " : t.TransNameOfEmployeeType.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.EmployeeTypes
                        where (o.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = o.EmployeeTypeId.ToString(),
                            Text = o.NameOfEmployeeType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> InvestigationOfficerDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                return (from e in context.Employees
                        join d in context.EmployeeDetails.Where(d => d.EntryStatus == StringLiteralValue.Verify) on e.PrmKey equals d.EmployeePrmKey into ed
                        from d in ed.DefaultIfEmpty()
                        join p in context.People.Where(p => p.EntryStatus == StringLiteralValue.Verify && p.ActivationStatus == StringLiteralValue.Active) on d.PersonPrmKey equals p.PrmKey into dt
                        from p in dt.DefaultIfEmpty()
                        where (e.EntryStatus.Equals(StringLiteralValue.Verify))
                        select new SelectListItem
                        {
                            Value = e.EmployeeId.ToString(),
                            Text = (p.FullName)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EmployeeDesignationDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Designations
                            join mf in context.DesignationModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DesignationPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.DesignationTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals t.DesignationPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            && (d.DesignationCategory == "EMP")
                            orderby d.NameOfDesignation
                            select new SelectListItem
                            {
                                Value = d.DesignationId.ToString(),
                                Text = ((mf.NameOfDesignation.Equals(null)) ? d.NameOfDesignation.Trim() + " ---> " + (t.TransNameOfDesignation.Equals(null) ? " " : t.TransNameOfDesignation.Trim()) : mf.NameOfDesignation + " ---> " + (t.TransNameOfDesignation.Equals(null) ? " " : t.TransNameOfDesignation.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Designations
                        join mf in context.DesignationModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on d.PrmKey equals mf.DesignationPrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                        && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        && (d.DesignationCategory == "EMP")

                        select new SelectListItem
                        {
                            Value = d.DesignationId.ToString(),
                            Text = ((mf.NameOfDesignation.Equals(null)) ? d.NameOfDesignation.Trim() : mf.NameOfDesignation)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EmployerNatureDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from n in context.EmployerNatures
                            join t in context.EmployerNatureTranslations on n.PrmKey equals t.EmployerNaturePrmKey
                            where n.ActivationStatus.Equals(StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = n.EmployerNatureId.ToString(),
                                Text = n.NameOfEmployerNature.Trim() + " --> " + t.TransNameOfEmployerNature.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from n in context.EmployerNatures
                        where n.ActivationStatus.Equals(StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = n.EmployerNatureId.ToString(),
                            Text = n.NameOfEmployerNature
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EmploymentTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from e in context.EmploymentTypes
                            join t in context.EmploymentTypeTranslations on e.PrmKey equals t.EmploymentTypePrmKey
                            where e.ActivationStatus.Equals(StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = e.EmploymentTypeId.ToString(),
                                Text = e.NameOfEmploymentType.Trim() + " --> " + t.TransNameOfEmploymentType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.EmploymentTypes
                        where e.ActivationStatus.Equals(StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = e.EmploymentTypeId.ToString(),
                            Text = e.NameOfEmploymentType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> EventTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.EventTypes
                            join t in context.EventTypeTranslations on d.PrmKey equals t.EventTypePrmKey
                            where ((d.ActivationStatus.Equals(StringLiteralValue.Active))
                                    || (d.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            orderby d.NameOfEventType
                            select new SelectListItem
                            {
                                Value = d.EventTypeId.ToString(),
                                Text = (d.NameOfEventType.Trim() + " ---> " + (t.TransNameOfEventType.Equals(null) ? " " : t.TransNameOfEventType.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.EventTypes
                        where (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = d.EventTypeId.ToString(),
                            Text = (d.NameOfEventType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> MeetingTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from c in context.MeetingTypes
                             join t in context.MeetingTypeTranslations on c.PrmKey equals t.MeetingTypePrmKey into ct
                             from t in ct.DefaultIfEmpty()

                             where ((c.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                     || (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                     && (t.LanguagePrmKey == regionalLanguagePrmKey))
                             orderby c.NameOfMeetingType
                             select new SelectListItem
                             {
                                 Value = c.MeetingTypeId.ToString(),
                                 Text = (c.NameOfMeetingType.Trim() + " ---> " + (t.TransNameOfMeetingType.Equals(null) ? " " : t.TransNameOfMeetingType.Trim()))
                             }).Distinct().OrderBy(l => l.Text).ToList();
                    return a;
                }

                // Default List In Default Language (i.e. English)
                return (from c in context.MeetingTypes
                        join t in context.MeetingTypeTranslations on c.PrmKey equals t.MeetingTypePrmKey into ct
                        from t in ct.DefaultIfEmpty()
                        where ((c.ActivationStatus.Equals(StringLiteralValue.Active))
                                || (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (c.ActivationStatus.Equals(StringLiteralValue.Active)))
                        orderby c.NameOfMeetingType
                        select new SelectListItem
                        {
                            Value = c.MeetingTypeId.ToString(),
                            Text = (c.NameOfMeetingType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> NoticeMediaDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var ab = (from d in context.NoticeMedias
                              join t in context.NoticeMediaTranslations on d.PrmKey equals t.NoticeMediaPrmKey into bt
                              from t in bt.DefaultIfEmpty()
                              where (d.ActivationStatus.Equals(StringLiteralValue.Active)
                                      && (t.LanguagePrmKey == regionalLanguagePrmKey))
                              select new SelectListItem
                              {
                                  Value = d.NoticeMediaId.ToString(),
                                  Text = (d.NameOfNoticeMedia.Trim() + " ---> " + (t.TransNameOfNoticeMedia.Equals(null) ? " " : t.TransNameOfNoticeMedia.Trim()))
                              }).Distinct().OrderBy(l => l.Text).ToList();
                    return ab;
                }

                // Default List In Defaul Language (i.e. English)
                var abc = (from d in context.NoticeMedias
                           where (d.ActivationStatus.Equals(StringLiteralValue.Active))
                           select new SelectListItem
                           {
                               Value = d.NoticeMediaId.ToString(),
                               Text = (d.NameOfNoticeMedia.Trim())
                           }).Distinct().OrderBy(l => l.Text).ToList();
                return abc;
            }
        }

        public List<SelectListItem> NoticeTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from n in context.NoticeTypes
                            join nt in context.NoticeTypeTranslations on n.PrmKey equals nt.NoticeTypePrmKey
                            orderby n.NameOfNoticeType
                            select new SelectListItem
                            {
                                Value = n.NoticeTypeId.ToString(),
                                Text = n.NameOfNoticeType.Trim() + " --> " + nt.TransNameOfNoticeType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from n in context.NoticeTypes
                        orderby n.NameOfNoticeType
                        select new SelectListItem
                        {
                            Value = n.NoticeTypeId.ToString(),
                            Text = n.NameOfNoticeType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SharesNoticeTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                // Shares Notice Type Allocat PrmKey Range From 1000 To 1999
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.NoticeTypes
                            join rt in context.NoticeTypeTranslations on r.PrmKey equals rt.NoticeTypePrmKey
                            where (r.PrmKey >= 1000 && r.PrmKey <= 1999)
                            select new SelectListItem
                            {
                                Value = r.NoticeTypeId.ToString(),
                                Text = r.NameOfNoticeType.Trim() + " --> " + rt.TransNameOfNoticeType.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from e in context.NoticeTypes

                        select new SelectListItem
                        {
                            Value = e.NoticeTypeId.ToString(),
                            Text = e.NameOfNoticeType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SalaryBreakupDropdownList
        {
            get
            {

                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.SalaryBreakups
                            join t in context.SalaryBreakupTranslations on o.PrmKey equals t.SalaryBreakupPrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.ActivationStatus.Equals(StringLiteralValue.Active)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = o.SalaryBreakupId.ToString(),
                                Text = (o.NameOfSalaryBreakup.Trim() + " ---> " + (t.TransNameOfSalaryBreakup.Equals(null) ? " " : t.TransNameOfSalaryBreakup.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.SalaryBreakups
                        where (o.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = o.SalaryBreakupId.ToString(),
                            Text = (o.NameOfSalaryBreakup.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> ScheduleTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.ScheduleTypes
                            join t in context.ScheduleTypeTranslations on s.PrmKey equals t.ScheduleTypePrmKey into st
                            from t in st.DefaultIfEmpty()
                            where (s.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey || t.LanguagePrmKey.Equals(null))
                                     || (s.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey || t.LanguagePrmKey.Equals(null))
                            select new SelectListItem
                            {
                                Value = s.ScheduleTypeId.ToString(),
                                Text = (s.NameOfScheduleType.Trim() + " ---> " + (t.TransNameOfScheduleType.Equals(null) ? " " : t.TransNameOfScheduleType.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from s in context.ScheduleTypes
                        where (s.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = s.ScheduleTypeId.ToString(),
                            Text = (s.NameOfScheduleType.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SocialMediaDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from s in context.SocialMedias
                            where (s.ActivationStatus.Equals(StringLiteralValue.Active))
                            orderby s.NameOfSocialMedia
                            select new SelectListItem
                            {
                                Value = s.SocialMediaId.ToString(),
                                Text = (s.NameOfSocialMedia.Trim())
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from s in context.SocialMedias
                        join t in context.SocialMediaTranslations on s.PrmKey equals t.SocialMediaPrmKey into st
                        from t in st.DefaultIfEmpty()
                        where (s.ActivationStatus.Equals(StringLiteralValue.Active))
                        orderby s.NameOfSocialMedia
                        select new SelectListItem
                        {
                            Value = s.SocialMediaId.ToString(),
                            Text = (s.NameOfSocialMedia.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }
    }
}
