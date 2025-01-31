using DemoProject.Domain.Entities.Security.Users;
using System.Collections.Generic;

namespace DemoProject.Services.Abstract.Security.Users
{
    //
    // Summary:
    //          This interface defines to keep degree of separation between the data model entities and the storage and retrieval logic,
    //          which we achieve using the repository pattern.
    //          A class that depends on the IUserAuthenticationTokenRepository interface can obtain UserAuthenticationTokenRepository object without needing to know anything about 
    //          where they are coming from or how the implementation class will deliver them.
    //          This is essence of Repository Pattern.
    public interface IUserAuthenticationTokenRepository
    {
        //
        // Summary:
        //          This interface uses IEnumerable<T> to allow a caller to obtain a sequence of UserAuthenticationTokenRepository objects, without saying how
        //          or where the data is stored or retrieved.
        IEnumerable<UserAuthenticationToken> UserAuthenticationTokens { get; }

        // 
        // To create UserAuthenticationToken entry in database.
        // Return Sms Delivery Status
        string CreateUserAuthenticationToken(short _userProfilePrmKey, byte _tokenFor);

        // Summary:
        //     To Get Last Token PrmKey of specified user.
        //
        // Parameters:
        //   _userProfilePrmKey:
        //                      The PrmKey of UserProfile.
        // Return:
        //          Last Saved UserAuthenticationPrmKey of Given User.
        //
        int GetRecentUserAuthenticationTokenPrmKey(short _userProfilePrmKey);

        //
        // Summary:
        //     Validates a userProfilePrmKey, mobileOTP and emailVCode against tokens stored in database

        // Parameters:
        //   _userProfilePrmKey: The authenticated user profile prmkey.

        // Returns:
        //     true if the all parameter combination valid; otherwise, false.
        bool IsTokenAuthenticate(short _userProfilePrmKey, string _mobileOTP, string _emailVCode, byte _tokenFor);
    }
}
