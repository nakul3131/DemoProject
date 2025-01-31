using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.GL;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerBusinessOfficeRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetRejectedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetUnverifiedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<GeneralLedgerBusinessOfficeViewModel>> GetVerifiedGeneralLedgerBusinessOfficeEntries(short _generalLedgerPrmKey);

        Task<bool> Modify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel);

        Task<IEnumerable<GeneralLedgerIndexViewModel>> GetGeneralLedgerBusinessOfficeUnverifiedIndex(string _entryType);

        Task<bool> Reject(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel);

       Task<bool> Verify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel);

        Task<IEnumerable<GeneralLedgerIndexViewModel>> GetRejectedGeneralLedgerBusinessOfficeIndex(string _entryType);

        Task<bool> Amend(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel);

        Task<bool> Delete(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel);

    }
}
