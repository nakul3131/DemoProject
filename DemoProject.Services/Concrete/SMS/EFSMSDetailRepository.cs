using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.SMS;
using DemoProject.Services.Abstract.SMS;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.SMS
{
    public class EFSMSDetailRepository : ISMSDetailRepository
    {
        private readonly EFDbContext context;

        public EFSMSDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public IEnumerable<SmsProviderAccountDetail> SmsProviderAccountDetails
        {
            get { return context.SmsProviderAccountDetails; }
        }

        public SmsAccountCredential GetSmsAccountCredentialsByNoticeType(string _nameOfNoticeType)
        {
            return context.Database.SqlQuery<SmsAccountCredential>("SELECT * FROM dbo.GetSmsAccountCredentialsByNoticeType (@NameOfSubCategory)", new SqlParameter("@NameOfSubCategory", _nameOfNoticeType)).FirstOrDefault();
        }

        public void SaveSmsProviderAccountDetail(SmsProviderAccountDetail _smsProviderAccountDetail)
        {
            context.SmsProviderAccountDetails.Add(_smsProviderAccountDetail);
            context.SaveChanges();
        }

        public IEnumerable<SmsProvider> SmsProviders
        {
            get { return context.SmsProviders; }
        }

        public void SaveSmsProvider(SmsProvider _smsProvider)
        {
            context.SmsProviders.Attach(_smsProvider);
            context.Entry(_smsProvider).State = EntityState.Added;

            //context.SmsProviders.Add(_smsProvider);
            context.SaveChanges();
        }

        public IEnumerable<SmsUserAuthenticationToken> SmsUserAuthenticationTokens
        {
            get { return context.SmsUserAuthenticationTokens; }
        }

        public string GetSmsForAccountOpening(long _customerAccountPrmKey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetSmsTemplateForAccountOpening (@CustomerAccountPrmKey)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey)).FirstOrDefault();
        }

        public string GetSmsForMembershipApproval(long _customerAccountPrmKey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetSmsTemplateForMembershipApproval (@CustomerAccountPrmKey)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey)).FirstOrDefault();
        }

        public string GetSmsForMembershipRejection(long _customerAccountPrmKey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetSmsTemplateForMembershipRejection (@CustomerAccountPrmKey)", new SqlParameter("@CustomerAccountPrmKey", _customerAccountPrmKey)).FirstOrDefault();
        }

        public string GetSmsForUserAuthenticationToken(int _userAuthenticationTokenPrmKey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetSmsTemplateForUserAuthenticationOTP (@UserAuthenticationTokenPrmKey)", new SqlParameter("@UserAuthenticationTokenPrmKey", _userAuthenticationTokenPrmKey)).FirstOrDefault();
        }

        public string GetSmsForTeleVerificationToken(int _teleVerificationTokenPrmKey)
        {
            return context.Database.SqlQuery<string>("SELECT dbo.GetSmsTemplateForTeleVerificationOTP (@TeleVerificationTokenPrmKey)", new SqlParameter("@TeleVerificationTokenPrmKey", _teleVerificationTokenPrmKey)).FirstOrDefault();
        }

        public void SaveSmsUserAuthenticationToken(int _userAuthenticationToken, SmsResponse _smsResponse)
        {
            try
            {
                DateTime now = DateTime.Now;

                SmsUserAuthenticationToken smsUserAuthenticationToken = new SmsUserAuthenticationToken
                {
                    UserAuthenticationTokenPrmKey = _userAuthenticationToken,
                    SendingDate = now,
                    SMSProviderMessageID = _smsResponse.MessageId.ToString(),
                    SMSProviderClientID = _smsResponse.ClientId.ToString(),
                    DeliveryStatus = _smsResponse.Result.ToString(),
                    DeliveryDate = now
                };

                //context.SmsUserAuthenticationTokens.Add(smsUserAuthenticationToken);
                context.SmsUserAuthenticationTokens.Attach(smsUserAuthenticationToken);
                context.Entry(smsUserAuthenticationToken).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        public void SaveMessageLog(string _referencePrmKey, SmsResponse _smsResponse, string _mobileNumber, string _message, string _senderId)
        {
            try
            {
                DateTime now = DateTime.Now;

                MessageLog messageLog = new MessageLog
                {
                    SystemReferenceNumber = _referencePrmKey.ToString(),
                    GateWayReferenceNumber = _smsResponse.MessageId,
                    MobileNumber = _mobileNumber,
                    Message = _message,
                    SentDateTime = now,
                    DeliveryDateTime = now,
                    DeliveryCode = "0",
                    DeliveryStatus = _smsResponse.Status,
                    SenderId = _senderId
                };

                //context.SmsUserAuthenticationTokens.Add(smsUserAuthenticationToken);
                context.MessageLogs.Attach(messageLog);
                context.Entry(messageLog).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

    }
}
