using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICenterDemographicDetailRepository
    {
        // Amend CenterDemographicDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);

        // Delete CenterDemographicDetail - Only For Rejected Entry
        Task<bool> Delete(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);

        short GetPrmKeyById(Guid _CenterId);

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected Entries
        Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CenterDemographicDetail Table Which Are Not Authorized
        Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CenterDemographicDetail Table For Modification
        Task<IEnumerable<CenterDemographicDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterDemographicDetailViewModel (Used For Create)
        Task<CenterDemographicDetailViewModel> GetViewModelForCreate(short _centerPrmKey);

        // Return CenterDemographicDetailViewModel (Used For Reject View)
        Task<CenterDemographicDetailViewModel> GetViewModelForReject(short _centerPrmKey);

        // Return CenterDemographicDetailViewModel (Used For Unverified View)
        Task<CenterDemographicDetailViewModel> GetViewModelForUnverified(short _centerPrmKey);

        // Return CenterDemographicDetailViewModel (Used For Unverified View)
        Task<CenterDemographicDetailViewModel> GetViewModelForVerified(short _centerPrmKey);

        // Reject CenterDemographicDetail Entry
        Task<bool> Reject(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);

        // Save CenterDemographicDetail New Entry
        Task<bool> Save(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);

        // Save CenterDemographicDetail Modification New Entry
        Task<bool> Modify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);

        // Authorize CenterDemographicDetail Entry
        Task<bool> Verify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel);


        // Return GetRejectedEntry Entry
        Task<CenterDemographicDetailViewModel> GetRejectedEntry(short _centerPrmKey);

        // Return GetUnVerifiedEntry Entry
        Task<CenterDemographicDetailViewModel> GetUnverifiedEntry(short _centerPrmKey);

        // Return GetVerifiedEntry Entry
        Task<CenterDemographicDetailViewModel> GetVerifiedEntry(short _centerPrmKey);
    }
}
