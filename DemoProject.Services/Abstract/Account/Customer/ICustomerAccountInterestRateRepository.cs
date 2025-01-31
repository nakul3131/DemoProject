using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountInterestRateRepository
    {
        // Return GetRejectedEntries Entry
        Task<IEnumerable<CustomerAccountInterestRateViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountInterestRateViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return GetVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountInterestRateViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);
    }
}
