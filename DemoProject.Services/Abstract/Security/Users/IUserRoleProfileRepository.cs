using DemoProject.Services.ViewModel.Security.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserRoleProfileRepository
    {
        short GetPrmKeyById(Guid _userRoleProfileId);

        // Return Rejected UserRoleProfile Entries
        Task<IEnumerable<UserRoleProfileViewModel>> GetRejectedUserRoleProfileEntry(short _userProfilePrmKey);
        Task<IEnumerable<UserRoleProfileIndexViewModel>> GetRejectedUserRoleProfileEntries(string _entryType);

        // Return UnVerified UserRoleProfile Entries
        Task<IEnumerable<UserRoleProfileViewModel>> GetUnverifiedUserRoleProfileEntry(short _userProfilePrmKey);

        // Return Verified UserRoleProfile Entries
        Task<IEnumerable<UserRoleProfileViewModel>> GetVerifiedUserRoleProfileEntries(short _userProfilePrmKey);
        Task<bool> Modify(UserRoleProfileViewModel _userRoleProfileViewModel);
        Task<IEnumerable<UserRoleProfileIndexViewModel>> GetUserRoleProfileUnverifiedIndex(string _entryType);
        Task<bool> Reject(UserRoleProfileViewModel _userRoleProfileViewModel);
         Task<bool> Verify(UserRoleProfileViewModel _userRoleProfileViewModel);
        Task<bool> Amend(UserRoleProfileViewModel _userRoleProfileViewModel);
        Task<bool> Delete(UserRoleProfileViewModel _userRoleProfileViewModel);
    }
}
