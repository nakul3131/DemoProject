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
    public class EFPersonGroupAuthorizedSignatoryRepository: IPersonGroupAuthorizedSignatoryRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonGroupAuthorizedSignatoryRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        public async Task<bool> Amend(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel)
        {

            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                if (result)
                {
                    IEnumerable<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelListForAmend = await GetEntries(_personGroupAuthorizedSignatoryViewModel.PersonGroupPrmKey,StringLiteralValue.Reject);
                    foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelListForAmend)
                    {
                        if (personGroupAuthorizedSignatoryViewModelListForAmend != null)
                            result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Amend);
                    }

                    // New Record Create For Amened
                    List<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelList = (List<PersonGroupAuthorizedSignatoryViewModel>)HttpContext.Current.Session["GroupAuthorizedSignatory"];

                    if (personGroupAuthorizedSignatoryViewModelList != null)
                    {
                        foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelList)
                        {
                            viewModel.Remark = _personGroupAuthorizedSignatoryViewModel.Remark;

                            string oldLocalStoragePath = viewModel.SignLocalStoragePath;

                            string oldFileName = viewModel.SignNameOfFile;

                            if (personInformationParameterViewModel.SignDocumentUpload != "D")
                            {
                                if (viewModel.IsAuthorizedSignatory == true)
                                {
                                    //If Local Storage
                                    if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                    {
                                        if (viewModel.PhotoPathSign != null)
                                        {
                                                result = personDbContextRepository.AttachGroupAuthorizedSignatoryInLocalStorage(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.SignNameOfFile = oldFileName;

                                            viewModel.SignLocalStoragePath = oldLocalStoragePath;
                                        }
                                    }

                                    // If Db Storage
                                    else
                                    {
                                        if (viewModel.PhotoPathSign != null)
                                        {
                                            result = personDbContextRepository.AttachGroupAuthorizedSignatoryInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                        }
                                        else
                                        {
                                            viewModel.SignNameOfFile = oldFileName;

                                            viewModel.SignLocalStoragePath = oldLocalStoragePath;

                                        }
                                    }
                                }
                                result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Create);


                            }
                            else
                            {
                                if (personInformationParameterViewModel.SignDocumentUpload == "D")
                                {
                                    viewModel.SignNameOfFile = "None";
                                    viewModel.SignLocalStoragePath = "None";
                                    result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel)
        {
            try
            {

                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                bool result = true;

                List<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelList = (List<PersonGroupAuthorizedSignatoryViewModel>)HttpContext.Current.Session["GroupAuthorizedSignatory"];

                if (personGroupAuthorizedSignatoryViewModelList != null)
                {
                    foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelList)
                    {
                        viewModel.Remark = _personGroupAuthorizedSignatoryViewModel.Remark;

                        string oldLocalStoragePath = viewModel.SignLocalStoragePath;

                        string oldFileName = viewModel.SignNameOfFile;

                        if (personInformationParameterViewModel.SignDocumentUpload != "D")
                        {
                            if (viewModel.IsAuthorizedSignatory == true)
                            {
                                //If Local Storage
                                if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathSign != null)
                                    {
                                        result = personDbContextRepository.AttachGroupAuthorizedSignatoryInLocalStorage(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.SignNameOfFile = oldFileName;

                                        viewModel.SignLocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                // If Db Storage
                                else
                                {
                                    if (viewModel.PhotoPathSign != null)
                                    {
                                        result = personDbContextRepository.AttachGroupAuthorizedSignatoryInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.SignNameOfFile = oldFileName;

                                        viewModel.SignLocalStoragePath = oldLocalStoragePath;

                                    }
                                }
                            }
                            result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Create);


                        }
                        else
                        {
                            if (personInformationParameterViewModel.SignDocumentUpload == "D")
                            {
                                viewModel.SignNameOfFile = "None";
                                viewModel.SignLocalStoragePath = "None";
                                result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonGroupAuthorizedSignatoryViewModel _personGroupAuthorizedSignatoryViewModel, string _entryType)
        {
            try
            {
                bool result = true;
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old PersonGroupAuthorizedSignatory 
                    IEnumerable<PersonGroupAuthorizedSignatoryViewModel> PersonGroupAuthorizedSignatoryViewModelListForModify = await GetEntries(_personGroupAuthorizedSignatoryViewModel.PersonGroupPrmKey,StringLiteralValue.Verify);

                    foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in PersonGroupAuthorizedSignatoryViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Modify);
                        
                    }
                }

                //Perform Operation As Per _entryType
                List<PersonGroupAuthorizedSignatoryViewModel> PersonGroupAuthorizedSignatoryViewModelList = new List<PersonGroupAuthorizedSignatoryViewModel>();
                PersonGroupAuthorizedSignatoryViewModelList = (List<PersonGroupAuthorizedSignatoryViewModel>)HttpContext.Current.Session["GroupAuthorizedSignatory"];

                foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in PersonGroupAuthorizedSignatoryViewModelList)
                {
                    result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, _entryType);
                }

                await context.SaveChangesAsync();

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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonGroupAuthorizedSignatory ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonGroupAuthorizedSignatoryViewModel>> GetEntries(long _personGroupPrmKey,string _entryType)
        {
            try
            {
                IEnumerable<PersonGroupAuthorizedSignatoryViewModel> PersonGroupAuthorizedSignatoryViewModels;
                PersonGroupAuthorizedSignatoryViewModels = await context.Database.SqlQuery<PersonGroupAuthorizedSignatoryViewModel>("SELECT * FROM dbo.GetPersonGroupAuthorizedSignatoryEntriesByPersonGroupPrmKey (@PersonGroupPrmKey, @EntriesType)", new SqlParameter("@PersonGroupPrmKey", _personGroupPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();

                foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in PersonGroupAuthorizedSignatoryViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.Sign, viewModel.SignNameOfFile);

                    viewModel.PhotoPathSign = objFile;

                }
                return PersonGroupAuthorizedSignatoryViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<bool> IsAnyAuthorizationPending(long personGroupPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonGroupAuthorizedSignatories
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject) && u.PersonGroupPrmKey == personGroupPrmKey)
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
