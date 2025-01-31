using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerLoanAccountRepository
    {
        // Amend CustomerLoanAccount Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);

        // Delete CustomerLoanAccount - Only For Rejected Entry
        Task<bool> Delete(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);

        // Save CustomerLoanAccount Modification New Entry
        Task<bool> Modify(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);

        // Reject CustomerLoanAccount Entry
        Task<bool> Reject(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);

        // Save CustomerLoanAccount New Entry
        Task<bool> Save(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);

        // Authorize CustomerLoanAccount Entry
        Task<bool> Verify(CustomerLoanAccountViewModel _CustomerLoanAccountViewModel);


        // Return Rejected Entries
        Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerLoanAccount Table Which Are Not Authorized
        Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerLoanAccount Table For Modification
        Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return CustomerLoanAccountViewModel (Used For Reject View)
        Task<CustomerLoanAccountViewModel> GetRejectedEntry(long _CustomerLoanAccountPrmKey);

        // Return CustomerLoanAccountViewModel (Used For Unverified View)
        Task<CustomerLoanAccountViewModel> GetUnVerifiedEntry(long _CustomerLoanAccountPrmKey);

        // Return CustomerLoanAccountViewModel (Used For Unverified View)
        Task<CustomerLoanAccountViewModel> GetVerifiedEntry(long _CustomerLoanAccountPrmKey);

        // Return Empty CustomerLoanAccountViewModel (Used For Create)
        Task<CustomerLoanAccountViewModel> GetViewModelForCreate(long _CustomerLoanAccountPrmKey);



    }
}
