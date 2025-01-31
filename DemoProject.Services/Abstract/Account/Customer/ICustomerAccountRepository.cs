using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountRepository
    {

        // Get CustomerAccountSchemeId
        short GetCustomerAccountSchemePrmKeyById(Guid _customerAccountId);
        long GetPrmKeyById(Guid _customerAccountDetailId);

        // Amend CustomerAccount Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerAccountViewModel _customerAccountViewModel);

        // Delete CustomerAccount - Only For Rejected Entry
        Task<bool> Delete(CustomerAccountViewModel _customerAccountViewModel);

        // Save CustomerAccount Modification New Entry
        Task<bool> Modify(CustomerAccountViewModel _customerAccountViewModel);

        // Reject CustomerAccount Entry
        Task<bool> Reject(CustomerAccountViewModel _customerAccountViewModel);

        // Save CustomerAccount New Entry
        Task<bool> Save(CustomerAccountViewModel _customerAccountViewModel);

        // Authorize CustomerAccount Entry
        Task<bool> Verify(CustomerAccountViewModel _customerAccountViewModel);



        // Return Rejected Entry
        Task<DepositCustomerAccountViewModel> GetRejectedEntry(Guid _customerAccountId);

        // Return UnVerifiedEntry
        Task<DepositCustomerAccountViewModel> GetUnVerifiedEntry(Guid _customerAccountId);

        // Return VerifiedEntry
        Task<DepositCustomerAccountViewModel> GetVerifiedEntry(Guid _customerAccountId);

        // Return Rejected Entries
        Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerAccount Table Which Are Not Authorized
        Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerAccount Table For Modification
        Task<IEnumerable<CustomerAccountViewModel>> GetIndexOfVerifiedEntries();

        // Return SharesCapitalSchemePrmKey By schemeId
        ///long GetPrmKeyById(Guid _customerAccountId);     

        

        // CustomerAccountDropdown List 
        List<SelectListItem> CustomerAccountDropdownList { get; }

        // Get CustomerAccountDropdownList By General Ledger, PersonPrmKey
        List<SelectListItem> GetCustomerAccountDropdownList(short _generalLedgerPrmKey, long _personPrmKey);

        // Get GetCustomerJointAccountDropdownList By General Ledger, PersonPrmKey
        List<SelectListItem> GetCustomerJointAccountDropdownList(short _generalLedgerPrmKey, long _personPrmKey);

        // Get GetCustomerWithJointAccountDropdownList By General Ledger, PersonPrmKey
        List<SelectListItem> GetCustomerWithJointAccountDropdownList(Guid _generalLedgerId, Guid _personId);
    }
}
