using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerPreOwnedVehicleLoanInspectionRepository
    {
        // Amend CustomerPreOwnedVehicleLoanInspection Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);

        // Delete CustomerPreOwnedVehicleLoanInspection - Only For Rejected Entry
        Task<bool> Delete(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);

        // Save CustomerPreOwnedVehicleLoanInspection Modification New Entry
        Task<bool> Modify(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);

        // Reject CustomerPreOwnedVehicleLoanInspection Entry
        Task<bool> Reject(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);

        // Save CustomerPreOwnedVehicleLoanInspection New Entry
        Task<bool> Save(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);

        // Authorize CustomerPreOwnedVehicleLoanInspection Entry
        Task<bool> Verify(CustomerPreOwnedVehicleLoanInspectionViewModel _CustomerPreOwnedVehicleLoanInspectionViewModel);


        // Return CustomerPreOwnedVehicleLoanInspectionViewModel (Used For Reject View)
        Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetRejectedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey);

        // Return CustomerPreOwnedVehicleLoanInspectionViewModel (Used For Unverified View)
        Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetUnVerifiedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey);

        // Return CustomerPreOwnedVehicleLoanInspectionViewModel (Used For Unverified View)
        Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetVerifiedEntry(int _CustomerVehicleLoanCollateralDetailPrmKey);

        // Return Empty CustomerPreOwnedVehicleLoanInspectionViewModel (Used For Create)
        Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetViewModelForCreate(int _CustomerVehicleLoanCollateralDetailPrmKey);

       

        // Return Rejected Entries
        Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerPreOwnedVehicleLoanInspection Table Which Are Not Authorized
        Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerPreOwnedVehicleLoanInspection Table For Modification
        Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CustomerPreOwnedVehicleLoanInspection Table 
        Task<IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel>> GetIndexWithCreateModifyOperationStatus();






    }
}
