using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Wrapper;
using System.Data.Entity;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileAccessibilityRepository : IUserProfileAccessibilityRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileAccessibilityRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public IEnumerable<UserProfileAccessibility> GetUserProfileAccessibilities
        {
            get
            {
                return context.UserProfileAccessibilities;
            }
        }

        public void SaveUserProfileAccessibility(UserProfileAccessibility _userProfileAccessibility)
        {
            context.UserProfileAccessibilities.Attach(_userProfileAccessibility);
            context.Entry(_userProfileAccessibility).State = EntityState.Added;

            //context.UserProfileAccessibilities.Add(_userProfileAccessibility);
            context.SaveChanges();
        }

        public bool IsUserAllowTokenBasedAuthentication(short _userProfilePrmKey)
        {
            return context.Database.SqlQuery<bool>("SELECT dbo.IsTokenBasedAuthenticationEnabledForUser(@UserProfilePrmKey)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey)).FirstOrDefault();
        }

        public string GetDeliveryChannelsOfTokenForUser(short _userProfilePrmKey)
        {
            return context.UserProfileAccessibilities
                           .Where(ua => ua.UserProfilePrmKey == _userProfilePrmKey)
                           .Select(ua => ua.TokenDeliveryChannel).FirstOrDefault();
        }

        public byte GetUserProfileSessionTimeOut(short _userProfilePrmKey)
        {
            return context.UserProfileAccessibilities
                            .Where(a => a.UserProfilePrmKey == _userProfilePrmKey)
                            .Select(a => a.SessionTimeOut).FirstOrDefault();
        }

    }
}
