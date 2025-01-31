using DemoProject.Services.ViewModel.Account.Customer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerDepositAccountRepository
    {
        // Amend CustomerDepositAccount Delete Entry - If Entry Rejected
        Task<bool> Amend(DepositCustomerAccountViewModel _depositCustomerAccountViewModel);

        Task<bool> GetSessionValues(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string _entryType);

        // Save CustomerDepositAccount New Entry
        Task<bool> Save(DepositCustomerAccountViewModel _depositCustomerAccountViewModel);

        // Authorize CustomerDepositAccount Entry
        Task<bool> VerifyRejectDelete(DepositCustomerAccountViewModel _depositCustomerAccountViewModel,string _entryType);

        // Return Rejected Entries
        Task<IEnumerable<DepositCustomerAccountIndexViewModel>> GetDepositCustomerAccountIndex(string _entryType);

        // Return Rejected Entry
        Task<DepositCustomerAccountViewModel> GetDepositCustomerAccountEntry(Guid _customerAccountId, string _entryType);

        

    }
}
