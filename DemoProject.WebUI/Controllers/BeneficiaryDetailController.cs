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
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/BeneficiaryDetail")]
    public class BeneficiaryDetailController : Controller
    {
        private readonly IBeneficiaryRepository beneficiaryRepository;

        public BeneficiaryDetailController(IBeneficiaryRepository _beneficiaryRepository)
        {
            beneficiaryRepository = _beneficiaryRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BeneficiaryDetailId)
        {
            BeneficiaryDetailViewModel beneficiaryDetailViewModel = await beneficiaryRepository.GetRejectedEntry(BeneficiaryDetailId);

            if (beneficiaryDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(beneficiaryDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BeneficiaryDetailViewModel _beneficiaryDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await beneficiaryRepository.Amend(_beneficiaryDetailViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "BeneficiaryDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await beneficiaryRepository.Delete(_beneficiaryDetailViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BeneficiaryDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "BeneficiaryDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_beneficiaryDetailViewModel.BeneficiaryDetailId);
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
        public async Task<ActionResult> Create(BeneficiaryDetailViewModel _beneficiaryDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await beneficiaryRepository.Save(_beneficiaryDetailViewModel);

                if (result)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BeneficiaryDetail");
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

            return View(_beneficiaryDetailViewModel);
        }
        
        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BeneficiaryDetailViewModel> beneficiaryDetailViewModel = await beneficiaryRepository.GetIndexOfRejectedEntries();

            if (beneficiaryDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(beneficiaryDetailViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BeneficiaryDetailViewModel> beneficiaryDetailViewModels = await beneficiaryRepository.GetIndexOfUnVerifiedEntries();

            if (beneficiaryDetailViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(beneficiaryDetailViewModels);
        }
        
        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid BeneficiaryDetailId)
        {
            BeneficiaryDetailViewModel beneficiaryDetailViewModel = await beneficiaryRepository.GetUnVerifiedEntry(BeneficiaryDetailId);

            if (beneficiaryDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(beneficiaryDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BeneficiaryDetailViewModel _beneficiaryDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _beneficiaryDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await beneficiaryRepository.Verify(_beneficiaryDetailViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BeneficiaryDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await beneficiaryRepository.Reject(_beneficiaryDetailViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BeneficiaryDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BeneficiaryDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_beneficiaryDetailViewModel.BeneficiaryDetailId);
        }
    }
}