using AutoMapper;
using DemoProject.Domain.Entities.Enterprise.Establishment;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Wrapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{

   public class EFOrganizationDetailDbContextRepository : IOrganizationDetailDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        private Organization organization = new Organization();
        private byte organizationPrmKey = 0;
        private EntityState entityState;

        public EFOrganizationDetailDbContextRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            accountDetailRepository = _accountDetailRepository;
        }
        public bool AttachOrganizationData(OrganizationViewModel _organizationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_organizationViewModel, _entryType);

                // Get PrmKey By Id
                _organizationViewModel.AreaOfOperationPrmKey = enterpriseDetailRepository.GetAreaOfOperationPrmKeyById(_organizationViewModel.AreaOfOperationId);
                _organizationViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_organizationViewModel.CenterId);
                _organizationViewModel.CoopClassification = enterpriseDetailRepository.GetCoopSocietyClassPrmKeyById(_organizationViewModel.CoopClassificationId);
                _organizationViewModel.CoopSubClassification = enterpriseDetailRepository.GetCoopSocietySubClassPrmKeyById(_organizationViewModel.CoopSubClassificationId);
                _organizationViewModel.LanguageOfBookPrmKey = configurationDetailRepository.GetAppLanguagePrmKeyById(_organizationViewModel.LanguageId);
                _organizationViewModel.OrganizationType = enterpriseDetailRepository.GetFinancialOrganizationTypePrmKeyById(_organizationViewModel.OrganizationTypeId);
                _organizationViewModel.RBIClassification = enterpriseDetailRepository.GetCoopSocietyClassPrmKeyById(_organizationViewModel.RBIClassificationId);
                _organizationViewModel.RBISubClassification = enterpriseDetailRepository.GetCoopSocietySubClassPrmKeyById(_organizationViewModel.RBISubClassificationId);

                // Organization
                Organization organization = Mapper.Map<Organization>(_organizationViewModel);
                OrganizationMakerChecker organizationMakerChecker = Mapper.Map<OrganizationMakerChecker>(_organizationViewModel);

                //OrganizationTranslation
                OrganizationTranslation organizationTranslation = Mapper.Map<OrganizationTranslation>(_organizationViewModel);
                OrganizationTranslationMakerChecker organizationTranslationMakerChecker = Mapper.Map<OrganizationTranslationMakerChecker>(_organizationViewModel);

               
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    organizationPrmKey = _organizationViewModel.OrganizationPrmKey;
                    organization.PrmKey = organizationPrmKey;
                    organizationTranslation.PrmKey = _organizationViewModel.OrganizationTranslationPrmKey;

                    //Organization
                    context.Organizations.Attach(organization);
                    context.Entry(organization).State = entityState;

                    context.OrganizationMakerCheckers.Attach(organizationMakerChecker);
                    context.Entry(organizationMakerChecker).State = EntityState.Added;
                    organization.OrganizationMakerCheckers.Add(organizationMakerChecker);

                    //OrganizationTranslation
                    context.OrganizationTranslations.Attach(organizationTranslation);
                    context.Entry(organizationTranslation).State = entityState;
                    organization.OrganizationTranslations.Add(organizationTranslation);

                    context.OrganizationTranslationMakerCheckers.Attach(organizationTranslationMakerChecker);
                    context.Entry(organizationTranslationMakerChecker).State = EntityState.Added;
                    organizationTranslation.OrganizationTranslationMakerCheckers.Add(organizationTranslationMakerChecker);
                }
                else
                {
                    context.OrganizationMakerCheckers.Attach(organizationMakerChecker);
                    context.Entry(organizationMakerChecker).State = EntityState.Added;

                    context.OrganizationTranslationMakerCheckers.Attach(organizationTranslationMakerChecker);
                    context.Entry(organizationTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        public bool AttachAuthorizedSharesCapitalData(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_authorizedSharesCapitalViewModel, _entryType);
                
                //AuthorizedSharesCapital
                AuthorizedSharesCapital authorizedSharesCapital = Mapper.Map<AuthorizedSharesCapital>(_authorizedSharesCapitalViewModel);
                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerChecker = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    //AuthorizedSharesCapital
                    context.AuthorizedSharesCapitals.Attach(authorizedSharesCapital);
                    context.Entry(authorizedSharesCapital).State = entityState;

                    context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                    context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;
                    authorizedSharesCapital.AuthorizedSharesCapitalMakerCheckers.Add(authorizedSharesCapitalMakerChecker);
                }
                else
                {
                    context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                    context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachOrganizationContactDetailData(OrganizationContactDetailViewModel _organizationContactDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_organizationContactDetailViewModel, _entryType);
                //Mapping
                OrganizationContactDetail organizationContactDetail = Mapper.Map<OrganizationContactDetail>(_organizationContactDetailViewModel);
                OrganizationContactDetailMakerChecker organizationContactDetailMakerChecker = Mapper.Map<OrganizationContactDetailMakerChecker>(_organizationContactDetailViewModel);

                //Get PrmKey By Id
                organizationContactDetail.ContactTypePrmKey = personDetailRepository.GetContactTypePrmKeyById(_organizationContactDetailViewModel.ContactTypeId);
                organizationContactDetail.ContactGroupPrmKey = personDetailRepository.GetContactGroupPrmKeyById(_organizationContactDetailViewModel.ContactGroupId);

                if (_entryType == StringLiteralValue.Create)
                {
                    //OrganizationContactDetail
                    context.OrganizationContactDetails.Attach(organizationContactDetail);
                    context.Entry(organizationContactDetail).State = EntityState.Added;

                    context.OrganizationContactDetailMakerCheckers.Attach(organizationContactDetailMakerChecker);
                    context.Entry(organizationContactDetailMakerChecker).State = EntityState.Added;
                    organizationContactDetail.OrganizationContactDetailMakerCheckers.Add(organizationContactDetailMakerChecker);
                }
                else
                {
                    context.OrganizationContactDetailMakerCheckers.Attach(organizationContactDetailMakerChecker);
                    context.Entry(organizationContactDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachOrganizationGSTRegistrationDetailData(OrganizationGSTRegistrationDetailViewModel _organizationGSTRegistrationDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_organizationGSTRegistrationDetailViewModel, _entryType);
                //Mapping
                OrganizationGSTRegistrationDetail organizationGSTRegistrationDetail = Mapper.Map<OrganizationGSTRegistrationDetail>(_organizationGSTRegistrationDetailViewModel);
                OrganizationGSTRegistrationDetailMakerChecker organizationGSTRegistrationDetailMakerChecker = Mapper.Map<OrganizationGSTRegistrationDetailMakerChecker>(_organizationGSTRegistrationDetailViewModel);

                //Get PrmKey By Id
                organizationGSTRegistrationDetail.GSTRegistrationTypePrmKey = accountDetailRepository.GetGSTRegistrationTypePrmKeyById(_organizationGSTRegistrationDetailViewModel.GSTRegistrationTypeId);
                organizationGSTRegistrationDetail.GSTReturnPeriodicityPrmKey = accountDetailRepository.GetGSTReturnPeriodicityPrmKeyById(_organizationGSTRegistrationDetailViewModel.GSTReturnPeriodicityId);
                organizationGSTRegistrationDetail.StatePrmKey = personDetailRepository.GetCenterPrmKeyById(_organizationGSTRegistrationDetailViewModel.StateId);

                if (_entryType == StringLiteralValue.Create)
                {
                    //OrganizationGSTRegistrationDetail
                    context.OrganizationGSTRegistrationDetails.Attach(organizationGSTRegistrationDetail);
                    context.Entry(organizationGSTRegistrationDetail).State = EntityState.Added;

                    context.OrganizationGSTRegistrationDetailMakerCheckers.Attach(organizationGSTRegistrationDetailMakerChecker);
                    context.Entry(organizationGSTRegistrationDetailMakerChecker).State = EntityState.Added;
                    organizationGSTRegistrationDetail.OrganizationGSTRegistrationDetailMakerCheckers.Add(organizationGSTRegistrationDetailMakerChecker);
                }
                else
                {
                    context.OrganizationGSTRegistrationDetailMakerCheckers.Attach(organizationGSTRegistrationDetailMakerChecker);
                    context.Entry(organizationGSTRegistrationDetailMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachOrganizationFundData(OrganizationFundViewModel _organizationFundViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_organizationFundViewModel, _entryType);
                //Mapping
                //OrganizationFund
                OrganizationFund organizationFund = Mapper.Map<OrganizationFund>(_organizationFundViewModel);
                OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(_organizationFundViewModel);

                //OrganizationFundTranlation
                OrganizationFundTranslation organizationFundTranslation = Mapper.Map<OrganizationFundTranslation>(_organizationFundViewModel);
                OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(_organizationFundViewModel);

                //Get PrmKey By Id
                organizationFund.FundPrmKey = accountDetailRepository.GetFundPrmKeyById(_organizationFundViewModel.FundId);

                if (_entryType == StringLiteralValue.Create)
                {
                    //OrganizationFund
                    context.OrganizationFunds.Attach(organizationFund);
                    context.Entry(organizationFund).State = EntityState.Added;

                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;
                    organizationFund.OrganizationFundMakerCheckers.Add(organizationFundMakerChecker);

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslations.Attach(organizationFundTranslation);
                    context.Entry(organizationFundTranslation).State = EntityState.Added;
                    organizationFund.OrganizationFundTranslations.Add(organizationFundTranslation);

                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;
                    organizationFundTranslation.OrganizationFundTranslationMakerCheckers.Add(organizationFundTranslationMakerChecker);
                }
                else
                {
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;
                    
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachOrganizationLoanTypeData(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_organizationLoanTypeViewModel, _entryType);
                
                //Mapping
                //OrganizationLoanType
                OrganizationLoanType organizationLoanType = Mapper.Map<OrganizationLoanType>(_organizationLoanTypeViewModel);
                OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(_organizationLoanTypeViewModel);

                //OrganizationLoanTypeTranslation
                OrganizationLoanTypeTranslation organizationLoanTypeTranslation = Mapper.Map<OrganizationLoanTypeTranslation>(_organizationLoanTypeViewModel);
                OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(_organizationLoanTypeViewModel);

                //Get PrmKey By Id
                organizationLoanType.LoanTypePrmKey = accountDetailRepository.GetLoanTypePrmKeyById(_organizationLoanTypeViewModel.LoanTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    //OrganizationLoanType
                    context.OrganizationLoanTypes.Attach(organizationLoanType);
                    context.Entry(organizationLoanType).State = EntityState.Added;

                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeMakerCheckers.Add(organizationLoanTypeMakerChecker);

                    //OrganizationLoanTypeTranslations
                    context.OrganizationLoanTypeTranslations.Attach(organizationLoanTypeTranslation);
                    context.Entry(organizationLoanTypeTranslation).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeTranslations.Add(organizationLoanTypeTranslation);

                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;
                    organizationLoanTypeTranslation.OrganizationLoanTypeTranslationMakerCheckers.Add(organizationLoanTypeTranslationMakerChecker);
                }
                else
                {
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;

                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveData()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


    }
}
