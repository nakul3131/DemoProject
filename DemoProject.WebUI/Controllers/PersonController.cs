using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.WebUI.Utility;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Person/Information")]
    public class PersonController : Controller
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonRepository personRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public PersonController(IPersonDetailRepository _personDetailRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonRepository _personRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            personDetailRepository = _personDetailRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personRepository = _personRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid PersonId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonViewModel personViewModel = await personRepository.GetPersonEntry(PersonId, StringLiteralValue.Reject);

            bool data = await personRepository.GetSessionValues(personViewModel, StringLiteralValue.Reject);

            string personType = personDetailRepository.GetSysNameOfPersonTypeById(personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);

            if (personViewModel.PersonPhotoSignViewModel != null)
            {
                if (personType == "INDVL")
                {
                    if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                    {
                        personViewModel.PersonPhotoSignViewModel.PhotoCopy = GetPersonPhotoSignFromPath("Photo", personViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath);
                    }
                    if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                    {
                        personViewModel.PersonPhotoSignViewModel.PersonSign = GetPersonPhotoSignFromPath("Sign", personViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath);
                    }
                }
                else
                {
                    personViewModel.PersonPhotoSignViewModel.PhotoCopy = new byte[123];
                    personViewModel.PersonPhotoSignViewModel.PersonSign = new byte[123];
                }
            }

            if (personViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonViewModel _personViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_personViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_personViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personRepository.Amend(_personViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.Amend;

                        return RedirectToAction("RejectedIndex", "Person");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personRepository.VerifyRejectDelete(_personViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.Delete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Person"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "Person");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personViewModel.PersonId);
        }

        [NonAction]
        private async Task ClearModelStateOfDataTableFields(PersonViewModel _personViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonAddressViewModel, PersonAdditionalIncomeDetailViewModel,PersonAgricultureAssetViewModel, PersonContactDetailViewModel,PersonBankDetailViewModel, PersonBoardOfDirectorRelationViewModel, PersonBorrowingDetailViewModel, PersonChronicDiseaseViewModel,PersonCourtCaseViewModel,PersonFamilyDetailViewModel, PersonFinancialAssetViewModel, PersonImmovableAssetViewModel,PersonIncomeTaxDetailViewModel,PersonInsuranceDetailViewModel,PersonKYCDocumentViewModel,PersonMovableAssetViewModel,PersonMachineryAssetViewModel,PersonSMSAlertViewModel,PersonCreditRatingViewModel,PersonGSTRegistrationDetailViewModel,PersonGSTReturnDocumentViewModel,PersonGroupAuthorizedSignatoryViewModel,PersonSocialMediaViewModel";

            // Get Record Of All Master Table And Transaction Table
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            string marritalStatus = personDetailRepository.GetSysNameOfMaritalStatusById(_personViewModel.PersonAdditionalDetailViewModel.MaritalStatusId);
            string occupation = personDetailRepository.GetSysNameOfOccupationById(_personViewModel.PersonAdditionalDetailViewModel.OccupationId);
            string personType = personDetailRepository.GetSysNameOfPersonTypeById(_personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);


            //PersonAdditionalDetailViewModel.PersonTypeId

            if (personInformationParameterViewModel.EnableAutoGeneratePersonInformationNumber == true)
            {
                ModelState["PersonInformationNumber"]?.Errors?.Clear();
            }

            int age = DateTime.Now.Year - _personViewModel.DateOfBirth.Year;
            if (age > 18)
            {
                errorViewModelName = errorViewModelName + ",GuardianPersonViewModel";
            }

            if (personInformationParameterViewModel.EnableCommoditiesAsset == false)
            {
                errorViewModelName = errorViewModelName + ",PersonCommoditiesAssetViewModel";

            }

            if (personInformationParameterViewModel.EnableGSTRegistration == false)
            {
                errorViewModelName = errorViewModelName + ",PersonGSTRegistrationDetailViewModel";
                ModelState["PersonGSTRegistrationDetailViewModel.PersonGSTReturnDocumentViewModel.AssessmentYear"]?.Errors?.Clear();
                ModelState["PersonGSTRegistrationDetailViewModel.PersonGSTReturnDocumentViewModel.TaxAmount"]?.Errors?.Clear();

            }

            // PersonPhotoSign
            // Also Clear ViewModel If Not Changing Photo Or Sign On Amend Operation
            if ((personInformationParameterViewModel.SignDocumentUpload == "D") || (personInformationParameterViewModel.PhotoDocumentUpload == "O") || _personViewModel.PersonPhotoSignViewModel.PersonPhotoSignPrmKey > 0)
            {
                errorViewModelName = errorViewModelName + ",PersonPhotoSignViewModel";
            }

            if (personInformationParameterViewModel.EnableKYCDocumentUploadInLocalStorage == false)
            {
                errorViewModelName = errorViewModelName + ",PersonKYCDocumentViewModel";
                ModelState["PersonKYCDocumentViewModel.NameOfFile"]?.Errors?.Clear();
                ModelState["PersonKYCDocumentViewModel.LocalStoragePath"]?.Errors?.Clear();
            }

            if (marritalStatus != "MARRID")
            {
                ModelState["PersonAdditionalDetailViewModel.TransLifePartnerName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.TransLifePartnerMaidenName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.LifePartnerName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.LifePartnerMaidenName"]?.Errors?.Clear();
            }

            // Person Type Visibility Condition
            if (personType != "INDVL")
            {
                errorViewModelName = errorViewModelName + ",PersonPhotoSignViewModel";
                errorViewModelName = errorViewModelName + ",PersonFamilyDetailViewModel";
                errorViewModelName = errorViewModelName + ",PersonChronicDiseaseViewModel";
                errorViewModelName = errorViewModelName + ",PersonCommoditiesAssetViewModel";
                errorViewModelName = errorViewModelName + ",GuardianPersonViewModel";
                ModelState["PersonPrefixViewModel.PrefixId"]?.Errors?.Clear();
                ModelState["PersonViewModel.FirstName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransFirstName"]?.Errors?.Clear();
                ModelState["PersonViewModel.MiddleName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransMiddleName"]?.Errors?.Clear();
                ModelState["PersonViewModel.LastName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransLastName"]?.Errors?.Clear();
                ModelState["PersonViewModel.DateOfBirth"]?.Errors?.Clear();
                ModelState["PersonViewModel.DateOfBirthOnDocument"]?.Errors?.Clear();
                ModelState["PersonViewModel.MotherName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransMotherName"]?.Errors?.Clear();
                ModelState["PersonViewModel.MothersMaidenName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransMothersMaidenName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.GenderId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.BirthCityId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.MaritalStatusId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.BloodGroupId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PovertyStatusId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PhysicalStatusId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.CastCategoryId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.EducationalQualificationId"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.OccupationId"]?.Errors?.Clear();

            }
            else
            {
                errorViewModelName = errorViewModelName + ",PersonGroupViewModel";

                ModelState["PersonViewModel.FullName"]?.Errors?.Clear();
                ModelState["PersonViewModel.TransFullName"]?.Errors?.Clear();

            }
            if (_personViewModel.PersonAdditionalDetailViewModel.IsPolitician == false)
            {
                ModelState["PersonAdditionalDetailViewModel.TransPoliticialBackgroundDetails"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PoliticialBackgroundDetails"]?.Errors?.Clear();
            }

            if (occupation != "SLRD" || _personViewModel.PersonAdditionalDetailViewModel.IsEmployee == true)
            {
                errorViewModelName = errorViewModelName + ",PersonEmploymentDetailViewModel";

            }
            ModelState["PersonAdditionalDetailViewModel.VIPRank"]?.Errors?.Clear();
            
            // Clear Commodities Asset Errors For Optional
            errorViewModelName = errorViewModelName + ",PersonCommoditiesAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {

                ModelState["PersonViewModel.PersonPrmKey"]?.Errors?.Clear();
                ModelState["PersonAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PersonAdditionalDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAgricultureAssetViewModel.PersonAgricultureAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonContactDetailViewModel.PersonContactDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAddressViewModel.PersonAddressPrmKey"]?.Errors?.Clear();
                ModelState["PersonBankDetailViewModel.PersonBankDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonBoardOfDirectorRelationViewModel.PersonBoardOfDirectorRelationPrmKey"]?.Errors?.Clear();
                ModelState["PersonBorrowingDetailViewModel.PersonBorrowingDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonChronicDiseaseViewModel.PersonChronicDiseasePrmKey"]?.Errors?.Clear();
                ModelState["PersonCourtCaseViewModel.PersonCourtCasePrmKey"]?.Errors?.Clear();
                ModelState["PersonEmployementDetailViewModel.PersonEmployementDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonEmployementDetailViewModel.PersonEmployementDetailTranslationPrmKey"]?.Errors?.Clear();
                ModelState["PersonFamilyDetailViewModel.PersonFamilyDetailServicePrmKey"]?.Errors?.Clear();
                ModelState["PersonFinancialAssetViewModel.PersonFinancialAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonImmovableAssetViewModel.PersonImmovableAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonInsuranceDetailViewModel.PersonInsuranceDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonKYCDocumentViewModel.PersonKYCDocumenPrmKey"]?.Errors?.Clear();
                ModelState["PersonMovableAssetViewModel.PersonMovableAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonMachineryAssetViewModel.PersonMachineryAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonSMSAlertViewModel.PersonSMSAlertPrmKey"]?.Errors?.Clear();
                ModelState["PersonSocialMediaViewModel.PersonSocialMediaPrmKey"]?.Errors?.Clear();
                ModelState["PersonCommoditiesAssetViewModel.PersonCommoditiesAssetPrmKey"]?.Errors?.Clear();
                ModelState["PersonPhotoSignViewModel.PersonPhotoSignPrmKey"]?.Errors?.Clear();
                ModelState["ForeignerViewModel.ForeignerPersonPrmKey"]?.Errors?.Clear();
                ModelState["PersonGroupAuthorizedSignatoryViewModel.PersonGroupAuthorizedSignatoryPrmKey"]?.Errors?.Clear();
                ModelState["GuardianPersonViewModel.GuardianPersonPrmKey"]?.Errors?.Clear();
                ModelState["GuardianPersonTranslationViewModel.GuardianPersonTranslationPrmKey"]?.Errors?.Clear();
            }

            // Clear DataTable Error
            foreach (var key in ModelState.Keys)
            {
                var viewModelPropertyArray = key.Split('.');
                int arrayLength = viewModelPropertyArray.Length;

                if (arrayLength > 1)
                {
                    var viewModel = viewModelPropertyArray[arrayLength - 2];

                    if (errorViewModelName.Contains(viewModel) || key.Contains("Enable"))
                    {
                        ModelState[key]?.Errors?.Clear();
                    }
                }
                else
                    ModelState[key]?.Errors?.Clear();
            }

        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(PersonViewModel _personViewModel)
        {
            await ClearModelStateOfDataTableFields(_personViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await personRepository.Save(_personViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Person");
                    }

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_personViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personRepository.GetPersonIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("PersonSaveDataTables")]
        public ActionResult PersonSaveDataTables(List<PersonAddressViewModel> _address,
                                                List<PersonBoardOfDirectorRelationViewModel> _boardOfDirectorRelation,
                                                List<PersonBorrowingDetailViewModel> _borrowingDetail,
                                                List<PersonChronicDiseaseViewModel> _chronicDisease,
                                                List<PersonContactDetailViewModel> _contactDetail,
                                                List<PersonCourtCaseViewModel> _courtCase,
                                                List<PersonCreditRatingViewModel> _creditRating,
                                                List<PersonFamilyDetailViewModel> _familyDetail,
                                                List<PersonAdditionalIncomeDetailViewModel> _incomeDetails,
                                                List<PersonInsuranceDetailViewModel> _insurance,
                                                List<PersonSocialMediaViewModel> _socialMedia,
                                                List<PersonSMSAlertViewModel> _sMSAlert)
        {
            HttpContext.Session.Add("Address", _address);
            HttpContext.Session.Add("BoardOfDirectorRelation", _boardOfDirectorRelation);
            HttpContext.Session.Add("BorrowingDetail", _borrowingDetail);
            HttpContext.Session.Add("ChronicDisease", _chronicDisease);
            HttpContext.Session.Add("ContactDetail", _contactDetail);
            HttpContext.Session.Add("CourtCase", _courtCase);
            HttpContext.Session.Add("CreditRating", _creditRating);
            HttpContext.Session.Add("FamilyDetail", _familyDetail);
            HttpContext.Session.Add("AdditionalIncomeDetail", _incomeDetails);
            HttpContext.Session.Add("InsuranceDetail", _insurance);
            HttpContext.Session.Add("SMSAlert", _sMSAlert);
            HttpContext.Session.Add("SocialMedia", _socialMedia);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("ImageSaveDataTable")]
        public ActionResult AgreecultureImageSaveDataTables(List<PersonAgricultureAssetViewModel> _agricultureAsset,
                                                            List<PersonBankDetailViewModel> _bankDetail,
                                                            List<PersonFinancialAssetViewModel> _financialAsset,
                                                            List<PersonGSTReturnDocumentViewModel> _gSTRegistrationDetail,
                                                            List<PersonImmovableAssetViewModel> _immovableAsset,
                                                            List<PersonIncomeTaxDetailViewModel> _incomeTaxDetail,
                                                            List<PersonKYCDocumentViewModel> _kYCDocument,
                                                            List<PersonMachineryAssetViewModel> _machineryAsset,
                                                            List<PersonMovableAssetViewModel> _movableAsset,
                                                            List<PersonGroupAuthorizedSignatoryViewModel> _groupAuthorizedSignatory)
        {
            HttpContext.Session.Add("AgricultureAsset", _agricultureAsset);
            HttpContext.Session.Add("BankDetail", _bankDetail);
            HttpContext.Session.Add("FinancialAsset", _financialAsset);
            HttpContext.Session.Add("GSTReturnDocument", _gSTRegistrationDetail);
            HttpContext.Session.Add("ImmovableAsset", _immovableAsset);
            HttpContext.Session.Add("IncomeTaxDetail", _incomeTaxDetail);
            HttpContext.Session.Add("KYCDocument", _kYCDocument);
            HttpContext.Session.Add("MachineryAsset", _machineryAsset);
            HttpContext.Session.Add("MovableAsset", _movableAsset);
            HttpContext.Session.Add("GroupAuthorizedSignatory", _groupAuthorizedSignatory);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personRepository.GetPersonIndex(StringLiteralValue.Unverified);

            if (personIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(personIndexViewModels);
        }


        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personRepository.GetPersonIndex(StringLiteralValue.Verify);

            if (personIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid PersonId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonViewModel personViewModel = await personRepository.GetPersonEntry(PersonId, StringLiteralValue.Unverified);
            bool data = await personRepository.GetSessionValues(personViewModel, StringLiteralValue.Unverified);

            string personType = personDetailRepository.GetSysNameOfPersonTypeById(personViewModel.PersonAdditionalDetailViewModel.PersonTypeId);

            if (personViewModel.PersonPhotoSignViewModel != null)
            {
                if (personType == "INDVL")
                {
                    if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                    {
                        personViewModel.PersonPhotoSignViewModel.PhotoCopy = GetPersonPhotoSignFromPath("Photo", personViewModel, personInformationParameterViewModel.PhotoDocumentLocalStoragePath);
                    }
                    if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                    {
                        personViewModel.PersonPhotoSignViewModel.PersonSign = GetPersonPhotoSignFromPath("Sign", personViewModel, personInformationParameterViewModel.SignDocumentLocalStoragePath);
                    }
                }
                else
                {
                    personViewModel.PersonPhotoSignViewModel.PhotoCopy = new byte[123];
                    personViewModel.PersonPhotoSignViewModel.PersonSign = new byte[123];
                }
            }

            if (personViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonViewModel _personViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_personViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_personViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)

            {
                _personViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personRepository.VerifyRejectDelete(_personViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.Verify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Person"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personRepository.VerifyRejectDelete(_personViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.Reject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Person"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Person");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personViewModel.PersonId);
        }

        [NonAction]
        private byte[] GetPersonPhotoSignFromPath(string _photoSign, PersonViewModel _personViewModel,string _serverPath)
        {

            string filePath;
            string fileName;
            byte[] imagecode = null;
            byte[] fileBytes;

            if (_photoSign == "Photo")
            {
                filePath = _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath;

                //Get Full Destination Path
                string fullFilePath = personDbContextRepository.GetFullFilePath(filePath, _personViewModel.PersonPhotoSignViewModel.PhotoNameOfFile);

                //Check File Exist Or Not
                bool fileExistance = personDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personViewModel.PersonPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _personViewModel.PersonPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _personViewModel.PersonPhotoSignViewModel.PhotoLocalStoragePath;
                    string path;
                    if (originalPath != "None")
                    {
                        // Find The Last Index Of '/' to Replace File Name
                        int lastSlashIndex = originalPath.LastIndexOf('/');

                         path = originalPath.Substring(0, lastSlashIndex);
                    }
                    else
                    {
                        path = _serverPath;
                    }
                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;
                    //Get Full Destination Path
                    fullFilePath = personDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personViewModel.PersonPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _personViewModel.PersonPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
            }
            else
            {
                filePath = _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath;
                //Get Full Destination Path
                string fullFilePath = personDbContextRepository.GetFullFilePath(filePath, _personViewModel.PersonPhotoSignViewModel.SignNameOfFile);

                //Check File Exist Or Not
                bool fileExistance = personDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personViewModel.PersonPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _personViewModel.PersonPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _personViewModel.PersonPhotoSignViewModel.SignLocalStoragePath;
                    string path;
                    if (originalPath != "None")
                    {
                        // Find The Last Index Of '/' to Replace File Name
                        int lastSlashIndex = originalPath.LastIndexOf('/');

                         path = originalPath.Substring(0, lastSlashIndex);
                    }
                    else
                    {
                        path = _serverPath;
                    }
                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;

                    //Get Full Destination Path
                    fullFilePath = personDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personViewModel.PersonPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _personViewModel.PersonPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
            }

            return imagecode;
        }
    }
}