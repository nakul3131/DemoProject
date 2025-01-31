using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileDbContextRepository
    {
        bool AttachUserProfileData(UserProfileViewModel _userProfileViewModel, string _entryType);

        bool AttachUserProfileModificationData(UserProfileViewModel _userProfileViewModel, string _entryType);

        bool AttachUserProfileAccessibilityData(UserProfileViewModel _userProfileViewModel, string _entryType);

        bool AttachUserProfileIdentityData(UserProfileViewModel _userProfileViewModel, string _entryType);

        bool AttachUserProfileHomeBusinessOfficeData(UserProfileHomeBusinessOfficeViewModel _userProfileHomeBusinessOfficeViewModel, string _entryType);

        bool AttachUserProfileBusinessOfficeData(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string _entryType);

        bool AttachUserProfileCurrencyData(UserProfileCurrencyViewModel _userProfileCurrencyViewModel, string _entryType);

        bool AttachUserProfileGeneralLedgerData(UserProfileGeneralLedgerViewModel _userProfileGeneralLedgerViewModel, string _entryType);

        bool AttachUserProfileMenuData(UserProfileMenuViewModel _userProfileMenuViewModel, string _entryType);

        bool AttachUserProfilePasswordPolicyData(UserProfilePasswordPolicyViewModel _userProfilePasswordPolicyViewModel, string _entryType);

        bool AttachUserProfileSpecialPermissionData(UserProfileSpecialPermissionViewModel _userProfileSpecialPermissionViewModel, string _entryType);

        bool AttachUserProfileTransactionLimitData(UserProfileTransactionLimitViewModel _userProfileTransactionLimitViewModel, string _entryType);

        bool AttachUserRoleProfileData(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryType);

        bool AttachUserHomeBranchRoleProfileData(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryType);

        Task<bool> SaveData();
    }
}
