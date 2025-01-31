using DemoProject.Services.ViewModel.Account.Customer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ILoanCustomerAccountRepository
    {
        // Return LoanCustomerAccountPrmKey By customerAccountId
        int GetCustomerLoanAccountPrmKeyByCustomerAccountId(Guid _customerAccountId);

        // Return LoanCustomerAccountPrmKey By customerAccountId
        int GetCustomerLoanAccountPrmKeyByCustomerAccountId(Guid _customerAccountId, string _entryStatus);

        // Return LoanCustomerAccountPrmKey By customerAccountId

        // Amend LoanCustomerAccount Delete Entry - If Entry Rejected
        Task<bool> Amend(LoanCustomerAccountViewModel _loanCustomerAccountViewModel);

        
        Task<bool> EnablePreOwnedVehiclePhotoUploadInLocalStorage(Guid _customerAccountId);


        Task<bool> GetSessionValues(LoanCustomerAccountViewModel loanCustomerAccountViewModel, string _entryType);

        Task<bool> IsValidAccountNumber(Guid _customerAccountId, int _accountNumber);
        
        Task<bool> IsValidMemberNumber(Guid _customerAccountId, int _memberNumber);
       
        // Save LoanCustomerAccount New Entry
        Task<bool> Save(LoanCustomerAccountViewModel _loanCustomerAccountViewModel);

        //Authorize LoanCustomerAccount Entry
        Task<bool> VerifyRejectDelete(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType);

        Task<string> GetPreOwnedVehiclePhotoUpload(Guid _customerAccountId);

        // Return Rejected Entries
        Task<IEnumerable<LoanCustomerAccountIndexViewModel>> GetLoanCustomerAccountIndex(string _entryType);



        // Return Rejected Entry
        Task<LoanCustomerAccountViewModel> GetLoanCustomerAccountEntry(Guid _customerAccountId, string _entryType);

        // LoanCustomerAccountDropdown List
        List<SelectListItem> LoanCustomerAccountDropdownList { get; }

    }
}
