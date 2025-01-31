using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using System.Linq;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Person/PersonMaster")]
    public class PersonMasterController : Controller
    {
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonRepository personRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;

        public PersonMasterController(IPersonMasterRepository _personMasterRepository, IPersonDetailRepository _personDetailRepository, IPersonInformationParameterRepository _personInformationParameterRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonRepository _personRepository)
        {
            personDetailRepository = _personDetailRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
            personRepository = _personRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMasterViewModel personMasterViewModel = await personRepository.GetPersonMasterEntry(personId, StringLiteralValue.Reject);

            bool data = await personRepository.GetPersonMasterSessionValues(personMasterViewModel, StringLiteralValue.Reject);

            if (personMasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personMasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonMasterViewModel _personMasterViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_personMasterViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_personMasterViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMasterViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMasterViewModel.PersonId);

                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personMasterRepository.Amend(_personMasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personMasterRepository.VerifyRejectDelete(_personMasterViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "PersonMaster");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personMasterViewModel.PersonId);
        }

        [NonAction]
        private async Task ClearModelStateOfDataTableFields(PersonMasterViewModel _personMasterViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonAddressViewModel,PersonKYCDocumentViewModel,PersonGSTReturnDocumentViewModel";

            // Get Record Of All Master Table And Transaction Table
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            string marritalStatus = personDetailRepository.GetSysNameOfMaritalStatusById(_personMasterViewModel.PersonAdditionalDetailViewModel.MaritalStatusId);
            string occupation = personDetailRepository.GetSysNameOfOccupationById(_personMasterViewModel.PersonAdditionalDetailViewModel.OccupationId);

            int age = DateTime.Now.Year - _personMasterViewModel.DateOfBirth.Year;
            if (age > 18)
            {
                errorViewModelName = errorViewModelName + ",GuardianPersonViewModel";
            }

            if (personInformationParameterViewModel.EnableGSTRegistration == false || _personMasterViewModel.EnableGSTRegistrationDetails == false)
            {
                errorViewModelName = errorViewModelName + ",PersonGSTRegistrationDetailViewModel";
                ModelState["PersonGSTRegistrationDetailViewModel.PersonGSTReturnDocumentViewModel.AssessmentYear"]?.Errors?.Clear();
                ModelState["PersonGSTRegistrationDetailViewModel.PersonGSTReturnDocumentViewModel.TaxAmount"]?.Errors?.Clear();
            }
            //Clear Toggleswitch Value Required Error
            ModelState["PersonGSTRegistrationDetailViewModel.IsApplicableEWayBill"]?.Errors?.Clear();
            ModelState["PersonGSTRegistrationDetailViewModel.UploadGSTReturnDocument"]?.Errors?.Clear();

            if (marritalStatus != "MARRID")
            {
                ModelState["PersonAdditionalDetailViewModel.TransLifePartnerName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.TransLifePartnerMaidenName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.LifePartnerName"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.LifePartnerMaidenName"]?.Errors?.Clear();
            }

            if (_personMasterViewModel.PersonAdditionalDetailViewModel.IsPolitician == false)
            {
                ModelState["PersonAdditionalDetailViewModel.TransPoliticialBackgroundDetails"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PoliticialBackgroundDetails"]?.Errors?.Clear();
            }

            if (occupation != "SLRD" || _personMasterViewModel.PersonAdditionalDetailViewModel.IsEmployee ==true)
            {
                errorViewModelName = errorViewModelName + ",PersonEmployementDetailViewModel";
            }

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonViewModel.PersonPrmKey"]?.Errors?.Clear();
                ModelState["PersonAdditionalDetailViewModel.PersonAdditionalDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAddressViewModel.PersonAddressPrmKey"]?.Errors?.Clear();
                ModelState["PersonKYCDocumentViewModel.PersonKYCDocumentPrmKey"]?.Errors?.Clear();
                ModelState["PersonGSTRegistrationDetailViewModel.PersonGSTRegistrationDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonGSTReturnDocumentViewModel.PersonGSTReturnDocumentPrmKey"]?.Errors?.Clear();
                ModelState["GuardianPersonViewModel.GuardianPersonTranslationPrmKey"]?.Errors?.Clear();
                ModelState["GuardianPersonViewModel.GuardianPersonPrmKey"]?.Errors?.Clear();
                ModelState["PersonEmployementDetailViewModel.PersonEmployementDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonEmployementDetailViewModel.PersonEmployementDetailTranslationPrmKey"]?.Errors?.Clear();
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

        [HttpPost]
        public ActionResult GetUniqueInfoNumberStatus(long personInformationNumber)
        {
            bool data = personMasterRepository.GetPersonInfoNumber(personInformationNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMasterRepository.GetIndex(StringLiteralValue.Verify);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personMasterRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonMasterViewModel personMasterViewModel = await personRepository.GetPersonMasterEntry(personId, StringLiteralValue.Verify);

            bool data = await personRepository.GetPersonMasterSessionValues(personMasterViewModel, StringLiteralValue.Verify);

            if (personMasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personMasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonMasterViewModel _personMasterViewModel)
        {
            await ClearModelStateOfDataTableFields(_personMasterViewModel, StringLiteralValue.Modify);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMasterViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMasterViewModel.PersonId);

                bool result = await personMasterRepository.Modify(_personMasterViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonMaster");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personMasterViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {

            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMasterRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("PersonMasterSaveDataTables")]
        public ActionResult PersonMasterSaveDataTables(List<PersonAddressViewModel> _address)
        {
            HttpContext.Session.Add("Address", _address);
            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("PersonMasterImageSaveDataTables")]
        public ActionResult PersonMasterImageSaveDataTables(List<PersonGSTReturnDocumentViewModel> _gSTRegistrationDetail,
                                                            List<PersonKYCDocumentViewModel> _kYCDocument)

        {
            HttpContext.Session.Add("GSTReturnDocument", _gSTRegistrationDetail);
            HttpContext.Session.Add("KYCDocument", _kYCDocument);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {

            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMasterRepository.GetIndex(StringLiteralValue.Unverified);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMasterViewModel personMasterViewModel = await personRepository.GetPersonMasterEntry(personId, StringLiteralValue.Unverified);

            bool data = await personRepository.GetPersonMasterSessionValues(personMasterViewModel, StringLiteralValue.Unverified);

            if (personMasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personMasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonMasterViewModel _personMasterViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_personMasterViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_personMasterViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMasterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personMasterViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMasterViewModel.PersonId);

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personMasterRepository.VerifyRejectDelete(_personMasterViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _personMasterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personMasterRepository.VerifyRejectDelete(_personMasterViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personMasterViewModel.PersonId);
        }
    }
}