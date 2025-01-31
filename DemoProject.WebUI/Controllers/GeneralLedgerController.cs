using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Account/GL")]
    public class GeneralLedgerController : Controller
    {
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IGeneralLedgerDetailRepository generalLedgerDetailRepository;

        public GeneralLedgerController(IGeneralLedgerRepository _generalLedgerRepository, IGeneralLedgerDetailRepository _generalLedgerDetailRepository)
        {
            generalLedgerRepository = _generalLedgerRepository;
            generalLedgerDetailRepository = _generalLedgerDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid GeneralLedgerId)
        {

            bool data = await generalLedgerRepository.GetSessionValues(generalLedgerRepository.GetPrmKeyById(GeneralLedgerId), StringLiteralValue.Reject);

            GeneralLedgerViewModel generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerEntry(GeneralLedgerId, StringLiteralValue.Reject);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(GeneralLedgerViewModel _generalLedgerViewModel, string Command)
        {
            //For Clear ModelState Error
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await generalLedgerRepository.Amend(_generalLedgerViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "GeneralLedger");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await generalLedgerRepository.VerifyRejectDelete(_generalLedgerViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "GeneralLedger"), }, JsonRequestBehavior.AllowGet);
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

            return View(_generalLedgerViewModel.GeneralLedgerId);
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
        public async Task<ActionResult> Create(GeneralLedgerViewModel _generalLedgerViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await generalLedgerRepository.Save(_generalLedgerViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "GeneralLedger");
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

            return View(_generalLedgerViewModel);
        }

        private void ClearModelStateOfDataTableFields(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType)
        {
            string errorViewModelName = "GeneralLedgerBusinessOfficeViewModel,GeneralLedgerCurrencyViewModel,GeneralLedgerTransactionTypeViewModel,GeneralLedgerCustomerTypeViewModel";

            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["GeneralLedgerViewModel.GeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["GeneralLedgerBusinessOfficeViewModel.GeneralLedgerBusinessOfficePrmKey"]?.Errors?.Clear();
                ModelState["GeneralLedgerTransactionTypeViewModel.GeneralLedgerTransactionTypePrmKey"]?.Errors?.Clear();
                ModelState["GeneralLedgerCustomerTypeViewModel.GeneralLedgerCustomerTypePrmKey"]?.Errors?.Clear();
            }

            // Clear DataTable Error
            foreach (var key in ModelState.Keys)
            {
                var viewModelPropertyArray = key.Split('.');
                int arrayLength = viewModelPropertyArray.Length;

                if (arrayLength > 1)
                {
                    var viewModel = viewModelPropertyArray[arrayLength - 2];

                    if (errorViewModelName.Contains(viewModel) || key.Contains("Enable"))
                    {
                        ModelState[key]?.Errors?.Clear();
                    }
                }
                else
                    ModelState[key]?.Errors?.Clear();
            }
        }

        [HttpPost]
        public ActionResult GetUniqueGLName(string NameOfGL)
        {
            bool data = generalLedgerDetailRepository.GetUniqueGLName(NameOfGL);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetNumberOfGeneralLegerLimit(Guid _accountClassId)
        {
            int[] data;
            var numberOfGeneralLegerLimit = generalLedgerDetailRepository.GetNumberOfGeneralLegerLimit(_accountClassId);
            var countOfGeneralLedger = generalLedgerDetailRepository.GetCountOfGeneralLedger();

            data = new int[] { numberOfGeneralLegerLimit, countOfGeneralLedger };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid GeneralLedgerId)
        {
            bool data = await generalLedgerRepository.GetSessionValues(generalLedgerRepository.GetPrmKeyById(GeneralLedgerId), StringLiteralValue.Verify);

            GeneralLedgerViewModel generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerEntry(GeneralLedgerId, StringLiteralValue.Verify);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(GeneralLedgerViewModel _generalLedgerViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Modify);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await generalLedgerRepository.Modify(_generalLedgerViewModel);

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

            return View(_generalLedgerViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerIndex(StringLiteralValue.Reject);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<GeneralLedgerBusinessOfficeViewModel> _businessOffice, List<GeneralLedgerCurrencyViewModel> _currency,
                                           List<GeneralLedgerTransactionTypeViewModel> _transactionType, List<GeneralLedgerCustomerTypeViewModel> _customerType)
        {
            HttpContext.Session.Add("GeneralLedgerBusinessOffice", _businessOffice);

            HttpContext.Session.Add("GeneralLedgerCurrency", _currency);

            HttpContext.Session.Add("GeneralLedgerTransactionType", _transactionType);

            HttpContext.Session.Add("GeneralLedgerCustomerType", _customerType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerIndex(StringLiteralValue.Unverified);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerIndex(StringLiteralValue.Verify);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid GeneralLedgerId)
        {
            bool data = await generalLedgerRepository.GetSessionValues(generalLedgerRepository.GetPrmKeyById(GeneralLedgerId), StringLiteralValue.Unverified);

            GeneralLedgerViewModel generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerEntry(GeneralLedgerId, StringLiteralValue.Unverified);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(GeneralLedgerViewModel _generalLedgerViewModel, string Command)
        {
            //For Clear ModelState Error
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_generalLedgerViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await generalLedgerRepository.VerifyRejectDelete(_generalLedgerViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "GeneralLedger"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await generalLedgerRepository.VerifyRejectDelete(_generalLedgerViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "GeneralLedger"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "GeneralLedger");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_generalLedgerViewModel.GeneralLedgerId);
        }

    }
}