using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.WebUI.Reports;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Application/Shares")]
    public class SharesApplicationController : Controller
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonRepository personRepository;
        private readonly ISharesApplicationRepository sharesApplicationRepository;

        public SharesApplicationController(IConfigurationDetailRepository _configurationDetailRepository, IPersonRepository _personRepository, ISharesApplicationRepository _sharesApplicationRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            personRepository = _personRepository;
            sharesApplicationRepository = _sharesApplicationRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(long ApplicationNumber)
        {
            SharesApplicationViewModel sharesApplicationViewModel = await sharesApplicationRepository.GetRejectedEntry(ApplicationNumber);

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(SharesApplicationViewModel _sharesApplicationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await sharesApplicationRepository.Amend(_sharesApplicationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "SharesApplication");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await sharesApplicationRepository.Delete(_sharesApplicationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "SharesApplication"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "SharesApplication");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_sharesApplicationViewModel.ApplicationNumber);
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
        public async Task<ActionResult> Create(SharesApplicationViewModel _sharesApplicationViewModel)
        {

            if (ModelState.IsValid)
            {
                bool result = await sharesApplicationRepository.Save(_sharesApplicationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "SharesApplication");
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
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_sharesApplicationViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(long ApplicationNumber)
        {
            SharesApplicationViewModel sharesApplicationViewModel = await sharesApplicationRepository.GetVerifiedEntry(ApplicationNumber);

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await sharesApplicationRepository.Modify(_sharesApplicationViewModel);

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

            return View(_sharesApplicationViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<SharesApplicationViewModel> sharesApplicationViewModel = await sharesApplicationRepository.GetIndexOfRejectedEntries();

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }
        
        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {

            IEnumerable<SharesApplicationViewModel> sharesApplicationViewModel = await sharesApplicationRepository.GetIndexOfUnVerifiedEntries();

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<SharesApplicationViewModel> sharesApplicationViewModel = await sharesApplicationRepository.GetIndexOfVerifiedEntries();

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(long ApplicationNumber)
        {
            SharesApplicationViewModel sharesApplicationViewModel = await sharesApplicationRepository.GetUnVerifiedEntry(ApplicationNumber);

            if (sharesApplicationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesApplicationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(SharesApplicationViewModel _sharesApplicationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _sharesApplicationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await sharesApplicationRepository.Verify(_sharesApplicationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "SharesApplication"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await sharesApplicationRepository.Reject(_sharesApplicationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "SharesApplication"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "SharesApplication");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_sharesApplicationViewModel.ApplicationNumber);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SharesApplicationReport()
        {
            return View();
        } 

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SharesApplicationReport(SharesApplicationReportViewModel _sharesApplicationReportViewModel)
        {
            SharesApplicationReport rpt = new SharesApplicationReport();
            //rpt.Parameters["SharesApplicationPrmKey"].Value = sharesApplicationRepository.GetPrmKeyByNumber(_sharesApplicationReportViewModel.ApplicationNumber);
            rpt.Parameters["SharesApplicationPrmKey"].Visible = false;
            rpt.Parameters["UserProfilePrmKey"].Value = 4;
            rpt.Parameters["UserProfilePrmKey"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = 2;// configurationDetailRepository.GetLanguagePrmKeyById(_sharesApplicationReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument(); 
            return View();
            
        }
    }
}