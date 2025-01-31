using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IPeriodCodeRepository
    {
        // Amend PeriodCode Entry
        Task<bool> Amend(PeriodCodeViewModel _periodCodeViewModel); 

        // Delete PeriodCode
        Task<bool> Delete(PeriodCodeViewModel _periodCodeViewModel);

        // Return PeriodCode Rejected Entries
        Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From PeriodCode Table Which Are Not Authorized
        Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From PeriodCode Table Which Are Authorized
        Task<IEnumerable<PeriodCodeViewModel>> GetIndexOfVerifiedEntries();
         
        // Return Empty PeriodCode Table 
        Task<IEnumerable<PeriodCodeViewModel>> GetIndexWithCreateModifyOperationStatus();

        short GetPrmKeyByFinancialCycleId(Guid _financialCycleId); 

        // Return Rejected PeriodCode Entries
        Task<IEnumerable<PeriodCodeViewModel>> GetRejectedEntries(short _financialCyclePrmKey); 

        // Return UnVerified PeriodCode Entries
        Task<IEnumerable<PeriodCodeViewModel>> GetUnverifiedEntries(short _financialCyclePrmKey);

        // Return Verified PeriodCode Entries
        Task<IEnumerable<PeriodCodeViewModel>> GetVerifiedEntries(short _financialCyclePrmKey);

        // Return Empty PeriodCode (Used For Create)
        Task<PeriodCodeViewModel> GetViewModelForCreate(short _financialCyclePrmKey);

        // Return PeriodCode (Used For Reject View)
        Task<PeriodCodeViewModel> GetViewModelForReject(short _financialCyclePrmKey);

        // Return PeriodCode (Used For Unverified View)
        Task<PeriodCodeViewModel> GetViewModelForUnverified(short _financialCyclePrmKey);

        // Return PeriodCode (Used For Unverified View)
        Task<PeriodCodeViewModel> GetViewModelForVerified(short _financialCyclePrmKey);

        // Reject PeriodCode Entry
        Task<bool> Reject(PeriodCodeViewModel _periodCodeViewModel);

        // Save PeriodCode New Entry
        Task<bool> Save(PeriodCodeViewModel _periodCodeViewModel);

        // Authorize PeriodCode Entry
        Task<bool> Verify(PeriodCodeViewModel _periodCodeViewModel);
    }
}
