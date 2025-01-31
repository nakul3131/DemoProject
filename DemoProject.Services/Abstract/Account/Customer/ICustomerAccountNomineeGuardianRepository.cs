using System.Collections.Generic;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountNomineeGuardianRepository
    {
        //long GetPrmKeyById(Guid _customerAccountId);  

        // Return Rejected CustomerAccountNomineeGuardian Entries
       IEnumerable<CustomerAccountNomineeGuardianViewModel> GetRejectedEntries(long _customerAccountNomineePrmKey);

        // Return UnVerified CustomerAccountNomineeGuardian Entries
        IEnumerable<CustomerAccountNomineeGuardianViewModel> GetUnverifiedEntries(long _customerAccountNomineePrmKey);

        // Return Verified CustomerAccountNomineeGuardian Entries 
        IEnumerable<CustomerAccountNomineeGuardianViewModel> GetVerifiedEntries(long _customerAccountNomineePrmKey);
    }
}
