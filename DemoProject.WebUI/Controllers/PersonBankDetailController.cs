using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonBankDetail")]
    public class PersonBankDetailController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonBankDetailRepository personBankDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonBankDetailController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonMasterRepository _personMasterRepository, IPersonBankDetailRepository _personBankDetailRepository )
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personMasterRepository = _personMasterRepository;
            personBankDetailRepository = _personBankDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["BankDetail"] = await personBankDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;
            PersonBankDetailViewModel PersonBankDetailViewModel = new PersonBankDetailViewModel();

            IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels = (IEnumerable<PersonBankDetailViewModel>)HttpContext.Session["BankDetail"];

            foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModels)
            {
                PersonBankDetailViewModel = viewModel;
                break;
            }

            return View(PersonBankDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonBankDetailViewModel _personBankDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personBankDetailViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personBankDetailViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personBankDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBankDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personBankDetailRepository.Amend(_personBankDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonBankDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personBankDetailRepository.VerifyRejectDelete(_personBankDetailViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonBankDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_personBankDetailViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonBankDetailViewModel _personBankDetailViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonBankDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonBankDetailViewModel.PersonBankDetailPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBankDetailRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["BankDetail"] = await personBankDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personBankDetailRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonBankDetailViewModel PersonBankDetailViewModel = new PersonBankDetailViewModel();

            IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels = (IEnumerable<PersonBankDetailViewModel>)HttpContext.Session["BankDetail"];

            foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModels)
            {
                PersonBankDetailViewModel = viewModel;
                break;
            }

            return View(PersonBankDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonBankDetailViewModel _personBankDetailViewModel)
        {
             ClearModelStateOfDataTableFields(_personBankDetailViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personBankDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBankDetailViewModel.PersonId);

                bool result = await personBankDetailRepository.Modify(_personBankDetailViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonBankDetail");
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

            return View(_personBankDetailViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBankDetailRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveBankDataTables")]
        public ActionResult SaveBankDataTables(List<PersonBankDetailViewModel> _bankDetail)
        {
            HttpContext.Session.Add("BankDetail", _bankDetail);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBankDetailRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonBankDetailViewModel personBankDetailViewModel = new PersonBankDetailViewModel();
            HttpContext.Session["BankDetail"] = await personBankDetailRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            IEnumerable<PersonBankDetailViewModel> personBankDetailViewModels = (IEnumerable<PersonBankDetailViewModel>)HttpContext.Session["BankDetail"];

            foreach (PersonBankDetailViewModel viewModel in personBankDetailViewModels)
            {
                personBankDetailViewModel = viewModel;
                break;
            }
            return View(personBankDetailViewModel);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonBankDetailViewModel _personBankDetailViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personBankDetailViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personBankDetailViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personBankDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personBankDetailViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBankDetailViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personBankDetailRepository.VerifyRejectDelete(_personBankDetailViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBankDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personBankDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personBankDetailRepository.VerifyRejectDelete(_personBankDetailViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBankDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonBankDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personBankDetailViewModel.PersonId);
        }
    }
}