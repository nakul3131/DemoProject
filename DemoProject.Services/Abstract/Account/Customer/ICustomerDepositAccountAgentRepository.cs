using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerDepositAccountAgentRepository
    {
        // Amend CustomerDepositAccountAgent Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel);

        // Delete CustomerDepositAccountAgent - Only For Rejected Entry
        Task<bool> Delete(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel);

        // Reject CustomerDepositAccountAgent Entry
        Task<bool> Reject(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel);

        // Save CustomerDepositAccountAgent New Entry
        Task<bool> Save(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel);

        // Authorize CustomerDepositAccountAgent Entry
        Task<bool> Verify(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerDepositAccountAgent Table Which Are Not Authorized
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerDepositAccountAgent Table For Modification
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetRejectedEntries(int _customerDepositAccountAgentId);

        // Return Record From CustomerDepositAccountAgent Table By Given Parameter (i.e. GeneralLedgerId)
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetUnVerifiedEntries(int _customerDepositAccountAgentId);

        // Return Record From CustomerDepositAccountAgent Table By Given Parameter (i.e. GeneralLedgerId)
        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetVerifiedEntries(int _customerDepositAccountAgentId);

      
    }
}
