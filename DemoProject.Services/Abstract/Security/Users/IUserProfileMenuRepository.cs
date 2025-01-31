using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    //
    // Summary:
    //          This interface defines to keep degree of separation between the data model entities and the storage and retrieval logic,
    //          which we achieve using the repository pattern.
    //          A class that depends on the IMenu interface can obtain Menu object without needing to know anything about 
    //          where they are coming from or how the implementation class will deliver them.
    //          This is essence of Repository Pattern.
    public interface IUserProfileMenuRepository
    {
        // Return Rejected General Ledger Business Office Entry
        Task<IEnumerable<UserProfileMenuViewModel>> GetRejectedUserProfileMenuEntries(short _userProfilePrmKey);

        // Return Unverified Entries of Business Office
        Task<IEnumerable<UserProfileMenuViewModel>> GetUnverifiedUserProfileMenuEntries(short _userProfilePrmKey);

        // Return Valid List From General Ledger Business Office Table For Modification
        Task<IEnumerable<UserProfileMenuViewModel>> GetVerifiedUserProfileMenuEntries(short _userProfilePrmKey);

        //
        // Summary:
        //          This interface uses IEnumerable<T> to allow a caller to obtain a sequence of Menu objects, without saying how
        //          or where the data is stored or retrieved.
        IEnumerable<UserProfileMenu> UserProfileMenus { get; }

        //
        // Summary:
        //     Get Authorized Menus with details for given user

        // Parameters:
        //   _userProfilePrmKey: PrmKey of the authenticated user profile.

        //
        // Returns:
        //     Menu List of given user.  
        List<MenuViewModel> GetUserMenus(short _userProfilePrmKey);

        //
        // Summary:
        //     Return Wheter user is authorized to access the action method.
        // Parameter:
        //      _userId - The User PrmKey
        //      _actionName - The Name Of Action Method          
        //
        // Returns:
        //     True if user is authorized to access the action method, otherwise false.
        bool HasUserPermission(string _actionName, string _controllerName);

        //
        // Summary:
        //     Return Wheter user is authorized to redirected same page.
        // Parameter:
        //      _userId - The User PrmKey
        //      _actionName - The Name Of Action Method          
        //
        // Returns:
        //     True if user is authorized to access the action method, otherwise false.
        bool IsRedirectedToSamePage(int _menuPrmKey);
    }
}
