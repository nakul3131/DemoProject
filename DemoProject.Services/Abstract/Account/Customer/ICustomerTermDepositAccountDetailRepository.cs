using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerTermDepositAccountDetailRepository
    {
        // Amend CustomerTermDepositAccountDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerTermDepositAccountDetailViewModel _customerDepositAccountAgentViewModel);

        // Delete CustomerTermDepositAccountDetail - Only For Rejected Entry
        Task<bool> Delete(CustomerTermDepositAccountDetailViewModel _customerDepositAccountAgentViewModel);

        // Reject CustomerTermDepositAccountDetail Entry
        Task<bool> Reject(CustomerTermDepositAccountDetailViewModel _customerDepositAccountAgentViewModel);

        // Save CustomerTermDepositAccountDetail New Entry
        Task<bool> Save(CustomerTermDepositAccountDetailViewModel _customerDepositAccountAgentViewModel);

        // Authorize CustomerTermDepositAccountDetail Entry
        Task<bool> Verify(CustomerTermDepositAccountDetailViewModel _customerDepositAccountAgentViewModel);


        // Return Rejected Entries
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerTermDepositAccountDetail Table Which Are Not Authorized
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerTermDepositAccountDetail Table For Modification
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfVerifiedEntries();


        // Return Rejected Entry
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetRejectedEntries(int _customerTermDepositAccountDetailPrmKey);

        // Return Record From CustomerTermDepositAccountDetail Table By Given Parameter (i.e. GeneralLedgerPrmKey)
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetUnVerifiedEntries(int _customerTermDepositAccountDetailPrmKey);

        // Return Record From CustomerTermDepositAccountDetail Table By Given Parameter (i.e. GeneralLedgerId)
        Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetVerifiedEntries(int _customerTermDepositAccountDetailPrmKey);

        
    }
}
