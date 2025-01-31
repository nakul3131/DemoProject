using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFOfficeDetailRepository : IOfficeDetailRepository
    {
        private readonly EFDbContext context;

        public EFOfficeDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }


        public async Task<BusinessOfficeDetailViewModel> GetBusinessOfficeDetailEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                BusinessOfficeDetailViewModel businessOfficeDetailViewModel = await context.Database.SqlQuery<BusinessOfficeDetailViewModel>("SELECT * FROM dbo.GetBusinessOfficeDetailEntryByBusinessOfficePrmKey ( @BusinessOfficePrmkey, @EntriesType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return businessOfficeDetailViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeCoopRegistrationViewModel> GetCoopRegistrationEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeCoopRegistrationViewModel>("SELECT * FROM dbo.GetBusinessOfficeCoopRegistrationEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeCustomerNumberViewModel> GetCustomerNumberEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeCustomerNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeCustomerNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeMemberNumberViewModel> GetMemberNumberEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {

                BusinessOfficeMemberNumberViewModel businessOfficeMemberNumberViewModel = await context.Database.SqlQuery<BusinessOfficeMemberNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeMemberNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeMemberNumberViewModel.MaskTypeCharacterForMember = businessOfficeMemberNumberViewModel.MemberNumberMask.Split(',');

                return businessOfficeMemberNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeRBIRegistrationViewModel> GetRBIRegistrationEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficeRBIRegistrationViewModel>("SELECT * FROM dbo.GetBusinessOfficeRBIRegistrationEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeTransactionParameterViewModel> GetTransactionParameterEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                BusinessOfficeTransactionParameterViewModel businessOfficeTransactionParameterViewModel = await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask = businessOfficeTransactionParameterViewModel.TransactionNumberMask.Split(',');

                return businessOfficeTransactionParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeSharesCertificateNumberViewModel> GetSharesCertificateNumberEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                BusinessOfficeSharesCertificateNumberViewModel businessOfficeSharesCertificateNumberViewModel = await context.Database.SqlQuery<BusinessOfficeSharesCertificateNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeSharesCertificateNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeSharesCertificateNumberViewModel.MaskTypeCharacterForSharesCertificate = businessOfficeSharesCertificateNumberViewModel.SharesCertificateNumberMask.Split(',');

                return businessOfficeSharesCertificateNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<BusinessOfficePersonInformationNumberViewModel> GetPersonInformationNumberEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                BusinessOfficePersonInformationNumberViewModel businessOfficePersonInformationNumberViewModel = await context.Database.SqlQuery<BusinessOfficePersonInformationNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficePersonInformationNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficePersonInformationNumberViewModel.MaskTypeCharacterForPerson = businessOfficePersonInformationNumberViewModel.PersonInformationNumberMask.Split(',');

                return businessOfficePersonInformationNumberViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        public async Task<IEnumerable<BusinessOfficeAccountNumberViewModel>> GetAccountNumberEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficeAccountNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeAccountNumberEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<BusinessOfficeAgreementNumberViewModel>> GetAgreementNumberEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficeAgreementNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeAgreementNumberEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeApplicationNumberViewModel>> GetApplicationNumberEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeApplicationNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeApplicationNumberEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeCurrencyViewModel>> GetCurrencyEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeCurrencyViewModel>("SELECT * FROM dbo.GetBusinessOfficeCurrencyEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeDepositCertificateNumberViewModel>> GetDepositCertificateNumberEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficeDepositCertificateNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeDepositCertificateNumberEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficePassbookNumberViewModel>> GetPassbookNumberEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficePassbookNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficePassbookNumberEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeMenuViewModel>> GetMenuEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                IEnumerable<BusinessOfficeMenuViewModel> businessOfficeMenuViewModels = await context.Database.SqlQuery<BusinessOfficeMenuViewModel>("SELECT * FROM dbo.GetBusinessOfficeMenuEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return businessOfficeMenuViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficePasswordPolicyViewModel>> GetPasswordPolicyEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<BusinessOfficePasswordPolicyViewModel>("SELECT * FROM dbo.GetBusinessOfficePasswordPolicyEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeSpecialPermissionViewModel>("SELECT * FROM dbo.GetBusinessOfficeSpecialPermissionEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeTransactionLimitViewModel>> GetTransactionLimitEntries(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeTransactionLimitViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionLimitEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        public async Task<BusinessOfficePasswordPolicyViewModel> GetPasswordPolicyEntry(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficePasswordPolicyViewModel>("SELECT * FROM dbo.GetBusinessOfficePasswordPolicyEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<BusinessOfficeViewModel>> GetCooperativeEntriesForOperation(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntriesForCoopRegistrationAddUpdate ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeViewModel>> GetBusinessOfficeDetailEntriesForOperation(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntriesForDetailAddUpdate ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeViewModel>> GetRBIRegistrationEntriesForOperation(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntriesForRBIRegistrationAddUpdate ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeViewModel>> GetPasswordPolicyEntriesForOperation(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntriesForPasswordPolicyAddUpdate(@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        public void GetBusinessOfficeAllDefaultValues(BusinessOfficeViewModel _businessOfficeViewModel, string _entryStatus)
        {
            _businessOfficeViewModel.ActivationStatus = StringLiteralValue.Active;
            _businessOfficeViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _businessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.UserAction = _entryStatus;

            ////BusinessOfficeCustomerNumberViewModel
            //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.EntryDateTime = DateTime.Now;
            //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.EntryStatus = _entryStatus;
            //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.UserAction = _entryStatus;

            //BusinessOfficeCoopRegistrationViewModel
            _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.UserAction = _entryStatus;

            //BusinessOfficeDetailViewModel
            _businessOfficeViewModel.BusinessOfficeDetailViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.BusinessOfficeDetailViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _businessOfficeViewModel.BusinessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.BusinessOfficeDetailViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.BusinessOfficeDetailViewModel.UserAction = _entryStatus;

            //BusinessOfficeMemberNumberViewModel
            _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.UserAction = _entryStatus;

            //BusinessOfficeRBIRegistrationViewModel
            _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
            _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.UserAction = _entryStatus;

            //BusinessOfficeTransactionParameterViewModel
            _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EntryDateTime = DateTime.Now;
            _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EntryStatus = _entryStatus;
            _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.UserAction = _entryStatus;


            if (_entryStatus != StringLiteralValue.Modify)
            {
                _businessOfficeViewModel.ReasonForModification = "None";
                // _businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.ReasonForModification = "None";
                _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeViewModel.BusinessOfficeDetailViewModel.ReasonForModification = "None";
                _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.ReasonForModification = "None";
                _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.ReasonForModification = "None";
                _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.ReasonForModification = "None";

            }

            if ((_entryStatus == StringLiteralValue.Create) || (_entryStatus == StringLiteralValue.Modify))
            {
                _businessOfficeViewModel.Remark = "None";
                //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.Remark = "None";
                _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.Remark = "None";
                _businessOfficeViewModel.BusinessOfficeDetailViewModel.Remark = "None";
                _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.Remark = "None";
                _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.Remark = "None";
                _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.Remark = "None";

            }
            else
            {
                //_businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel.Remark = _businessOfficeViewModel.Remark;
                _businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel.Remark = _businessOfficeViewModel.Remark;
                _businessOfficeViewModel.BusinessOfficeDetailViewModel.Remark = _businessOfficeViewModel.Remark;
                _businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.Remark = _businessOfficeViewModel.Remark;
                _businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel.Remark = _businessOfficeViewModel.Remark;
                _businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.Remark = _businessOfficeViewModel.Remark;

            }
        }

        
    }
}
