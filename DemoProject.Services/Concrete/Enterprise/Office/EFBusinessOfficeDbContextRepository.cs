using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.Security;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeDbContextRepository : IBusinessOfficeDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        private BusinessOffice businessOffice = new BusinessOffice();
        private EntityState entityState;
        short businessOfficePrmKey = 0;

        public EFBusinessOfficeDbContextRepository(RepositoryConnection _connection, IOfficeDetailRepository _officeDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            officeDetailRepository = _officeDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            accountDetailRepository = _accountDetailRepository;
        }

        public bool AttachBusinessOfficeData(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeViewModel, _entryType);

                BusinessOffice businessOffice = Mapper.Map<BusinessOffice>(_businessOfficeViewModel);
                BusinessOfficeMakerChecker businessOfficeMakerChecker = Mapper.Map<BusinessOfficeMakerChecker>(_businessOfficeViewModel);

                BusinessOfficeTranslation businessOfficeTranslation = Mapper.Map<BusinessOfficeTranslation>(_businessOfficeViewModel);
                BusinessOfficeTranslationMakerChecker businessOfficeTranslationMakerChecker = Mapper.Map<BusinessOfficeTranslationMakerChecker>(_businessOfficeViewModel);

                businessOffice.ClearingBusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeViewModel.ClearingBusinessOfficeId);
                businessOffice.ParentBusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeViewModel.ParentBusinessOfficeId);
                businessOffice.RegionalOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeViewModel.RegionalOfficeId);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOffice.PrmKey = _businessOfficeViewModel.BusinessOfficePrmKey;
                    businessOfficeTranslation.PrmKey = _businessOfficeViewModel.BusinessOfficeTranslationPrmKey;
                    businessOfficePrmKey = _businessOfficeViewModel.BusinessOfficePrmKey;
                    context.BusinessOffices.Attach(businessOffice);
                    context.Entry(businessOffice).State = entityState;

                    context.BusinessOfficeMakerCheckers.Attach(businessOfficeMakerChecker);
                    context.Entry(businessOfficeMakerChecker).State = EntityState.Added;
                    businessOffice.BusinessOfficeMakerCheckers.Add(businessOfficeMakerChecker);

                    context.BusinessOfficeTranslations.Attach(businessOfficeTranslation);
                    context.Entry(businessOfficeTranslation).State = entityState;
                    businessOffice.BusinessOfficeTranslations.Add(businessOfficeTranslation);

                    context.BusinessOfficeTranslationMakerCheckers.Attach(businessOfficeTranslationMakerChecker);
                    context.Entry(businessOfficeTranslationMakerChecker).State = EntityState.Added;
                    businessOfficeTranslation.BusinessOfficeTranslationMakerCheckers.Add(businessOfficeTranslationMakerChecker);

                }
                else
                {
                    context.BusinessOfficeMakerCheckers.Attach(businessOfficeMakerChecker);
                    context.Entry(businessOfficeMakerChecker).State = EntityState.Added;

                    context.BusinessOfficeTranslationMakerCheckers.Attach(businessOfficeTranslationMakerChecker);
                    context.Entry(businessOfficeTranslationMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachBusinessOfficeModificationData(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeViewModel, _entryType);

                // BusinessOfficeModification
                BusinessOfficeModification businessOfficeModification = Mapper.Map<BusinessOfficeModification>(_businessOfficeViewModel);
                BusinessOfficeModificationMakerChecker businessOfficeModificationMakerChecker = Mapper.Map<BusinessOfficeModificationMakerChecker>(_businessOfficeViewModel);
                businessOfficeModification.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOfficeModification.PrmKey = _businessOfficeViewModel.BusinessOfficeModificationPrmKey;

                    // BusinessOfficeModification

                    context.BusinessOfficeModifications.Attach(businessOfficeModification);
                    context.Entry(businessOfficeModification).State = entityState;

                    context.BusinessOfficeModificationMakerCheckers.Attach(businessOfficeModificationMakerChecker);
                    context.Entry(businessOfficeModificationMakerChecker).State = EntityState.Added;
                    businessOfficeModification.BusinessOfficeModificationMakerCheckers.Add(businessOfficeModificationMakerChecker);

                }
                else
                {
                    context.BusinessOfficeModificationMakerCheckers.Attach(businessOfficeModificationMakerChecker);
                    context.Entry(businessOfficeModificationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachBusinessOfficeDetailData(BusinessOfficeDetailViewModel _businessOfficeDetail, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeDetail, _entryType);

                // BusinessOfficeDetail
                BusinessOfficeDetail businessOfficeDetail = Mapper.Map<BusinessOfficeDetail>(_businessOfficeDetail);
                BusinessOfficeDetailMakerChecker businessOfficeDetailMakerChecker = Mapper.Map<BusinessOfficeDetailMakerChecker>(_businessOfficeDetail);

                businessOfficeDetail.PrmKey = _businessOfficeDetail.BusinessOfficeDetailPrmKey;
                businessOfficeDetail.BusinessOfficePrmKey = businessOfficePrmKey;

                businessOfficeDetail.BusinessOfficeTypePrmKey = enterpriseDetailRepository.GetBusinessOfficeTypePrmKeyById(_businessOfficeDetail.BusinessOfficeTypeId);
                businessOfficeDetail.BusinessNaturePrmKey = enterpriseDetailRepository.GetBusinessNaturePrmKeyById(_businessOfficeDetail.BusinessNatureId);
                businessOfficeDetail.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_businessOfficeDetail.CenterId);
                businessOfficeDetail.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_businessOfficeDetail.LocalCurrencyId);
                businessOfficeDetail.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeDetail.GeneralLedgerId);
                businessOfficeDetail.OfficeSchedulePrmKey = enterpriseDetailRepository.GetOfficeSchedulePrmKeyById(_businessOfficeDetail.OfficeScheduleId);
                businessOfficeDetail.LanguagePrmKey = configurationDetailRepository.GetAppLanguagePrmKeyById(_businessOfficeDetail.RegionalLanguageId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    businessOfficeDetail.PrmKey = _businessOfficeDetail.BusinessOfficeDetailPrmKey;

                    context.BusinessOfficeDetails.Attach(businessOfficeDetail);
                    context.Entry(businessOfficeDetail).State = entityState;
                    businessOffice.BusinessOfficeDetails.Add(businessOfficeDetail);

                    context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                    context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;
                    businessOfficeDetail.BusinessOfficeDetailMakerCheckers.Add(businessOfficeDetailMakerChecker);
                }
                else
                {
                    context.BusinessOfficeDetailMakerCheckers.Attach(businessOfficeDetailMakerChecker);
                    context.Entry(businessOfficeDetailMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCooprativeRegistrationData(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeCoopRegistrationViewModel, _entryType);
                // BusinessOfficeCoopRegistration
                BusinessOfficeCoopRegistration businessOfficeCoopRegistration = Mapper.Map<BusinessOfficeCoopRegistration>(_businessOfficeCoopRegistrationViewModel);
                BusinessOfficeCoopRegistrationMakerChecker businessOfficeCoopRegistrationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationMakerChecker>(_businessOfficeCoopRegistrationViewModel);
                businessOfficeCoopRegistration.PrmKey = _businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationPrmKey;
                businessOfficeCoopRegistration.BusinessOfficePrmKey = businessOfficePrmKey;

                // BusinessOfficeCoopRegistrationTranslation
                BusinessOfficeCoopRegistrationTranslation businessOfficeCoopRegistrationTranslation = Mapper.Map<BusinessOfficeCoopRegistrationTranslation>(_businessOfficeCoopRegistrationViewModel);
                BusinessOfficeCoopRegistrationTranslationMakerChecker businessOfficeCoopRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeCoopRegistrationTranslationMakerChecker>(_businessOfficeCoopRegistrationViewModel);


                businessOfficeCoopRegistrationTranslation.PrmKey = _businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationTranslationPrmKey;


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    // BusinessOfficeCoopRegistrationTranslation                 
                    context.BusinessOfficeCoopRegistrationTranslations.Attach(businessOfficeCoopRegistrationTranslation);
                    context.Entry(businessOfficeCoopRegistrationTranslation).State = entityState;
                    businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationTranslations.Add(businessOfficeCoopRegistrationTranslation);

                    context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                    context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;
                    businessOfficeCoopRegistrationTranslation.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Add(businessOfficeCoopRegistrationTranslationMakerChecker);

                    // BusinessOfficeCoopRegistration
                    context.BusinessOfficeCoopRegistrations.Attach(businessOfficeCoopRegistration);
                    context.Entry(businessOfficeCoopRegistration).State = entityState;
                    businessOffice.BusinessOfficeCoopRegistrations.Add(businessOfficeCoopRegistration);

                    context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                    context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;
                    businessOfficeCoopRegistration.BusinessOfficeCoopRegistrationMakerCheckers.Add(businessOfficeCoopRegistrationMakerChecker);

                }
                else
                {

                    context.BusinessOfficeCoopRegistrationMakerCheckers.Attach(businessOfficeCoopRegistrationMakerChecker);
                    context.Entry(businessOfficeCoopRegistrationMakerChecker).State = EntityState.Added;

                    context.BusinessOfficeCoopRegistrationTranslationMakerCheckers.Attach(businessOfficeCoopRegistrationTranslationMakerChecker);
                    context.Entry(businessOfficeCoopRegistrationTranslationMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachRBIRegistrationData(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeRBIRegistrationViewModel, _entryType);
                // BusinessOfficeRBIRegistration
                BusinessOfficeRBIRegistration businessOfficeRBIRegistration = Mapper.Map<BusinessOfficeRBIRegistration>(_businessOfficeRBIRegistrationViewModel);
                BusinessOfficeRBIRegistrationMakerChecker businessOfficeRBIRegistrationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                // BusinessOfficeRBIRegistrationTranslation
                BusinessOfficeRBIRegistrationTranslation businessOfficeRBIRegistrationTranslation = Mapper.Map<BusinessOfficeRBIRegistrationTranslation>(_businessOfficeRBIRegistrationViewModel);
                BusinessOfficeRBIRegistrationTranslationMakerChecker businessOfficeRBIRegistrationTranslationMakerChecker = Mapper.Map<BusinessOfficeRBIRegistrationTranslationMakerChecker>(_businessOfficeRBIRegistrationViewModel);

                businessOfficeRBIRegistration.IBANCheckDigitAlgorithm = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_businessOfficeRBIRegistrationViewModel.IBANCheckDigitAlgorithmId);
                businessOfficeRBIRegistration.BBANCheckDigitAlgorithm = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_businessOfficeRBIRegistrationViewModel.BBANCheckDigitAlgorithmId);
                businessOfficeRBIRegistration.UserDefinedAlgorithm = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_businessOfficeRBIRegistrationViewModel.UserDefinedAlgorithmId);
                businessOfficeRBIRegistration.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOfficeRBIRegistration.PrmKey = _businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationPrmKey;
                    businessOfficeRBIRegistrationTranslation.PrmKey = _businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationTranslationPrmKey;

                    // BusinessOfficeRBIRegistration

                    context.BusinessOfficeRBIRegistrations.Attach(businessOfficeRBIRegistration);
                    context.Entry(businessOfficeRBIRegistration).State = entityState;
                    businessOffice.BusinessOfficeRBIRegistrations.Add(businessOfficeRBIRegistration);

                    context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                    context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;
                    businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationMakerCheckers.Add(businessOfficeRBIRegistrationMakerChecker);

                    // BusinessOfficeRBIRegistrationTranslation

                    context.BusinessOfficeRBIRegistrationTranslations.Attach(businessOfficeRBIRegistrationTranslation);
                    context.Entry(businessOfficeRBIRegistrationTranslation).State = entityState;
                    businessOfficeRBIRegistration.BusinessOfficeRBIRegistrationTranslations.Add(businessOfficeRBIRegistrationTranslation);

                    context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                    context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;
                    businessOfficeRBIRegistrationTranslation.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Add(businessOfficeRBIRegistrationTranslationMakerChecker);

                }
                else
                {
                    context.BusinessOfficeRBIRegistrationMakerCheckers.Attach(businessOfficeRBIRegistrationMakerChecker);
                    context.Entry(businessOfficeRBIRegistrationMakerChecker).State = EntityState.Added;

                    context.BusinessOfficeRBIRegistrationTranslationMakerCheckers.Attach(businessOfficeRBIRegistrationTranslationMakerChecker);
                    context.Entry(businessOfficeRBIRegistrationTranslationMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPasswordPolicyData(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficePasswordPolicyViewModel, _entryType);

                // BusinessOfficePasswordPolicy
                BusinessOfficePasswordPolicy businessOfficePasswordPolicy = Mapper.Map<BusinessOfficePasswordPolicy>(_businessOfficePasswordPolicyViewModel);
                BusinessOfficePasswordPolicyMakerChecker businessOfficePasswordPolicyMakerChecker = Mapper.Map<BusinessOfficePasswordPolicyMakerChecker>(_businessOfficePasswordPolicyViewModel);

                businessOfficePasswordPolicy.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(_businessOfficePasswordPolicyViewModel.PasswordPolicyId);
                businessOfficePasswordPolicy.BusinessOfficePrmKey = businessOfficePrmKey;
                if (_entryType == StringLiteralValue.Create)
                {
                    context.BusinessOfficePasswordPolicies.Attach(businessOfficePasswordPolicy);
                    context.Entry(businessOfficePasswordPolicy).State = EntityState.Added;
                    businessOffice.BusinessOfficePasswordPolicies.Add(businessOfficePasswordPolicy);

                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                    businessOfficePasswordPolicy.BusinessOfficePasswordPolicyMakerCheckers.Add(businessOfficePasswordPolicyMakerChecker);

                }
                else
                {
                    context.BusinessOfficePasswordPolicyMakerCheckers.Attach(businessOfficePasswordPolicyMakerChecker);
                    context.Entry(businessOfficePasswordPolicyMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachBusinessOfficeMenuData(BusinessOfficeMenuViewModel _businessOfficeMenuViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeMenuViewModel, _entryType);
                // BusinessOfficeMenu
                BusinessOfficeMenu businessOfficeMenu = Mapper.Map<BusinessOfficeMenu>(_businessOfficeMenuViewModel);
                BusinessOfficeMenuMakerChecker businessOfficeMenuMakerChecker = Mapper.Map<BusinessOfficeMenuMakerChecker>(_businessOfficeMenuViewModel);

                businessOfficeMenu.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(_businessOfficeMenuViewModel.MenuId);
                businessOfficeMenu.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.BusinessOfficeMenus.Attach(businessOfficeMenu);
                    context.Entry(businessOfficeMenu).State = EntityState.Added;
                    businessOffice.BusinessOfficeMenus.Add(businessOfficeMenu);

                    context.BusinessOfficeMenuMakerCheckers.Attach(businessOfficeMenuMakerChecker);
                    context.Entry(businessOfficeMenuMakerChecker).State = EntityState.Added;
                    businessOfficeMenu.BusinessOfficeMenuMakerCheckers.Add(businessOfficeMenuMakerChecker);
                }
                else
                {
                    context.BusinessOfficeMenuMakerCheckers.Attach(businessOfficeMenuMakerChecker);
                    context.Entry(businessOfficeMenuMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSpecialPermissionData(BusinessOfficeSpecialPermissionViewModel _businessOfficeSpecialPermissionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeSpecialPermissionViewModel, _entryType);
                // BusinessOfficeSpecialPermission
                BusinessOfficeSpecialPermission businessOfficeSpecialPermission = Mapper.Map<BusinessOfficeSpecialPermission>(_businessOfficeSpecialPermissionViewModel);
                BusinessOfficeSpecialPermissionMakerChecker businessOfficeSpecialPermissionMakerChecker = Mapper.Map<BusinessOfficeSpecialPermissionMakerChecker>(_businessOfficeSpecialPermissionViewModel);

                businessOfficeSpecialPermission.SpecialPermissionPrmKey = securityDetailRepository.GetSpecialPermissionPrmKeyById(_businessOfficeSpecialPermissionViewModel.SpecialPermissionId);
                businessOfficeSpecialPermission.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {

                    context.BusinessOfficeSpecialPermissions.Attach(businessOfficeSpecialPermission);
                    context.Entry(businessOfficeSpecialPermission).State = EntityState.Added;
                    businessOffice.BusinessOfficeSpecialPermissions.Add(businessOfficeSpecialPermission);

                    context.BusinessOfficeSpecialPermissionMakerCheckers.Attach(businessOfficeSpecialPermissionMakerChecker);
                    context.Entry(businessOfficeSpecialPermissionMakerChecker).State = EntityState.Added;
                    businessOfficeSpecialPermission.BusinessOfficeSpecialPermissionMakerCheckers.Add(businessOfficeSpecialPermissionMakerChecker);

                }
                else
                {
                    context.BusinessOfficeSpecialPermissionMakerCheckers.Attach(businessOfficeSpecialPermissionMakerChecker);
                    context.Entry(businessOfficeSpecialPermissionMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachTransactionLimitData(BusinessOfficeTransactionLimitViewModel _businessOfficeTransactionLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeTransactionLimitViewModel, _entryType);
                // BusinessOfficeTransactionLimit
                BusinessOfficeTransactionLimit businessOfficeTransactionLimit = Mapper.Map<BusinessOfficeTransactionLimit>(_businessOfficeTransactionLimitViewModel);
                BusinessOfficeTransactionLimitMakerChecker businessOfficeTransactionLimitMakerChecker = Mapper.Map<BusinessOfficeTransactionLimitMakerChecker>(_businessOfficeTransactionLimitViewModel);

                businessOfficeTransactionLimit.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeTransactionLimitViewModel.GeneralLedgerId);
                businessOfficeTransactionLimit.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_businessOfficeTransactionLimitViewModel.TransactionTypeId);
                businessOfficeTransactionLimit.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_businessOfficeTransactionLimitViewModel.CurrencyId);
                businessOfficeTransactionLimit.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.BusinessOfficeTransactionLimits.Attach(businessOfficeTransactionLimit);
                    context.Entry(businessOfficeTransactionLimit).State = EntityState.Added;
                    businessOffice.BusinessOfficeTransactionLimits.Add(businessOfficeTransactionLimit);

                    context.BusinessOfficeTransactionLimitMakerCheckers.Attach(businessOfficeTransactionLimitMakerChecker);
                    context.Entry(businessOfficeTransactionLimitMakerChecker).State = EntityState.Added;
                    businessOfficeTransactionLimit.BusinessOfficeTransactionLimitMakerCheckers.Add(businessOfficeTransactionLimitMakerChecker);

                }
                else
                {
                    context.BusinessOfficeTransactionLimitMakerCheckers.Attach(businessOfficeTransactionLimitMakerChecker);
                    context.Entry(businessOfficeTransactionLimitMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachAccountNumberParameterData(BusinessOfficeAccountNumberViewModel _businessOfficeAccountNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeAccountNumberViewModel, _entryType);
                // BusinessOfficeAccountNumber
                BusinessOfficeAccountNumber businessOfficeAccountNumber = Mapper.Map<BusinessOfficeAccountNumber>(_businessOfficeAccountNumberViewModel);
                BusinessOfficeAccountNumberMakerChecker businessOfficeAccountNumberMakerChecker = Mapper.Map<BusinessOfficeAccountNumberMakerChecker>(_businessOfficeAccountNumberViewModel);
                businessOfficeAccountNumber.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeAccountNumberViewModel.GeneralLedgerId);
                businessOfficeAccountNumber.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if (_businessOfficeAccountNumberViewModel.EnableAutoAccountNumber == false)
                    {
                        businessOfficeAccountNumber.AccountNumberMask = "None";

                    }
                    context.BusinessOfficeAccountNumbers.Attach(businessOfficeAccountNumber);
                    context.Entry(businessOfficeAccountNumber).State = EntityState.Added;
                    businessOffice.BusinessOfficeAccountNumbers.Add(businessOfficeAccountNumber);

                    context.BusinessOfficeAccountNumberMakerCheckers.Attach(businessOfficeAccountNumberMakerChecker);
                    context.Entry(businessOfficeAccountNumberMakerChecker).State = EntityState.Added;
                    businessOfficeAccountNumber.BusinessOfficeAccountNumberMakerCheckers.Add(businessOfficeAccountNumberMakerChecker);

                }
                else
                {
                    context.BusinessOfficeAccountNumberMakerCheckers.Attach(businessOfficeAccountNumberMakerChecker);
                    context.Entry(businessOfficeAccountNumberMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        public bool AttachAgreementNumberParameterData(BusinessOfficeAgreementNumberViewModel _businessOfficeAgreementNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeAgreementNumberViewModel, _entryType);
                // BusinessOfficeAgreementNumber
                BusinessOfficeAgreementNumber businessOfficeAgreementNumber = Mapper.Map<BusinessOfficeAgreementNumber>(_businessOfficeAgreementNumberViewModel);
                BusinessOfficeAgreementNumberMakerChecker businessOfficeAgreementNumberMakerChecker = Mapper.Map<BusinessOfficeAgreementNumberMakerChecker>(_businessOfficeAgreementNumberViewModel);
                businessOfficeAgreementNumber.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeAgreementNumberViewModel.GeneralLedgerId);
                businessOfficeAgreementNumber.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if (_businessOfficeAgreementNumberViewModel.EnableAutoAgreementNumber == false)
                    {
                        businessOfficeAgreementNumber.AgreementNumberMask = "None";
                    }
                    context.BusinessOfficeAgreementNumbers.Attach(businessOfficeAgreementNumber);
                    context.Entry(businessOfficeAgreementNumber).State = EntityState.Added;
                    businessOffice.BusinessOfficeAgreementNumbers.Add(businessOfficeAgreementNumber);

                    context.BusinessOfficeAgreementNumberMakerCheckers.Attach(businessOfficeAgreementNumberMakerChecker);
                    context.Entry(businessOfficeAgreementNumberMakerChecker).State = EntityState.Added;
                    businessOfficeAgreementNumber.BusinessOfficeAgreementNumberMakerCheckers.Add(businessOfficeAgreementNumberMakerChecker);
                }
                else
                {
                    context.BusinessOfficeAgreementNumberMakerCheckers.Attach(businessOfficeAgreementNumberMakerChecker);
                    context.Entry(businessOfficeAgreementNumberMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachApplicationNumberData(BusinessOfficeApplicationNumberViewModel _businessOfficeApplicationNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeApplicationNumberViewModel, _entryType);
                // BusinessOfficeApplicationNumber
                BusinessOfficeApplicationNumber businessOfficeApplicationNumber = Mapper.Map<BusinessOfficeApplicationNumber>(_businessOfficeApplicationNumberViewModel);
                BusinessOfficeApplicationNumberMakerChecker businessOfficeApplicationNumberMakerChecker = Mapper.Map<BusinessOfficeApplicationNumberMakerChecker>(_businessOfficeApplicationNumberViewModel);

                businessOfficeApplicationNumber.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeApplicationNumberViewModel.GeneralLedgerId);
                businessOfficeApplicationNumber.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create)
                {
                    if (_businessOfficeApplicationNumberViewModel.EnableAutoApplicationNumber == false)
                    {
                        businessOfficeApplicationNumber.ApplicationNumberMask = "None";
                    }

                    context.BusinessOfficeApplicationNumbers.Attach(businessOfficeApplicationNumber);
                    context.Entry(businessOfficeApplicationNumber).State = EntityState.Added;
                    businessOffice.BusinessOfficeApplicationNumbers.Add(businessOfficeApplicationNumber);

                    context.BusinessOfficeApplicationNumberMakerCheckers.Attach(businessOfficeApplicationNumberMakerChecker);
                    context.Entry(businessOfficeApplicationNumberMakerChecker).State = EntityState.Added;
                    businessOfficeApplicationNumber.BusinessOfficeApplicationNumberMakerCheckers.Add(businessOfficeApplicationNumberMakerChecker);

                }
                else
                {
                    context.BusinessOfficeApplicationNumberMakerCheckers.Attach(businessOfficeApplicationNumberMakerChecker);
                    context.Entry(businessOfficeApplicationNumberMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachCurrencyData(BusinessOfficeCurrencyViewModel _businessOfficeCurrencyViewModel, string _entryType)
        {

            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeCurrencyViewModel, _entryType);
                BusinessOfficeCurrency businessOfficeCurrency = Mapper.Map<BusinessOfficeCurrency>(_businessOfficeCurrencyViewModel);
                BusinessOfficeCurrencyMakerChecker businessOfficeCurrencyMakerChecker = Mapper.Map<BusinessOfficeCurrencyMakerChecker>(_businessOfficeCurrencyViewModel);

                // Get PrmKey By Id Of All Dropdowns  
                businessOfficeCurrency.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_businessOfficeCurrencyViewModel.CurrencyId);
                businessOfficeCurrency.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.BusinessOfficeCurrencies.Attach(businessOfficeCurrency);
                    context.Entry(businessOfficeCurrency).State = EntityState.Added;

                    context.BusinessOfficeCurrencyMakerCheckers.Attach(businessOfficeCurrencyMakerChecker);
                    context.Entry(businessOfficeCurrencyMakerChecker).State = EntityState.Added;
                    businessOfficeCurrency.BusinessOfficeCurrencyMakerCheckers.Add(businessOfficeCurrencyMakerChecker);
                }
                else
                {
                    context.BusinessOfficeCurrencyMakerCheckers.Attach(businessOfficeCurrencyMakerChecker);
                    context.Entry(businessOfficeCurrencyMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachDepositCertificateNumberData(BusinessOfficeDepositCertificateNumberViewModel _businessOfficeDepositCertificateNumberViewModel, string _entryType)
        {

            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeDepositCertificateNumberViewModel, _entryType);
                BusinessOfficeDepositCertificateNumber businessOfficeDepositCertificateNumber = Mapper.Map<BusinessOfficeDepositCertificateNumber>(_businessOfficeDepositCertificateNumberViewModel);
                BusinessOfficeDepositCertificateNumberMakerChecker businessOfficeDepositCertificateNumberMakerChecker = Mapper.Map<BusinessOfficeDepositCertificateNumberMakerChecker>(_businessOfficeDepositCertificateNumberViewModel);

                // Get PrmKey By Id Of All Dropdowns  
                businessOfficeDepositCertificateNumber.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficeDepositCertificateNumberViewModel.GeneralLedgerId);
                businessOfficeDepositCertificateNumber.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create)
                {
                    if (_businessOfficeDepositCertificateNumberViewModel.EnableAutoCertificateNumber == false)
                    {
                        businessOfficeDepositCertificateNumber.CertificateNumberMask = "None";
                    }
                    context.BusinessOfficeDepositCertificateNumbers.Attach(businessOfficeDepositCertificateNumber);
                    context.Entry(businessOfficeDepositCertificateNumber).State = EntityState.Added;

                    context.BusinessOfficeDepositCertificateNumberMakerCheckers.Attach(businessOfficeDepositCertificateNumberMakerChecker);
                    context.Entry(businessOfficeDepositCertificateNumberMakerChecker).State = EntityState.Added;
                    businessOfficeDepositCertificateNumber.BusinessOfficeDepositCertificateNumberMakerCheckers.Add(businessOfficeDepositCertificateNumberMakerChecker);

                }
                else
                {
                    context.BusinessOfficeDepositCertificateNumberMakerCheckers.Attach(businessOfficeDepositCertificateNumberMakerChecker);
                    context.Entry(businessOfficeDepositCertificateNumberMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachCustomerNumberData(BusinessOfficeCustomerNumberViewModel _businessOfficeCustomerNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeCustomerNumberViewModel, _entryType);
                // BusinessOfficeCustomerNumber
                BusinessOfficeCustomerNumber businessOfficeCustomerNumber = Mapper.Map<BusinessOfficeCustomerNumber>(_businessOfficeCustomerNumberViewModel);
                BusinessOfficeCustomerNumberMakerChecker businessOfficeCustomerNumberMaker = Mapper.Map<BusinessOfficeCustomerNumberMakerChecker>(_businessOfficeCustomerNumberViewModel);
                //businessOfficeCustomerNumber.PrmKey = _businessOfficeCustomerNumberViewModel.BusinessOfficeCustomerNumberPrmKey;

                businessOfficeCustomerNumber.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOfficeCustomerNumber.PrmKey = _businessOfficeCustomerNumberViewModel.BusinessOfficeCustomerNumberPrmKey;

                    // BusinessOfficeCustomerNumber

                    context.BusinessOfficeCustomerNumbers.Attach(businessOfficeCustomerNumber);
                    context.Entry(businessOfficeCustomerNumber).State = entityState;
                    businessOffice.BusinessOfficeCustomerNumbers.Add(businessOfficeCustomerNumber);

                    context.BusinessOfficeCustomerNumberMakerCheckers.Attach(businessOfficeCustomerNumberMaker);
                    context.Entry(businessOfficeCustomerNumberMaker).State = EntityState.Added;
                    businessOfficeCustomerNumber.BusinessOfficeCustomerNumberMakerCheckers.Add(businessOfficeCustomerNumberMaker);

                }
                else
                {
                    context.BusinessOfficeCustomerNumberMakerCheckers.Attach(businessOfficeCustomerNumberMaker);
                    context.Entry(businessOfficeCustomerNumberMaker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSharesCertificateNumberData(BusinessOfficeSharesCertificateNumberViewModel _businessOfficeSharesCertificateNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeSharesCertificateNumberViewModel, _entryType);
                // SharesCertificateNumber
                BusinessOfficeSharesCertificateNumber businessOfficeSharesCertificateNumber = Mapper.Map<BusinessOfficeSharesCertificateNumber>(_businessOfficeSharesCertificateNumberViewModel);
                BusinessOfficeSharesCertificateNumberMakerChecker businessOfficeSharesCertificateNumberMaker = Mapper.Map<BusinessOfficeSharesCertificateNumberMakerChecker>(_businessOfficeSharesCertificateNumberViewModel);
                businessOfficeSharesCertificateNumber.PrmKey = _businessOfficeSharesCertificateNumberViewModel.BusinessOfficeSharesCertificateNumberPrmKey;

                businessOfficeSharesCertificateNumber.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_businessOfficeSharesCertificateNumberViewModel.EnableAutoSharesCertificateNumber == false)
                    businessOfficeSharesCertificateNumber.SharesCertificateNumberMask = "None";
                else
                    businessOfficeSharesCertificateNumber.SharesCertificateNumberMask = string.Join(",", _businessOfficeSharesCertificateNumberViewModel.MaskTypeCharacterForSharesCertificate);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.BusinessOfficeSharesCertificateNumbers.Attach(businessOfficeSharesCertificateNumber);
                    context.Entry(businessOfficeSharesCertificateNumber).State = entityState;
                    businessOffice.BusinessOfficeSharesCertificateNumbers.Add(businessOfficeSharesCertificateNumber);

                    context.BusinessOfficeSharesCertificateNumberMakerCheckers.Attach(businessOfficeSharesCertificateNumberMaker);
                    context.Entry(businessOfficeSharesCertificateNumberMaker).State = EntityState.Added;
                    businessOfficeSharesCertificateNumber.BusinessOfficeSharesCertificateNumberMakerCheckers.Add(businessOfficeSharesCertificateNumberMaker);

                }
                else
                {
                    context.BusinessOfficeSharesCertificateNumberMakerCheckers.Attach(businessOfficeSharesCertificateNumberMaker);
                    context.Entry(businessOfficeSharesCertificateNumberMaker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonInformationNumberData(BusinessOfficePersonInformationNumberViewModel _businessOfficePersonInformationNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficePersonInformationNumberViewModel, _entryType);
                // PersonInformationNumber
                BusinessOfficePersonInformationNumber businessOfficePersonInformationNumber = Mapper.Map<BusinessOfficePersonInformationNumber>(_businessOfficePersonInformationNumberViewModel);
                BusinessOfficePersonInformationNumberMakerChecker businessOfficePersonInformationNumberMaker = Mapper.Map<BusinessOfficePersonInformationNumberMakerChecker>(_businessOfficePersonInformationNumberViewModel);
                businessOfficePersonInformationNumber.PrmKey = _businessOfficePersonInformationNumberViewModel.BusinessOfficePersonInformationNumberPrmKey;

                if (_businessOfficePersonInformationNumberViewModel.EnableAutoPersonInformationNumber == false)
                    businessOfficePersonInformationNumber.PersonInformationNumberMask = "None";
                else
                    businessOfficePersonInformationNumber.PersonInformationNumberMask = string.Join(",", _businessOfficePersonInformationNumberViewModel.MaskTypeCharacterForPerson);

                businessOfficePersonInformationNumber.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOfficePersonInformationNumber.PrmKey = _businessOfficePersonInformationNumberViewModel.BusinessOfficePersonInformationNumberPrmKey;

                    context.BusinessOfficePersonInformationNumbers.Attach(businessOfficePersonInformationNumber);
                    context.Entry(businessOfficePersonInformationNumber).State = entityState;
                    //businessOffice.BusinessOfficePersonInformationNumbers.Add(businessOfficePersonInformationNumber);

                    context.BusinessOfficePersonInformationNumberMakerCheckers.Attach(businessOfficePersonInformationNumberMaker);
                    context.Entry(businessOfficePersonInformationNumberMaker).State = EntityState.Added;
                    businessOfficePersonInformationNumber.BusinessOfficePersonInformationNumberMakerCheckers.Add(businessOfficePersonInformationNumberMaker);

                }
                else
                {
                    context.BusinessOfficePersonInformationNumberMakerCheckers.Attach(businessOfficePersonInformationNumberMaker);
                    context.Entry(businessOfficePersonInformationNumberMaker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPassbookNumberData(BusinessOfficePassbookNumberViewModel _businessOfficePassbookNumberViewModel, string _entryType)
        {

            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficePassbookNumberViewModel, _entryType);
                BusinessOfficePassbookNumber businessOfficePassbookNumber = Mapper.Map<BusinessOfficePassbookNumber>(_businessOfficePassbookNumberViewModel);
                BusinessOfficePassbookNumberMakerChecker businessOfficePassbookNumberMakerChecker = Mapper.Map<BusinessOfficePassbookNumberMakerChecker>(_businessOfficePassbookNumberViewModel);

                // Get PrmKey By Id Of All Dropdowns  
                businessOfficePassbookNumber.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_businessOfficePassbookNumberViewModel.GeneralLedgerId);
                businessOfficePassbookNumber.BusinessOfficePrmKey = businessOfficePrmKey;


                if (_entryType == StringLiteralValue.Create)
                {
                    if (_businessOfficePassbookNumberViewModel.EnableAutoPassbookNumber == false)
                    {
                        businessOfficePassbookNumber.PassbookNumberMask = "None";
                    }
                    context.BusinessOfficePassbookNumbers.Attach(businessOfficePassbookNumber);
                    context.Entry(businessOfficePassbookNumber).State = EntityState.Added;

                    context.BusinessOfficePassbookNumberMakerCheckers.Attach(businessOfficePassbookNumberMakerChecker);
                    context.Entry(businessOfficePassbookNumberMakerChecker).State = EntityState.Added;
                    businessOfficePassbookNumber.BusinessOfficePassbookNumberMakerCheckers.Add(businessOfficePassbookNumberMakerChecker);
                }
                else
                {
                    context.BusinessOfficePassbookNumberMakerCheckers.Attach(businessOfficePassbookNumberMakerChecker);
                    context.Entry(businessOfficePassbookNumberMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }



        public bool AttachMemberNumberData(BusinessOfficeMemberNumberViewModel _businessOfficeMemberNumberViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeMemberNumberViewModel, _entryType);
                BusinessOfficeMemberNumber businessOfficeMemberNumber = Mapper.Map<BusinessOfficeMemberNumber>(_businessOfficeMemberNumberViewModel);
                BusinessOfficeMemberNumberMakerChecker businessOfficeMemberNumberMakerChecker = Mapper.Map<BusinessOfficeMemberNumberMakerChecker>(_businessOfficeMemberNumberViewModel);

                businessOfficeMemberNumber.PrmKey = _businessOfficeMemberNumberViewModel.BusinessOfficeMemberNumberPrmKey;
                businessOfficeMemberNumber.BusinessOfficePrmKey = businessOfficePrmKey;

                if (_businessOfficeMemberNumberViewModel.EnableAutoMemberNumber == false)
                    businessOfficeMemberNumber.MemberNumberMask = "None";
                else
                    businessOfficeMemberNumber.MemberNumberMask = string.Join(",", _businessOfficeMemberNumberViewModel.MaskTypeCharacterForMember);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    businessOfficeMemberNumber.PrmKey = _businessOfficeMemberNumberViewModel.BusinessOfficeMemberNumberPrmKey;

                    // BusinessOfficeMemberNumber
                    context.BusinessOfficeMemberNumbers.Attach(businessOfficeMemberNumber);
                    context.Entry(businessOfficeMemberNumber).State = entityState;
                    businessOffice.BusinessOfficeMemberNumbers.Add(businessOfficeMemberNumber);


                    context.BusinessOfficeMemberNumberMakerCheckers.Attach(businessOfficeMemberNumberMakerChecker);
                    context.Entry(businessOfficeMemberNumberMakerChecker).State = EntityState.Added;
                    businessOfficeMemberNumber.BusinessOfficeMemberNumberMakerCheckers.Add(businessOfficeMemberNumberMakerChecker);


                }
                else
                {
                    context.BusinessOfficeMemberNumberMakerCheckers.Attach(businessOfficeMemberNumberMakerChecker);
                    context.Entry(businessOfficeMemberNumberMakerChecker).State = EntityState.Added;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachTransactionParameterData(BusinessOfficeTransactionParameterViewModel _businessOfficeTransactionParameterViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_businessOfficeTransactionParameterViewModel, _entryType);
                // BusinessOfficeTransactionParameter 
                BusinessOfficeTransactionParameter businessOfficeTransactionParameter = Mapper.Map<BusinessOfficeTransactionParameter>(_businessOfficeTransactionParameterViewModel);
                BusinessOfficeTransactionParameterMakerChecker businessOfficeTransactionParameterMakerChecker = Mapper.Map<BusinessOfficeTransactionParameterMakerChecker>(_businessOfficeTransactionParameterViewModel);

                businessOfficeTransactionParameter.BusinessOfficePrmKey = businessOfficePrmKey;
                businessOfficeTransactionParameter.ChecksumAlgorithmPrmKey = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_businessOfficeTransactionParameterViewModel.ChecksumAlgorithmId);

                //businessOfficeTransactionParameter.PrmKey = _businessOfficeTransactionParameterViewModel.BusinessOfficeTransactionParameterPrmKey;

                if (_businessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == false)
                    businessOfficeTransactionParameter.TransactionNumberMask = "None";
                else
                    businessOfficeTransactionParameter.TransactionNumberMask = string.Join(",", _businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    businessOfficeTransactionParameter.PrmKey = _businessOfficeTransactionParameterViewModel.BusinessOfficeTransactionParameterPrmKey;

                    if (_businessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == false)
                    {
                        businessOfficeTransactionParameter.TransactionNumberMask = "NONE";
                    }

                    else
                        businessOfficeTransactionParameter.TransactionNumberMask = string.Join(",", _businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask);
                    // BusinessOfficeTransactionParameter

                    context.BusinessOfficeTransactionParameters.Attach(businessOfficeTransactionParameter);
                    context.Entry(businessOfficeTransactionParameter).State = entityState;
                    businessOffice.BusinessOfficeTransactionParameters.Add(businessOfficeTransactionParameter);

                    context.BusinessOfficeTransactionParameterMakerCheckers.Attach(businessOfficeTransactionParameterMakerChecker);
                    context.Entry(businessOfficeTransactionParameterMakerChecker).State = EntityState.Added;
                    businessOfficeTransactionParameter.BusinessOfficeTransactionParameterMakerCheckers.Add(businessOfficeTransactionParameterMakerChecker);
                }
                else
                {
                    context.BusinessOfficeTransactionParameterMakerCheckers.Attach(businessOfficeTransactionParameterMakerChecker);
                    context.Entry(businessOfficeTransactionParameterMakerChecker).State = EntityState.Added;
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
