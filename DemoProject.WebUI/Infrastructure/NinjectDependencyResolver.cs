using Ninject;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using DemoProject.Services.Concrete.SMS;
using DemoProject.Services.Abstract.Security.Log;
using DemoProject.Services.Concrete.Security.Log;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Concrete.Security.Users;
using DemoProject.Services.Abstract.Security.Login;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Concrete.Enterprise.Establishment;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Concrete.Enterprise.Office;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.Concrete.Enterprise.Schedule;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Concrete.Account.GL;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Concrete.PersonInformation;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.Concrete.Security.UserRoles;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Concrete.Account.Layout;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Concrete.Management.Conference;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Concrete.Account.Customer;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Concrete.Account.Transaction;
using DemoProject.Services.Concrete.Account.Master;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Concrete.Account.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Security.Parameter;
using DemoProject.Services.Abstract.Management.Parameter;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.Services.Abstract.SMS;
using DemoProject.Services.Concrete.Management.Master;
using DemoProject.Services.Concrete.Management.Servant;
using DemoProject.Services.Concrete.PersonInformation.Master;
using DemoProject.Services.Concrete.Management.Parameter;
using DemoProject.Services.Concrete.PersonInformation.Parameter;
using DemoProject.Services.Concrete.Security.Parameter;
using DemoProject.Services.Concrete.Security;
using DemoProject.Services.Concrete.Security.Master;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Concrete.Configuration;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Concrete.MachineLearning;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Concrete.Enterprise;
using DemoProject.Services.Concrete.Account;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Concrete.Management;

namespace DemoProject.WebUI.Infrastructure
{
    // 
    // Summary:
    //          It create instances of the classes it needs to service requests.
    //          The NinjectDependencyResolver class implements the IDependencyResolver interface, which is part of the 
    //          System.Mvc namespace and which the MVC Framework uses to get the objects it needs.

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBindings();
        }
        //
        //  Summary
        //          The MVC Framework will call the GetService or GetServices methods when it needs an instance of a class to 
        //          service an incoming request. The job of a dependency resolver is to create that instance, a task that  
        //          we perform by calling the Ninject TryGet and GetAll methods. The TryGet method works like the Get method i used 
        //          previously, but it returns null when there is no suitable binding rather than throwing an exception. The GetAll 
        //          method supports multiple bindings for a single type, which is used when there are several different implementation objects available.
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        //
        //  Summary
        //          In the AddBindings method, we use the Bind and To methods to configure up the relationship between the interface and the implementation class
        private void AddBindings()
        {
            // Note - ** Main Module Name In CAPITAL and Parent Module Name In UPPER Case and Child Module Name In UpperCamel case **

            // @@@@@@@@@@@@@@@@@@@@@@@@@ A C C O U N T @@@@@@@@@@@@@@@@@@@@@@@@@
            kernel.Bind<IAccountDetailRepository>().To<EFAccountDetailRepository>();

            // ####################################### Customer ####################################### 
            kernel.Bind<ICustomerAccountDbContextRepository>().To<EFCustomerAccountDbContextRepository>();
            kernel.Bind<IBeneficiaryRepository>().To<EFBeneficiaryRepository>();
            kernel.Bind<ICustomerAccountRepository>().To<EFCustomerAccountRepository>();
            kernel.Bind<ICustomerDepositAccountRepository>().To<EFCustomerDepositAccountRepository>();
            kernel.Bind<ICustomerDepositAccountAgentRepository>().To<EFCustomerDepositAccountAgentRepository>();
            kernel.Bind<ICustomerTermDepositAccountDetailRepository>().To<EFCustomerTermDepositAccountDetailRepository>();
            kernel.Bind<ICustomerJointAccountHolderRepository>().To<EFCustomerJointAccountHolderRepository>();
            kernel.Bind<ICustomerAccountNomineeRepository>().To<EFCustomerAccountNomineeRepository>();
            kernel.Bind<ICustomerAccountNomineeGuardianRepository>().To<EFCustomerAccountNomineeGuardianRepository>();
            kernel.Bind<ISharesCapitalCustomerAccountRepository>().To<EFSharesCapitalCustomerAccountRepository>();
            kernel.Bind<ICustomerAccountTurnOverLimitRepository>().To<EFCustomerAccountTurnOverLimitRepository>();
            kernel.Bind<ICustomerAccountDetailRepository>().To<EFCustomerAccountDetailRepository>();
            kernel.Bind<ICustomerAccountFacilityRepository>().To<EFCustomerAccountFacilityRepository>();
            kernel.Bind<ICustomerAccountInterestRateRepository>().To<EFCustomerAccountInterestRateRepository>();
            kernel.Bind<ICustomerAccountDocumentRepository>().To<EFCustomerAccountDocumentRepository>();
            kernel.Bind<IAgentRepository>().To<EFAgentRepository>();
            kernel.Bind<ICustomerSharesCapitalAccountRepository>().To<EFCustomerSharesCapitalAccountRepository>();
            kernel.Bind<ICustomerDetailRepository>().To<EFCustomerDetailRepository>();
           


            kernel.Bind<ILoanCustomerAccountRepository>().To<EFLoanCustomerAccountRepository>();
            kernel.Bind<ICustomerLoanAccountRepository>().To<EFCustomerLoanAccountRepository>();
            kernel.Bind<ICustomerLoanAccountGuarantorDetailRepository>().To<EFCustomerLoanAccountGuarantorDetailRepository>();
            kernel.Bind<ICustomerPreOwnedVehicleLoanInspectionRepository>().To<EFCustomerPreOwnedVehicleLoanInspectionRepository>();
            kernel.Bind<ICustomerLoanAccountVehicleInsuranceDetailRepository>().To<EFCustomerLoanAccountVehicleInsuranceDetailRepository>();
            kernel.Bind<ICustomerPreOwnedVehicleLoanPhotoRepository>().To<EFCustomerPreOwnedVehicleLoanPhotoRepository>();
            kernel.Bind<ICustomerLoanFieldInvestigationRepository>().To<EFCustomerLoanFieldInvestigationRepository>();
            kernel.Bind<ICustomerVehicleLoanCollateralDetailRepository>().To<EFCustomerVehicleLoanCollateralDetailRepository>();

            //// ##################### Transaction #####################
            kernel.Bind<ITransactionRepository>().To<EFTransactionRepository>();
            kernel.Bind<ITransactionCashDenominationRepository>().To<EFTransactionCashDenominationRepository>();
            kernel.Bind<ITransactionCustomerAccountRepository>().To<EFTransactionCustomerAccountRepository>();
            kernel.Bind<ITransactionGeneralLedgerRepository>().To<EFTransactionGeneralLedgerRepository>();
            kernel.Bind<IOpeningBalanceRepository>().To<EFOpeningBalanceRepository>();
            kernel.Bind<ITransactionDividendRepository>().To<EFTransactionDividendRepository>();
            kernel.Bind<IPersonStatusRepository>().To<EFPersonStatusRepository>();
            kernel.Bind<ITransactionDetailRepository>().To<EFTransactionDetailRepository>();
            kernel.Bind<ITransactionDbContextRepository>().To<EFTransactionDbContextRepository>();

            // ####################################### FinancialCycleAndPeriod #######################################
            kernel.Bind<IFinancialCycleRepository>().To<EFFinancialCycleRepository>();
            kernel.Bind<IPeriodCodeRepository>().To<EFPeriodCodeRepository>();

            // ####################################### GL #######################################
            kernel.Bind<IGeneralLedgerRepository>().To<EFGeneralLedgerRepository>();
            kernel.Bind<IGeneralLedgerDbContextRepository>().To<EFGeneralLedgerDbContextRepository>();
            kernel.Bind<IGeneralLedgerDetailRepository>().To<EFGeneralLedgerDetailRepository>();
            kernel.Bind<IGeneralLedgerBusinessOfficeRepository>().To<EFGeneralLedgerBusinessOfficeRepository>();
            kernel.Bind<IGeneralLedgerCurrencyRepository>().To<EFGeneralLedgerCurrencyRepository>();
            kernel.Bind<IGeneralLedgerCustomerTypeRepository>().To<EFGeneralLedgerCustomerTypeRepository>();
            kernel.Bind<IGeneralLedgerTransactionTypeRepository>().To<EFGeneralLedgerTransactionTypeRepository>();

            // ####################################### Layout #######################################
            kernel.Bind<ISchemeDetailRepository>().To<EFSchemeDetailRepository>();
            //kernel.Bind<IFundSchemeRepository>().To<EFFundSchemeRepository>();

            //######################### Master ######################################
            kernel.Bind<IFixedAssetItemRepository>().To<EFFixedAssetItemRepository>();
            kernel.Bind<ILoanSchemeRepository>().To<EFLoanSchemeRepository>();

            kernel.Bind<ISharesCapitalSchemeRepository>().To<EFSharesCapitalSchemeRepository>();
            kernel.Bind<ISchemeDbContextRepository>().To<EFSchemeDbContextRepository>();
            // ####################################### DepositScheme #######################################

            kernel.Bind<IDepositSchemeRepository>().To<EFDepositSchemeRepository>();

            // ####################################### SHARES #######################################
            kernel.Bind<ISharesApplicationRepository>().To<EFSharesApplicationRepository>();

            // @@@@@@@@@@@@@@@@@@@@@@@@ C O M M U N I C A T I O N @@@@@@@@@@@@@@@@@@@@@@@@
            
            // @@@@@@@@@@@@@@@@@@@@@@@@ C O N F I G U R A T I O N @@@@@@@@@@@@@@@@@@@@@@@@
                        kernel.Bind<IConfigurationDetailRepository>().To<EFConfigurationDetailRepository>();

            // @@@@@@@@@@@@@@@@@@@@@@@@ E N T E R P R I S E @@@@@@@@@@@@@@@@@@@@@@@@
            kernel.Bind<IEnterpriseDetailRepository>().To<EFEnterpriseDetailRepository>();

            // ####################################### Capital #######################################
            kernel.Bind<IAuthorizedSharesCapitalRepository>().To<EFAuthorizedSharesCapitalRepository>();

            // ############################# Establishment #############################
            kernel.Bind<IOrganizationRepository>().To<EFOrganizationRepository>();
            kernel.Bind<IOrganizationDetailRepository>().To<EFOrganizationDetailRepository>();
            kernel.Bind<IOrganizationDetailDbContextRepository>().To<EFOrganizationDetailDbContextRepository>();
            kernel.Bind<IOrganizationContactDetailRepository>().To<EFOrganizationContactDetailRepository>();
            kernel.Bind<IOrganizationFundRepository>().To<EFOrganizationFundRepository>();
            kernel.Bind<IOrganizationGSTRegistrationDetailRepository>().To<EFOrganizationGSTRegistrationDetailRepository>();
            kernel.Bind<IOrganizationLoanTypeRepository>().To<EFOrganizationLoanTypeRepository>();

            // ############################# Office #############################
            kernel.Bind<IBusinessOfficeAccountNumberRepository>().To<EFBusinessOfficeAccountNumberRepository>();
            kernel.Bind<IBusinessOfficeApplicationNumberRepository>().To<EFBusinessOfficeApplicationNumberRepository>();
            kernel.Bind<IBusinessOfficeCoopRegistrationRepository>().To<EFBusinessOfficeCoopRegistrationRepository>();
            kernel.Bind<IBusinessOfficeCurrencyRepository>().To<EFBusinessOfficeCurrencyRepository>();
            kernel.Bind<IBusinessOfficeDetailRepository>().To<EFBusinessOfficeDetailRepository>();
            kernel.Bind<IBusinessOfficeMenuRepository>().To<EFBusinessOfficeMenuRepository>();
            kernel.Bind<IBusinessOfficePasswordPolicyRepository>().To<EFBusinessOfficePasswordPolicyRepository>();
            kernel.Bind<IBusinessOfficeRBIRegistrationRepository>().To<EFBusinessOfficeRBIRegistrationRepository>();
            kernel.Bind<IBusinessOfficeRepository>().To<EFBusinessOfficeRepository>();
            kernel.Bind<IBusinessOfficeSpecialPermissionRepository>().To<EFBusinessOfficeSpecialPermissionRepository>();
            kernel.Bind<IBusinessOfficeTransactionLimitRepository>().To<EFBusinessOfficeTransactionLimitRepository>();
            kernel.Bind<IBusinessOfficeTransactionParameterRepository>().To<EFBusinessOfficeTransactionParameterRepository>();
            kernel.Bind<IBusinessOfficeMemberNumberRepository>().To<EFBusinessOfficeMemberNumberRepository>();
            kernel.Bind<IBusinessOfficeCustomerNumberRepository>().To<EFBusinessOfficeCustomerNumberRepository>();
            kernel.Bind<IOfficeDetailRepository>().To<EFOfficeDetailRepository>();
            kernel.Bind<IBusinessOfficeDbContextRepository>().To<EFBusinessOfficeDbContextRepository>();

            // ############################# Schedule #############################
            kernel.Bind<IOfficeScheduleRepository>().To<EFOfficeScheduleRepository>();
            kernel.Bind<IWorkingScheduleRepository>().To<EFWorkingScheduleRepository>();

            // @@@@@@@@@@@@@@@@@@@@@@@ H U M A N     R E S O U R C E @@@@@@@@@@@@@@@@@@@@@@@
            kernel.Bind<IEmployeeRepository>().To<EFEmployeeRepository>();
            kernel.Bind<IEmployeeDbContextRepository>().To<EFEmployeeDbContextRepository>();
            kernel.Bind<IEmployeeDepartmentRepository>().To<EFEmployeeDepartmentRepository>();
            kernel.Bind<IEmployeeDesignationRepository>().To<EFEmployeeDesignationRepository>();
            kernel.Bind<IChequeBookMasterRepository>().To<EFChequeBookMasterRepository>();
            kernel.Bind<IEmployeeDetailRepository>().To<EFEmployeeDetailRepository>();
            kernel.Bind<IEmployeeDocumentRepository>().To<EFEmployeeDocumentRepository>();
            kernel.Bind<IEmployeePerformanceRatingRepository>().To<EFEmployeePerformanceRatingRepository>();
            kernel.Bind<IEmployeePhotoRepository>().To<EFEmployeePhotoRepository>();
            kernel.Bind<IEmployeeSalaryStructureRepository>().To<EFEmployeeSalaryStructureRepository>();
            kernel.Bind<IEmployeeWorkingScheduleRepository>().To<EFEmployeeWorkingScheduleRepository>();
            kernel.Bind<IServantDetailRepository>().To<EFServantDetailRepository>();

            kernel.Bind<IContentItemRepository>().To<EFContentItemRepository>();
            kernel.Bind<IEvaluationSectionRepository>().To<EFEvaluationSectionRepository>();
            kernel.Bind<IEvaluationSectorContentItemRepository>().To<EFEvaluationSectorContentItemRepository>();

            // @@@@@@@@@@@@@@@@ M A C H I N E     L E A R N I N G @@@@@@@@@@@@@@@@
            kernel.Bind<IMLDetailRepository>().To<EFMLDetailRepository>();


            // @@@@@@@@@@@@@@@@ M A N A G E M E N T @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            kernel.Bind<IManagementDetailRepository>().To<EFManagementDetailRepository>();

            // ##################### Conference #####################
            kernel.Bind<IMeetingRepository>().To<EFMeetingRepository>();
            kernel.Bind<IMeetingAgendaRepository>().To<EFMeetingAgendaRepository>();
            kernel.Bind<IMeetingAllowanceRepository>().To<EFMeetingAllowanceRepository>();
            kernel.Bind<IMeetingInviteeBoardOfDirectorRepository>().To<EFMeetingInviteeBoardOfDirectorRepository>();
            kernel.Bind<IMeetingInviteeMemberRepository>().To<EFMeetingInviteeMemberRepository>();
            kernel.Bind<IMeetingNoticeRepository>().To<EFMeetingNoticeRepository>();
            kernel.Bind<IMinuteOfMeetingAgendaRepository>().To<EFMinuteOfMeetingAgendaRepository>();
            kernel.Bind<IMinuteOfMeetingAgendaSpokespersonRepository>().To<EFMinuteOfMeetingAgendaSpokespersonRepository>();

            // ############################# Notification #############################
            // ############################# Event #############################
            kernel.Bind<IEventMasterRepository>().To<EFEventMasterRepository>();

            // @@@@@@@@@@@@@@@@@@@ M A S T E R @@@@@@@@@@@@@@@@@@@
            kernel.Bind<IScheduleRepository>().To<EFScheduleRepository>();
            kernel.Bind<IScheduleFrequencyRepository>().To<EFScheduleFrequencyRepository>();

            // ##################### Address #####################
            kernel.Bind<ICountryRepository>().To<EFCountryRepository>();
            kernel.Bind<IVillageTownCityRepository>().To<EFVillageTownCityRepository>();
            kernel.Bind<IStateRepository>().To<EFStateRepository>();
            kernel.Bind<IContinentRepository>().To<EFContinentRepository>();
            kernel.Bind<IDistrictRepository>().To<EFDistrictRepository>();
            kernel.Bind<IDivisionRepository>().To<EFDivisionRepository>();
            kernel.Bind<ITalukaRepository>().To<EFTalukaRepository>();
            kernel.Bind<ICenterTradingEntityDetailsRepository>().To<EFCenterTradingEntityDetailsRepository>();
            kernel.Bind<ICenterOccupationRepository>().To<EFCenterOccupationRepository>();
            kernel.Bind<ICenterDemographicDetailRepository>().To<EFCenterDemographicDetailRepository>();
            kernel.Bind<ICenterISOCodeRepository>().To<EFCenterISOCodeRepository>();
            kernel.Bind<ICountryAdditionalDetailRepository>().To<EFCountryAdditionalDetailRepository>();

            // ##################### Bank Detail #####################

            // ##################### Enterprise #####################
            kernel.Bind<IPowerAndFunctionRepository>().To<EFPowerAndFunctionRepository>();
            kernel.Bind<IBoardOfDirectorRepository>().To<EFBoardOfDirectorRepository>();
            kernel.Bind<IBoardOfDirectorPowerAndFunctionRepository>().To<EFBoardOfDirectorPowerAndFunctionRepository>();

            // ##################### General #####################
            kernel.Bind<IAgendaRepository>().To<EFAgendaRepository>();
            kernel.Bind<IAgendaMeetingTypeRepository>().To<EFAgendaMeetingTypeRepository>();
            kernel.Bind<IDepartmentRepository>().To<EFDepartmentRepository>();
            kernel.Bind<IDesignationRepository>().To<EFDesignationRepository>();


            // ##################### Vehicle #####################
            kernel.Bind<IVehicleMakeRepository>().To<EFVehicleMakeRepository>();
            kernel.Bind<IVehicleModelRepository>().To<EFVehicleModelRepository>();
            kernel.Bind<IVehicleVariantRepository>().To<EFVehicleVariantRepository>();
            kernel.Bind<IVehicleSupplierRepository>().To<EFVehicleSupplierRepository>();
            kernel.Bind<IVehicleDbContextRepository>().To<EFVehicleDbContextRepository>();
            // *************************************** Security *********************************************

            // ##################### ACCOUNT #####################
            // ##################### Layout #####################
            kernel.Bind<IDepositSchemeParameterRepository>().To<EFDepositSchemeParameterRepository>();
            kernel.Bind<ILoanSchemeParameterRepository>().To<EFLoanSchemeParameterRepository>();
            kernel.Bind<ISharesCapitalSchemeParameterRepository>().To<EFSharesCapitalSchemeParameterRepository>();
            kernel.Bind<IAccountParameterDetailRepository>().To<EFAccountParameterDetailRepository>();

            // ##################### Transaction #####################
            kernel.Bind<ITransactionParameterRepository>().To<EFTransactionParameterRepository>();

            // ##################### Board Of Director #####################
            kernel.Bind<IBoardOfDirectorParameterRepository>().To<EFBoardOfDirectorParameterRepository>();

            kernel.Bind<IAssuranceDeedFormatRepository>().To<EFAssuranceDeedFormatRepository>();

            // ##################### LEGAL #####################
            kernel.Bind<ISharesCapitalActParameterRepository>().To<EFSharesCapitalActParameterRepository>();
            kernel.Bind<ISharesCapitalByLawsParameterRepository>().To<EFSharesCapitalByLawsParameterRepository>();
            kernel.Bind<IIncomeTaxActParameterRepository>().To<EFIncomeTaxActParameterRepository>();

            // ##################### Person #####################
            kernel.Bind<IPersonInformationParameterRepository>().To<EFPersonInformationParameterRepository>();
            kernel.Bind<IPersonInformationParameterDocumentTypeRepository>().To<EFPersonInformationParameterDocumentTypeRepository>();
            kernel.Bind<IPersonInformationParameterNoticeTypeRepository>().To<EFPersonInformationParameterNoticeTypeRepository>();
            kernel.Bind<IPersonInformationParameterDetailRepository>().To<EFPersonInformationParameterDetailRepository>();
            kernel.Bind<IPersonDbContextRepository>().To<EFPersonDbContextRepository>();

            // ##################### Security #####################
            kernel.Bind<IUserAuthenticationParameterRepository>().To<EFUserAuthenticationParameterRepository>();
            kernel.Bind<IUserProfileDetailRepository>().To<EFUserProfileDetailRepository>();
            kernel.Bind<IUserProfileDbContextRepository>().To<EFUserProfileDbContextRepository>();

            // @@@@@@@@@@@@@@@@@@ P E R S O N     I N F O R M A T I O N @@@@@@@@@@@@@@@@@@
            kernel.Bind<IPersonDetailRepository>().To<EFPersonDetailRepository>();

            kernel.Bind<IForeignerPersonRepository>().To<EFForeignerPersonRepository>();
            kernel.Bind<IGuardianPersonRepository>().To<EFGuardianPersonRepository>();
            kernel.Bind<IPersonAdditionalDetailRepository>().To<EFPersonAdditionalDetailRepository>();
            kernel.Bind<IPersonAdditionalIncomeDetailRepository>().To<EFPersonAdditionalIncomeDetailRepository>();
            kernel.Bind<IPersonAgricultureAssetRepository>().To<EFPersonAgricultureAssetRepository>();
            kernel.Bind<IPersonBankDetailRepository>().To<EFPersonBankDetailRepository>();
            kernel.Bind<IPersonBoardOfDirectorRelationRepository>().To<EFPersonBoardOfDirectorRelationRepository>();
            kernel.Bind<IPersonBorrowingDetailRepository>().To<EFPersonBorrowingDetailRepository>();
            kernel.Bind<IPersonChronicDiseaseRepository>().To<EFPersonChronicDiseaseRepository>();
            kernel.Bind<IPersonContactDetailsRepository>().To<EFPersonContactDetailsRepository>();
            kernel.Bind<IPersonCourtCaseRepository>().To<EFPersonCourtCaseRepository>();
            kernel.Bind<IPersonCommoditiesAssetRepository>().To<EFPersonCommoditiesAssetRepository>();
            kernel.Bind<IPersonCreditRatingRepository>().To<EFPersonCreditRatingRepository>();
            kernel.Bind<IPersonDeathRepository>().To<EFPersonDeathRepository>();
            kernel.Bind<IPersonDeathDocumentRepository>().To<EFPersonDeathDocumentRepository>();
            kernel.Bind<IPersonEmploymentDetailRepository>().To<EFPersonEmploymentDetailRepository>();
            kernel.Bind<IPersonFamilyDetailRepository>().To<EFPersonFamilyDetailRepository>();
            kernel.Bind<IPersonFinancialAssetRepository>().To<EFPersonFinancialAssetRepository>();
            kernel.Bind<IPersonImmovableAssetRepository>().To<EFPersonImmovableAssetRepository>();
            kernel.Bind<IPersonIncomeTaxDetailRepository>().To<EFPersonIncomeTaxDetailRepository>();
            kernel.Bind<IPersonInsuranceDetailRepository>().To<EFPersonInsuranceDetailRepository>();
            kernel.Bind<IPersonMachineryAssetRepository>().To<EFPersonMachineryAssetRepository>();
            kernel.Bind<IPersonMasterRepository>().To<EFPersonMasterRepository>();
            kernel.Bind<IPersonMovableAssetRepository>().To<EFPersonMovableAssetRepository>();
            kernel.Bind<IPersonPhotoSignRepository>().To<EFPersonPhotoSignRepository>();
            kernel.Bind<IPersonRepository>().To<EFPersonRepository>();
            kernel.Bind<IPersonInformationDetailRepository>().To<EFPersonInformationDetailRepository>();
            kernel.Bind<IPersonSMSAlertRepository>().To<EFPersonSMSAlertRepository>();
            kernel.Bind<IPersonSocialMediaRepository>().To<EFPersonSocialMediaRepository>();
            kernel.Bind<IPersonGSTReturnDocumentRepository>().To<EFPersonGSTReturnDocumentRepository>();
            kernel.Bind<IPersonPrefixRepository>().To<EFPersonPrefixRepository>();
            kernel.Bind<IPersonHomeBranchRepository>().To<EFPersonHomeBranchRepository>();
            kernel.Bind<IPersonGroupMasterRepository>().To<EFPersonGroupMasterRepository>();

            // @@@@@@@@@@@@@@@@@@@@@@@@@@ S E C U R I T Y  @@@@@@@@@@@@@@@@@@@@@@@@@@
            kernel.Bind<ISecurityDetailRepository>().To<EFSecurityDetailRepository>();
            kernel.Bind<ICryptoAlgorithmRepository>().To<EFCryptoAlgorithmRepository>();

            //*************** AuthenticationProvider *******
            kernel.Bind<IAuthProviderRepository>().To<EFAuthProviderRepository>();

            //*************** Log **************************
            kernel.Bind<IAccountRecoveryLogRepository>().To<EFAccountRecoveryLogRepository>();
            kernel.Bind<IActivityLogRepository>().To<EFActivityLogRepository>();
            kernel.Bind<ILoginLogRepository>().To<EFLoginLogRepository>();
            kernel.Bind<IInvalidLoginLogRepository>().To<EFInvalidLoginLogRepository>();

            //*************** Login **************************
            kernel.Bind<IEmergencyScreenRepository>().To<EFEmergencyScreenRepository>();

            //*************** Password **************************
            kernel.Bind<IPasswordPolicyRepository>().To<EFPasswordPolicyRepository>();

            //*************** RoleProfile **************************
            kernel.Bind<IRoleProfileRepository>().To<EFRoleProfileRepository>();
            kernel.Bind<IRoleProfileDbContextRepository>().To<EFRoleProfileDbContextRepository>();
            kernel.Bind<IRoleProfileDetailRepository>().To<EFRoleProfileDetailRepository>();
            kernel.Bind<IRoleProfileSpecialPermissionRepository>().To<EFRoleProfileSpecialPermissionRepository>();
            kernel.Bind<IRoleProfileMenuRepository>().To<EFRoleProfileMenuRepository>();
            kernel.Bind<IRoleProfileGeneralLedgerRepository>().To<EFRoleProfileGeneralLedgerRepository>();
            kernel.Bind<IRoleProfileBusinessOfficeRepository>().To<EFRoleProfileBusinessOfficeRepository>();
            kernel.Bind<IRoleProfileTransactionLimitRepository>().To<EFRoleProfileTransactionLimitRepository>();

            //*************** Users ************************
            kernel.Bind<IUserAuthenticationTokenRepository>().To<EFUserAuthenticationTokenRepository>();
            kernel.Bind<IUserProfileAccessibilityRepository>().To<EFUserProfileAccessibilityRepository>();
            kernel.Bind<IUserProfileBusinessOfficeRepository>().To<EFUserProfileBusinessOfficeRepository>();
            kernel.Bind<IUserProfileCurrencyRepository>().To<EFUserProfileCurrencyRepository>();
            kernel.Bind<IUserProfileGeneralLedgerRepository>().To<EFUserProfileGeneralLedgerRepository>();
            kernel.Bind<IUserProfileHomeBranchRepository>().To<EFUserProfileHomeBranchRepository>();
            kernel.Bind<IUserProfileLoginDeviceRepository>().To<EFUserProfileLoginDeviceRepository>();
            kernel.Bind<IUserProfileMenuRepository>().To<EFUserProfileMenuRepository>();
            kernel.Bind<IUserProfilePasswordPolicyRepository>().To<EFUserProfilePasswordPolicyRepository>();
            kernel.Bind<IUserProfilePasswordRepository>().To<EFUserProfilePasswordRepository>();
            kernel.Bind<IUserProfilePhotoRepository>().To<EFUserProfilePhotoRepository>();
            kernel.Bind<IUserProfileRepository>().To<EFUserProfileRepository>();
            kernel.Bind<IUserProfileSpecialPermissionRepository>().To<EFUserProfileSpecialPermissionRepository>();
            kernel.Bind<IUserProfileTransactionLimitRepository>().To<EFUserProfileTransactionLimitRepository>();
            kernel.Bind<IUserRoleProfileRepository>().To<EFUserRoleProfileRepository>();
            kernel.Bind<IUserProfileGroupRepository>().To<EFUserProfileGroupRepository>();
            kernel.Bind<IUserProfileHomeBusinessOfficeRepository>().To<EFUserProfileHomeBusinessOfficeRepository>();
            kernel.Bind<IUserProfileIdentityRepository>().To<EFUserProfileIdentityRepository>();
            kernel.Bind<IUserStatusRepository>().To<EFUserStatusRepository>();

            //*************************************** SMS **************************************************
            kernel.Bind<ISMSDetailRepository>().To<EFSMSDetailRepository>();
            kernel.Bind<ISMSRepository>().To<EFSMSRepository>();

            //************************************ByLawsLoanScheduleParameter************************
            kernel.Bind<IByLawsLoanScheduleParameterRepository>().To<EFByLawsLoanScheduleParameterRepository>();

        }
    }
}
