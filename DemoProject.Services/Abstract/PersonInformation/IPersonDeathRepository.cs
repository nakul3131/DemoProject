using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonDeathRepository
    {
        // Amend PersonDeath Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonDeathViewModel _PersonDeathViewModel);

        // Delete PersonDeath - Only For Rejected Entry
        Task<bool> Delete(PersonDeathViewModel _PersonDeathViewModel);

        // Return Empty PersonDeath Table 
        Task<IEnumerable<PersonDeathViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected Entries
        Task<IEnumerable<PersonDeathViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From PersonDeath Table Which Are Not Authorized
        Task<IEnumerable<PersonDeathViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From PersonDeath Table For Modification
        Task<IEnumerable<PersonDeathViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected PersonDeath Entries
        Task<IEnumerable<PersonDeathViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return UnVerified PersonDeath Entries
        Task<IEnumerable<PersonDeathViewModel>> GetUnverifiedEntries(long _personPrmKey);

        // Return Verified PersonDeath Entries
        Task<IEnumerable<PersonDeathViewModel>> GetVerifiedEntries(long _personPrmKey);

        // Return Empty PersonDeathViewModel (Used For Create)
        Task<PersonDeathViewModel> GetViewModelForCreate(long _PersonPrmKey);

        // Return PersonDeathViewModel (Used For Reject View)
        Task<PersonDeathViewModel> GetViewModelForReject(long _PersonPrmKey);

        // Return PersonDeathViewModel (Used For Unverified View)
        Task<PersonDeathViewModel> GetViewModelForUnverified(long _PersonPrmKey);

        // Return PersonDeathViewModel (Used For Unverified View)
        Task<PersonDeathViewModel> GetViewModelForVerified(long _PersonPrmKey);

        // Reject PersonDeath Entry
        Task<bool> Reject(PersonDeathViewModel _PersonDeathViewModel);

        // Save PersonDeath New Entry
        Task<bool> Save(PersonDeathViewModel _PersonDeathViewModel);

        // Save PersonDeath Modification New Entry
        Task<bool> Modify(PersonDeathViewModel _PersonDeathViewModel);

        // Authorize PersonDeath Entry
        Task<bool> Verify(PersonDeathViewModel _PersonDeathViewModel);

    }
}
