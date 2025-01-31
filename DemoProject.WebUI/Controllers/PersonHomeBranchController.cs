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
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonHomeBranch")]
    public class PersonHomeBranchController : Controller
    {
        private readonly IPersonHomeBranchRepository personHomeBranchRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonHomeBranchController(IPersonMasterRepository _personMasterRepository, IPersonHomeBranchRepository _personHomeBranchRepository, IPersonInformationParameterRepository _personInformationParameterRepository)
        {
            personMasterRepository = _personMasterRepository;
            personHomeBranchRepository = _personHomeBranchRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonHomeBranchViewModel PersonHomeBranchViewModel = await personHomeBranchRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);


            if (PersonHomeBranchViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(PersonHomeBranchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonHomeBranchViewModel _personHomeBranchViewModel, string command)
        {
           
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personHomeBranchViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personHomeBranchViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personHomeBranchRepository.Amend(_personHomeBranchViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonHomeBranch");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personHomeBranchRepository.VerifyRejectDelete(_personHomeBranchViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonHomeBranch") }, JsonRequestBehavior.AllowGet);
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

            return View(_personHomeBranchViewModel.PersonId);
        }

        [HttpGet]
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personHomeBranchRepository.GetIndex(StringLiteralValue.Verify);

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
            PersonHomeBranchViewModel PersonHomeBranchViewModel = await personHomeBranchRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (await personHomeBranchRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            if (PersonHomeBranchViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(PersonHomeBranchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonHomeBranchViewModel _personHomeBranchViewModel)
        {
           
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personHomeBranchViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personHomeBranchViewModel.PersonId);

                bool result = await personHomeBranchRepository.Modify(_personHomeBranchViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonHomeBranch");
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

            return View(_personHomeBranchViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personHomeBranchRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }


        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personHomeBranchRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonHomeBranchViewModel PersonHomeBranchViewModel = await personHomeBranchRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            if (PersonHomeBranchViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(PersonHomeBranchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonHomeBranchViewModel _personHomeBranchViewModel, string command)
        {
           
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personHomeBranchViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personHomeBranchViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personHomeBranchViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personHomeBranchRepository.VerifyRejectDelete(_personHomeBranchViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonHomeBranch"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "PersonHomeBranch"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personHomeBranchViewModel.UserAction = StringLiteralValue.Reject;
                    _personHomeBranchViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personHomeBranchViewModel.PersonId);

                    bool result = await personHomeBranchRepository.VerifyRejectDelete(_personHomeBranchViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonHomeBranch"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "PersonHomeBranch"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonHomeBranch");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personHomeBranchViewModel.PersonId);
        }
    }
}