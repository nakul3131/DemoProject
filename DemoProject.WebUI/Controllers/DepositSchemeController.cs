using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Setting/GeneralLedger/Scheme/Deposit")]
    public class DepositSchemeController : Controller
    {
        // GET: DepositScheme
        private readonly IDepositSchemeParameterRepository depositSchemeParameterRepository;
        private readonly IDepositSchemeRepository depositSchemeRepository;

        public DepositSchemeController(IDepositSchemeParameterRepository _depositSchemeParameterRepository,
                                       IDepositSchemeRepository _depositSchemeRepository)
        {
            depositSchemeParameterRepository = _depositSchemeParameterRepository;
            depositSchemeRepository = _depositSchemeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid SchemeId)
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            bool data = await depositSchemeRepository.GetSessionValues(depositSchemeRepository.GetPrmKeyById(SchemeId), Services.Constants.StringLiteralValue.Reject);

            DepositSchemeViewModel schemeViewModel = await depositSchemeRepository.GetDepositSchemeEntry(SchemeId, StringLiteralValue.Reject);

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(DepositSchemeViewModel _depositSchemeViewModel, string Command)
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_depositSchemeViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_depositSchemeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await depositSchemeRepository.Amend(_depositSchemeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "DepositScheme");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await depositSchemeRepository.VerifyRejectDelete(_depositSchemeViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "DepositScheme"), }, JsonRequestBehavior.AllowGet);
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

            return View(_depositSchemeViewModel.SchemeId);
        }

        private async Task ClearModelStateOfDataTableFields(DepositSchemeViewModel _depositSchemeViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "SchemeTenureListViewModel, SchemeGeneralLedgerViewModel, SchemeBusinessOfficeViewModel, SchemeDepositAgentIncentiveViewModel, SchemeOtherFundSubscriptionViewModel, SchemeClosingChargesViewModel, SchemeTransactionAmountLimitViewModel, SchemeNumberOfTransactionLimitViewModel, SchemeReportFormatViewModel, SchemeNoticeScheduleViewModel, SchemeDepositInterestPayoutFrequencyViewModel,SchemeTargetGroupViewModel";

            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

            // if EnableTenure is False 
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == false)
                errorViewModelName = errorViewModelName + ",SchemeTenureViewModel";

            // if EnableApplication is False
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == false || _depositSchemeViewModel.SchemeApplicationParameterViewModel.EnableApplicationNumberBranchwise == true || _depositSchemeViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber == false)
                errorViewModelName = errorViewModelName + ",SchemeApplicationParameterViewModel";

            // If EnableAccountNumberBranchwise is true
            if (_depositSchemeViewModel.SchemeCustomerAccountNumberViewModel.EnableAccountNumberBranchwise == true || _depositSchemeViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == false)
                errorViewModelName = errorViewModelName + ",SchemeCustomerAccountNumberViewModel";

            // MaturityDate is false and Enable Maturity Date Override false and Enable Number Of Joint Account Holding Limit is false and EnableNumberOfNomineeLimit is false
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableMaturityDate == false || _depositSchemeViewModel.SchemeAccountParameterViewModel.EnableMaturityDateOverride == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumOverridePeriod"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumOverridePeriod"]?.Errors?.Clear();

                ModelState["SchemeAccountParameterViewModel.TimePeriodForNewCustomerFlag"]?.Errors?.Clear();
            }

            // EnableNumberOfJointAccountHoldingLimit
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfJointAccountHoldingLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultJointAccountHolder"]?.Errors?.Clear();
            }

            // EnableNumberOfNomineeLimit 
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfNomineeLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultNominee"]?.Errors?.Clear();
            }

            // MaximumTaxExemptAmount
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.IsApplicableForTaxExempt == false)
                ModelState["SchemeDepositAccountParameterViewModel.MaximumTaxExemptAmount"]?.Errors?.Clear();

            // If EnableInterestRate is false - Clear Both Interest And Provision Otherwise Check For Interest Payout
            if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == false)
                errorViewModelName = errorViewModelName + ",SchemeDepositInterestProvisionParameterViewModel";

            if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.InterestPayoutDay != "CST")
                ModelState["SchemeDepositInterestParameterViewModel.InterestPayoutDayOther"]?.Errors?.Clear();

            // If EnableInterestProvision is false
            if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnableInterestProvision == false)
                errorViewModelName = errorViewModelName + ",SchemeDepositInterestProvisionParameterViewModel";

            // If EnableBankingChannel is false
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableBankingChannel == false)
                errorViewModelName = errorViewModelName + ",SchemeAccountBankingChannelParameterViewModel";

            // If EnablePassbookDetail is false
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == false || _depositSchemeViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber == false)
                errorViewModelName = errorViewModelName + ",SchemePassbookViewModel";
          
            // If EnablePassbookDetail is true then clear toggleswitch
            if (_depositSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true)
            {
                ModelState["SchemePassbookViewModel.IsVisiblePassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.DuplicatePassbookCharges"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.IsRandomlyGeneratedPassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.ReGenerateUnusedPassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.EnablePassbookVerification"]?.Errors?.Clear();

            }
            // If EnableAgent is false
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == false)
            {
                errorViewModelName = errorViewModelName + ", SchemeDepositAgentParameterViewModel";
            }
            else
            {
                if (_depositSchemeViewModel.SchemeDepositAgentParameterViewModel.EnableCommissionOnOverDuesInstallment == false)
                {
                    ModelState["SchemeDepositAgentParameterViewModel.MinimumOverDuesInstallment"]?.Errors?.Clear();
                    ModelState["SchemeDepositAgentParameterViewModel.MaximumOverDuesInstallment"]?.Errors?.Clear();
                    ModelState["SchemeDepositAgentParameterViewModel.DefaultOverDuesInstallment"]?.Errors?.Clear();
                }

                if (_depositSchemeViewModel.SchemeDepositAgentParameterViewModel.EnableCommissionOnAdditionalInvestment == false)
                {
                    ModelState["SchemeDepositAgentParameterViewModel.MinimumAdditionalInstallment"]?.Errors?.Clear();
                    ModelState["SchemeDepositAgentParameterViewModel.MaximumAdditionalInstallment"]?.Errors?.Clear();
                }

                if (_depositSchemeViewModel.SchemeDepositAgentParameterViewModel.IsRequiredSecurity == false)
                {
                    ModelState["SchemeDepositAgentParameterViewModel.MinimumSecurityAmount"]?.Errors?.Clear();
                    ModelState["SchemeDepositAgentParameterViewModel.MaximumSecurityAmount"]?.Errors?.Clear();
                    ModelState["SchemeDepositAgentParameterViewModel.DefaultSecurityAmount"]?.Errors?.Clear();
                }

                ModelState["SchemeDepositAgentParameterViewModel.IsRequiredSecurity"]?.Errors?.Clear();
            }


            // Account Closure
            if (_depositSchemeViewModel.SchemeDepositAccountClosureParameterViewModel.EnableExtendMaturity == false)
            {
                ModelState["SchemeDepositAccountClosureParameterViewModel.MinimumExtendPeriod"]?.Errors?.Clear();
                ModelState["SchemeDepositAccountClosureParameterViewModel.MaximumExtendPeriod"]?.Errors?.Clear();
            }

            // ############# Based On Deposit Scheme Parameter    #############
            // EnableLimitParameter is False
            if (depositSchemeParameterViewModel.EnableLimitParameter == false)
                errorViewModelName = errorViewModelName + ",SchemeLimitViewModel";

            // EnableTargetEstimationParameter is False
            if (depositSchemeParameterViewModel.EnableTargetEstimationParameter == false)
                errorViewModelName = errorViewModelName + ",SchemeEstimateTargetViewModel";

            if(_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType != "FDP" || _depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePayoutInterestAmountOverride == false)
            {
                // Clear Hidden Input Of Interest Parameter Accordion
                ModelState["SchemeDepositInterestParameterViewModel.MinimumOverrideInterestAmount"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.MaximumOverrideInterestAmount"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.InterestPayoutDay"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.InterestPayoutDayOther"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.MinimumMonthForPeriodicInterestPayout"]?.Errors?.Clear();

            }

            // @@@@@@@@@@@  Deposit Type Is Demand Deposit / PPF  @@@@@@@@@@@
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "DMN" || _depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "PPF")
            {
                // Clear All View Models Whose Not Visible On Demand Deposit Type ViewModel
                errorViewModelName = errorViewModelName + ",SchemeTenureViewModel,SchemeFixedDepositParameterViewModel,SchemeDepositCertificateParameterViewModel,,SchemeDepositAccountRenewalParameterViewModel,SchemeDepositPledgeLoanParameterViewModel,SchemeDepositAccountClosureParameterViewModel,SchemeDepositInstallmentParameterViewModel,SchemeDepositPledgeLoanParameterViewModel";

                // Reference Person
                if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableReferencePersonDetail == false)
                {
                    ModelState["SchemeDemandDepositDetailViewModel.MinimumNumberOfReferencePerson"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumNumberOfReferencePerson"]?.Errors?.Clear();
                }

                // EnableSweepOut is false
                if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableSweepOut == false)
                {
                    ModelState["SchemeDemandDepositDetailViewModel.MinimumBalanceToTriggerSweepIn"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumAmountToTriggerSweep"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MinimumTermDepositAmount"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumTermDepositAmount"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MinimumTermDepositTenure"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumTermDepositTenure"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.DefaultTermDepositTenure"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumNumberOfSweepOut"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.SweepOutFrequencyId"]?.Errors?.Clear();
                }

                // PhotoSign
                if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoSign == false)
                {
                    ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentUpload"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForDb"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInDb"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentLocalStoragePath"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInLocalStorage"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.SignDocumentUpload"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForDb"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInDb"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.SignDocumentLocalStoragePath"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForLocalStorage"]?.Errors?.Clear();
                    ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
                else
                { 
                    // Photo Doucment Upload, If Mandatory Or Optional (i.e. Not Disable)
                    if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentUpload != StringLiteralValue.Disable)
                    {
                        // If Database Storage Is False Then 
                        if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInDb == false)
                        {
                            ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForDatabase"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInDb"]?.Errors?.Clear();
                        }

                        // If Local Storage Is False Then 
                        if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage == false)
                        {
                            ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentLocalStoragePath"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInLocalStorage"]?.Errors?.Clear();
                        }
                    }
                    else // If Disable Then Clear Both Db And Local Storage Inputs
                    {
                        ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForDatabase"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInDb"]?.Errors?.Clear();

                        ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentLocalStoragePath"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.PhotoDocumentFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInLocalStorage"]?.Errors?.Clear();
                    }

                    // Sign Doucment Upload, If Mandatory Or Optional (i.e. Not Disable)
                    if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.SignDocumentUpload != StringLiteralValue.Disable)
                    {
                        // If Database Storage Is False Then 
                        if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInDb == false)
                        {
                            ModelState["SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForDatabase"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInDb"]?.Errors?.Clear();
                        }

                        // If Local Storage Is False Then 
                        if (_depositSchemeViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage == false)
                        {
                            ModelState["SchemeDemandDepositDetailViewModel.SignDocumentLocalStoragePath"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                            ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInLocalStorage"]?.Errors?.Clear();
                        }
                    }
                    else // If Disable Then Clear Both Db And Local Storage Inputs
                    {
                        ModelState["SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForDatabase"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInDb"]?.Errors?.Clear();

                        ModelState["SchemeDemandDepositDetailViewModel.SignDocumentLocalStoragePath"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.SignDocumentFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                        ModelState["SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInLocalStorage"]?.Errors?.Clear();
                    }
                }
            }

            // @@@@@@@@@@@  Deposit Type Is Fixed Deposit  @@@@@@@@@@@
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "FDP")
            {
                // Clear All View Models Whose Not Visible On Fixed Deposit Type ViewModel
                errorViewModelName = errorViewModelName + ",SchemeDepositBalanceParameterViewModel,SchemeDemandDepositDetailViewModel,SchemePassbookViewModel,SchemeDepositInstallmentParameterViewModel";

                // EnableCertificateNumberBranchwise is false
                if (_depositSchemeViewModel.SchemeDepositCertificateParameterViewModel.EnableCertificateNumberBranchwise == false || _depositSchemeViewModel.SchemeDepositCertificateParameterViewModel.EnableAutoCertificateNumber == false)
                    errorViewModelName = errorViewModelName + ",SchemeDepositCertificateParameterViewModel";
            }

            // Recurring Deposit
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "REC")
            {
                //BalanceParameter
                errorViewModelName = errorViewModelName + ",SchemeDepositBalanceParameterViewModel,SchemeDemandDepositDetailViewModel,SchemeFixedDepositParameterViewModel,SchemeDepositCertificateParameterViewModel";

                     errorViewModelName = errorViewModelName + ",SchemeDepositInstallmentParameterViewModel";
                     ModelState["SchemeDepositInterestParameterViewModel.PostMatureVoidInterestPeriod"]?.Errors?.Clear();

            }

            // EnableRenewal
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.EnableRenewal == false)
                errorViewModelName = errorViewModelName + ",SchemeDepositAccountRenewalParameterViewModel";
            else
            {
                if (_depositSchemeViewModel.SchemeDepositAccountRenewalParameterViewModel.EnableAutoRenewal == false)
                {
                    ModelState["SchemeDepositAccountRenewalParameterViewModel.MinimumDurationForAutoRenewal"]?.Errors?.Clear();
                    ModelState["SchemeDepositAccountRenewalParameterViewModel.MaximumDurationForAutoRenewal"]?.Errors?.Clear();
                    ModelState["SchemeDepositAccountRenewalParameterViewModel.DefaultDurationForAutoRenewal"]?.Errors?.Clear();
                }
            }

            //DepositType == "DMN"
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "DMN")
            {
                ModelState["SchemeDepositInterestParameterViewModel.LessInterestRateForPrematurity"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.PostMatureVoidInterestPeriod"]?.Errors?.Clear();
            }

            //InterestParameterToggleSwitch
            ModelState["SchemeDepositInterestParameterViewModel.TakePrematureInterestRateSameAsSaving"]?.Errors?.Clear();
            ModelState["SchemeDepositInterestParameterViewModel.TakePostMatureInterestRateSameAsSaving"]?.Errors?.Clear();
            ModelState["SchemeDepositInterestParameterViewModel.TakePostMatureInterestRateSameAsMaturityDate"]?.Errors?.Clear();

            //DepositType == "REC"
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "REC")
            {
                ModelState["SchemeDepositInterestParameterViewModel.PostMatureVoidInterestPeriod"]?.Errors?.Clear();

                if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePrematureInterestCalculation == false)
                {
                    ModelState["SchemeDepositInterestParameterViewModel.LessInterestRateForPrematurity"]?.Errors?.Clear();
                }
            }

            //DepositType == "FDP"
            if (_depositSchemeViewModel.SchemeDepositAccountParameterViewModel.DepositType == "FDP")
            {
                if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePrematureInterestCalculation == false)
                {
                    ModelState["SchemeDepositInterestParameterViewModel.LessInterestRateForPrematurity"]?.Errors?.Clear();
                }

                if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePostMatureInterestCalculation == false)
                {
                    ModelState["SchemeDepositInterestParameterViewModel.PostMatureVoidInterestPeriod"]?.Errors?.Clear();
                }

                if (_depositSchemeViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false)
                {
                    ModelState["SchemeDepositInterestParameterViewModel.MinimumMonthForPeriodicInterestPayout"]?.Errors?.Clear();
                }

            }
            // Clear Error Of All Transaction Table PrmKey
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["SchemeAccountParameterViewModel.SchemeAccountParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositAccountParameterViewModel.SchemeDepositAccountParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLimitViewModel.SchemeLimitPrmKey"]?.Errors?.Clear();
                ModelState["SchemeEstimateTargetViewModel.SchemeEstimateTargetPrmKey"]?.Errors?.Clear();
                ModelState["SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositInstallmentParameterViewModel.SchemeDepositInstallmentParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestParameterViewModel.SchemeDepositInterestParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositCertificateParameterViewModel.SchemeDepositCertificateParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDemandDepositDetailViewModel.SchemeDemandDepositDetailPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositAccountRenewalParameterViewModel.SchemeDepositAccountRenewalParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeFixedDepositParameterViewModel.SchemeFixedDepositParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositAccountClosureParameterViewModel.SchemeDepositAccountClosureParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeGeneralLedgerViewModel.SchemeGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTenureViewModel.SchemeTenurePrmKey"]?.Errors?.Clear();
                ModelState["SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.SchemePassbookPrmKey"]?.Errors?.Clear();
                ModelState["SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTenureListViewModel.SchemeTenureListPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositAgentParameterViewModel.SchemeDepositAgentParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestProvisionParameterViewModel.SchemeDepositInterestProvisionParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeBusinessOfficeViewModel.SchemeBusinessOfficePrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositAgentIncentiveViewModel.SchemeDepositAgentIncentivePrmKey"]?.Errors?.Clear();
                ModelState["SchemeClosingChargesViewModel.SchemeClosingChargesPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTransactionAmountLimitViewModel.SchemeTransactionAmountLimitPrmKey"]?.Errors?.Clear();
                ModelState["SchemeNumberOfTransactionLimitViewModel.SchemeNumberOfTransactionLimitPrmKey"]?.Errors?.Clear();
                ModelState["SchemeReportFormatViewModel.SchemeReportFormatPrmKey"]?.Errors?.Clear();
                ModelState["SchemeNoticeScheduleViewModel.SchemeNoticeSchedulePrmKey"]?.Errors?.Clear();
                ModelState["SchemeDepositInterestPayoutFrequencyViewModel.SchemeDepositInterestPayoutFrequencyPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTargetGroupViewModel.SchemeTargetGroupPrmKey"]?.Errors?.Clear();
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
        public async Task<ActionResult> Create()
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(DepositSchemeViewModel _depositSchemeViewModel)
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            await ClearModelStateOfDataTableFields(_depositSchemeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await depositSchemeRepository.Save(_depositSchemeViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "DepositScheme");
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

            return View(_depositSchemeViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<DepositSchemeIndexViewModel> schemeViewModel = await depositSchemeRepository.GetDepositSchemeIndex(StringLiteralValue.Reject);

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(
                                           List<SchemeBusinessOfficeViewModel> _schemeBusinessOffice,
                                           List<SchemeClosingChargesViewModel> _schemeClosingChargesDetail,
                                           List<SchemeDepositAgentIncentiveViewModel> _schemeDepositAgentIncentive,
                                           List<SchemeGeneralLedgerViewModel> _schemeGeneralLedger,
                                           List<SchemeNoticeScheduleViewModel> _schemeNoticeSchedule,
                                           List<SchemeNumberOfTransactionLimitViewModel> _schemeNumberOfTransactionLimit,
                                           List<SchemeReportFormatViewModel> _schemeReportFormat,
                                           List<SchemeTenureListViewModel> _tenureList,
                                           List<SchemeTransactionAmountLimitViewModel> _schemeTransactionAmountLimit,
                                           List<SchemeTargetGroupViewModel> _targetGroup)


        {
            HttpContext.Session.Add("SchemeBusinessOffice", _schemeBusinessOffice);
            HttpContext.Session.Add("SchemeClosingChargesDetail", _schemeClosingChargesDetail);
            HttpContext.Session.Add("SchemeDepositAgentIncentive", _schemeDepositAgentIncentive);
            HttpContext.Session.Add("SchemeGeneralLedger", _schemeGeneralLedger);
            HttpContext.Session.Add("SchemeNoticeSchedule", _schemeNoticeSchedule);
            HttpContext.Session.Add("SchemeNumberOfTransactionLimit", _schemeNumberOfTransactionLimit);
            HttpContext.Session.Add("SchemeReportFormat", _schemeReportFormat);
            HttpContext.Session.Add("SchemeTenureList", _tenureList);
            HttpContext.Session.Add("SchemeTransactionAmountLimit", _schemeTransactionAmountLimit);
            HttpContext.Session.Add("SchemeTargetGroup", _targetGroup);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<DepositSchemeIndexViewModel> schemeViewModel = await depositSchemeRepository.GetDepositSchemeIndex(StringLiteralValue.Unverified);

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<DepositSchemeIndexViewModel> schemeViewModel = await depositSchemeRepository.GetDepositSchemeIndex(StringLiteralValue.Verify);

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid SchemeId)
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            bool data = await depositSchemeRepository.GetSessionValues(depositSchemeRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Unverified);

            DepositSchemeViewModel schemeViewModel = await depositSchemeRepository.GetDepositSchemeEntry(SchemeId, StringLiteralValue.Unverified);

            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(DepositSchemeViewModel _depositSchemeViewModel, string Command)
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_depositSchemeViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_depositSchemeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                _depositSchemeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await depositSchemeRepository.VerifyRejectDelete(_depositSchemeViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "DepositScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _depositSchemeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await depositSchemeRepository.VerifyRejectDelete(_depositSchemeViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "DepositScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "DepositScheme");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_depositSchemeViewModel.SchemeId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid SchemeId)
        {

            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();
            ViewBag.DepositSchemeParameter = depositSchemeParameterViewModel;

            bool data = await depositSchemeRepository.GetSessionValues(depositSchemeRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Verify);

            DepositSchemeViewModel schemeViewModel = await depositSchemeRepository.GetDepositSchemeEntry(SchemeId, StringLiteralValue.Verify);


            if (schemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(schemeViewModel);
        }

    }
}