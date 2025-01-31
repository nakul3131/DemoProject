using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerSharesCapitalAccountRepository
    {
        // Return GetRejectedEntries Entry
        Task<CustomerSharesCapitalAccountViewModel> GetRejectedEntries(long _customerAccountPrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<CustomerSharesCapitalAccountViewModel> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return GetVerifiedEntries Entry
        Task<CustomerSharesCapitalAccountViewModel> GetVerifiedEntries(long _customerAccountPrmKey);
    }
}
