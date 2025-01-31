using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonRepository : IPersonRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDbContextRepository personDbContextRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonAdditionalDetailRepository personAdditionalDetailRepository;
        private readonly ICountryRepository countryRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        bool isPanCardNumber = false;

        public EFPersonRepository(RepositoryConnection _connection, IPersonDbContextRepository _personDbContextRepository, IPersonDetailRepository _personDetailRepository, IPersonInformationDetailRepository _personInformationDetailRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonAdditionalDetailRepository _personAdditionalDetailRepository,
                                 ICountryRepository _countryRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            personDbContextRepository = _personDbContextRepository;
            personDetailRepository = _personDetailRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personAdditionalDetailRepository = _personAdditionalDetailRepository;
            countryRepository = _countryRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(PersonViewModel _personViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personViewModel.PersonAdditionalDetailViewModel.OccupationId);
                string personType = personDetailRepository.GetSysNameOfPersonTypeById(_personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);

                bool result;
                
                //Add Default Values When PersonType Is Not Individual
                if (personType != "INDVL")
                {
                    _personViewModel.FirstName = "None";
                    _personViewModel.TransFirstName = "None";
                    _personViewModel.MiddleName = "None";
                    _personViewModel.TransMiddleName = "None";
                    _personViewModel.LastName = "None";
                    _personViewModel.TransLastName = "None";
                    _personViewModel.DateOfBirth = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    _personViewModel.DateOfBirthOnDocument = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    _personViewModel.MotherName = "None";
                    _personViewModel.TransMotherName = "None";
                    _personViewModel.MothersMaidenName = "None";
                    _personViewModel.TransMothersMaidenName = "None";
                }

                result = personDbContextRepository.AttachPersonData(_personViewModel, StringLiteralValue.Amend);

                if (result)
                {
                    if (personType != "INDVL")
                    {
                        _personViewModel.PersonPrefixViewModel.PrefixPrmKey = 1;
                    }
                    result = personDbContextRepository.AttachPersonPrefixData(_personViewModel.PersonPrefixViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personViewModel.PersonAdditionalDetailViewModel, StringLiteralValue.Amend);

                    if (_personViewModel.PersonEmploymentDetailViewModel.PersonEmploymentDetailPrmKey > 0)
                    {
                        if (occupation == "SLRD" && _personViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                        {
                            result = personDbContextRepository.AttachPersonEmploymentDetailData(_personViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Amend);
                        }
                        else
                        {
                            result = personDbContextRepository.AttachPersonEmploymentDetailData(_personViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Delete);
                        }
                    }
                    else
                    {
                        if (occupation == "SLRD" && _personViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                        {
                            result = personDbContextRepository.AttachPersonEmploymentDetailData(_personViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Create);
                        }
                    }

                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonHomeBranchData(_personViewModel.PersonHomeBranchViewModel, StringLiteralValue.Amend);
                }

                // GuardianPerson
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        int age = configurationDetailRepository.GetAge(_personViewModel.DateOfBirth);
                        if (_personViewModel.GuardianPersonViewModel.GuardianPersonPrmKey > 0)
                        {
                            if (configurationDetailRepository.GetAge(_personViewModel.DateOfBirth) < 18)
                                result = personDbContextRepository.AttachGuardianPersonData(_personViewModel.GuardianPersonViewModel, StringLiteralValue.Amend);

                        }
                        else
                        {
                            if (configurationDetailRepository.GetAge(_personViewModel.DateOfBirth) < 18)
                                result = personDbContextRepository.AttachGuardianPersonData(_personViewModel.GuardianPersonViewModel, StringLiteralValue.Create);

                        }
                    }
                }

                // PersonCommoditiesAsset
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (_personViewModel.PersonCommoditiesAssetViewModel.PersonCommoditiesAssetPrmKey > 0)
                        {
                            if (personInformationParameterViewModel.EnableCommoditiesAsset == true)
                                result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personViewModel.PersonCommoditiesAssetViewModel, StringLiteralValue.Amend);
                        }
                        else
                        {
                            if (personInformationParameterViewModel.EnableCommoditiesAsset == true)
                            {
                                result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personViewModel.PersonCommoditiesAssetViewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //PersonAdditionalIncomeDetail
                //Amend Old Record
                if (result)
                {
                    IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelsListForAmend = await personInformationDetailRepository.AdditionalIncomeDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                    if (personAdditionalIncomeDetailViewModelsListForAmend != null)
                    {
                        foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelsListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    if (personInformationParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        // New Record Create For Amened 
                        List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = new List<PersonAdditionalIncomeDetailViewModel>();
                        personAdditionalIncomeDetailViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["AdditionalIncomeDetail"];

                        if (personAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Amend Old Record Of PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModelListForAmend = await personInformationDetailRepository.AddressEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

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

                //PersonAgricultureAsset
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelListForAmend = await personInformationDetailRepository.AgricultureAssetEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                    if (personAgricultureAssetViewModelListForAmend != null)
                    {
                        foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelListForAmend)
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
                        if (personAgricultureAssetViewModelList != null)
                        {
                            foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                            {
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

                                    result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // PersonBankDetail
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModelListForAmend = await personInformationDetailRepository.BankDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                    if (personBankDetailViewModelListForAmend != null)
                    {
                        foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelListForAmend)
                        {
                        
                            result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonBankDetailDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
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
                                viewModel.Remark = _personViewModel.Remark;
                                viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;

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
                                
                            }
                        }
                    }

                }

                //PersonGroupViewModel
                if (result)
                {
                    if (personType != "INDVL")
                    {
                        if (_personViewModel.PersonGroupViewModel.PersonGroupPrmKey > 0)
                        {
                            result = personDbContextRepository.AttachPersonGroupData(_personViewModel.PersonGroupViewModel, StringLiteralValue.Amend);
                        }



                        // PersonGroupAuthorizedSignatory
                        // Amend Old Record (i.e. Existing In Db)

                        IEnumerable<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelListForAmend = await personInformationDetailRepository.GroupAuthorizedSignatoryEntries(_personViewModel.PersonGroupViewModel.PersonGroupPrmKey, StringLiteralValue.Reject);
                        if (personGroupAuthorizedSignatoryViewModelListForAmend != null)
                        {
                            foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelListForAmend)
                            {
                                result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, StringLiteralValue.Amend);
                            }
                        }

                        // New Record Create For Amened
                        List<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelList = (List<PersonGroupAuthorizedSignatoryViewModel>)HttpContext.Current.Session["GroupAuthorizedSignatory"];

                        if (personGroupAuthorizedSignatoryViewModelList != null)
                        {
                            foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.SignLocalStoragePath;
                                string oldFileName = viewModel.SignNameOfFile;

                                if (personInformationParameterViewModel.SignDocumentUpload != "D")
                                {
                                    if (viewModel.IsAuthorizedSignatory == true)
                                    {
                                        //If Local Storage
                                        if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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
                                                viewModel.SignNameOfFile = "None";
                                                viewModel.SignLocalStoragePath = "None";
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
                }

                //PersonBoardOfDirectorRelation
                //Amend Old Record From Database
                if (result)
                {
                    IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationsViewModelListForAmend = await personInformationDetailRepository.BoardOfDirectorRelationEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personBoardOfDirectorRelationsViewModelListForAmend != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationsViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Amend);

                        }
                    }
                }
                // New Record Create For Amened 
                if (result)
                {
                    List<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = new List<PersonBoardOfDirectorRelationViewModel>();
                    personBoardOfDirectorRelationViewModelList = (List<PersonBoardOfDirectorRelationViewModel>)HttpContext.Current.Session["BoardOfDirectorRelation"];

                    if (personBoardOfDirectorRelationViewModelList != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //PersonBorrowingDetail
                //Amend Old Record From DAtabase
                if (result)
                {
                    IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelListForAmend = await personInformationDetailRepository.BorrowingDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                    if (personBorrowingDetailViewModelListForAmend != null)
                    {
                        foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelListForAmend)
                        {
                        
                            result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (personInformationParameterViewModel.EnableBorrowingDetail == true)
                    {
                        // New Record Create For Amened 
                        List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelsList = new List<PersonBorrowingDetailViewModel>();
                        personBorrowingDetailViewModelsList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];

                        if (personBorrowingDetailViewModelsList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelsList)
                            {
                                result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }
                
                //Amend Old Rcord from Database
                if (personType == "INDVL")
                {
                    if (result)
                    {
                        IEnumerable<PersonChronicDiseaseViewModel> PersonChronicDiseasesViewModelListForAmend = await personInformationDetailRepository.ChronicDiseaseEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                        if (PersonChronicDiseasesViewModelListForAmend != null)
                        {
                            foreach (PersonChronicDiseaseViewModel viewModel in PersonChronicDiseasesViewModelListForAmend)
                            {
                            
                                result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }

                    // New Record Create For Amened 
                    if (result)
                    {
                        if (personInformationParameterViewModel.EnableChronicDisease == true)
                        {
                            List<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = new List<PersonChronicDiseaseViewModel>();
                            personChronicDiseaseViewModelList = (List<PersonChronicDiseaseViewModel>)HttpContext.Current.Session["ChronicDisease"];

                            if (personChronicDiseaseViewModelList != null)
                            {
                                foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                                {
                                    result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }
                
                //PersonContactDetail
                //Amend Old Rcord from Database
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> PersonContactDetailsViewModelListForAmend = await personInformationDetailRepository.ContactDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (PersonContactDetailsViewModelListForAmend != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in PersonContactDetailsViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened 
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                    personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }
                // PersonCourtCase
                // Amend Old PersonCourtCase
                if (result)
                {
                    IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelListForAmend = await personInformationDetailRepository.CourtCaseEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personCourtCaseViewModelListForAmend != null)
                    {
                        foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened 
                    if (personInformationParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        List<PersonCourtCaseViewModel> personCourtCaseViewModelList = new List<PersonCourtCaseViewModel>();
                        personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["CourtCase"];

                        if (personCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //PersonCreditRating
                //Amend Old PersonCourtCase
                if (result)
                {
                    IEnumerable<PersonCreditRatingViewModel> PersonCreditRatingViewModelListForAmend = await personInformationDetailRepository.CreditRatingEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (PersonCreditRatingViewModelListForAmend != null)
                    {
                        foreach (PersonCreditRatingViewModel viewModel in PersonCreditRatingViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Record Create For Amened 
                    if (personInformationParameterViewModel.EnableCreditRating == true)
                    {
                        List<PersonCreditRatingViewModel> personCreditRatingViewModelList = new List<PersonCreditRatingViewModel>();
                        personCreditRatingViewModelList = (List<PersonCreditRatingViewModel>)HttpContext.Current.Session["CreditRating"];

                        if (personCreditRatingViewModelList != null)
                        {
                            foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //PersonFamilyDetail
                if (personType == "INDVL")
                {
                    if (result)
                    {
                        //Amend Old Record
                        IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailsViewModelListForAmend = await personInformationDetailRepository.FamilyDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                        if (personFamilyDetailsViewModelListForAmend != null)
                        {
                            foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailsViewModelListForAmend)
                            {
                                result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        // New Record Create For Amened 
                        if (personInformationParameterViewModel.EnableFamilyDetails == true)
                        {
                            List<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = new List<PersonFamilyDetailViewModel>();
                            personFamilyDetailViewModelList = (List<PersonFamilyDetailViewModel>)HttpContext.Current.Session["FamilyDetail"];

                            if (personFamilyDetailViewModelList != null)
                            {
                                foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                                {
                                    result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // PersonFinancialAsset
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModelListForAmend = await personInformationDetailRepository.FinancialAssetEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personFinancialAssetViewModelListForAmend != null)
                    {
                        foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonFinancialAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
                        }
                    }
                    // New Record Create For Amened 

                    if (personInformationParameterViewModel.EnableFinancialAsset == true)
                    {
                        List<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = (List<PersonFinancialAssetViewModel>)HttpContext.Current.Session["FinancialAsset"];
                        if (personFinancialAssetViewModelList != null)
                        {
                            foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                            {
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
                                        //Db Storage
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
                }

                // PersonKYCDocument
                if (result)
                {
                    
                        // Amend Old Record (i.e. Existing In Db)
                        IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModelListForAmend = await personInformationDetailRepository.KYCDocumentEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

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
                    // PersonKYCDocument
                    if (result)
                    {
                        List<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = new List<PersonKYCDocumentViewModel>();
                        personKYCDocumentViewModelList = (List<PersonKYCDocumentViewModel>)HttpContext.Current.Session["KYCDocument"];

                        if (personKYCDocumentViewModelList != null)
                        {
                            foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _personViewModel.Remark;
                                viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;
                                string isPanCard = personDetailRepository.GetSysNameOfDocumentByDocumentId(viewModel.DocumentDocumentTypeId);
                                if (isPanCard == "PAN")
                                {
                                    isPanCardNumber = true;
                                }
                                result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.KYCDocumentUpload != "D")
                                {
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
                    
                }

                //EnableGSTRegistration
                if (result)
                {
                    if (_personViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey > 0)
                    {
                        if (isPanCardNumber)
                        {
                            if (personInformationParameterViewModel.EnableGSTRegistration == true && _personViewModel.EnableGSTRegistrationDetails == true)
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Delete);
                            }
                        }
                        else
                        {
                            if (isPanCardNumber)
                            {
                                if (personInformationParameterViewModel.EnableGSTRegistration == true && _personViewModel.EnableGSTRegistrationDetails == true)
                                {
                                    result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        // PersonGSTReturnDocument
                        // Amend Old Record (i.e. Existing In Db)
                        IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelForAmend = await personInformationDetailRepository.GSTReturnDocumentEntries(_personViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, StringLiteralValue.Reject);

                        if (personGSTReturnDocumentViewModelForAmend != null)
                        {
                            foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelForAmend)
                            {
                                result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
                        }

                        if (_personViewModel.PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument == true)
                        {
                            if (personInformationParameterViewModel.GSTDocumentUpload != "D")
                            {
                                List<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["GSTReturnDocument"];

                                if (personGSTRegistrationDetailViewModelList != null)
                                {
                                    foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                                    {
                                        string oldLocalStoragePath = viewModel.LocalStoragePath;

                                        string oldFileName = viewModel.NameOfFile;

                                        // EnableImmovableAssetDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                                        result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                    }
                                }
                            }
                        }
                    }
                }

                // PersonImmovableAsset
                // Amend Old PersonImmovableAsset
                if (result)
                {
                    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModelListForAmend = await personInformationDetailRepository.ImmovableAssetEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);
                    if (personImmovableAssetViewModelListForAmend != null)
                    {
                        foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonImmovableAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
                        }
                    }

                    //Create New Record For Amend
                    if (personInformationParameterViewModel.EnableImmovableAsset == true)
                    {
                        List<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = (List<PersonImmovableAssetViewModel>)HttpContext.Current.Session["ImmovableAsset"];

                        if (personImmovableAssetViewModelList != null)
                        {
                            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                            {

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
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                // PersonIncomeTaxDetail
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelListForAmend = await personInformationDetailRepository.IncomeTaxDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

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
                    
                    //Create New Record For Amend
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
                                viewModel.Remark = _personViewModel.Remark;
                                viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;

                                result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                                if (personIncomeTaxDetailViewModelList != null)
                                {
                                    if (personInformationParameterViewModel.IncomeTaxDocumentUpload != "D")
                                    {
                                        // EnableIncomeTaxDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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
                //PersonInsuranceDetail
                // Amend Old PersonInsuranceDetail
                if (result)
                {
                    IEnumerable<PersonInsuranceDetailViewModel> personAssetDetailViewModelListForAmend = await personInformationDetailRepository.InsuranceDetailEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personAssetDetailViewModelListForAmend != null)
                    {
                        foreach (PersonInsuranceDetailViewModel viewModel in personAssetDetailViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                    // Create New Record For PersonInsuranceDetail
                    if (personInformationParameterViewModel.EnableInsuranceDetail == true)
                    {
                        List<PersonInsuranceDetailViewModel> personAssetDetailViewModelList = (List<PersonInsuranceDetailViewModel>)HttpContext.Current.Session["InsuranceDetail"];

                        if (personAssetDetailViewModelList != null)
                        {
                            foreach (PersonInsuranceDetailViewModel viewModel in personAssetDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonMachineryAsset
                // Amend Old Fund
                if (result)
                {
                    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModelListForAmend = await personInformationDetailRepository.MachineryAssetEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personMachineryAssetViewModelListForAmend != null)
                    {
                        foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonMachineryAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
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
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.MachineryAssetDocumentUpload != "D")
                                {
                                    // EnableMachineryAssetDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                // PersonMovableAsset
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModelListForAmend = await personInformationDetailRepository.MovableAssetEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personMovableAssetViewModelListForAmend != null)
                    {
                        foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Amend);

                            if (viewModel.PersonMovableAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                            }
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
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Create);
                                if (personInformationParameterViewModel.MovableAssetDocumentUpload != "D")
                                {

                                    if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                //PersonPhotoSign
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (personInformationParameterViewModel.PhotoDocumentUpload != "D" || personInformationParameterViewModel.SignDocumentUpload != "D")
                        {
                            PersonPhotoSignViewModel personPhotoSignViewModelForAmend = await personInformationDetailRepository.PhotoSignEntry(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                            if (_personViewModel.PersonPhotoSignViewModel.PhotoPath != null || _personViewModel.PersonPhotoSignViewModel.SignPath != null)
                            {
                                personPhotoSignViewModelForAmend.PhotoPath = _personViewModel.PersonPhotoSignViewModel.PhotoPath;
                                personPhotoSignViewModelForAmend.SignPath = _personViewModel.PersonPhotoSignViewModel.SignPath;

                                //Photo
                                if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                                {
                                    if (_personViewModel.PersonPhotoSignViewModel.PhotoPath != null)
                                    {
                                        result = personDbContextRepository.AttachPhotoDocumentInLocalStorage(_personViewModel.PersonPhotoSignViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                    }
                                    else
                                    {
                                        _personViewModel.PersonPhotoSignViewModel.PhotoNameOfFile = personPhotoSignViewModelForAmend.PhotoNameOfFile;
                                        _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath = personPhotoSignViewModelForAmend.PhotoLocalStoragePath;
                                    }
                                }
                                else
                                    result = personDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_personViewModel.PersonPhotoSignViewModel, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);

                                //Sign
                                if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                {
                                    if (_personViewModel.PersonPhotoSignViewModel.SignPath != null)
                                    {
                                        result = personDbContextRepository.AttachSignDocumentInLocalStorage(_personViewModel.PersonPhotoSignViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                    }
                                    else
                                    {
                                        _personViewModel.PersonPhotoSignViewModel.SignNameOfFile = personPhotoSignViewModelForAmend.SignNameOfFile;
                                        _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath = personPhotoSignViewModelForAmend.SignLocalStoragePath;
                                    }
                                }
                                else
                                {
                                    result = personDbContextRepository.AttachSignDocumentInDatabaseStorage(_personViewModel.PersonPhotoSignViewModel, personPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                }
                                result = personDbContextRepository.AttachPersonPhotoSignData(_personViewModel.PersonPhotoSignViewModel, StringLiteralValue.Amend);

                            }
                            else
                            {
                                result = personDbContextRepository.AttachPersonPhotoSignData(_personViewModel.PersonPhotoSignViewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // EnableSMSAlert
                // Old Record Amended For Amened 
                if (result)
                {
                    IEnumerable<PersonSMSAlertViewModel> personBankDetailsViewModelListForAmend = await personInformationDetailRepository.SMSAlertEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personBankDetailsViewModelListForAmend != null)
                    {
                        foreach (PersonSMSAlertViewModel viewModel in personBankDetailsViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Amend);

                        }
                    }
                    // New Record Create For Amend
                    if (personInformationParameterViewModel.EnableSMSAlert == true)
                    {
                        List<PersonSMSAlertViewModel> personSMSAlertViewModelList = new List<PersonSMSAlertViewModel>();
                        personSMSAlertViewModelList = (List<PersonSMSAlertViewModel>)HttpContext.Current.Session["SMSAlert"];

                        if (personSMSAlertViewModelList != null)
                        {
                            foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }


                // EnableSocialMedia
                // Amend Old PersonSocialMedia
                if (result)
                {
                    IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModelListForAmend = await personInformationDetailRepository.SocialMediaEntries(_personViewModel.PersonPrmKey, StringLiteralValue.Reject);

                    if (personSocialMediaViewModelListForAmend != null)
                    {
                        foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelListForAmend)
                        {
                            result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Amend);

                        }
                    }
                    // New Record Create For Amend
                    if (personInformationParameterViewModel.EnableSocialMedia == true)
                    {
                        List<PersonSocialMediaViewModel> personSocialMediaViewModelList = new List<PersonSocialMediaViewModel>();
                        personSocialMediaViewModelList = (List<PersonSocialMediaViewModel>)HttpContext.Current.Session["SocialMedia"];

                        if (personSocialMediaViewModelList != null)
                        {
                            foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }
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


        public long GetPrmKeyById(Guid _personId)
        {
            return context.People
                    .Where(c => c.PersonId == _personId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<bool> GetSessionValues(PersonViewModel _personViewModel, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["AdditionalIncomeDetail"] = await personInformationDetailRepository.AdditionalIncomeDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["Address"] = await personInformationDetailRepository.AddressEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["AgricultureAsset"] = await personInformationDetailRepository.AgricultureAssetEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["BankDetail"] = await personInformationDetailRepository.BankDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["BoardOfDirectorRelation"] = await personInformationDetailRepository.BoardOfDirectorRelationEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["BorrowingDetail"] = await personInformationDetailRepository.BorrowingDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["ChronicDisease"] = await personInformationDetailRepository.ChronicDiseaseEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["ContactDetail"] = await personInformationDetailRepository.ContactDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["CourtCase"] = await personInformationDetailRepository.CourtCaseEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["CreditRating"] = await personInformationDetailRepository.CreditRatingEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["FamilyDetail"] = await personInformationDetailRepository.FamilyDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["FinancialAsset"] = await personInformationDetailRepository.FinancialAssetEntries(_personViewModel.PersonPrmKey, _entryType);
                if (_personViewModel.PersonGSTRegistrationDetailViewModel != null)
                    HttpContext.Current.Session["GSTReturnDocument"] = await personInformationDetailRepository.GSTReturnDocumentEntries(_personViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, _entryType);

                HttpContext.Current.Session["ImmovableAsset"] = await personInformationDetailRepository.ImmovableAssetEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["IncomeTaxDetail"] = await personInformationDetailRepository.IncomeTaxDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["InsuranceDetail"] = await personInformationDetailRepository.InsuranceDetailEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["KYCDocument"] = await personInformationDetailRepository.KYCDocumentEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["MachineryAsset"] = await personInformationDetailRepository.MachineryAssetEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["MovableAsset"] = await personInformationDetailRepository.MovableAssetEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["SMSAlert"] = await personInformationDetailRepository.SMSAlertEntries(_personViewModel.PersonPrmKey, _entryType);
                HttpContext.Current.Session["SocialMedia"] = await personInformationDetailRepository.SocialMediaEntries(_personViewModel.PersonPrmKey, _entryType);
                long personGroupPrmkey = GetPersonGroupPrmKeyByPersonPrmKey(_personViewModel.PersonPrmKey);
                HttpContext.Current.Session["GroupAuthorizedSignatory"] = await personInformationDetailRepository.GroupAuthorizedSignatoryEntries(personGroupPrmkey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public long GetPersonGroupPrmKeyByPersonPrmKey(long _personPrmKey)
        {
            return context.PersonGroups
                    .Where(c => c.PersonPrmKey == _personPrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }
        public async Task<bool> GetPersonMasterSessionValues(PersonMasterViewModel _personMasterViewModel, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["Address"] = await personInformationDetailRepository.AddressEntries(_personMasterViewModel.PersonPrmKey, _entryType);
                if (_personMasterViewModel.PersonGSTRegistrationDetailViewModel != null)
                    HttpContext.Current.Session["GSTReturnDocument"] = await personInformationDetailRepository.GSTReturnDocumentEntries(_personMasterViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, _entryType);

                HttpContext.Current.Session["KYCDocument"] = await personInformationDetailRepository.KYCDocumentEntries(_personMasterViewModel.PersonPrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<PersonIndexViewModel>> GetPersonIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonViewModel> GetPersonEntry(Guid _personId, string _entryType)
        {
            try
            {
                PersonViewModel personViewModel = await context.Database.SqlQuery<PersonViewModel>("SELECT * FROM dbo.GetPersonEntry (@PersonId, @EntriesType)", new SqlParameter("@PersonId", _personId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                long _personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);
                personViewModel.PersonPrefixViewModel = await personInformationDetailRepository.PrefixEntry(_personPrmKey, _entryType);
                personViewModel.PersonHomeBranchViewModel = await personInformationDetailRepository.HomeBranchEntry(_personPrmKey, _entryType);
                personViewModel.PersonGSTRegistrationDetailViewModel = await personInformationDetailRepository.GSTRegistrationDetailEntry(_personPrmKey, _entryType);
                personViewModel.PersonCommoditiesAssetViewModel = await personInformationDetailRepository.CommoditiesAssetEntry(_personPrmKey, _entryType);
                personViewModel.ForeignerViewModel = await personInformationDetailRepository.ForeignerEntry(_personPrmKey, _entryType);
                personViewModel.GuardianPersonViewModel = await personInformationDetailRepository.GuardianPersonEntry(_personPrmKey, _entryType);
                personViewModel.PersonPhotoSignViewModel = await personInformationDetailRepository.PhotoSignEntry(_personPrmKey, _entryType);
                personViewModel.PersonAdditionalDetailViewModel = await personInformationDetailRepository.AdditionalDetailEntry(_personPrmKey, _entryType);
                personViewModel.PersonEmploymentDetailViewModel = await personInformationDetailRepository.EmploymentDetailEntry(_personPrmKey, _entryType);
                personViewModel.PersonGroupViewModel = await personInformationDetailRepository.PersonGroupEntry(_personPrmKey, _entryType);

                return personViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonMasterViewModel> GetPersonMasterEntry(Guid _personId, string _entryType)
        {
            try
            {
                PersonMasterViewModel personMasterViewModel = await context.Database.SqlQuery<PersonMasterViewModel>("SELECT * FROM dbo.GetPersonMasterEntry (@PersonId, @EntriesType)", new SqlParameter("@PersonId", _personId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                long _personPrmKey = personDetailRepository.GetPersonPrmKeyById(_personId);
                personMasterViewModel.PersonPrefixViewModel = await personInformationDetailRepository.PrefixEntry(_personPrmKey, _entryType);
                personMasterViewModel.PersonGSTRegistrationDetailViewModel = await personInformationDetailRepository.GSTRegistrationDetailEntry(_personPrmKey, _entryType);
                personMasterViewModel.GuardianPersonViewModel = await personInformationDetailRepository.GuardianPersonEntry(_personPrmKey, _entryType);
                personMasterViewModel.PersonAdditionalDetailViewModel = await personInformationDetailRepository.AdditionalDetailEntry(_personPrmKey, _entryType);
                personMasterViewModel.PersonEmploymentDetailViewModel = await personInformationDetailRepository.EmploymentDetailEntry(_personPrmKey, _entryType);

                return personMasterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Save(PersonViewModel _personViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personViewModel.PersonAdditionalDetailViewModel.OccupationId);
                string personType = personDetailRepository.GetSysNameOfPersonTypeById(_personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);

                bool result;

                //Add Default Values When PersonType Is Not Individual
                if (personType != "INDVL")
                {
                    _personViewModel.FirstName = "None";
                    _personViewModel.TransFirstName = "None";
                    _personViewModel.MiddleName = "None";
                    _personViewModel.TransMiddleName = "None";
                    _personViewModel.LastName = "None";
                    _personViewModel.TransLastName = "None";
                    _personViewModel.DateOfBirth = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    _personViewModel.DateOfBirthOnDocument = new DateTime(1900, 01, 01, 0, 0, 0, 0);
                    _personViewModel.MotherName = "None";
                    _personViewModel.TransMotherName = "None";
                    _personViewModel.MothersMaidenName = "None";
                    _personViewModel.TransMothersMaidenName = "None";
                }
                

                result = personDbContextRepository.AttachPersonData(_personViewModel, StringLiteralValue.Create);

                if (result)
                {
                    if (personType != "INDVL")
                    {
                        _personViewModel.PersonPrefixViewModel.PrefixPrmKey = 1;
                    }
                    result = personDbContextRepository.AttachPersonPrefixData(_personViewModel.PersonPrefixViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    //If IsPolitician Toggleswitch Is Off Then Provide Default Values
                    if (_personViewModel.PersonAdditionalDetailViewModel.IsPolitician == false)
                    {
                        _personViewModel.PersonAdditionalDetailViewModel.PoliticialBackgroundDetails = "None";
                        _personViewModel.PersonAdditionalDetailViewModel.TransPoliticialBackgroundDetails = "None";
                    }

                    int age = configurationDetailRepository.GetAge(_personViewModel.DateOfBirth);

                    if (age < 16)
                    {
                        _personViewModel.PersonAdditionalDetailViewModel.MaritalStatusPrmKey = 2;
                    }
                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personViewModel.PersonAdditionalDetailViewModel, StringLiteralValue.Create);

                    // Check Person Is Salaried Or Not
                    if (occupation == "SLRD" && _personViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                    {
                        //If Salaried And IsEmployee Toggleswitch is Off Then Attach PersonEmploymentDetailViewModel
                        result = personDbContextRepository.AttachPersonEmploymentDetailData(_personViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Create);
                    }
                    else
                    {
                        _personViewModel.PersonEmploymentDetailViewModel.NameOfEmployer = "None";
                        _personViewModel.PersonEmploymentDetailViewModel.TransNameOfEmployer = "None";
                        _personViewModel.PersonEmploymentDetailViewModel.EPFNumber = "None";
                        _personViewModel.PersonEmploymentDetailViewModel.TransEPFNumber = "None";
                    }
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonHomeBranchData(_personViewModel.PersonHomeBranchViewModel, StringLiteralValue.Create);
                }
                // Person
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        int age = configurationDetailRepository.GetAge(_personViewModel.DateOfBirth);
                        if (age < 18)
                        {
                            result = personDbContextRepository.AttachGuardianPersonData(_personViewModel.GuardianPersonViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // EnableAdditionalIncomeDetail
                if (result)
                {
                    if (personInformationParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["AdditionalIncomeDetail"];

                        if (personAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonAddress
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

                // PersonAgricultureAsset
                if (result)
                {
                    if (personInformationParameterViewModel.EnableAgricultureAsset == true)
                    {
                        List<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelList = (List<PersonAgricultureAssetViewModel>)HttpContext.Current.Session["AgricultureAsset"];
                        if (personAgricultureAssetViewModelList != null)
                        {
                            foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.AgricultureAssetDocumentUpload != "D")
                                {
                                    //If Local Storage
                                    if (personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                                    result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // PersonBankDetail
                if (personInformationParameterViewModel.EnableBankingDetail == true)
                {
                    List<PersonBankDetailViewModel> personBankDetailViewModelList = (List<PersonBankDetailViewModel>)HttpContext.Current.Session["BankDetail"];

                    if (personBankDetailViewModelList != null)
                    {
                        foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;
                            string oldFileName = viewModel.NameOfFile;
                            viewModel.Remark = _personViewModel.Remark;
                            viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;

                            result = personDbContextRepository.AttachPersonBankDetailData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.BankStatementDocumentUpload != "D")
                            {
                                //If Local Storage
                                if (personInformationParameterViewModel.EnableBankStatementDocumentUploadInLocalStorage == true)
                                {
                                    //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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
                            
                        }
                    }
                }

                //PersonGroupViewModel
                if (result)
                {
                    if (personType != "INDVL")
                    {
                        result = personDbContextRepository.AttachPersonGroupData(_personViewModel.PersonGroupViewModel, StringLiteralValue.Create);


                        // PersonGroupAuthorizedSignatoryViewModel
                        List<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelList = (List<PersonGroupAuthorizedSignatoryViewModel>)HttpContext.Current.Session["GroupAuthorizedSignatory"];

                        if (personGroupAuthorizedSignatoryViewModelList != null)
                        {
                            foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.SignLocalStoragePath;

                                string oldFileName = viewModel.SignNameOfFile;

                                if (personInformationParameterViewModel.SignDocumentUpload != "D")
                                {
                                    if (viewModel.IsAuthorizedSignatory == true)
                                    {
                                        //If Local Storage
                                        if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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
                }

                //PersonBoardOfDirectorRelation
                if (result)
                {
                    List<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = (List<PersonBoardOfDirectorRelationViewModel>)HttpContext.Current.Session["BoardOfDirectorRelation"];

                    if (personBoardOfDirectorRelationViewModelList != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // EnableBorrowingDetail
                if (result)
                {
                    if (personInformationParameterViewModel.EnableBorrowingDetail == true)
                    {
                        List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];

                        if (personBorrowingDetailViewModelList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // EnableChronicDisease
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (personInformationParameterViewModel.EnableChronicDisease == true)
                        {
                            List<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = (List<PersonChronicDiseaseViewModel>)HttpContext.Current.Session["ChronicDisease"];

                            if (personChronicDiseaseViewModelList != null)
                            {
                                foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                                {
                                    result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // PersonContactDetail
                if (result)
                {
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //EnableCommoditiesAsset
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (personInformationParameterViewModel.EnableCommoditiesAsset == true)
                        {
                            result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personViewModel.PersonCommoditiesAssetViewModel, StringLiteralValue.Create);
                        }
                    }
                }


                // EnableCourtCaseDetail
                if (result)
                {
                    if (personInformationParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        List<PersonCourtCaseViewModel> personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["CourtCase"];

                        if (personCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // EnableCreditRating
                if (result)
                {
                    if (personInformationParameterViewModel.EnableCreditRating == true)
                    {
                        List<PersonCreditRatingViewModel> personCreditRatingViewModelList = (List<PersonCreditRatingViewModel>)HttpContext.Current.Session["CreditRating"];

                        if (personCreditRatingViewModelList != null)
                        {
                            foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // EnableFamilyDetails
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (personInformationParameterViewModel.EnableFamilyDetails == true)
                        {
                            List<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = (List<PersonFamilyDetailViewModel>)HttpContext.Current.Session["FamilyDetail"];

                            if (personFamilyDetailViewModelList != null)
                            {
                                foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                                {
                                    result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }
                }

                // PersonFinancialAsset
                if (result)
                {
                    if (personInformationParameterViewModel.EnableFinancialAsset == true)
                    {
                        List<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = (List<PersonFinancialAssetViewModel>)HttpContext.Current.Session["FinancialAsset"];
                        if (personFinancialAssetViewModelList != null)
                        {
                            foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                            {
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
                }

                // PersonKYCDocument
                if (result)
                {
                    List<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = new List<PersonKYCDocumentViewModel>();
                    personKYCDocumentViewModelList = (List<PersonKYCDocumentViewModel>)HttpContext.Current.Session["KYCDocument"];

                    if (personKYCDocumentViewModelList != null)
                    {
                        foreach (PersonKYCDocumentViewModel viewModel in personKYCDocumentViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;
                            viewModel.Remark = _personViewModel.Remark;
                            viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;
                            string isPanCard = personDetailRepository.GetSysNameOfDocumentByDocumentId(viewModel.DocumentDocumentTypeId);
                            if (isPanCard == "PAN")
                            {
                                isPanCardNumber = true;
                            }
                            result = personDbContextRepository.AttachPersonKYCData(viewModel, StringLiteralValue.Create);

                            if (personInformationParameterViewModel.KYCDocumentUpload != "D")
                            {
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
                    if (personInformationParameterViewModel.EnableGSTRegistration == true && _personViewModel.EnableGSTRegistrationDetails == true)
                    {
                        if (isPanCardNumber)
                        {
                            result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personViewModel.PersonGSTRegistrationDetailViewModel, StringLiteralValue.Create);
                            if (personInformationParameterViewModel.GSTDocumentUpload != "D")
                            {
                                // PersonGSTRegistrationDetail
                                if (_personViewModel.PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument == true)
                                {
                                    List<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["GSTReturnDocument"];

                                    if (personGSTRegistrationDetailViewModelList != null)
                                    {
                                        foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                                        {
                                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                                            string oldFileName = viewModel.NameOfFile;

                                            // EnableImmovableAssetDocumentUploadInLocalStorage
                                            if (personInformationParameterViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                                            {
                                                //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                                            result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
                // PersonImmovableAsset
                if (result)
                {
                    if (personInformationParameterViewModel.EnableImmovableAsset == true)
                    {
                        List<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = (List<PersonImmovableAssetViewModel>)HttpContext.Current.Session["ImmovableAsset"];

                        if (personImmovableAssetViewModelList != null)
                        {
                            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                            {
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
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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
                                        //Db Storage
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

                // PersonIncomeTaxDetail
                if (result)
                {
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
                                viewModel.Remark = _personViewModel.Remark;
                                viewModel.PersonPrmKey = _personViewModel.PersonPrmKey;

                                result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                                if (personIncomeTaxDetailViewModelList != null)
                                {
                                    if (personInformationParameterViewModel.IncomeTaxDocumentUpload != "D")
                                    {
                                        // EnableIncomeTaxDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                // EnableInsuranceDetail
                if (result)
                {
                    if (personInformationParameterViewModel.EnableInsuranceDetail == true)
                    {
                        List<PersonInsuranceDetailViewModel> personDocumentViewModelList = (List<PersonInsuranceDetailViewModel>)HttpContext.Current.Session["InsuranceDetail"];

                        if (personDocumentViewModelList != null)
                        {
                            foreach (PersonInsuranceDetailViewModel viewModel in personDocumentViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonMachineryAsset
                if (result)
                {
                    if (personInformationParameterViewModel.EnableMachineryAsset == true)
                    {
                        List<PersonMachineryAssetViewModel> personMachineryAssetViewModelList = (List<PersonMachineryAssetViewModel>)HttpContext.Current.Session["MachineryAsset"];

                        if (personMachineryAssetViewModelList != null)
                        {
                            foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, StringLiteralValue.Create);

                                if (personInformationParameterViewModel.MachineryAssetDocumentUpload != "D")
                                {
                                    // EnableMachineryAssetDocumentUploadInLocalStorage
                                    if (personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                // PersonMovableAsset
                if (result)
                {
                    if (personInformationParameterViewModel.EnableMovableAsset == true)
                    {
                        // Insert Record From Session Object
                        List<PersonMovableAssetViewModel> personMovableAssetViewModelList = (List<PersonMovableAssetViewModel>)HttpContext.Current.Session["MovableAsset"];

                        if (personMovableAssetViewModelList != null)
                        {
                            foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;

                                string oldFileName = viewModel.NameOfFile;

                                result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, StringLiteralValue.Create);
                                if (personInformationParameterViewModel.MovableAssetDocumentUpload != "D")
                                {

                                    if (personInformationParameterViewModel.EnableMovableAssetDocumentUploadInLocalStorage == true)
                                    {
                                        //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
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

                // EnablePhotoDocumentUploadInLocalStorage
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        if (personInformationParameterViewModel.PhotoDocumentUpload != "D")
                        {
                            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                            {
                                if (_personViewModel.PersonPhotoSignViewModel.PhotoPath != null)
                                {
                                    result = personDbContextRepository.AttachPhotoDocumentInLocalStorage(_personViewModel.PersonPhotoSignViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    _personViewModel.PersonPhotoSignViewModel.PhotoNameOfFile = "None";
                                    _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath = "None";
                                }
                            }
                            else
                            {
                                if (_personViewModel.PersonPhotoSignViewModel.PhotoPath != null)
                                {
                                    result = personDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_personViewModel.PersonPhotoSignViewModel, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    _personViewModel.PersonPhotoSignViewModel.PhotoNameOfFile = "None";
                                    _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath = "None";
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
                                    if (_personViewModel.PersonPhotoSignViewModel.SignPath != null)
                                    {
                                        result = personDbContextRepository.AttachSignDocumentInLocalStorage(_personViewModel.PersonPhotoSignViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        _personViewModel.PersonPhotoSignViewModel.SignNameOfFile = "None";
                                        _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath = "None";
                                    }
                                }
                                else
                                {
                                    if (_personViewModel.PersonPhotoSignViewModel.SignPath != null)
                                    {
                                        result = personDbContextRepository.AttachSignDocumentInDatabaseStorage(_personViewModel.PersonPhotoSignViewModel, null, StringLiteralValue.Create);
                                    }
                                    else
                                    {
                                        _personViewModel.PersonPhotoSignViewModel.SignNameOfFile = "None";
                                        _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath = "None";
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
                                    _personViewModel.PersonPhotoSignViewModel.PhotoNameOfFile = "None";
                                    _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath = "None";
                                    _personViewModel.PersonPhotoSignViewModel.PhotoFileCaption = "None";

                                }

                                if (personInformationParameterViewModel.SignDocumentUpload == "D")
                                {
                                    _personViewModel.PersonPhotoSignViewModel.SignNameOfFile = "None";
                                    _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath = "None";
                                    _personViewModel.PersonPhotoSignViewModel.SignFileCaption = "None";
                                }
                                result = personDbContextRepository.AttachPersonPhotoSignData(_personViewModel.PersonPhotoSignViewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }
               
                // EnableSMSAlert
                if (result)
                {
                    if (personInformationParameterViewModel.EnableSMSAlert == true)
                    {
                        List<PersonSMSAlertViewModel> personSMSAlertViewModelList = (List<PersonSMSAlertViewModel>)HttpContext.Current.Session["SMSAlert"];

                        if (personSMSAlertViewModelList != null)
                        {
                            foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Create);
                            }
                        }


                    }
                }

                // EnableSocialMedia
                if (result)
                {
                    if (personInformationParameterViewModel.EnableSocialMedia == true)
                    {
                        List<PersonSocialMediaViewModel> personSocialMediaViewModelList = (List<PersonSocialMediaViewModel>)HttpContext.Current.Session["SocialMedia"];

                        if (personSocialMediaViewModelList != null)
                        {
                            foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonViewModel _personViewModel, string _entryType)
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

                bool result = true;
                string occupation = personDetailRepository.GetSysNameOfOccupationById(_personViewModel.PersonAdditionalDetailViewModel.OccupationId);
                string personType = personDetailRepository.GetSysNameOfPersonTypeById(_personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);

                if (result)
                {
                    result = personDbContextRepository.AttachPersonData(_personViewModel, _entryType);
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonPrefixData(_personViewModel.PersonPrefixViewModel, _entryType);
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personViewModel.PersonAdditionalDetailViewModel, _entryType);
                    if (occupation == "SLRD" && _personViewModel.PersonAdditionalDetailViewModel.IsEmployee == false)
                    {
                        PersonEmploymentDetailViewModel personEmploymentDetailViewModel = await personInformationDetailRepository.EmploymentDetailEntry(_personViewModel.PersonPrmKey, _entriesType);
                        if (personEmploymentDetailViewModel != null)
                        {
                            result = personDbContextRepository.AttachPersonEmploymentDetailData(_personViewModel.PersonEmploymentDetailViewModel, _entryType);
                        }
                    }
                }

                if (result)
                {
                    result = personDbContextRepository.AttachPersonHomeBranchData(_personViewModel.PersonHomeBranchViewModel, _entryType);
                }

                //GuardianPerson
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        GuardianPersonViewModel guardianPersonViewModel = await personInformationDetailRepository.GuardianPersonEntry(_personViewModel.PersonPrmKey, _entriesType);
                        if (guardianPersonViewModel != null)
                        {
                            result = personDbContextRepository.AttachGuardianPersonData(_personViewModel.GuardianPersonViewModel, _entryType);
                        }
                    }
                }

                //PersonPhotoSign
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        PersonPhotoSignViewModel personPhotoSignViewModel = await personInformationDetailRepository.PhotoSignEntry(_personViewModel.PersonPrmKey, _entriesType);
                        if (personPhotoSignViewModel != null)
                        {
                            result = personDbContextRepository.AttachPersonPhotoSignData(_personViewModel.PersonPhotoSignViewModel, _entryType);
                        }
                    }
                }

                //PersonAdditionalIncomeDetail
                if (result)
                {
                    IEnumerable<PersonAdditionalIncomeDetailViewModel> _personAdditionalIncomeDetailViewModelList = await personInformationDetailRepository.AdditionalIncomeDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (_personAdditionalIncomeDetailViewModelList != null)
                    {
                        foreach (PersonAdditionalIncomeDetailViewModel viewModel in _personAdditionalIncomeDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, _entryType);
                        }
                    }
                }

                //PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModelViewModelList = await personInformationDetailRepository.AddressEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personAddressViewModelViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                        }
                    }
                }

                //PersonAgricultureAsset
                if (result)
                {
                    IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModelList = await personInformationDetailRepository.AgricultureAssetEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personAgricultureAssetViewModelList != null)
                    {
                        foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAgricultureAssetData(viewModel, _entryType);

                            if (viewModel.PersonAgricultureAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonAgricultureAssetDocumentData(viewModel, personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);

                            }
                        }
                    }
                }

                // PersonBankDetail
                if (result)
                {
                    IEnumerable<PersonBankDetailViewModel> personBankDetailViewModelList = await personInformationDetailRepository.BankDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personBankDetailViewModelList != null)
                    {
                        foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBankDetailData(viewModel, _entryType);

                            if (viewModel.PersonBankDetailDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonBankDetailDocumentData(viewModel, personInformationParameterViewModel.BankStatementDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                            }
                        }
                    }
                }

                //PersonGroupViewModel
                if (result)
                {
                    if (personType != "INDVL")
                    {
                        PersonGroupViewModel personGroupViewModel = await personInformationDetailRepository.PersonGroupEntry(_personViewModel.PersonPrmKey, _entriesType);
                        if (personGroupViewModel != null)
                        {
                            result = personDbContextRepository.AttachPersonGroupData(personGroupViewModel, _entryType);
                        }

                        // PersonGroupAuthorizedSignatory
                        IEnumerable<PersonGroupAuthorizedSignatoryViewModel> personGroupAuthorizedSignatoryViewModelList = await personInformationDetailRepository.GroupAuthorizedSignatoryEntries(personGroupViewModel.PersonGroupPrmKey, _entriesType);

                        if (personGroupAuthorizedSignatoryViewModelList != null)
                        {
                            foreach (PersonGroupAuthorizedSignatoryViewModel viewModel in personGroupAuthorizedSignatoryViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonGroupAuthorizedSignatoryData(viewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath, viewModel.SignNameOfFile, _entryType);
                            }
                        }
                    }
                }

                //PersonBoardOfDirectorRelation
                if (result)
                {
                    IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = await personInformationDetailRepository.BoardOfDirectorRelationEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personBoardOfDirectorRelationViewModelList != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, _entryType);
                        }
                    }
                }

                //PersonBorrowingDetail
                if (result)
                {
                    IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelsList = await personInformationDetailRepository.BorrowingDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personBorrowingDetailViewModelsList != null)
                    {
                        foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelsList)
                        {
                            result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, _entryType);
                        }
                    }
                }

                //PersonChronicDetails
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        IEnumerable<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = await personInformationDetailRepository.ChronicDiseaseEntries(_personViewModel.PersonPrmKey, _entriesType);

                        if (personChronicDiseaseViewModelList != null)
                        {
                            foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, _entryType);
                            }
                        }
                    }
                }

                //PersonCommoditiesAsset
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        PersonCommoditiesAssetViewModel personCommoditiesAssetViewModel = await personInformationDetailRepository.CommoditiesAssetEntry(_personViewModel.PersonPrmKey, _entriesType);
                        if (personCommoditiesAssetViewModel != null)
                        {
                            result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personViewModel.PersonCommoditiesAssetViewModel, _entryType);
                        }
                    }
                }

                //PersonContactDetail
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelList = await personInformationDetailRepository.ContactDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                        }
                    }
                }

                //PersonCourtCase
                if (result)
                {
                    IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelList = await personInformationDetailRepository.CourtCaseEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personCourtCaseViewModelList != null)
                    {
                        foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, _entryType);
                        }
                    }
                }

                //PersonCreditRating
                if (result)
                {
                    IEnumerable<PersonCreditRatingViewModel> personCreditRatingViewModelList = await personInformationDetailRepository.CreditRatingEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personCreditRatingViewModelList != null)
                    {
                        foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, _entryType);
                        }
                    }
                }

                //PersonFamilyDetail
                if (result)
                {
                    if (personType == "INDVL")
                    {
                        IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = await personInformationDetailRepository.FamilyDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                        if (personFamilyDetailViewModelList != null)
                        {
                            foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                            {
                                result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                //PersonFinancialAsset
                if (result)
                {
                    IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModelList = await personInformationDetailRepository.FinancialAssetEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personFinancialAssetViewModelList != null)
                    {
                        foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonFinancialAssetData(viewModel, _entryType);

                            if (viewModel.PersonFinancialAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonFinancialAssetDocumentData(viewModel, personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                            }
                        }
                    }
                }

                //PersonGSTRegistrationDetail
                if (result)
                {
                    PersonGSTRegistrationDetailViewModel personGSTRegistrationDetailViewModel = await personInformationDetailRepository.GSTRegistrationDetailEntry(_personViewModel.PersonPrmKey, _entriesType);
                    if (personGSTRegistrationDetailViewModel != null)
                    {
                        result = personDbContextRepository.AttachPersonGSTRegistrationDetailData(_personViewModel.PersonGSTRegistrationDetailViewModel, _entryType);
                    }
                }
                if (result)
                {
                    IEnumerable<PersonGSTReturnDocumentViewModel> personGSTRegistrationDetailViewModelList = await personInformationDetailRepository.GSTReturnDocumentEntries(_personViewModel.PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey, _entriesType);

                    if (personGSTRegistrationDetailViewModelList != null)
                    {
                        foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTRegistrationDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonGSTReturnDocumentData(viewModel, personInformationParameterViewModel.GSTDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                        }
                    }
                }

                //PersonImmovable
                if (result)
                {
                    IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModelList = await personInformationDetailRepository.ImmovableAssetEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personImmovableAssetViewModelList != null)
                    {
                        foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonImmovableAssetData(viewModel, _entryType);

                            if (viewModel.PersonImmovableAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonImmovableAssetDocumentData(viewModel, personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                            }
                        }
                    }
                }

                //PersonIncomeTaxDetail
                if (result)
                {
                    IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = await personInformationDetailRepository.IncomeTaxDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personIncomeTaxDetailViewModelList != null)
                    {
                        foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, _entryType);

                            if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                            }
                        }
                    }
                }

                //PersonInsuranceDetail
                if (result)
                {
                    IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModelList = await personInformationDetailRepository.InsuranceDetailEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personInsuranceDetailViewModelList != null)
                    {
                        foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, _entryType);
                        }
                    }
                }

                // PersonKYCDocument
                if (result)
                {
                        IEnumerable<PersonKYCDocumentViewModel> personKYCDocumentViewModelList = await personInformationDetailRepository.KYCDocumentEntries(_personViewModel.PersonPrmKey, _entriesType);

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

                //PersonMachinaryASset
                if (result)
                {
                    IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModelList = await personInformationDetailRepository.MachineryAssetEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personMachineryAssetViewModelList != null)
                    {
                        foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonMachineryAssetData(viewModel, _entryType);

                            if (viewModel.PersonMachineryAssetDocumentPrmKey > 0)
                            {
                                result = personDbContextRepository.AttachPersonMachineryAssetDocumentData(viewModel, personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                            }

                        }
                    }
                }

                //PersonMovableAsset
                if (result)
                {
                    IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModelList = await personInformationDetailRepository.MovableAssetEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personMovableAssetViewModelList != null)
                    {
                        foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonMovableAssetData(viewModel, _entryType);

                            if (viewModel.PersonMovableAssetDocumentPrmKey > 0)
                                result = personDbContextRepository.AttachPersonMovableAssetDocumentData(viewModel, personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                        }
                    }
                }

                //PersonSMSAlert
                if (result)
                {
                    IEnumerable<PersonSMSAlertViewModel> personSMSAlertViewModelList = await personInformationDetailRepository.SMSAlertEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personSMSAlertViewModelList != null)
                    {
                        foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, _entryType);
                        }
                    }
                }

                //PersonSocialMedia
                if (result)
                {
                    IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModelList = await personInformationDetailRepository.SocialMediaEntries(_personViewModel.PersonPrmKey, _entriesType);

                    if (personSocialMediaViewModelList != null)
                    {
                        foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, _entryType);
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
    }
}
