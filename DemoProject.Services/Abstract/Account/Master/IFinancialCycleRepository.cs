using DemoProject.Services.ViewModel.Account.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IFinancialCycleRepository
    {
        // Amend FinancialCycle Delete Entry - If Entry Rejected
        Task<bool> Amend(FinancialCycleViewModel _financialCycleViewModel);

        // Delete FinancialCycle - Only For Rejected Entry
        Task<bool> Delete(FinancialCycleViewModel _financialCycleViewModel);

        // Return Rejected Entries
        Task<IEnumerable<FinancialCycleViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From FinancialCycle Table Which Are Not Authorized
        Task<IEnumerable<FinancialCycleViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Rejected Entry
        Task<FinancialCycleViewModel> GetRejectedEntry(Guid _financialCycleId); 

        //bool GetUniqueDesignationName(string _nameOfDesignation);

        // Return Record From FinancialCycle Table By Given Parameter (i.e. FinancialCycle)
        Task<FinancialCycleViewModel> GetUnVerifiedEntry(Guid _financialCycleId);

        // Reject FinancialCycle Entry
        Task<bool> Reject(FinancialCycleViewModel _financialCycleViewModel);

        // Save FinancialCycle New Entry
        Task<bool> Save(FinancialCycleViewModel _financialCycleViewModel);

        // Authorize FinancialCycle Entry
        Task<bool> Verify(FinancialCycleViewModel _financialCycleViewModel); 
    }
}
