using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileDetailRepository
    {

        Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetBusinessOfficeEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileCurrencyViewModel>> GetCurrencyEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileGeneralLedgerViewModel>> GetGeneralLedgerEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileLoginDeviceViewModel>> GetLoginDeviceEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileMenuViewModel>> GetMenuEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserProfileTransactionLimitViewModel>> GetTransactionLimitEntries(short _userProfilePrmKey, string _entryType);

        Task<IEnumerable<UserRoleProfileViewModel>> GetUserRoleProfileEntries(short _userProfilePrmKey, string _entryType);

        Task<UserProfileHomeBusinessOfficeViewModel> GetHomeBusinessOfficeEntries(short _userProfilePrmKey, string _entryType);

        Task<UserProfileGroupViewModel> GetGroupEntries(short _userProfilePrmKey, string _entryType);

        Task<UserProfilePasswordPolicyViewModel> GetPasswordPolicyEntries(short _userProfilePrmKey, string _entryType);
        
        void GetUserProfileDefaultValues(UserProfileViewModel _userProfileViewModel, string _entryStatus);  

        void GetUserProfileBusinessOfficeDefaultValues(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string _entryStatus);

        void GetUserProfileCurrencyDefaultValues(UserProfileCurrencyViewModel _userProfileCurrencyViewModel, string _entryStatus); 

        void GetUserProfileGeneralLedgerDefaultValues(UserProfileGeneralLedgerViewModel _userProfileGeneralLedgerViewModel, string _entryStatus); 

        void GetUserProfileLoginDeviceDefaultValues(UserProfileLoginDeviceViewModel _userProfileLoginDeviceViewModel, string _entryStatus);

        void GetUserProfileMenuDefaultValues(UserProfileMenuViewModel _userProfileMenuViewModel, string _entryStatus);

        void GetUserProfileSpecialPermissionDefaultValues(UserProfileSpecialPermissionViewModel _userProfileSpecialPermissionViewModel, string _entryStatus);

        void GetUserProfileTransactionLimitDefaultValues(UserProfileTransactionLimitViewModel _userProfileTransactionLimitViewModel, string _entryStatus);
         
        void GetUserRoleProfileDefaultValues(UserRoleProfileViewModel _userRoleProfileViewModel, string _entryStatus);

        void GetUserProfileHomeBusinessOfficeDefaultValues(UserProfileHomeBusinessOfficeViewModel _userProfileHomeBusinessOfficeViewModel, string _entryStatus);

        void GetUserProfilePasswordPolicyDefaultValues(UserProfilePasswordPolicyViewModel _userProfilePasswordPolicyViewModel, string _entryStatus);
    }
}
