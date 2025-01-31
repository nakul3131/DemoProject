using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.GL;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerCurrencyRepository
    {
        // Return Rejected General Ledger Currency Entry
        Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetRejectedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey);

        // Return Unverified Entries of Currency
        Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetUnverifiedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey);

        // Return Valid List From General Ledger Currency Table For Modification
        Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetVerifiedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey);
    }
}
