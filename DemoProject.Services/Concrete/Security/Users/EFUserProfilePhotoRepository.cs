using System.Linq;
using System.Collections.Generic;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfilePhotoRepository : IUserProfilePhotoRepository 
    {
        private readonly EFDbContext context;

        public EFUserProfilePhotoRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public IEnumerable<UserProfilePhoto> UserProfilePhotoes
        {
            get
            {
                return context.UserProfilePhotoes;
            }
        }

        public UserProfilePhoto GetUserPhoto(short _userProfilePrmKey)
        {
            return context.UserProfilePhotoes
                        .Where(u => u.UserProfilePrmKey == _userProfilePrmKey)
                        .FirstOrDefault();           
        }
    }
}
