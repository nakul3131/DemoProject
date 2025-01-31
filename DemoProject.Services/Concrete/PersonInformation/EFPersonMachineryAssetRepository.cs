using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonMachineryAssetRepository : IPersonMachineryAssetRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonMachineryAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonMachineryAssetViewModel _personMachineryAssetViewModel)
        {
          

            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (result)
                {
                    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModelListForAmend = await GetEntries(_personMachineryAssetViewModel.PersonPrmKey,StringLiteralValue.Reject);

                    if (personMachineryAssetViewModelListForAmend != null)
                    {
                        foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonMachineryAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened 
                    if (personInformationParameterViewModel.EnableMachineryAsset == true)
                    {
                        List<PersonMachineryAssetViewModel> personMachineryAssetViewModelList = (List<PersonMachineryAssetViewModel>)HttpContext.Current.Session["MachineryAsset"];

                        if (personMachineryAssetViewModelList != null)
                        {
                            foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelList)
                            {
                                viewModel.Remark = _personMachineryAssetViewModel.Remark;
                                viewModel.PersonPrmKey = _personMachineryAssetViewModel.PersonPrmKey;

                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.MachineryAssetDocumentUpload != "D")
                                {
                                    // EnableMachineryAssetDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathMachinery != null)
                                        {
                                                result = personDbContextRepository.AttachMachineryAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;

                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    else
                                    {
                                        if (viewModel.PhotoPathMachinery != null)
                                        {
                                            result = personDbContextRepository.AttachMachineryAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }
                                    result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonMachineryAssetViewModel _personMachineryAssetViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                if (personInformationParameterViewModel.EnableMachineryAsset == true)
                {
                    List<PersonMachineryAssetViewModel> personMachineryAssetViewModelList = (List<PersonMachineryAssetViewModel>)HttpContext.Current.Session["MachineryAsset"];

                    if (personMachineryAssetViewModelList != null)
                    {
                        foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelList)
                        {
                            viewModel.Remark = _personMachineryAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personMachineryAssetViewModel.PersonPrmKey;

                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.MachineryAssetDocumentUpload != "D")
                            {
                                // EnableMachineryAssetDocumentUploadInLocalStorage
                                if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathMachinery != null)
                                    {
                                        result = personDbContextRepository.AttachMachineryAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                else
                                {
                                    if (viewModel.PhotoPathMachinery != null)
                                        result = personDbContextRepository.AttachMachineryAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                }

                                result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModelListForModify = await GetEntries(_personMachineryAssetViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personMachineryAssetViewModelListForModify != null)
                    {
                        foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Modify);

                            if (viewModel.PersonMachineryAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                            }
                        }
                    }
                }

                // Verify Record
                // Set Default Value
                List<PersonMachineryAssetViewModel> personMachineryAssetViewModelList = new List<PersonMachineryAssetViewModel>();
                personMachineryAssetViewModelList = (List<PersonMachineryAssetViewModel>)HttpContext.Current.Session["MachineryAsset"];

                if (personMachineryAssetViewModelList != null)
                {
                    foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelList)
                    {
                        viewModel.Remark = _personMachineryAssetViewModel.Remark;
                        viewModel.PersonPrmKey = _personMachineryAssetViewModel.PersonPrmKey;

                        result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, _entryType);

                        if (viewModel.PersonMachineryAssetDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);

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



        //Get Verified Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonMachineryAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonMachineryAssetViewModel>> GetEntries(long _personPrmKey , string _entryType)
        {
            try
            {
                IEnumerable<PersonMachineryAssetViewModel> PersonMachineryAssetViewModels = await context.Database.SqlQuery<PersonMachineryAssetViewModel>("SELECT * FROM dbo.GetPersonMachineryAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonMachineryAssetViewModel viewModel in PersonMachineryAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathMachinery = objFile;

                }
                return PersonMachineryAssetViewModels;
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
            int count = await context.PersonMachineryAssets
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
