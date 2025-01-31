using DemoProject.Services.ViewModel.Security.UserRoles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileDetailRepository
    {
        Task<IEnumerable<RoleProfileGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _roleProfilePrmKey, string _entryType);

        Task<IEnumerable<RoleProfileBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _roleProfilePrmKey, string _entryType);

        Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetTransactionLimitEntries(short _roleProfilePrmKey, string _entryType);

        Task<IEnumerable<RoleProfileMenuViewModel>> GetMenuEntries(short _roleProfilePrmKey, string _entryType);

        Task<IEnumerable<RoleProfileSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _roleProfilePrmKey, string _entryType);

        void GetRoleProfileAllDefaultValues(RoleProfileViewModel _roleProfileViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileDefaultValues(RoleProfileViewModel _roleProfileViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileGeneralLedgerDefaultValues(RoleProfileGeneralLedgerViewModel _roleProfileGeneralLedgerViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileBusinessOfficeDefaultValues(RoleProfileBusinessOfficeViewModel _roleProfileBusinessOfficeViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileTransactionLimitDefaultValues(RoleProfileTransactionLimitViewModel _roleProfileTransactionLimitViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileMenuDefaultValues(RoleProfileMenuViewModel _roleProfileMenuViewModel, bool _isModify, string _entryStatus);

        void GetRoleProfileSpecialPermissionDefaultValues(RoleProfileSpecialPermissionViewModel _roleProfileSpecialPermissionViewModel, bool _isModify, string _entryStatus);
    }
}
