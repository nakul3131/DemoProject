using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Establishment;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IOrganizationFundRepository
    {
        // Amend OrganizationFund Entry
        Task<bool> Amend(OrganizationFundViewModel _organizationFundViewModel);

        // Delete OrganizationFund
        Task<bool> Delete(OrganizationFundViewModel _organizationFundViewModel);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject OrganizationFund Entry
        Task<bool> Reject(OrganizationFundViewModel _organizationFundViewModel);

        // Save OrganizationFund New Entry
        Task<bool> Save(OrganizationFundViewModel _organizationFundViewModel);

        // Authorize OrganizationFund Entry
        Task<bool> Verify(OrganizationFundViewModel _organizationFundViewModel);

    }
}
