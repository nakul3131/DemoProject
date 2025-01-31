using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonGSTReturnDocumentRepository
    {// Amend PersonGSTReturnDocument Delete Entry - If Entry Rejected
        Task<bool> Amend(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

        // Delete PersonGSTReturnDocument - Only For Rejected Entry
        Task<bool> Delete(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

        // Return Empty personGSTReturnDocument Table 
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected Entries
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From PersonGSTReturnDocument Table Which Are Not Authorized
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From PersonGSTReturnDocument Table For Modification
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected personGSTReturnDocument Entries
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetRejectedEntries(long _personGSTRegistrationDetailPrmKey);

        // Return UnVerified personGSTReturnDocument Entries
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetUnverifiedEntries(long _personGSTRegistrationDetailPrmKey);

        // Return Verified personGSTReturnDocument Entries
        Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetVerifiedEntries(long _personGSTRegistrationDetailPrmKey);

        // Return Empty PersonGSTReturnDocumentViewModel (Used For Create)
        Task<PersonGSTReturnDocumentViewModel> GetViewModelForCreate(long _PersonPrmKey);

        // Return PersonGSTReturnDocumentViewModel (Used For Reject View)
        Task<PersonGSTReturnDocumentViewModel> GetViewModelForReject(long _PersonPrmKey);

        // Return PersonGSTReturnDocumentViewModel (Used For Unverified View)
        Task<PersonGSTReturnDocumentViewModel> GetViewModelForUnverified(long _PersonPrmKey);

        // Return PersonGSTReturnDocumentViewModel (Used For Unverified View)
        Task<PersonGSTReturnDocumentViewModel> GetViewModelForVerified(long _PersonPrmKey);

        // Reject PersonGSTReturnDocument Entry
        Task<bool> Reject(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

        // Save PersonGSTReturnDocument New Entry
        Task<bool> Save(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

        // Save PersonGSTReturnDocument Modification New Entry
        Task<bool> Modify(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

        // Authorize PersonGSTReturnDocument Entry
        Task<bool> Verify(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel);

    }
}
