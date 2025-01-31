using AutoMapper;
using DemoProject.Domain.Entities.Security.UserRoles;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.UserRoles;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Security.UserRoles
{
    public class EFRoleProfileDbContextRepository : IRoleProfileDbContextRepository
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly EFDbContext context;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IRoleProfileDetailRepository roleProfileDetailRepository;

        public EFRoleProfileDbContextRepository(IConfigurationDetailRepository _configurationDetailRepository, EFDbContext _context, ISecurityDetailRepository _securityDetailRepository, IPersonDetailRepository _personDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IAccountDetailRepository _accountDetailRepository, IRoleProfileDetailRepository _roleProfileDetailRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            context = _context;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            roleProfileDetailRepository = _roleProfileDetailRepository;
        }
        private EntityState entityState;
        short oldRoleProfilePrmKey;

        public bool AttachRoleProfileData(RoleProfileViewModel _roleProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_roleProfileViewModel, _entryType);

                // RoleProfile
                RoleProfile roleProfile = Mapper.Map<RoleProfile>(_roleProfileViewModel);
                RoleProfileMakerChecker roleProfileMakerChecker = Mapper.Map<RoleProfileMakerChecker>(_roleProfileViewModel);

                //RoleProfileTranslation
                RoleProfileTranslation roleProfileTranslation = Mapper.Map<RoleProfileTranslation>(_roleProfileViewModel);
                RoleProfileTranslationMakerChecker roleProfileTranslationMakerChecker = Mapper.Map<RoleProfileTranslationMakerChecker>(_roleProfileViewModel);

                oldRoleProfilePrmKey = _roleProfileViewModel.RoleProfilePrmKey;
                roleProfile.PrmKey = oldRoleProfilePrmKey;
                roleProfileTranslation.PrmKey = _roleProfileViewModel.RoleProfileTranslationPrmKey;
                _roleProfileViewModel.RoleProfilePrmKey = oldRoleProfilePrmKey;
                roleProfileTranslation.TransReasonForModification = "None";
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    
                    // RoleProfile
                    context.RoleProfiles.Attach(roleProfile);
                    context.Entry(roleProfile).State = entityState;

                    context.RoleProfileMakerCheckers.Attach(roleProfileMakerChecker);
                    context.Entry(roleProfileMakerChecker).State = EntityState.Added;
                    roleProfile.RoleProfileMakerCheckers.Add(roleProfileMakerChecker);

                    //RoleProfileTranslation
                    context.RoleProfileTranslations.Attach(roleProfileTranslation);
                    context.Entry(roleProfileTranslation).State = entityState;
                    roleProfile.RoleProfileTranslations.Add(roleProfileTranslation);

                    context.RoleProfileTranslationMakerCheckers.Attach(roleProfileTranslationMakerChecker);
                    context.Entry(roleProfileTranslationMakerChecker).State = EntityState.Added;
                    roleProfileTranslation.RoleProfileTranslationMakerCheckers.Add(roleProfileTranslationMakerChecker);
                }
                else
                {
                    context.RoleProfileMakerCheckers.Attach(roleProfileMakerChecker);
                    context.Entry(roleProfileMakerChecker).State = EntityState.Added;

                    context.RoleProfileTranslationMakerCheckers.Attach(roleProfileTranslationMakerChecker);
                    context.Entry(roleProfileTranslationMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachRoleProfileModificationData(RoleProfileViewModel _roleProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_roleProfileViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, _entryType);

                // RoleProfileModification
                RoleProfileModification roleProfileModification = Mapper.Map<RoleProfileModification>(_roleProfileViewModel);
                RoleProfileModificationMakerChecker roleProfileModificationMakerChecker = Mapper.Map<RoleProfileModificationMakerChecker>(_roleProfileViewModel);

                // RoleProfileTranslation
                RoleProfileTranslation roleProfileTranslation = Mapper.Map<RoleProfileTranslation>(_roleProfileViewModel);
                RoleProfileTranslationMakerChecker roleProfileTranslationMakerChecker = Mapper.Map<RoleProfileTranslationMakerChecker>(_roleProfileViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;


                    context.RoleProfileModifications.Attach(roleProfileModification);
                    context.Entry(roleProfileModification).State = entityState;

                    // RoleProfileModification
                    context.RoleProfileModificationMakerCheckers.Attach(roleProfileModificationMakerChecker);
                    context.Entry(roleProfileModificationMakerChecker).State = EntityState.Added;
                    roleProfileModification.RoleProfileModificationMakerCheckers.Add(roleProfileModificationMakerChecker);


                    //RoleProfileTranslation
                    context.RoleProfileTranslations.Attach(roleProfileTranslation);
                    context.Entry(roleProfileTranslation).State = entityState;
                    //roleProfile.RoleProfileTranslations.Add(roleProfileTranslation);

                    context.RoleProfileTranslationMakerCheckers.Attach(roleProfileTranslationMakerChecker);
                    context.Entry(roleProfileTranslationMakerChecker).State = EntityState.Added;
                    roleProfileTranslation.RoleProfileTranslationMakerCheckers.Add(roleProfileTranslationMakerChecker);

                }
                else
                {
                    context.RoleProfileModificationMakerCheckers.Attach(roleProfileModificationMakerChecker);
                    context.Entry(roleProfileModificationMakerChecker).State = EntityState.Added;

                    context.RoleProfileTranslationMakerCheckers.Attach(roleProfileTranslationMakerChecker);
                    context.Entry(roleProfileTranslationMakerChecker).State = EntityState.Added;

                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }


        public bool AttachRoleProfileGeneralLedgerData(RoleProfileGeneralLedgerViewModel _roleProfileGeneralLedgerViewModel, string _entryType)
        {
            try
            {
                //userProfileDetailRepository.GetUserProfileGeneralLedgerDefaultValues(_userProfileGeneralLedgerViewModel, _entryType);

                configurationDetailRepository.SetDefaultValues(_roleProfileGeneralLedgerViewModel, _entryType);
                // Get Prmkey By Id
                _roleProfileGeneralLedgerViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_roleProfileGeneralLedgerViewModel.GeneralLedgerId);

                RoleProfileGeneralLedger roleProfileGeneralLedger = Mapper.Map<RoleProfileGeneralLedger>(_roleProfileGeneralLedgerViewModel);

                RoleProfileGeneralLedgerMakerChecker roleProfileGeneralLedgerMakerChecker = Mapper.Map<RoleProfileGeneralLedgerMakerChecker>(_roleProfileGeneralLedgerViewModel);
                
                if (_entryType == StringLiteralValue.Create)
                {
                    roleProfileGeneralLedger.RoleProfilePrmKey = oldRoleProfilePrmKey;
                    context.RoleProfileGeneralLedgers.Attach(roleProfileGeneralLedger);
                    context.Entry(roleProfileGeneralLedger).State = EntityState.Added;

                    context.RoleProfileGeneralLedgerMakerCheckers.Attach(roleProfileGeneralLedgerMakerChecker);
                    context.Entry(roleProfileGeneralLedgerMakerChecker).State = EntityState.Added;
                    roleProfileGeneralLedger.RoleProfileGeneralLedgerMakerCheckers.Add(roleProfileGeneralLedgerMakerChecker);

                }
                else
                {
                    context.RoleProfileGeneralLedgerMakerCheckers.Attach(roleProfileGeneralLedgerMakerChecker);
                    context.Entry(roleProfileGeneralLedgerMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachRoleProfileBusinessOfficeData(RoleProfileBusinessOfficeViewModel _roleProfileBusinessOfficeViewModel, string _entryType)
        {
            try
            {
                //userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(_userProfileBusinessOfficeViewModel, _entryType);

                configurationDetailRepository.SetDefaultValues(_roleProfileBusinessOfficeViewModel, _entryType);
                // Get Prmkey By Id
                _roleProfileBusinessOfficeViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_roleProfileBusinessOfficeViewModel.BusinessOfficeId);

                RoleProfileBusinessOffice roleProfileBusinessOffice = Mapper.Map<RoleProfileBusinessOffice>(_roleProfileBusinessOfficeViewModel);

                RoleProfileBusinessOfficeMakerChecker roleProfileBusinessOfficeMakerChecker = Mapper.Map<RoleProfileBusinessOfficeMakerChecker>(_roleProfileBusinessOfficeViewModel);


                if (_entryType == StringLiteralValue.Create)
                {
                    roleProfileBusinessOffice.RoleProfilePrmKey = oldRoleProfilePrmKey;

                    context.RoleProfileBusinessOffices.Attach(roleProfileBusinessOffice);
                    context.Entry(roleProfileBusinessOffice).State = EntityState.Added;

                    context.RoleProfileBusinessOfficeMakerCheckers.Attach(roleProfileBusinessOfficeMakerChecker);
                    context.Entry(roleProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                    roleProfileBusinessOffice.RoleProfileBusinessOfficeMakerCheckers.Add(roleProfileBusinessOfficeMakerChecker);

                }
                else
                {
                    context.RoleProfileBusinessOfficeMakerCheckers.Attach(roleProfileBusinessOfficeMakerChecker);
                    context.Entry(roleProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachRoleProfileTransactionLimitData(RoleProfileTransactionLimitViewModel _roleProfileTransactionLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_roleProfileTransactionLimitViewModel, _entryType);
                // Get Prmkey By Id
                _roleProfileTransactionLimitViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_roleProfileTransactionLimitViewModel.GeneralLedgerId);
                _roleProfileTransactionLimitViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_roleProfileTransactionLimitViewModel.TransactionTypeId);
                _roleProfileTransactionLimitViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_roleProfileTransactionLimitViewModel.CurrencyId);

                RoleProfileTransactionLimit roleProfileTransactionLimit = Mapper.Map<RoleProfileTransactionLimit>(_roleProfileTransactionLimitViewModel);
                RoleProfileTransactionLimitMakerChecker roleProfileTransactionLimitMakerChecker = Mapper.Map<RoleProfileTransactionLimitMakerChecker>(_roleProfileTransactionLimitViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    roleProfileTransactionLimit.RoleProfilePrmKey = oldRoleProfilePrmKey;

                    context.RoleProfileTransactionLimits.Attach(roleProfileTransactionLimit);
                    context.Entry(roleProfileTransactionLimit).State = EntityState.Added;

                    context.RoleProfileTransactionLimitMakerCheckers.Attach(roleProfileTransactionLimitMakerChecker);
                    context.Entry(roleProfileTransactionLimitMakerChecker).State = EntityState.Added;
                    roleProfileTransactionLimit.RoleProfileTransactionLimitMakerCheckers.Add(roleProfileTransactionLimitMakerChecker);

                }
                else
                {
                    context.RoleProfileTransactionLimitMakerCheckers.Attach(roleProfileTransactionLimitMakerChecker);
                    context.Entry(roleProfileTransactionLimitMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachRoleProfileMenuData(RoleProfileMenuViewModel _roleProfileMenuViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_roleProfileMenuViewModel, _entryType);


                // Get Prmkey By Id
                _roleProfileMenuViewModel.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(_roleProfileMenuViewModel.ModelMenuId);

                RoleProfileMenu roleProfileMenu = Mapper.Map<RoleProfileMenu>(_roleProfileMenuViewModel);

                RoleProfileMenuMakerChecker roleProfileMenuMakerChecker = Mapper.Map<RoleProfileMenuMakerChecker>(_roleProfileMenuViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    roleProfileMenu.RoleProfilePrmKey = oldRoleProfilePrmKey;

                    context.RoleProfileMenus.Attach(roleProfileMenu);
                    context.Entry(roleProfileMenu).State = EntityState.Added;

                    context.RoleProfileMenuMakerCheckers.Attach(roleProfileMenuMakerChecker);
                    context.Entry(roleProfileMenuMakerChecker).State = EntityState.Added;
                    roleProfileMenu.RoleProfileMenuMakerCheckers.Add(roleProfileMenuMakerChecker);

                }
                else
                {
                    context.RoleProfileMenuMakerCheckers.Attach(roleProfileMenuMakerChecker);
                    context.Entry(roleProfileMenuMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachRoleProfileSpecialPermissionData(RoleProfileSpecialPermissionViewModel _roleProfileSpecialPermissionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_roleProfileSpecialPermissionViewModel, _entryType);

                // Get Prmkey By Id
                _roleProfileSpecialPermissionViewModel.SpecialPermissionPrmKey = securityDetailRepository.GetSpecialPermissionPrmKeyById(_roleProfileSpecialPermissionViewModel.SpecialPermissionId);

                RoleProfileSpecialPermission roleProfileSpecialPermission = Mapper.Map<RoleProfileSpecialPermission>(_roleProfileSpecialPermissionViewModel);

                RoleProfileSpecialPermissionMakerChecker roleProfileSpecialPermissionMakerChecker = Mapper.Map<RoleProfileSpecialPermissionMakerChecker>(_roleProfileSpecialPermissionViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    
                    roleProfileSpecialPermission.RoleProfilePrmKey = oldRoleProfilePrmKey;

                    context.RoleProfileSpecialPermissions.Attach(roleProfileSpecialPermission);
                    context.Entry(roleProfileSpecialPermission).State = EntityState.Added;

                    context.RoleProfileSpecialPermissionMakerCheckers.Attach(roleProfileSpecialPermissionMakerChecker);
                    context.Entry(roleProfileSpecialPermissionMakerChecker).State = EntityState.Added;
                    roleProfileSpecialPermission.RoleProfileSpecialPermissionMakerCheckers.Add(roleProfileSpecialPermissionMakerChecker);

                }
                else
                {
                    context.RoleProfileSpecialPermissionMakerCheckers.Attach(roleProfileSpecialPermissionMakerChecker);
                    context.Entry(roleProfileSpecialPermissionMakerChecker).State = EntityState.Added;
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
