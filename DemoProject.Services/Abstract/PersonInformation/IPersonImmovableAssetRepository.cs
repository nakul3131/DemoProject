using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonImmovableAssetRepository
    {
        // Amend PersonFinancialAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonImmovableAssetViewModel _personFinancialAssetViewModel);

        // Save PersonFinancialAsset Modification New Entry
        Task<bool> Modify(PersonImmovableAssetViewModel _personFinancialAssetViewModel);

        // Authorize PersonFinancialAsset Entry
        Task<bool> VerifyRejectDelete(PersonImmovableAssetViewModel _personFinancialAssetViewModel, string _entryType);


        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonImmovableAssetViewModel>> GetEntries(long _personPrmKey,string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);



    }
}
