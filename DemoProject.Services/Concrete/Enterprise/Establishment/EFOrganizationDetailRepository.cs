using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationDetailRepository : IOrganizationDetailRepository
    {
        private readonly EFDbContext context;

        public EFOrganizationDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<AuthorizedSharesCapitalViewModel> GetAuthorizedSharesCapitalEntry(string _entryType)
        {
            AuthorizedSharesCapitalViewModel authorizedSharesCapitalViewModel = new AuthorizedSharesCapitalViewModel();

            try
            {
                authorizedSharesCapitalViewModel = await context.Database.SqlQuery<AuthorizedSharesCapitalViewModel>("SELECT * FROM dbo.GetAuthorizedSharesCapitalEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }

            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return authorizedSharesCapitalViewModel;
        }

        public async Task<OrganizationFundViewModel> GetFundEntry(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationFundViewModel>("SELECT * FROM dbo.GetOrganizationFundEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<OrganizationLoanTypeViewModel> GetLoanTypeEntry(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<OrganizationContactDetailViewModel>> GetContactDetailEntries(string _entryType)
        {
            try
            {
                IEnumerable<OrganizationContactDetailViewModel> organizationContactDetailViewModel;

                organizationContactDetailViewModel = await context.Database.SqlQuery<OrganizationContactDetailViewModel>("SELECT * FROM dbo.GetOrganizationContactDetailEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();

                return organizationContactDetailViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationGSTRegistrationDetailViewModel>> GetGSTRegistrationDetailEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationGSTRegistrationDetailViewModel>("SELECT * FROM dbo.GetOrganizationGSTRegistrationDetailEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationFundViewModel>> GetFundEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationFundViewModel>("SELECT * FROM dbo.GetOrganizationFundEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetLoanTypeEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<AuthorizedSharesCapitalViewModel>> GetAuthorizedSharesCapitalIndex()
        {
            try
            {
                return await context.Database.SqlQuery<AuthorizedSharesCapitalViewModel>("SELECT * FROM dbo.GetAuthorizedSharesCapitalEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return null;
        }
        public async Task<IEnumerable<OrganizationIndexViewModel>> GetFundIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationIndexViewModel>("SELECT * FROM dbo.GetOrganizationFundEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetLoanTypeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void GetOrganizationAllDefaultValues(OrganizationViewModel _organizationViewModel, string _entryStatus)
        {
            _organizationViewModel.EntryDateTime = DateTime.Now;
            _organizationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _organizationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _organizationViewModel.EntryStatus = _entryStatus;
            _organizationViewModel.UserAction = _entryStatus;

            //AuthorizedSharesCapitalViewModel
            _organizationViewModel.AuthorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
            _organizationViewModel.AuthorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _organizationViewModel.AuthorizedSharesCapitalViewModel.EntryStatus = _entryStatus;
            _organizationViewModel.AuthorizedSharesCapitalViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _organizationViewModel.ReasonForModification = "None";
                _organizationViewModel.AuthorizedSharesCapitalViewModel.ReasonForModification = "None";

            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _organizationViewModel.Remark = "None";
                _organizationViewModel.AuthorizedSharesCapitalViewModel.Remark = "None";
            }
            else
            {
                _organizationViewModel.AuthorizedSharesCapitalViewModel.Remark = _organizationViewModel.Remark;

            }
        }

        public void GetOrganizationDefaultValues(OrganizationViewModel _organizationViewModel, string _entryStatus)
        {
            _organizationViewModel.EntryDateTime = DateTime.Now;
            _organizationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _organizationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _organizationViewModel.EntryStatus = _entryStatus;
            _organizationViewModel.UserAction = _entryStatus;
            
            if (_entryStatus != StringLiteralValue.Modify)
            {
                _organizationViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create)|| (_entryStatus == StringLiteralValue.Modify))
            {
                _organizationViewModel.Remark = "None";
            }
        }

        public void GetAuthorizedSharesCapitalDefaultValues(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string _entryStatus)
        {
            _authorizedSharesCapitalViewModel.EntryDateTime = DateTime.Now;
            _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _authorizedSharesCapitalViewModel.EntryStatus = _entryStatus;
            _authorizedSharesCapitalViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _authorizedSharesCapitalViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _authorizedSharesCapitalViewModel.Remark = "None";
            }
            else
            {
                _authorizedSharesCapitalViewModel.Remark = _authorizedSharesCapitalViewModel.Remark;

            }
        }

        public void GetContactDetailDefaultValues(OrganizationContactDetailViewModel _contactDetailViewModel, string _entryStatus)
        {
            _contactDetailViewModel.EntryDateTime = DateTime.Now;
            _contactDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _contactDetailViewModel.EntryStatus = _entryStatus;
            _contactDetailViewModel.UserAction = _entryStatus;

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _contactDetailViewModel.Remark = "None";
            }
        }

        public void GetGSTRegistrationDetailDefaultValues(OrganizationGSTRegistrationDetailViewModel _organizationGSTRegistrationDetailViewModel, string _entryStatus)
        {
            _organizationGSTRegistrationDetailViewModel.EntryDateTime = DateTime.Now;
            _organizationGSTRegistrationDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _organizationGSTRegistrationDetailViewModel.EntryStatus = _entryStatus;
            _organizationGSTRegistrationDetailViewModel.UserAction = _entryStatus;

            if (_entryStatus != StringLiteralValue.Modify)
            {
                _organizationGSTRegistrationDetailViewModel.ReasonForModification = "None";
            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _organizationGSTRegistrationDetailViewModel.Remark = "None";
            }
        }

        public void GetFundDefaultValues(OrganizationFundViewModel _organizationFundViewModel, string _entryStatus)
        {
            _organizationFundViewModel.ActivationStatus = StringLiteralValue.Active;
            _organizationFundViewModel.EntryDateTime = DateTime.Now;
            _organizationFundViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _organizationFundViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _organizationFundViewModel.EntryStatus = _entryStatus;
            _organizationFundViewModel.UserAction = _entryStatus;

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _organizationFundViewModel.Remark = "None";
            }
        }

        public void GetLoanTypeDefaultValues(OrganizationLoanTypeViewModel _loanTypeViewModel, string _entryStatus)
        {
            _loanTypeViewModel.ActivationStatus = StringLiteralValue.Active;
            _loanTypeViewModel.EntryDateTime = DateTime.Now;
            _loanTypeViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _loanTypeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _loanTypeViewModel.EntryStatus = _entryStatus;
            _loanTypeViewModel.UserAction = _entryStatus;

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _loanTypeViewModel.Remark = "None";
            }
        }

    }
}
