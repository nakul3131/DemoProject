using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/HumanResource/ContentItem")]
    public class ContentItemController : Controller
    {
        private readonly IContentItemRepository contentItemRepository;

        public ContentItemController(IContentItemRepository _contentItemRepository)
        {
            contentItemRepository = _contentItemRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid ContentItemId)
        {
            ContentItemViewModel contentItemViewModel = await contentItemRepository.GetRejectedEntry(ContentItemId);

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(ContentItemViewModel _contentItemViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await contentItemRepository.Amend(_contentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "ContentItem");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await contentItemRepository.Delete(_contentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "ContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_contentItemViewModel.ContentItemId);
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
        public async Task<ActionResult> Create(ContentItemViewModel _contentItemViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await contentItemRepository.Save(_contentItemViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "ContentItem");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_contentItemViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueContentItemName(string NameOfContentItem)
        {
            bool data = contentItemRepository.GetUniqueContentItemName(NameOfContentItem);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid ContentItemId)
        {
            ContentItemViewModel contentItemViewModel = await contentItemRepository.GetVerifiedEntry(ContentItemId);

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(ContentItemViewModel _contentItemViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await contentItemRepository.Modify(_contentItemViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_contentItemViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<ContentItemViewModel> contentItemViewModel = await contentItemRepository.GetIndexOfRejectedEntries();

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<ContentItemViewModel> contentItemViewModel = await contentItemRepository.GetIndexOfUnVerifiedEntries();

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<ContentItemViewModel> contentItemViewModel = await contentItemRepository.GetIndexOfVerifiedEntries();

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid ContentItemId)
        {
            ContentItemViewModel contentItemViewModel = await contentItemRepository.GetUnVerifiedEntry(ContentItemId);

            if (contentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(contentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(ContentItemViewModel _contentItemViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _contentItemViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await contentItemRepository.Verify(_contentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await contentItemRepository.Reject(_contentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "ContentItem");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_contentItemViewModel.ContentItemId);
        }

    }
}