using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonDeath")]
    public class PersonDeathController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonDeathRepository personDeathRepository;
        private readonly IPersonDeathDocumentRepository personDeathDocumentRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonDeathController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonMasterRepository _personMasterRepository, IPersonDeathRepository _personDeathRepository, IPersonDeathDocumentRepository _personDeathDocumentRepository)
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personMasterRepository = _personMasterRepository;
            personDeathRepository = _personDeathRepository;
            personDeathDocumentRepository = _personDeathDocumentRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid PersonId)
        {
            PersonDeathViewModel personDeathViewModel = await personDeathRepository.GetViewModelForReject(personMasterRepository.GetPersonPrmKeyById(PersonId));

            HttpContext.Session["PersonDeathDocument"] = await personDeathDocumentRepository.GetRejectedEntries(personMasterRepository.GetPersonPrmKeyById(PersonId));

            if (personDeathViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personDeathViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonDeathViewModel _personDeathViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_personDeathViewModel);
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personDeathRepository.Amend(_personDeathViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonDeath");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personDeathRepository.Delete(_personDeathViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonDeath") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personDeathViewModel.PersonId);
        }

        private void ClearModelStateOfDataTableFields(PersonDeathViewModel _personDeathViewModel)
        {
            ModelState[nameof(_personDeathViewModel.DeathDate)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.DocumentTypeId)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.NameAsPerDocument)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.DocumentNumber)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.SequenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.DateOfIssue)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.IssuingAuthority)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.PlaceOfIssue)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.Note)]?.Errors?.Clear();
            ModelState[nameof(_personDeathViewModel.FileCaption)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid PersonId)
        {

            PersonDeathViewModel personDeathViewModel = await personDeathRepository.GetViewModelForCreate(personMasterRepository.GetPersonPrmKeyById(PersonId));

            if (personDeathViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personDeathViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(PersonDeathViewModel _personDeathViewModel)
        {
            //_personDeathViewModel.PersonPrmKey = personMasterRepository.GetPrmKeyById(_personDeathViewModel.PersonId);
            ClearModelStateOfDataTableFields(_personDeathViewModel);
            if (ModelState.IsValid)
            {
                bool result = await personDeathRepository.Save(_personDeathViewModel);

                if (result)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    return RedirectToAction("Default", "Home");
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

            return View(_personDeathViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<PersonDeathViewModel> villageTownCityViewModels = await personDeathRepository.GetIndexWithCreateModifyOperationStatus();

            if (villageTownCityViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(villageTownCityViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid PersonId)
        {
            HttpContext.Session["PersonDeathDocument"] = await personDeathDocumentRepository.GetVerifiedEntries(personMasterRepository.GetPersonPrmKeyById(PersonId));

            PersonDeathViewModel personDeathViewModel = await personDeathRepository.GetViewModelForVerified(personMasterRepository.GetPersonPrmKeyById(PersonId));

            if (personDeathViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personDeathViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonDeathViewModel _personDeathViewModel)
        {
            ClearModelStateOfDataTableFields(_personDeathViewModel);
            if (ModelState.IsValid)
            {

                bool result = await personDeathRepository.Modify(_personDeathViewModel);

                if (result)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("Default", "Home");
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

            return View(_personDeathViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonDeathViewModel> villageTownCityViewModels = await personDeathRepository.GetIndexOfRejectedEntries();

            if (villageTownCityViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(villageTownCityViewModels);
        }

        [HttpPost]
        [Route("SavePersonDeathDataTables")]
        public ActionResult SavePersonDeathDataTables(List<PersonDeathDocumentViewModel> _PersonDeathDocument)
        {
            HttpContext.Session.Add("PersonDeathDocument", _PersonDeathDocument);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedISORecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonDeathViewModel> villageTownCityViewModels = await personDeathRepository.GetIndexOfUnVerifiedEntries();

            if (villageTownCityViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(villageTownCityViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid PersonId)
        {
            HttpContext.Session["PersonDeathDocument"] = await personDeathDocumentRepository.GetUnverifiedEntries(personMasterRepository.GetPersonPrmKeyById(PersonId));

            PersonDeathViewModel personDeathViewModel = await personDeathRepository.GetViewModelForUnverified(personMasterRepository.GetPersonPrmKeyById(PersonId));

            if (personDeathViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personDeathViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonDeathViewModel _personDeathViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_personDeathViewModel);
            if (ModelState.IsValid)
            {
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personDeathRepository.Verify(_personDeathViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonDeath"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "PersonDeath"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _personDeathViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personDeathRepository.Reject(_personDeathViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonDeath"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "PersonDeath"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonDeath");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personDeathViewModel.PersonId);
        }
    }
}