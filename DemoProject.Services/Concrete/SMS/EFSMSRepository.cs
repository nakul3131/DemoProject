using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.SMS;
using DemoProject.Services.Abstract.SMS;
using DemoProject.Services.Utility.SmsSender;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.SMS
{
    public class EFSMSRepository : ISMSRepository 
    {
        private readonly EFDbContext context;

        private readonly ISMSDetailRepository smsDetailRepository;

        public EFSMSRepository(RepositoryConnection _connection, ISMSDetailRepository _smsDetailRepository)
        {
            context = _connection.EFDbContext;
            smsDetailRepository = _smsDetailRepository;
        }


        public string CreateTeleVerificationSMSToken(string _mobileNumber)
        {
            try
            {

                SmsSender sms = new SmsSender();

                DateTime now = DateTime.Now;

                TeleVerificationToken teleVerificationToken = new TeleVerificationToken()
                {
                    TeleType = "Mobile",
                    TeleNumber = _mobileNumber,
                    Token = Encoding.UTF8.GetBytes("1"),
                    TokenGeneratedOn = now,
                    TokenExpiredOn = now,
                    IsGenerated = false,
                    TokenStatus = "UNU"
                };

                context.TeleVerificationTokens.Attach(teleVerificationToken);
                context.Entry(teleVerificationToken).State = EntityState.Added;

                //context.UserAuthenticationTokens.Add(userAuthenticationToken);
                context.SaveChanges();

                int teleVerificationTokenPrmKey = teleVerificationToken.PrmKey;

                // Send Token Sms and get response (i.e. sending status)
                SmsResponse smsResponse = sms.SendTeleVerificationSMSToken(teleVerificationToken.PrmKey, _mobileNumber);

                // Save Sms User Authentication Entry
                //smsDetailRepository.SaveMessageLog(teleVerificationToken.PrmKey, smsResponse, _mobileNumber);

                return smsResponse.Result.ToString();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return error;
            }
        }


        public int GetRecentTeleVerificationTokenPrmKey(string _mobileNumber)
        {
            return context.TeleVerificationTokens
                    .Where(t => t.TeleNumber == _mobileNumber)
                    .OrderByDescending(t => t.TokenGeneratedOn)
                    .Select(t => t.PrmKey).FirstOrDefault();
        }

        public bool IsValidMobileVerificationToken(string _teleNumber, string _token)
        {
            return context.Database.SqlQuery<bool>("SELECT dbo.IsValidMobileVerificationToken (@TeleNumber, @Token)", new SqlParameter("@TeleNumber", _teleNumber), new SqlParameter("@Token", _token)).FirstOrDefault();
        }

    }
}
