using DemoProject.Domain.Entities.Security.Users;
using System.Collections.Generic;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileHomeBranchRepository
    {
        IEnumerable<UserProfileHomeBranch> UserProfileHomeBranches { get; }

        //Used when required prmkey
        short GetHomeBranchPrmKeyOfUser(short _userProfilePrmKey);

        short GetUserHomeBranchPrmKey(short _userProfilePrmKey);

    }
}
