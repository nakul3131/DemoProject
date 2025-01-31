using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonFinancialAssetRepository : IPersonFinancialAssetRepository
    {
        private readonly EFDbContext context;

        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonFinancialAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        public async Task<bool> Amend(PersonFinancialAssetViewModel _personFinancialAssetViewModel)
        {

            try
            {
                bool result = true;

                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModelListForAmend = await GetEntries(_personFinancialAssetViewModel.PersonPrmKey, StringLiteralValue.Reject);

                if (personFinancialAssetViewModelListForAmend != null)
                {
                    foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelListForAmend)
                    {
                        result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, StringLiteralValue.Amend);

                        if (viewModel.PersonFinancialAssetDocumentPrmKey > 0)
                            result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);

                    }
                }


                if (result)
                {
                    if (personInformationParameterViewModel.EnableFinancialAsset == true)
                    {
                        List<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = (List<PersonFinancialAssetViewModel>)HttpContext.Current.Session["FinancialAsset"];

                        foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                        {
                            viewModel.Remark = _personFinancialAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personFinancialAssetViewModel.PersonPrmKey;

                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            if (personFinancialAssetViewModelList != null)
                            {
                                result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.FinancialAssetDocumentUpload != "D")
                                {
                                    // EnableFinancialAssetDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
                                        if (viewModel.PhotoPathFinance != null)
                                        {
                                            result = personDbContextRepository.AttachFinancialAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;

                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    else
                                    {
                                        if (viewModel.PhotoPathFinance != null)
                                        {
                                            result = personDbContextRepository.AttachFinancialAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(PersonFinancialAssetViewModel _personFinancialAssetViewModel)
        {
            try
            {

                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                if (result)
                {
                    if (personInformationParameterViewModel.EnableFinancialAsset == true)
                    {
                        List<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = (List<PersonFinancialAssetViewModel>)HttpContext.Current.Session["FinancialAsset"];

                        foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                        {
                            viewModel.Remark = _personFinancialAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personFinancialAssetViewModel.PersonPrmKey;

                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;


                            result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.FinancialAssetDocumentUpload != "D")
                            {
                                // EnableFinancialAssetDocumentUploadInLocalStorage
                                if (personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInLocalStorage == true)
                                {
                                    //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
                                    if (viewModel.PhotoPathFinance != null)
                                    {
                                        result = personDbContextRepository.AttachFinancialAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                else
                                {
                                    if (viewModel.PhotoPathFinance != null)
                                    {
                                        result = personDbContextRepository.AttachFinancialAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                            }

                        }
                    }
                }


                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                bool result = true;
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModelListForModify = await GetEntries(_personFinancialAssetViewModel.PersonPrmKey, StringLiteralValue.Verify);

                    if (personFinancialAssetViewModelListForModify != null)
                    {
                        foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, StringLiteralValue.Modify);

                            if (viewModel.PersonFinancialAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);

                        }
                    }
                }
                // Verify Record
                // Set Default Value
                List<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = new List<PersonFinancialAssetViewModel>();
                personFinancialAssetViewModelList = (List<PersonFinancialAssetViewModel>)HttpContext.Current.Session["FinancialAsset"];

                if (personFinancialAssetViewModelList != null)
                {
                    foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                    {
                        viewModel.Remark = _personFinancialAssetViewModel.Remark;
                        viewModel.PersonPrmKey = _personFinancialAssetViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, _entryType);

                        if (viewModel.PersonFinancialAssetDocumentPrmKey > 0)
                            result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);

                    }
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //Get Verified Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonFinancialAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Verified Entries By PersonPrmKey
        public async Task<IEnumerable<PersonFinancialAssetViewModel>> GetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = await context.Database.SqlQuery<PersonFinancialAssetViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathFinance = objFile;

                }
                return personFinancialAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonFinancialAssets
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject) && u.PersonPrmKey == personPrmKey)
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
