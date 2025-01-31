using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerVehicleLoanCollateralDetailRepository
    {

        // Return LoanCustomerAccountPrmKey By schemeId
        int GetPrmKeyByCustomerLoanAccountPrmKey(int _customerLoanAccountPrmKey);


        // Amend CustomerVehicleLoanCollateralDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);

        // Delete CustomerVehicleLoanCollateralDetail - Only For Rejected Entry
        Task<bool> Delete(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);

        // Save CustomerVehicleLoanCollateralDetail Modification New Entry
        Task<bool> Modify(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);

        // Reject CustomerVehicleLoanCollateralDetail Entry
        Task<bool> Reject(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);

        // Save CustomerVehicleLoanCollateralDetail New Entry
        Task<bool> Save(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);

        // Authorize CustomerVehicleLoanCollateralDetail Entry
        Task<bool> Verify(CustomerVehicleLoanCollateralDetailViewModel _CustomerVehicleLoanCollateralDetailViewModel);




        // Return CustomerVehicleLoanCollateralDetailViewModel (Used For Reject View)
        Task<CustomerVehicleLoanCollateralDetailViewModel> GetRejectedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerVehicleLoanCollateralDetailViewModel (Used For Unverified View)
        Task<CustomerVehicleLoanCollateralDetailViewModel> GetUnVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerVehicleLoanCollateralDetailViewModel (Used For Unverified View)
        Task<CustomerVehicleLoanCollateralDetailViewModel> GetVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Return Empty CustomerVehicleLoanCollateralDetailViewModel (Used For Create)
        Task<CustomerVehicleLoanCollateralDetailViewModel> GetViewModelForCreate(int _CustomerLoanAccountPrmKey);


        // Return Rejected Entries
        Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerVehicleLoanCollateralDetail Table Which Are Not Authorized
        Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerVehicleLoanCollateralDetail Table For Modification
        Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CustomerVehicleLoanCollateralDetailViewModel>> GetIndexWithCreateModifyOperationStatus();


    }
}
