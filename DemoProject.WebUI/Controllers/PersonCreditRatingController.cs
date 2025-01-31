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
    [RoutePrefix("Employee/PersonInformation/PersonCreditRating")]
    public class PersonCreditRatingController : Controller
    {
        private readonly IPersonCreditRatingRepository personCreditRatingRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonCreditRatingController(IPersonCreditRatingRepository _personCreditRatingRepository, IPersonMasterRepository _personMasterRepository)
        {
            personCreditRatingRepository = _personCreditRatingRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["CreditRating"] = await personCreditRatingRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);

            PersonCreditRatingViewModel personCreditRatingViewModel = new PersonCreditRatingViewModel();
            IEnumerable<PersonCreditRatingViewModel> personCreditRatingViewModels = (IEnumerable<PersonCreditRatingViewModel>)HttpContext.Session["CreditRating"];

            foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModels)
            {
                personCreditRatingViewModel = viewModel;
                break;
            }
            return View(personCreditRatingViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonCreditRatingViewModel _personCreditRatingViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personCreditRatingViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personCreditRatingViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCreditRatingViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCreditRatingViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personCreditRatingRepository.Amend(_personCreditRatingViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonCreditRating");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personCreditRatingRepository.VerifyRejectDelete(_personCreditRatingViewModel , StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonCreditRating") }, JsonRequestBehavior.AllowGet);
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

            return View(_personCreditRatingViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonCreditRatingViewModel _personCreditRatingViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonCreditRatingViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonCreditRatingViewModel.PersonCreditRatingPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCreditRatingRepository.GetIndex(StringLiteralValue.Verify);

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
            
            HttpContext.Session["CreditRating"] = await personCreditRatingRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personCreditRatingRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonCreditRatingViewModel personCreditRatingViewModel = new PersonCreditRatingViewModel();
            IEnumerable<PersonCreditRatingViewModel> personCreditRatingViewModels = (IEnumerable<PersonCreditRatingViewModel>)HttpContext.Session["CreditRating"];

            foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModels)
            {
                personCreditRatingViewModel = viewModel;
                break;
            }
            return View(personCreditRatingViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonCreditRatingViewModel _personCreditRatingViewModel)
        {
             ClearModelStateOfDataTableFields(_personCreditRatingViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCreditRatingViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCreditRatingViewModel.PersonId);

                bool result = await personCreditRatingRepository.Modify(_personCreditRatingViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonCreditRating");
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

            return View(_personCreditRatingViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCreditRatingRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveCreditRatingDataTable")]
        public ActionResult SaveCreditRatingDataTable(List<PersonCreditRatingViewModel> _creditRating)
        {
            HttpContext.Session.Add("CreditRating", _creditRating);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCreditRatingRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["CreditRating"] = await personCreditRatingRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonCreditRatingViewModel personCreditRatingViewModel = new PersonCreditRatingViewModel();
            IEnumerable<PersonCreditRatingViewModel> personCreditRatingViewModels = (IEnumerable<PersonCreditRatingViewModel>)HttpContext.Session["CreditRating"];

            foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModels)
            {
                personCreditRatingViewModel = viewModel;
                break;
            }
            return View(personCreditRatingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonCreditRatingViewModel _personCreditRatingViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personCreditRatingViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personCreditRatingViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCreditRatingViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personCreditRatingViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCreditRatingViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personCreditRatingRepository.VerifyRejectDelete(_personCreditRatingViewModel , StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCreditRating"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personCreditRatingViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personCreditRatingRepository.VerifyRejectDelete(_personCreditRatingViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCreditRating"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonCreditRating");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personCreditRatingViewModel.PersonId);
        }

    }
}