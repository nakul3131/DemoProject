using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface IDepositSchemeParameterRepository
    {
        // Amend DepositSchemeParameter Delete Entry - If Entry Rejected
        Task<bool> Amend(DepositSchemeParameterViewModel _depositSchemeParameterViewModel);

        // Delete DepositSchemeParameter - Only For Rejected Entry
        Task<bool> Delete(DepositSchemeParameterViewModel _depositSchemeParameterViewModel);

        // Return Current Active Entry
        Task<DepositSchemeParameterViewModel> GetActiveEntry();

        // Return DepositSchemeParameter Verified Entries
        Task<IEnumerable<DepositSchemeParameterViewModel>> GetDepositSchemeParameterIndex();

        // Return Rejected Entry
        Task<DepositSchemeParameterViewModel> GetRejectedEntry();

        // // Return UnVerified Entry
        Task<DepositSchemeParameterViewModel> GetUnverifiedEntry();

        // Return True If Any Verification Pending
        Task<bool> IsAnyAuthorizationPending();

        // Return True If Any Deposit Scheme Verification Pending
        Task<bool> IsAnyDepositSchemeAuthorizationPending();

        // Reject DepositSchemeParameter Entry
        Task<bool> Reject(DepositSchemeParameterViewModel _depositSchemeParameterViewModel);

        // Save DepositSchemeParameter New Entry
        Task<bool> Save(DepositSchemeParameterViewModel _depositSchemeParameterViewModel);

        // Verify DepositSchemeParameter Entry
        Task<bool> Verify(DepositSchemeParameterViewModel _depositSchemeParameterViewModel);
    }
}
