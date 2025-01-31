using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileDetailRepository : IUserProfileDetailRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileCurrencyViewModel>> GetCurrencyEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileCurrencyViewModel>("SELECT * FROM dbo.GetUserProfileCurrencyEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileGeneralLedgerViewModel>("SELECT * FROM dbo.GetUserProfileGeneralLedgerEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetLoginDeviceEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileLoginDeviceViewModel>("SELECT * FROM dbo.GetUserProfileLoginDeviceEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileMenuViewModel>> GetMenuEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileMenuViewModel>("SELECT * FROM dbo.GetUserProfileMenuEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileSpecialPermissionViewModel>("SELECT * FROM dbo.GetUserProfileSpecialPermissionEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SpecialPermissionPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SpecialPermissionPrmkey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileTransactionLimitViewModel>> GetTransactionLimitEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileTransactionLimitViewModel>("SELECT * FROM dbo.GetUserProfileTransactionLimitEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserRoleProfileViewModel>> GetUserRoleProfileEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserRoleProfileViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileHomeBusinessOfficeViewModel> GetHomeBusinessOfficeEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileHomeBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileHomeBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfileGroupViewModel> GetGroupEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileGroupViewModel>("SELECT * FROM dbo.GetUserProfileGroupEntriesByUserProfilePrmKey (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<UserProfilePasswordPolicyViewModel> GetPasswordPolicyEntries(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfilePasswordPolicyViewModel>("SELECT * FROM dbo.GetUserProfilePasswordPolicyEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void GetUserProfileAllDefaultValues(UserProfileViewModel _userProfileViewModel, string _entryStatus)
        {
            _userProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserAction = _entryStatus;

            // UserProfileAccessibilityViewModel
            _userProfileViewModel.UserProfileAccessibilityViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            // UserProfileMenuViewModel
            _userProfileViewModel.UserProfileMenuViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileMenuViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileMenuViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileMenuViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileMenuViewModel.UserAction = _entryStatus;

            // UserProfilePasswordPolicyViewModel
            _userProfileViewModel.UserProfilePasswordPolicyViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfilePasswordPolicyViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfilePasswordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfilePasswordPolicyViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfilePasswordPolicyViewModel.UserAction = _entryStatus;

            // UserProfileSpecialPermissionViewModel
            _userProfileViewModel.UserProfileSpecialPermissionViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileSpecialPermissionViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileSpecialPermissionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileSpecialPermissionViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileSpecialPermissionViewModel.UserAction = _entryStatus;

            // UserProfileTransactionLimitViewModel
            _userProfileViewModel.UserProfileTransactionLimitViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileTransactionLimitViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileTransactionLimitViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileTransactionLimitViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileTransactionLimitViewModel.UserAction = _entryStatus;

            // UserProfileBusinessOfficeViewModel
            _userProfileViewModel.UserProfileBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileBusinessOfficeViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileBusinessOfficeViewModel.UserAction = _entryStatus;

            // UserProfileCurrencyViewModel
            _userProfileViewModel.UserProfileCurrencyViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileCurrencyViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileCurrencyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileCurrencyViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileCurrencyViewModel.UserAction = _entryStatus;

            // UserProfileGeneralLedgerViewModel
            _userProfileViewModel.UserProfileGeneralLedgerViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileGeneralLedgerViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileGeneralLedgerViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileGeneralLedgerViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileGeneralLedgerViewModel.UserAction = _entryStatus;

            // UserRoleProfileViewModel
            _userProfileViewModel.UserRoleProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserRoleProfileViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserRoleProfileViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserRoleProfileViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserRoleProfileViewModel.UserAction = _entryStatus;

            // UserProfileHomeBusinessOfficeViewModel
            _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileMenuViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfilePasswordPolicyViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileSpecialPermissionViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileTransactionLimitViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileBusinessOfficeViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileCurrencyViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileGeneralLedgerViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserRoleProfileViewModel.ReasonForModification = "None";
                _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.ReasonForModification = "None";

            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileViewModel.Remark = "None";
                _userProfileViewModel.UserProfileMenuViewModel.Remark = "None";
                _userProfileViewModel.UserProfilePasswordPolicyViewModel.Remark = "None";
                _userProfileViewModel.UserProfileSpecialPermissionViewModel.Remark = "None";
                _userProfileViewModel.UserProfileTransactionLimitViewModel.Remark = "None";
                _userProfileViewModel.UserProfileBusinessOfficeViewModel.Remark = "None";
                _userProfileViewModel.UserProfileCurrencyViewModel.Remark = "None";
                _userProfileViewModel.UserProfileGeneralLedgerViewModel.Remark = "None";
                _userProfileViewModel.UserRoleProfileViewModel.Remark = "None";
                _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.Remark = "None";
            }
            else
            {
                _userProfileViewModel.UserProfileMenuViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfilePasswordPolicyViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileSpecialPermissionViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileTransactionLimitViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileBusinessOfficeViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileCurrencyViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileGeneralLedgerViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserRoleProfileViewModel.Remark = _userProfileViewModel.Remark;
                _userProfileViewModel.UserProfileHomeBusinessOfficeViewModel.Remark = _userProfileViewModel.Remark;
            }
        }

        public void GetUserProfileDefaultValues(UserProfileViewModel _userProfileViewModel, string _entryStatus)
        {
            _userProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileViewModel.EntryDateTime = DateTime.Now;
            _userProfileViewModel.EntryStatus = _entryStatus;
            _userProfileViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileViewModel.Remark = "None";
            }
        }

        public void GetUserProfileBusinessOfficeDefaultValues(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string _entryStatus)
        {
            _userProfileBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _userProfileBusinessOfficeViewModel.EntryStatus = _entryStatus;
            _userProfileBusinessOfficeViewModel.UserAction = _entryStatus;


            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileBusinessOfficeViewModel.ReasonForModification = "None";
                _userProfileBusinessOfficeViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileBusinessOfficeViewModel.Remark = "None";
            }
        }

        public void GetUserProfileCurrencyDefaultValues(UserProfileCurrencyViewModel _userProfileCurrencyViewModel, string _entryStatus)
        {
            _userProfileCurrencyViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileCurrencyViewModel.EntryDateTime = DateTime.Now;
            _userProfileCurrencyViewModel.EntryStatus = _entryStatus;
            _userProfileCurrencyViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileCurrencyViewModel.ReasonForModification = "None";
                _userProfileCurrencyViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileCurrencyViewModel.Remark = "None";
            }
        }

        public void GetUserProfileGeneralLedgerDefaultValues(UserProfileGeneralLedgerViewModel _userProfileGeneralLedgerViewModel, string _entryStatus)
        {
            _userProfileGeneralLedgerViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileGeneralLedgerViewModel.EntryDateTime = DateTime.Now;
            _userProfileGeneralLedgerViewModel.EntryStatus = _entryStatus;
            _userProfileGeneralLedgerViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileGeneralLedgerViewModel.ReasonForModification = "None";
                _userProfileGeneralLedgerViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileGeneralLedgerViewModel.Remark = "None";
            }
        }

        public void GetUserProfileLoginDeviceDefaultValues(UserProfileLoginDeviceViewModel _userProfileLoginDeviceViewModel, string _entryStatus)
        {
            _userProfileLoginDeviceViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileLoginDeviceViewModel.EntryDateTime = DateTime.Now;
            _userProfileLoginDeviceViewModel.EntryStatus = _entryStatus;
            _userProfileLoginDeviceViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileLoginDeviceViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileLoginDeviceViewModel.Remark = "None";
            }
        }

        public void GetUserProfileMenuDefaultValues(UserProfileMenuViewModel _userProfileMenuViewModel, string _entryStatus)
        {
            _userProfileMenuViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileMenuViewModel.EntryDateTime = DateTime.Now;
            _userProfileMenuViewModel.EntryStatus = _entryStatus;
            _userProfileMenuViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileMenuViewModel.ReasonForModification = "None";
                _userProfileMenuViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileMenuViewModel.Remark = "None";
            }
        }

        public void GetUserProfileSpecialPermissionDefaultValues(UserProfileSpecialPermissionViewModel _userProfileSpecialPermissionViewModel, string _entryStatus)
        {
            _userProfileSpecialPermissionViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileSpecialPermissionViewModel.EntryDateTime = DateTime.Now;
            _userProfileSpecialPermissionViewModel.EntryStatus = _entryStatus;
            _userProfileSpecialPermissionViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileSpecialPermissionViewModel.ReasonForModification = "None";
                _userProfileSpecialPermissionViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileSpecialPermissionViewModel.Remark = "None";
            }
        }

        public void GetUserProfileTransactionLimitDefaultValues(UserProfileTransactionLimitViewModel _userProfileTransactionLimitViewModel, string _entryStatus)
        {
            _userProfileTransactionLimitViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileTransactionLimitViewModel.EntryDateTime = DateTime.Now;
            _userProfileTransactionLimitViewModel.EntryStatus = _entryStatus;
            _userProfileTransactionLimitViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileTransactionLimitViewModel.ReasonForModification = "None";
                _userProfileTransactionLimitViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileTransactionLimitViewModel.Remark = "None";
            }
        }

        public void GetUserRoleProfileDefaultValues(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryStatus)
        {
            _userRoleProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _userRoleProfileViewModel.EntryDateTime = DateTime.Now;
            _userRoleProfileViewModel.EntryStatus = _entryStatus;
            _userRoleProfileViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userRoleProfileViewModel.ReasonForModification = "None";
                _userRoleProfileViewModel.CloseDate = null;
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userRoleProfileViewModel.Remark = "None";
            }
        }

        public void GetUserProfileHomeBusinessOfficeDefaultValues(UserProfileHomeBusinessOfficeViewModel _userProfileHomeBusinessOfficeViewModel, string _entryStatus)
        {
            _userProfileHomeBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfileHomeBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _userProfileHomeBusinessOfficeViewModel.EntryStatus = _entryStatus;
            _userProfileHomeBusinessOfficeViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfileHomeBusinessOfficeViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfileHomeBusinessOfficeViewModel.Remark = "None";
            }
        }

        public void GetUserProfilePasswordPolicyDefaultValues(UserProfilePasswordPolicyViewModel _userProfilePasswordPolicyViewModel, string _entryStatus)
        {
            _userProfilePasswordPolicyViewModel.ActivationStatus = StringLiteralValue.Active;
            _userProfilePasswordPolicyViewModel.EntryDateTime = DateTime.Now;
            _userProfilePasswordPolicyViewModel.EntryStatus = _entryStatus;
            _userProfilePasswordPolicyViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _userProfilePasswordPolicyViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _userProfilePasswordPolicyViewModel.Remark = "None";
            }
        }
    }
}
