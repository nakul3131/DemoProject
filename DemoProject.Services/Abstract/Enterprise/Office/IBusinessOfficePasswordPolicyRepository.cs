using DemoProject.Services.ViewModel.Enterprise.Office;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficePasswordPolicyRepository
    {
        // Amend BusinessOfficePasswordPolicy Delete Entry - If Entry Rejected
        Task<bool> Amend(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);

        // Closed BusinessOfficePasswordPolicy - Only For Verified Entry
        Task<bool> Closed(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);

        // Delete BusinessOfficePasswordPolicy - Only For Rejected Entry
        Task<bool> Delete(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);

        // Reject BusinessOfficePasswordPolicy Entry
        Task<bool> Reject(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);

        // Save BusinessOfficePasswordPolicy New Entry
        Task<bool> Save(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);

        // Authorize BusinessOfficePasswordPolicy Entry
        Task<bool> Verify(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel);
    }
}
