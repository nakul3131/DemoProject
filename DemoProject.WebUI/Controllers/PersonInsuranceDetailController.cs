using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonInsuranceDetail")]
    public class PersonInsuranceDetailController : Controller
    {
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonInsuranceDetailRepository personInsuranceDetailRepository;

        public PersonInsuranceDetailController(IPersonMasterRepository _personMasterRepository, IPersonInsuranceDetailRepository _personInsuranceDetailRepository)
        {
            personMasterRepository = _personMasterRepository;
            personInsuranceDetailRepository = _personInsuranceDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["InsuranceDetail"] = await personInsuranceDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);

            PersonInsuranceDetailViewModel personInsuranceDetailViewModel = new PersonInsuranceDetailViewModel();

            IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModels = (IEnumerable<PersonInsuranceDetailViewModel>)HttpContext.Session["InsuranceDetail"];

            foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModels)
            {
                personInsuranceDetailViewModel = viewModel;
                break;
            }
            return View(personInsuranceDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personInsuranceDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personInsuranceDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personInsuranceDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personInsuranceDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personInsuranceDetailRepository.Amend(_personInsuranceDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonInsuranceDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personInsuranceDetailRepository.VerifyRejectDelete(_personInsuranceDetailViewModel , StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonInsuranceDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personInsuranceDetailViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonInsuranceDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonInsuranceDetailViewModel.PersonInsuranceDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personInsuranceDetailRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["InsuranceDetail"] = await personInsuranceDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Verify);

            if (await personInsuranceDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonInsuranceDetailViewModel personInsuranceDetailViewModel = new PersonInsuranceDetailViewModel();

            IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModels = (IEnumerable<PersonInsuranceDetailViewModel>)HttpContext.Session["InsuranceDetail"];

            foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModels)
            {
                personInsuranceDetailViewModel = viewModel;
                break;
            }
            return View(personInsuranceDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personInsuranceDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personInsuranceDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personInsuranceDetailViewModel.PersonId);

                bool result = await personInsuranceDetailRepository.Modify(_personInsuranceDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonInsuranceDetail");
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

            return View(_personInsuranceDetailViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personInsuranceDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveInsuranceDataTable")]
        public ActionResult SaveInsuranceDataTable(List<PersonInsuranceDetailViewModel> _insuranceDetail)
        {
            HttpContext.Session.Add("InsuranceDetail", _insuranceDetail);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personInsuranceDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["InsuranceDetail"] = await personInsuranceDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInsuranceDetailViewModel personInsuranceDetailViewModel = new PersonInsuranceDetailViewModel();

            IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModels = (IEnumerable<PersonInsuranceDetailViewModel>)HttpContext.Session["InsuranceDetail"];

            foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModels)
            {
                personInsuranceDetailViewModel = viewModel;
                break;
            }
            return View(personInsuranceDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personInsuranceDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personInsuranceDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                _personInsuranceDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personInsuranceDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personInsuranceDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personInsuranceDetailRepository.VerifyRejectDelete(_personInsuranceDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonInsuranceDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personInsuranceDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personInsuranceDetailRepository.VerifyRejectDelete(_personInsuranceDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonInsuranceDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonInsuranceDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personInsuranceDetailViewModel.PersonId);
        }

    }
}