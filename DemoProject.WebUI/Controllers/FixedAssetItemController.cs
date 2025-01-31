using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/FixedAssetItem")]
    public class FixedAssetItemController : Controller
    {
        private readonly IFixedAssetItemRepository fixedAssetItemRepository;

        public FixedAssetItemController(IFixedAssetItemRepository _fixedAssetItemRepository)
        {
            fixedAssetItemRepository = _fixedAssetItemRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid FixedAssetItemId)
        {
            FixedAssetItemViewModel fixedAssetItemViewModel = await fixedAssetItemRepository.GetRejectedEntry(FixedAssetItemId);

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(FixedAssetItemViewModel _fixedAssetItemViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await fixedAssetItemRepository.Amend(_fixedAssetItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "FixedAssetItem");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await fixedAssetItemRepository.Delete(_fixedAssetItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "FixedAssetItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "FixedAssetItem");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_fixedAssetItemViewModel.FixedAssetItemId);
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
        public async Task<ActionResult> Create(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await fixedAssetItemRepository.Save(_fixedAssetItemViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "FixedAssetItem");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_fixedAssetItemViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid FixedAssetItemId)
        {
            FixedAssetItemViewModel fixedAssetItemViewModel = await fixedAssetItemRepository.GetVerifiedEntry(FixedAssetItemId);

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(FixedAssetItemViewModel _fixedAssetItemViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await fixedAssetItemRepository.Modify(_fixedAssetItemViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_fixedAssetItemViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<FixedAssetItemViewModel> fixedAssetItemViewModel = await fixedAssetItemRepository.GetIndexOfRejectedEntries();

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<FixedAssetItemViewModel> fixedAssetItemViewModel = await fixedAssetItemRepository.GetIndexOfUnVerifiedEntries();

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<FixedAssetItemViewModel> fixedAssetItemViewModel = await fixedAssetItemRepository.GetIndexOfVerifiedEntries();

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid FixedAssetItemId)
        {
            FixedAssetItemViewModel fixedAssetItemViewModel = await fixedAssetItemRepository.GetUnVerifiedEntry(FixedAssetItemId);

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(FixedAssetItemViewModel _fixedAssetItemViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _fixedAssetItemViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await fixedAssetItemRepository.Verify(_fixedAssetItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "FixedAssetItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await fixedAssetItemRepository.Reject(_fixedAssetItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "FixedAssetItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "FixedAssetItem");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_fixedAssetItemViewModel.FixedAssetItemId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid FixedAssetItemId)
        {
            FixedAssetItemViewModel fixedAssetItemViewModel = await fixedAssetItemRepository.GetVerifiedEntry(FixedAssetItemId);

            if (fixedAssetItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(fixedAssetItemViewModel);
        }
    }
}