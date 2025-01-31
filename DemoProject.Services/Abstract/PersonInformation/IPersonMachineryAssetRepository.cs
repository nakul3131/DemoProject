using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonMachineryAssetRepository
    {
        // Amend PersonMachineryAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonMachineryAssetViewModel _personFinancialAssetViewModel);

        // Save PersonMachineryAsset Modification New Entry
        Task<bool> Modify(PersonMachineryAssetViewModel _personFinancialAssetViewModel);

        // Authorize PersonMachineryAsset Entry
        Task<bool> VerifyRejectDelete(PersonMachineryAssetViewModel _personFinancialAssetViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonMachineryAssetViewModel>> GetEntries(long _personPrmKey , string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}