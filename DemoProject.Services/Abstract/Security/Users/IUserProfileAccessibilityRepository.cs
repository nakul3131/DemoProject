using DemoProject.Domain.Entities.Security.Users;
using System.Collections.Generic;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileAccessibilityRepository
    {
        IEnumerable<UserProfileAccessibility> GetUserProfileAccessibilities { get; }

        void SaveUserProfileAccessibility(UserProfileAccessibility _userProfileAccessibility);

        //
        // Summary
        //      Returns true if user enabled token based authentication other wise false
        // Parameter
        //      User Profile PrmKey
        bool IsUserAllowTokenBasedAuthentication(short _userProfilePrmKey);

        //
        // Summary
        //      Returns Delivery Channels Of Token
        // Parameter
        //      User Profile PrmKey
        string GetDeliveryChannelsOfTokenForUser(short _userProfilePrmKey);

        //
        // Returns:
        //     Numeric Value Which Contain SessionTimeOut of User Profile.     
        byte GetUserProfileSessionTimeOut(short _userProfilePrmKey);
    }
}
