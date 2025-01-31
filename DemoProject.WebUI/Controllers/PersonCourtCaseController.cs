using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;

//Modified By Dhanashri Wagh 19/09/20224
namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonCourtCase")]
    public class PersonCourtCaseController : Controller
    {
        private readonly IPersonCourtCaseRepository personCourtCaseRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonCourtCaseController(IPersonCourtCaseRepository _personCourtCaseRepository, IPersonMasterRepository _personMasterRepository)
        {
            personCourtCaseRepository = _personCourtCaseRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["CourtCase"] = await personCourtCaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);

            PersonCourtCaseViewModel personCourtCaseViewModel = new PersonCourtCaseViewModel();
            IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModels = (IEnumerable<PersonCourtCaseViewModel>)HttpContext.Session["CourtCase"];

            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModels)
            {
                personCourtCaseViewModel = viewModel;
                break;
            }
            return View(personCourtCaseViewModel);
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonCourtCaseViewModel _personCourtCaseViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personCourtCaseViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personCourtCaseViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personCourtCaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCourtCaseViewModel.PersonId);

                    bool result = await personCourtCaseRepository.Amend(_personCourtCaseViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonCourtCase");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personCourtCaseRepository.VerifyRejectDelete(_personCourtCaseViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonCourtCase") }, JsonRequestBehavior.AllowGet);
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

            return View(_personCourtCaseViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonCourtCaseViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonCourtCaseViewModel.PersonCourtCasePrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCourtCaseRepository.GetIndex(StringLiteralValue.Verify);

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
            
            HttpContext.Session["CourtCase"] = await personCourtCaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Verify);
            if (await personCourtCaseRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonCourtCaseViewModel personCourtCaseViewModel = new PersonCourtCaseViewModel();
            IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModels = (IEnumerable<PersonCourtCaseViewModel>)HttpContext.Session["CourtCase"];

            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModels)
            {
                personCourtCaseViewModel = viewModel;
                break;
            }
            return View(personCourtCaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonCourtCaseViewModel _personCourtCaseViewModel)
        {
             ClearModelStateOfDataTableFields(_personCourtCaseViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCourtCaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCourtCaseViewModel.PersonId);

                bool result = await personCourtCaseRepository.Modify(_personCourtCaseViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonCourtCase");
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

            return View(_personCourtCaseViewModel);
        }
        
        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCourtCaseRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SavePersonCourtCaseDataTable")]
        public ActionResult SavePersonCourtCaseDataTable(List<PersonCourtCaseViewModel> _courtCase)
        {
            HttpContext.Session.Add("CourtCase", _courtCase);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCourtCaseRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["CourtCase"] = await personCourtCaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonCourtCaseViewModel personCourtCaseViewModel = new PersonCourtCaseViewModel();
            IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModels = (IEnumerable<PersonCourtCaseViewModel>)HttpContext.Session["CourtCase"];

            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModels)
            {
                personCourtCaseViewModel = viewModel;
                break;
            }
            return View(personCourtCaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonCourtCaseViewModel _personCourtCaseViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personCourtCaseViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personCourtCaseViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCourtCaseViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personCourtCaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCourtCaseViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personCourtCaseRepository.VerifyRejectDelete(_personCourtCaseViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCourtCase"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personCourtCaseViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personCourtCaseRepository.VerifyRejectDelete(_personCourtCaseViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCourtCase"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonCourtCase");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personCourtCaseViewModel.PersonId);
        }
    }
}