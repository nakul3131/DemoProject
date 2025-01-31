using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonImmovableAssetRepository : IPersonImmovableAssetRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonImmovableAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;
        public async Task<bool> Amend(PersonImmovableAssetViewModel _personImmovableAssetViewModel)
        {
          
            try
            {

                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                if (result)
                {
                    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModelListForAmend = await GetEntries(_personImmovableAssetViewModel.PersonPrmKey,StringLiteralValue.Reject);
                    if (personImmovableAssetViewModelListForAmend != null)
                    {
                        foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonImmovableAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);

                        }
                    }

                    if (personInformationParameterViewModel.EnableImmovableAsset == true)
                    {
                        List<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = (List<PersonImmovableAssetViewModel>)HttpContext.Current.Session["ImmovableAsset"];

                        if (personImmovableAssetViewModelList != null)
                        {
                            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                            {
                                viewModel.Remark = _personImmovableAssetViewModel.Remark;
                                viewModel.PersonPrmKey = _personImmovableAssetViewModel.PersonPrmKey;

                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                if (personImmovableAssetViewModelList != null)
                                {
                                    result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, StringLiteralValue.Create);
                                    if (personInformationParameterViewModel.ImmovableAssetDocumentUpload != "D")
                                    {

                                        // EnableImmovableAssetDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInLocalStorage == true)
                                        {
                                            if (viewModel.PhotoPathImmovable != null)
                                            {
                                                result = personDbContextRepository.AttachImmovableAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }
                                        else
                                        {
                                            if (viewModel.PhotoPathImmovable != null)
                                            {
                                                result = personDbContextRepository.AttachImmovableAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }


                                        }
                                        result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                    }
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
        
        public async Task<bool> Modify(PersonImmovableAssetViewModel _personImmovableAssetViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                

                if (personInformationParameterViewModel.EnableImmovableAsset == true)
                {
                    List<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = (List<PersonImmovableAssetViewModel>)HttpContext.Current.Session["ImmovableAsset"];

                    if (personImmovableAssetViewModelList != null)
                    {
                        foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                        {
                            viewModel.Remark = _personImmovableAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personImmovableAssetViewModel.PersonPrmKey;

                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            if (personImmovableAssetViewModelList != null)
                            {
                                result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, StringLiteralValue.Create);
                                if (personInformationParameterViewModel.ImmovableAssetDocumentUpload != "D")
                                {

                                    // EnableImmovableAssetDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathImmovable != null)
                                        {
                                                result = personDbContextRepository.AttachImmovableAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }
                                    else
                                    {
                                        if (viewModel.PhotoPathImmovable != null)
                                        {
                                            result = personDbContextRepository.AttachImmovableAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }


                                    }
                                    result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonImmovableAssetViewModel _personImmovableAssetViewModel,string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModelListForModify = await GetEntries(_personImmovableAssetViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, StringLiteralValue.Modify);

                        if (viewModel.PersonImmovableAssetDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                        }
                    }
                }
                // Verify Record
                // Set Default Value
                List<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = new List<PersonImmovableAssetViewModel>();
                personImmovableAssetViewModelList = (List<PersonImmovableAssetViewModel>)HttpContext.Current.Session["ImmovableAsset"];

                if (personImmovableAssetViewModelList != null)
                {
                    foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                    {
                        viewModel.Remark = _personImmovableAssetViewModel.Remark;
                        viewModel.PersonPrmKey = _personImmovableAssetViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, _entryType);

                        if (viewModel.PersonImmovableAssetDocumentPrmKey > 0)
                            result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);

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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonImmovableAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonImmovableAssetViewModel>> GetEntries(long _personPrmKey,string _entryType)
        {
            try
            {
                IEnumerable<PersonImmovableAssetViewModel> PersonImmovableAssetViewModels = await context.Database.SqlQuery<PersonImmovableAssetViewModel>("SELECT * FROM dbo.GetPersonImmovableAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonImmovableAssetViewModel viewModel in PersonImmovableAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathImmovable = objFile;

                }
                return PersonImmovableAssetViewModels;
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
            int count = await context.PersonImmovableAssets
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
