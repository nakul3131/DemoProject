using DemoProject.Services.ViewModel.Security.Users;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfilePasswordPolicyRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<UserProfilePasswordPolicyViewModel> GetRejectedUserProfilePasswordPolicyEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<UserProfilePasswordPolicyViewModel> GetUnverifiedUserProfilePasswordPolicyEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<UserProfilePasswordPolicyViewModel> GetVerifiedUserProfilePasswordPolicyEntries(short _userProfilePrmKey);
    }
}
