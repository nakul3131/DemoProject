using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/Continent")]
    public class ContinentController : Controller
    {
        private readonly IContinentRepository continentRepository;

        public ContinentController(IContinentRepository _continentRepository)
        {
            continentRepository = _continentRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            ContinentViewModel continentViewModel = await continentRepository.GetRejectedEntry(CenterId);

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(ContinentViewModel _continentViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_continentViewModel);
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await continentRepository.Amend(_continentViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;
                        return RedirectToAction("RejectedIndex", "Continent");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await continentRepository.Delete(_continentViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Continent"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_continentViewModel.CenterId);
        }

        private void ClearModelStateOfDataTableFields(ContinentViewModel _continentViewModel)
        {
            if (_continentViewModel.CenterCategoryPrmKey == 12)
            {
                ModelState.Remove("ParentCenterId");
            }
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(ContinentViewModel _continentViewModel)
        {
            ClearModelStateOfDataTableFields(_continentViewModel);

            if (ModelState.IsValid)
            {
                bool result = await continentRepository.Save(_continentViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Continent");
                    }

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_continentViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = continentRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            ContinentViewModel continentViewModel = await continentRepository.GetVerifiedEntry(CenterId);

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(ContinentViewModel _continentViewModel)
        {
            ClearModelStateOfDataTableFields(_continentViewModel);
            if (ModelState.IsValid)
            {
                bool result = await continentRepository.Modify(_continentViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_continentViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<ContinentViewModel> continentViewModel = await continentRepository.GetIndexOfRejectedEntries();

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<ContinentViewModel> continentViewModel = await continentRepository.GetIndexOfUnVerifiedEntries();

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<ContinentViewModel> continentViewModel = await continentRepository.GetIndexOfVerifiedEntries();

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            ContinentViewModel continentViewModel = await continentRepository.GetUnVerifiedEntry(CenterId);

            return continentViewModel is null ? throw new DatabaseException() : (ActionResult)View(continentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(ContinentViewModel _continentViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_continentViewModel);
            if (ModelState.IsValid)
            {
                _continentViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await continentRepository.Verify(_continentViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Continent"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _continentViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await continentRepository.Reject(_continentViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Continent"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Continent");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_continentViewModel.CenterId);
        }
    }
}