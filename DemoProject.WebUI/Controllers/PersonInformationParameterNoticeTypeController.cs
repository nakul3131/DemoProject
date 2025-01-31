using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/PersonInformationParameterNoticeType")]
    public class PersonInformationParameterNoticeTypeController : Controller
    {
        private readonly IPersonInformationParameterNoticeTypeRepository personInformationParameterNoticeTypeRepository;

        public PersonInformationParameterNoticeTypeController(IPersonInformationParameterNoticeTypeRepository _personInformationParameterNoticeTypeRepository)
        {
            personInformationParameterNoticeTypeRepository = _personInformationParameterNoticeTypeRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            PersonInformationParameterNoticeTypeViewModel personInformationParameterNoticeTypeViewModel = await personInformationParameterNoticeTypeRepository.GetPersonInformationParameterNoticeTypeEntry(StringLiteralValue.Reject);
            bool data = await personInformationParameterNoticeTypeRepository.GetSessionValues(StringLiteralValue.Verify);

            if (personInformationParameterNoticeTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personInformationParameterNoticeTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_personInformationParameterNoticeTypeViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personInformationParameterNoticeTypeRepository.Amend(_personInformationParameterNoticeTypeViewModel);

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
                    bool result = await personInformationParameterNoticeTypeRepository.Delete(_personInformationParameterNoticeTypeViewModel);

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

            return View(_personInformationParameterNoticeTypeViewModel);
        }

        private void ClearModelStateOfDataTableFields(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            ModelState["NoticeTypeId"]?.Errors?.Clear();
            ModelState["EnableNoticeInRegionalLanguage"]?.Errors?.Clear();
            ModelState["MaximumResendsOnFailure"]?.Errors?.Clear();
            ModelState["ActivationDate"]?.Errors?.Clear();
            ModelState["Note"]?.Errors?.Clear();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModels = await personInformationParameterNoticeTypeRepository.GetPersonInformationParameterNoticeTypeIndex();

            return View(personInformationParameterNoticeTypeViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await personInformationParameterNoticeTypeRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonInformationParameterNoticeTypeViewModel personInformationParameterNoticeTypeViewModel = await personInformationParameterNoticeTypeRepository.GetPersonInformationParameterNoticeTypeEntry(StringLiteralValue.Verify);
            bool data = await personInformationParameterNoticeTypeRepository.GetSessionValues(StringLiteralValue.Verify);

            return View(personInformationParameterNoticeTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            ClearModelStateOfDataTableFields(_personInformationParameterNoticeTypeViewModel);

            if (ModelState.IsValid)
            {
                bool result = await personInformationParameterNoticeTypeRepository.Save(_personInformationParameterNoticeTypeViewModel);

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

            return View(_personInformationParameterNoticeTypeViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<PersonInformationParameterNoticeTypeViewModel> _personInformationParameterNoticeType)
        {
            HttpContext.Session.Add("PersonInformationParameterNoticeType", _personInformationParameterNoticeType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            PersonInformationParameterNoticeTypeViewModel personInformationParameterNoticeTypeViewModel = await personInformationParameterNoticeTypeRepository.GetPersonInformationParameterNoticeTypeEntry(StringLiteralValue.Unverified);
            bool data = await personInformationParameterNoticeTypeRepository.GetSessionValues(StringLiteralValue.Unverified);

            if (personInformationParameterNoticeTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personInformationParameterNoticeTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _personInformationParameterNoticeTypeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personInformationParameterNoticeTypeRepository.Verify(_personInformationParameterNoticeTypeViewModel);

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
                    _personInformationParameterNoticeTypeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personInformationParameterNoticeTypeRepository.Reject(_personInformationParameterNoticeTypeViewModel);

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

            return View(_personInformationParameterNoticeTypeViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            PersonInformationParameterNoticeTypeViewModel personInformationParameterNoticeTypeViewModel = await personInformationParameterNoticeTypeRepository.GetPersonInformationParameterNoticeTypeEntry(StringLiteralValue.Verify);
            bool data = await personInformationParameterNoticeTypeRepository.GetSessionValues(StringLiteralValue.Verify);

            return View(personInformationParameterNoticeTypeViewModel);
        }
    }
}