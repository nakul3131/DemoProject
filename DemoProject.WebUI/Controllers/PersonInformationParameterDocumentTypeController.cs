using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/PersonInformationParameterDocumentType")]
    public class PersonInformationParameterDocumentTypeController : Controller
    {
        private readonly IPersonInformationParameterDocumentTypeRepository personInformationParameterDocumentTypeRepository;

        public PersonInformationParameterDocumentTypeController(IPersonInformationParameterDocumentTypeRepository _personInformationParameterDocumentTypeRepository)
        {
            personInformationParameterDocumentTypeRepository = _personInformationParameterDocumentTypeRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            PersonInformationParameterDocumentTypeViewModel personInformationParameterDocumentTypeViewModel = await personInformationParameterDocumentTypeRepository.GetPersonInformationParameterDocumentTypeEntry(StringLiteralValue.Reject);
            bool data = await personInformationParameterDocumentTypeRepository.GetSessionValues(StringLiteralValue.Reject);

            if (personInformationParameterDocumentTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personInformationParameterDocumentTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_personInformationParameterDocumentTypeViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personInformationParameterDocumentTypeRepository.Amend(_personInformationParameterDocumentTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("Default", "Home");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personInformationParameterDocumentTypeRepository.Delete(_personInformationParameterDocumentTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_personInformationParameterDocumentTypeViewModel);
        }

        private void ClearModelStateOfDataTableFields(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            ModelState["DocumentTypeId"]?.Errors?.Clear();
            ModelState["IsMandatory"]?.Errors?.Clear();
            ModelState["ActivationDate"]?.Errors?.Clear();
            ModelState["Note"]?.Errors?.Clear();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModels = await personInformationParameterDocumentTypeRepository.GetPersonInformationParameterDocumentTypeIndex();

            return View(personInformationParameterDocumentTypeViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await personInformationParameterDocumentTypeRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonInformationParameterDocumentTypeViewModel personInformationParameterDocumentTypeViewModel = await personInformationParameterDocumentTypeRepository.GetPersonInformationParameterDocumentTypeEntry(StringLiteralValue.Verify);
            bool data = await personInformationParameterDocumentTypeRepository.GetSessionValues(StringLiteralValue.Verify);

            return View(personInformationParameterDocumentTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            ClearModelStateOfDataTableFields(_personInformationParameterDocumentTypeViewModel);

            if (ModelState.IsValid)
            {
                bool result = await personInformationParameterDocumentTypeRepository.Save(_personInformationParameterDocumentTypeViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_personInformationParameterDocumentTypeViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<PersonInformationParameterDocumentTypeViewModel> _personInformationParameterDocumentType)
        {
            HttpContext.Session.Add("PersonInformationParameterDocumentType", _personInformationParameterDocumentType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            PersonInformationParameterDocumentTypeViewModel personInformationParameterDocumentTypeViewModel = await personInformationParameterDocumentTypeRepository.GetPersonInformationParameterDocumentTypeEntry(StringLiteralValue.Unverified);
            
            // Get PersonInformationParameterDocumentType In Session Object For Future Use
            bool data = await personInformationParameterDocumentTypeRepository.GetSessionValues(StringLiteralValue.Unverified);

            if (personInformationParameterDocumentTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personInformationParameterDocumentTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _personInformationParameterDocumentTypeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personInformationParameterDocumentTypeRepository.Verify(_personInformationParameterDocumentTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _personInformationParameterDocumentTypeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personInformationParameterDocumentTypeRepository.Reject(_personInformationParameterDocumentTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_personInformationParameterDocumentTypeViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            PersonInformationParameterDocumentTypeViewModel personInformationParameterDocumentTypeViewModel = await personInformationParameterDocumentTypeRepository.GetPersonInformationParameterDocumentTypeEntry(StringLiteralValue.Verify);

            // Get PersonInformationParameterDocumentType In Session Object For Future Use
            bool data = await personInformationParameterDocumentTypeRepository.GetSessionValues(StringLiteralValue.Verify);

            return View(personInformationParameterDocumentTypeViewModel);
        }
    }
}