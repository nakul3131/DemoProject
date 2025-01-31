using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficeCoopRegistrationRepository
    {
        // Amend BusinessOfficeCoopRegistration Delete Entry - If Entry Rejected
        Task<bool> Amend(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);
        
        // Delete BusinessOfficeCoopRegistration - Only For Rejected Entry
        Task<bool> Delete(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);

        // Save BusinessOfficeCoopRegistration Modification New Entry
        Task<bool> Modify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);

        // Reject BusinessOfficeCoopRegistration Entry
        Task<bool> Reject(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);
        
        // Save BusinessOfficeCoopRegistration New Entry
        Task<bool> Save(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);

        // Authorize BusinessOfficeCoopRegistration Entry
        Task<bool> Verify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel);
    }
}
