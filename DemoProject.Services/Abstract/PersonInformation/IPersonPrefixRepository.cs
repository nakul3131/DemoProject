using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonPrefixRepository
    {
        Task<bool> Amend(PersonPrefixViewModel _personPrefixViewModel);

        Task<bool> Delete(PersonPrefixViewModel _personPrefixViewModel);

        Task<IEnumerable<PersonPrefixViewModel>> GetIndexOfRejectedEntries();

        Task<IEnumerable<PersonPrefixViewModel>> GetIndexOfUnVerifiedEntries();

        Task<IEnumerable<PersonPrefixViewModel>> GetIndexWithCreateModifyOperationStatus();

        Task<PersonPrefixViewModel> GetViewModelForCreate(long _personPrmKey);

        Task<PersonPrefixViewModel> GetViewModelForReject(long _personPrmKey);

        Task<PersonPrefixViewModel> GetViewModelForUnverified(long _personPrmKey);

        Task<PersonPrefixViewModel> GetViewModelForVerified(long _personPrmKey);

        Task<bool> Modify(PersonPrefixViewModel _personPrefixViewModel);

        Task<bool> Reject(PersonPrefixViewModel _personPrefixViewModel);

        Task<bool> Save(PersonPrefixViewModel _personPrefixViewModel);

        Task<bool> Verify(PersonPrefixViewModel _personPrefixViewModel);
    }
}
