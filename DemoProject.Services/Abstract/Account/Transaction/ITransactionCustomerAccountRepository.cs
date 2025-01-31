using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionCustomerAccountRepository
    {
        // Return Rejected TransactionCustomerAccount Entries
        Task<IEnumerable<TransactionCustomerAccountViewModel>> GetRejectedEntries(long _transactionMasterPrmKey);

        // Return Closing Balance
        decimal GetClosingBalance(DateTime _balanceDate, Guid _customerAccountId);
        
        // Return Ledger Balance
        decimal GetLedgerBalance(DateTime _balanceDate, Guid _generalLedgerId);
        // Return UnVerified TransactionCustomerAccount Entries
        Task<IEnumerable<TransactionCustomerAccountViewModel>> GetUnverifiedEntries(long _transactionMasterPrmKey);

        // Return Verified TransactionCustomerAccount Entries
        Task<IEnumerable<TransactionCustomerAccountViewModel>> GetVerifiedEntries(long _transactionMasterPrmKey);
    }
}
