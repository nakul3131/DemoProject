using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountTurnOverLimitRepository
    {
        // Return GetRejectedEntries Entry
        Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return GetVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);        
    }
}
