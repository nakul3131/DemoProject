using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IGuardianPersonRepository
    {
        Task<bool> Amend(GuardianPersonViewModel _guardianPersonViewModel);

        Task<IEnumerable<GuardianPersonViewModel>> GetIndexOfRejectedEntries();

        Task<IEnumerable<GuardianPersonViewModel>> GetIndexOfUnVerifiedEntries();

        Task<IEnumerable<GuardianPersonViewModel>> GetIndexWithCreateModifyOperationStatus();
        
        Task<GuardianPersonViewModel> GetViewModelForCreate(long _personPrmKey);

        Task<GuardianPersonViewModel> GetViewModelForReject(long _personPrmKey);

        Task<GuardianPersonViewModel> GetViewModelForUnverified(long _personPrmKey);

        Task<GuardianPersonViewModel> GetViewModelForVerified(long _personPrmKey);

        Task<bool> Modify(GuardianPersonViewModel _guardianPersonViewModel);
        
        Task<bool> VerifyRejectDelete(GuardianPersonViewModel _guardianPersonViewModel,string _entryType);
    }
}
