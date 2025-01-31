using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/ChequeBookMaster")]
    public class ChequeBookMasterController : Controller
    {
        private readonly IChequeBookMasterRepository chequebookmasterRepository;

        public ChequeBookMasterController(IChequeBookMasterRepository _chequebookmasterRepository)
        {
            chequebookmasterRepository = _chequebookmasterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid ChequeBookMasterId)
        {
            ChequeBookMasterViewModel chequebookmasterViewModel = await chequebookmasterRepository.GetRejectedEntry(ChequeBookMasterId);

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(ChequeBookMasterViewModel _chequebookmasterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await chequebookmasterRepository.Amend(_chequebookmasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "ChequeBookMaster");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await chequebookmasterRepository.Delete(_chequebookmasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "ChequeBookMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "ChequeBookMaster");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_chequebookmasterViewModel.ChequeBookMasterId);
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
        public async Task<ActionResult> Create(ChequeBookMasterViewModel _chequebookmasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await chequebookmasterRepository.Save(_chequebookmasterViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "ChequeBookMaster");
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

            return View(_chequebookmasterViewModel);
        }

        //[HttpPost]
        //public ActionResult GetUniqueDesignationName(string NameOfDesignation)
        //{
        //    bool data = designationRepository.GetUniqueDesignationName(NameOfDesignation);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid ChequeBookMasterId)
        {
            ChequeBookMasterViewModel chequebookmasterViewModel = await chequebookmasterRepository.GetVerifiedEntry(ChequeBookMasterId);

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<ChequeBookMasterViewModel> chequebookmasterViewModel = await chequebookmasterRepository.GetIndexOfRejectedEntries();

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<ChequeBookMasterViewModel> chequebookmasterViewModel = await chequebookmasterRepository.GetIndexOfUnVerifiedEntries();

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<ChequeBookMasterViewModel> chequebookmasterViewModel = await chequebookmasterRepository.GetIndexOfVerifiedEntries();

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid ChequeBookMasterId)
        {
            ChequeBookMasterViewModel chequebookmasterViewModel = await chequebookmasterRepository.GetUnVerifiedEntry(ChequeBookMasterId);

            if (chequebookmasterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(chequebookmasterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(ChequeBookMasterViewModel _chequebookmasterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _chequebookmasterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await chequebookmasterRepository.Verify(_chequebookmasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ChequeBookMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await chequebookmasterRepository.Reject(_chequebookmasterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ChequeBookMaster"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "ChequeBookMaster");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_chequebookmasterViewModel.ChequeBookMasterId);
        }
    }
}





