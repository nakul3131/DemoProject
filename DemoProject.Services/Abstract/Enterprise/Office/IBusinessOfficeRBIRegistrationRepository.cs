using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficeRBIRegistrationRepository
    {
        // Amend BusinessOfficeRBIRegistration Delete Entry - If Entry Rejected
        Task<bool> Amend(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);

        // Delete BusinessOfficeRBIRegistration - Only For Rejected Entry
        Task<bool> Delete(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);

        // Modify BusinessOfficeRBIRegistration Old Entry
        Task<bool> Modify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);

        // Reject BusinessOfficeRBIRegistration Entry
        Task<bool> Reject(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);

        // Save BusinessOfficeRBIRegistration New Entry
        Task<bool> Save(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);

        // Authorize BusinessOfficeRBIRegistration Entry
        Task<bool> Verify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel);
    }
}
