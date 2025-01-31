using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IForeignerPersonRepository
    {
        // Amend PersonVehiclesDetail Entry
        Task<bool> Amend(ForeignerViewModel _foreignerPersonViewModel);

        // Delete PersonVehiclesDetail
        Task<bool> Delete(ForeignerViewModel _foreignerPersonViewModel);

        // Return PersonVehiclesDetail Rejected Entries
        Task<IEnumerable<ForeignerViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From PersonVehiclesDetail Table Which Are Not Authorized
        Task<IEnumerable<ForeignerViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From PersonVehiclesDetail Table Which Are Authorized
        Task<IEnumerable<PersonViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty PersonVehiclesDetail Table 
        Task<IEnumerable<ForeignerViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return list
      //  long GetPrmKeyById(Guid _foreignerPersonId);

        // Return Rejected PersonVehiclesDetail Entries
        Task<IEnumerable<ForeignerViewModel>> GetRejectedEntries(long _personPrmKey);

        // Return UnVerified PersonVehiclesDetail Entries
        Task<IEnumerable<ForeignerViewModel>> GetUnverifiedEntries(long _personPrmKey);

        // Return Verified PersonVehiclesDetail Entries
        Task<IEnumerable<ForeignerViewModel>> GetVerifiedEntries(long _personPrmKey);

        // Return Empty ForeignerViewModel (Used For Create)
        Task<ForeignerViewModel> GetViewModelForCreate(long _personPrmKey);

        // Return ForeignerViewModel (Used For Reject View)
        Task<ForeignerViewModel> GetViewModelForReject(long _personPrmKey);

        // Return ForeignerViewModel (Used For Unverified View)
        Task<ForeignerViewModel> GetViewModelForUnverified(long _personPrmKey);

        // Return ForeignerViewModel (Used For Unverified View)
        Task<ForeignerViewModel> GetViewModelForVerified(long _personPrmKey);

        Task<bool> Modify(ForeignerViewModel _foreignerPersonViewModel);

        // Reject PersonVehiclesDetail Entry
        Task<bool> Reject(ForeignerViewModel _foreignerPersonViewModel);

        // Save PersonVehiclesDetail New Entry
        Task<bool> Save(ForeignerViewModel _foreignerPersonViewModel);

        // Save PersonVehiclesDetail Modification New Entry
        Task<bool> SaveModification(ForeignerViewModel _foreignerPersonViewModel);

        // Authorize PersonVehiclesDetail Entry
        Task<bool> Verify(ForeignerViewModel _foreignerPersonViewModel);
    }
}
