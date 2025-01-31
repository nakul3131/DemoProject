using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountNomineeRepository
    {

        // Amend CustomerAccountNominee Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Delete CustomerAccountNominee - Only For Rejected Entry
        Task<bool> Delete(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Save CustomerAccountNominee Modification New Entry
        Task<bool> Modify(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Reject CustomerAccountNominee Entry
        Task<bool> Reject(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Save CustomerAccountNominee New Entry
        Task<bool> Save(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Authorize CustomerAccountNominee Entry
        Task<bool> Verify(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel);

        // Return SharesCapitalSchemePrmKey By schemeId
        //long GetPrmKeyById(Guid _customerAccountNomineeId);

       

        // Return Rejected Entries
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerAccountNominee Table Which Are Not Authorized
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerAccountNominee Table For Modification
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return Record From CustomerAccountNominee Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return Record From CustomerAccountNominee Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);

        List<SelectListItem> CustomerAccountForNomineeDropdownList(List<string> _personId);


    }
}
