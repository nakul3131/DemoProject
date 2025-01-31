using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonIncomeTaxDetail")]
    public class PersonIncomeTaxDetailController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonIncomeTaxDetailRepository personIncomeTaxDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonIncomeTaxDetailController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonMasterRepository _personMasterRepository, IPersonIncomeTaxDetailRepository _personIncomeTaxDetailRepository)
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personMasterRepository = _personMasterRepository;
            personIncomeTaxDetailRepository = _personIncomeTaxDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["IncomeTaxDetail"] = await personIncomeTaxDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonIncomeTaxDetailViewModel personIncomeTaxDetailViewModel = new PersonIncomeTaxDetailViewModel();

            IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels = (IEnumerable<PersonIncomeTaxDetailViewModel>)HttpContext.Session["IncomeTaxDetail"];

            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
            {
                personIncomeTaxDetailViewModel = viewModel;
                break;
            }
            return View(personIncomeTaxDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string command)
        {

            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personIncomeTaxDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personIncomeTaxDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personIncomeTaxDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personIncomeTaxDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personIncomeTaxDetailRepository.Amend(_personIncomeTaxDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonIncomeTaxDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personIncomeTaxDetailRepository.VerifyRejectDelete(_personIncomeTaxDetailViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonIncomeTaxDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personIncomeTaxDetailViewModel.PersonId);
        }


        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonIncomeTaxDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personIncomeTaxDetailRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["IncomeTaxDetail"] = await personIncomeTaxDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personIncomeTaxDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonIncomeTaxDetailViewModel personIncomeTaxDetailViewModel = new PersonIncomeTaxDetailViewModel();

            IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels = (IEnumerable<PersonIncomeTaxDetailViewModel>)HttpContext.Session["IncomeTaxDetail"];

            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
            {
                personIncomeTaxDetailViewModel = viewModel;
                break;
            }
            return View(personIncomeTaxDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personIncomeTaxDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _personIncomeTaxDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personIncomeTaxDetailViewModel.PersonId);

                bool result = await personIncomeTaxDetailRepository.Modify(_personIncomeTaxDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonIncomeTaxDetail");
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

            return View(_personIncomeTaxDetailViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personIncomeTaxDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveIncomeTaxDataTables")]
        public ActionResult SaveIncomeTaxDataTables(List<PersonIncomeTaxDetailViewModel> _incomeTaxDetail)
        {
            HttpContext.Session.Add("IncomeTaxDetail", _incomeTaxDetail);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personIncomeTaxDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["IncomeTaxDetail"] = await personIncomeTaxDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonIncomeTaxDetailViewModel personIncomeTaxDetailViewModel = new PersonIncomeTaxDetailViewModel();

            IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModels = (IEnumerable<PersonIncomeTaxDetailViewModel>)HttpContext.Session["IncomeTaxDetail"];

            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModels)
            {
                personIncomeTaxDetailViewModel = viewModel;
                break;
            }
            return View(personIncomeTaxDetailViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personIncomeTaxDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personIncomeTaxDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personIncomeTaxDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personIncomeTaxDetailViewModel.PersonId);

                _personIncomeTaxDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personIncomeTaxDetailRepository.VerifyRejectDelete(_personIncomeTaxDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonIncomeTaxDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personIncomeTaxDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personIncomeTaxDetailRepository.VerifyRejectDelete(_personIncomeTaxDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonIncomeTaxDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonIncomeTaxDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personIncomeTaxDetailViewModel.PersonId);
        }
    }
}