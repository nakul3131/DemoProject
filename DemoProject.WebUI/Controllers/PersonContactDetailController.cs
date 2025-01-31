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
    [RoutePrefix("Employee/PersonInformation/PersonContactDetail")]
    public class PersonContactDetailController : Controller
    {
        private readonly IPersonContactDetailsRepository personContactDetailsRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonContactDetailController(IPersonContactDetailsRepository _personContactDetailsRepository, IPersonMasterRepository _personMasterRepository)
        {
            personContactDetailsRepository = _personContactDetailsRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["ContactDetail"] = await personContactDetailsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonContactDetailViewModel personContactDetailViewModel = new PersonContactDetailViewModel();
            IEnumerable<PersonContactDetailViewModel> personContactDetailViewModels = (IEnumerable<PersonContactDetailViewModel>)HttpContext.Session["ContactDetail"];

            foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModels)
            {
                personContactDetailViewModel = viewModel;
                break;
            }
            return View(personContactDetailViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonContactDetailViewModel _personContactDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personContactDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personContactDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personContactDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personContactDetailViewModel.PersonId);

                    bool result = await personContactDetailsRepository.Amend(_personContactDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonContactDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personContactDetailsRepository.VerifyRejectDelete(_personContactDetailViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonContactDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personContactDetailViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonContactDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonContactDetailViewModel.PersonContactDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personContactDetailsRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["ContactDetail"] = await personContactDetailsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personContactDetailsRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonContactDetailViewModel personContactDetailViewModel = new PersonContactDetailViewModel();
            IEnumerable<PersonContactDetailViewModel> personContactDetailViewModels = (IEnumerable<PersonContactDetailViewModel>)HttpContext.Session["ContactDetail"];

            foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModels)
            {
                personContactDetailViewModel = viewModel;
                break;
            }
            return View(personContactDetailViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonContactDetailViewModel _personContactDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personContactDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personContactDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personContactDetailViewModel.PersonId);

                bool result = await personContactDetailsRepository.Modify(_personContactDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonContactDetail");
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

            return View(_personContactDetailViewModel);
        }


        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {

            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personContactDetailsRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveContactDetailDataTable")]
        public ActionResult SaveContactDetailDataTable(List<PersonContactDetailViewModel> _contactDetail)
        {
            HttpContext.Session.Add("ContactDetail", _contactDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {

            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personContactDetailsRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["ContactDetail"] = await personContactDetailsRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonContactDetailViewModel personContactDetailViewModel = new PersonContactDetailViewModel();
            IEnumerable<PersonContactDetailViewModel> personContactDetailViewModels = (IEnumerable<PersonContactDetailViewModel>)HttpContext.Session["ContactDetail"];

            foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModels)
            {
                personContactDetailViewModel = viewModel;
                break;
            }
            return View(personContactDetailViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonContactDetailViewModel _personContactDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personContactDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personContactDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personContactDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personContactDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personContactDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personContactDetailsRepository.VerifyRejectDelete(_personContactDetailViewModel , StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonContactDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personContactDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personContactDetailsRepository.VerifyRejectDelete(_personContactDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonContactDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonContactDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personContactDetailViewModel.PersonId);
        }

    }
}