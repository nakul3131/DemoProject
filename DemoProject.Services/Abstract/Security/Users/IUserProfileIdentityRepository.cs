using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileIdentityRepository
    {
        // Return GetRejectedEntries Entry
        Task<UserProfileIdentityViewModel> GetRejectedEntries(short _userProfilePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<UserProfileIdentityViewModel> GetUnVerifiedEntries(short _userProfilePrmKey);

        // Return GetVerifiedEntries Entry
        Task<UserProfileIdentityViewModel> GetVerifiedEntries(short _userProfilePrmKey);
    }
}
