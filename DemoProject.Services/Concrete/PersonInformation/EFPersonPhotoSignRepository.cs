using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonPhotoSignRepository : IPersonPhotoSignRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;
        public EFPersonPhotoSignRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository, IPersonInformationDetailRepository _personInformationDetailRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonPhotoSignViewModel _personPhotoSignViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                if (result)
                {

                    if (personInformationParameterViewModel.PhotoDocumentUpload != "D" || personInformationParameterViewModel.SignDocumentUpload != "D")
                    {
                        PersonPhotoSignViewModel personPhotoSignViewModelForAmend = await personInformationDetailRepository.PhotoSignEntry(_personPhotoSignViewModel.PersonPrmKey, StringLiteralValue.Reject);

                        if (_personPhotoSignViewModel.PhotoPath != null || _personPhotoSignViewModel.SignPath != null)
                        {
                            personPhotoSignViewModelForAmend.PhotoPath = _personPhotoSignViewModel.PhotoPath;
                            personPhotoSignViewModelForAmend.SignPath = _personPhotoSignViewModel.SignPath;

                            //Photo
                            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                            {
                                if (_personPhotoSignViewModel.PhotoPath != null)
                                    result = personDbContextRepository.AttachPhotoDocumentInLocalStorage(_personPhotoSignViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Amend);
                                else
                                {
                                    _personPhotoSignViewModel.PhotoNameOfFile = personPhotoSignViewModelForAmend.PhotoNameOfFile;
                                    _personPhotoSignViewModel.PhotoLocalStoragePath = personPhotoSignViewModelForAmend.PhotoLocalStoragePath;
                                }
                            }
                            else
                                result = personDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_personPhotoSignViewModel, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);

                            //Sign
                            if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                            {
                                if (_personPhotoSignViewModel.SignPath != null)
                                    result = personDbContextRepository.AttachSignDocumentInLocalStorage(_personPhotoSignViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Amend);
                                else
                                {
                                    _personPhotoSignViewModel.SignNameOfFile = personPhotoSignViewModelForAmend.SignNameOfFile;
                                    _personPhotoSignViewModel.SignLocalStoragePath = personPhotoSignViewModelForAmend.SignLocalStoragePath;
                                }
                            }
                            else
                                result = personDbContextRepository.AttachSignDocumentInDatabaseStorage(_personPhotoSignViewModel, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);

                            result = personDbContextRepository.AttachPersonPhotoSignData(_personPhotoSignViewModel, StringLiteralValue.Amend);

                        }
                        else
                            result = personDbContextRepository.AttachPersonPhotoSignData(personPhotoSignViewModelForAmend, StringLiteralValue.Amend);

                    }
                }

                //Final Method
                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                if (result)
                {
                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    result = personDbContextRepository.DeleteOldLocalStorageDocument();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
        
      
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonPhotoSign ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<PersonPhotoSignViewModel> GetEntry(long _personPrmKey , string _entryType)
        {
            try
            {
                var a= await context.Database.SqlQuery<PersonPhotoSignViewModel>("SELECT * FROM dbo.GetPersonPhotoSignEntryByPersonPrmKey ( @PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<bool> Modify(PersonPhotoSignViewModel _personPhotoSignViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;
                // EnablePhotoDocumentUploadInLocalStorage
                if (result)
                {
                    if (personInformationParameterViewModel.PhotoDocumentUpload != "D")
                    {
                        if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                        {
                            if (_personPhotoSignViewModel.PhotoPath != null)
                                result = personDbContextRepository.AttachPhotoDocumentInLocalStorage(_personPhotoSignViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Create);
                            else
                            {
                                if (_personPhotoSignViewModel.PhotoNameOfFile == null)
                                    _personPhotoSignViewModel.PhotoNameOfFile = "None";
                                if (_personPhotoSignViewModel.PhotoLocalStoragePath == null)
                                    _personPhotoSignViewModel.PhotoLocalStoragePath = "None";
                            }
                        }
                        else
                        {
                            if (_personPhotoSignViewModel.PhotoPath != null)
                                result = personDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_personPhotoSignViewModel, null, StringLiteralValue.Create);
                            else
                            {
                                _personPhotoSignViewModel.PhotoNameOfFile = "None";
                                _personPhotoSignViewModel.PhotoLocalStoragePath = "None";
                            }
                        }
                    }
                }

                // EnableSignDocumentUploadInLocalStorage
                if (result)
                {
                    if (personInformationParameterViewModel.SignDocumentUpload != "D")
                    {
                        if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                        {
                            if (_personPhotoSignViewModel.SignPath != null)
                                result = personDbContextRepository.AttachSignDocumentInLocalStorage(_personPhotoSignViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Create);
                            else
                            {
                                if (_personPhotoSignViewModel.SignNameOfFile == null)
                                    _personPhotoSignViewModel.SignNameOfFile = "None";

                                if (_personPhotoSignViewModel.SignLocalStoragePath == null)
                                    _personPhotoSignViewModel.SignLocalStoragePath = "None";
                            }
                        }
                        else
                        {
                            if (_personPhotoSignViewModel.SignPath != null)
                                result = personDbContextRepository.AttachSignDocumentInDatabaseStorage(_personPhotoSignViewModel, null, StringLiteralValue.Create);
                            else
                            {
                                _personPhotoSignViewModel.SignNameOfFile = "None";
                                _personPhotoSignViewModel.SignLocalStoragePath = "None";
                            }
                        }
                    }
                }

                // EnablePhotoDocumentUpload
                if (result)
                {
                    if (personInformationParameterViewModel.PhotoDocumentUpload != "D" || personInformationParameterViewModel.SignDocumentUpload != "D")
                    {
                        if (personInformationParameterViewModel.PhotoDocumentUpload == "D")
                        {
                            _personPhotoSignViewModel.PhotoNameOfFile = "None";
                            _personPhotoSignViewModel.PhotoLocalStoragePath = "None";
                            _personPhotoSignViewModel.PhotoFileCaption = "None";

                        }

                        if (personInformationParameterViewModel.SignDocumentUpload == "D")
                        {
                            _personPhotoSignViewModel.SignNameOfFile = "None";
                            _personPhotoSignViewModel.SignLocalStoragePath = "None";
                            _personPhotoSignViewModel.SignFileCaption = "None";
                        }
                        result = personDbContextRepository.AttachPersonPhotoSignData(_personPhotoSignViewModel, StringLiteralValue.Create);
                    }
                }

                //Final Method
                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                if (result)
                {
                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    result = personDbContextRepository.DeleteOldLocalStorageDocument();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(PersonPhotoSignViewModel _personPhotoSignViewModel , string _entryType)
        {
            try
            {if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Record
                    PersonPhotoSignViewModel _personPhotoSignViewModelListForModify = await GetEntry(_personPhotoSignViewModel.PersonPrmKey,StringLiteralValue.Verify);
                    if (_personPhotoSignViewModelListForModify != null)
                    {
                        result = personDbContextRepository.AttachPersonPhotoSignData(_personPhotoSignViewModelListForModify, StringLiteralValue.Modify);
                    }
                }
                // Persorm Operaation As Per _entryType Record
                result = personDbContextRepository.AttachPersonPhotoSignData(_personPhotoSignViewModel, _entryType);

                //Final Method
                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonCommoditiesAssets
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject || u.EntryStatus == StringLiteralValue.Amend) && u.PersonPrmKey == personPrmKey)
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
    }
}
