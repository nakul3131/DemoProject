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
    [RoutePrefix("Employee/Master/Enterprise/BusinessOfficeRBIRegistration")]
    public class BusinessOfficeRBIRegistrationController : Controller
    {
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IBusinessOfficeRBIRegistrationRepository businessOfficeRBIRegistrationRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public BusinessOfficeRBIRegistrationController(IEnterpriseDetailRepository _enterpriseDetailRepository, IBusinessOfficeRBIRegistrationRepository _businessOfficeRBIRegistrationRepository, IOfficeDetailRepository _officeDetailRepository)
        {
            enterpriseDetailRepository = _enterpriseDetailRepository;
            businessOfficeRBIRegistrationRepository = _businessOfficeRBIRegistrationRepository;
            officeDetailRepository = _officeDetailRepository;

        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BusinessOfficeId)
        {
            BusinessOfficeRBIRegistrationViewModel businessOfficeRBIRegistrationViewModel = await officeDetailRepository.GetRBIRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId),StringLiteralValue.Reject);

            if (businessOfficeRBIRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeRBIRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await businessOfficeRBIRegistrationRepository.Amend(_businessOfficeRBIRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "BusinessOfficeRBIRegistration");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await businessOfficeRBIRegistrationRepository.Delete(_businessOfficeRBIRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BusinessOfficeRBIRegistration") }, JsonRequestBehavior.AllowGet);
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

            return View(_businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationId);
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
        public async Task<ActionResult> Create(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {

                _businessOfficeRBIRegistrationViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeRBIRegistrationViewModel.BusinessOfficeId);

                bool result = await businessOfficeRBIRegistrationRepository.Save(_businessOfficeRBIRegistrationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BusinessOfficeRBIRegistration");
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

            return View(_businessOfficeRBIRegistrationViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetRBIRegistrationEntriesForOperation(StringLiteralValue.Verify);

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
            BusinessOfficeRBIRegistrationViewModel businessOfficeRBIRegistrationViewModel = await officeDetailRepository.GetRBIRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Verify);

            if (businessOfficeRBIRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeRBIRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await businessOfficeRBIRegistrationRepository.Modify(_businessOfficeRBIRegistrationViewModel);

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

            return View(_businessOfficeRBIRegistrationViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedISOCodeRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetRBIRegistrationEntriesForOperation(StringLiteralValue.Reject);

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
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetRBIRegistrationEntriesForOperation(StringLiteralValue.Unverified);

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
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetRBIRegistrationEntriesForOperation(StringLiteralValue.Verify);

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
            BusinessOfficeRBIRegistrationViewModel businessOfficeRBIRegistrationViewModel = await officeDetailRepository.GetRBIRegistrationEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Unverified);

            if (businessOfficeRBIRegistrationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeRBIRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _businessOfficeRBIRegistrationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await businessOfficeRBIRegistrationRepository.Verify(_businessOfficeRBIRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeRBIRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeRBIRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _businessOfficeRBIRegistrationViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await businessOfficeRBIRegistrationRepository.Reject(_businessOfficeRBIRegistrationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficeRBIRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BusinessOfficeRBIRegistration"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BusinessOfficeRBIRegistration");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_businessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationId);
        }

    }
}