using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Security.UserRoles
{
    public class EFRoleProfileDetailRepository : IRoleProfileDetailRepository
    {
        private readonly EFDbContext context;

        public EFRoleProfileDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<RoleProfileGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _roleProfilePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<RoleProfileGeneralLedgerViewModel>("SELECT * FROM dbo.GetRoleProfileGeneralLedgerEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _roleProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileBusinessOfficeViewModel>("SELECT * FROM dbo.GetRoleProfileBusinessOfficeEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetTransactionLimitEntries(short _roleProfilePrmKey, string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<RoleProfileTransactionLimitViewModel>("SELECT * FROM dbo.GetRoleProfileTransactionLimitEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileMenuViewModel>> GetMenuEntries(short _roleProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileMenuViewModel>("SELECT * FROM dbo.GetRoleProfileMenuEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<RoleProfileSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _roleProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<RoleProfileSpecialPermissionViewModel>("SELECT * FROM dbo.GetRoleProfileSpecialPermissionEntriesByRoleProfilePrmKey (@UserProfilePrmKey, @RoleProfilePrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@RoleProfilePrmkey", _roleProfilePrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void GetRoleProfileAllDefaultValues(RoleProfileViewModel _roleProfileViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileViewModel.EntryDateTime = DateTime.Now;
            _roleProfileViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _roleProfileViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileViewModel.EntryStatus = entryType;
            _roleProfileViewModel.UserAction = entryType;

            if (_entryType == StringLiteralValue.Amend)
            {
                // Set ReferenceKey As PrmKey To Every Object
                _roleProfileViewModel.PrmKey = _roleProfileViewModel.RoleProfilePrmKey;
                _roleProfileViewModel.RoleProfileModificationPrmKey = _roleProfileViewModel.RoleProfileModificationPrmKey;
                _roleProfileViewModel.RoleProfileTranslationPrmKey = _roleProfileViewModel.RoleProfileTranslationPrmKey;
            }

            // RoleProfileGeneralLedgerViewModel
            if (_roleProfileViewModel.IsAllowAllAccessForGeneralLedger != true)
            {
                _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.ActivationStatus = StringLiteralValue.Active;
                _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.EntryDateTime = DateTime.Now;
                _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.EntryStatus = entryType;
                _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.UserAction = entryType;
            }

            // RoleProfileBusinessOfficeViewModel
            if (_roleProfileViewModel.IsAllowAllAccessForBusinessOffice != true)
            {
                _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
                _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
                _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.EntryStatus = entryType;
                _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.UserAction = entryType;
            }

            // RoleProfileTransactionLimitViewModel
            if (_roleProfileViewModel.IsAllowAllTransactions != true)
            {
                _roleProfileViewModel.RoleProfileTransactionLimitViewModel.ActivationStatus = StringLiteralValue.Active;
                _roleProfileViewModel.RoleProfileTransactionLimitViewModel.EntryDateTime = DateTime.Now;
                _roleProfileViewModel.RoleProfileTransactionLimitViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _roleProfileViewModel.RoleProfileTransactionLimitViewModel.EntryStatus = entryType;
                _roleProfileViewModel.RoleProfileTransactionLimitViewModel.UserAction = entryType;
            }

            // RoleProfileMenuViewModel
            if (_roleProfileViewModel.IsAllowAllAccessForMenu != true)
            {
                _roleProfileViewModel.RoleProfileMenuViewModel.ActivationStatus = StringLiteralValue.Active;
                _roleProfileViewModel.RoleProfileMenuViewModel.EntryDateTime = DateTime.Now;
                _roleProfileViewModel.RoleProfileMenuViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _roleProfileViewModel.RoleProfileMenuViewModel.EntryStatus = entryType;
                _roleProfileViewModel.RoleProfileMenuViewModel.UserAction = entryType;
            }

            // RoleProfileSpecialPermissionViewModel
            if (_roleProfileViewModel.IsAllowAllAccessForSpecialPermission != true)
            {
                _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.ActivationStatus = StringLiteralValue.Active;
                _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.EntryDateTime = DateTime.Now;
                _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.EntryStatus = entryType;
                _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.UserAction = entryType;
            }

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileViewModel.ReasonForModification = "None";
                _roleProfileViewModel.TransReasonForModification = "None";
                if (_roleProfileViewModel.IsAllowAllAccessForGeneralLedger != true)
                {
                    _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.ReasonForModification = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForBusinessOffice != true)
                {
                    _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.ReasonForModification = "None";
                }
                if (_roleProfileViewModel.IsAllowAllTransactions != true)
                {
                    _roleProfileViewModel.RoleProfileTransactionLimitViewModel.ReasonForModification = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForMenu != true)
                {
                    _roleProfileViewModel.RoleProfileMenuViewModel.ReasonForModification = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForSpecialPermission != true)
                {
                    _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.ReasonForModification = "None";
                }
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileViewModel.Remark = "None";
                if (_roleProfileViewModel.IsAllowAllAccessForGeneralLedger != true)
                {
                    _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.Remark = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForBusinessOffice != true)
                {
                    _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.Remark = "None";
                }
                if (_roleProfileViewModel.IsAllowAllTransactions != true)
                {
                    _roleProfileViewModel.RoleProfileTransactionLimitViewModel.Remark = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForMenu != true)
                {
                    _roleProfileViewModel.RoleProfileMenuViewModel.Remark = "None";
                }
                if (_roleProfileViewModel.IsAllowAllAccessForSpecialPermission != true)
                {
                    _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.Remark = "None";
                }
            }
            else
            {
                if (_roleProfileViewModel.IsAllowAllAccessForGeneralLedger != true)
                {
                    _roleProfileViewModel.RoleProfileGeneralLedgerViewModel.Remark = _roleProfileViewModel.Remark;
                }
                if (_roleProfileViewModel.IsAllowAllAccessForBusinessOffice != true)
                {
                    _roleProfileViewModel.RoleProfileBusinessOfficeViewModel.Remark = _roleProfileViewModel.Remark;
                }
                if (_roleProfileViewModel.IsAllowAllTransactions != true)
                {
                    _roleProfileViewModel.RoleProfileTransactionLimitViewModel.Remark = _roleProfileViewModel.Remark;
                }
                if (_roleProfileViewModel.IsAllowAllAccessForMenu != true)
                {
                    _roleProfileViewModel.RoleProfileMenuViewModel.Remark = _roleProfileViewModel.Remark;
                }
                if (_roleProfileViewModel.IsAllowAllAccessForSpecialPermission != true)
                {
                    _roleProfileViewModel.RoleProfileSpecialPermissionViewModel.Remark = _roleProfileViewModel.Remark;
                }
            }
        }

        public void GetRoleProfileDefaultValues(RoleProfileViewModel _roleProfileViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileViewModel.EntryDateTime = DateTime.Now;
            _roleProfileViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _roleProfileViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileViewModel.EntryStatus = entryType;
            _roleProfileViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileViewModel.ReasonForModification = "None";
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileViewModel.Remark = "None";
            }
        }

        public void GetRoleProfileGeneralLedgerDefaultValues(RoleProfileGeneralLedgerViewModel _roleProfileGeneralLedgerViewModel, bool _isModify, string _entryType)
        {
            string entryType;

            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileGeneralLedgerViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileGeneralLedgerViewModel.EntryDateTime = DateTime.Now;
            _roleProfileGeneralLedgerViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileGeneralLedgerViewModel.EntryStatus = entryType;
            _roleProfileGeneralLedgerViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileGeneralLedgerViewModel.ReasonForModification = "None";
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileGeneralLedgerViewModel.Remark = "None";
            }
        }

        public void GetRoleProfileBusinessOfficeDefaultValues(RoleProfileBusinessOfficeViewModel _roleProfileBusinessOfficeViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileBusinessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileBusinessOfficeViewModel.EntryDateTime = DateTime.Now;
            _roleProfileBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileBusinessOfficeViewModel.EntryStatus = entryType;
            _roleProfileBusinessOfficeViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileBusinessOfficeViewModel.ReasonForModification = "None";
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileBusinessOfficeViewModel.Remark = "None";
            }
        }

        public void GetRoleProfileTransactionLimitDefaultValues(RoleProfileTransactionLimitViewModel _roleProfileTransactionLimitViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileTransactionLimitViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileTransactionLimitViewModel.EntryDateTime = DateTime.Now;
            _roleProfileTransactionLimitViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileTransactionLimitViewModel.EntryStatus = entryType;
            _roleProfileTransactionLimitViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileTransactionLimitViewModel.ReasonForModification = "None";
                _roleProfileTransactionLimitViewModel.CloseDate = null;
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileTransactionLimitViewModel.Remark = "None";
            }
        }

        public void GetRoleProfileMenuDefaultValues(RoleProfileMenuViewModel _roleProfileMenuViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileMenuViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileMenuViewModel.EntryDateTime = DateTime.Now;
            _roleProfileMenuViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileMenuViewModel.EntryStatus = entryType;
            _roleProfileMenuViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileMenuViewModel.ReasonForModification = "None";
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileMenuViewModel.Remark = "None";
            }
        }

        public void GetRoleProfileSpecialPermissionDefaultValues(RoleProfileSpecialPermissionViewModel _roleProfileSpecialPermissionViewModel, bool _isModify, string _entryType)
        {
            string entryType;
            if (_entryType == StringLiteralValue.Modify && _isModify == false)
            {
                entryType = StringLiteralValue.Create;
            }
            else
            {
                entryType = _entryType;
            }

            _roleProfileSpecialPermissionViewModel.ActivationStatus = StringLiteralValue.Active;
            _roleProfileSpecialPermissionViewModel.EntryDateTime = DateTime.Now;
            _roleProfileSpecialPermissionViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _roleProfileSpecialPermissionViewModel.EntryStatus = entryType;
            _roleProfileSpecialPermissionViewModel.UserAction = entryType;

            if (_entryType != StringLiteralValue.Modify)
            {
                _roleProfileSpecialPermissionViewModel.ReasonForModification = "None";
                _roleProfileSpecialPermissionViewModel.CloseDate = null;
            }

            if ((_entryType == StringLiteralValue.Create) || (_entryType == StringLiteralValue.Modify))
            {
                _roleProfileSpecialPermissionViewModel.Remark = "None";
            }
        }
    }
}
