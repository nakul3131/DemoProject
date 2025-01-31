using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Transaction;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ISharesApplicationRepository
    {
        // Amend SharesApplication Delete Entry - If Entry Rejected
        Task<bool> Amend(SharesApplicationViewModel _sharesApplicationViewModel);

        // Delete SharesApplication - Only For Rejected Entry
        Task<bool> Delete(SharesApplicationViewModel _sharesApplicationViewModel);

        // Return Rejected Entries
        Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From SharesApplication Table Which Are Not Authorized
        Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From SharesApplication Table For Modification
        Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty PersonFinancialAssetViewModel (Used For Create)
        Task<SharesApplicationViewModel> GetViewModelForCreate(long _PersonPrmKey);

        // Return PersonFinancialAssetViewModel (Used For Reject View)
        Task<SharesApplicationViewModel> GetViewModelForReject(long _PersonPrmKey);

        // Return PersonFinancialAssetViewModel (Used For Unverified View)
        Task<SharesApplicationViewModel> GetViewModelForUnverified(long _PersonPrmKey);

        // Return PersonFinancialAssetViewModel (Used For Unverified View)
        Task<SharesApplicationViewModel> GetViewModelForVerified(long _PersonPrmKey);

        int GetPrmKeyByNumber(long _sharesApplicationNumbers);

        // Return Rejected Entry
        Task<SharesApplicationViewModel> GetRejectedEntry(long sharesApplicationNumber);

        //bool GetUniqueSharesApplicationName(string _nameOfSharesApplication);

        // Return Record From SharesApplication Table By Given Parameter (i.e. SharesApplicationId)
        Task<SharesApplicationViewModel> GetUnVerifiedEntry(long _sharesApplicationNumber);

        // Return Record From SharesApplication Table By Given Parameter (i.e. SharesApplicationId)
        Task<SharesApplicationViewModel> GetVerifiedEntry(long _sharesApplicationNumber);

        // Save SharesApplication Modification New Entry
        Task<bool> Modify(SharesApplicationViewModel _sharesApplicationViewModel);

        // Return Rejected SharesApplication Entries
        Task<IEnumerable<SharesApplicationViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return UnVerified SharesApplication Entries
        Task<IEnumerable<SharesApplicationViewModel>> GetUnverifiedEntries(long _personPrmKey);

        // Return Verified SharesApplication Entries
        Task<IEnumerable<SharesApplicationViewModel>> GetVerifiedEntries(long _personPrmKey);

        // Reject SharesApplication Entry
        Task<bool> Reject(SharesApplicationViewModel _sharesApplicationViewModel);

        // Save SharesApplication New Entry
        Task<bool> Save(SharesApplicationViewModel _sharesApplicationViewModel);

        // Authorize SharesApplication Entry
        Task<bool> Verify(SharesApplicationViewModel _sharesApplicationViewModel);
    }
}
