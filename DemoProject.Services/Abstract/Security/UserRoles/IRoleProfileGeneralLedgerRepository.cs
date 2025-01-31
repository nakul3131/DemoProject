using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileGeneralLedgerRepository
    {
        // Return Rejected RoleProfileGeneralLedger Entries
        Task<IEnumerable<RoleProfileGeneralLedgerViewModel>> GetRejectedEntries(short _roleProfilePrmKey);

        // Return UnVerified RoleProfileGeneralLedger Entries
        Task<IEnumerable<RoleProfileGeneralLedgerViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey);

        // Return Verified RoleProfileGeneralLedger Entries
        Task<IEnumerable<RoleProfileGeneralLedgerViewModel>> GetVerifiedEntries(short _roleProfilePrmKey);

    }
}
