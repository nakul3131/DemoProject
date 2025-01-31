using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.GL;

namespace DemoProject.Services.Abstract.Account.GL
{
    public interface IGeneralLedgerTransactionTypeRepository
    {
        // Amend GeneralLedgerTransactionType Entry
        Task<bool> Amend(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);

        // Delete GeneralLedgerTransactionType
        Task<bool> Delete(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);

        // Return GeneralLedgerTransactionType Rejected Entries
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From GeneralLedgerTransactionType Table Which Are Not Authorized
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From GeneralLedgerTransactionType Table Which Are Authorized
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty GeneralLedgerTransactionType Table 
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected GeneralLedgerTransactionType Entries
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetRejectedEntries(short _generalLedgerPrmKey);

        // Return UnVerified GeneralLedgerTransactionType Entries
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetUnverifiedEntries(short _generalLedgerPrmKey);

        // Return Verified GeneralLedgerTransactionType Entries
        Task<IEnumerable<GeneralLedgerTransactionTypeViewModel>> GetVerifiedEntries(short _generalLedgerPrmKey);

        // Return Empty CenterTradingEntityViewModel (Used For Create)
        Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForCreate(short _generalLedgerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Reject View)
        Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForReject(short _generalLedgerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Unverified View)
        Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForUnverified(short _generalLedgerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Unverified View)
        Task<GeneralLedgerTransactionTypeViewModel> GetViewModelForVerified(short _generalLedgerPrmKey);

        // Reject GeneralLedgerTransactionType Entry
        Task<bool> Reject(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);

        // Save GeneralLedgerTransactionType New Entry
        Task<bool> Save(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);

        // Save GeneralLedgerTransactionType Modification New Entry
        Task<bool> Modify(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);

        // Authorize GeneralLedgerTransactionType Entry
        Task<bool> Verify(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel);
    }
}