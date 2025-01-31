using DemoProject.Services.ViewModel.Security.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileGeneralLedgerRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileGeneralLedgerViewModel>> GetRejectedUserProfileGeneralLedgerEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileGeneralLedgerViewModel>> GetUnverifiedUserProfileGeneralLedgerEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileGeneralLedgerViewModel>> GetVerifiedUserProfileGeneralLedgerEntries(short _userProfilePrmKey);
    }
}
