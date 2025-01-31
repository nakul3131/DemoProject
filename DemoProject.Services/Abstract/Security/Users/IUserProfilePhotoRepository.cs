using System.Collections.Generic;
using DemoProject.Domain.Entities.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    //
    // Summary:
    //          This interface defines to keep degree of separation between the data model entities and the storage and retrieval logic,
    //          which we achieve using the repository pattern.
    //          A class that depends on the IMenu interface can obtain Menu object without needing to know anything about 
    //          where they are coming from or how the implementation class will deliver them.
    //          This is essence of Repository Pattern.
    public interface IUserProfilePhotoRepository
    {
        //
        // Summary:
        //          This interface uses IEnumerable<T> to allow a caller to obtain a sequence of Menu objects, without saying how
        //          or where the data is stored or retrieved.
        IEnumerable<UserProfilePhoto> UserProfilePhotoes { get; }

        //
        // Summary:
        //     Get user profile photo for given user

        // Parameters:
        //   _userProfilePrmKey: PrmKey of the authenticated user profile.
        UserProfilePhoto GetUserPhoto(short _userProfilePrmKey);
    }
}
