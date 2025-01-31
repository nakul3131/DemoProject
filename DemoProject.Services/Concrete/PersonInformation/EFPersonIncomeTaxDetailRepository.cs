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
    public class EFPersonIncomeTaxDetailRepository : IPersonIncomeTaxDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonIncomeTaxDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel)
        {
           
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                 //Amend old Record
                if (result)
                {
                    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelListForAmend = await GetEntries(_personIncomeTaxDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);

                    if (personIncomeTaxDetailViewModelListForAmend != null)
                    {
                        foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //Create New Record
                    if (personInformationParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        // Insert Record From Session Object
                        List<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = (List<PersonIncomeTaxDetailViewModel>)HttpContext.Current.Session["IncomeTaxDetail"];

                        if (personIncomeTaxDetailViewModelList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;
                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _personIncomeTaxDetailViewModel.Remark;
                                viewModel.PersonPrmKey = _personIncomeTaxDetailViewModel.PersonPrmKey;

                                result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                                if (personIncomeTaxDetailViewModelList != null)
                                {
                                    if (personInformationParameterViewModel.IncomeTaxDocumentUpload != "D")
                                    {
                                        // EnableIncomeTaxDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                        {
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = personDbContextRepository.AttachIncomeTaxDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        // If Db Storage
                                        else
                                        {
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = personDbContextRepository.AttachIncomeTaxDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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
        
        public async Task<bool> Modify(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (personInformationParameterViewModel.EnableIncomeTaxDetail == true)
                {
                    // Insert Record From Session Object
                    List<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = (List<PersonIncomeTaxDetailViewModel>)HttpContext.Current.Session["IncomeTaxDetail"];

                    if (personIncomeTaxDetailViewModelList != null)
                    {
                        foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;
                            string oldFileName = viewModel.NameOfFile;
                            viewModel.Remark = _personIncomeTaxDetailViewModel.Remark;
                            viewModel.PersonPrmKey = _personIncomeTaxDetailViewModel.PersonPrmKey;

                            result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                            if (personIncomeTaxDetailViewModelList != null)
                            {
                                if (personInformationParameterViewModel.IncomeTaxDocumentUpload != "D")
                                {
                                    // EnableIncomeTaxDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathTax != null)
                                        {
                                            result = personDbContextRepository.AttachIncomeTaxDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    // If Db Storage
                                    else
                                    {
                                        if (viewModel.PhotoPathTax != null)
                                        {
                                            result = personDbContextRepository.AttachIncomeTaxDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Record
                    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelListForModify = await GetEntries(_personIncomeTaxDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Modify);

                        if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                        }
                    }
                }
                
                List<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = new List<PersonIncomeTaxDetailViewModel>();
                personIncomeTaxDetailViewModelList = (List<PersonIncomeTaxDetailViewModel>)HttpContext.Current.Session["IncomeTaxDetail"];

                foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                {
                    viewModel.Remark = _personIncomeTaxDetailViewModel.Remark;
                    viewModel.PersonPrmKey = _personIncomeTaxDetailViewModel.PersonPrmKey;

                    result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, _entryType);

                    if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                    {
                        result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonIncomeTaxDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                IEnumerable<PersonIncomeTaxDetailViewModel> PersonIncomeTaxDetailViewModels = await context.Database.SqlQuery<PersonIncomeTaxDetailViewModel>("SELECT * FROM dbo.GetPersonIncomeTaxDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                foreach (PersonIncomeTaxDetailViewModel viewModel in PersonIncomeTaxDetailViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathTax = objFile;

                }
                return PersonIncomeTaxDetailViewModels;
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
            int count = await context.PersonIncomeTaxDetails
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
