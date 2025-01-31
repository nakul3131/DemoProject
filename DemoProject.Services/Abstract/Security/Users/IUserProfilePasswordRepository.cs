using System;
using System.Collections.Generic;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfilePasswordRepository
    {
        IEnumerable<UserProfilePassword> UserProfilePasswords { get; }

        void SaveUserProfilePassword(ResetPasswordViaTokenViewModel _resetPasswordViaTokenViewModel);

        //
        // Summary:
        //     Determines whether the User Credentials is valid For authentication.

        // Parameters:
        //   _userName: The Name of User Profile for which to check.

        //   _password: The password of the user for which to check.

        // Returns:
        //     true, if the combination of _userName and  _password are valid; otherwise, false
        bool IsValidUserCredentialsForAuthentication(string _userName, string _password);

        //
        // Summary:
        //     Determines whether the password is valid.

        // Parameters:
        //   _userProfileName: The Name of User Profile for which to check.

        //   _password: The password of the user for which to check.

        // Returns:
        //     true, if the combination of _userProfileName and  _password are valid; otherwise, false
        bool IsValidUserPassword(string _userName, string _password);

        //
        // Summary:
        //     Determines whether the password is valid.

        // Parameters:
        //   _userProfilePrmKey: The PrmKey of User Profile for which to check.

        //   _password: The password of the user for which to check.

        // Returns:
        //     true, if the combination of _userProfilePrmKey and  _password are valid; otherwise, false
        bool IsValidUserPassword(short _userProfilePrmKey, string _password);

        //
        // Summary:
        //     Get percentage of similarity Between inputed password and orginal password n percentage. 

        // Parameters:
        //   _userProfilePrmKey: PrmKey of the authenticated user profile.

        //
        // Returns:
        //     Percentage of similarity Between inputed password and orginal password n percentage.  
        short GetPasswordMatchRatio(short _userProfilePrmKey, string _inputedPassword);

        DateTime? GetPasswordExpiryDate(short _userProfilePrmKey);
    }
}
