using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileRepository : IUserProfileRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IUserProfileDetailRepository userProfileDetailRepository;
        private readonly IUserProfileDbContextRepository userProfileDbContextRepository;

        public EFUserProfileRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository,
                                        IEnterpriseDetailRepository _enterpriseDetailRepository, ISecurityDetailRepository _securityDetailRepository, IPersonDetailRepository _personDetailRepository,
                                       IUserProfileDetailRepository _userProfileDetailRepository, IUserProfileDbContextRepository _userProfileDbContextRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            personDetailRepository = _personDetailRepository;
            userProfileDetailRepository = _userProfileDetailRepository;
            userProfileDbContextRepository = _userProfileDbContextRepository;
        }

        public async Task<bool> Amend(UserProfileViewModel _userProfileViewModel)
        {
            try
            {
                // Set Default Value
                _userProfileViewModel.IsEmailIdConfirmed = false;
                _userProfileViewModel.IsAlternateEmailIdConfirmed = false;
                _userProfileViewModel.IsMobileNumberConfirmed = false;
                _userProfileViewModel.IsAlternateMobileNumberConfirmed = false;
                _userProfileViewModel.LastLoginDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastActivityDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastPasswordChangeDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastLockedDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.UserProfileStatus = "INA";

                bool result = true;

                //UserProfile
                if (result)
                {
                    if (_userProfileViewModel.UserProfileModificationPrmKey == 0)
                        result = userProfileDbContextRepository.AttachUserProfileData(_userProfileViewModel, StringLiteralValue.Amend);
                    else
                        result = userProfileDbContextRepository.AttachUserProfileModificationData(_userProfileViewModel, StringLiteralValue.Amend);
                }

                //UserProfileHomeBusinessOffice
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfileHomeBusinessOfficeData(_userProfileViewModel.UserProfileHomeBusinessOfficeViewModel, StringLiteralValue.Amend);


                if (result)
                {
                    IEnumerable<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelListForAmend = await userProfileDetailRepository.GetBusinessOfficeEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);

                    if (userProfileBusinessOfficeViewModelListForAmend != null)
                    {
                        foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelListForAmend)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                    //New Record
                    List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];
                    if (userProfileBusinessOfficeViewModelList != null)
                    {
                        foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }
                if (result)
                {
                    // Amend Old UserProfileCurrency
                    if (_userProfileViewModel.IsShowUserProfileCurrency == true)
                    {
                        IEnumerable<UserProfileCurrencyViewModel> userProfileCurrencyViewModelListForAmend = await userProfileDetailRepository.GetCurrencyEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                        if (userProfileCurrencyViewModelListForAmend != null)
                        {
                            foreach (UserProfileCurrencyViewModel viewModel in userProfileCurrencyViewModelListForAmend)
                            {
                                result = userProfileDbContextRepository.AttachUserProfileCurrencyData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //Get UserProfileCurrency From Session Object New Added Record / Updated Record
                    if (_userProfileViewModel.IsShowUserProfileCurrency == true)
                    {
                        List<UserProfileCurrencyViewModel> UserProfileCurrencyViewModelList = (List<UserProfileCurrencyViewModel>)HttpContext.Current.Session["UserProfileCurrency"];
                        if (UserProfileCurrencyViewModelList != null)
                        {
                            foreach (UserProfileCurrencyViewModel viewModel in UserProfileCurrencyViewModelList)
                            {
                                result = userProfileDbContextRepository.AttachUserProfileCurrencyData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }
                if (result)
                {
                    // Amend Old UserProfileGeneralLedger
                    IEnumerable<UserProfileGeneralLedgerViewModel> userProfileGeneralLedgerViewModelListForAmend = await userProfileDetailRepository.GetGeneralLedgerEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                    if (userProfileGeneralLedgerViewModelListForAmend != null)
                    {
                        foreach (UserProfileGeneralLedgerViewModel viewModel in userProfileGeneralLedgerViewModelListForAmend)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    //Get UserProfileGeneralLedger From Session Object New Added Record / Updated Record
                    List<UserProfileGeneralLedgerViewModel> UserProfileGeneralLedgerViewModelList = (List<UserProfileGeneralLedgerViewModel>)HttpContext.Current.Session["UserProfileGeneralLedger"];
                    if (UserProfileGeneralLedgerViewModelList != null)
                    {
                        foreach (UserProfileGeneralLedgerViewModel viewModel in UserProfileGeneralLedgerViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileGeneralLedgerData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                {
                    // Amend Old UserProfileMenu
                    IEnumerable<UserProfileMenuViewModel> userProfileMenuViewModelListForAmend = await userProfileDetailRepository.GetMenuEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                    if (userProfileMenuViewModelListForAmend != null)
                    {
                        foreach (UserProfileMenuViewModel viewModel in userProfileMenuViewModelListForAmend)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileMenuData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    //Get UserProfileMenu From Session Object New Added Record / Updated Record
                    List<UserProfileMenuViewModel> UserProfileMenuViewModelList = (List<UserProfileMenuViewModel>)HttpContext.Current.Session["UserProfileMenu"];
                    if (UserProfileMenuViewModelList != null)
                    {
                        foreach (UserProfileMenuViewModel viewModel in UserProfileMenuViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileMenuData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // UserProfilePasswordPolicy
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfilePasswordPolicyData(_userProfileViewModel.UserProfilePasswordPolicyViewModel, StringLiteralValue.Amend);


                if (result)
                {
                    // Amend Old UserProfileSpecialPermission
                    IEnumerable<UserProfileSpecialPermissionViewModel> userProfileSpecialPermissionViewModelListForAmend = await userProfileDetailRepository.GetSpecialPermissionEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                    if (userProfileSpecialPermissionViewModelListForAmend != null)
                    {
                        foreach (UserProfileSpecialPermissionViewModel viewModel in userProfileSpecialPermissionViewModelListForAmend)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileSpecialPermissionData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    //Get UserProfileSpecialPermission From Session Object New Added Record / Updated Record
                    List<UserProfileSpecialPermissionViewModel> UserProfileSpecialPermissionViewModelList = (List<UserProfileSpecialPermissionViewModel>)HttpContext.Current.Session["UserProfileSpecialPermission"];
                    if (UserProfileSpecialPermissionViewModelList != null)
                    {
                        foreach (UserProfileSpecialPermissionViewModel viewModel in UserProfileSpecialPermissionViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileSpecialPermissionData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                {
                    // Amend Old UserProfileTransactionLimit
                    IEnumerable<UserProfileTransactionLimitViewModel> userProfileTransactionLimitViewModelListForAmend = await userProfileDetailRepository.GetTransactionLimitEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                    if (userProfileTransactionLimitViewModelListForAmend != null)
                    {
                        foreach (UserProfileTransactionLimitViewModel viewModel in userProfileTransactionLimitViewModelListForAmend)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileTransactionLimitData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    //Get UserProfileTransactionLimit From Session Object New Added Record / Updated Record
                    List<UserProfileTransactionLimitViewModel> UserProfileTransactionLimitViewModelList = (List<UserProfileTransactionLimitViewModel>)HttpContext.Current.Session["UserProfileTransactionLimit"];
                    if (UserProfileTransactionLimitViewModelList != null)
                    {
                        foreach (UserProfileTransactionLimitViewModel viewModel in UserProfileTransactionLimitViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileTransactionLimitData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Amend Old UserRoleProfileMenu
                IEnumerable<UserRoleProfileViewModel> userRoleProfileViewModelListForAmend = await userProfileDetailRepository.GetUserRoleProfileEntries(_userProfileViewModel.PrmKey, StringLiteralValue.Reject);
                if (userRoleProfileViewModelListForAmend != null)
                {
                    foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelListForAmend)
                    {
                        result = userProfileDbContextRepository.AttachUserRoleProfileData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get UserRoleProfile From Session Object New Added Record / Updated Record
                List<UserRoleProfileViewModel> UserRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];
                if (UserRoleProfileViewModelList != null)
                {
                    foreach (UserRoleProfileViewModel viewModel in UserRoleProfileViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserHomeBranchRoleProfileData(viewModel, StringLiteralValue.Create);
                    }
                }
                else
                {
                    //UserHomeBranchRoleProfileData
                    _userProfileViewModel.UserRoleProfileViewModel.RoleProfileId = _userProfileViewModel.RoleProfileId;
                    _userProfileViewModel.UserRoleProfileViewModel.BusinessOfficeId = _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.BusinessOfficeId;
                    _userProfileViewModel.UserRoleProfileViewModel.ActivationDate = _userProfileViewModel.ActivationDate;
                    result = userProfileDbContextRepository.AttachUserHomeBranchRoleProfileData(_userProfileViewModel.UserRoleProfileViewModel, StringLiteralValue.Create);
                }

                if (result)
                    result = await userProfileDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public IEnumerable<UserProfile> GetUserProfiles
        {
            get { return context.UserProfiles; }
        }

        public async Task<bool> GetSessionValues(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["UserProfileBusinessOffice"] = await userProfileDetailRepository.GetBusinessOfficeEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserProfileCurrency"] = await userProfileDetailRepository.GetCurrencyEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserProfileGeneralLedger"] = await userProfileDetailRepository.GetGeneralLedgerEntries(_userProfilePrmKey, _entryType);

                //HttpContext.Current.Session["UserProfileLoginDevice"] = await userProfileDetailRepository.GetLoginDeviceEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserProfileMenu"] = await userProfileDetailRepository.GetMenuEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserProfileSpecialPermission"] = await userProfileDetailRepository.GetSpecialPermissionEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserProfileTransactionLimit"] = await userProfileDetailRepository.GetTransactionLimitEntries(_userProfilePrmKey, _entryType);

                HttpContext.Current.Session["UserRoleProfile"] = await userProfileDetailRepository.GetUserRoleProfileEntries(_userProfilePrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<UserProfileViewModel> GetUserProfileEntry(Guid _userProfileId, string _entryType)
        {
            try
            {
                UserProfileViewModel userProfileViewModel = await context.Database.SqlQuery<UserProfileViewModel>("SELECT * FROM dbo.GetUserProfileEntry (@UserProfileId, @EntryType)", new SqlParameter("@UserProfileId", _userProfileId), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                userProfileViewModel.UserProfileHomeBusinessOfficeViewModel = await userProfileDetailRepository.GetHomeBusinessOfficeEntries(userProfileViewModel.PrmKey, _entryType);

                //userProfileViewModel.UserProfileGroupViewModel = await userProfileDetailRepository.GetGroupEntries(userProfileViewModel.PrmKey, _entryType);

                userProfileViewModel.UserProfilePasswordPolicyViewModel = await userProfileDetailRepository.GetPasswordPolicyEntries(userProfileViewModel.PrmKey, _entryType);

                return userProfileViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileIndexViewModel>> GetUserProfileIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileIndexViewModel>("SELECT * FROM dbo.GetUserProfileEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUserProfilePasswords(short _userProfilePrmKey, string _inputedPassword)
        {
            var result = context.Database.SqlQuery<bool>("SELECT dbo.GetUserProfilePasswords(@UserProfilePrmKey, @InputedPassword)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("@InputedPassword", _inputedPassword)).FirstOrDefault();
            return result;
        }

        public List<SelectListItem> GetMenuByHomeBranch(Guid homeBranchId)
        {
            List<SelectListItem> modelNames = new List<SelectListItem>();
            List<SelectListItem> modelName1 = new List<SelectListItem>();
            string branchType = configurationDetailRepository.GetBranchTypeById(homeBranchId);
            if (branchType == "BR")
            {
                string[] HoMenuCode = { "OPNBAL", "PERINF", "SHARES", "ACCOPN", "TRNSCT" };
                var selectedmenu = context.Menus.Where(c => HoMenuCode.Contains(c.MenuCode) && c.IsVisible == true).ToList();
                //var allmenus = (from e in context.Menus select e).ToList();
                modelNames = (from e in selectedmenu
                              select new SelectListItem
                              {
                                  Value = e.MenuId.ToString(),
                                  Text = e.NameOfMenu
                              }).ToList();
            }
            else
            {
                var selectedmenu = context.Menus.Where(c => c.ParentMenuPrmKey == 0 && c.IsVisible == true).ToList();
                //var allmenus = (from e in context.Menus select e).ToList();
                modelNames = (from e in selectedmenu
                              select new SelectListItem
                              {
                                  Value = e.MenuId.ToString(),
                                  Text = e.NameOfMenu
                              }).ToList();
            }
            modelName1.AddRange(modelNames.ToList());
            return modelName1;
        }

        public async Task<bool> VerifyRejectDelete(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            try
            {
                bool result = true;
                string entriesType;

                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;

                //UserProfile
                if (result)
                {
                    if (_userProfileViewModel.UserProfileModificationPrmKey == 0)
                        result = userProfileDbContextRepository.AttachUserProfileData(_userProfileViewModel, _entryType);
                    else
                        result = userProfileDbContextRepository.AttachUserProfileModificationData(_userProfileViewModel, _entryType);
                }

                //UserProfileHomeBusinessOffice
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfileHomeBusinessOfficeData(_userProfileViewModel.UserProfileHomeBusinessOfficeViewModel, _entryType);

                //UserProfileBusinessOffice
                IEnumerable<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = await userProfileDetailRepository.GetBusinessOfficeEntries(_userProfileViewModel.PrmKey, entriesType);
                if (userProfileBusinessOfficeViewModelList != null)
                {
                    foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserProfileBusinessOfficeData(viewModel, _entryType);
                    }
                }

                //UserProfileCurrency
                if (_userProfileViewModel.IsShowUserProfileCurrency == true)
                {
                    IEnumerable<UserProfileCurrencyViewModel> userProfileCurrencyViewModelList = await userProfileDetailRepository.GetCurrencyEntries(_userProfileViewModel.PrmKey, entriesType);
                    if (userProfileCurrencyViewModelList != null)
                    {
                        foreach (UserProfileCurrencyViewModel viewModel in userProfileCurrencyViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileCurrencyData(viewModel, _entryType);
                        }
                    }
                }

                //UserProfileGeneralLedger
                IEnumerable<UserProfileGeneralLedgerViewModel> userProfileGeneralLedgerViewModelList = await userProfileDetailRepository.GetGeneralLedgerEntries(_userProfileViewModel.PrmKey, entriesType);
                if (userProfileGeneralLedgerViewModelList != null)
                {
                    foreach (UserProfileGeneralLedgerViewModel viewModel in userProfileGeneralLedgerViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserProfileGeneralLedgerData(viewModel, _entryType);
                    }
                }

                //Get UserProfileMenu From Session Object New Added Record / Updated Record
                IEnumerable<UserProfileMenuViewModel> UserProfileMenuViewModelList = await userProfileDetailRepository.GetMenuEntries(_userProfileViewModel.PrmKey, entriesType);
                if (UserProfileMenuViewModelList != null)
                {
                    foreach (UserProfileMenuViewModel viewModel in UserProfileMenuViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserProfileMenuData(viewModel, _entryType);
                    }
                }

                // UserProfilePasswordPolicy
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfilePasswordPolicyData(_userProfileViewModel.UserProfilePasswordPolicyViewModel, _entryType);


                //UserProfileSpecialPermission
                IEnumerable<UserProfileSpecialPermissionViewModel> userProfileSpecialPermissionViewModelList = await userProfileDetailRepository.GetSpecialPermissionEntries(_userProfileViewModel.PrmKey, entriesType);
                if (userProfileSpecialPermissionViewModelList != null)
                {
                    foreach (UserProfileSpecialPermissionViewModel viewModel in userProfileSpecialPermissionViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserProfileSpecialPermissionData(viewModel, _entryType);
                    }

                }

                //UserProfileTransactionLimit
                IEnumerable<UserProfileTransactionLimitViewModel> userProfileTransactionLimitViewModelList = await userProfileDetailRepository.GetTransactionLimitEntries(_userProfileViewModel.PrmKey, entriesType);
                if (userProfileTransactionLimitViewModelList != null)
                {
                    foreach (UserProfileTransactionLimitViewModel viewModel in userProfileTransactionLimitViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserProfileTransactionLimitData(viewModel, _entryType);
                    }
                }

                //UserRoleProfile
                IEnumerable<UserRoleProfileViewModel> userRoleProfileViewModelList = await userProfileDetailRepository.GetUserRoleProfileEntries(_userProfileViewModel.PrmKey, entriesType);
                if (userRoleProfileViewModelList != null)
                {
                    foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelList)
                    {
                        result = userProfileDbContextRepository.AttachUserRoleProfileData(viewModel, _entryType);
                    }
                }

                if (result)
                    result = await userProfileDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //Transaction Table Entries Are Modified After Verification, So For Current Operation(i.e.Create / Modify) Not Required Modify Old Entries***
        public async Task<bool> Save(UserProfileViewModel _userProfileViewModel)
        {
            try
            {
                bool result = true;

                // Set Default Value
                _userProfileViewModel.IsEmailIdConfirmed = false;
                _userProfileViewModel.IsAlternateEmailIdConfirmed = false;
                _userProfileViewModel.IsMobileNumberConfirmed = false;
                _userProfileViewModel.IsAlternateMobileNumberConfirmed = false;
                _userProfileViewModel.LastLoginDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastActivityDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastPasswordChangeDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.LastLockedDate = DateTime.Parse("1900-01-01 00:00:00");
                _userProfileViewModel.UserProfileStatus = "INA";
                _userProfileViewModel.CreateDate = DateTime.Now;
                _userProfileViewModel.UserId = _userProfileViewModel.NameOfUserProfile;

                //userProfileDetailRepository.GetUserProfileDefaultValues(_userProfileViewModel, StringLiteralValue.Create);

                //UserProfile
                if (result)
                {
                    result = userProfileDbContextRepository.AttachUserProfileData(_userProfileViewModel, StringLiteralValue.Create);
                }

                //UserProfileAccessibility
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfileAccessibilityData(_userProfileViewModel, StringLiteralValue.Create);

                //UserProfileIdentity
                if (result)
                    result = userProfileDbContextRepository.AttachUserProfileIdentityData(_userProfileViewModel, StringLiteralValue.Create);

                //UserProfileHomeBusinessOffice

                if (result)
                    result = userProfileDbContextRepository.AttachUserProfileHomeBusinessOfficeData(_userProfileViewModel.UserProfileHomeBusinessOfficeViewModel, StringLiteralValue.Create);

                //UserProfileBusinessOffice

                if (result)
                {
                    List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];
                    if (userProfileBusinessOfficeViewModelList != null)
                    {

                        foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //UserProfileCurrency
                if (result)
                {
                    if (_userProfileViewModel.IsShowUserProfileCurrency == true)
                    {

                        List<UserProfileCurrencyViewModel> userProfileCurrencyViewModelList = (List<UserProfileCurrencyViewModel>)HttpContext.Current.Session["UserProfileCurrency"];
                        if (userProfileCurrencyViewModelList != null)
                        {
                            foreach (UserProfileCurrencyViewModel viewModel in userProfileCurrencyViewModelList)
                            {

                                result = userProfileDbContextRepository.AttachUserProfileCurrencyData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //UserProfileGeneralLedger
                if (result)
                {
                    List<UserProfileGeneralLedgerViewModel> UserProfileGeneralLedgerViewModelList = (List<UserProfileGeneralLedgerViewModel>)HttpContext.Current.Session["UserProfileGeneralLedger"];
                    if (UserProfileGeneralLedgerViewModelList != null)
                    {
                        foreach (UserProfileGeneralLedgerViewModel viewModel in UserProfileGeneralLedgerViewModelList)
                        {

                            result = userProfileDbContextRepository.AttachUserProfileGeneralLedgerData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //UserProfileMenu
                if (result)
                {
                    List<UserProfileMenuViewModel> UserProfileMenuViewModelList = (List<UserProfileMenuViewModel>)HttpContext.Current.Session["UserProfileMenu"];
                    if (UserProfileMenuViewModelList != null)
                    {
                        var MainUserProfileMenuViewModelList = UserProfileMenuViewModelList.GroupBy(x => x.ModelMenuId).Select(x => x.FirstOrDefault());
                        foreach (UserProfileMenuViewModel viewModel in MainUserProfileMenuViewModelList)
                        {

                            result = userProfileDbContextRepository.AttachUserProfileMenuData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //UserProfilePasswordPolicy

                if (result)
                    result = userProfileDbContextRepository.AttachUserProfilePasswordPolicyData(_userProfileViewModel.UserProfilePasswordPolicyViewModel, StringLiteralValue.Create);

                //UserProfileSpecialPermission
                if (result)
                {
                    List<UserProfileSpecialPermissionViewModel> UserProfileSpecialPermissionViewModelList = (List<UserProfileSpecialPermissionViewModel>)HttpContext.Current.Session["UserProfileSpecialPermission"];
                    if (UserProfileSpecialPermissionViewModelList != null)
                    {
                        foreach (UserProfileSpecialPermissionViewModel viewModel in UserProfileSpecialPermissionViewModelList)
                        {

                            result = userProfileDbContextRepository.AttachUserProfileSpecialPermissionData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //UserProfileTransactionLimit
                if (result)
                {
                    List<UserProfileTransactionLimitViewModel> UserProfileTransactionLimitViewModelList = (List<UserProfileTransactionLimitViewModel>)HttpContext.Current.Session["UserProfileTransactionLimit"];
                    if (UserProfileTransactionLimitViewModelList != null)
                    {
                        foreach (UserProfileTransactionLimitViewModel viewModel in UserProfileTransactionLimitViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserProfileTransactionLimitData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //UserRoleProfile
                if (result)
                {
                    List<UserRoleProfileViewModel> UserRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];
                    if (UserRoleProfileViewModelList != null)
                    {
                        foreach (UserRoleProfileViewModel viewModel in UserRoleProfileViewModelList)
                        {
                            result = userProfileDbContextRepository.AttachUserRoleProfileData(viewModel, StringLiteralValue.Create);
                        }
                    }
                    else
                    {
                        //UserHomeBranchRoleProfileData
                        _userProfileViewModel.UserRoleProfileViewModel.RoleProfileId = _userProfileViewModel.RoleProfileId;
                        _userProfileViewModel.UserRoleProfileViewModel.BusinessOfficeId = _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.BusinessOfficeId;
                        result = userProfileDbContextRepository.AttachUserHomeBranchRoleProfileData(_userProfileViewModel.UserRoleProfileViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                    result = await userProfileDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

    }
}
