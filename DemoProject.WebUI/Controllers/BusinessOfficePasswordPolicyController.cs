using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/BusinessOfficePasswordPolicy")]
    public class BusinessOfficePasswordPolicyController : Controller
    {
        private readonly IBusinessOfficePasswordPolicyRepository businessOfficePasswordPolicyRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;

        public BusinessOfficePasswordPolicyController(IEnterpriseDetailRepository _enterpriseDetailRepository, IBusinessOfficePasswordPolicyRepository _businessOfficePasswordPolicyRepository, IOfficeDetailRepository _officeDetailRepository)
        {
            enterpriseDetailRepository = _enterpriseDetailRepository;
            businessOfficePasswordPolicyRepository = _businessOfficePasswordPolicyRepository;
            officeDetailRepository = _officeDetailRepository;

        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BusinessOfficeId)
        {
            HttpContext.Session["BusinessOfficePasswordPolicy"] = await officeDetailRepository.GetPasswordPolicyEntries(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId),StringLiteralValue.Reject);

            BusinessOfficePasswordPolicyViewModel businessOfficePasswordPolicyViewModel = await officeDetailRepository.GetPasswordPolicyEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Reject);

            if (businessOfficePasswordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficePasswordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_businessOfficePasswordPolicyViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await businessOfficePasswordPolicyRepository.Amend(_businessOfficePasswordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "BusinessOfficePasswordPolicy");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await businessOfficePasswordPolicyRepository.Delete(_businessOfficePasswordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BusinessOfficePasswordPolicy") }, JsonRequestBehavior.AllowGet);
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

            return View(_businessOfficePasswordPolicyViewModel.BusinessOfficePasswordPolicyId);
        }

        private void ClearModelStateOfDataTableFields(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            ModelState[nameof(_businessOfficePasswordPolicyViewModel.PasswordPolicyId)]?.Errors?.Clear();
            ModelState[nameof(_businessOfficePasswordPolicyViewModel.NameOfPasswordPolicy)]?.Errors?.Clear();
            ModelState[nameof(_businessOfficePasswordPolicyViewModel.ActivationDate)]?.Errors?.Clear();
            ModelState[nameof(_businessOfficePasswordPolicyViewModel.CloseDate)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid BusinessOfficeId)
        {
            BusinessOfficePasswordPolicyViewModel businessOfficePasswordPolicyViewModel = await officeDetailRepository.GetPasswordPolicyEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Initiated);

            if (businessOfficePasswordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficePasswordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            ClearModelStateOfDataTableFields(_businessOfficePasswordPolicyViewModel);

            if (ModelState.IsValid)
            {
                bool result = await businessOfficePasswordPolicyRepository.Save(_businessOfficePasswordPolicyViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Index", "BusinessOfficePasswordPolicy");
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

            return View(_businessOfficePasswordPolicyViewModel);
        }

        [HttpGet]
        [Route("Closed")]
        public async Task<ActionResult> Closed(Guid BusinessOfficeId)
        {
            HttpContext.Session["BusinessOfficePasswordPolicy"] = await officeDetailRepository.GetPasswordPolicyEntries(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Closed);

            BusinessOfficePasswordPolicyViewModel businessOfficePasswordPolicyViewModel = await officeDetailRepository.GetPasswordPolicyEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Closed);

            if (businessOfficePasswordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficePasswordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Closed")]
        public async Task<ActionResult> Closed(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel)
        {
            ClearModelStateOfDataTableFields(_businessOfficePasswordPolicyViewModel);

            if (ModelState.IsValid)
            {
                bool result = await businessOfficePasswordPolicyRepository.Closed(_businessOfficePasswordPolicyViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

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

            return View(_businessOfficePasswordPolicyViewModel);
        }

        [HttpGet]
        [Route("ClosedIndex")]
        public async Task<ActionResult> ClosedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetPasswordPolicyEntriesForOperation(StringLiteralValue.Closed);

            if (businessOfficeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("BusinessOfficeList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetPasswordPolicyEntriesForOperation(StringLiteralValue.Verify);

            if (businessOfficeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetPasswordPolicyEntriesForOperation(StringLiteralValue.Reject);

            if (businessOfficeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModels);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<BusinessOfficePasswordPolicyViewModel> _businessOfficePasswordPolicy)
        {
            HttpContext.Session.Add("BusinessOfficePasswordPolicy", _businessOfficePasswordPolicy);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BusinessOfficeViewModel> businessOfficeViewModels = await officeDetailRepository.GetPasswordPolicyEntriesForOperation(StringLiteralValue.Unverified);

            if (businessOfficeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(businessOfficeViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid BusinessOfficeId)
        {
            HttpContext.Session["BusinessOfficePasswordPolicy"] = await officeDetailRepository.GetPasswordPolicyEntries(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Reject);

            BusinessOfficePasswordPolicyViewModel businessOfficePasswordPolicyViewModel = await officeDetailRepository.GetPasswordPolicyEntry(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Reject);

            if (businessOfficePasswordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficePasswordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _businessOfficePasswordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await businessOfficePasswordPolicyRepository.Verify(_businessOfficePasswordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficePasswordPolicy"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _businessOfficePasswordPolicyViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await businessOfficePasswordPolicyRepository.Reject(_businessOfficePasswordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOfficePasswordPolicy"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BusinessOfficePasswordPolicy");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_businessOfficePasswordPolicyViewModel.BusinessOfficePasswordPolicyId);
        }
    }
}