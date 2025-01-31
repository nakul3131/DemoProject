using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileBusinessOfficeRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetRejectedUserProfileBusinessOfficeEntry(short _userProfilePrmKey, string _entryType);
        Task<IEnumerable<UserProfileBusinessOfficeIndexViewModel>> GetRejectedUserProfileBusinessOfficeEntries(string _entryType);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetUnverifiedUserProfileBusinessOfficeEntry(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileBusinessOfficeIndexViewModel>> GetUnverifiedUserProfileBusinessOfficeEntries(string _entryType);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetVerifiedUserProfileBusinessOfficeEntries(short _userProfilePrmKey);

        Task<bool> Modify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel);

        Task<bool> Reject(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel);

        Task<bool> Verify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel);

        Task<bool> Amend(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel);

        Task<bool> Delete(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel);
    }
}
