using DemoProject.Services.ViewModel.Enterprise.Establishment;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IOrganizationDetailDbContextRepository
    {
        bool AttachOrganizationData(OrganizationViewModel _organizationViewModel, string _entryType);

        bool AttachAuthorizedSharesCapitalData(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string _entryType);

        bool AttachOrganizationContactDetailData(OrganizationContactDetailViewModel _organizationContactDetailViewModel, string _entryType);

        bool AttachOrganizationGSTRegistrationDetailData(OrganizationGSTRegistrationDetailViewModel _organizationGSTRegistrationDetailViewModel, string _entryType);

        bool AttachOrganizationFundData(OrganizationFundViewModel _organizationFundViewModel, string _entryType);

        bool AttachOrganizationLoanTypeData(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel, string _entryType);

        Task<bool> SaveData();

    }
}
