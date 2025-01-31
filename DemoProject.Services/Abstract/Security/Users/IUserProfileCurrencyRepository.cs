using DemoProject.Services.ViewModel.Security.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileCurrencyRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileCurrencyViewModel>> GetRejectedUserProfileCurrencyEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileCurrencyViewModel>> GetUnverifiedUserProfileCurrencyEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileCurrencyViewModel>> GetVerifiedUserProfileCurrencyEntries(short _userProfilePrmKey);
    }
}
