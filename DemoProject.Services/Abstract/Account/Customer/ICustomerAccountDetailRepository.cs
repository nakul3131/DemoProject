using System;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountDetailRepository
    {
        // Get Scheme PrmKey
        short GetSchemePrmKey(Guid _customerAccountId);


        // Return GetRejectedEntry Entry
        Task<CustomerAccountDetailViewModel> GetRejectedEntry(long _customerAccountPrmKey);

       
        // Return GetUnVerifiedEntry Entry
        Task<CustomerAccountDetailViewModel> GetUnVerifiedEntry(long _customerAccountPrmKey);

        // Return GetVerifiedEntry Entry
        Task<CustomerAccountDetailViewModel> GetVerifiedEntry(long _customerAccountPrmKey);
    }
}
