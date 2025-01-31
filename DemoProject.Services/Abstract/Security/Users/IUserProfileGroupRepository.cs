using DemoProject.Services.ViewModel.Security.Users;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileGroupRepository
    {
        // Return GetRejectedEntries Entry
        Task<UserProfileGroupViewModel> GetRejectedEntries(short _userProfilePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<UserProfileGroupViewModel> GetUnVerifiedEntries(short _userProfilePrmKey);

        // Return GetVerifiedEntries Entry
        Task<UserProfileGroupViewModel> GetVerifiedEntries(short _userProfilePrmKey);
    }
}
