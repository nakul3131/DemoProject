using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonAgricultureAssetRepository : IPersonAgricultureAssetRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonAgricultureAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        public async Task<bool> Amend(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            bool result = true;


            try
            {
                if (result)
                {
                    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelListForAmend = await GetEntries(_personAgricultureAssetViewModel.PersonPrmKey ,StringLiteralValue.Reject);
                    foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelListForAmend)
                    {
                        if (personAgricultureAssetViewModelListForAmend != null)
                        {
                            result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonAgricultureAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
                        }
                    }

                    // New Record Create For Amened  personAgricultureAsset
                    if (personInformationParameterViewModel.EnableAgricultureAsset == true)
                    {
                        List<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelList = (List<PersonAgricultureAssetViewModel>)HttpContext.Current.Session["AgricultureAsset"];
                        foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                        {
                            viewModel.Remark = _personAgricultureAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personAgricultureAssetViewModel.PersonPrmKey;
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.AgricultureAssetDocumentUpload != "D")
                            {
                                //If Local Storage
                                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathAgree != null)
                                    {
                                        result = personDbContextRepository.AttachAgricultureAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                else
                                {
                                    //If Db Storage
                                    if (viewModel.PhotoPathAgree != null)
                                    {
                                        result = personDbContextRepository.AttachAgricultureAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                if (personInformationParameterViewModel.AgricultureAssetDocumentUpload == "D")
                                {
                                    viewModel.NameOfFile = "None";
                                    viewModel.LocalStoragePath = "None";
                                }

                                result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel)
        {
            
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                if (result)
                {
                    if (personInformationParameterViewModel.EnableAgricultureAsset == true)
                    {
                        List<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelList = (List<PersonAgricultureAssetViewModel>)HttpContext.Current.Session["AgricultureAsset"];
                        foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                        {
                            viewModel.Remark = _personAgricultureAssetViewModel.Remark;
                            viewModel.PersonPrmKey = _personAgricultureAssetViewModel.PersonPrmKey;
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.AgricultureAssetDocumentUpload != "D")
                            {
                                //If Local Storage
                                if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathAgree != null)
                                    {
                                            result = personDbContextRepository.AttachAgricultureAssetDocumentInLocalStorage(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                else
                                {
                                    //If Db Storage
                                    if (viewModel.PhotoPathAgree != null)
                                    {
                                        result = personDbContextRepository.AttachAgricultureAssetDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }
                                if (personInformationParameterViewModel.AgricultureAssetDocumentUpload == "D")
                                {
                                    viewModel.NameOfFile = "None";
                                    viewModel.LocalStoragePath = "None";
                                }

                                result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                bool result = true;
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelListForModify = await GetEntries(_personAgricultureAssetViewModel.PersonPrmKey ,StringLiteralValue.Verify);

                    foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, StringLiteralValue.Modify);

                        if (viewModel.PersonAgricultureAssetDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                        }

                    }
                }
                // Verify Record
                List<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelList = new List<PersonAgricultureAssetViewModel>();
                personAgricultureAssetViewModelList = (List<PersonAgricultureAssetViewModel>)HttpContext.Current.Session["AgricultureAsset"];

                if (personAgricultureAssetViewModelList != null)
                {
                    foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                    {
                        viewModel.Remark = _personAgricultureAssetViewModel.Remark;
                        viewModel.PersonPrmKey = _personAgricultureAssetViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, _entryType);

                        if (viewModel.PersonAgricultureAssetDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);

                        }
                    }
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return true;
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonAgricultureAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType",_entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        ////Get Verified Entries By PersonPrmKey
        //public async Task<IEnumerable<PersonAgricultureAssetViewModel>> GetVerifiedEntries(long _personPrmKey)
        //{
        //    try
        //    {
        //        return await context.Database.SqlQuery<PersonAgricultureAssetViewModel>("SELECT * FROM dbo.GetPersonAgricultureAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return null;
        //    }
        //}

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonAgricultureAssetViewModel>> GetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = await context.Database.SqlQuery<PersonAgricultureAssetViewModel>("SELECT * FROM dbo.GetPersonAgricultureAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();

                foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathAgree = objFile;

                }

                return personAgricultureAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        ////Get Rejected Entries By PersonPrmKey
        //public async Task<IEnumerable<PersonAgricultureAssetViewModel>> GetRejectedEntries(long _personPrmKey)
        //{
        //    try
        //    {
        //        IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels =  await context.Database.SqlQuery<PersonAgricultureAssetViewModel>("SELECT * FROM dbo.GetPersonAgricultureAssetEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

        //        foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
        //        {
        //            HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

        //            viewModel.PhotoPathAgree = objFile;

        //        }

        //        return personAgricultureAssetViewModels;
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;

        //        return null;
        //    }
        //}

        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonAgricultureAssets
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
