using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfilePasswordRepository : IUserProfilePasswordRepository
    {
        private readonly EFDbContext context;

        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IUserProfileHomeBranchRepository userProfileHomeBranchRepository;

        public EFUserProfilePasswordRepository(RepositoryConnection _connection, ISecurityDetailRepository _securityDetailRepository, IUserProfileHomeBranchRepository _userProfileHomeBranchRepository)
        {
            context = _connection.EFDbContext;
            securityDetailRepository = _securityDetailRepository;
            userProfileHomeBranchRepository = _userProfileHomeBranchRepository;
        }

        public IEnumerable<UserProfilePassword> UserProfilePasswords => throw new NotImplementedException();

        public void SaveUserProfilePassword(ResetPasswordViaTokenViewModel _resetPasswordViaTokenViewModel)
        {
            //context.Database.ExecuteSqlCommand("EXECUTE Usp_AddUserProfilePassword @p0, @p1, @p2, @p3, @p4", new SqlParameter("@p0", (short)HttpContext.Current.Session["TmpUserProfilePrmKey"]), new SqlParameter("@p1", _resetPasswordViaTokenViewModel.), new SqlParameter("@p2", _customiseInvalidLoginLog.InputedPassword), new SqlParameter("@p3", _customiseInvalidLoginLog.UserProfilePrmKey), new SqlParameter("@p4", _customiseInvalidLoginLog.ClientMachineName), new SqlParameter("@p5", _customiseInvalidLoginLog.ClientBrowser), new SqlParameter("@p6", _customiseInvalidLoginLog.ClientIPAddress), new SqlParameter("@p7", _customiseInvalidLoginLog.ClientLocation), new SqlParameter("@p8", _customiseInvalidLoginLog.ClientApp), new SqlParameter("@p9", _customiseInvalidLoginLog.ClientOperatingSystem), new SqlParameter("@p10", _customiseInvalidLoginLog.MatchingRatioOfInputedPassword));
        }

        public bool IsValidUserCredentialsForAuthentication(string _userName, string _password)
        {
            short userProfilePrmKey = securityDetailRepository.GetUserProfilePrmKeyByName(_userName);

            return context.Database.SqlQuery<bool>("SELECT dbo.IsValidUserCredentialsForAuthentication(@UserProfilePrmKey, @Password)", new SqlParameter("@UserProfilePrmKey", userProfilePrmKey), new SqlParameter("@Password", _password)).FirstOrDefault();
        }

        public bool IsValidUserPassword(string _userName, string _password)
        {
            short userProfilePrmKey = securityDetailRepository.GetUserProfilePrmKeyByName(_userName);

            return context.Database.SqlQuery<bool>("SELECT dbo.IsValidUserPassword (@UserProfilePrmKey, @Password)", new SqlParameter("@UserProfilePrmKey", userProfilePrmKey), new SqlParameter("@Password", _password)).FirstOrDefault();
        }

        public bool IsValidUserPassword(short _userProfilePrmKey, string _password)
        {
            return context.Database.SqlQuery<bool>("SELECT dbo.IsValidUserPassword (@UserProfilePrmKey, @Password)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("@Password", _password)).FirstOrDefault();
        }

        public short GetPasswordMatchRatio(short _userProfilePrmKey, string _inputedPassword)
        {
            return context.Database.SqlQuery<short>("SELECT dbo.GetStringMatchingRatio (@UserProfilePrmKey, @Password)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("@Password", _inputedPassword)).FirstOrDefault();
        }

        public DateTime? GetPasswordExpiryDate(short _userProfilePrmKey)
        {
            return context.UserProfilePasswords
                        .Where(u => u.UserProfilePrmKey == _userProfilePrmKey & u.ActivationStatus == "ACT")
                        .OrderByDescending(u => u.PrmKey)
                        .Select(u => u.ExpiryDate)
                        .FirstOrDefault();
        }
    }
}
