using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface ISharesCapitalSchemeParameterRepository
    {
        // Amend SharesCapitalSchemeParameter Delete Entry - If Entry Rejected
        Task<bool> Amend(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel);

        // Delete SharesCapitalSchemeParameter - Only For Rejected Entry
        Task<bool> Delete(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel);

        // Return Current Active Entry
        Task<SharesCapitalSchemeParameterViewModel> GetActiveEntry();

        // Return SharesCapitalSchemeParameter Verified Entries
        Task<IEnumerable<SharesCapitalSchemeParameterViewModel>> GetSharesCapitalSchemeParameterIndex();

        // Return Rejected Entry
        Task<SharesCapitalSchemeParameterViewModel> GetRejectedEntry();

        // // Return UnVerified Entry
        Task<SharesCapitalSchemeParameterViewModel> GetUnverifiedEntry();

        // Return True If Any Verification Pending
        Task<bool> IsAnyAuthorizationPending();

        // Return True If Any Shares Capital Scheme Verification Pending
        Task<bool> IsAnySharesCapitalSchemeAuthorizationPending();

        // Reject SharesCapitalSchemeParameter Entry
        Task<bool> Reject(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel);

        // Save SharesCapitalSchemeParameter New Entry
        Task<bool> Save(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel);

        // Verify SharesCapitalSchemeParameter Entry
        Task<bool> Verify(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel);
    }
}
