using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Setting/GeneralLedger/SharesCapital/Scheme")]
    public class SharesCapitalSchemeController : Controller
    {
        // GET: SharesCapitalScheme
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISharesCapitalByLawsParameterRepository lawsSharesCapitalParameterRepository;
        private readonly ISharesCapitalSchemeParameterRepository sharesCapitalSchemeParameterRepository;
        private readonly ISharesCapitalSchemeRepository sharesCapitalSchemeRepository;

        public SharesCapitalSchemeController(IAccountDetailRepository _accountDetailRepository, ISharesCapitalByLawsParameterRepository _lawsSharesCapitalParameterRepository,
                                             ISharesCapitalSchemeParameterRepository _sharesCapitalSchemeParameterRepository, ISharesCapitalSchemeRepository _sharesCapitalSchemeRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            lawsSharesCapitalParameterRepository = _lawsSharesCapitalParameterRepository;
            sharesCapitalSchemeParameterRepository = _sharesCapitalSchemeParameterRepository;
            sharesCapitalSchemeRepository = _sharesCapitalSchemeRepository;

        }


        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid SchemeId)
        {

            bool data = await sharesCapitalSchemeRepository.GetSessionValues(accountDetailRepository.GetSchemePrmKeyById(SchemeId), Services.Constants.StringLiteralValue.Reject);

            SharesCapitalSchemeViewModel schemeViewModel = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeEntry(SchemeId, StringLiteralValue.Reject);

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();
            ViewBag.SharesCapitalSchemeParameter = sharesCapitalSchemeParameterViewModel;

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_sharesCapitalSchemeViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_sharesCapitalSchemeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await sharesCapitalSchemeRepository.Amend(_sharesCapitalSchemeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "SharesCapitalScheme");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await sharesCapitalSchemeRepository.VerifyRejectDelete(_sharesCapitalSchemeViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "SharesCapitalScheme"), }, JsonRequestBehavior.AllowGet);
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

            return View(_sharesCapitalSchemeViewModel.SchemeId);
        }

        [NonAction]
        private async Task ClearModelStateOfDataTableFields(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "SchemeChargesDetailViewModel,SchemeNoticeScheduleViewModel,SchemeReportFormatViewModel, SchemeGeneralLedgerViewModel, SchemeBusinessOfficeViewModel,SchemeSharesTransferChargesViewModel, SchemeClosingChargesViewModel";

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

            // SchemeApplicationParameter
            if (sharesCapitalSchemeParameterViewModel.EnableApplicationParameter == false ? true : (_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel.EnableApplicationNumberBranchwise || !_sharesCapitalSchemeViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber))
            {
                errorViewModelName = errorViewModelName + ",SchemeApplicationParameterViewModel";
            }

            // EnableAccountNumberBranchwise
            if (_sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel.EnableAccountNumberBranchwise == true || _sharesCapitalSchemeViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeCustomerAccountNumberViewModel";
            }

            // Member Number 
            if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableMemberNumberBranchwise == true || _sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeSharesCapitalAccountParameterViewModel";
            }

            // SchemeSharesCertificateParameter
            if (_sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel.EnableCertificateNumberBranchwise == true || _sharesCapitalSchemeViewModel.SchemeSharesCertificateParameterViewModel.EnableAutoCertificateNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeSharesCertificateParameterViewModel";
            }

            // SchemeSharesCapitalDividendParameterViewModel
            if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableDividend == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeSharesCapitalDividendParameterViewModel";
            }
            else
            {
                //  If Rounding Method Is None Then Hide Round Nearest (i.e. Require To Clear Error)
                if (_sharesCapitalSchemeViewModel.SchemeSharesCapitalDividendParameterViewModel.RoundMethod == "NOR")
                    ModelState["SchemeSharesCapitalDividendParameterViewModel.RoundNearest"]?.Errors?.Clear();
            }

            // SchemeAccountBankingChannelParameter              
            if (sharesCapitalSchemeParameterViewModel.EnableBankingChannelParameter == false ? true : !_sharesCapitalSchemeViewModel.SchemeAccountBankingChannelParameterViewModel.EnableBankingChannelParameter)
            {
                errorViewModelName = errorViewModelName + ",SchemeAccountBankingChannelParameterViewModel";
            }

            // SchemeEstimateTarget
            if (sharesCapitalSchemeParameterViewModel.EnableTargetEstimationParameter == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeEstimateTargetViewModel";
            }

            // SchemeLimit
            if (sharesCapitalSchemeParameterViewModel.EnableLimitParameter == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLimitViewModel";
            }

            // AccountParameter
            if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfJointAccountHoldingLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultJointAccountHolder"]?.Errors?.Clear();
            }
            if (_sharesCapitalSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfNomineeLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultNominee"]?.Errors?.Clear();
            }

            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {                
                ModelState["SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey"]?.Errors?.Clear();             
                ModelState["SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();             
                ModelState["SchemeSharesCertificateParameterViewModel.SchemeSharesCertificateParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeSharesCapitalDividendParameterViewModel.SchemeSharesCapitalDividendParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeEstimateTargetViewModel.SchemeEstimateTargetPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLimitViewModel.SchemeLimitPrmKey"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.SchemeAccountParameterPrmKey"]?.Errors?.Clear();
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
            }
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();
            ViewBag.SharesCapitalSchemeParameter = sharesCapitalSchemeParameterViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel)
        {
            await ClearModelStateOfDataTableFields(_sharesCapitalSchemeViewModel, StringLiteralValue.Create);

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();
            ViewBag.SharesCapitalSchemeParameter = sharesCapitalSchemeParameterViewModel;

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await sharesCapitalSchemeRepository.Save(_sharesCapitalSchemeViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "SharesCapitalScheme");
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

            return View(_sharesCapitalSchemeViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<SharesCapitalSchemeIndexViewModel> sharesCapitalSchemeIndexViewModels = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeIndex(StringLiteralValue.Reject);

            if (sharesCapitalSchemeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeIndexViewModels);
        }

        [HttpPost]
        [Route("SaveContact")]
        public ActionResult SaveDataTables(List<SchemeBusinessOfficeViewModel> _schemeBusinessOffice, List<SchemeClosingChargesViewModel> _schemeClosingCharges, List<SchemeGeneralLedgerViewModel> _schemeGeneralLedger, List<SchemeNoticeScheduleViewModel> _schemeNoticeSchedule, List<SchemeReportFormatViewModel> _schemeReportFormat,  List<SchemeSharesTransferChargesViewModel> _schemeSharesTransferCharges)
        {
            HttpContext.Session.Add("SchemeBusinessOffice", _schemeBusinessOffice);
            HttpContext.Session.Add("SchemeClosingCharges", _schemeClosingCharges);
            HttpContext.Session.Add("SchemeGeneralLedger", _schemeGeneralLedger);
            HttpContext.Session.Add("SchemeNoticeSchedule", _schemeNoticeSchedule);
            HttpContext.Session.Add("SchemeReportFormat", _schemeReportFormat);
            HttpContext.Session.Add("SchemeSharesTransferCharges", _schemeSharesTransferCharges);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<SharesCapitalSchemeIndexViewModel> sharesCapitalSchemeIndexViewModels = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeIndex(StringLiteralValue.Unverified);

            if (sharesCapitalSchemeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<SharesCapitalSchemeIndexViewModel> sharesCapitalSchemeIndexViewModels = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeIndex(StringLiteralValue.Verify);

            if (sharesCapitalSchemeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid SchemeId)
        {

            bool data = await sharesCapitalSchemeRepository.GetSessionValues(accountDetailRepository.GetSchemePrmKeyById(SchemeId), StringLiteralValue.Unverified);

            SharesCapitalSchemeViewModel schemeViewModel = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeEntry(SchemeId, StringLiteralValue.Unverified);

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();
            ViewBag.SharesCapitalSchemeParameter = sharesCapitalSchemeParameterViewModel;

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(SharesCapitalSchemeViewModel _sharesCapitalSchemeViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_sharesCapitalSchemeViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_sharesCapitalSchemeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _sharesCapitalSchemeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await sharesCapitalSchemeRepository.VerifyRejectDelete(_sharesCapitalSchemeViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "SharesCapitalScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _sharesCapitalSchemeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await sharesCapitalSchemeRepository.VerifyRejectDelete(_sharesCapitalSchemeViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "SharesCapitalScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "SharesCapitalScheme");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_sharesCapitalSchemeViewModel.SchemeId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid SchemeId)
        {

            bool data = await sharesCapitalSchemeRepository.GetSessionValues(accountDetailRepository.GetSchemePrmKeyById(SchemeId), StringLiteralValue.Verify);

            SharesCapitalSchemeViewModel schemeViewModel = await sharesCapitalSchemeRepository.GetSharesCapitalSchemeEntry(SchemeId, StringLiteralValue.Verify);

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();
            ViewBag.SharesCapitalSchemeParameter = sharesCapitalSchemeParameterViewModel;

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

    }
}