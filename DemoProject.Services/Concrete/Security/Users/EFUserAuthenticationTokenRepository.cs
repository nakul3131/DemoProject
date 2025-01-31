using System;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Utility.SmsSender;
using DemoProject.Services.Wrapper;
using System.Data.Entity;
using DemoProject.Services.Abstract.SMS;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserAuthenticationTokenRepository : IUserAuthenticationTokenRepository
    {
        //
        // Summary:
        //          This is repository class. It implements the IUserAuthenticationTokenRepository interface and uses an instance of EFDbContext
        //          to retrieve data from the database using the Entity Framework
        private readonly EFDbContext context;

        private readonly ISMSDetailRepository smsDetailRepository;

        public EFUserAuthenticationTokenRepository(RepositoryConnection _connection, ISMSDetailRepository _smsDetailRepository)
        {
            context = _connection.EFDbContext;
            smsDetailRepository = _smsDetailRepository;
        }

        private DateTime now = DateTime.Now;

        public IEnumerable<UserAuthenticationToken> UserAuthenticationTokens
        {
            get { return context.UserAuthenticationTokens; }
        }

        public string CreateUserAuthenticationToken(short _userProfilePrmKey, byte _tokenFor)
        {
            try
            {

                SmsSender sms = new SmsSender();

                DateTime now = DateTime.Now;

                UserAuthenticationToken userAuthenticationToken = new UserAuthenticationToken()
                {
                    UserProfilePrmKey = _userProfilePrmKey,
                    TokenFor = _tokenFor,
                    MobileOTP = Encoding.UTF8.GetBytes("1"),
                    EmailVCode = Encoding.UTF8.GetBytes("2"),
                    TokenStatus = "UNU",
                    TokenGeneratedOn = now,
                    TokenExpiredOn = now,
                    IsSmsGenerated = false,
                    IsEmailGenerated = false
                };

                context.UserAuthenticationTokens.Attach(userAuthenticationToken);
                context.Entry(userAuthenticationToken).State = EntityState.Added;

                //context.UserAuthenticationTokens.Add(userAuthenticationToken);
                context.SaveChanges();

                int userAuthenticationTokenPrmKey = userAuthenticationToken.PrmKey;
     
                // Send Token Sms and get response (i.e. sending status)
                SmsResponse smsResponse = sms.SendTokenSms(userAuthenticationTokenPrmKey);

                // Save Sms User Authentication Entry
                /*=*///smsDetailRepository.SaveSmsUserAuthenticationToken(userAuthenticationTokenPrmKey, smsResponse);

                return smsResponse.Result.ToString();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return error;
            }
        }

        public int GetRecentUserAuthenticationTokenPrmKey(short _userProfilePrmKey)
        {
            return context.Database.SqlQuery<int>("SELECT dbo.GetRecentUserAuthenticationTokenPrmKey (@UserProfilePrmKey)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey)).FirstOrDefault();
        }

        public bool IsTokenAuthenticate(short _userProfilePrmKey, string _mobileOTP, string _emailVCode, byte _tokenFor)
        {
            return context.Database.SqlQuery<bool>("SELECT dbo.IsValidUserToken (@UserProfilePrmKey, @MobileOTP, @EmailVCode, @TokenFor)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("@MobileOTP", _mobileOTP), new SqlParameter("EmailVCode", _emailVCode), new SqlParameter("TokenFor", _tokenFor)).FirstOrDefault();             
        }        
    }
}
