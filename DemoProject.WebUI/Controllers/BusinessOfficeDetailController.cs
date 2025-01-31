using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/BusinessOfficeDetail")]
    public class BusinessOfficeDetailController : Controller
    {
        private readonly IBusinessOfficeDetailRepository businessOfficeDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public BusinessOfficeDetailController(IBusinessOfficeDetailRepository _businessOfficeDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IOfficeDetailRepository _officeDetailRepository)
        {
            businessOfficeDetailRepository = _businessOfficeDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            officeDetailRepository = _officeDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BusinessOfficeId)
        {
            BusinessOfficeDetailViewModel businessOfficeDetailViewModel = await officeDetailRepository.GetBusinessOfficeDetailEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId),StringLiteralValue.Reject);

            if (businessOfficeDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await businessOfficeDetailRepository.Amend(_businessOfficeDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "BusinessOfficeDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await businessOfficeDetailRepository.Delete(_businessOfficeDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BusinessOfficeDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_businessOfficeDetailViewModel.BusinessOfficeDetailId);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid BusinessOfficeId)
        {

            BusinessOfficeDetailViewModel businessOfficeDetailViewModel = await officeDetailRepository.GetBusinessOfficeDetailEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Initiated);

            if (businessOfficeDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeDetailViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            if (ModelState.IsValid)
            {

                _businessOfficeDetailViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeDetailViewModel.BusinessOfficeId);

                bool result = await businessOfficeDetailRepository.Save(_businessOfficeDetailViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BusinessOfficeDetail");
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

            return View(_businessOfficeDetailViewModel);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetBusinessOfficeDetailEntriesForOperation(StringLiteralValue.Verify);

            if (businessOfficeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid BusinessOfficeId)
        {
            BusinessOfficeDetailViewModel businessOfficeDetailViewModel = await officeDetailRepository.GetBusinessOfficeDetailEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Verify);

            if (businessOfficeDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await businessOfficeDetailRepository.Modify(_businessOfficeDetailViewModel);

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

            return View(_businessOfficeDetailViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedISOCodeRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetBusinessOfficeDetailEntriesForOperation(StringLiteralValue.Reject);

            if (businessOfficeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedISORecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetBusinessOfficeDetailEntriesForOperation(StringLiteralValue.Unverified);

            if (businessOfficeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetBusinessOfficeDetailEntriesForOperation(StringLiteralValue.Verify);

            if (businessOfficeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(Guid BusinessOfficeId)
        {
            BusinessOfficeDetailViewModel businessOfficeDetailViewModel = await officeDetailRepository.GetBusinessOfficeDetailEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Unverified);

            if (businessOfficeDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(BusinessOfficeDetailViewModel _businessOfficeDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _businessOfficeDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await businessOfficeDetailRepository.Verify(_businessOfficeDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _businessOfficeDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await businessOfficeDetailRepository.Reject(_businessOfficeDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BusinessOfficeDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_businessOfficeDetailViewModel.BusinessOfficeDetailId);
        }
    }
}