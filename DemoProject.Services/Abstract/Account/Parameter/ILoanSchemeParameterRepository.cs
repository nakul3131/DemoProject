using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface ILoanSchemeParameterRepository
    {
        // Amend Loan Scheme Parameter Amend Entry - If Entry Rejected
        Task<bool> Amend(LoanSchemeParameterViewModel _loanSchemeParameterViewModel);

        // Delete Loan Scheme Parameter - Only For Rejected Entry
        Task<bool> Delete(LoanSchemeParameterViewModel _loanSchemeParameterViewModel);

        // Return Current Active Entry
        Task<LoanSchemeParameterViewModel> GetActiveEntry();

        LoanSchemeParameterViewModel GetActiveEntry1();

        LoanSchemeParameterViewModel ClearModelStateGetActiveEntry();

        // Return Autherize Entry
        Task<IEnumerable<LoanSchemeParameterViewModel>> GetLoanSchemeParameterIndex();

        // Return Rejected Entry
        Task<LoanSchemeParameterViewModel> GetRejectedEntry();

        // Return UnAuthorized Entry
        Task<LoanSchemeParameterViewModel> GetUnVerifiedEntry();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject Loan Scheme Parameter Entry
        Task<bool> Reject(LoanSchemeParameterViewModel _loanSchemeParameterViewModel);

        // Save DLoan Scheme Parameter New Entry
        Task<bool> Save(LoanSchemeParameterViewModel _loanSchemeParameterViewModel);

        // Authorize Loan Scheme Parameter Entry
        Task<bool> Verify(LoanSchemeParameterViewModel _loanSchemeParameterViewModel);
    }
}
