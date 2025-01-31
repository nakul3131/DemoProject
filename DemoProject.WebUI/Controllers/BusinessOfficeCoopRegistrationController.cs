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
    [RoutePrefix("Employee/Master/Enterprise/BusinessOfficeCoopRegistration")]
    public class BusinessOfficeCoopRegistrationController : Controller
    {
        private readonly IBusinessOfficeCoopRegistrationRepository businessOfficeCoopRegistrationRepository;

        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public BusinessOfficeCoopRegistrationController(IBusinessOfficeCoopRegistrationRepository _businessOfficeCoopRegistrationRepository, IEnterpriseDetailRepository _enterpriseDetailRepository,  IOfficeDetailRepository _officeDetailRepository)
        {
            businessOfficeCoopRegistrationRepository = _businessOfficeCoopRegistrationRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            officeDetailRepository = _officeDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BusinessOfficeId)
        {
            BusinessOfficeCoopRegistrationViewModel businessOfficeCoopRegistrationViewModel = await officeDetailRepository.GetCoopRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Reject);

            if (businessOfficeCoopRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeCoopRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await businessOfficeCoopRegistrationRepository.Amend(_businessOfficeCoopRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "BusinessOfficeCoopRegistration");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await businessOfficeCoopRegistrationRepository.Delete(_businessOfficeCoopRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BusinessOfficeCoopRegistration") }, JsonRequestBehavior.AllowGet);
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

            return View(_businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationId);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid BusinessOfficeId)
        {

            BusinessOfficeCoopRegistrationViewModel businessOfficeCoopRegistrationViewModel = await officeDetailRepository.GetCoopRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId),StringLiteralValue.Initiated);

            if (businessOfficeCoopRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeCoopRegistrationViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {

                _businessOfficeCoopRegistrationViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeCoopRegistrationViewModel.BusinessOfficeId);

                bool result = await businessOfficeCoopRegistrationRepository.Save(_businessOfficeCoopRegistrationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BusinessOfficeCoopRegistration");
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

            return View(_businessOfficeCoopRegistrationViewModel);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetCooperativeEntriesForOperation(StringLiteralValue.Verify);

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
            BusinessOfficeCoopRegistrationViewModel businessOfficeCoopRegistrationViewModel = await officeDetailRepository.GetCoopRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Verify);

            if (businessOfficeCoopRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeCoopRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await businessOfficeCoopRegistrationRepository.Modify(_businessOfficeCoopRegistrationViewModel);

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

            return View(_businessOfficeCoopRegistrationViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetCooperativeEntriesForOperation(StringLiteralValue.Reject);

            if (businessOfficeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetCooperativeEntriesForOperation(StringLiteralValue.Unverified);

            if (businessOfficeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetCooperativeEntriesForOperation(StringLiteralValue.Verify);

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
            BusinessOfficeCoopRegistrationViewModel businessOfficeCoopRegistrationViewModel = await officeDetailRepository.GetCoopRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId),StringLiteralValue.Unverified);

            if (businessOfficeCoopRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeCoopRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _businessOfficeCoopRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await businessOfficeCoopRegistrationRepository.Verify(_businessOfficeCoopRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeCoopRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeCoopRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _businessOfficeCoopRegistrationViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await businessOfficeCoopRegistrationRepository.Reject(_businessOfficeCoopRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeCoopRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeCoopRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BusinessOfficeCoopRegistration");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_businessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationId);
        }

    }
}