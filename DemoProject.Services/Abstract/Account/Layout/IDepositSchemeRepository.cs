using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.Layout
{
    public interface IDepositSchemeRepository
    {
        // Amend DepositScheme Delete Entry - If Entry Rejected
        Task<bool> Amend(DepositSchemeViewModel _depositSchemeViewModel);

        // Return Rejected Entries
        Task<IEnumerable<DepositSchemeIndexViewModel>> GetDepositSchemeIndex(string _entryType);

        // Return DepositSchemePrmKey By schemeId
        short GetPrmKeyById(Guid _SchemeId);

        // Return Rejected Entry
        Task<DepositSchemeViewModel> GetDepositSchemeEntry(Guid _SchemeId, string _entryType);

        Task<bool> GetSessionValues(short _schemePrmKey, string _entryType);

        bool GetUniqueSchemeName(string _nameOfScheme);

        // Save DepositScheme New Entry
        Task<bool> Save(DepositSchemeViewModel _depositSchemeViewModel);

        // DepositSchemeDropdown List
        List<SelectListItem> SchemeDropdownList { get; }

        // Verify,Reject,Delete DepositScheme Entry
        Task<bool> VerifyRejectDelete(DepositSchemeViewModel _depositSchemeViewModel,string _entryType);
    }
}
