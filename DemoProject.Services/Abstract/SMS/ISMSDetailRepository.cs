using System.Collections.Generic;
using DemoProject.Domain.CustomEntities;
using DemoProject.Domain.Entities.SMS;

namespace DemoProject.Services.Abstract.SMS
{
    public interface ISMSDetailRepository
    {
        IEnumerable<SmsProviderAccountDetail> SmsProviderAccountDetails { get; }

        IEnumerable<SmsUserAuthenticationToken> SmsUserAuthenticationTokens { get; }

        IEnumerable<SmsProvider> SmsProviders { get; }

        string GetSmsForAccountOpening(long _customerAccountPrmKey);

        string GetSmsForMembershipApproval(long _customerAccountPrmKey);

        string GetSmsForUserAuthenticationToken(int _userAuthenticationTokenPrmKey);

        string GetSmsForTeleVerificationToken(int _teleVerificationTokenPrmKey);

        void SaveSmsProvider(SmsProvider _smsProvider);

        void SaveSmsUserAuthenticationToken(int _userAuthenticationTokenPrmKey, SmsResponse _smsResponse);

        void SaveMessageLog(string _referencePrmKey, SmsResponse _smsResponse, string _mobileNumber, string _message, string _senderId);

        void SaveSmsProviderAccountDetail(SmsProviderAccountDetail _smsProviderAccountDetail);

        SmsAccountCredential GetSmsAccountCredentialsByNoticeType(string _nameOfNoticeType);
    }
}
