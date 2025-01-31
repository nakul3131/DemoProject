using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonEmploymentDetailRepository
    {
        // Amend PersonEmploymentDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

        // Delete PersonEmploymentDetail - Only For Rejected Entry
        Task<bool> Delete(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

        // Return Empty personEmploymentDetail Table 
        Task<IEnumerable<PersonViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected PersonEmploymentDetail Entries
        Task<IEnumerable<PersonEmploymentDetailViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return UnVerified PersonEmploymentDetail Entries
        Task<IEnumerable<PersonEmploymentDetailViewModel>> GetUnverifiedEntries(long _personPrmKey);

        // Return Verified PersonEmploymentDetail Entries
        Task<IEnumerable<PersonEmploymentDetailViewModel>> GetVerifiedEntries(long _personPrmKey);

        // Return Rejected Entries
        Task<IEnumerable<PersonViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From PersonEmploymentDetail Table Which Are Not Authorized
        Task<IEnumerable<PersonViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From PersonEmploymentDetail Table For Modification
        Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty PersonEmploymentDetailViewModel (Used For Create)
        Task<PersonEmploymentDetailViewModel> GetViewModelForCreate(long _PersonPrmKey);

        // Return PersonEmploymentDetailViewModel (Used For Reject View)
        Task<PersonEmploymentDetailViewModel> GetViewModelForReject(long _PersonPrmKey);

        // Return PersonEmploymentDetailViewModel (Used For Unverified View)
        Task<PersonEmploymentDetailViewModel> GetViewModelForUnverified(long _PersonPrmKey);

        // Return PersonEmploymentDetailViewModel (Used For Unverified View)
        Task<PersonEmploymentDetailViewModel> GetViewModelForVerified(long _PersonPrmKey);

        // Reject PersonEmploymentDetail Entry
        Task<bool> Reject(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

        // Save PersonEmploymentDetail New Entry
        Task<bool> Save(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

        // Save PersonEmploymentDetail Modification New Entry
        Task<bool> SaveModification(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

        // Authorize PersonEmploymentDetail Entry
        Task<bool> Verify(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel);

    }
}
