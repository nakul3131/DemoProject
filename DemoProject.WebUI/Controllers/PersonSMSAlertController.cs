using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonSMSAlert")]
    public class PersonSMSAlertController : Controller
    {
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonSMSAlertRepository personSMSAlertsRepository;

        public PersonSMSAlertController(IPersonMasterRepository _personMasterRepository, IPersonSMSAlertRepository _personSMSAlertsRepository)
        {
            personMasterRepository = _personMasterRepository;
            personSMSAlertsRepository = _personSMSAlertsRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["SMSAlert"] = await personSMSAlertsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonSMSAlertViewModel personSMSAlertViewModel = new PersonSMSAlertViewModel();

            IEnumerable<PersonSMSAlertViewModel> personSMSAlertViewModels = (IEnumerable<PersonSMSAlertViewModel>)HttpContext.Session["SMSAlert"];

            foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModels)
            {
                personSMSAlertViewModel = viewModel;
                break;
            }
            return View(personSMSAlertViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonSMSAlertViewModel _personSMSAlertViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personSMSAlertViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personSMSAlertViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSMSAlertViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSMSAlertViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personSMSAlertsRepository.Amend(_personSMSAlertViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonSMSAlert");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personSMSAlertsRepository.VerifyRejectDelete(_personSMSAlertViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonSMSAlert") }, JsonRequestBehavior.AllowGet);
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

            return View(_personSMSAlertViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonSMSAlertViewModel _personSMSAlertViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonSMSAlertViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonSMSAlertViewModel.PersonSMSAlertPrmKey"]?.Errors?.Clear();
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
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSMSAlertsRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["SMSAlert"] = await personSMSAlertsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personSMSAlertsRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonSMSAlertViewModel personSMSAlertViewModel = new PersonSMSAlertViewModel();

            IEnumerable<PersonSMSAlertViewModel> personSMSAlertViewModels = (IEnumerable<PersonSMSAlertViewModel>)HttpContext.Session["SMSAlert"];

            foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModels)
            {
                personSMSAlertViewModel = viewModel;
                break;
            }

            return View(personSMSAlertViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonSMSAlertViewModel _personSMSAlertViewModel)
        {
             ClearModelStateOfDataTableFields(_personSMSAlertViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSMSAlertViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSMSAlertViewModel.PersonId);

                bool result = await personSMSAlertsRepository.Modify(_personSMSAlertViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonSMSAlert");
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

            return View(_personSMSAlertViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSMSAlertsRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveSMSAlertDataTables")]
        public ActionResult SaveSMSAlertDataTables(List<PersonSMSAlertViewModel> _sMSAlert)
        {
            HttpContext.Session.Add("SMSAlert", _sMSAlert);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSMSAlertsRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["SMSAlert"] = await personSMSAlertsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonSMSAlertViewModel personSMSAlertViewModel = new PersonSMSAlertViewModel();

            IEnumerable<PersonSMSAlertViewModel> personSMSAlertViewModels = (IEnumerable<PersonSMSAlertViewModel>)HttpContext.Session["SMSAlert"];

            foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModels)
            {
                personSMSAlertViewModel = viewModel;
                break;
            }
            return View(personSMSAlertViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonSMSAlertViewModel _personSMSAlertViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personSMSAlertViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personSMSAlertViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSMSAlertViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personSMSAlertViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSMSAlertViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {

                    bool result = await personSMSAlertsRepository.VerifyRejectDelete(_personSMSAlertViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonSMSAlert"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personSMSAlertViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personSMSAlertsRepository.VerifyRejectDelete(_personSMSAlertViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonSMSAlert"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonSMSAlert");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personSMSAlertViewModel.PersonId);
        }

    }
}