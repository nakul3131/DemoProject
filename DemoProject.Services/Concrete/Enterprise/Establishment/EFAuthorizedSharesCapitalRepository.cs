using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Domain.Entities.Enterprise.Establishment;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFAuthorizedSharesCapitalRepository : IAuthorizedSharesCapitalRepository
    {
        private readonly EFDbContext context;
        private readonly IOrganizationDetailRepository organizationDetailRepository;

        public EFAuthorizedSharesCapitalRepository(RepositoryConnection _connection, IOrganizationDetailRepository _organizationDetailRepository)
        {
            context = _connection.EFDbContext;
            organizationDetailRepository = _organizationDetailRepository;
        }

        public async Task<bool> Amend(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            try
            {
                // Set Default Value
                _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
                _authorizedSharesCapitalViewModel.EntryStatus = StringLiteralValue.Amend;
                _authorizedSharesCapitalViewModel.ReasonForModification = "None";
                _authorizedSharesCapitalViewModel.Remark = "None";
                _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Amend;
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapital authorizedSharesCapitalForAmend = Mapper.Map<AuthorizedSharesCapital>(_authorizedSharesCapitalViewModel);
                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerCheckerForAmend = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerCheckerForAmend);
                context.Entry(authorizedSharesCapitalMakerCheckerForAmend).State = EntityState.Added;
                authorizedSharesCapitalForAmend.AuthorizedSharesCapitalMakerCheckers.Add(authorizedSharesCapitalMakerCheckerForAmend);

                context.AuthorizedSharesCapitals.Attach(authorizedSharesCapitalForAmend);
                context.Entry(authorizedSharesCapitalForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            try
            {
                // Set Default Value
                _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
                _authorizedSharesCapitalViewModel.Remark = "None";
                _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Delete;
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerChecker = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            //check waiting for response and rejected entries count
            int count = await context.AuthorizedSharesCapitals
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject)
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

        public async Task<bool> Reject(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            try
            {
                // Set Default Value
                _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
                _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Reject;
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerChecker = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            try
            {
                // Set Default Value
                _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
                _authorizedSharesCapitalViewModel.EntryStatus = StringLiteralValue.Create;
                _authorizedSharesCapitalViewModel.Remark = "None";
                _authorizedSharesCapitalViewModel.ReasonForModification = "None";
                _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Create;
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapital authorizedSharesCapital = Mapper.Map<AuthorizedSharesCapital>(_authorizedSharesCapitalViewModel);
                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerChecker = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;
                authorizedSharesCapital.AuthorizedSharesCapitalMakerCheckers.Add(authorizedSharesCapitalMakerChecker);

                context.AuthorizedSharesCapitals.Attach(authorizedSharesCapital);
                context.Entry(authorizedSharesCapital).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            try
            {
                // First Modify Old Record
                AuthorizedSharesCapitalViewModel authorizedSharesCapitalViewModelOfOldEntry = await organizationDetailRepository.GetAuthorizedSharesCapitalEntry(StringLiteralValue.Verify);

                // Set Default Value
                authorizedSharesCapitalViewModelOfOldEntry.EntryDateTime = DateTime.Now;
                authorizedSharesCapitalViewModelOfOldEntry.UserAction = StringLiteralValue.Modify;
                authorizedSharesCapitalViewModelOfOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerCheckerForModify = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(authorizedSharesCapitalViewModelOfOldEntry);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerCheckerForModify);
                context.Entry(authorizedSharesCapitalMakerCheckerForModify).State = EntityState.Added;

                // Insert New Verified Entry
                // Set Default Value
                _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
                _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Verify;
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                AuthorizedSharesCapitalMakerChecker authorizedSharesCapitalMakerChecker = Mapper.Map<AuthorizedSharesCapitalMakerChecker>(_authorizedSharesCapitalViewModel);

                context.AuthorizedSharesCapitalMakerCheckers.Attach(authorizedSharesCapitalMakerChecker);
                context.Entry(authorizedSharesCapitalMakerChecker).State = EntityState.Added;

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
