using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Transaction;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionGeneralLedgerRepository
    {
        // Return Rejected TransactionGeneralLedger Entries
        Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetRejectedEntries(long _transactionMasterPrmKey);

        // Return UnVerified TransactionGeneralLedger Entries
        Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetUnverifiedEntries(long _transactionMasterPrmKey);

        // Return Verified TransactionGeneralLedger Entries
        Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetVerifiedEntries(long _transactionMasterPrmKey);
    }
}
