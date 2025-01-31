using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileHomeBusinessOfficeRepository
    {
        // Return GetRejectedEntries Entry
        Task<UserProfileHomeBusinessOfficeViewModel> GetRejectedEntries(short _userProfilePrmKey);

        // Return GetUnVerifiedEntries Entry
        Task<UserProfileHomeBusinessOfficeViewModel> GetUnVerifiedEntries(short _userProfilePrmKey);

        // Return GetVerifiedEntries Entry
        Task<UserProfileHomeBusinessOfficeViewModel> GetVerifiedEntries(short _userProfilePrmKey);
    }
}
