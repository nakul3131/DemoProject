using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileTransactionLimitRepository
    {
        
       
        // Return Rejected UserProfileTransactionLimit Entries
        Task<IEnumerable<UserProfileTransactionLimitViewModel>> GetRejectedUserProfileTransactionLimitEntries(short _userProfilePrmKey);

        // Return UnVerified UserProfileTransactionLimit Entries
        Task<IEnumerable<UserProfileTransactionLimitViewModel>> GetUnverifiedUserProfileTransactionLimitEntries(short _userProfilePrmKey);

        // Return Verified UserProfileTransactionLimit Entries
        Task<IEnumerable<UserProfileTransactionLimitViewModel>> GetVerifiedUserProfileTransactionLimitEntries(short _userProfilePrmKey);

    }
}
