using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface ISharesCapitalByLawsParameterRepository
    {
        // Amend Address Parameter Delete Entry - If Entry Rejected
        Task<bool> Amend(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameterViewModel);

        // Delete Address Parameter - Only For Rejected Entry
        Task<bool> Delete(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameterViewModel);

        // Return Current Active Entry
        SharesCapitalByLawsParameterViewModel GetActiveEntry();

        // Return Autherize Entry
        Task<IEnumerable<SharesCapitalByLawsParameterViewModel>> GetSharesCapitalByLawsParameterIndex();

        // Return Rejected Entry
        Task<SharesCapitalByLawsParameterViewModel> GetRejectedEntry();

        // Return UnAuthorized Entry
        Task<SharesCapitalByLawsParameterViewModel> GetUnVerifiedEntry();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        decimal GetSharesFaceValueAmount();
        decimal AdmissionFeesForMembershipAmount();
        decimal AdmissionFeesForNominalMember();

        int MinimumNumberOfSharesHoldingForActiveMember();

        // Reject Address Parameter Entry
        Task<bool> Reject(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameterViewModel);

        // Save Address Parameter New Entry
        Task<bool> Save(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameterViewModel);

        // Authorize Address Parameter Entry
        Task<bool> Verify(SharesCapitalByLawsParameterViewModel _sharesCapitalByLawsParameterViewModel);
    }
}
