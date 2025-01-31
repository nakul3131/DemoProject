using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Security.SystemEntity;

namespace DemoProject.Services.Abstract.Security
{
    public interface ISecurityDetailRepository
    {
        IEnumerable<AuthenticationMethod> AuthenticationMethods { get; }

        bool GetUserProfilePasswords(short _userProfilePrmKey, string _inputedPassword);

        bool IsUserLocked(short _userProfilePrmKey);

        bool IsUserOnline(short _userProfilePrmKey);

        byte GetAuthenticationMethodPrmKeyByName(string _methodName);

        byte GetChecksumAlgorithmPrmKeyById(Guid _checksumAlgorithmId);

        byte GetUserTypePrmKeyById(Guid _userTypeId);
        
        // Get Maximum Amount Limit For Transaction
        decimal GetMaximumAmountLimitForTransaction(short _userProfilePrmKey);

        // Get Minimum Amount Limit For Transaction
        decimal GetMinimumAmountLimitForTransaction(short _userProfilePrmKey);

        short GetLoginDevicePrmKeyById(Guid _loginDeviceId);

        short GetPasswordPolicyPrmKeyById(Guid _passwordPolicyId);

        // Return User Past Day Permission For Auto Verify
        short GetPastDaysPermissionForAutoVerification(short _userProfilePrmKey);

        // Return User Past Day Permission For Transaction
        short GetPastDaysPermissionForTransaction(short _userProfilePrmKey, DateTime _closingFinancialYearEndDate);

        short GetPastDaysPermissionForVerification(short _userProfilePrmKey);

        short GetRoleProfilePrmKeyById(Guid _roleProfileId);

        short GetSpecialPermissionPrmKeyById(Guid _specialPermissionId);

        short GetUserProfilePrmKeyByColumn(string _columnName, string _valueOfColumn);

        short GetUserProfilePrmKeyById(Guid _userProfileId);

        short GetUserProfilePrmKeyByName(string _nameOfUserProfile);

        string GetUserProfileFieldByPrmKey(string _columnName, short _prmKey);

        string GetUserProfileRegisteredEmailId(short _userProfilePrmKey);

        string GetUserProfileRegisteredMobileNumber(short _userProfilePrmKey);
        
        string GetUserProfileStatus(short _userProfilePrmKey);

       
        List<SelectListItem> ChecksumAlgorithmDropdownList { get; }

        List<SelectListItem> LoginDeviceDropDownList { get; }

        List<SelectListItem> PasswordPolicyDropDownList { get; }

        List<SelectListItem> RoleProfileDropDownList { get; }

        List<SelectListItem> SpecialPermissionDropdownList { get; }

        List<SelectListItem> UserProfileDropDownList { get; }

        List<SelectListItem> UserTypeDropdownList { get; }

    }
}
