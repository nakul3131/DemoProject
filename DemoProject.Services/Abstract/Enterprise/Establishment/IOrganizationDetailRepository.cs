using DemoProject.Services.ViewModel.Enterprise.Establishment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IOrganizationDetailRepository
    {
        Task<AuthorizedSharesCapitalViewModel> GetAuthorizedSharesCapitalEntry(string _entryType);

        Task<OrganizationFundViewModel> GetFundEntry(string _entryType);

        Task<OrganizationLoanTypeViewModel> GetLoanTypeEntry(string _entryType);



        Task<IEnumerable<OrganizationContactDetailViewModel>> GetContactDetailEntries(string _entryType);

        Task<IEnumerable<OrganizationGSTRegistrationDetailViewModel>> GetGSTRegistrationDetailEntries(string _entryType);

        Task<IEnumerable<OrganizationFundViewModel>> GetFundEntries(string _entryType);

        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetLoanTypeEntries(string _entryType);



        Task<IEnumerable<AuthorizedSharesCapitalViewModel>> GetAuthorizedSharesCapitalIndex();

        Task<IEnumerable<OrganizationIndexViewModel>> GetFundIndex(string _entryType);

        Task<IEnumerable<OrganizationLoanTypeViewModel>> GetLoanTypeIndex(string _entryType);



        void GetOrganizationAllDefaultValues(OrganizationViewModel _organizationViewModel, string _entryStatus);

        void GetOrganizationDefaultValues(OrganizationViewModel _organizationViewModel , string _entryStatus);

        void GetAuthorizedSharesCapitalDefaultValues(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string _entryStatus);

        void GetContactDetailDefaultValues(OrganizationContactDetailViewModel _contactDetailViewModel, string _entryStatus);

        void GetGSTRegistrationDetailDefaultValues(OrganizationGSTRegistrationDetailViewModel _organizationGSTRegistrationDetailViewModel, string _entryStatus);

        void GetFundDefaultValues(OrganizationFundViewModel _organizationFundViewModel, string _entryStatus);

        void GetLoanTypeDefaultValues(OrganizationLoanTypeViewModel _loanTypeViewModel, string _entryStatus);

    }
}
