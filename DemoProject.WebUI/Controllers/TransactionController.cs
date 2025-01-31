using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.ViewModel.Account.Parameter;
using System.Linq;
using DemoProject.Services.ViewModel.Account.Customer;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Parameter/Account/Transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionParameterRepository transactionParameterRepository;
        private readonly ITransactionRepository transactionRepository;

        public TransactionController( ITransactionParameterRepository _transactionParameterRepository,
                                              ITransactionRepository _transactionRepository )
        {
            transactionParameterRepository = _transactionParameterRepository;
            transactionRepository = _transactionRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            TransactionViewModel transactionViewModel = await transactionRepository.GetRejectedEntry();
            bool data = await transactionRepository.GetSessionValues(transactionViewModel, StringLiteralValue.Reject);

            if (transactionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(transactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(TransactionViewModel _transactionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await transactionRepository.Amend(_transactionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;
                        
                        return RedirectToAction("RejectedIndex", "Transaction");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await transactionRepository.VerifyRejectDelete(_transactionViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Transaction"), }, JsonRequestBehavior.AllowGet);
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

            return View(_transactionViewModel);
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<TransactionViewModel> transactionViewModels = await transactionRepository.GetTransactionParameterIndex();

            return View(transactionViewModels);
        }


        private async Task ClearModelStateOfDataTableFields(TransactionViewModel transactionViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "TransactionCustomerAccountViewModel, SharesCessationTransactionViewModel, TransactionGeneralLedgerViewModel, SharesCapitalTransactionViewModel,TransactionCashDenominationViewModel";

            TransactionParameterViewModel transactionParameterViewModel = await transactionParameterRepository.GetActiveEntry();

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["TransactionViewModel.TransactionPrmKey"]?.Errors?.Clear();
                ModelState["TransactionCustomerAccountViewModel.TransactionCustomerAccountPrmKey"]?.Errors?.Clear();
                ModelState["SharesCessationTransactionViewModel.SharesCessationTransactionPrmKey"]?.Errors?.Clear();
                ModelState["TransactionGeneralLedgerViewModel.TransactionGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["SharesCapitalTransactionViewModel.SharesCapitalTransactionPrmKey"]?.Errors?.Clear();
                ModelState["TransactionCashDenominationViewModel.TransactionCashDenominationPrmKey"]?.Errors?.Clear();
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

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(TransactionViewModel _transactionViewModel)
        {
            await ClearModelStateOfDataTableFields(_transactionViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                bool result = await transactionRepository.Save(_transactionViewModel);

                if (result)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Transaction");
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
            return View(_transactionViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<TransactionCustomerAccountViewModel> _customerAccount,
                                           List<TransactionGeneralLedgerViewModel> _generalLedger,
                                           List<SharesCapitalTransactionViewModel> _sharesCapital,
                                           List<TransactionGSTDetailViewModel> _gstDetail,
                                           List<SharesCessationTransactionViewModel> _sharesCessation,
                                           List<TransactionCashDenominationViewModel> _cashDenomination)
        {
            HttpContext.Session.Add("CustomerAccount", _customerAccount);
            HttpContext.Session.Add("GeneralLedger", _generalLedger);
            HttpContext.Session.Add("SharesCapital", _sharesCapital);
            HttpContext.Session.Add("GSTDetail", _gstDetail);
            HttpContext.Session.Add("SharesCessation", _sharesCessation);
            HttpContext.Session.Add("CashDenomination", _cashDenomination);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<TransactionIndexViewModel> transactionViewModels = await transactionRepository.GetIndexOfUnVerifiedEntries();

            if (transactionViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(transactionViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            TransactionParameterViewModel transactionParameterViewModel = await transactionParameterRepository.GetActiveEntry();

            //bool data = await transactionRepository.GetSessionValues(transactionRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Unverified);
            TransactionViewModel transactionViewModel = await transactionRepository.GetUnVerifiedEntry();

            if (transactionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(transactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(TransactionViewModel _transactionViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_transactionViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_transactionViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                _transactionViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await transactionRepository.VerifyRejectDelete(_transactionViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Transaction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _transactionViewModel.UserAction = StringLiteralValue.CommandReject;

                    bool result = await transactionRepository.VerifyRejectDelete(_transactionViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Transaction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_transactionViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            TransactionViewModel transactionViewModel = await transactionRepository.GetActiveEntry();

            if (transactionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(transactionViewModel);
        }
    }
}