using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
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
    [RoutePrefix("Employee/PersonInformation/PersonGroupMaster")]
    public class PersonGroupAuthorizedSignatoryController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonGroupMasterRepository personGroupMasterRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonGroupAuthorizedSignatoryController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonMasterRepository _personMasterRepository, IPersonGroupMasterRepository _personGroupMasterRepository)
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personMasterRepository = _personMasterRepository;
            personGroupMasterRepository = _personGroupMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            long personPrmKey = personMasterRepository.GetPersonPrmKeyById(personId);
            long personGroupPrmKey = personMasterRepository.GetPersonGroupPrmKeyByPersonPrmKey(personPrmKey);

            PersonGroupMasterViewModel personGroupMasterViewModel = await personGroupMasterRepository.GetPersonGroupMasterEntry(personPrmKey, StringLiteralValue.Reject);

            bool data = await personGroupMasterRepository.GetPersonGroupMasterSessionValues(personGroupMasterViewModel, StringLiteralValue.Reject);

            if (personGroupMasterViewModel is null)
            {
                throw new DatabaseException();
            }
            return View(personGroupMasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonGroupMasterViewModel _personGroupMasterViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personGroupMasterViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personGroupMasterViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personGroupMasterRepository.Amend(_personGroupMasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonGroupAuthorizedSignatory");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personGroupMasterRepository.VerifyRejectDelete(_personGroupMasterViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonGroupAuthorizedSignatory") }, JsonRequestBehavior.AllowGet);
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

            return View(_personGroupMasterViewModel.PersonGroupId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonGroupMasterViewModel _personGroupMasterViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonGroupAuthorizedSignatoryViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonGroupAuthorizedSignatoryViewModel.PersonGroupAuthorizedSignatoryPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personGroupMasterRepository.GetIndex(StringLiteralValue.Verify);

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
            
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            long personPrmKey = personMasterRepository.GetPersonPrmKeyById(personId);
            long personGroupPrmKey = personMasterRepository.GetPersonGroupPrmKeyByPersonPrmKey(personPrmKey);

            //Check Any Authorization Pending
            if (await personGroupMasterRepository.IsAnyAuthorizationPending(personGroupPrmKey))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonGroupMasterViewModel personGroupMasterViewModel = await personGroupMasterRepository.GetPersonGroupMasterEntry(personPrmKey, StringLiteralValue.Verify);

            bool data = await personGroupMasterRepository.GetPersonGroupMasterSessionValues(personGroupMasterViewModel, StringLiteralValue.Verify);

            if (personGroupMasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personGroupMasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonGroupMasterViewModel _personGroupMasterViewModel)
        {
             ClearModelStateOfDataTableFields(_personGroupMasterViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await personGroupMasterRepository.Modify(_personGroupMasterViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonGroupAuthorizedSignatory");
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

            return View(_personGroupMasterViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personGroupMasterRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveAuthorizedSignatoryDataTables")]
        public ActionResult SaveAuthorizedSignatoryDataTables(List<PersonGroupAuthorizedSignatoryViewModel> _groupAuthorizedSignatory)
        {
            HttpContext.Session.Add("GroupAuthorizedSignatory", _groupAuthorizedSignatory);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personGroupMasterRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            long personPrmKey = personMasterRepository.GetPersonPrmKeyById(personId);

            PersonGroupMasterViewModel personGroupMasterViewModel = await personGroupMasterRepository.GetPersonGroupMasterEntry(personPrmKey, StringLiteralValue.Unverified);
            //personGroupMasterViewModel.PersonGroupPrmKey = personMasterRepository.GetPersonGroupPrmKeyByPersonPrmKey(personPrmKey);

            bool data = await personGroupMasterRepository.GetPersonGroupMasterSessionValues(personGroupMasterViewModel, StringLiteralValue.Unverified);

            if (personGroupMasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personGroupMasterViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonGroupMasterViewModel _personGroupMasterViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personGroupMasterViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personGroupMasterViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personGroupMasterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personGroupMasterRepository.VerifyRejectDelete(_personGroupMasterViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonGroupAuthorizedSignatory"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personGroupMasterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personGroupMasterRepository.VerifyRejectDelete(_personGroupMasterViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonGroupAuthorizedSignatory"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonGroupAuthorizedSignatory");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personGroupMasterViewModel.PersonGroupId);
        }
    }
}