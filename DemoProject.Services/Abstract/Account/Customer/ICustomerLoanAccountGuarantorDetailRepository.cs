using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerLoanAccountGuarantorDetailRepository
    {

        // Amend CustomerLoanAccountGuarantorDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Delete CustomerLoanAccountGuarantorDetail - Only For Rejected Entry
        Task<bool> Delete(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Save CustomerLoanAccountGuarantorDetail Modification New Entry
        Task<bool> Modify(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Reject CustomerLoanAccountGuarantorDetail Entry
        Task<bool> Reject(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Save CustomerLoanAccountGuarantorDetail New Entry
        Task<bool> Save(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Authorize CustomerLoanAccountGuarantorDetail Entry
        Task<bool> Verify(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail);

        // Return Rejected Entries
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CustomerLoanAccountGuarantorDetail Table Which Are Not Authorized
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CustomerLoanAccountGuarantorDetail Table For Modification
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetRejectedEntries(long _customerLoanAccountPrmKey);

        // Return Record From CustomerLoanAccountGuarantorDetail Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetUnVerifiedEntries(long _customerLoanAccountPrmKey);

        // Return Record From CustomerLoanAccountGuarantorDetail Table By Given Parameter (i.e. SchemeId)
        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetVerifiedEntries(long _customerLoanAccountPrmKey);

        
    }
}
