using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileMenuRepository
    {
        // Return Rejected RoleProfileMenu Entries
        Task<IEnumerable<RoleProfileMenuViewModel>> GetRejectedEntries(short _roleProfilePrmKey);

        // Return UnVerified RoleProfileMenu Entries
        Task<IEnumerable<RoleProfileMenuViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey);

        // Return Verified RoleProfileMenu Entries
        Task<IEnumerable<RoleProfileMenuViewModel>> GetVerifiedEntries(short _roleProfilePrmKey);

    }
}
