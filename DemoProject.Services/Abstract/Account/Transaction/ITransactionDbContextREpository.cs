using DemoProject.Services.ViewModel.Account.Transaction;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionDbContextRepository
    {
        bool AttachTransactionData(TransactionViewModel _transactionViewModel, string _entryType);

        bool AttachTransactionCustomerAccountData(TransactionCustomerAccountViewModel _transactionCustomerAccountViewModel, string _entryType);

        bool AttachSharesCessationTransactionData(SharesCessationTransactionViewModel _sharesCessationTransactionViewModel, string _entryType);

        bool AttachTransactionGeneralLedgerData(TransactionGeneralLedgerViewModel _transactionGeneralLedgerViewModel, string _entryType);
        bool AttachTransactionGSTDetailData(TransactionGSTDetailViewModel _transactionGSTDetailViewModel, string _entryType);

        bool AttachSharesTransactionData(SharesCapitalTransactionViewModel _sharesTransactionViewModel, string _entryType);

        bool AttachTransactionCashDenominationData(TransactionCashDenominationViewModel _transactionCashDenominationViewModel, string _entryType);

        Task<bool> SaveData();


    }
}
