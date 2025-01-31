using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerLoanFieldInvestigationRepository
    {
        // Amend CustomerLoanFieldInvestigation Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);

        // Delete CustomerLoanFieldInvestigation - Only For Rejected Entry
        Task<bool> Delete(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);

        // Return Empty CenterTradingEntityDetail Table 
        Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected Entries
        Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerLoanFieldInvestigation Table Which Are Not Authorized
        Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerLoanFieldInvestigation Table For Modification
        Task<IEnumerable<CustomerLoanFieldInvestigationViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CustomerLoanFieldInvestigationViewModel (Used For Create)
        Task<CustomerLoanFieldInvestigationViewModel> GetViewModelForCreate(int _CustomerLoanAccountPrmKey);

        // Return CustomerLoanFieldInvestigationViewModel (Used For Reject View)
        Task<CustomerLoanFieldInvestigationViewModel> GetRejectedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerLoanFieldInvestigationViewModel (Used For Unverified View)
        Task<CustomerLoanFieldInvestigationViewModel> GetUnVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Return CustomerLoanFieldInvestigationViewModel (Used For Unverified View)
        Task<CustomerLoanFieldInvestigationViewModel> GetVerifiedEntry(int _CustomerLoanAccountPrmKey);

        // Save CustomerLoanFieldInvestigation Modification New Entry
        Task<bool> Modify(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);

        // Reject CustomerLoanFieldInvestigation Entry
        Task<bool> Reject(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);

        // Save CustomerLoanFieldInvestigation New Entry
        Task<bool> Save(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);

        // Authorize CustomerLoanFieldInvestigation Entry
        Task<bool> Verify(CustomerLoanFieldInvestigationViewModel _CustomerLoanFieldInvestigationViewModel);
    }
}
