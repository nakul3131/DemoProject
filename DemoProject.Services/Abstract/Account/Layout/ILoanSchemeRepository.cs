using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.Layout
{
    public interface ILoanSchemeRepository
    {
        // Amend Scheme Delete Entry - If Entry Rejected
        Task<bool> Amend(LoanSchemeViewModel _loanSchemeViewModel);
       
        // Return Rejected Entries
        Task<IEnumerable<LoanSchemeIndexViewModel>> GetLoanSchemeIndex(string _entryType);

        // Return Rejected Entry
        Task<LoanSchemeViewModel> GetLoanSchemeEntry(Guid _schemeId, string _entryType);

        // Return LoanSchemePrmKey By schemeId
        short GetPrmKeyById(Guid _SchemeId);

        Task<bool> GetSessionValues(short _schemePrmKey, string _entryType);

        bool GetUniqueSchemeName(string _nameOfScheme);
        
        // Save Scheme New Entry
        Task<bool> Save(LoanSchemeViewModel _loanSchemeViewModel);

        Task<bool> VerifyRejectDelete(LoanSchemeViewModel _loanSchemeViewModel, string _entryType);
    }
}
