using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICenterTradingEntityDetailsRepository
    {
        // Amend CenterTradingDetail Entry
        Task<bool> Amend(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);

        // Delete CenterTradingDetail
        Task<bool> Delete(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);

        // Return CenterTradingEntityDetail Rejected Entries
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CenterTradingEntityDetail Table Which Are Not Authorized
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CenterTradingEntityDetail Table Which Are Authorized
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetRejectedEntries(short _centerPrmKey);

        // Return UnVerified CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetUnverifiedEntries(short _centerPrmKey);

        // Return Verified CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterTradingEntityDetailViewModel>> GetVerifiedEntries(short _centerPrmKey);

        // Return Empty CenterTradingEntityViewModel (Used For Create)
        Task<CenterTradingEntityDetailViewModel> GetViewModelForCreate(short _centerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Reject View)
        Task<CenterTradingEntityDetailViewModel> GetViewModelForReject(short _centerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Unverified View)
        Task<CenterTradingEntityDetailViewModel> GetViewModelForUnverified(short _centerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Unverified View)
        Task<CenterTradingEntityDetailViewModel> GetViewModelForVerified(short _centerPrmKey);

        // Reject CenterTradingDetail Entry
        Task<bool> Reject(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);

        // Save CenterTradingDetail New Entry
        Task<bool> Save(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);

        // Save CenterTradingDetail Modification New Entry
        Task<bool> Modify(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);

        // Authorize CenterTradingDetail Entry
        Task<bool> Verify(CenterTradingEntityDetailViewModel _centerTradingEntityDetailViewModel);
    }
}
