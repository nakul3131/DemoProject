using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonCommoditiesAsset")]
    public class PersonCommoditiesAssetController : Controller
    {

        private readonly IPersonCommoditiesAssetRepository personCommoditiesAssetRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonCommoditiesAssetController(IPersonMasterRepository _personMasterRepository, IPersonCommoditiesAssetRepository _personCommoditiesAssetRepository, IPersonInformationParameterRepository _personInformationParameterRepository)
        {
            personMasterRepository = _personMasterRepository;
            personCommoditiesAssetRepository = _personCommoditiesAssetRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonCommoditiesAssetViewModel personCommoditiesAssetViewModel = await personCommoditiesAssetRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            if (personCommoditiesAssetViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personCommoditiesAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_personCommoditiesAssetViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCommoditiesAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCommoditiesAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personCommoditiesAssetRepository.Amend(_personCommoditiesAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonCommoditiesAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personCommoditiesAssetRepository.VerifyRejectDelete(_personCommoditiesAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonCommoditiesAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personCommoditiesAssetViewModel.PersonId);
        }

        private void ClearModelStateOfDataTableFields(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel)
        {
            ModelState[nameof(_personCommoditiesAssetViewModel.GoldOrnaments)]?.Errors?.Clear();
            ModelState[nameof(_personCommoditiesAssetViewModel.SilverOrnaments)]?.Errors?.Clear();
            ModelState[nameof(_personCommoditiesAssetViewModel.PlatinumOrnaments)]?.Errors?.Clear();
            ModelState[nameof(_personCommoditiesAssetViewModel.NumberOfDiamondsInGoldOrnaments)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCommoditiesAssetRepository.GetIndex(StringLiteralValue.Verify);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult GetDocumentValidationFields(bool _enableCommoditiesAssetDocumentForLocal)
        //{
        //    //PersonInformationParameterViewModel document = new PersonInformationParameterViewModel();
        //    var data = personInformationParameterRepository.DocumentValidationForPersonCommoditiesAsset(_enableCommoditiesAssetDocumentForLocal);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid personId)
        {
            PersonCommoditiesAssetViewModel personCommoditiesAssetViewModel = await personCommoditiesAssetRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);
            if (await personCommoditiesAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            if (personCommoditiesAssetViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personCommoditiesAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel)
        {
            ClearModelStateOfDataTableFields(_personCommoditiesAssetViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCommoditiesAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCommoditiesAssetViewModel.PersonId);

                bool result = await personCommoditiesAssetRepository.Modify(_personCommoditiesAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonCommoditiesAsset");
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

            return View(_personCommoditiesAssetViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCommoditiesAssetRepository.GetIndex(StringLiteralValue.Reject);

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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personCommoditiesAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonCommoditiesAssetViewModel personCommoditiesAssetViewModel = await personCommoditiesAssetRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            if (personCommoditiesAssetViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personCommoditiesAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_personCommoditiesAssetViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personCommoditiesAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personCommoditiesAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCommoditiesAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personCommoditiesAssetRepository.VerifyRejectDelete(_personCommoditiesAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCommoditiesAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personCommoditiesAssetViewModel.UserAction = StringLiteralValue.Reject;
                    _personCommoditiesAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personCommoditiesAssetViewModel.PersonId);

                    bool result = await personCommoditiesAssetRepository.VerifyRejectDelete(_personCommoditiesAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonCommoditiesAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonCommoditiesAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personCommoditiesAssetViewModel.PersonId);
        }
    }
}