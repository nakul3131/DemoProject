using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileSpecialPermissionRepository
    {
        // Return Rejected RoleProfileSpecialPermission Entries
        Task<IEnumerable<RoleProfileSpecialPermissionViewModel>> GetRejectedEntries(short _roleProfilePrmKey);

        // Return UnVerified RoleProfileSpecialPermission Entries
        Task<IEnumerable<RoleProfileSpecialPermissionViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey);

        // Return Verified RoleProfileSpecialPermission Entries
        Task<IEnumerable<RoleProfileSpecialPermissionViewModel>> GetVerifiedEntries(short _roleProfilePrmKey);

    }
}
