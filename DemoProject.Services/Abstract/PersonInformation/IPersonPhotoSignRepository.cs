using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonPhotoSignRepository
    {
        // Amend PersonMovableAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonPhotoSignViewModel _personPhotoSignViewModel);

        // Get Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        // Return PersonPhotoSignViewModel
        Task<PersonPhotoSignViewModel> GetEntry(long _PersonPrmKey,string _entryType);

        
        // Save PersonMovableAsset Modification New Entry
        Task<bool> Modify(PersonPhotoSignViewModel _personPhotoSignViewModel);

        // Authorize PersonMovableAsset Entry
        Task<bool> VerifyRejectDelete(PersonPhotoSignViewModel _personPhotoSignViewModel, string _entryType);


        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
