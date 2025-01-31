using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
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

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonMasterRepository : IPersonMasterRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDbContextRepository personDbContextRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;

        public EFPersonMasterRepository(RepositoryConnection _connection, IPersonDbContextRepository _personDbContextRepository, IPersonDetailRepository _personDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonInformationDetailRepository _personInformationDetailRepository)
        {
            context = _connection.EFDbContext;
            personDbContextRepository = _personDbContextRepository;
            personDetailRepository = _personDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
        }

        bool result = true;
        bool isPanCardNumber = false;

        public async Task<bool> Amend(PersonMasterViewModel _personMasterViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personMasterViewModel.PersonAdditionalDetailViewModel.OccupationId);

                result = personDbContextRepository.AttachPersonMasterData(_personMasterViewModel, StringLiteralValue.Amend);

                if (result)
                {
                    result = personDbContextRepository.AttachPersonPrefixData(_personMasterViewModel.PersonPrefixViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personMasterViewModel.PersonAdditionalDetailViewModel, StringLiteralValue.Amend);

                    // Check Person Is Salaried Or Not And Also Check Is Employee Toggleswitch
                    if (occupation == "SLRD" && _personMasterViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                    {
                        //If Salaried Then Attach PersonEmploymentDetailViewModel
                        result = personDbContextRepository.AttachPersonEmploymentDetailData(_personMasterViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Amend);
                    }
                    else
                    {
                        _personMasterViewModel.PersonEmploymentDetailViewModel.NameOfEmployer = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.TransNameOfEmployer = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.EPFNumber = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.TransEPFNumber = "None";

                    }
                }

                // GuardianPerson
                if (result)
                {
                    int age = configurationDetailRepository.GetAge(_personMasterViewModel.DateOfBirth);

                    if (configurationDetailRepository.GetAge(_personMasterViewModel.DateOfBirth) < 18)
                        result = personDbContextRepository.AttachGuardianPersonData(_personMasterViewModel.GuardianPersonViewModel, StringLiteralValue.Amend);
                }


                //Amend Old Record Of PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModelListForAmend = await personInformationDetailRepository.AddressEntries(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personAddressViewModelListForAmend != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened 
                    List<PersonAddressViewModel> personAddressViewModelViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["Address"];

                    if (personAddressViewModelViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                // PersonKYCDocument
                if (result)
                {
                    string kYCDocumentUpload = personInformationParameterViewModel.KYCDocumentUpload;
                    if (!(kYCDocumentUpload == "D"))
                    {

                        // Amend Old Record (i.e. Existing In Db)
                        IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModelListForAmend = await personInformationDetailRepository.KYCDocumentEntries(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Reject);

                        if (personKYCDocumentViewModelListForAmend != null)
                        {
                            foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelListForAmend)
                            {
                                result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Amend);

                                if (viewModel.PersonKYCDetailDocumentPrmKey > 0)
                                {
                                    result = personDbContextRepository.AttachPersonKYCDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                                }
                            }
                        }

                        // New Record Create For Amened 
                        List<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = new List<PersonKYCDocumentViewModel>();
                        personKYCDocumentViewModelList = (List<PersonKYCDocumentViewModel>)HttpContext.Current.Session["KYCDocument"];


                        if (personKYCDocumentViewModelList != null)
                        {
                            foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _personMasterViewModel.Remark;
                                viewModel.PersonPrmKey = _personMasterViewModel.PersonPrmKey;
                                // ******** Add This Statement In All Operation - 05.11.2024
                                viewModel.DocumentDocumentTypePrmKey = personDetailRepository.GetDocumentDocumentTypePrmKeyById(viewModel.DocumentDocumentTypeId);
                                string isPanCard = personDetailRepository.GetSysNameOfDocumentByDocumentId(viewModel.DocumentDocumentTypeId);
                                if (isPanCard == "PAN")
                                {
                                    isPanCardNumber = true;
                                }
                                result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Create);

                                // If Local Storage
                                if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathKYC != null)
                                    {
                                        result = personDbContextRepository.AttachKYCDocumentInLocalStorage(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, null, StringLiteralValue.Create);
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
                                    if (viewModel.PhotoPathKYC != null)
                                    {
                                        result = personDbContextRepository.AttachKYCDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;

                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                result = personDbContextRepository.AttachPersonKYCDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //EnableGSTRegistration
                if (result)
                {
                    if (_personMasterViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey > 0)
                    {
                        if (isPanCardNumber)
                        {
                            if (personInformationParameterViewModel.EnableGSTRegistration == true && _personMasterViewModel.EnableGSTRegistrationDetails == true)
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personMasterViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personMasterViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Delete);
                            }
                        }
                    }
                    else
                    {
                        if (isPanCardNumber)
                        {
                            if (personInformationParameterViewModel.EnableGSTRegistration == true && _personMasterViewModel.EnableGSTRegistrationDetails == true)
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personMasterViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    // PersonGSTReturnDocument
                    // Amend Old Record (i.e. Existing In Db)
                    IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelForAmend = await personInformationDetailRepository.GSTReturnDocumentEntries(_personMasterViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, StringLiteralValue.Reject);

                    if (personGSTReturnDocumentViewModelForAmend != null)
                    {
                        foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelForAmend)
                        {
                            result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                        }
                    }

                    if (_personMasterViewModel.PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument == true)
                    {
                        List<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["GSTReturnDocument"];
                        foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            // EnableImmovableAssetDocumentUploadInLocalStorage
                            if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                            {
                                if (viewModel.PhotoPathGst != null)
                                {
                                    result = personDbContextRepository.AttachGSTDocumentInLocalStorage(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;

                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }
                            else
                            {
                                if (viewModel.PhotoPathGst != null)
                                {
                                    result = personDbContextRepository.AttachGSTDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;
                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }

                            if (personInformationParameterViewModel.GSTDocumentUpload == "D")
                            {
                                viewModel.NameOfFile = "None";
                                viewModel.LocalStoragePath = "None";
                            }
                            result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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
        
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonMasterEntriesOfPerson (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public bool GetPersonInfoNumber(long personInformationNumber)
        {
            bool status;
            if (context.People.Where(p => p.PersonInformationNumber == personInformationNumber).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }
            return status;
        }

        public byte GetPrmKeyById(Guid _prefixId)
        {
            return context.Prefixes
                    .Where(c => c.PrefixId == _prefixId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public long GetPersonPrmKeyById(Guid _personId)
        {
            return context.People
                    .Where(c => c.PersonId == _personId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public long GetPersonGroupPrmKeyByPersonPrmKey(long _personPrmKey)
        {
            return context.PersonGroups
                    .Where(c => c.PersonPrmKey == _personPrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }
       
        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(PersonMasterViewModel _personMasterViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personMasterViewModel.PersonAdditionalDetailViewModel.OccupationId);
                int age = configurationDetailRepository.GetAge(_personMasterViewModel.DateOfBirth);
                result = personDbContextRepository.AttachPersonMasterData(_personMasterViewModel, StringLiteralValue.Create);

                if (result)
                    result = personDbContextRepository.AttachPersonPrefixData(_personMasterViewModel.PersonPrefixViewModel, StringLiteralValue.Create);

                if (result)
                {
                    //If IsPolitician Toggleswitch Is Off Then Provide Default Values
                    if (_personMasterViewModel.PersonAdditionalDetailViewModel.IsPolitician == false)
                    {
                        _personMasterViewModel.PersonAdditionalDetailViewModel.PoliticialBackgroundDetails = "None";
                        _personMasterViewModel.PersonAdditionalDetailViewModel.TransPoliticialBackgroundDetails = "None";
                    }

                    if (age < 16)
                        _personMasterViewModel.PersonAdditionalDetailViewModel.MaritalStatusPrmKey = 2;

                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personMasterViewModel.PersonAdditionalDetailViewModel, StringLiteralValue.Create);

                    // Check Person Is Salaried Or Not And Also Check Is Employee Toggleswitch
                    if (occupation == "SLRD"  && _personMasterViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                    {
                        //If Salaried Then Attach PersonEmploymentDetailViewModel
                        result = personDbContextRepository.AttachPersonEmploymentDetailData(_personMasterViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Create);
                    }
                    else
                    {
                        _personMasterViewModel.PersonEmploymentDetailViewModel.NameOfEmployer = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.TransNameOfEmployer = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.EPFNumber = "None";
                        _personMasterViewModel.PersonEmploymentDetailViewModel.TransEPFNumber = "None";
                    }
                }

                if (age < 18)
                {

                    result = personDbContextRepository.AttachGuardianPersonData(_personMasterViewModel.GuardianPersonViewModel, StringLiteralValue.Create);
                }


                if (result)
                {
                    List<PersonAddressViewModel> personAddressViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["Address"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // PersonKYCDocument
                if (result)
                {
                    string kYCDocumentUpload = personInformationParameterViewModel.KYCDocumentUpload;
                    if (!(kYCDocumentUpload == "D"))
                    {
                        List<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = new List<PersonKYCDocumentViewModel>();
                        personKYCDocumentViewModelList = (List<PersonKYCDocumentViewModel>)HttpContext.Current.Session["KYCDocument"];


                        if (personKYCDocumentViewModelList != null)
                        {
                            foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _personMasterViewModel.Remark;
                                viewModel.PersonPrmKey = _personMasterViewModel.PersonPrmKey;
                                string isPanCard = personDetailRepository.GetSysNameOfDocumentByDocumentId(viewModel.DocumentDocumentTypeId);
                                if (isPanCard == "PAN")
                                {
                                    isPanCardNumber = true;
                                }
                                result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Create);

                                // If Local Storage
                                if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage == true)
                                {
                                    if (viewModel.PhotoPathKYC != null)
                                    {
                                        result = personDbContextRepository.AttachKYCDocumentInLocalStorage(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, null, StringLiteralValue.Create);
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
                                    if (viewModel.PhotoPathKYC != null)
                                    {
                                        result = personDbContextRepository.AttachKYCDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = oldFileName;
                                        viewModel.LocalStoragePath = oldLocalStoragePath;
                                    }
                                }

                                result = personDbContextRepository.AttachPersonKYCDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //PersonGSTRegistrationDetail
                if (result)
                {
                    if (personInformationParameterViewModel.EnableGSTRegistration == true && _personMasterViewModel.EnableGSTRegistrationDetails==true)
                    {
                        if (isPanCardNumber)
                        {
                            result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personMasterViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Create);
                            if (personInformationParameterViewModel.GSTDocumentUpload != "D")
                            {
                                // PersonGSTRegistrationDetail
                                if (_personMasterViewModel.PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument == true)
                                {
                                    List<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["GSTReturnDocument"];
                                    foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                                    {
                                        string oldLocalStoragePath = viewModel.LocalStoragePath;

                                        string oldFileName = viewModel.NameOfFile;

                                        // EnableImmovableAssetDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                                        {
                                            if (viewModel.PhotoPathGst != null)
                                            {
                                                    result = personDbContextRepository.AttachGSTDocumentInLocalStorage(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }
                                        else
                                        {
                                            if (viewModel.PhotoPathGst != null)
                                            {
                                                result = personDbContextRepository.AttachGSTDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        if (personInformationParameterViewModel.GSTDocumentUpload == "D")
                                        {
                                            viewModel.NameOfFile = "None";
                                        }
                                        result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonMasterViewModel _personMasterViewModel,string _entryType)
        {
            string _entriesType;
            if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
            {
                _entriesType = StringLiteralValue.Unverified;
            }
            else
                _entriesType = StringLiteralValue.Reject;

            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personMasterViewModel.PersonAdditionalDetailViewModel.OccupationId);

                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry
                    // Assign MDF Status To EntryStatus For Performing Modify Operation

                    if (result)
                    {
                        PersonMasterViewModel personMasterViewModelForModify = await GetPersonMasterOldVerifiedEntry(_personMasterViewModel.PersonId);
                        if (personMasterViewModelForModify != null)
                        {
                            if (personMasterViewModelForModify.PersonModificationPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonMasterData(personMasterViewModelForModify, StringLiteralValue.Modify);
                            }
                        }
                    }

                    if (result)
                    {
                        
                        PersonPrefixViewModel personPrefixViewModelForModify = await personInformationDetailRepository.PrefixEntry(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);
                        if (personPrefixViewModelForModify != null)
                        {
                            result = personDbContextRepository.AttachPersonPrefixData(personPrefixViewModelForModify, StringLiteralValue.Modify);
                        }
                    }

                    if (result)
                    {
                        PersonAdditionalDetailViewModel personAdditionalDetailViewModelForModify = await personInformationDetailRepository.AdditionalDetailEntry(_personMasterViewModel.PersonPrmKey,StringLiteralValue.Verify);
                        if (personAdditionalDetailViewModelForModify != null)
                        {
                            result = personDbContextRepository.AttachPersonAdditionalDetailData(personAdditionalDetailViewModelForModify, StringLiteralValue.Modify);
                            if (occupation == "SLRD")
                            {
                                PersonEmploymentDetailViewModel personEmploymentDetailViewModelForModify = await personInformationDetailRepository.EmploymentDetailEntry(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);
                                if (personEmploymentDetailViewModelForModify != null)
                                {
                                    result = personDbContextRepository.AttachPersonEmploymentDetailData(personEmploymentDetailViewModelForModify, StringLiteralValue.Modify);
                                }
                            }
                        }
                    }


                    //GuardianPerson
                    if (result)
                    {
                        GuardianPersonViewModel guardianPersonViewModelForModify = await personInformationDetailRepository.GuardianPersonEntry(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);
                        if (guardianPersonViewModelForModify != null)
                        {
                            result = personDbContextRepository.AttachGuardianPersonData(guardianPersonViewModelForModify, StringLiteralValue.Modify);
                        }
                    }

                    //PersonAddress
                    if (result)
                    {
                        IEnumerable<PersonAddressViewModel> personAddressViewModelListForModify = await personInformationDetailRepository.AddressEntries(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                    // PersonKYCDocument
                    if (result)
                    {
                        string kYCDocumentUpload = personInformationParameterViewModel.KYCDocumentUpload;
                        if (!(kYCDocumentUpload == "D"))
                        {
                            IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModelListForModify = await personInformationDetailRepository.KYCDocumentEntries(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);

                            if (personKYCDocumentViewModelListForModify != null)
                            {
                                foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelListForModify)
                                {
                                    result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Modify);

                                    if (viewModel.PersonKYCDetailDocumentPrmKey > 0)
                                    {
                                        result = personDbContextRepository.AttachPersonKYCDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                                    }
                                }
                            }

                        }
                    }

                    //PersonGSTRegistrationDetail
                    if (result)
                    {
                        if (personInformationParameterViewModel.EnableGSTRegistration == true && _personMasterViewModel.EnableGSTRegistrationDetails == true)
                        {
                            PersonGSTRegistrationDetailViewModel personGSTRegistrationDetailViewModelForModify = await personInformationDetailRepository.GSTRegistrationDetailEntry(_personMasterViewModel.PersonPrmKey, StringLiteralValue.Verify);
                            if (personGSTRegistrationDetailViewModelForModify != null)
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(personGSTRegistrationDetailViewModelForModify, StringLiteralValue.Modify);
                            }
                        }
                    }

                    if (result)
                    {
                        IEnumerable<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = await personInformationDetailRepository.GSTReturnDocumentEntries(_personMasterViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, StringLiteralValue.Verify);

                        if (personGSTRegistrationDetailViewModelList != null)
                        {
                            foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Modify);
                            }
                        }
                    }
                }


                //Change Entries As Per _entryType
                if (result)
                    result = personDbContextRepository.AttachPersonMasterData(_personMasterViewModel, _entryType);

                if (result)
                    result = personDbContextRepository.AttachPersonPrefixData(_personMasterViewModel.PersonPrefixViewModel, _entryType);

                if (result)
                {
                    PersonAdditionalDetailViewModel personAdditionalDetailViewModel = await personInformationDetailRepository.AdditionalDetailEntry(_personMasterViewModel.PersonPrmKey, _entriesType);
                    if (personAdditionalDetailViewModel != null)
                    {
                        result = personDbContextRepository.AttachPersonAdditionalDetailData(personAdditionalDetailViewModel, _entryType);
                        if (occupation == "SLRD")
                        {
                            PersonEmploymentDetailViewModel personEmploymentDetailViewModel = await personInformationDetailRepository.EmploymentDetailEntry(_personMasterViewModel.PersonPrmKey, _entriesType);
                            if (personEmploymentDetailViewModel != null)
                            {
                                result = personDbContextRepository.AttachPersonEmploymentDetailData(personEmploymentDetailViewModel, _entryType);
                            }
                        }
                    }
                }


                //GuardianPerson
                if (result)
                {
                    GuardianPersonViewModel guardianPersonViewModel = await personInformationDetailRepository.GuardianPersonEntry(_personMasterViewModel.PersonPrmKey, _entriesType);
                    if (guardianPersonViewModel != null)
                    {
                        result = personDbContextRepository.AttachGuardianPersonData(guardianPersonViewModel, _entryType);
                    }
                }

                //PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModelViewModelList = await personInformationDetailRepository.AddressEntries(_personMasterViewModel.PersonPrmKey, _entriesType);

                    if (personAddressViewModelViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                        }
                    }
                }

                // PersonKYCDocument
                if (result)
                {
                    string kYCDocumentUpload = personInformationParameterViewModel.KYCDocumentUpload;
                    if (!(kYCDocumentUpload == "D"))
                    {
                        IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = await personInformationDetailRepository.KYCDocumentEntries(_personMasterViewModel.PersonPrmKey, _entriesType);

                        if (personKYCDocumentViewModelList != null)
                        {
                            foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonKYCData(viewModel, _entryType);

                                if (viewModel.PersonKYCDetailDocumentPrmKey > 0)
                                {
                                    result = personDbContextRepository.AttachPersonKYCDocumentData(viewModel, personInformationParameterViewModel.KYCDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                                }
                            }
                        }
                    }
                }

                //PersonGSTRegistrationDetail
                if (result)
                {
                    PersonGSTRegistrationDetailViewModel personGSTRegistrationDetailViewModel = await personInformationDetailRepository.GSTRegistrationDetailEntry(_personMasterViewModel.PersonPrmKey, _entriesType);
                    if (personGSTRegistrationDetailViewModel != null)
                    {
                        result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(personGSTRegistrationDetailViewModel, _entryType);

                        IEnumerable<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = await personInformationDetailRepository.GSTReturnDocumentEntries(personGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, _entriesType);

                        if (personGSTRegistrationDetailViewModelList != null)
                        {
                            foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
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
        
        //Get Old Verified Entries of PersonMaster By PersonPrmKey
        public async Task<PersonMasterViewModel> GetPersonMasterOldVerifiedEntry(Guid _personId)
        {
            PersonMasterViewModel personMastViewModel = new PersonMasterViewModel();

            try
            {
                personMastViewModel = await context.Database.SqlQuery<PersonMasterViewModel>("SELECT * FROM dbo.GetPersonMasterEntry (@PersonId, @EntryType)", new SqlParameter("@PersonId", _personId), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }

            return personMastViewModel;
        }

        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count of PersonModification
            int count = await context.PersonModifications
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject ||  u.EntryStatus == StringLiteralValue.Amend) && u.PersonPrmKey == personPrmKey)
                        .Select(u => u.PrmKey).CountAsync();

            //check waiting for response and rejected entries count of Person
            int personcount = await context.People
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject ||  u.EntryStatus == StringLiteralValue.Amend) && u.PrmKey == personPrmKey)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0 || personcount > 0)
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
