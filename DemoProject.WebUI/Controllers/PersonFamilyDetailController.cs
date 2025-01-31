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
    [RoutePrefix("Employee/PersonInformation/PersonFamilyDetail")]
    public class PersonFamilyDetailController : Controller
    {
        private readonly IPersonFamilyDetailRepository personFamilyDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonFamilyDetailController(IPersonFamilyDetailRepository _personFamilyDetailRepository, IPersonMasterRepository _personMasterRepository)
        {
            personFamilyDetailRepository = _personFamilyDetailRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["FamilyDetail"] = await personFamilyDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);
            PersonFamilyDetailViewModel personFamilyDetailViewModel = new PersonFamilyDetailViewModel();

            IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailViewModels = (IEnumerable<PersonFamilyDetailViewModel>)HttpContext.Session["FamilyDetail"];

            foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModels)
            {
                personFamilyDetailViewModel = viewModel;
                break;
            }
            return View(personFamilyDetailViewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonFamilyDetailViewModel _personFamilyDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personFamilyDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personFamilyDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personFamilyDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFamilyDetailViewModel.PersonId);

                    bool result = await personFamilyDetailRepository.Amend(_personFamilyDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonFamilyDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    _personFamilyDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFamilyDetailViewModel.PersonId);

                    bool result = await personFamilyDetailRepository.VerifyRejectDelete(_personFamilyDetailViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonFamilyDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personFamilyDetailViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonFamilyDetailViewModel _personFamilyDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonFamilyDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonFamilyDetailViewModel.PersonFamilyDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFamilyDetailRepository.GetIndex(StringLiteralValue.Verify);

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
            
            HttpContext.Session["FamilyDetail"] = await personFamilyDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personFamilyDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonFamilyDetailViewModel personFamilyDetailViewModel = new PersonFamilyDetailViewModel();

            IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailViewModels = (IEnumerable<PersonFamilyDetailViewModel>)HttpContext.Session["FamilyDetail"];

            foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModels)
            {
                personFamilyDetailViewModel = viewModel;
                break;
            }
            return View(personFamilyDetailViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonFamilyDetailViewModel _personFamilyDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personFamilyDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personFamilyDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFamilyDetailViewModel.PersonId);

                bool result = await personFamilyDetailRepository.Modify(_personFamilyDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonFamilyDetail");
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

            return View(_personFamilyDetailViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFamilyDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveFamilyDetailDataTable")]
        public ActionResult SaveFamilyDetailDataTable(List<PersonFamilyDetailViewModel> _familyDetail)
        {
            HttpContext.Session.Add("FamilyDetail", _familyDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFamilyDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["FamilyDetail"] = await personFamilyDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonFamilyDetailViewModel personFamilyDetailViewModel = new PersonFamilyDetailViewModel();

            IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailViewModels = (IEnumerable<PersonFamilyDetailViewModel>)HttpContext.Session["FamilyDetail"];

            foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModels)
            {
                personFamilyDetailViewModel = viewModel;
                break;
            }
            return View(personFamilyDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonFamilyDetailViewModel _personFamilyDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personFamilyDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personFamilyDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personFamilyDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {

                    bool result = await personFamilyDetailRepository.VerifyRejectDelete(_personFamilyDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonFamilyDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personFamilyDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personFamilyDetailRepository.VerifyRejectDelete(_personFamilyDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonFamilyDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonFamilyDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personFamilyDetailViewModel.PersonId);
        }

    }
}