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
    [RoutePrefix("Employee/PersonInformation/PersonBorrowingDetail")]
    public class PersonBorrowingDetailController : Controller
    {
        private readonly IPersonBorrowingDetailRepository personBorrowingDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonBorrowingDetailController(IPersonBorrowingDetailRepository _personBorrowingDetailRepository, IPersonMasterRepository _personMasterRepository)
        {
            personBorrowingDetailRepository = _personBorrowingDetailRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["BorrowingDetail"] = await personBorrowingDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonBorrowingDetailViewModel personBorrowingDetailViewModel = new PersonBorrowingDetailViewModel();

            IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModels = (IEnumerable<PersonBorrowingDetailViewModel>)HttpContext.Session["BorrowingDetail"];

            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModels)
            {
                personBorrowingDetailViewModel = viewModel;
                break;
            }

            return View(personBorrowingDetailViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personBorrowingDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personBorrowingDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personBorrowingDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBorrowingDetailViewModel.PersonId);

                    bool result = await personBorrowingDetailRepository.Amend(_personBorrowingDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonBorrowingDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personBorrowingDetailRepository.VerifyRejectDelete(_personBorrowingDetailViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonBorrowingDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personBorrowingDetailViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonBorrowingDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonBorrowingDetailViewModel.PersonBorrowingDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBorrowingDetailRepository.GetIndex(StringLiteralValue.Verify);

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

            HttpContext.Session["BorrowingDetail"] = await personBorrowingDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);
            if (await personBorrowingDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonBorrowingDetailViewModel personBorrowingDetailViewModel = new PersonBorrowingDetailViewModel();

            IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModels = (IEnumerable<PersonBorrowingDetailViewModel>)HttpContext.Session["BorrowingDetail"];

            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModels)
            {
                personBorrowingDetailViewModel = viewModel;
                break;
            }

            return View(personBorrowingDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personBorrowingDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personBorrowingDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBorrowingDetailViewModel.PersonId);

                bool result = await personBorrowingDetailRepository.Modify(_personBorrowingDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonBorrowingDetail");
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

            return View(_personBorrowingDetailViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBorrowingDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveBorrowingDetailDataTable")]
        public ActionResult SaveBorrowingDetailDataTable(List<PersonBorrowingDetailViewModel> _borrowingDetail)
        {
            HttpContext.Session.Add("BorrowingDetail", _borrowingDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBorrowingDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["BorrowingDetail"] = await personBorrowingDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonBorrowingDetailViewModel personBorrowingDetailViewModel = new PersonBorrowingDetailViewModel();

            IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModels = (IEnumerable<PersonBorrowingDetailViewModel>)HttpContext.Session["BorrowingDetail"];

            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModels)
            {
                personBorrowingDetailViewModel = viewModel;
                break;
            }

            return View(personBorrowingDetailViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personBorrowingDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personBorrowingDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _personBorrowingDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personBorrowingDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBorrowingDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personBorrowingDetailRepository.VerifyRejectDelete(_personBorrowingDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBorrowingDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personBorrowingDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personBorrowingDetailRepository.VerifyRejectDelete(_personBorrowingDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBorrowingDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonBorrowingDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personBorrowingDetailViewModel.PersonId);
        }

    }
}