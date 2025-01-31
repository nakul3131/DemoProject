using System;

namespace DemoProject.Services.Abstract.Security
{
    //
    // Summarry
    // This Interface decouple the controller from the authentication methods.
    // Manages authentication services for Web applications.
    public interface IAuthProviderRepository
    {

        //
        // Summary:
        // Determines whether the user name and password are valid against credentials stored in database.

        // Parameters:
        //   _userName: The name of the user for which to check

        //   _password: The password of the user for which to check.

        //Profile Image
        string IsAuthenticateImage(short _userProfilePrmKey);

        // Returns:
        //     true if the user name and password are valid; otherwise, false.
        bool IsAuthenticate(string _userName, string _password);

        //
        // Summary:
        //     Validates a userProfilePrmKey, mobileOTP and emailVCode against tokens stored in database

        // Parameters:
        //   _userProfilePrmKey: The authenticated user profile prmkey.

        // Returns:
        //     true if the all parameter combination valid; otherwise, false.
        bool IsTokenAuthenticate(short _userProfilePrmKey, string _mobileOTP, string _emailVCode, byte _tokenFor);


        // Creates an authentication ticket for the supplied user name and adds it to the
        //     cookies collection of the response and 
        //     false to create a persistent cookie (that is not saved across browser sessions)
        void CreateAuthenticationTicket(string _userName);

        //
        // Summary:
        //     Determines whether the password is valid.

        // Parameters:
        //   _userProfileName: The Name of User Profile for which to check.

        //   _password: The password of the user for which to check.

        // Returns:
        //     true, if the combination of _userProfileName and  _password are valid; otherwise, false
        bool IsValidUserPassword(short _userProfilePrmKey, string _password);

        //
        // Summary:
        //     Indicates whether Token Based Authentication is enabled of given user.

        // Parameters:
        //   _userProfilePrmKey: The authenticated user profile prmkey.

        // Returns:
        //     true if the Token Based Authentication is enabled; otherwise, false.
        bool IsTokenBasedAuthenticationEnabledForUser(short _userProfilePrmKey);

        //
        // Summary:
        //     Indicates whether User Has Any Login Issue.

        // Parameters:
        //   _userProfilePrmKey: The authenticated user profile prmkey.

        // Returns:
        //     true if user has any issue; otherwise, false.
        bool IsUserHasTrouble(string _userName, string _password);

        //
        // Summary:
        //     Update suspicious activity count.

        // Parameters:
        //   _isResetToZero: The Count reset to zero.            

        // Update:
        //   Increase suspicious count by one (1) if user name is not valid, 
        //   Reset count to zero (0), if user name is valid
        void UpdateSuspiciousActivityCount(bool _isResetToZero);

        //
        // Summary:
        //     Gets the PrmKey of User Profile based on Name Of User Profile. 
        // Parameters:
        //   _nameOfUserProfile: The name of the user profile for which to check.
        //
        // Returns:
        //     PrmKey of UserProfile specified by parameter. 
        //     returns 0, if not match
        short GetUserProfilePrmKey(string _nameOfUserProfile);

        //
        // Summary:
        //     Returns the User Profile Status of given user.  

        // Parameters:
        //   _userProfilePrmKey: The authenticated user profile prmkey.

        //
        // Returns:
        //     String which contain Staus of User Profile.     
        string GetUserProfileStatus(short _userProfilePrmKey);

        //
        // Returns:
        //     String which contain SessionTimeOut of User Profile.     
        byte GetUserProfileSessionTimeOut(short _userProfilePrmKey);

        //
        // Summary
        //      Returns Delivery Channels Of Token
        // Parameter
        //      User Profile PrmKey
        string GetDeliveryChannelsOfTokenForUser(short _userProfilePrmKey);

        //
        // Summary
        //      Returns Mobile Number Of User
        // Parameter
        //      User Profile PrmKey
        string GetRegisteredEmailIdOfUser(short _userProfilePrmKey);

        //
        // Summary
        //      Returns Mobile Number Of User
        // Parameter
        //      User Profile PrmKey
        string GetRegisteredMobileNumberOfUser(short _userProfilePrmKey);

        //
        // Summary:
        //     Get Error Message For Invalid Attempt

        // Parameters:
        //   _userProfilePrmKey: PrmKey of the authenticated user profile.

        //
        // Returns:
        //     Error Message for invalid attempt.   
        string GetInvalidAttemptErrorMessage(short _userProfilePrmKey);

        //
        // Summary:
        //     Get Error Message For Suspicious Activity

        // Parameters:
        //   _userProfilePrmKey: PrmKey of the authenticated user profile.

        //
        // Returns:
        //     Error Message for Suspicious Activity.  
        string GetSuspiciousActivityErrorMessage();

        //
        // Summary:
        //     Unlock User.
        void UnlockUser(short _userProfilePrmKey, string _method);

        //
        // Summary:
        //     Clear Recent Session.
        void ClearUserRecentSession(short _userProfilePrmKey, string _method);

        // 
        // Summary - Invalid Login Log 
        //        Insert Invalid Login Log in database. 
        //
        void InvalidLoginLog(string _input1, string _input2, short _userProfilePrmKey);

        // 
        // Summary - Login Log Method
        //        Insert Login Log in database. 
        //
        void LoginLog(short _userProfilePrmKey, bool _isMFAEnabled, byte mfaMethodPrmKey,string ip);

        // 
        // Summary - Account Recovery Log Method
        //        Insert Account Recovery Log in database. 
        //
        void SaveAccountRecoveryLog(short _userProfilePrmKey, byte _accountRecoveryFor, string _recoveryMethod);

        // 
        // Summary - Reset User Password
        void ResetUserPassword(short _userProfilePrmKey, string _userPassword);

        //
        // Summary:
        //     Removes the authentication status from the database.
        void LogOut();
    }
}
