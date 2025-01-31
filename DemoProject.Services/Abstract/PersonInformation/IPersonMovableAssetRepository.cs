using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonMovableAssetRepository
    {
        // Amend PersonMovableAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonMovableAssetViewModel _personMovableAssetViewModel);

        // Save PersonMovableAsset Modification New Entry
        Task<bool> Modify(PersonMovableAssetViewModel _personMovableAssetViewModel);

        // Authorize PersonMovableAsset Entry
        Task<bool> VerifyRejectDelete(PersonMovableAssetViewModel _personMovableAssetViewModel, string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonMovableAssetViewModel>> GetEntries(long _personPrmKey , string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
