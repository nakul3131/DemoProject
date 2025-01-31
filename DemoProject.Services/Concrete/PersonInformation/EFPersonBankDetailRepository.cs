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
    public class EFPersonBankDetailRepository : IPersonBankDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonBankDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        public async Task<bool> Amend(PersonBankDetailViewModel _personBankDetailViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                if (result)
                {
                    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModelListForAmend = await GetEntries(_personBankDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);
                    foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelListForAmend)
                    {
                        if (personBankDetailViewModelListForAmend != null)
                            result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Amend);

                        if (viewModel.PersonBankDetailDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened
                    if (personInformationParameterViewModel.EnableBankingDetail == true)
                    {
                        List<PersonBankDetailViewModel> personBankDetailViewModelList = (List<PersonBankDetailViewModel>)HttpContext.Current.Session["BankDetail"];

                        if (personBankDetailViewModelList != null)
                        {
                            foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;
                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _personBankDetailViewModel.Remark;
                                viewModel.PersonPrmKey = _personBankDetailViewModel.PersonPrmKey;

                                result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.BankStatementDocumentUpload != "D")
                                {
                                    //If Local Storage
                                    if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathBank != null)
                                        {
                                            result = personDbContextRepository.AttachBankDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, null, StringLiteralValue.Create);
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
                                        if (viewModel.PhotoPathBank != null)
                                        {
                                            result = personDbContextRepository.AttachBankDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.NameOfFile = oldFileName;
                                            viewModel.LocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                }

                                if (personInformationParameterViewModel.BankStatementDocumentUpload == "D")
                                {
                                    viewModel.NameOfFile = "None";
                                    viewModel.LocalStoragePath = "None";
                                    result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Create);

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

        public async Task<bool> Modify(PersonBankDetailViewModel _personBankDetailViewModel)
        {
            try
            {

                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;
                if (personInformationParameterViewModel.EnableBankingDetail == true)
                {
                    List<PersonBankDetailViewModel> personBankDetailViewModelList = (List<PersonBankDetailViewModel>)HttpContext.Current.Session["BankDetail"];

                    if (personBankDetailViewModelList != null)
                    {
                        foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;
                            string oldFileName = viewModel.NameOfFile;

                            viewModel.Remark = _personBankDetailViewModel.Remark;
                            viewModel.PersonPrmKey = _personBankDetailViewModel.PersonPrmKey;

                            result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.BankStatementDocumentUpload != "D")
                            {
                                // If Local Storage
                                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathBank != null)
                                    {
                                        result = personDbContextRepository.AttachBankDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, null, StringLiteralValue.Create);
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
                                    if (viewModel.PhotoPathBank != null)
                                    {
                                        result = personDbContextRepository.AttachBankDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                            }

                            if (personInformationParameterViewModel.BankStatementDocumentUpload == "D")
                            {
                                viewModel.NameOfFile = "None";
                                viewModel.LocalStoragePath = "None";
                                result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(PersonBankDetailViewModel _personBankDetailViewModel, string _entryType)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                bool result = true;
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModelListForModify = await GetEntries(_personBankDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Modify);

                        if (viewModel.PersonBankDetailDocumentPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify Record
                // Set Default Value
                List<PersonBankDetailViewModel> personBankDetailViewModelList = new List<PersonBankDetailViewModel>();
                personBankDetailViewModelList = (List<PersonBankDetailViewModel>)HttpContext.Current.Session["BankDetail"];

                foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelList)
                {
                    viewModel.Remark = _personBankDetailViewModel.Remark;
                    viewModel.PersonPrmKey = _personBankDetailViewModel.PersonPrmKey;
                    result = personDbContextRepository.AttachPersonBankDetailData(viewModel, _entryType);

                    if (viewModel.PersonBankDetailDocumentPrmKey > 0)
                    {
                        result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
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

        //Get Verified Entries By PersonPrmKey
        public async Task<IEnumerable<PersonBankDetailViewModel>> GetEntries(long _personPrmKey,string _entryType)
        {
            try
            {
                IEnumerable<PersonBankDetailViewModel> PersonBankDetailViewModels = await context.Database.SqlQuery<PersonBankDetailViewModel>("SELECT * FROM dbo.GetPersonBankDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();

                foreach (PersonBankDetailViewModel viewModel in PersonBankDetailViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);

                    viewModel.PhotoPathBank = objFile;
                }

                return PersonBankDetailViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
       

        //Get  Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonBankDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType",_entryType)).ToListAsync();
                return a;
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
            int count = await context.PersonBankDetails
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
