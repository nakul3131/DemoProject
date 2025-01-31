using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.Parameter.Security;
using DemoProject.Domain.Entities.Security.Parameter;
using DemoProject.Services.Abstract.Security.Parameter;

namespace DemoProject.Services.Concrete.Security.Parameter
{
    public class EFUserAuthenticationParameterRepository : IUserAuthenticationParameterRepository
    {
        private readonly EFDbContext context;

        public EFUserAuthenticationParameterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            try
            {
                // Set Default Value
                _userAuthenticationParameterViewModel.EntryDateTime = DateTime.Now;
                _userAuthenticationParameterViewModel.EntryStatus = StringLiteralValue.Amend;
                _userAuthenticationParameterViewModel.ReasonForModification = "None";
                _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Amend;
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForAuthentication)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForAuthenticationMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForAuthentication)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForAuthenticationEmailCode = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForClearingSession)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForClearingSessionMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForClearingSession)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForClearingSessionEmailCode = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForUnlockAccount)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForUnlockAccountMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForUnlockAccount)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForUnlockAccountEmailCode = 0;
                }

                // Mapping
                UserAuthenticationParameter userAuthenticationParameter = Mapper.Map<UserAuthenticationParameter>(_userAuthenticationParameterViewModel);
                UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerChecker = Mapper.Map<UserAuthenticationParameterMakerChecker>(_userAuthenticationParameterViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                userAuthenticationParameter.PrmKey = _userAuthenticationParameterViewModel.UserAuthenticationParameterPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // UserAuthenticationParameter
                context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerChecker);
                context.Entry(userAuthenticationParameterMakerChecker).State = EntityState.Added;
                userAuthenticationParameter.UserAuthenticationParameterMakerCheckers.Add(userAuthenticationParameterMakerChecker);

                context.UserAuthenticationParameters.Attach(userAuthenticationParameter);
                context.Entry(userAuthenticationParameter).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            try
            {
                // Set Default Value
                _userAuthenticationParameterViewModel.EntryDateTime = DateTime.Now;
                _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Delete;
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerChecker = Mapper.Map<UserAuthenticationParameterMakerChecker>(_userAuthenticationParameterViewModel);

                // UserAuthenticationParameter
                context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerChecker);
                context.Entry(userAuthenticationParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<UserAuthenticationParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<UserAuthenticationParameterViewModel>("SELECT * FROM dbo.GetUserAuthenticationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }

        public async Task<UserAuthenticationParameterViewModel> GetRejectedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<UserAuthenticationParameterViewModel>("SELECT * FROM dbo.GetUserAuthenticationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }

        public async Task<UserAuthenticationParameterViewModel> GetUnAuthorizedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<UserAuthenticationParameterViewModel>("SELECT * FROM dbo.GetUserAuthenticationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }

        public async Task<IEnumerable<UserAuthenticationParameterViewModel>> GetUserAuthenticationParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<UserAuthenticationParameterViewModel>("SELECT * FROM dbo.GetUserAuthenticationParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }

        public async Task<UserAuthenticationParameterViewModel> GetUnVerifiedEntry()
        {
            try
            {
                return await context.Database.SqlQuery<UserAuthenticationParameterViewModel>("SELECT * FROM dbo.GetUserAuthenticationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            //check waiting for response and rejected entries count
            int count = await context.UserAuthenticationParameters
                        .Where(u => u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            try
            {
                // Set Default Value
                _userAuthenticationParameterViewModel.EntryDateTime = DateTime.Now;
                _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Reject;
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerChecker = Mapper.Map<UserAuthenticationParameterMakerChecker>(_userAuthenticationParameterViewModel);

                //UserAuthenticationParameter
                context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerChecker);
                context.Entry(userAuthenticationParameterMakerChecker).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            try
            {
                // Set Default Value
                _userAuthenticationParameterViewModel.EntryDateTime = DateTime.Now;
                _userAuthenticationParameterViewModel.EntryStatus = StringLiteralValue.Create;
                _userAuthenticationParameterViewModel.Remark = "None";
                _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Create;
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForAuthentication)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForAuthenticationMobileOTP = "None";
                    _userAuthenticationParameterViewModel.AuthenticationMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForAuthenticationMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForAuthentication)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForAuthenticationEmailCode = "None";
                    _userAuthenticationParameterViewModel.AuthenticationEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForAuthenticationEmailCode = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForClearingSession)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForClearingSessionMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ClearingSessionMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForClearingSessionMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForClearingSession)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForClearingSessionEmailCode = "None";
                    _userAuthenticationParameterViewModel.ClearingSessionEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForClearingSessionEmailCode = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableMobileOTPForUnlockAccount)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPDataType = "NNN";
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForUnlockAccountMobileOTP = "None";
                    _userAuthenticationParameterViewModel.UnlockAccountMobileOTPExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForUnlockAccountMobileOTP = 0;
                }

                if (_userAuthenticationParameterViewModel.EnableEmailCodeForUnlockAccount)
                {
                }

                else
                {
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeDataType = "NNN";
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeLength = 0;
                    _userAuthenticationParameterViewModel.PrefixStringForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.PostfixStringForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.IncludedCharactersForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.ExcludedCharactersForUnlockAccountEmailCode = "None";
                    _userAuthenticationParameterViewModel.UnlockAccountEmailCodeExpiryTime = Convert.ToDateTime("12:00 AM").TimeOfDay;
                    _userAuthenticationParameterViewModel.MaximumResendForUnlockAccountEmailCode = 0;
                }

                // Mapping
                UserAuthenticationParameter userAuthenticationParameter = Mapper.Map<UserAuthenticationParameter>(_userAuthenticationParameterViewModel);
                UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerChecker = Mapper.Map<UserAuthenticationParameterMakerChecker>(_userAuthenticationParameterViewModel);

                // UserAuthenticationParameter
                context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerChecker);
                context.Entry(userAuthenticationParameterMakerChecker).State = EntityState.Added;
                userAuthenticationParameter.UserAuthenticationParameterMakerCheckers.Add(userAuthenticationParameterMakerChecker);

                context.UserAuthenticationParameters.Attach(userAuthenticationParameter);
                context.Entry(userAuthenticationParameter).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            try
            {
                // Modify Old Record      
                UserAuthenticationParameterViewModel userAuthenticationParameterViewModelOfOldEntry = await GetActiveEntry();

                if (userAuthenticationParameterViewModelOfOldEntry.PrmKey > 0)
                {
                    // Set Default Value
                    userAuthenticationParameterViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                    userAuthenticationParameterViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                    userAuthenticationParameterViewModelOfOldEntry.UserProfilePrmKey = _userAuthenticationParameterViewModel.UserProfilePrmKey;

                    // Mapping
                    UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerCheckerForModify = Mapper.Map<UserAuthenticationParameterMakerChecker>(userAuthenticationParameterViewModelOfOldEntry);

                    // UserAuthenticationParameter
                    context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerCheckerForModify);
                    context.Entry(userAuthenticationParameterMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                // Set Default Value
                _userAuthenticationParameterViewModel.EntryDateTime = DateTime.Now;
                _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Verify;
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                UserAuthenticationParameterMakerChecker userAuthenticationParameterMakerChecker = Mapper.Map<UserAuthenticationParameterMakerChecker>(_userAuthenticationParameterViewModel);

                // UserAuthenticationParameter
                context.UserAuthenticationParameterMakerCheckers.Attach(userAuthenticationParameterMakerChecker);
                context.Entry(userAuthenticationParameterMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public IEnumerable<UserAuthenticationParameter> UserAuthenticationParameters
        {
            get
            {
                return context.UserAuthenticationParameters;
            }
        }
    }
}