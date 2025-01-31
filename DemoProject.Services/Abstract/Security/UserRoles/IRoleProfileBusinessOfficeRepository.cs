using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileBusinessOfficeRepository
    {
        // Return Rejected RoleProfileBusinessOffice Entries
        Task<IEnumerable<RoleProfileBusinessOfficeViewModel>> GetRejectedEntries(short _roleProfilePrmKey);

        // Return UnVerified RoleProfileBusinessOffice Entries
        Task<IEnumerable<RoleProfileBusinessOfficeViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey);

        // Return Verified RoleProfileBusinessOffice Entries
        Task<IEnumerable<RoleProfileBusinessOfficeViewModel>> GetVerifiedEntries(short _roleProfilePrmKey);

    }
}
