using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;

//Modified By Dhanashri Wagh 19/09/20224
namespace DemoProject.WebUI.Controllers
{ 
    [RoutePrefix("Employee/PersonInformation/PersonChronicDisease")]
    public class PersonChronicDiseaseController : Controller
    {
        private readonly IPersonChronicDiseaseRepository personChronicDiseaseRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonChronicDiseaseController(IPersonChronicDiseaseRepository _personChronicDiseaseRepository, IPersonMasterRepository _personMasterRepository)
        {
            personChronicDiseaseRepository = _personChronicDiseaseRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["ChronicDisease"] = await personChronicDiseaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonChronicDiseaseViewModel personChronicDiseaseViewModel = new PersonChronicDiseaseViewModel();
            IEnumerable<PersonChronicDiseaseViewModel> personChronicDiseaseViewModels = (IEnumerable<PersonChronicDiseaseViewModel>)HttpContext.Session["ChronicDisease"];

            foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModels)
            {
                personChronicDiseaseViewModel = viewModel;
                break;
            }
            return View(personChronicDiseaseViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personChronicDiseaseViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personChronicDiseaseViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personChronicDiseaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personChronicDiseaseViewModel.PersonId);

                    bool result = await personChronicDiseaseRepository.Amend(_personChronicDiseaseViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonChronicDisease");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personChronicDiseaseRepository.VerifyRejectDelete(_personChronicDiseaseViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonChronicDisease") }, JsonRequestBehavior.AllowGet);
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

            return View(_personChronicDiseaseViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonChronicDiseaseViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonChronicDiseaseViewModel.PersonChronicDiseasePrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personChronicDiseaseRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["ChronicDisease"] = await personChronicDiseaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personChronicDiseaseRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonChronicDiseaseViewModel personChronicDiseaseViewModel = new PersonChronicDiseaseViewModel();
            IEnumerable<PersonChronicDiseaseViewModel> personChronicDiseaseViewModels = (IEnumerable<PersonChronicDiseaseViewModel>)HttpContext.Session["ChronicDisease"];

            foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModels)
            {
                personChronicDiseaseViewModel = viewModel;
                break;
            }
            return View(personChronicDiseaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel)
        {
             ClearModelStateOfDataTableFields(_personChronicDiseaseViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personChronicDiseaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personChronicDiseaseViewModel.PersonId);

                bool result = await personChronicDiseaseRepository.Modify(_personChronicDiseaseViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonChronicDisease");
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

            return View(_personChronicDiseaseViewModel);
        }


        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personChronicDiseaseRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveChronicDiseaseDataTable")]
        public ActionResult SaveChronicDiseaseDataTable(List<PersonChronicDiseaseViewModel> _chronicDisease)
        {
            HttpContext.Session.Add("ChronicDisease", _chronicDisease);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personChronicDiseaseRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["ChronicDisease"] = await personChronicDiseaseRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonChronicDiseaseViewModel personChronicDiseaseViewModel = new PersonChronicDiseaseViewModel();
            IEnumerable<PersonChronicDiseaseViewModel> personChronicDiseaseViewModels = (IEnumerable<PersonChronicDiseaseViewModel>)HttpContext.Session["ChronicDisease"];

            foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModels)
            {
                personChronicDiseaseViewModel = viewModel;
                break;
            }
            return View(personChronicDiseaseViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personChronicDiseaseViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personChronicDiseaseViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personChronicDiseaseViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personChronicDiseaseRepository.VerifyRejectDelete(_personChronicDiseaseViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonChronicDisease"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personChronicDiseaseViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personChronicDiseaseRepository.VerifyRejectDelete(_personChronicDiseaseViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonChronicDisease"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonChronicDisease");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personChronicDiseaseViewModel.PersonId);
        }

    }
}