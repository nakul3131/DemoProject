using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonAdditionalIncomeDetail")]
    public class PersonAdditionalIncomeDetailController : Controller
    {
        private readonly IPersonAdditionalIncomeDetailRepository personAdditionalIncomeDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonAdditionalIncomeDetailController(IPersonAdditionalIncomeDetailRepository _personAdditionalIncomeDetailRepository, IPersonMasterRepository _personMasterRepository)
        {
            personAdditionalIncomeDetailRepository = _personAdditionalIncomeDetailRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonAdditionalIncomeDetailViewModel personAdditionalIncomeDetailViewModel = new PersonAdditionalIncomeDetailViewModel();

            HttpContext.Session["AdditionalIncomeDetail"] = await personAdditionalIncomeDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);
            IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModels = (IEnumerable<PersonAdditionalIncomeDetailViewModel>)HttpContext.Session["AdditionalIncomeDetail"];

            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModels)
            {
                personAdditionalIncomeDetailViewModel = viewModel;
                break;
            }

            return View(personAdditionalIncomeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonAdditionalIncomeDetailViewModel _personadditionalIncomeDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personadditionalIncomeDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personadditionalIncomeDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personadditionalIncomeDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personadditionalIncomeDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personAdditionalIncomeDetailRepository.Amend(_personadditionalIncomeDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonAdditionalIncomeDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personAdditionalIncomeDetailRepository.VerifyRejectDelete(_personadditionalIncomeDetailViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonAdditionalIncomeDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personadditionalIncomeDetailViewModel.PersonId);
        }
        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonAdditionalIncomeDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAdditionalIncomeDetailRepository.GetIndex(StringLiteralValue.Verify);

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
            PersonAdditionalIncomeDetailViewModel personAdditionalIncomeDetailViewModel = new PersonAdditionalIncomeDetailViewModel();

            HttpContext.Session["AdditionalIncomeDetail"] = await personAdditionalIncomeDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personAdditionalIncomeDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModels = (IEnumerable<PersonAdditionalIncomeDetailViewModel>)HttpContext.Session["AdditionalIncomeDetail"];

            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModels)
            {
                personAdditionalIncomeDetailViewModel = viewModel;
                break;
            }

            return View(personAdditionalIncomeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonAdditionalIncomeDetailViewModel _personadditionalIncomeDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personadditionalIncomeDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personadditionalIncomeDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personadditionalIncomeDetailViewModel.PersonId);

                bool result = await personAdditionalIncomeDetailRepository.Modify(_personadditionalIncomeDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonAdditionalIncomeDetail");
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

            return View(_personadditionalIncomeDetailViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAdditionalIncomeDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveIncomeDetailDataTable")]
        public ActionResult SaveIncomeDetailDataTable(List<PersonAdditionalIncomeDetailViewModel> _additionalIncomeDetail)
        {
            HttpContext.Session.Add("AdditionalIncomeDetail", _additionalIncomeDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAdditionalIncomeDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonAdditionalIncomeDetailViewModel personAdditionalIncomeDetailViewModel = new PersonAdditionalIncomeDetailViewModel();
            HttpContext.Session["AdditionalIncomeDetail"] = await personAdditionalIncomeDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Unverified);
            IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModels = (IEnumerable<PersonAdditionalIncomeDetailViewModel>)HttpContext.Session["AdditionalIncomeDetail"];

            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModels)
            {
                personAdditionalIncomeDetailViewModel = viewModel;
                break;
            }
            return View(personAdditionalIncomeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonAdditionalIncomeDetailViewModel _personadditionalIncomeDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personadditionalIncomeDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personadditionalIncomeDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personadditionalIncomeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personadditionalIncomeDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personadditionalIncomeDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personAdditionalIncomeDetailRepository.VerifyRejectDelete(_personadditionalIncomeDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonAdditionalIncomeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personadditionalIncomeDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personAdditionalIncomeDetailRepository.VerifyRejectDelete(_personadditionalIncomeDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonAdditionalIncomeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonAdditionalIncomeDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personadditionalIncomeDetailViewModel.PersonId);
        }
    }
}