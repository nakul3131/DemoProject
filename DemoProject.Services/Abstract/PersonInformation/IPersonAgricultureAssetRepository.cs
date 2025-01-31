using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonAgricultureAssetRepository
    {
        // Amend PersonAgricultureAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel);

        
        // Save PersonAgricultureAsset Modification New Entry
        Task<bool> Modify(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel);

        // Authorize PersonAgricultureAsset Entry
        Task<bool> VerifyRejectDelete(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel ,string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex( string _entryType);

        //Get Verified Entries By PersonPrmKey
        Task<IEnumerable<PersonAgricultureAssetViewModel>> GetEntries(long _personPrmKey ,string _entryType);

        ////Get UnVerified Entries By PersonPrmKey
        //Task<IEnumerable<PersonAgricultureAssetViewModel>> GetUnVerifiedEntries(long _personPrmKey);

        ////Get Rejected Entries By PersonPrmKey
        //Task<IEnumerable<PersonAgricultureAssetViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
