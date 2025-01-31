using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Establishment;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IAuthorizedSharesCapitalRepository
    {
        // Amend AuthorizedSharesCapital Entry - If Entry Rejected
        Task<bool> Amend(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel);

        // Delete AuthorizedSharesCapital Entry - Only For Rejected Entry
        Task<bool> Delete(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject AuthorizedSharesCapital Entry
        Task<bool> Reject(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel);

        // Save AuthorizedSharesCapital New Entry
        Task<bool> Save(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel);

        // Verified AuthorizedSharesCapital Entry
        Task<bool> Verify(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel);
    }

}
