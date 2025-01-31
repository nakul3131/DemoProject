using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation.Parameter
{
    public class EFPersonInformationParameterDetailRepository : IPersonInformationParameterDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public EFPersonInformationParameterDetailRepository(RepositoryConnection _connection, IManagementDetailRepository _managementDetailRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            context = _connection.EFDbContext;
            managementDetailRepository = _managementDetailRepository;
            personDetailRepository = _personDetailRepository;
            securityDetailRepository = _securityDetailRepository;
        }

        public PersonInformationParameterViewModel GetDocumentValidations()
        {
            return GetPersonInformationParameterActiveEntry();
        }

        public PersonInformationParameterViewModel GetPersonInformationParameterActiveEntry()
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = context.Database.SqlQuery<PersonInformationParameterViewModel>("SELECT * FROM dbo.GetPersonInformationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefault();

                // Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForDatabase From (String) PhotoDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnablePhotoDocumentUploadInDb)
                {
                    personInformationParameterViewModel.PhotoDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForLocalStorage From (String) PhotoDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.PhotoDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForDatabase From (String) SignDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableSignDocumentUploadInDb)
                {
                    personInformationParameterViewModel.SignDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.SignDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForLocalStorage From (String) SignDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.SignDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.SignDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) KYCDocumentFormatTypeIdForDatabase From (String) KYCDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableKYCDocumentUploadInDb)
                {
                    personInformationParameterViewModel.KYCDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) KYCDocumentFormatTypeIdForLocalStorage From (String) KYCDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.KYCDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) BankDetailFormatTypeIdForDatabase From (String) BankStatementAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInDb)
                {
                    personInformationParameterViewModel.BankDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) BankDetailFormatTypeIdForLocalStorage From (String) BankStatementAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.BankDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) GSTDetailFormatTypeIdForDatabase From (String) GSTDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableGSTDocumentUploadInDb)
                {
                    personInformationParameterViewModel.GSTDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) GSTDetailFormatTypeIdForLocalStorage From (String) GSTDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.GSTDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) IncomeTaxDocumentFormatTypeIdForDatabase From (String) IncomeTaxDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInDb)
                {
                    personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) IncomeTaxDocumentFormatTypeIdForLocalStorage From (String) IncomeTaxDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) FinancialAssetDocumentFormatTypeIdForDatabase From (String) FinancialAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) FinancialAssetDocumentFormatTypeIdForLocalStorage From (String) FinancialAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) AgricultureAssetFormatTypeIdForDatabase From (String) AgricultureAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) AgricultureAssetFormatTypeIdForLocalStorage From (String) AgricultureAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) ImmovableAssetFormatTypeIdForDatabase From (String) ImmovableAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) ImmovableAssetFormatTypeIdForLocalStorage From (String) ImmovableAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MovableAssetFormatTypeIdForDatabase From (String) MovableAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MovableAssetFormatTypeIdForLocalStorage From (String) MovableAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MachineryAssetFormatTypeIdForDatabase From (String) MachineryAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MachineryAssetFormatTypeIdForLocalStorage From (String) MachineryAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) DeathFormatTypeIdForDatabase From (String) DeathDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableDeathDocumentUploadInDb)
                {
                    personInformationParameterViewModel.DeathDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) DeathFormatTypeIdForLocalStorage From (String) DeathDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableDeathDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.DeathDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                personInformationParameterViewModel.MaskTypeCharacter = personInformationParameterViewModel.PersonInformationNumberMask.Split(',');

                return personInformationParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterViewModel>> GetPersonInformationParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterViewModel>("SELECT * FROM dbo.GetPersonInformationParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonInformationParameterViewModel> GetPersonInformationParameterEntry(string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await context.Database.SqlQuery<PersonInformationParameterViewModel>("SELECT * FROM dbo.GetPersonInformationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", _entryType)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForDatabase From (String) PhotoDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnablePhotoDocumentUploadInDb)
                {
                    personInformationParameterViewModel.PhotoDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) PhotoDocumentFormatTypeIdForLocalStorage From (String) PhotoDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.PhotoDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForDatabase From (String) SignDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableSignDocumentUploadInDb)
                {
                    personInformationParameterViewModel.SignDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.SignDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) SignDocumentFormatTypeIdForLocalStorage From (String) SignDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.SignDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.SignDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) KYCDocumentFormatTypeIdForDatabase From (String) KYCDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableKYCDocumentUploadInDb)
                {
                    personInformationParameterViewModel.KYCDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) KYCDocumentFormatTypeIdForLocalStorage From (String) KYCDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.KYCDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) BankDetailFormatTypeIdForDatabase From (String) BankStatementAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInDb)
                {
                    personInformationParameterViewModel.BankDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) BankDetailFormatTypeIdForLocalStorage From (String) BankStatementAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.BankDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) GSTDetailFormatTypeIdForDatabase From (String) GSTDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableGSTDocumentUploadInDb)
                {
                    personInformationParameterViewModel.GSTDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) GSTDetailFormatTypeIdForLocalStorage From (String) GSTDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.GSTDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) IncomeTaxDocumentFormatTypeIdForDatabase From (String) IncomeTaxDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInDb)
                {
                    personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) IncomeTaxDocumentFormatTypeIdForLocalStorage From (String) IncomeTaxDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) FinancialAssetDocumentFormatTypeIdForDatabase From (String) FinancialAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) FinancialAssetDocumentFormatTypeIdForLocalStorage From (String) FinancialAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) AgricultureAssetFormatTypeIdForDatabase From (String) AgricultureAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) AgricultureAssetFormatTypeIdForLocalStorage From (String) AgricultureAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) ImmovableAssetFormatTypeIdForDatabase From (String) ImmovableAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) ImmovableAssetFormatTypeIdForLocalStorage From (String) ImmovableAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MovableAssetFormatTypeIdForDatabase From (String) MovableAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MovableAssetFormatTypeIdForLocalStorage From (String) MovableAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MachineryAssetFormatTypeIdForDatabase From (String) MachineryAssetDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInDb)
                {
                    personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MachineryAssetFormatTypeIdForLocalStorage From (String) MachineryAssetDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) DeathFormatTypeIdForDatabase From (String) DeathDocumentAllowedFileFormatsForDb)
                if (personInformationParameterViewModel.EnableDeathDocumentUploadInDb)
                {
                    personInformationParameterViewModel.DeathDocumentFormatTypeIdForDatabase = personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForDb.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) DeathFormatTypeIdForLocalStorage From (String) DeathDocumentAllowedFileFormatsForLocalStorage)
                if (personInformationParameterViewModel.EnableDeathDocumentUploadInLocalStorage)
                {
                    personInformationParameterViewModel.DeathDocumentFormatTypeIdForLocalStorage = personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForLocalStorage.Split(',');
                }

                // Get Multiselect Id's From String (i.e. (Array) MaskTypeCharacter From (String) PersonInformationNumberMask)
                personInformationParameterViewModel.MaskTypeCharacter = personInformationParameterViewModel.PersonInformationNumberMask.Split(',');

                return personInformationParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterDocumentTypeViewModel>> GetPersonInformationParameterDocumentTypeEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterDocumentTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterDocumentTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonInformationParameterDocumentTypeViewModel> GetPersonInformationParameterDocumentTypeEntry(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterDocumentTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterDocumentTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterNoticeTypeViewModel>> GetPersonInformationParameterNoticeTypeEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterNoticeTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterNoticeTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonInformationParameterNoticeTypeViewModel> GetPersonInformationParameterNoticeTypeEntry(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterNoticeTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterNoticeTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void GetPersonInformationParameterAllDefaultValues(PersonInformationParameterViewModel _personInformationParameterViewModel, string _entryType)
        {
            // Set Default Value
            _personInformationParameterViewModel.EntryDateTime = DateTime.Now;
            _personInformationParameterViewModel.EntryStatus = _entryType;
            _personInformationParameterViewModel.ReasonForModification = "None";
            _personInformationParameterViewModel.UserAction = _entryType;
            _personInformationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _personInformationParameterViewModel.Remark = "None";
            // Photo Upload - Default Values
            if (_personInformationParameterViewModel.PhotoDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnablePhotoDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.PhotoDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.PhotoDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.PhotoDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.PhotoDocumentAllowedFileFormatsForDb = "None";
            }

            // Sign Upload - Default Values
            if (_personInformationParameterViewModel.SignDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableSignDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.SignDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.SignDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.SignDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.SignDocumentAllowedFileFormatsForDb = "None";
            }

            // KYC Document Upload - Default Values
            if (_personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableKYCDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.KYCDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.KYCDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.KYCDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.KYCDocumentAllowedFileFormatsForDb = "None";
            }

            // Bank Document Upload - Default Values]

            if (_personInformationParameterViewModel.EnableBankingDetail == true && _personInformationParameterViewModel.BankStatementDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableBankStatementDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.BankDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.BankDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.BankStatementDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.BankStatementDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.BankStatementDocumentLocalStoragePath = "None";
            }


            // GST Document Upload - Default Values
            if (_personInformationParameterViewModel.EnableGSTRegistration == true && _personInformationParameterViewModel.GSTDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableGSTDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.GSTDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.GSTDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.GSTDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.GSTDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.GSTDocumentLocalStoragePath = "None";

            }



            // Income Tax Document Upload - Default Values

            if (_personInformationParameterViewModel.EnableIncomeTaxDetail == true && _personInformationParameterViewModel.IncomeTaxDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.IncomeTaxDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.IncomeTaxDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.IncomeTaxDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath = "None";
            }


            // Financial Asset Document Upload - Default Values

            if (_personInformationParameterViewModel.EnableFinancialAsset == true && _personInformationParameterViewModel.FinancialAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.FinancialAssetDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.FinancialAssetDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.FinancialAssetDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath = "None";
            }


            // AgricultureAsset Document Upload - Default Values
            if (_personInformationParameterViewModel.EnableAgricultureAsset == true && _personInformationParameterViewModel.AgricultureAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.AgricultureAssetDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.AgricultureAssetDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.AgricultureAssetDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath = "None";
            }

            // ImmovableAsset Document Upload - Default Values

            if (_personInformationParameterViewModel.EnableImmovableAsset == true && _personInformationParameterViewModel.ImmovableAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.ImmovableAssetDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.ImmovableAssetDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForLocalStorage = "None1";
                _personInformationParameterViewModel.ImmovableAssetDocumentAllowedFileFormatsForDb = "None1";
                _personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath = "None1";
            }

            // MovableAsset Document Upload - Default Values

            if (_personInformationParameterViewModel.EnableMovableAsset == true && _personInformationParameterViewModel.MovableAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableMovableAssetDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.MovableAssetDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.MovableAssetDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.MovableAssetDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath = "None";
            }

            // MachineryAsset Document Upload - Default Values

            if (_personInformationParameterViewModel.EnableMachineryAsset == true && _personInformationParameterViewModel.MachineryAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.MachineryAssetDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.MachineryAssetDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.MachineryAssetDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath = "None";
            }

            // Death Document Upload - Default Values
            if (_personInformationParameterViewModel.DeathDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableDeathDocumentUploadInDb)
                {
                    _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForDb = string.Join(",", _personInformationParameterViewModel.DeathDocumentFormatTypeIdForDatabase);
                    _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForLocalStorage = "None";
                }
                else
                {
                    _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForLocalStorage = string.Join(",", _personInformationParameterViewModel.DeathDocumentFormatTypeIdForLocalStorage);
                    _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForDb = "None";
                }
            }
            else
            {
                _personInformationParameterViewModel.DeathDocumentUpload = StringLiteralValue.Disable;
                _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForLocalStorage = "None";
                _personInformationParameterViewModel.DeathDocumentAllowedFileFormatsForDb = "None";
                _personInformationParameterViewModel.DeathDocumentLocalStoragePath = "None";
            }

            // Person Information Number Mask
            if (_personInformationParameterViewModel.EnableAutoGeneratePersonInformationNumber == true)
            {
                _personInformationParameterViewModel.PersonInformationNumberMask = string.Join(",", _personInformationParameterViewModel.MaskTypeCharacter);
                // Get PrmKey By Id Of All Dropdowns
                _personInformationParameterViewModel.ChecksumAlgorithmPrmKey = securityDetailRepository.GetChecksumAlgorithmPrmKeyById(_personInformationParameterViewModel.ChecksumAlgorithmId);
            }
            else
            {
                _personInformationParameterViewModel.PersonInformationNumberMask = "None";
                _personInformationParameterViewModel.ChecksumAlgorithmPrmKey = 0;
            }

            // Assign Values According To Entry Type
            if (_entryType == StringLiteralValue.Amend)
            {
                _personInformationParameterViewModel.PrmKey = _personInformationParameterViewModel.PersonInformationParameterPrmKey;
            }

            if (_personInformationParameterViewModel.EnableMobileOTPForVerification == false)
            {
                _personInformationParameterViewModel.VerificationMobileOTPDataType = "NUM";
                _personInformationParameterViewModel.PrefixStringForVerificationMobileOTP = "abcd";
                _personInformationParameterViewModel.PostfixStringForVerificationMobileOTP = "xyz";
                _personInformationParameterViewModel.IncludedCharactersForVerificationMobileOTP = "!@#$%";
                _personInformationParameterViewModel.ExcludedCharactersForVerificationMobileOTP = "+_)(";

            }
        }

        public void GetPersonInformationParameterDocumentTypeDefaultValues(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel, string _entryType, byte _personInformationParameterPrmKey)
        {
            // Set Default Value
            _personInformationParameterDocumentTypeViewModel.EntryDateTime = DateTime.Now;
            _personInformationParameterDocumentTypeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _personInformationParameterDocumentTypeViewModel.EntryStatus = _entryType;
            _personInformationParameterDocumentTypeViewModel.UserAction = _entryType;

            // Get PrmKey By Id Of All Dropdowns
            _personInformationParameterDocumentTypeViewModel.DocumentTypePrmKey = personDetailRepository.GetDocumentTypePrmKeyById(_personInformationParameterDocumentTypeViewModel.DocumentTypeId);

            if (_entryType == StringLiteralValue.Create)
            {
                _personInformationParameterDocumentTypeViewModel.Remark = "None";
                _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey = _personInformationParameterPrmKey;
                _personInformationParameterDocumentTypeViewModel.PrmKey = 0;
                _personInformationParameterDocumentTypeViewModel.PersonInformationParameterDocumentTypePrmKey = 0;
            }
        }

        public void GetPersonInformationParameterNoticeDefaultValues(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel, string _entryType, byte _personInformationParameterPrmKey)
        {
            // Set Default Value
            _personInformationParameterNoticeTypeViewModel.EntryDateTime = DateTime.Now;
            _personInformationParameterNoticeTypeViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
            _personInformationParameterNoticeTypeViewModel.EntryStatus = _entryType;
            _personInformationParameterNoticeTypeViewModel.UserAction = _entryType;

            // Get PrmKey By Id Of All Dropdowns
            _personInformationParameterNoticeTypeViewModel.NoticeTypePrmKey = managementDetailRepository.GetNoticeTypePrmKeyById(_personInformationParameterNoticeTypeViewModel.NoticeTypeId);

            if (_entryType == StringLiteralValue.Create)
            {
                _personInformationParameterNoticeTypeViewModel.Remark = "None";
                _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey = _personInformationParameterPrmKey;
                _personInformationParameterNoticeTypeViewModel.PrmKey = 0;
                _personInformationParameterNoticeTypeViewModel.PersonInformationParameterNoticeTypePrmKey = 0;
            }
        }

        public bool IsEnableSMSAlert()
        {
            return context.PersonInformationParameters
                    .Where(p => p.EntryStatus == StringLiteralValue.Verify)
                    .Select(p => p.EnableSMSAlert).FirstOrDefault();
        }

        public bool IsRequiredKYCDocumentUpload()
        {
            string docUpload = context.PersonInformationParameters
                                .Where(p => p.EntryStatus == StringLiteralValue.Verify)
                                .Select(p => p.KYCDocumentUpload).FirstOrDefault();

            if (docUpload == StringLiteralValue.Disable)
                return false;
            else
                return true;
        }
    }
}
