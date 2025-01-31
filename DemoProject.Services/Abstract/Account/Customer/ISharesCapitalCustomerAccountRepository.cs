using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ISharesCapitalCustomerAccountRepository
    {

        // Return SharesCapitalSchemePrmKey By schemeId
        long GetPrmKeyById(Guid _customerAccountId);


        // Amend SharesCapitalScheme Delete Entry - If Entry Rejected
        Task<bool> Amend(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel);

        Task<bool> GetSessionValues(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType);

        Task<bool> IsValidAccountNumber(Guid _schemeId, int _accountNumber);

        Task<bool> IsValidMemberNumber(Guid _schemeId, int _memberNumber);

        Task<bool> IsVisibleAccountNumber(Guid _schemeId);

        Task<bool> IsVisibleMemberNumber(Guid _schemeId);

        // Save SharesCapitalScheme New Entry
        Task<bool> Save(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel);

        // Verify,Eject,Delete SharesCapitalScheme Entry
        Task<bool> VerifyRejectDelete(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType);

        // Return Rejected Entries
        Task<IEnumerable<SharesCapitalCustomerAccountIndexViewModel>> GetSharesCapitalCustomerAccountIndex(string _entryType);

        // Return Rejected Entry
        Task<SharesCapitalCustomerAccountViewModel> GetSharesCapitalCustomerAccountEntry(Guid _customerAccountId, string _entryType);
        


        // SharesCapitalSchemeDropdown List
        List<SelectListItem> SharesCapitalCustomerAccountDropdownList { get; }

       
    }
}
