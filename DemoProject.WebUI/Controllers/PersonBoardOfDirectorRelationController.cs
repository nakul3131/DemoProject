using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using System.Linq;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.WebUI.Infrastructure.CustomException;

//Modified By Dhanashri Wagh 20/09/20224
namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonBoardOfDirectorRelation")]
    public class PersonBoardOfDirectorRelationController : Controller
    {
        private readonly IPersonBoardOfDirectorRelationRepository personBoardOfDirectorRelationRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonBoardOfDirectorRelationController(IPersonBoardOfDirectorRelationRepository _personBoardOfDirectorRelationRepository, IPersonMasterRepository _personMasterRepository)
        {
            personBoardOfDirectorRelationRepository = _personBoardOfDirectorRelationRepository;
            personMasterRepository = _personMasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["BoardOfDirectorRelation"] = await personBoardOfDirectorRelationRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId) ,StringLiteralValue.Reject);
            IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModels = (IEnumerable<PersonBoardOfDirectorRelationViewModel>)HttpContext.Session["BoardOfDirectorRelation"];

            PersonBoardOfDirectorRelationViewModel personBoardOfDirectorRelationViewModel = new PersonBoardOfDirectorRelationViewModel();

            foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModels)
            {
                personBoardOfDirectorRelationViewModel = viewModel;
                break;
            }
            return View(personBoardOfDirectorRelationViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    _personBoardOfDirectorRelationViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBoardOfDirectorRelationViewModel.PersonId);

                    bool result = await personBoardOfDirectorRelationRepository.Amend(_personBoardOfDirectorRelationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonBoardOfDirectorRelation");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personBoardOfDirectorRelationRepository.VerifyRejectDelete(_personBoardOfDirectorRelationViewModel ,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonBoardOfDirectorRelation") }, JsonRequestBehavior.AllowGet);
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

            return View(_personBoardOfDirectorRelationViewModel.PersonId);
        }

        private void  ClearModelStateOfDataTableFields(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonAddressViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonBoardOfDirectorRelationViewModel.PersonBoardOfDirectorRelationPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBoardOfDirectorRelationRepository.GetIndex(StringLiteralValue.Verify);

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
            
            HttpContext.Session["BoardOfDirectorRelation"] = await personBoardOfDirectorRelationRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId) , StringLiteralValue.Verify);

            if (await personBoardOfDirectorRelationRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModels = (IEnumerable<PersonBoardOfDirectorRelationViewModel>)HttpContext.Session["BoardOfDirectorRelation"];

            PersonBoardOfDirectorRelationViewModel personBoardOfDirectorRelationViewModel = new PersonBoardOfDirectorRelationViewModel();

            foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModels)
            {
                personBoardOfDirectorRelationViewModel = viewModel;
                break;
            }
            return View(personBoardOfDirectorRelationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel)
        {
             ClearModelStateOfDataTableFields(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personBoardOfDirectorRelationViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBoardOfDirectorRelationViewModel.PersonId);

                bool result = await personBoardOfDirectorRelationRepository.Modify(_personBoardOfDirectorRelationViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonBoardOfDirectorRelation");
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

            return View(_personBoardOfDirectorRelationViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBoardOfDirectorRelationRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveBoardOfDirectorRelationDataTable")]
        public ActionResult SaveBoardOfDirectorRelationDataTable(List<PersonBoardOfDirectorRelationViewModel> _boardOfDirectorRelation)
        {
            HttpContext.Session.Add("BoardOfDirectorRelation", _boardOfDirectorRelation);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personBoardOfDirectorRelationRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["BoardOfDirectorRelation"] = await personBoardOfDirectorRelationRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Unverified);

            IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModels = (IEnumerable<PersonBoardOfDirectorRelationViewModel>)HttpContext.Session["BoardOfDirectorRelation"];

            PersonBoardOfDirectorRelationViewModel personBoardOfDirectorRelationViewModel = new PersonBoardOfDirectorRelationViewModel();

            foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModels)
            {
                personBoardOfDirectorRelationViewModel = viewModel;
                break;
            }
            return View(personBoardOfDirectorRelationViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            _personBoardOfDirectorRelationViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personBoardOfDirectorRelationViewModel.PersonId);

            if (ModelState.IsValid)
            {
                _personBoardOfDirectorRelationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personBoardOfDirectorRelationRepository.VerifyRejectDelete(_personBoardOfDirectorRelationViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBoardOfDirectorRelation"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personBoardOfDirectorRelationViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personBoardOfDirectorRelationRepository.VerifyRejectDelete(_personBoardOfDirectorRelationViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonBoardOfDirectorRelation"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonBoardOfDirectorRelation");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personBoardOfDirectorRelationViewModel.PersonId);
        }
    }
}