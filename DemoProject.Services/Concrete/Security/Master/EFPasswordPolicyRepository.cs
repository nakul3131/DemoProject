using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Password;
using DemoProject.Services.Wrapper;
using DemoProject.Domain.Entities.Security.Master;

namespace DemoProject.Services.Concrete.Security.Master
{
    public class EFPasswordPolicyRepository : IPasswordPolicyRepository
    {
        private readonly EFDbContext context;

        public EFPasswordPolicyRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            try
            {
                // Set Default Value
                _passwordPolicyViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _passwordPolicyViewModel.EntryDateTime = DateTime.Now;
                _passwordPolicyViewModel.EntryStatus = StringLiteralValue.Amend;
                _passwordPolicyViewModel.Remark = "None";
                _passwordPolicyViewModel.UserAction = StringLiteralValue.Amend;
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _passwordPolicyViewModel.PrmKey = _passwordPolicyViewModel.PasswordPolicyPrmKey;

                PasswordPolicy passwordPolicy = Mapper.Map<PasswordPolicy>(_passwordPolicyViewModel);
                PasswordPolicyMakerChecker passwordPolicyMakerChecker = Mapper.Map<PasswordPolicyMakerChecker>(_passwordPolicyViewModel);

                context.PasswordPolicyMakerCheckers.Attach(passwordPolicyMakerChecker);
                context.Entry(passwordPolicyMakerChecker).State = EntityState.Added;
                context.PasswordPolicyMakerCheckers.Add(passwordPolicyMakerChecker);

                context.PasswordPolicies.Attach(passwordPolicy);
                context.Entry(passwordPolicy).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            try
            {
                // Set Default Value
                _passwordPolicyViewModel.EntryDateTime = DateTime.Now;
                _passwordPolicyViewModel.Remark = "None";
                _passwordPolicyViewModel.UserAction = StringLiteralValue.Delete;
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PasswordPolicyMakerChecker passwordPolicyMakerChecker = Mapper.Map<PasswordPolicyMakerChecker>(_passwordPolicyViewModel);

                context.PasswordPolicyMakerCheckers.Attach(passwordPolicyMakerChecker);
                context.Entry(passwordPolicyMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PasswordPolicyViewModel> GetRejectedEntry(Guid PasswordPolicyId)
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntry (@PasswordPolicyId, @EntriesType)", new SqlParameter("@PasswordPolicyId", PasswordPolicyId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PasswordPolicyViewModel> GetUnVerifiedEntry(Guid PasswordPolicyId)
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntry (@PasswordPolicyId, @EntriesType)", new SqlParameter("@PasswordPolicyId", PasswordPolicyId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PasswordPolicyViewModel> GetVerifiedEntry(Guid PasswordPolicyId)
        {
            try
            {
                return await context.Database.SqlQuery<PasswordPolicyViewModel>("SELECT * FROM dbo.GetPasswordPolicyEntry (@PasswordPolicyId, @EntriesType)", new SqlParameter("@PasswordPolicyId", PasswordPolicyId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public PasswordPolicyValueViewModel GetPasswordPolicyValues(short _userProfilePrmKey)
        {
            try
            {
                return context.Database.SqlQuery<PasswordPolicyValueViewModel>("SELECT * FROM dbo.GetPasswordPolicyValues(@UserProfilePrmKey)", new SqlParameter("@UserProfilePrmKey", _userProfilePrmKey)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<bool> Reject(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            try
            {
                // Set Default Value
                _passwordPolicyViewModel.EntryDateTime = DateTime.Now;
                _passwordPolicyViewModel.UserAction = StringLiteralValue.Reject;
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PasswordPolicyMakerChecker passwordPolicyMakerChecker = Mapper.Map<PasswordPolicyMakerChecker>(_passwordPolicyViewModel);

                context.PasswordPolicyMakerCheckers.Attach(passwordPolicyMakerChecker);
                context.Entry(passwordPolicyMakerChecker).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            try
            {
                // Set Default Value
                _passwordPolicyViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _passwordPolicyViewModel.EntryDateTime = DateTime.Now;
                _passwordPolicyViewModel.EntryStatus = StringLiteralValue.Create;
                _passwordPolicyViewModel.Remark = "None";
                _passwordPolicyViewModel.UserAction = StringLiteralValue.Create;
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PasswordPolicy passwordPolicy = Mapper.Map<PasswordPolicy>(_passwordPolicyViewModel);

                PasswordPolicyMakerChecker passwordPolicyMakerChecker = Mapper.Map<PasswordPolicyMakerChecker>(_passwordPolicyViewModel);

                context.PasswordPolicyMakerCheckers.Attach(passwordPolicyMakerChecker);
                context.Entry(passwordPolicyMakerChecker).State = EntityState.Added;
                passwordPolicy.PasswordPolicyMakerCheckers.Add(passwordPolicyMakerChecker);

                context.PasswordPolicies.Attach(passwordPolicy);
                context.Entry(passwordPolicy).State = EntityState.Added;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            try
            {
                // Set Default Value
                _passwordPolicyViewModel.EntryDateTime = DateTime.Now;
                _passwordPolicyViewModel.Remark = "None";
                _passwordPolicyViewModel.UserAction = StringLiteralValue.Verify;
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                PasswordPolicyMakerChecker passwordPolicyMakerChecker = Mapper.Map<PasswordPolicyMakerChecker>(_passwordPolicyViewModel);

                context.PasswordPolicyMakerCheckers.Attach(passwordPolicyMakerChecker);
                context.Entry(passwordPolicyMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
