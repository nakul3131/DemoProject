using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerLoanAccountVehicleInsuranceDetailRepository
    {
        // Amend CustomerLoanAccountVehicleInsuranceDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);

        // Delete CustomerLoanAccountVehicleInsuranceDetail - Only For Rejected Entry
        Task<bool> Delete(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);

        // Save CustomerLoanAccountVehicleInsuranceDetail Modification New Entry
        Task<bool> Modify(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);

        // Reject CustomerLoanAccountVehicleInsuranceDetail Entry
        Task<bool> Reject(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);

        // Save CustomerLoanAccountVehicleInsuranceDetail New Entry
        Task<bool> Save(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);

        // Authorize CustomerLoanAccountVehicleInsuranceDetail Entry
        Task<bool> Verify(CustomerVehicleLoanInsuranceDetailViewModel _CustomerLoanAccountVehicleInsuranceDetailViewModel);


        // Return CustomerLoanAccountVehicleInsuranceDetailViewModel (Used For Reject View)
        Task<CustomerVehicleLoanInsuranceDetailViewModel> GetRejectedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerLoanAccountVehicleInsuranceDetailViewModel (Used For Unverified View)
        Task<CustomerVehicleLoanInsuranceDetailViewModel> GetUnVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerLoanAccountVehicleInsuranceDetailViewModel (Used For verified View)
        Task<CustomerVehicleLoanInsuranceDetailViewModel> GetVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Return Empty CustomerLoanAccountVehicleInsuranceDetailViewModel (Used For Create)
        Task<CustomerVehicleLoanInsuranceDetailViewModel> GetViewModelForCreate(int _CustomerLoanAccountPrmKey);


        // Return Rejected Entries
        Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerLoanAccountVehicleInsuranceDetail Table Which Are Not Authorized
        Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerLoanAccountVehicleInsuranceDetail Table For Modification
        Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel>> GetIndexWithCreateModifyOperationStatus();

    }
}
