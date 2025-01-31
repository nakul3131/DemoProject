using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IResponsibilityRepository
    {
        // Amend Responsibility Delete Entry - If Entry Rejected
        Task<bool> Amend(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);

        // Delete Responsibility - Only For Rejected Entry
        Task<bool> Delete(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);

        List<SelectListItem> ResponsibilityDropdownList { get; }

        // Return Rejected Entries
        Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Responsibility Table Which Are Not Authorized
        Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Responsibility Table For Modification
        Task<IEnumerable<ResponsibilityViewModel>> GetIndexOfVerifiedEntries();

        short GetResponsibilityPrmKeyById(Guid _responsibilityId);

        // Return Rejected Entry
        Task<ResponsibilityViewModel> GetRejectedEntry(Guid _charityFundUtilizationRuleId);

        bool GetUniqueResponsibilityName(string _nameOfResponsibility);

        // Return Record From Responsibility Table By Given Parameter (i.e. ResponsibilityId)
        Task<ResponsibilityViewModel> GetUnVerifiedEntry(Guid _charityFundUtilizationRuleId);

        // Return Record From Responsibility Table By Given Parameter (i.e. ResponsibilityId)
        Task<ResponsibilityViewModel> GetVerifiedEntry(Guid _charityFundUtilizationRuleId);

        // Save Responsibility Modification New Entry
        Task<bool> Modify(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);

        // Reject Responsibility Entry
        Task<bool> Reject(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);

        // Save Responsibility New Entry
        Task<bool> Save(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);

        // Authorize Responsibility Entry
        Task<bool> Verify(ResponsibilityViewModel _charityFundUtilizationRuleViewModel);
    }
}
