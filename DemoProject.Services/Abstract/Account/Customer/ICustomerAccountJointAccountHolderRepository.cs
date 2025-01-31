using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerJointAccountHolderRepository
    {
        // Amend CustomerJointAccountHolder Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);

        // Delete CustomerJointAccountHolder - Only For Rejected Entry
        Task<bool> Delete(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);

        // Save CustomerJointAccountHolder Modification New Entry
        Task<bool> Modify(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);

        // Reject CustomerJointAccountHolder Entry
        Task<bool> Reject(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);

        // Save CustomerJointAccountHolder New Entry
        Task<bool> Save(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);

        // Authorize CustomerJointAccountHolder Entry
        Task<bool> Verify(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel);
    

        // Return Rejected Entries
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerJointAccountHolder Table Which Are Not Authorized
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerJointAccountHolder Table For Modification
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return Record From CustomerJointAccountHolder Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey);

        // Return Record From CustomerJointAccountHolder Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);

       
    }
}
