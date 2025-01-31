using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonMachineryAsset")]
    public class PersonMachineryAssetController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonMachineryAssetRepository personMachineryAssetRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonMachineryAssetController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository,IPersonMachineryAssetRepository _personMachineryAssetRepository, IPersonMasterRepository _personMasterRepository)
        {
           personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
           personMachineryAssetRepository = _personMachineryAssetRepository;
           personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["MachineryAsset"] = await personMachineryAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMachineryAssetViewModel personMachineryAssetViewModel = new PersonMachineryAssetViewModel();

            IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels = (IEnumerable<PersonMachineryAssetViewModel>)HttpContext.Session["MachineryAsset"];

            foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModels)
            {
                personMachineryAssetViewModel = viewModel;
                break;
            }

            return View(personMachineryAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personMachineryAssetViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personMachineryAssetViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _personMachineryAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMachineryAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personMachineryAssetRepository.Amend(_personMachineryAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonMachineryAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personMachineryAssetRepository.VerifyRejectDelete(_personMachineryAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonMachineryAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personMachineryAssetViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonMachineryAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonMachineryAssetViewModel.PersonMachineryAssetPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMachineryAssetRepository.GetIndex(StringLiteralValue.Verify);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        //[HttpPost]
        //public ActionResult GetUniqueMachineryName(string NameOfMachinery)
        //{
        //    bool data = personMachineryAssetRepository.GetUniqueMachineryName(NameOfMachinery);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid personId)
        {
            HttpContext.Session["MachineryAsset"] = await personMachineryAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Verify);
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personMachineryAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonMachineryAssetViewModel personMachineryAssetViewModel = new PersonMachineryAssetViewModel();

            IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels = (IEnumerable<PersonMachineryAssetViewModel>)HttpContext.Session["MachineryAsset"];

            foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModels)
            {
                personMachineryAssetViewModel = viewModel;
                break;
            }
            return View(personMachineryAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonMachineryAssetViewModel _personMachineryAssetViewModel)
        {
             ClearModelStateOfDataTableFields(_personMachineryAssetViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMachineryAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMachineryAssetViewModel.PersonId);

                bool result = await personMachineryAssetRepository.Modify(_personMachineryAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonMachineryAsset");
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

            return View(_personMachineryAssetViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMachineryAssetRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveMachineryDataTables")]
        public ActionResult SaveMachineryDataTables(List<PersonMachineryAssetViewModel> _machineryAsset)
        {
            HttpContext.Session.Add("MachineryAsset", _machineryAsset);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMachineryAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["MachineryAsset"] = await personMachineryAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMachineryAssetViewModel personMachineryAssetViewModel = new PersonMachineryAssetViewModel();

            IEnumerable<PersonMachineryAssetViewModel> personMachineryAssetViewModels = (IEnumerable<PersonMachineryAssetViewModel>)HttpContext.Session["MachineryAsset"];

            foreach (PersonMachineryAssetViewModel viewModel in personMachineryAssetViewModels)
            {
                personMachineryAssetViewModel = viewModel;
                break;
            }
            return View(personMachineryAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonMachineryAssetViewModel _personMachineryAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personMachineryAssetViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personMachineryAssetViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _personMachineryAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personMachineryAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMachineryAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personMachineryAssetRepository.VerifyRejectDelete(_personMachineryAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMachineryAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personMachineryAssetViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personMachineryAssetRepository.VerifyRejectDelete(_personMachineryAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMachineryAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonMachineryAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personMachineryAssetViewModel.PersonId);
        }
    }
}