using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonFinancialAssetRepository
    {

        // Amend PersonFinancialAsset Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonFinancialAssetViewModel _personFinancialAssetViewModel);
        
        // Save PersonFinancialAsset Modification New Entry
        Task<bool> Modify(PersonFinancialAssetViewModel _personFinancialAssetViewModel);

        // Authorize PersonFinancialAsset Entry
        Task<bool> VerifyRejectDelete(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _entryType);

        //Get Verified Index
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        //Get Entries By PersonPrmKey
        Task<IEnumerable<PersonFinancialAssetViewModel>> GetEntries(long _personPrmKey,string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);


    }
}
