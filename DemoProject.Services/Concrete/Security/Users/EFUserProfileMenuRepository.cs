using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    //
    // Summary:
    //          This is repository class. It implements the IUserProfileMenuRepository interface and uses an instance of EFDbContext
    //          to retrieve data from the database using the Entity Framework
    public class EFUserProfileMenuRepository : IUserProfileMenuRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileMenuRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<UserProfileMenuViewModel>> GetRejectedUserProfileMenuEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileMenuViewModel>("SELECT * FROM dbo.GetUserProfileMenuEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileMenuViewModel>> GetUnverifiedUserProfileMenuEntries(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserProfileMenuViewModel>("SELECT * FROM dbo.GetUserProfileMenuEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileMenuViewModel>> GetVerifiedUserProfileMenuEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileMenuViewModel>("SELECT * FROM dbo.GetUserProfileMenuEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public IEnumerable<UserProfileMenu> UserProfileMenus
        {
            get { return context.UserProfileMenus; }
        }

        public List<MenuViewModel> GetUserMenus(short _userProfilePrmKey)
        {
            List<MenuViewModel> customiseUserProfileMenu = new List<MenuViewModel>();

            customiseUserProfileMenu = context.Database.SqlQuery<MenuViewModel>("SELECT * FROM dbo.GetUserProfileMenus (@UserProfilePrmKey)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey)).ToList();

            return customiseUserProfileMenu;
        }

        public bool HasUserPermission(string _actionName, string _controllerName)
        {
            short userPrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            return context.Database.SqlQuery<bool>("SELECT dbo.HasUserAccess(@UserProfilePrmKey, @NameOfController, @NameOfAction)", new SqlParameter("@UserProfilePrmKey", userPrmKey), new SqlParameter("@NameOfController", _controllerName), new SqlParameter("@NameOfAction", _actionName)).FirstOrDefault();
        }

        public bool IsRedirectedToSamePage(int _menuPrmKey)
        {
            short userPrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

            return context.UserProfileMenus
                    .Where(m => m.UserProfilePrmKey == userPrmKey & m.MenuPrmKey == _menuPrmKey)
                    .Select(m => m.EnableSamePageRedirection).FirstOrDefault();
        }

    }
}
