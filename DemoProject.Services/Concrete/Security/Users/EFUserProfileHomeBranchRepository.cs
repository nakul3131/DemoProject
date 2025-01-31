using System.Linq;
using System.Collections.Generic;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Constants;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileHomeBranchRepository : IUserProfileHomeBranchRepository
    {
        private readonly EFDbContext context;

        public EFUserProfileHomeBranchRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public IEnumerable<UserProfileHomeBranch> UserProfileHomeBranches
        {
            get { return context.UserProfileHomeBranches; }

        }

        public short GetHomeBranchPrmKeyOfUser(short _userProfilePrmKey)
        {
            return context.UserProfileHomeBranches 
                          .Where(u => u.UserProfilePrmKey == _userProfilePrmKey & u.RowStatus == "A")
                          .Select(u => u.BranchPrmKey).FirstOrDefault();
        }

        public short GetUserHomeBranchPrmKey(short _userProfilePrmKey)
        {
            return context.UserProfileHomeBusinessOffices
                          .Where(u => u.UserProfilePrmKey == _userProfilePrmKey && u.EntryStatus == StringLiteralValue.Verify && u.ActivationStatus == StringLiteralValue.Active)
                          .Select(u => u.BusinessOfficePrmKey).FirstOrDefault();
        }
    }
}
