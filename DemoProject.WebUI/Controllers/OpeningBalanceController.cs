using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Security/OpeningBalance")]
    public class OpeningBalanceController : Controller
    {
        private readonly IOpeningBalanceRepository OpeningBalanceRepository;

        public OpeningBalanceController(IOpeningBalanceRepository _OpeningBalanceRepository, IPersonMasterRepository _personMasterRepository /*IGeneralLedgerRepository _generalLedgerRepository*/)
        {
            OpeningBalanceRepository = _OpeningBalanceRepository;
        }

        [HttpGet]
        [Route("RejectedIndex")]
        public ActionResult RejectedIndex()
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            openingBalanceViewModel.EntryStatus = StringLiteralValue.Reject;
            return View("Index", openingBalanceViewModel);
        }

        [HttpGet]
        [Route("Amend")]
        public ActionResult Amend()
        {
            return View();
        }

        [HttpPost]
        [Route("Amend")]
        public async Task<ActionResult> Amend(OpeningBalanceViewModel _OpeningBalanceViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _OpeningBalanceViewModel.SchemeTypePrmKey = (byte)TempData["SchemeType"];
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await OpeningBalanceRepository.Save(_OpeningBalanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("Default", "Home");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await OpeningBalanceRepository.Delete(_OpeningBalanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
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

            return View("RejectedIndex");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveDataTables(List<OpeningBalanceViewModel> _data)
        {
            HttpContext.Session.Add("OpeningBalance", _data);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCustomerAccount(Guid _generalLedgerId)
        {
            var customerAccounts = OpeningBalanceRepository.CustomerAccountDropdownList(_generalLedgerId);
            return Json(customerAccounts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            openingBalanceViewModel.EntryStatus = "NON";
            return View(openingBalanceViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GetOpeningBalance(Guid _generalLedgerId, string _entryStatus)
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            var result = await OpeningBalanceRepository.GetOpeningBalanceValues(OpeningBalanceRepository.GetGeneralLedgerPrmKeyById(_generalLedgerId), _entryStatus);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetSchemeType(Guid _generalLedgerId)
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            var schemeType = await OpeningBalanceRepository.GetSchemeType(OpeningBalanceRepository.GetGeneralLedgerPrmKeyById(_generalLedgerId));
            TempData["SchemeType"] = schemeType;
            var depositType = await OpeningBalanceRepository.GetDepositType(OpeningBalanceRepository.GetGeneralLedgerPrmKeyById(_generalLedgerId));
            TempData["DepositType"] = depositType;
            return Json(schemeType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(OpeningBalanceViewModel openingBalanceViewModel)
        {
            if (ModelState.IsValid)
            {
                openingBalanceViewModel.SchemeTypePrmKey = (byte)TempData["SchemeType"];
                bool result = await OpeningBalanceRepository.Save(openingBalanceViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Index", "OpeningBalance");
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

            return View(openingBalanceViewModel);
        }

        [HttpGet]
        [Route("ModifiedIndex")]
        public ActionResult ModifiedIndex()
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            openingBalanceViewModel.EntryStatus = StringLiteralValue.Verify;
            return View(openingBalanceViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ModifyDataTables(List<OpeningBalanceViewModel> _modifyOpeningBalance)
        {
            HttpContext.Session.Add("ModifyOpeningBalance", _modifyOpeningBalance);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid _generalLedgerId, Guid _personId)
        {
            OpeningBalanceViewModel openingBalanceViewModel = await OpeningBalanceRepository.GetOpeningBalanceValue(OpeningBalanceRepository.GetGeneralLedgerPrmKeyById(_generalLedgerId), OpeningBalanceRepository.GetPersonPrmKeyById(_personId));

            return Json(openingBalanceViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Modify")]
        public async Task<ActionResult> Modify(OpeningBalanceViewModel openingBalanceViewModel)
        {
            if (ModelState.IsValid)
            {
                openingBalanceViewModel.SchemeTypePrmKey = (byte)TempData["SchemeType"];
                bool result = await OpeningBalanceRepository.Modify(openingBalanceViewModel);

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

            return View(openingBalanceViewModel);
        }

        [HttpGet]
        [Route("VerifyIndex")]
        public ActionResult VerifyIndex()
        {
            OpeningBalanceViewModel openingBalanceViewModel = new OpeningBalanceViewModel();
            openingBalanceViewModel.EntryStatus = StringLiteralValue.Amend;
            return View("Index", openingBalanceViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public ActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(OpeningBalanceViewModel _OpeningBalanceViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _OpeningBalanceViewModel.SchemeTypePrmKey = (byte)TempData["SchemeType"];
                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await OpeningBalanceRepository.Verify(_OpeningBalanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await OpeningBalanceRepository.Reject(_OpeningBalanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("VerifyIndex", "OpeningBalance");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View("VerifyIndex");
        }
    }
}