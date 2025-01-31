using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.ViewModel.Enterprise.Establishment;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IOrganizationLoanTypeRepository
    {


        // Return Autherize Entry
        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterIndex();
        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterUnverifiedIndex();
        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterRejectedIndex();
        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetloanSanctionAuthorityIndex();
        Task<ByLawsLoanScheduleParameterViewModel> GetVerifiedEntries(byte _loanTypePrmKey);
        Task<ByLawsLoanScheduleParameterViewModel> GetUnVerifiedEntries(byte _loanTypePrmKey);

        // Amend OrganizationLoanType Entry
        Task<bool> Amend(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel);

        // Delete OrganizationLoanType
        Task<bool> Delete(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel);

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject OrganizationLoanType Entry
        Task<bool> Reject(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel);

        // Save OrganizationLoanType New Entry
        Task<bool> Save(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel);

        // Authorize OrganizationLoanType Entry
        Task<bool> Verify(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel);

    }
}
