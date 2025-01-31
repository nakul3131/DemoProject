using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonHomeBranchRepository
    {
        Task<bool> Amend(PersonHomeBranchViewModel _personHomeBranchViewModel);
        
        Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType);

        Task<PersonHomeBranchViewModel> GetEntry(long _personPrmKey,string _entryType);

        Task<bool> Modify(PersonHomeBranchViewModel _personHomeBranchViewModel);

        Task<bool> VerifyRejectDelete(PersonHomeBranchViewModel _personHomeBranchViewModel, string _entryType);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending(long personPrmKey);

    }
}
