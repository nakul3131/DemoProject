using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonCommoditiesAssetRepository
    {
        Task<bool> Amend(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel);
        
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);
        
        Task<PersonCommoditiesAssetViewModel> GetEntry(long _personPrmKey ,string _entryType);

        Task<bool> Modify(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel);
        
        Task<bool> VerifyRejectDelete(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel, string _entryType);
        
        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
