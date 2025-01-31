using DemoProject.Services.ViewModel.Account.GL;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerDbContextRepository
    {
        bool AttachGeneralLedgerData(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType);

        bool AttachGeneralLedgerModificationData(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType);

        bool AttachGeneralLedgerBusinessOfficeData(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string _entryType);

        bool AttachGeneralLedgerCurrencyData(GeneralLedgerCurrencyViewModel _generalLedgerCurrencyViewModel, string _entryType);

        bool AttachGeneralLedgerTransactionTypeData(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel, string _entryType);

        bool AttachGeneralLedgerCustomerTypeData(GeneralLedgerCustomerTypeViewModel _generalLedgerCustomerTypeViewModel, string _entryType);

        Task<bool> SaveData();
    }
}
