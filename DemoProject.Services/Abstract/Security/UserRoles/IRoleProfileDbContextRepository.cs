using DemoProject.Services.ViewModel.Security.UserRoles;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileDbContextRepository
    {
        bool AttachRoleProfileData(RoleProfileViewModel _roleProfileViewModel, string _entryType);

        bool AttachRoleProfileModificationData(RoleProfileViewModel _roleProfileViewModel, string _entryType);

        bool AttachRoleProfileGeneralLedgerData(RoleProfileGeneralLedgerViewModel _roleProfileGeneralLedgerViewModel, string _entryType);

        bool AttachRoleProfileBusinessOfficeData(RoleProfileBusinessOfficeViewModel _roleProfileBusinessOfficeViewModel, string _entryType);

        bool AttachRoleProfileTransactionLimitData(RoleProfileTransactionLimitViewModel _roleProfileTransactionLimitViewModel, string _entryType);

        bool AttachRoleProfileMenuData(RoleProfileMenuViewModel _roleProfileMenuViewModel, string _entryType);

        bool AttachRoleProfileSpecialPermissionData(RoleProfileSpecialPermissionViewModel _roleProfileSpecialPermissionViewModel, string _entryType);

        Task<bool> SaveData();

    }
}
