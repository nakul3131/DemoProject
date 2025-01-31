using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.Layout
{
    public interface ISharesCapitalSchemeRepository
    {
        // Amend SharesCapitalScheme Delete Entry - If Entry Rejected
        Task<bool> Amend(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel);
        
        // Return Rejected Entries
        Task<IEnumerable<SharesCapitalSchemeIndexViewModel>> GetSharesCapitalSchemeIndex(string _entryType);

        Task<bool> GetSessionValues(short _schemePrmKey, string _entryType);

        // Return Rejected Entry
        Task<SharesCapitalSchemeViewModel> GetSharesCapitalSchemeEntry(Guid _SchemeId, string _entryType);

        // Return True If Name Is Unique
        bool IsUniqueSchemeName(string _nameOfScheme);
        
        // Save SharesCapitalScheme New Entry
        Task<bool> Save(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel);

        // Verify,Reject,Delete SharesCapitalScheme Entry
        Task<bool> VerifyRejectDelete(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel,string _entryType);
    }
}
