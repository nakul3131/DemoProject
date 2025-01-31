using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountDocumentRepository
    {
        // Return GetRejectedEntries Entry
        Task<IEnumerable<CustomerAccountDocumentViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountDocumentViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return GetVerifiedEntries Entry
        Task<IEnumerable<CustomerAccountDocumentViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);
    }
}
