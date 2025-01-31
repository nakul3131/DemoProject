using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.Services.Concrete.Security.UserRoles
{
    public class EFRoleProfileRepository : IRoleProfileRepository
    {
        private readonly EFDbContext context;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IRoleProfileDetailRepository roleProfileDetailRepository;
        private readonly IRoleProfileDbContextRepository roleProfileDbContextRepository;

        public EFRoleProfileRepository(RepositoryConnection _connection, IEnterpriseDetailRepository _enterpriseDetailRepository, IAccountDetailRepository _accountDetailRepository,
                                        IConfigurationDetailRepository _configurationDetailRepository, ISecurityDetailRepository _securityDetailRepository,
                                        IRoleProfileDetailRepository _roleProfileDetailRepository, IRoleProfileDbContextRepository _roleProfileDbContextRepository)
        {
            context = _connection.EFDbContext;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            roleProfileDetailRepository = _roleProfileDetailRepository;
            roleProfileDbContextRepository = _roleProfileDbContextRepository;
        }

        public List<SelectListItem> GetModelEntries(Guid MenuId)
        {
            List<SelectListItem> modelNames = new List<SelectListItem>();
            List<SelectListItem> modelName1 = new List<SelectListItem>();
            int ParentMenuPrmKey = configurationDetailRepository.GetMenuPrmKeyById(MenuId);
            var mainmenu = context.Menus.Find(ParentMenuPrmKey);
            if (mainmenu != null)
            {
                var selectedmenu = context.Menus.Where(c => c.ParentMenuPrmKey == ParentMenuPrmKey && c.IsVisible == true).ToList();
                //var allmenus = (from e in context.Menus select e).ToList();
                modelNames = (from e in selectedmenu
                              select new SelectListItem
                              {
                                  Value = e.MenuId.ToString(),
                                  Text = e.NameOfMenu
                              }).ToList();
            };

            modelName1.AddRange(modelNames.ToList());
            return modelName1;
        }

        public async Task<bool> Amend(RoleProfileViewModel _roleProfileViewModel)
        {
            try
            {
                bool result = true;
                if (result)

                    // Check Entry Existance In Modification Table Or Main Table
                    if (_roleProfileViewModel.RoleProfileModificationPrmKey == 0)
                        result = roleProfileDbContextRepository.AttachRoleProfileData(_roleProfileViewModel, StringLiteralValue.Amend);
                    else
                        result = roleProfileDbContextRepository.AttachRoleProfileModificationData(_roleProfileViewModel, StringLiteralValue.Amend);

                // Amend Old RoleProfileGeneralLedger
                IEnumerable<RoleProfileGeneralLedgerViewModel> roleProfileGeneralLedgerViewModelListForAmend = await roleProfileDetailRepository.GetGeneralLedgerEntries(_roleProfileViewModel.RoleProfilePrmKey, StringLiteralValue.Reject);
                if (roleProfileGeneralLedgerViewModelListForAmend != null)
                {
                    foreach (RoleProfileGeneralLedgerViewModel viewModel in roleProfileGeneralLedgerViewModelListForAmend)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileGeneralLedgerData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get RoleProfileGeneralLedger From Session Object New Added Record / Updated Record
                List<RoleProfileGeneralLedgerViewModel> RoleProfileGeneralLedgerViewModelList = (List<RoleProfileGeneralLedgerViewModel>)HttpContext.Current.Session["RoleProfileGeneralLedger"];
                if (RoleProfileGeneralLedgerViewModelList != null)
                {
                    foreach (RoleProfileGeneralLedgerViewModel viewModel in RoleProfileGeneralLedgerViewModelList)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileGeneralLedgerData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old RoleProfileBusinessOffice
                IEnumerable<RoleProfileBusinessOfficeViewModel> roleProfileBusinessOfficeViewModelListForAmend = await roleProfileDetailRepository.GetBusinessOfficeEntries(_roleProfileViewModel.RoleProfilePrmKey, StringLiteralValue.Reject);
                if (roleProfileBusinessOfficeViewModelListForAmend != null)
                {
                    foreach (RoleProfileBusinessOfficeViewModel viewModel in roleProfileBusinessOfficeViewModelListForAmend)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get RoleProfileBusinessOffice From Session Object New Added Record / Updated Record
                List<RoleProfileBusinessOfficeViewModel> RoleProfileBusinessOfficeViewModelList = (List<RoleProfileBusinessOfficeViewModel>)HttpContext.Current.Session["RoleProfileBusinessOffice"];
                if (RoleProfileBusinessOfficeViewModelList != null)
                {
                    foreach (RoleProfileBusinessOfficeViewModel viewModel in RoleProfileBusinessOfficeViewModelList)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileBusinessOfficeData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old RoleProfileTransactionLimit
                IEnumerable<RoleProfileTransactionLimitViewModel> roleProfileTransactionLimitViewModelListForAmend = await roleProfileDetailRepository.GetTransactionLimitEntries(_roleProfileViewModel.RoleProfilePrmKey, StringLiteralValue.Reject);
                if (roleProfileTransactionLimitViewModelListForAmend != null)
                {
                    foreach (RoleProfileTransactionLimitViewModel viewModel in roleProfileTransactionLimitViewModelListForAmend)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileTransactionLimitData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get RoleProfileTransactionLimit From Session Object New Added Record / Updated Record
                List<RoleProfileTransactionLimitViewModel> RoleProfileTransactionLimitViewModelList = (List<RoleProfileTransactionLimitViewModel>)HttpContext.Current.Session["RoleProfileTransactionLimit"];
                if (RoleProfileTransactionLimitViewModelList != null)
                {
                    foreach (RoleProfileTransactionLimitViewModel viewModel in RoleProfileTransactionLimitViewModelList)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileTransactionLimitData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old RoleProfileMenu
                IEnumerable<RoleProfileMenuViewModel> roleProfileMenuViewModelListForAmend = await roleProfileDetailRepository.GetMenuEntries(_roleProfileViewModel.RoleProfilePrmKey, StringLiteralValue.Reject);
                if (roleProfileMenuViewModelListForAmend != null)
                {
                    foreach (RoleProfileMenuViewModel viewModel in roleProfileMenuViewModelListForAmend)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileMenuData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get RoleProfileMenu From Session Object New Added Record / Updated Record
                List<RoleProfileMenuViewModel> RoleProfileMenuViewModelList = (List<RoleProfileMenuViewModel>)HttpContext.Current.Session["RoleProfileMenu"];
                if (RoleProfileMenuViewModelList != null)
                {
                    foreach (RoleProfileMenuViewModel viewModel in RoleProfileMenuViewModelList)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileMenuData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old RoleProfileSpecialPermission
                IEnumerable<RoleProfileSpecialPermissionViewModel> roleProfileSpecialPermissionViewModelListForAmend = await roleProfileDetailRepository.GetSpecialPermissionEntries(_roleProfileViewModel.RoleProfilePrmKey, StringLiteralValue.Reject);
                if (roleProfileSpecialPermissionViewModelListForAmend != null)
                {
                    foreach (RoleProfileSpecialPermissionViewModel viewModel in roleProfileSpecialPermissionViewModelListForAmend)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileSpecialPermissionData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get RoleProfileSpecialPermission From Session Object New Added Record / Updated Record
                List<RoleProfileSpecialPermissionViewModel> RoleProfileSpecialPermissionViewModelList = (List<RoleProfileSpecialPermissionViewModel>)HttpContext.Current.Session["RoleProfileSpecialPermission"];
                if (RoleProfileSpecialPermissionViewModelList != null)
                {
                    foreach (RoleProfileSpecialPermissionViewModel viewModel in RoleProfileSpecialPermissionViewModelList)
                    {
                        result = roleProfileDbContextRepository.AttachRoleProfileSpecialPermissionData(viewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                    result = await roleProfileDbContextRepository.SaveData();

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

        public async Task<IEnumerable<RoleProfileIndexViewModel>> GetRoleProfileIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileIndexViewModel>("SELECT * FROM dbo.GetRoleProfileEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> GetSessionValues(short _roleProfilePrmKey, string _entryType)
        {
            try
            {

                HttpContext.Current.Session["RoleProfileGeneralLedger"] = await roleProfileDetailRepository.GetGeneralLedgerEntries(_roleProfilePrmKey, _entryType);

                HttpContext.Current.Session["RoleProfileBusinessOffice"] = await roleProfileDetailRepository.GetBusinessOfficeEntries(_roleProfilePrmKey, _entryType);

                HttpContext.Current.Session["RoleProfileTransactionLimit"] = await roleProfileDetailRepository.GetTransactionLimitEntries(_roleProfilePrmKey, _entryType);

                HttpContext.Current.Session["RoleProfileMenu"] = await roleProfileDetailRepository.GetMenuEntries(_roleProfilePrmKey, _entryType);

                HttpContext.Current.Session["RoleProfileSpecialPermission"] = await roleProfileDetailRepository.GetSpecialPermissionEntries(_roleProfilePrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<RoleProfileViewModel> GetRoleProfileEntry(Guid _roleProfileId, string _entryType)
        {
            //RoleProfileViewModel roleProfileViewModel = new RoleProfileViewModel();
            try
            {
                RoleProfileViewModel roleProfileViewModel = await context.Database.SqlQuery<RoleProfileViewModel>("SELECT * FROM dbo.GetRoleProfileEntry (@RoleProfileId, @EntriesType)", new SqlParameter("@RoleProfileId", _roleProfileId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return roleProfileViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

        }

        public RoleProfileViewModel GetRoleProfileAllowAllAccess(Guid _roleProfileId)
        {
            try
            {
                return context.RoleProfiles
                       .Where(r => r.RoleProfileId == _roleProfileId)
                       .Select(r => new RoleProfileViewModel
                       {
                           IsAllowAllAccessForBusinessOffice = r.IsAllowAllAccessForBusinessOffice,
                           IsAllowAllAccessForGeneralLedger = r.IsAllowAllAccessForGeneralLedger,
                           IsAllowAllAccessForMenu = r.IsAllowAllAccessForMenu,
                           IsAllowAllAccessForSpecialPermission = r.IsAllowAllAccessForSpecialPermission,
                           IsAllowAllTransactions = r.IsAllowAllTransactions
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }

        public bool GetUniqueRoleProfileName(string _nameOfRoleProfile)
        {
            bool status;
            if (context.RoleProfiles.Where(p => p.NameOfRoleProfile == _nameOfRoleProfile).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public async Task<bool> VerifyRejectDelete(RoleProfileViewModel _roleProfileViewModel, string _entryType)
        {
            try
            {
                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;

                bool result = true;
                if (result)
                {

                    if (_roleProfileViewModel.RoleProfileModificationPrmKey == 0)
                        result = roleProfileDbContextRepository.AttachRoleProfileData(_roleProfileViewModel, _entryType);
                    else
                        result = roleProfileDbContextRepository.AttachRoleProfileModificationData(_roleProfileViewModel, _entryType);
                }

                //RoleProfileGeneralLedger
                if (result)
                {
                    IEnumerable<RoleProfileGeneralLedgerViewModel> roleProfileGeneralLedgerViewModelList = await roleProfileDetailRepository.GetGeneralLedgerEntries(_roleProfileViewModel.RoleProfilePrmKey, entriesType);
                    if (roleProfileGeneralLedgerViewModelList != null)
                    {
                        foreach (RoleProfileGeneralLedgerViewModel viewModel in roleProfileGeneralLedgerViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileGeneralLedgerData(viewModel, _entryType);
                        }
                    }
                }

                //RoleProfileBusinessOffice

                if (result)
                {
                    IEnumerable<RoleProfileBusinessOfficeViewModel> roleProfileBusinessOfficeViewModelList = await roleProfileDetailRepository.GetBusinessOfficeEntries(_roleProfileViewModel.RoleProfilePrmKey, entriesType);
                    if (roleProfileBusinessOfficeViewModelList != null)
                    {
                        foreach (RoleProfileBusinessOfficeViewModel viewModel in roleProfileBusinessOfficeViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileBusinessOfficeData(viewModel, _entryType);
                        }
                    }
                }

                //RoleProfileTransactionLimit
                if (result)
                {
                    IEnumerable<RoleProfileTransactionLimitViewModel> roleProfileTransactionLimitViewModelList = await roleProfileDetailRepository.GetTransactionLimitEntries(_roleProfileViewModel.RoleProfilePrmKey, entriesType);
                    if (roleProfileTransactionLimitViewModelList != null)
                    {
                        foreach (RoleProfileTransactionLimitViewModel viewModel in roleProfileTransactionLimitViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileTransactionLimitData(viewModel, _entryType);
                        }
                    }
                }

                //RoleProfileMenu
                if (result)
                {
                    IEnumerable<RoleProfileMenuViewModel> roleProfileMenuViewModelList = await roleProfileDetailRepository.GetMenuEntries(_roleProfileViewModel.RoleProfilePrmKey, entriesType);
                    if (roleProfileMenuViewModelList != null)
                    {
                        foreach (RoleProfileMenuViewModel viewModel in roleProfileMenuViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileMenuData(viewModel, _entryType);

                        }
                    }
                }

                //RoleProfileSpecialPermission
                if (result)
                {
                    IEnumerable<RoleProfileSpecialPermissionViewModel> roleProfileSpecialPermissionViewModelList = await roleProfileDetailRepository.GetSpecialPermissionEntries(_roleProfileViewModel.RoleProfilePrmKey, entriesType);
                    if (roleProfileSpecialPermissionViewModelList != null)
                    {
                        foreach (RoleProfileSpecialPermissionViewModel viewModel in roleProfileSpecialPermissionViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileSpecialPermissionData(viewModel, _entryType);

                        }
                    }
                }

                if (result)
                    result = await roleProfileDbContextRepository.SaveData();

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

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(RoleProfileViewModel _roleProfileViewModel)
        {
            try
            {
                bool result = true;

                if (result)
                    result = roleProfileDbContextRepository.AttachRoleProfileData(_roleProfileViewModel, StringLiteralValue.Create);

                //RoleProfileGeneralLedger
                if (result)
                {
                    List<RoleProfileGeneralLedgerViewModel> RoleProfileGeneralLedgerViewModelList = (List<RoleProfileGeneralLedgerViewModel>)HttpContext.Current.Session["RoleProfileGeneralLedger"];

                    if (RoleProfileGeneralLedgerViewModelList != null)
                    {
                        foreach (RoleProfileGeneralLedgerViewModel viewModel in RoleProfileGeneralLedgerViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileGeneralLedgerData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //Get BusinessOffice From Session Object New Added Record / Updated Record
                if (result)
                {
                    List<RoleProfileBusinessOfficeViewModel> RoleProfileBusinessOfficeViewModelList = (List<RoleProfileBusinessOfficeViewModel>)HttpContext.Current.Session["RoleProfileBusinessOffice"];
                    if (RoleProfileBusinessOfficeViewModelList != null)
                    {
                        foreach (RoleProfileBusinessOfficeViewModel viewModel in RoleProfileBusinessOfficeViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //Get TransactionLimit From Session Object New Added Record / Updated Record
                if (result)
                {
                    List<RoleProfileTransactionLimitViewModel> RoleProfileTransactionLimitViewModelList = (List<RoleProfileTransactionLimitViewModel>)HttpContext.Current.Session["RoleProfileTransactionLimit"];
                    if (RoleProfileTransactionLimitViewModelList != null)
                    {
                        foreach (RoleProfileTransactionLimitViewModel viewModel in RoleProfileTransactionLimitViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileTransactionLimitData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //Get RoleProfileMenu From Session Object New Added Record / Updated Record
                if (result)
                {
                    List<RoleProfileMenuViewModel> RoleProfileMenuViewModelList = (List<RoleProfileMenuViewModel>)HttpContext.Current.Session["RoleProfileMenu"];
                    if (RoleProfileMenuViewModelList != null)
                    {
                        foreach (RoleProfileMenuViewModel viewModel in RoleProfileMenuViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileMenuData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                //Get RoleProfileSpecialPermission From Session Object New Added Record / Updated Record
                if (result)
                {
                    List<RoleProfileSpecialPermissionViewModel> RoleProfileSpecialPermissionViewModelList = (List<RoleProfileSpecialPermissionViewModel>)HttpContext.Current.Session["RoleProfileSpecialPermission"];
                    if (RoleProfileSpecialPermissionViewModelList != null)
                    {
                        foreach (RoleProfileSpecialPermissionViewModel viewModel in RoleProfileSpecialPermissionViewModelList)
                        {
                            result = roleProfileDbContextRepository.AttachRoleProfileSpecialPermissionData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                if (result)
                    result = await roleProfileDbContextRepository.SaveData();

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

