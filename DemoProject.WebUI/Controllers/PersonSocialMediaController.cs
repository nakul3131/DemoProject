using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonSocialMedia")]
    public class PersonSocialMediaController : Controller
    {
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonSocialMediaRepository personSocialMediaRepository;

        public PersonSocialMediaController(IPersonMasterRepository _personMasterRepository, IPersonSocialMediaRepository _personSocialMediaRepository)
        {
            personMasterRepository = _personMasterRepository;
            personSocialMediaRepository = _personSocialMediaRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["SocialMedia"] = await personSocialMediaRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonSocialMediaViewModel personSocialMediaViewModel = new PersonSocialMediaViewModel();

            IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModels = (IEnumerable<PersonSocialMediaViewModel>)HttpContext.Session["SocialMedia"];

            foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModels)
            {
                personSocialMediaViewModel = viewModel;
                break;
            }
            return View(personSocialMediaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonSocialMediaViewModel _personSocialMediaViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personSocialMediaViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personSocialMediaViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSocialMediaViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSocialMediaViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personSocialMediaRepository.Amend(_personSocialMediaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonSocialMedia");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personSocialMediaRepository.VerifyRejectDelete(_personSocialMediaViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonSocialMedia") }, JsonRequestBehavior.AllowGet);
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

            return View(_personSocialMediaViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonSocialMediaViewModel _personSocialMediaViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonSocialMediaViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonSocialMediaViewModel.PersonSocialMediaPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSocialMediaRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["SocialMedia"] = await personSocialMediaRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personSocialMediaRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonSocialMediaViewModel personSocialMediaViewModel = new PersonSocialMediaViewModel();

            IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModels = (IEnumerable<PersonSocialMediaViewModel>)HttpContext.Session["SocialMedia"];

            foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModels)
            {
                personSocialMediaViewModel = viewModel;
                break;
            }

            return View(personSocialMediaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonSocialMediaViewModel _personSocialMediaViewModel)
        {
             ClearModelStateOfDataTableFields(_personSocialMediaViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSocialMediaViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSocialMediaViewModel.PersonId);

                bool result = await personSocialMediaRepository.Modify(_personSocialMediaViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonSocialMedia");
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

            return View(_personSocialMediaViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSocialMediaRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveSocialMediaDataTable")]
        public ActionResult SaveSocialMediaDataTable(List<PersonSocialMediaViewModel> _socialMedia)
        {
            HttpContext.Session.Add("SocialMedia", _socialMedia);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personSocialMediaRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["SocialMedia"] = await personSocialMediaRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonSocialMediaViewModel personSocialMediaViewModel = new PersonSocialMediaViewModel();

            IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModels = (IEnumerable<PersonSocialMediaViewModel>)HttpContext.Session["SocialMedia"];

            foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModels)
            {
                personSocialMediaViewModel = viewModel;
                break;
            }
            return View(personSocialMediaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonSocialMediaViewModel _personSocialMediaViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personSocialMediaViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personSocialMediaViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personSocialMediaViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personSocialMediaViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personSocialMediaViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personSocialMediaRepository.VerifyRejectDelete(_personSocialMediaViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonSocialMedia"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personSocialMediaViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personSocialMediaRepository.VerifyRejectDelete(_personSocialMediaViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonSocialMedia"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonSocialMedia");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personSocialMediaViewModel.PersonId);
        }
    }
}