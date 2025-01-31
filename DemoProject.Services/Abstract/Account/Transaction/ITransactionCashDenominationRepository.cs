using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Transaction;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionCashDenominationRepository
    {
        
        // Return Rejected TransactionCashDenomination Entries
        Task<IEnumerable<TransactionCashDenominationViewModel>> GetRejectedEntries(long _transactionMasterPrmKey);

        // Return UnVerified TransactionCashDenomination Entries
        Task<IEnumerable<TransactionCashDenominationViewModel>> GetUnverifiedEntries(long _transactionMasterPrmKey);

        // Return Verified TransactionCashDenomination Entries
        Task<IEnumerable<TransactionCashDenominationViewModel>> GetVerifiedEntries(long _transactionMasterPrmKey);
    }
}
