using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Parameter.Security
{
    public class UserAuthenticationParameterViewModel
    {    
        // UserAuthenticationParameter

        public short PrmKey { get; set; }

        public Guid UserAuthenticationParameterId { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableUserNameCaseSensetivity { get; set; }

        public bool EnablePasswordCaseSensetivity { get; set; }

        public bool EnableSmartForgotPassword { get; set; }

        public bool EnableMobileOTPForAuthentication { get; set; }

        public bool EnableDeviceAuthentication { get; set; }

        [StringLength(3)]
        public string AuthenticationMobileOTPDataType { get; set; }

        public byte AuthenticationMobileOTPLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForAuthenticationMobileOTP { get; set; }

        [StringLength(10)]
        public string PostfixStringForAuthenticationMobileOTP { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForAuthenticationMobileOTP { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForAuthenticationMobileOTP { get; set; }

        public TimeSpan AuthenticationMobileOTPExpiryTime { get; set; }

        public byte MaximumResendForAuthenticationMobileOTP { get; set; }

        public bool EnableEmailCodeForAuthentication { get; set; }

        [StringLength(3)]
        public string AuthenticationEmailCodeDataType { get; set; }

        public byte AuthenticationEmailCodeLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForAuthenticationEmailCode { get; set; }

        [StringLength(10)]
        public string PostfixStringForAuthenticationEmailCode { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForAuthenticationEmailCode { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForAuthenticationEmailCode { get; set; }

        public TimeSpan AuthenticationEmailCodeExpiryTime { get; set; }

        public byte MaximumResendForAuthenticationEmailCode { get; set; }

        public bool EnableMobileOTPForClearingSession { get; set; }

        [StringLength(3)]
        public string ClearingSessionMobileOTPDataType { get; set; }

        public byte ClearingSessionMobileOTPLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForClearingSessionMobileOTP { get; set; }

        [StringLength(10)]
        public string PostfixStringForClearingSessionMobileOTP { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForClearingSessionMobileOTP { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForClearingSessionMobileOTP { get; set; }

        public TimeSpan ClearingSessionMobileOTPExpiryTime { get; set; }

        public byte MaximumResendForClearingSessionMobileOTP { get; set; }

        public bool EnableEmailCodeForClearingSession { get; set; }

        [StringLength(3)]
        public string ClearingSessionEmailCodeDataType { get; set; }

        public byte ClearingSessionEmailCodeLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForClearingSessionEmailCode { get; set; }

        [StringLength(10)]
        public string PostfixStringForClearingSessionEmailCode { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForClearingSessionEmailCode { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForClearingSessionEmailCode { get; set; }

        public TimeSpan ClearingSessionEmailCodeExpiryTime { get; set; }

        public byte MaximumResendForClearingSessionEmailCode { get; set; }

        public bool EnableMobileOTPForUnlockAccount { get; set; }

        [StringLength(3)]
        public string UnlockAccountMobileOTPDataType { get; set; }

        public byte UnlockAccountMobileOTPLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForUnlockAccountMobileOTP { get; set; }

        [StringLength(10)]
        public string PostfixStringForUnlockAccountMobileOTP { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForUnlockAccountMobileOTP { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForUnlockAccountMobileOTP { get; set; }

        public TimeSpan UnlockAccountMobileOTPExpiryTime { get; set; }

        public byte MaximumResendForUnlockAccountMobileOTP { get; set; }

        public bool EnableEmailCodeForUnlockAccount { get; set; }

        [StringLength(3)]
        public string UnlockAccountEmailCodeDataType { get; set; }

        public byte UnlockAccountEmailCodeLength { get; set; }

        [StringLength(10)]
        public string PrefixStringForUnlockAccountEmailCode { get; set; }

        [StringLength(10)]
        public string PostfixStringForUnlockAccountEmailCode { get; set; }

        [StringLength(100)]
        public string IncludedCharactersForUnlockAccountEmailCode { get; set; }

        [StringLength(100)]
        public string ExcludedCharactersForUnlockAccountEmailCode { get; set; }

        public TimeSpan UnlockAccountEmailCodeExpiryTime { get; set; }

        public byte MaximumResendForUnlockAccountEmailCode { get; set; }

        public TimeSpan TokenExpiredTime { get; set; }

        public byte NumOfResendToken { get; set; }

        public short SuccessiveInvalidAttempts { get; set; }

        public short CumulativeInvalidAttempts { get; set; }

        public short IntervalOfResettingCumulativeInvalidAttempt { get; set; }

        public short InvalidAttemptLockingTimePeriod { get; set; }

        [StringLength(1)]
        public string InvalidAttemptLockingTimePeriodIn { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // UserAuthenticationParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserAuthenticationParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(50)]
        public string NameOfAuthorizer { get; set; }
        
        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public short PrmKeyForScript { get; set; }
    }
}