using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICenterOccupationRepository
    {
        // Amend CenterOccupation Delete Entry - If Entry Rejected
        Task<bool> Amend(CenterOccupationViewModel _centerOccuptionStructureViewModel);

        // Delete CenterOccupation - Only For Rejected Entry
        Task<bool> Delete(CenterOccupationViewModel _centerOccuptionStructureViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CenterOccupation Table Which Are Not Authorized
        Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfUnverifiedEntries();

        // Return Valid List From CenterOccupation Table For Modification
        Task<IEnumerable<CenterOccupationViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CenterOccupationViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterOccupationViewModel>> GetRejectedEntries(short _centerPrmKey);

        // Return UnVerified CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterOccupationViewModel>> GetUnverifiedEntries(short _centerPrmKey);

        // Return Verified CenterTradingEntityDetail Entries
        Task<IEnumerable<CenterOccupationViewModel>> GetVerifiedEntries(short _centerPrmKey);

        // Return Empty CenterTradingEntityViewModel (Used For Create)
        Task<CenterOccupationViewModel> GetViewModelForCreate(short _centerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Reject View)
        Task<CenterOccupationViewModel> GetViewModelForReject(short _centerPrmKey);

        // Return CenterTradingEntityViewModel (Used For Unverified View)
        Task<CenterOccupationViewModel> GetViewModelForVerified(short _centerPrmKey);

        // Return CenterOccuptionStructureViewModel (Used For Unverified View)
        Task<CenterOccupationViewModel> GetViewModelForUnverified(short _centerPrmKey);

        // Reject CenterOccupation Entry
        Task<bool> Reject(CenterOccupationViewModel _centerOccuptionStructureViewModel);

        // Save CenterOccupation New Entry
        Task<bool> Save(CenterOccupationViewModel _centerOccuptionStructureViewModel);

        // Save CenterOccupation Modification New Entry
        Task<bool> Modify(CenterOccupationViewModel _centerOccuptionStructureViewModel);

        // Authorize CenterOccupation Entry
        Task<bool> Verify(CenterOccupationViewModel _centerOccuptionStructureViewModel);
    }
}
