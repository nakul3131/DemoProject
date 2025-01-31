using AutoMapper;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Constants;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.PersonInformation;
using System.Web;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileDbContextRepository : IUserProfileDbContextRepository
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly EFDbContext context;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IUserProfileDetailRepository userProfileDetailRepository;

        public EFUserProfileDbContextRepository(IConfigurationDetailRepository _configurationDetailRepository, EFDbContext _context, ISecurityDetailRepository _securityDetailRepository, IPersonDetailRepository _personDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IAccountDetailRepository _accountDetailRepository, IUserProfileDetailRepository _userProfileDetailRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            context = _context;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            userProfileDetailRepository = _userProfileDetailRepository;
        }
        private EntityState entityState;
        short oldUserProfilePrmKey;
        public bool AttachUserProfileData(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            try
            {

                oldUserProfilePrmKey = _userProfileViewModel.UserProfilePrmKey;
                configurationDetailRepository.SetDefaultValues(_userProfileViewModel, _entryType);
                //userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, _entryType);
                _userProfileViewModel.UserTypePrmKey = securityDetailRepository.GetUserTypePrmKeyById(_userProfileViewModel.UserTypeId);
                _userProfileViewModel.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_userProfileViewModel.PersonId);
                _userProfileViewModel.PrmKey = oldUserProfilePrmKey;
                _userProfileViewModel.UserProfilePrmKey = oldUserProfilePrmKey;

                UserProfile userProfile = Mapper.Map<UserProfile>(_userProfileViewModel);
                UserProfileMakerChecker userProfileMakerChecker = Mapper.Map<UserProfileMakerChecker>(_userProfileViewModel);
                userProfileMakerChecker.UserProfileCreatorPrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.UserProfiles.Attach(userProfile);
                    context.Entry(userProfile).State = entityState;

                    context.UserProfileMakerCheckers.Attach(userProfileMakerChecker);
                    context.Entry(userProfileMakerChecker).State = EntityState.Added;
                    userProfile.UserProfileMakerCheckers.Add(userProfileMakerChecker);
                }
                else
                {
                    context.UserProfileMakerCheckers.Attach(userProfileMakerChecker);
                    context.Entry(userProfileMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileModificationData(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, _entryType);

                UserProfileModification userProfileModificationForAmend = Mapper.Map<UserProfileModification>(_userProfileViewModel);
                UserProfileModificationMakerChecker userProfileModificationMakerCheckerForAmend = Mapper.Map<UserProfileModificationMakerChecker>(_userProfileViewModel);
                userProfileModificationMakerCheckerForAmend.UserProfileCreatorPrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;


                    context.UserProfileModifications.Attach(userProfileModificationForAmend);
                    context.Entry(userProfileModificationForAmend).State = entityState;

                    context.UserProfileModificationMakerCheckers.Attach(userProfileModificationMakerCheckerForAmend);
                    context.Entry(userProfileModificationMakerCheckerForAmend).State = EntityState.Added;
                    userProfileModificationForAmend.UserProfileModificationMakerCheckers.Add(userProfileModificationMakerCheckerForAmend);

                }
                else
                {
                    context.UserProfileModificationMakerCheckers.Attach(userProfileModificationMakerCheckerForAmend);
                    context.Entry(userProfileModificationMakerCheckerForAmend).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileAccessibilityData(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, _entryType);

                // UserProfileAccessibility
                UserProfileAccessibility userProfileAccessibility = Mapper.Map<UserProfileAccessibility>(_userProfileViewModel);
                //userProfileAccessibility.UserProfilePrmKey = _userProfileViewModel.PrmKey;
                userProfileAccessibility.LoginVia = "BTH";
                userProfileAccessibility.SessionTimeOut = 60;
                userProfileAccessibility.ScreenSaverTheme = 0;
                userProfileAccessibility.ApplicationTheme = 0;
                userProfileAccessibility.TokenDeliveryChannel = "MBL";
                userProfileAccessibility.Note = "None";

                if (_entryType == StringLiteralValue.Create)
                {
                    context.UserProfileAccessibilities.Attach(userProfileAccessibility);
                    context.Entry(userProfileAccessibility).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileIdentityData(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, _entryType);

                _userProfileViewModel.UserId = _userProfileViewModel.NameOfUserProfile;

                UserProfileIdentity userProfileIdentity = Mapper.Map<UserProfileIdentity>(_userProfileViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.UserProfileIdentitys.Attach(userProfileIdentity);
                    context.Entry(userProfileIdentity).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }


        public bool AttachUserProfileHomeBusinessOfficeData(UserProfileHomeBusinessOfficeViewModel _userProfileHomeBusinessOfficeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileHomeBusinessOfficeViewModel, _entryType);
                //userProfileDetailRepository.GetUserProfileHomeBusinessOfficeDefaultValues(_userProfileHomeBusinessOfficeViewModel, _entryType);

                // Get PrmKey By Id Of All Dropdowns
                _userProfileHomeBusinessOfficeViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_userProfileHomeBusinessOfficeViewModel.BusinessOfficeId);

                UserProfileHomeBusinessOffice userProfileHomeBusinessOffice = Mapper.Map<UserProfileHomeBusinessOffice>(_userProfileHomeBusinessOfficeViewModel);
                UserProfileHomeBusinessOfficeMakerChecker userProfileHomeBusinessOfficeMakerChecker = Mapper.Map<UserProfileHomeBusinessOfficeMakerChecker>(_userProfileHomeBusinessOfficeViewModel);
                userProfileHomeBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                userProfileHomeBusinessOffice.UserProfilePrmKey = oldUserProfilePrmKey;

                //set 0 to prmkey/Userprofileprmkey
                if (_entryType == StringLiteralValue.Create)
                {
                    userProfileHomeBusinessOffice.UserProfilePrmKey = 0;
                }
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;


                    context.UserProfileHomeBusinessOffices.Attach(userProfileHomeBusinessOffice);
                    context.Entry(userProfileHomeBusinessOffice).State = entityState;

                    context.UserProfileHomeBusinessOfficeMakerCheckers.Attach(userProfileHomeBusinessOfficeMakerChecker);
                    context.Entry(userProfileHomeBusinessOfficeMakerChecker).State = EntityState.Added;
                    userProfileHomeBusinessOffice.UserProfileHomeBusinessOfficeMakerCheckers.Add(userProfileHomeBusinessOfficeMakerChecker);


                }
                else
                {
                    context.UserProfileHomeBusinessOfficeMakerCheckers.Attach(userProfileHomeBusinessOfficeMakerChecker);
                    context.Entry(userProfileHomeBusinessOfficeMakerChecker).State = EntityState.Added;

                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileBusinessOfficeData(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string _entryType)
        {
            try
            {
                //userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(_userProfileBusinessOfficeViewModel, _entryType);
                
                configurationDetailRepository.SetDefaultValues(_userProfileBusinessOfficeViewModel, _entryType);
                // Get Prmkey By Id
                _userProfileBusinessOfficeViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_userProfileBusinessOfficeViewModel.BusinessOfficeId);

                UserProfileBusinessOffice userProfileBusinessOffice = Mapper.Map<UserProfileBusinessOffice>(_userProfileBusinessOfficeViewModel);
                UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(_userProfileBusinessOfficeViewModel);
                userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                userProfileBusinessOffice.UserProfilePrmKey = oldUserProfilePrmKey;
                if (_entryType == StringLiteralValue.Create)
                {
                    
                    context.UserProfileBusinessOffices.Attach(userProfileBusinessOffice);
                    context.Entry(userProfileBusinessOffice).State = EntityState.Added;

                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                    userProfileBusinessOffice.UserProfileBusinessOfficeMakerCheckers.Add(userProfileBusinessOfficeMakerChecker);
                }
                else
                {
                    //userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = oldUserProfilePrmKey;
                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }


        public bool AttachUserProfileCurrencyData(UserProfileCurrencyViewModel _userProfileCurrencyViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileCurrencyViewModel, _entryType);
                //userProfileDetailRepository.GetUserProfileCurrencyDefaultValues(_userProfileCurrencyViewModel, _entryType);

                // Get Prmkey By Id
                _userProfileCurrencyViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_userProfileCurrencyViewModel.CurrencyId);

                UserProfileCurrency userProfileCurrency = Mapper.Map<UserProfileCurrency>(_userProfileCurrencyViewModel);
                UserProfileCurrencyMakerChecker userProfileCurrencyMakerChecker = Mapper.Map<UserProfileCurrencyMakerChecker>(_userProfileCurrencyViewModel);
                userProfileCurrencyMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                userProfileCurrency.UserProfilePrmKey = oldUserProfilePrmKey;
                if (_entryType == StringLiteralValue.Create)
                {
                    //set 0 to prmkey/Userprofileprmkey
                    

                    context.UserProfileCurrencys.Attach(userProfileCurrency);
                    context.Entry(userProfileCurrency).State = EntityState.Added;

                    context.UserProfileCurrencyMakerCheckers.Attach(userProfileCurrencyMakerChecker);
                    context.Entry(userProfileCurrencyMakerChecker).State = EntityState.Added;
                    userProfileCurrency.UserProfileCurrencyMakerCheckers.Add(userProfileCurrencyMakerChecker);

                }
                else
                {
                    context.UserProfileCurrencyMakerCheckers.Attach(userProfileCurrencyMakerChecker);
                    context.Entry(userProfileCurrencyMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileGeneralLedgerData(UserProfileGeneralLedgerViewModel _userProfileGeneralLedgerViewModel, string _entryType)
        {
            try
            {
                //userProfileDetailRepository.GetUserProfileGeneralLedgerDefaultValues(_userProfileGeneralLedgerViewModel, _entryType);

                configurationDetailRepository.SetDefaultValues(_userProfileGeneralLedgerViewModel, _entryType);
                // Get Prmkey By Id
                _userProfileGeneralLedgerViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_userProfileGeneralLedgerViewModel.GeneralLedgerId);
                var userProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                UserProfileGeneralLedger userProfileGeneralLedger = Mapper.Map<UserProfileGeneralLedger>(_userProfileGeneralLedgerViewModel);
                UserProfileGeneralLedgerMakerChecker userProfileGeneralLedgerMakerChecker = Mapper.Map<UserProfileGeneralLedgerMakerChecker>(_userProfileGeneralLedgerViewModel);
                userProfileGeneralLedgerMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                //set 0 to prmkey/Userprofileprmkey
                userProfileGeneralLedger.UserProfilePrmKey = oldUserProfilePrmKey;
                if (_entryType == StringLiteralValue.Create)
                {
                   

                    context.UserProfileGeneralLedgers.Attach(userProfileGeneralLedger);
                    context.Entry(userProfileGeneralLedger).State = EntityState.Added;

                    context.UserProfileGeneralLedgerMakerCheckers.Attach(userProfileGeneralLedgerMakerChecker);
                    context.Entry(userProfileGeneralLedgerMakerChecker).State = EntityState.Added;
                    userProfileGeneralLedger.UserProfileGeneralLedgerMakerCheckers.Add(userProfileGeneralLedgerMakerChecker);
                }
                else
                {
                    context.UserProfileGeneralLedgerMakerCheckers.Attach(userProfileGeneralLedgerMakerChecker);
                    context.Entry(userProfileGeneralLedgerMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserProfileMenuData(UserProfileMenuViewModel _userProfileMenuViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileMenuViewModel, _entryType);
                _userProfileMenuViewModel.MenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(_userProfileMenuViewModel.ModelMenuId);
                //userProfileDetailRepository.GetUserProfileMenuDefaultValues(_userProfileMenuViewModel, _entryType);

                UserProfileMenu userProfileMenu = Mapper.Map<UserProfileMenu>(_userProfileMenuViewModel);
                UserProfileMenuMakerChecker userProfileMenuMakerChecker = Mapper.Map<UserProfileMenuMakerChecker>(_userProfileMenuViewModel);
                userProfileMenuMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                //set 0 to prmkey/Userprofileprmkey
                userProfileMenu.UserProfilePrmKey = oldUserProfilePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    
                    context.UserProfileMenus.Attach(userProfileMenu);
                    context.Entry(userProfileMenu).State = EntityState.Added;

                    context.UserProfileMenuMakerCheckers.Attach(userProfileMenuMakerChecker);
                    context.Entry(userProfileMenuMakerChecker).State = EntityState.Added;
                    userProfileMenu.UserProfileMenuMakerCheckers.Add(userProfileMenuMakerChecker);
                }
                else
                {
                    context.UserProfileMenuMakerCheckers.Attach(userProfileMenuMakerChecker);
                    context.Entry(userProfileMenuMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }


        public bool AttachUserProfilePasswordPolicyData(UserProfilePasswordPolicyViewModel _userProfilePasswordPolicyViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfilePasswordPolicyViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfilePasswordPolicyDefaultValues(_userProfilePasswordPolicyViewModel, _entryType);

                UserProfilePasswordPolicy userProfilePasswordPolicy = Mapper.Map<UserProfilePasswordPolicy>(_userProfilePasswordPolicyViewModel);
                UserProfilePasswordPolicyMakerChecker userProfilePasswordPolicyMakerChecker = Mapper.Map<UserProfilePasswordPolicyMakerChecker>(_userProfilePasswordPolicyViewModel);
                userProfilePasswordPolicyMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                userProfilePasswordPolicy.UserProfilePrmKey = oldUserProfilePrmKey;

                // Get Prmkey By Id
                userProfilePasswordPolicy.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(_userProfilePasswordPolicyViewModel.PasswordPolicyId);

                //set 0 to prmkey/Userprofileprmkey
                if (_entryType == StringLiteralValue.Create)
                    userProfilePasswordPolicy.UserProfilePrmKey = 0;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;


                    context.UserProfilePasswordPolicy.Attach(userProfilePasswordPolicy);
                    context.Entry(userProfilePasswordPolicy).State = entityState;

                    context.UserProfilePasswordPolicyMakerCheckers.Attach(userProfilePasswordPolicyMakerChecker);
                    context.Entry(userProfilePasswordPolicyMakerChecker).State = EntityState.Added;
                    userProfilePasswordPolicy.UserProfilePasswordPolicyMakerCheckers.Add(userProfilePasswordPolicyMakerChecker);


                }
                else
                {
                    context.UserProfilePasswordPolicyMakerCheckers.Attach(userProfilePasswordPolicyMakerChecker);
                    context.Entry(userProfilePasswordPolicyMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        //AttachUserProfileSpecialPermissionData
        public bool AttachUserProfileSpecialPermissionData(UserProfileSpecialPermissionViewModel _userProfileSpecialPermissionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileSpecialPermissionViewModel, _entryType);
                //userProfileDetailRepository.GetUserProfileSpecialPermissionDefaultValues(_userProfileSpecialPermissionViewModel, _entryType);
                // Get Prmkey By Id
                _userProfileSpecialPermissionViewModel.SpecialPermissionPrmKey = securityDetailRepository.GetSpecialPermissionPrmKeyById(_userProfileSpecialPermissionViewModel.SpecialPermissionId);

                UserProfileSpecialPermission userProfileSpecialPermission = Mapper.Map<UserProfileSpecialPermission>(_userProfileSpecialPermissionViewModel);
                UserProfileSpecialPermissionMakerChecker userProfileSpecialPermissionMakerChecker = Mapper.Map<UserProfileSpecialPermissionMakerChecker>(_userProfileSpecialPermissionViewModel);
                userProfileSpecialPermissionMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //set 0 to prmkey/Userprofileprmkey
                userProfileSpecialPermission.UserProfilePrmKey = oldUserProfilePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.UserProfileSpecialPermissions.Attach(userProfileSpecialPermission);
                    context.Entry(userProfileSpecialPermission).State = EntityState.Added;

                    context.UserProfileSpecialPermissionMakerCheckers.Attach(userProfileSpecialPermissionMakerChecker);
                    context.Entry(userProfileSpecialPermissionMakerChecker).State = EntityState.Added;
                    userProfileSpecialPermission.UserProfileSpecialPermissionMakerCheckers.Add(userProfileSpecialPermissionMakerChecker);

                }
                else
                {
                    context.UserProfileSpecialPermissionMakerCheckers.Attach(userProfileSpecialPermissionMakerChecker);
                    context.Entry(userProfileSpecialPermissionMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        //AttachUserProfileTransactionLimitData
        public bool AttachUserProfileTransactionLimitData(UserProfileTransactionLimitViewModel _userProfileTransactionLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userProfileTransactionLimitViewModel, _entryType);
                // userProfileDetailRepository.GetUserProfileTransactionLimitDefaultValues(_userProfileTransactionLimitViewModel, _entryType);

                // Get Prmkey By Id
                _userProfileTransactionLimitViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_userProfileTransactionLimitViewModel.GeneralLedgerId);
                _userProfileTransactionLimitViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_userProfileTransactionLimitViewModel.TransactionTypeId);
                _userProfileTransactionLimitViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_userProfileTransactionLimitViewModel.CurrencyId);

                UserProfileTransactionLimit UserProfileTransactionLimit = Mapper.Map<UserProfileTransactionLimit>(_userProfileTransactionLimitViewModel);
                UserProfileTransactionLimitMakerChecker UserProfileTransactionLimitMakerChecker = Mapper.Map<UserProfileTransactionLimitMakerChecker>(_userProfileTransactionLimitViewModel);
                UserProfileTransactionLimitMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                
                //set 0 to prmkey/Userprofileprmkey
                UserProfileTransactionLimit.UserProfilePrmKey = oldUserProfilePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    
                    context.UserProfileTransactionLimits.Attach(UserProfileTransactionLimit);
                    context.Entry(UserProfileTransactionLimit).State = EntityState.Added;

                    context.UserProfileTransactionLimitMakerCheckers.Attach(UserProfileTransactionLimitMakerChecker);
                    context.Entry(UserProfileTransactionLimitMakerChecker).State = EntityState.Added;
                    UserProfileTransactionLimit.UserProfileTransactionLimitMakerCheckers.Add(UserProfileTransactionLimitMakerChecker);
                }
                else
                {
                    context.UserProfileTransactionLimitMakerCheckers.Attach(UserProfileTransactionLimitMakerChecker);
                    context.Entry(UserProfileTransactionLimitMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }

        public bool AttachUserRoleProfileData(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userRoleProfileViewModel, _entryType);
                //userProfileDetailRepository.GetUserRoleProfileDefaultValues(_userRoleProfileViewModel, _entryType);

                // Get Prmkey By Id
                _userRoleProfileViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_userRoleProfileViewModel.BusinessOfficeId);
                _userRoleProfileViewModel.RoleProfilePrmKey = securityDetailRepository.GetRoleProfilePrmKeyById(_userRoleProfileViewModel.RoleProfileId);

                UserRoleProfile UserRoleProfile = Mapper.Map<UserRoleProfile>(_userRoleProfileViewModel);
                UserRoleProfileMakerChecker UserRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(_userRoleProfileViewModel);
                UserRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //set 0 to prmkey/Userprofileprmkey
                UserRoleProfile.UserProfilePrmKey = oldUserProfilePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.UserRoleProfiles.Attach(UserRoleProfile);
                    context.Entry(UserRoleProfile).State = EntityState.Added;

                    context.UserRoleProfileMakerCheckers.Attach(UserRoleProfileMakerChecker);
                    context.Entry(UserRoleProfileMakerChecker).State = EntityState.Added;
                    UserRoleProfile.UserRoleProfileMakerCheckers.Add(UserRoleProfileMakerChecker);
                }
                else
                {
                    context.UserRoleProfileMakerCheckers.Attach(UserRoleProfileMakerChecker);
                    context.Entry(UserRoleProfileMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

        }
        public bool AttachUserHomeBranchRoleProfileData(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_userRoleProfileViewModel, _entryType);
                // userProfileDetailRepository.GetUserRoleProfileDefaultValues(_userRoleProfileViewModel, _entryType);

                // Get Prmkey By Id
                _userRoleProfileViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_userRoleProfileViewModel.BusinessOfficeId);
                _userRoleProfileViewModel.RoleProfilePrmKey = securityDetailRepository.GetRoleProfilePrmKeyById(_userRoleProfileViewModel.RoleProfileId);

                UserRoleProfile UserRoleProfile = Mapper.Map<UserRoleProfile>(_userRoleProfileViewModel);
                UserRoleProfileMakerChecker UserRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(_userRoleProfileViewModel);
                UserRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //set 0 to prmkey/Userprofileprmkey
                UserRoleProfile.UserProfilePrmKey = oldUserProfilePrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    context.UserRoleProfiles.Attach(UserRoleProfile);
                    context.Entry(UserRoleProfile).State = EntityState.Added;

                    context.UserRoleProfileMakerCheckers.Attach(UserRoleProfileMakerChecker);
                    context.Entry(UserRoleProfileMakerChecker).State = EntityState.Added;
                    UserRoleProfile.UserRoleProfileMakerCheckers.Add(UserRoleProfileMakerChecker);
                }
                else
                {
                    context.UserRoleProfileMakerCheckers.Attach(UserRoleProfileMakerChecker);
                    context.Entry(UserRoleProfileMakerChecker).State = EntityState.Added;
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
