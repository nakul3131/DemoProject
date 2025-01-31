using DemoProject.Services.ViewModel.Security.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileLoginDeviceRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetRejectedUserProfileLoginDeviceEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetUnverifiedUserProfileLoginDeviceEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetVerifiedUserProfileLoginDeviceEntries(short _userProfilePrmKey);
    }
}
