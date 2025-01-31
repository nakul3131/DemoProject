using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Security.SystemEntity;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security
{
    public class EFSecurityDetailRepository : ISecurityDetailRepository
    {
        private readonly EFDbContext context;

        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        private readonly IPersonDetailRepository personDetailRepository;

        public EFSecurityDetailRepository(RepositoryConnection _connection, IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            personDetailRepository = _personDetailRepository;
        }

        public IEnumerable<AuthenticationMethod> AuthenticationMethods
        {
            get { return context.AuthenticationMethods; }
        }

        public bool GetUserProfilePasswords(short _userProfilePrmKey, string _inputedPassword)
        {
            var result = context.Database.SqlQuery<bool>("SELECT dbo.GetUserProfilePasswords(@UserProfilePrmKey, @InputedPassword)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey), new SqlParameter("@InputedPassword", _inputedPassword)).FirstOrDefault();
            return result;
        }

        public bool IsUserLocked(short _userProfilePrmKey)
        {
            string result = context.UserProfiles
                          .Where(u => u.PrmKey == _userProfilePrmKey)
                          .Select(u => u.UserProfileStatus).FirstOrDefault();

            if (result == "L")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserOnline(short _userProfilePrmKey)
        {
            string result = context.UserProfiles
                           .Where(u => u.PrmKey == _userProfilePrmKey)
                           .Select(u => u.UserProfileStatus).FirstOrDefault();

            if (result == "O")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public byte GetAuthenticationMethodPrmKeyByName(string _methodName)
        {
            return context.AuthenticationMethods
                            .Where(a => a.NameForSystem == _methodName)
                            .Select(a => a.PrmKey).FirstOrDefault();
        }

        public byte GetChecksumAlgorithmPrmKeyById(Guid _checksumAlgorithmId)
        {
            return context.ChecksumAlgorithms
                    .Where(c => c.ChecksumAlgorithmId == _checksumAlgorithmId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public byte GetUserTypePrmKeyById(Guid _userTypeId)
        {
            return context.UserTypes
                    .Where(c => c.UserTypeId == _userTypeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public decimal GetMaximumAmountLimitForTransaction(short _userProfilePrmKey)
        {
            return context.UserProfileTransactionLimits
                    .Where(l => l.UserProfilePrmKey.Equals(_userProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumAmountLimitForTransaction).FirstOrDefault();
        }

        public decimal GetMinimumAmountLimitForTransaction(short _userProfilePrmKey)
        {
            return context.UserProfileTransactionLimits
                    .Where(l => l.UserProfilePrmKey.Equals(_userProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MinimumAmountLimitForTransaction).FirstOrDefault();
        }

        public short GetLoginDevicePrmKeyById(Guid _loginDeviceId)
        {
            return context.LoginDevices
                    .Where(c => c.LoginDeviceId == _loginDeviceId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetPasswordPolicyPrmKeyById(Guid _passwordPolicyId)
        {
            return context.PasswordPolicies
                    .Where(c => c.PasswordPolicyId == _passwordPolicyId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetPastDaysPermissionForAutoVerification(short _userProfilePrmKey)
        {
            return context.UserProfileTransactionLimits
                    .Where(l => l.UserProfilePrmKey.Equals(_userProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumNumberOfBackDaysForAutoVerification).FirstOrDefault();
        }

        public short GetPastDaysPermissionForTransaction(short _userProfilePrmKey, DateTime _closingFinancialYearEndDate)
        {
            short days =  context.UserProfileTransactionLimits
                            .Where(l => l.UserProfilePrmKey.Equals(_userProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                            .Select(l => l.MaximumNumberOfBackDaysForTransaction).FirstOrDefault();


            if (days > 0) 
            { 
                DateTime allowedPastDate = DateTime.Now.AddDays(-days);

                short validDays = 0;

                if (allowedPastDate <= _closingFinancialYearEndDate)
                {
                    validDays = (short)DateTime.Now.Subtract(_closingFinancialYearEndDate).Days;

                    if(validDays > 0)
                    {
                        days = (short)(validDays - 1);
                    }                    
                }
            }

            return days;
        }

        public short GetPastDaysPermissionForVerification(short _userProfilePrmKey)
        {
            return context.UserProfileTransactionLimits
                    .Where(l => l.UserProfilePrmKey.Equals(_userProfilePrmKey) && l.EntryStatus.Equals(StringLiteralValue.Verify) && l.ActivationStatus.Equals(StringLiteralValue.Active))
                    .Select(l => l.MaximumNumberOfBackDaysForVerification).FirstOrDefault();
        }

        public short GetRoleProfilePrmKeyById(Guid _roleProfileId)
        {

            var r = context.RoleProfiles
                     .Where(c => c.RoleProfileId == _roleProfileId)
                     .Select(c => c.PrmKey).FirstOrDefault();

            return r;
        }

        public short GetSpecialPermissionPrmKeyById(Guid _specialPermissionId)
        {
            return context.SpecialPermissions
                    .Where(c => c.SpecialPermissionId == _specialPermissionId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public short GetUserProfilePrmKeyByColumn(string _columnName, string _valueOfColumn)
        {
            short result = 0;

            if (_columnName == "NameOfUserProfile")
            {
                result = context.UserProfiles
                            .Where(u => u.NameOfUserProfile == _valueOfColumn)
                            .Select(u => u.PrmKey)
                            .FirstOrDefault();
            }

            return result;
        }

        public short GetUserProfilePrmKeyById(Guid _userProfileId)
        {
            var a = context.UserProfiles
                     .Where(c => c.UserProfileId == _userProfileId)
                     .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }

        public short GetUserProfilePrmKeyByName(string _nameOfUserProfile)
        {
            return context.Database.SqlQuery<short>("SELECT dbo.GetUserPrmKey (@NameOfUserProfile)", new SqlParameter("@NameOfUserProfile", _nameOfUserProfile)).FirstOrDefault();
        }

        public string GetUserProfileFieldByPrmKey(string _columnName, short _prmKey)
        {
            string result;
            switch (_columnName)
            {
                case "MobileNumber":
                    result = context.UserProfiles
                            .Where(u => u.PrmKey == _prmKey & u.IsMobileNumberConfirmed == true)
                            .Select(u => u.MobileNumber).FirstOrDefault();
                    break;
                case "AlternateMobileNumber":
                    result = context.UserProfiles
                            .Where(u => u.PrmKey == _prmKey & u.IsAlternateEmailIdConfirmed == true)
                            .Select(u => u.AlternateMobileNumber).FirstOrDefault();
                    break;
                default:
                    result = "0";
                    break;
            }
            return result;
        }

        public string GetUserProfileRegisteredEmailId(short _userProfilePrmKey)
        {
            return context.UserProfiles
                            .Where(u => u.PrmKey == _userProfilePrmKey)
                            .Select(u => u.EmailId)
                            .FirstOrDefault();
        }

        public string GetUserProfileRegisteredMobileNumber(short _userProfilePrmKey)
        {
            return context.UserProfiles
                    .Where(u => u.PrmKey == _userProfilePrmKey)
                    .Select(u => u.MobileNumber)
                    .FirstOrDefault();
        }

        public string GetUserProfileStatus(short _userProfilePrmKey)
        {
            return context.UserProfiles
                        .Where(u => u.PrmKey == _userProfilePrmKey)
                        .Select(u => u.UserProfileStatus)
                        .FirstOrDefault();
        }

        public List<SelectListItem> ChecksumAlgorithmDropdownList
        {
            get
            {
                return (from c in context.ChecksumAlgorithms
                        where (c.ActivationStatus.Equals(StringLiteralValue.Active))
                        select new SelectListItem
                        {
                            Value = c.ChecksumAlgorithmId.ToString(),
                            Text = c.NameOfChecksumAlgorithm.Trim()
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> LoginDeviceDropDownList
        {
            get
            {
                return (from l in context.LoginDevices

                        select new SelectListItem
                        {
                            Value = l.LoginDeviceId.ToString(),
                            Text = l.NameOfDevice
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> PasswordPolicyDropDownList
        {
            get
            {
                return (from p in context.PasswordPolicies
                        join mf in context.PasswordPolicyModifications .Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.PasswordPolicyPrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus.Equals(StringLiteralValue.Verify) && p.ActivationStatus.Equals(StringLiteralValue.Active)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = p.PasswordPolicyId.ToString(),
                            Text = ((mf.NameOfPasswordPolicy.Equals(null)) ? p.NameOfPasswordPolicy.Trim() : mf.NameOfPasswordPolicy)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> RoleProfileDropDownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from c in context.RoleProfiles
                            join mf in context.RoleProfileModifications on c.PrmKey equals mf.RoleProfilePrmKey into cm
                            from mf in cm.DefaultIfEmpty()
                            join t in context.RoleProfileTranslations on c.PrmKey equals t.RoleProfilePrmKey into ct
                            from t in ct.DefaultIfEmpty()
                            where (c.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    || (c.EntryStatus == StringLiteralValue.Verify)
                                    && (c.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    && (c.IsModified.Equals(false))
                            select new SelectListItem
                            {
                                Value = c.RoleProfileId.ToString(),
                                Text = ((mf.NameOfRoleProfile.Equals(null)) ? c.NameOfRoleProfile.Trim() + " ---> " + (t.TransNameOfRoleProfile.Equals(null) ? " " : t.TransNameOfRoleProfile.Trim()) : mf.NameOfRoleProfile + " ---> " + (t.TransNameOfRoleProfile.Equals(null) ? " " : t.TransNameOfRoleProfile.Trim()))
                            }).Distinct().OrderBy(l => l.Text).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from h in context.RoleProfiles
                        join mf in context.RoleProfileModifications on h.PrmKey equals mf.RoleProfilePrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.RoleProfileTranslations on h.PrmKey equals t.RoleProfilePrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                || (h.EntryStatus == StringLiteralValue.Verify)
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (h.IsModified.Equals(false))
                        select new SelectListItem
                        {
                            Value = h.RoleProfileId.ToString(),
                            Text = ((mf.NameOfRoleProfile.Equals(null)) ? h.NameOfRoleProfile.Trim() : mf.NameOfRoleProfile.Trim())
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> SpecialPermissionDropdownList
        {
            get
            {
                return (from s in context.SpecialPermissions
                        where (s.EntryStatus.Equals(StringLiteralValue.Verify)
                        && (s.ActivationStatus.Equals(StringLiteralValue.Active)))
                        select new SelectListItem
                        {
                            Value = s.SpecialPermissionId.ToString(),
                            Text = ((s.NameOfSpecialPermission.Trim()))
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> UserProfileDropDownList
        {
            get
            {
                return (from p in context.UserProfiles
                        join mf in context.UserProfileModifications.Where(mf => mf.EntryStatus == StringLiteralValue.Verify) on p.PrmKey equals mf.UserProfilePrmKey into pm
                        from mf in pm.DefaultIfEmpty()
                        where (p.EntryStatus.Equals(StringLiteralValue.Verify)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = p.UserProfileId.ToString(),
                            Text = ((mf.NameOfUserProfile.Equals(null)) ? p.NameOfUserProfile.Trim() : mf.NameOfUserProfile)
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

        public List<SelectListItem> UserTypeDropdownList
        {
            get
            {
                return (from u in context.UserTypes
                        where (u.ActivationStatus.Equals(StringLiteralValue.Active))
                        orderby u.NameOfUserType
                        select new SelectListItem
                        {
                            Value = u.UserTypeId.ToString(),
                            Text = (u.NameOfUserType.Trim() + " ---> ")
                        }).Distinct().OrderBy(l => l.Text).ToList();
            }
        }

    }
}
