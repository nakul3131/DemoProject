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
    public class EFPersonMovableAssetRepository : IPersonMovableAssetRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonMovableAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonMovableAssetViewModel _personMovableAssetViewModel)
        {
           

            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModelListForAmend = await GetEntries(_personMovableAssetViewModel.PersonPrmKey,StringLiteralValue.Reject);

                    if (personMovableAssetViewModelListForAmend != null)
                    {
                        foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonMovableAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                        }
                    }

                    //New Record Create For Amend
                    if (personInformationParameterViewModel.EnableMovableAsset == true)
                    {
                        // Insert Record From Session Object
                        List<PersonMovableAssetViewModel> personMovableAssetViewModelList = (List<PersonMovableAssetViewModel>)HttpContext.Current.Session["MovableAsset"];

                        if (personMovableAssetViewModelList != null)
                        {
                            foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelList)
                            {
                                viewModel.Remark = _personMovableAssetViewModel.Remark;
                                viewModel.PersonPrmKey = _personMovableAssetViewModel.PersonPrmKey;

                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Create);
                                if (personInformationParameterViewModel.MovableAssetDocumentUpload != "D")
                                {

                                    if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathMovable != null)
                                        {
                                                result = personDbContextRepository.AttachMovableAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }
                                    else
                                    {
                                        if (viewModel.PhotoPathMovable != null)
                                        {
                                            result = personDbContextRepository.AttachMovableAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }
                                    result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);

                                }
                            }
                        }
                    }
                }

                if (result)
                    result = await personDbContextRepository.SaveData();
                return result; 
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(PersonMovableAssetViewModel _personMovableAssetViewModel)
        {

            try
            {
                bool result = true;
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);


                //New Record Create For Amend
                if (personInformationParameterViewModel.EnableMovableAsset == true)
                {
                    // Insert Record From Session Object
                    List<PersonMovableAssetViewModel> personMovableAssetViewModelList = (List<PersonMovableAssetViewModel>)HttpContext.Current.Session["MovableAsset"];

                    if (personMovableAssetViewModelList != null)
                    {
                        foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelList)
                        {
                            viewModel.Remark = _personMovableAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personMovableAssetViewModel.PersonPrmKey;

                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Create);
                            if (personInformationParameterViewModel.MovableAssetDocumentUpload != "D")
                            {

                                if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathMovable != null)
                                    {
                                        result = personDbContextRepository.AttachMovableAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                else
                                {
                                    if (viewModel.PhotoPathMovable != null)
                                    {
                                        result = personDbContextRepository.AttachMovableAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(PersonMovableAssetViewModel _personMovableAssetViewModel,string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModelListForModify = await GetEntries(_personMovableAssetViewModel.PersonPrmKey,StringLiteralValue.Verify);
                    if (personMovableAssetViewModelListForModify != null)
                    {
                        foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Modify);

                            if (viewModel.PersonMovableAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify Record
                // Set Default Value
                List<PersonMovableAssetViewModel> personMovableAssetViewModelList = new List<PersonMovableAssetViewModel>();
                personMovableAssetViewModelList = (List<PersonMovableAssetViewModel>)HttpContext.Current.Session["MovableAsset"];

                if (personMovableAssetViewModelList != null)
                {
                    foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelList)
                    {
                        viewModel.Remark = _personMovableAssetViewModel.Remark;
                        viewModel.PersonPrmKey = _personMovableAssetViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, _entryType);

                        if (viewModel.PersonMovableAssetDocumentPrmKey > 0)
                            result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonMovableAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonMovableAssetViewModel>> GetEntries(long _personPrmKey ,string _entryType)
        {
            try
            {
                IEnumerable<PersonMovableAssetViewModel> PersonMovableAssetViewModels = await context.Database.SqlQuery<PersonMovableAssetViewModel>("SELECT * FROM dbo.GetPersonMovableAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonMovableAssetViewModel viewModel in PersonMovableAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathMovable = objFile;

                }
                return PersonMovableAssetViewModels;
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
            int count = await context.PersonMovableAssets
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
