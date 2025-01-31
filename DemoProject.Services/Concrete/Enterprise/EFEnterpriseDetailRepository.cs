using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise
{
    public class EFEnterpriseDetailRepository : IEnterpriseDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFEnterpriseDetailRepository(IConfigurationDetailRepository _configurationDetailRepository, RepositoryConnection _connection) 
        {
            configurationDetailRepository = _configurationDetailRepository;
            context = _connection.EFDbContext;
        }

        public byte GetAreaOfOperationPrmKeyById(Guid _areaOfOperationId)
        {
            return context.AreaOfOperations
                    .Where(c => c.AreaOfOperationId == _areaOfOperationId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetBusinessNaturePrmKeyById(Guid _businessNatureId)
        {
            return context.BusinessNatures
                    .Where(c => c.BusinessNatureId == _businessNatureId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetDesignationPrmKeyById(Guid _designationId)
        {
            return context.Designations
                    .Where(b => b.DesignationId == _designationId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public byte GetFinancialOrganizationTypePrmKeyById(Guid _financialOrganizationTypeId)
        {
            return context.FinancialOrganizationTypes
                    .Where(a => a.FinancialOrganizationTypeId == _financialOrganizationTypeId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetOfficeSchedulePrmKeyById(Guid _officeScheduleId)
        {
            return context.OfficeSchedules
                    .Where(c => c.OfficeScheduleId == _officeScheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public string GetSysNameOfBusinessOfficeTypeByPrmKey(short _businessOfficePrmKey)
        {
            return  (from b in context.BusinessOfficeDetails
                    join t in context.BusinessOfficeTypes.Where(t => t.ActivationStatus == StringLiteralValue.Active) on b.BusinessOfficeTypePrmKey equals t.PrmKey into bt
                    from t in bt.DefaultIfEmpty()
                    where (b.BusinessOfficePrmKey == _businessOfficePrmKey)
                    select (t.SysNameOfBusinessOffice)).FirstOrDefault().ToString();
        }

        public bool IsSetApplicationNumberBranchwise()
        {
            short prmKey = context.BusinessOfficeApplicationNumbers.Where(b => b.EntryStatus == EntryStatus.Verified)
                            .Select(b => b.PrmKey).FirstOrDefault();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public bool IsSetCustomerAccountNumberBranchwise()
        {
            short prmKey = context.BusinessOfficeAccountNumbers.Where(b => b.EntryStatus == EntryStatus.Verified)
                            .Select(b => b.PrmKey).FirstOrDefault();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public bool IsSetMemberNumberBranchwise()
        {
            short prmKey = context.BusinessOfficeMemberNumbers.Where(b => b.EntryStatus == EntryStatus.Verified)
                            .Select(b => b.PrmKey).FirstOrDefault();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public bool IsSetSharesCertificateNumberBranchwise()
        {
            short prmKey = context.BusinessOfficeSharesCertificateNumbers.Where(b => b.EntryStatus == EntryStatus.Verified)
                            .Select(b => b.PrmKey).FirstOrDefault();

            if (prmKey > 0)
                return true;
            else
                return false;
        }

        public DateTime GetCoOperativeRegistrationDate()
        {
            return context.Organizations
                .Where(o => o.EntryStatus == StringLiteralValue.Verify)
                .Select(o => o.CoopRegistrationDate).FirstOrDefault();
        }

        public int GetCoopSocietyClassPrmKeyById(Guid _coopSocietyClassId)
        {
            return context.CoopSocietyClasses
                    .Where(c => c.SocietyClassId == _coopSocietyClassId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public int GetCoopSocietySubClassPrmKeyById(Guid _coopSocietySubClassId)
        {
            return context.CoopSocietySubClasses
                    .Where(c => c.SocietySubClassId == _coopSocietySubClassId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }


        public short GetBankBranchPrmKeyById(Guid _bankBranchId)
        {
            return context.BankBranches
                    .Where(b => b.BankBranchId == _bankBranchId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public short GetBankPrmKeyById(Guid _bankId)
        {
            return context.Banks
                    .Where(b => b.BankId == _bankId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public short GetBranchPrmKeyById(Guid _bankBranchId)
        {
            return context.BankBranches
                    .Where(b => b.BankBranchId == _bankBranchId)
                    .Select(b => b.PrmKey).FirstOrDefault();
        }

        public short GetBusinessOfficePrmKeyById(Guid _businessOfficeId)
        {
            return context.BusinessOffices
                    .Where(c => c.BusinessOfficeId == _businessOfficeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetBusinessOfficeRegionalLanguagePrmKey()
        {
            short userHomeBranchPrmKey = (short)HttpContext.Current.Session["UserHomeBranchPrmKey"];

            var a = context.BusinessOfficeDetails
                        .Where(bd => bd.EntryStatus == StringLiteralValue.Verify && bd.BusinessOfficePrmKey == userHomeBranchPrmKey)
                        .Select(bd => bd.LanguagePrmKey).FirstOrDefault();

            return (short)a;
        }

        public short GetBusinessOfficeTypePrmKeyById(Guid _businessOfficeTypeId)
        {
            return context.BusinessOfficeTypes
                    .Where(c => c.BusinessOfficeTypeId == _businessOfficeTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetCreditSocietyPrmKeyById(Guid _creditSocietyId)
        {
            return context.CreditSocieties
                    .Where(c => c.CreditSocietyId == _creditSocietyId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetSchedulePrmKeyById(Guid _scheduleId)
        {
            return context.Schedules
                    .Where(c => c.ScheduleId == _scheduleId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }
        
        public List<SelectListItem> AreaOfOperationDropdownList
        {
            get
            {
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from m in context.AreaOfOperations
                            join mt in context.AreaOfOperationTranslations on m.PrmKey equals mt.AreaOfOperationPrmKey
                            select new SelectListItem
                            {
                                Value = m.AreaOfOperationId.ToString(),
                                Text = m.NameOfAreaOfOperation.Trim() + " / " + mt.TransNameOfAreaOfOperation.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from e in context.AreaOfOperations
                        select new SelectListItem
                        {
                            Value = e.AreaOfOperationId.ToString(),
                            Text = e.NameOfAreaOfOperation
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BankDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.Banks
                            join t in context.BankTranslations on b.PrmKey equals t.BankPrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (b.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = b.BankId.ToString(),
                                Text = (b.NameOfBank.Trim() + " ---> " + (t.TransNameOfBank.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from b in context.Banks
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.BankId.ToString(),
                            Text = b.NameOfBank.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BusinessNatureDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from m in context.BusinessNatures
                            join t in context.BusinessNatureTranslations on m.PrmKey equals t.BusinessNaturePrmKey into mt
                            from t in mt.DefaultIfEmpty()
                            where (m.ActivationStatus == StringLiteralValue.Active
                                    && t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = m.BusinessNatureId.ToString(),
                                Text = (m.NameOfBusinessNature.Trim() + " ---> " + (t.TransNameOfBusinessNature.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from e in context.BusinessNatures
                        select new SelectListItem
                        {
                            Value = e.BusinessNatureId.ToString(),
                            Text = e.NameOfBusinessNature
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BusinessOfficeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1) 
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.BusinessOffices
                            join mf in context.BusinessOfficeModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on b.PrmKey equals mf.BusinessOfficePrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.BusinessOfficeTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on b.PrmKey equals t.BusinessOfficePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (b.EntryStatus == StringLiteralValue.Verify)
                            && (b.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = b.BusinessOfficeId.ToString(),
                                Text = mf.NameOfBusinessOffice ??  b.NameOfBusinessOffice.Trim() + " ---> " + t.TransNameOfBusinessOffice.Trim() ?? " "
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from b in context.BusinessOffices
                        join mf in context.BusinessOfficeModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on b.PrmKey equals mf.BusinessOfficePrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (b.EntryStatus ==StringLiteralValue.Verify)
                        && (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.BusinessOfficeId.ToString(),
                            Text = (mf.NameOfBusinessOffice.Trim() ?? b.NameOfBusinessOffice.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> BusinessOfficeTypeDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from b in context.BusinessOfficeTypes
                            join t in context.BusinessOfficeTypeTranslations on b.PrmKey equals t.BusinessOfficeTypePrmKey into mt
                            from t in mt.DefaultIfEmpty()
                            where (b.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = b.BusinessOfficeTypeId.ToString(),
                                Text = (b.NameOfBusinessOfficeType.Trim() + " ---> " + (t.TransNameOfBusinessOfficeType.Trim() ?? " " ))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from b in context.BusinessOfficeTypes
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = b.BusinessOfficeTypeId.ToString(),
                            Text = b.NameOfBusinessOfficeType.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> FinancialOrganizationTypeDropdownList
        {
            get
            {
                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from r in context.FinancialOrganizationTypes
                            join t in context.FinancialOrganizationTypeTranslations on r.PrmKey equals t.FinancialOrganizationTypePrmKey
                            where (r.ActivationStatus == StringLiteralValue.Active)
                            select new SelectListItem
                            {
                                Value = r.FinancialOrganizationTypeId.ToString(),
                                Text = r.NameOfFinancialOrganizationType.Trim() + " --> " + t.TransNameOfFinancialOrganization.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from r in context.FinancialOrganizationTypes
                        where (r.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = r.FinancialOrganizationTypeId.ToString(),
                            Text = r.NameOfFinancialOrganizationType
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> GetBankBranchDropdownList(Guid _bankId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            short bankPrmKey = GetBankPrmKeyById(_bankId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from r in context.BankBranches
                        join b in context.Banks.Where(b => b.ActivationStatus == StringLiteralValue.Active) on r.BankPrmKey equals b.PrmKey into rb
                        from b in rb.DefaultIfEmpty()
                        join t in context.BankBranchTranslations on b.PrmKey equals t.BankBranchPrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        where (r.ActivationStatus == StringLiteralValue.Active)
                        && (b.PrmKey == bankPrmKey)
                        && (r.BankPrmKey == bankPrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = r.BankBranchId.ToString(),
                            Text = (r.NameOfBranch.Trim()/* + " ---> " + (t.TransNameOfBranch.Equals(null) ? " " : t.TransNameOfBranch.Trim())*/)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from r in context.BankBranches
                    join b in context.Banks.Where(b => b.ActivationStatus == StringLiteralValue.Active) on r.BankPrmKey equals b.PrmKey into rb
                    from b in rb.DefaultIfEmpty()
                    where (r.ActivationStatus == StringLiteralValue.Active)
                        && (b.PrmKey == bankPrmKey)
                    select new SelectListItem
                    {
                        Value = r.BankBranchId.ToString(),
                        Text = r.NameOfBranch.Trim()
                    }).Distinct().OrderBy(l => l.Text).ToList();
        }

        public List<SelectListItem> GetBranchDropdownList(Guid _bankId)
        {
            // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
            short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

            short bankPrmKey = GetBankPrmKeyById(_bankId);

            // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
            if (regionalLanguagePrmKey != 1)
            {
                return (from b in context.BankBranches
                        join t in context.BankBranchTranslations on b.PrmKey equals t.BankBranchPrmKey into bt
                        from t in bt.DefaultIfEmpty()
                        where (b.ActivationStatus == StringLiteralValue.Active)
                        && (b.PrmKey == bankPrmKey)
                        && (t.LanguagePrmKey == regionalLanguagePrmKey)
                        select new SelectListItem
                        {
                            Value = b.BankBranchId.ToString(),
                            Text = (b.NameOfBranch.Trim() + " ---> " + (t.TransNameOfBranch.Trim() ?? " "))
                        }
                        ).ToList();
            }

            // Default List In Default Language (i.e. English)
            return (from b in context.BankBranches
                    where (b.ActivationStatus == StringLiteralValue.Active)
                    && (b.PrmKey == bankPrmKey)
                    select new SelectListItem
                    {
                        Value = b.BankBranchId.ToString(),
                        Text = b.NameOfBranch.Trim()
                    }
                    ).ToList();
        }

        public List<SelectListItem> OfficeSchedulesDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from o in context.OfficeSchedules
                            join mf in context.OfficeScheduleModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on o.PrmKey equals mf.OfficeSchedulePrmKey into om
                            from mf in om.DefaultIfEmpty()
                            join t in context.OfficeScheduleTranslations.Where(t => t.EntryStatus == StringLiteralValue.Verify) on o.PrmKey equals t.OfficeSchedulePrmKey into ot
                            from t in ot.DefaultIfEmpty()
                            where (o.EntryStatus == StringLiteralValue.Verify)
                            && (o.ActivationStatus == StringLiteralValue.Active)
                            && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = o.OfficeScheduleId.ToString(),
                                Text = ((mf.NameOfSchedule ==null) ? o.NameOfSchedule.Trim() + " ---> " + (t.TransNameOfSchedule.Trim() ?? " " ) : mf.NameOfSchedule + " ---> " + (t.TransNameOfSchedule.Trim() ?? " "))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from o in context.OfficeSchedules
                        join mf in context.OfficeScheduleModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on o.PrmKey equals mf.OfficeSchedulePrmKey into om
                        from mf in om.DefaultIfEmpty()
                        where (o.EntryStatus == StringLiteralValue.Verify)
                        && (o.ActivationStatus == StringLiteralValue.Active)
                        select new SelectListItem
                        {
                            Value = o.OfficeScheduleId.ToString(),
                            Text = ((mf.NameOfSchedule) ?? o.NameOfSchedule.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();

            }
        }

        public List<SelectListItem> ScheduleDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.Schedules
                            join mf in context.ScheduleModifications on d.PrmKey equals mf.SchedulePrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                            join t in context.ScheduleTranslations on d.PrmKey equals t.SchedulePrmKey into bt
                            from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus == StringLiteralValue.Verify
                                    && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus ==null)
                                    && (t.EntryStatus == StringLiteralValue.Verify || t.EntryStatus == null)
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey))
                            select new SelectListItem
                            {
                                Value = d.ScheduleId.ToString(),
                                Text = (mf.NameOfSchedule ?? d.NameOfSchedule.Trim()) + " ---> " + (t.TransNameOfSchedule.Trim() ?? " ")
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.Schedules
                        join mf in context.ScheduleModifications on d.PrmKey equals mf.SchedulePrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (d.EntryStatus == StringLiteralValue.Verify
                                && (mf.EntryStatus == StringLiteralValue.Verify || mf.EntryStatus ==null))
                        select new SelectListItem
                        {
                            Value = d.ScheduleId.ToString(),
                            Text = ((mf.NameOfSchedule) ?? d.NameOfSchedule.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }

        }

        public List<SelectListItem> SocietyClassDropdownList
        {
            get
            {
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from m in context.CoopSocietyClasses
                            join mt in context.CoopSocietyClassTranslations on m.PrmKey equals mt.CoopSocietyClassPrmKey
                            select new SelectListItem
                            {
                                Value = m.SocietyClassId.ToString(),
                                Text = m.NameOfClass.Trim() + " / " + mt.NameOfClass.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from e in context.CoopSocietyClasses
                        select new SelectListItem
                        {
                            Value = e.SocietyClassId.ToString(),
                            Text = e.NameOfClass
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SocietySubClassDropdownList
        {
            get
            {
                if ((short)HttpContext.Current.Session["RegionalLanguagePrmKey"] != 1)
                {
                    return (from m in context.CoopSocietySubClasses
                            join mt in context.CoopSocietySubClassTranslations on m.PrmKey equals mt.CoopSocietySubClassPrmKey
                            select new SelectListItem
                            {
                                Value = m.SocietySubClassId.ToString(),
                                Text = m.NameOfSubClass.Trim() + " / " + mt.NameOfSubClass.Trim()
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                return (from e in context.CoopSocietySubClasses
                        select new SelectListItem
                        {
                            Value = e.SocietySubClassId.ToString(),
                            Text = e.NameOfSubClass
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }
    }
}
