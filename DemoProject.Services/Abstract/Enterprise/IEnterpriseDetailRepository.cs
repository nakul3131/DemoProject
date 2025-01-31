using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Enterprise
{
    public interface IEnterpriseDetailRepository
    {
        byte GetAreaOfOperationPrmKeyById(Guid _areaOfOperationId);

        byte GetBusinessNaturePrmKeyById(Guid _businessNatureId);

        short GetDesignationPrmKeyById(Guid _designationId);

        byte GetFinancialOrganizationTypePrmKeyById(Guid _financialOrganizationTypeId);

        byte GetOfficeSchedulePrmKeyById(Guid _officeScheduleId);

        bool IsSetApplicationNumberBranchwise();

        bool IsSetCustomerAccountNumberBranchwise();

        bool IsSetMemberNumberBranchwise();

        bool IsSetSharesCertificateNumberBranchwise();

        DateTime GetCoOperativeRegistrationDate();

        int GetCoopSocietyClassPrmKeyById(Guid _coopSocietyClassId);

        int GetCoopSocietySubClassPrmKeyById(Guid _coopSocietySubClassId);

        short GetBankBranchPrmKeyById(Guid _bankBranchId);

        short GetBankPrmKeyById(Guid _bankId);

        short GetBranchPrmKeyById(Guid _bankBranchId);

        short GetBusinessOfficePrmKeyById(Guid _businessOfficeId);

        short GetBusinessOfficeRegionalLanguagePrmKey();

        short GetBusinessOfficeTypePrmKeyById(Guid _businessOfficeTypeId);

        short GetCreditSocietyPrmKeyById(Guid _creditSocietyId);

        short GetSchedulePrmKeyById(Guid _ScheduleId);


        string GetSysNameOfBusinessOfficeTypeByPrmKey(short _businessOfficePrmKey);


        List<SelectListItem> AreaOfOperationDropdownList { get; }

        List<SelectListItem> BankDropdownList { get; }

        List<SelectListItem> BusinessNatureDropdownList { get; }

        List<SelectListItem> BusinessOfficeDropdownList { get; }

        List<SelectListItem> BusinessOfficeTypeDropdownList { get; }

        List<SelectListItem> FinancialOrganizationTypeDropdownList { get; }

        List<SelectListItem> GetBankBranchDropdownList(Guid _bankId);

        List<SelectListItem> GetBranchDropdownList(Guid _bankId);
        
        List<SelectListItem> OfficeSchedulesDropdownList { get; }

        List<SelectListItem> ScheduleDropdownList { get; }

        List<SelectListItem> SocietyClassDropdownList { get; }

        List<SelectListItem> SocietySubClassDropdownList { get; }
    }
}
