using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountFacilityRepository
    {
        // Return GetRejectedEntries Entry
        Task<IEnumerable<CustomerAccountFacilityViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountFacilityViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return GetVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountFacilityViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);
    }
}
