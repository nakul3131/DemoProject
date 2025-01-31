using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileTransactionLimitRepository
    {
        // Return Rejected RoleProfileTransactionLimit Entries
        Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetRejectedEntries(short _roleProfilePrmKey);

        // Return UnVerified RoleProfileTransactionLimit Entries
        Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetUnverifiedEntries(short _roleProfilePrmKey);

        // Return Verified RoleProfileTransactionLimit Entries
        Task<IEnumerable<RoleProfileTransactionLimitViewModel>> GetVerifiedEntries(short _roleProfilePrmKey);

        // Return User Past Day Permission For Transaction
        short GetPastDaysPermissionForTransaction(short _roleProfilePrmKey);

        // Return User Past Day Permission For Verify
        short GetPastDaysPermissionForVerification(short _roleProfilePrmKey);

        // Return User Past Day Permission For Auto Verify
        short GetPastDaysPermissionForAutoVerification(short _roleProfilePrmKey);

    }

}
