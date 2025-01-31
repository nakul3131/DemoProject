using DemoProject.Services.ViewModel.Security.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileSpecialPermissionRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileSpecialPermissionViewModel>> GetRejectedUserProfileSpecialPermissionEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileSpecialPermissionViewModel>> GetUnverifiedUserProfileSpecialPermissionEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileSpecialPermissionViewModel>> GetVerifiedUserProfileSpecialPermissionEntries(short _userProfilePrmKey);
    }
}
