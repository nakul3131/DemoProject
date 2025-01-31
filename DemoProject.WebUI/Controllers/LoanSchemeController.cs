using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Setting/GeneralLedger/Loan/Scheme")]
    public class LoanSchemeController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ILoanSchemeParameterRepository loanSchemeParameterRepository;
        private readonly ILoanSchemeRepository loanSchemeRepository;

        public LoanSchemeController(IAccountDetailRepository _accountDetailRepository, ILoanSchemeParameterRepository _loanSchemeParameterRepository, ILoanSchemeRepository _loanSchemeRepository)
        {

            accountDetailRepository = _accountDetailRepository;
            loanSchemeParameterRepository = _loanSchemeParameterRepository;
            loanSchemeRepository = _loanSchemeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid SchemeId)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            bool data = await loanSchemeRepository.GetSessionValues(loanSchemeRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Reject);

            LoanSchemeViewModel loanSchemeViewModel = await loanSchemeRepository.GetLoanSchemeEntry(SchemeId, StringLiteralValue.Reject);

            if (loanSchemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(LoanSchemeViewModel _loanSchemeViewModel, string Command)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_loanSchemeViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_loanSchemeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await loanSchemeRepository.Amend(_loanSchemeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await loanSchemeRepository.VerifyRejectDelete(_loanSchemeViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.Delete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "LoanScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "LoanScheme");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_loanSchemeViewModel);
        }

        private void ClearModelStateOfDataTableFields(LoanSchemeViewModel _loanSchemeViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "SchemeTenureListViewModel,SchemeReportFormatViewModel, SchemeDocumentViewModel, SchemeNoticeScheduleViewModel,SchemeGeneralLedgerViewModel, SchemeBusinessOfficeViewModel," +
                                        "SchemeTargetGroupViewModel, SchemeLoanChargesParameterViewModel, SchemeLoanOverduesActionViewModel," +
                                        "SchemeVehicleTypeLoanParameterViewModel,SchemePreownedVehicleLoanParameterViewModel,SchemeConsumerDurableLoanItemViewModel,SchemeEducationalCourseViewModel,SchemeInstituteViewModel ";

            LoanSchemeParameterViewModel loanSchemeParameterViewModel = loanSchemeParameterRepository.GetActiveEntry1();

            // Get Loan Type For Valid Tables Insertion
            string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.LoanTypeId);

            //================================ Account Parameter ==============================                       
            // Check EnableMaturityDateOverride
            if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableMaturityDateOverride == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumOverridePeriod"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumOverridePeriod"]?.Errors?.Clear();
            }

            // Check EnableNumberOfJointAccountHoldingLimit
            if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfJointAccountHoldingLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumJointAccountHolder"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultJointAccountHolder"]?.Errors?.Clear();
            }

            // Check EnableNumberOfNomineeLimit
            if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableNumberOfNomineeLimit == false)
            {
                ModelState["SchemeAccountParameterViewModel.MinimumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.MaximumNominee"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.DefaultNominee"]?.Errors?.Clear();
            }

            //   =====================SchemeLoanAccountParameterViewModel ======================
            // Check EnableGuarantorDetail
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.MinimumNumberOfGuarantors"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.DefaultNumberOfGuarantors"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.RequiredGuarantorsSecurityPercentageOfLoan"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableAcquaintanceDetails == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.MinimumAcquaintance"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.MaximumAcquaintance"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableSWOTAnalysis == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.SWOTAnalysisMinimumLength"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnablePastCreditHistory == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.PastCreditHistoryMinimumLength"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableLegalAndRegulatoryCompliance == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.LegalAndRegulatoryComplianceMinimumLength"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureCIBILScore == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.MinimumCIBILScore"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.MaximumCIBILScore"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio == false)
            {
                ModelState["SchemeLoanAccountParameterViewModel.MinimumDebtToIncomeRatio"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.MaximumDebtToIncomeRatio"]?.Errors?.Clear();
            }

            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.IsRequiredOrdinaryMembership == true)
            {
                ModelState["SchemeLoanAccountParameterViewModel.IsRequiredNominalMembership"]?.Errors?.Clear();
            }

            //  Check SchemeLoanAgreementNumber  
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableAgreementNumber == false || _loanSchemeViewModel.SchemeLoanAgreementNumberViewModel.EnableAutoAgreementNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanAgreementNumberViewModel";
            }

            // MultipleDisbursement
            if (_loanSchemeViewModel.SchemeHomeLoanViewModel.EnableMultipleDisbursement == false)
            {
                ModelState["SchemeHomeLoanViewModel.MaximumNumberOfTimeDisbursement"]?.Errors?.Clear();
            }

            //=======================================================================================

            //Check EnableTenure
            if (_loanSchemeViewModel.SchemeAccountParameterViewModel.EnableTenure == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeTenureViewModel";
            }

            // Check EnableAutoAccountNumber
            if (_loanSchemeViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeCustomerAccountNumberViewModel";
            }

            // Check ApplicationParameter
            if (loanSchemeParameterViewModel.EnableApplicationParameter == false || _loanSchemeViewModel.SchemeAccountParameterViewModel.EnableApplication == false || _loanSchemeViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeApplicationParameterViewModel";
            }

            // Check PassbookParameter
            if (loanSchemeParameterViewModel.EnablePassbookParameter == false || _loanSchemeViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == false || _loanSchemeViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber == false)
            {
                errorViewModelName = errorViewModelName + ",SchemePassbookViewModel";
            }

            // Check BankingChannelParameter
            if (loanSchemeParameterViewModel.EnableBankingChannelParameter == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeAccountBankingChannelParameterViewModel";
            }

            // Number Of Missed Installment
            // Loan Installment Parameter & Loan Repayment Schedule Parameter ViewModel
            if (loanType == StringLiteralValue.CashCreditLoan || (((loanType == StringLiteralValue.GoldLoan) || (loanType == StringLiteralValue.LoanAgainstDeposit)) && _loanSchemeViewModel.EnableLoanInstallment == false))
            {
                ModelState["SchemeLoanFineInterestParameterViewModel.NumberOfMissedInstallment"]?.Errors?.Clear();
                errorViewModelName = errorViewModelName + ",SchemeLoanRepaymentScheduleParameterViewModel,SchemeLoanInstallmentParameterViewModel";
            }
            else
            {
                ModelState["SchemeLoanFineInterestParameterViewModel.FineDays"]?.Errors?.Clear();
            }

            //Loan Against Deposit Parameter 
            if (loanType == StringLiteralValue.LoanAgainstDeposit)
            {
                ModelState["SchemeLoanAccountParameterViewModel.SharesRatioWithLoan"]?.Errors?.Clear();

                //Is Applicable All General Ledgers
                if (_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.IsApplicableAllGeneralLedgers == true)
                {
                    ModelState["SchemeLoanAgainstDepositGeneralLedgerViewModel.MultiDepositeGeneralLedgerId"]?.Errors?.Clear();
                }
                //Enable Third Person Deposit Attachment
                if (_loanSchemeViewModel.SchemeLoanAgainstDepositParameterViewModel.EnableThirdPersonDepositAttachment == false)
                {
                    ModelState["SchemeLoanAgainstDepositParameterViewModel.MinimumAdditionalInterestRateForThirdPersonDeposit"]?.Errors?.Clear();
                    ModelState["SchemeLoanAgainstDepositParameterViewModel.MaximumAdditionalInterestRateForThirdPersonDeposit"]?.Errors?.Clear();
                }
            }
            else
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanAgainstDepositParameterViewModel,SchemeLoanAgainstDepositGeneralLedgerViewModel";
            }

            // Check Gold Loan
            if (loanType == StringLiteralValue.GoldLoan)
            {
                ModelState["SchemeLoanAccountParameterViewModel.SharesRatioWithLoan"]?.Errors?.Clear();

                if (_loanSchemeViewModel.SchemeGoldLoanParameterViewModel.EnableSuperValuation == false)
                {
                    ModelState["SchemeGoldLoanParameterViewModel.SuperValuationsInYear"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.TimePeriodBetweenTwoSuperValuations"]?.Errors?.Clear();
                }

                if (_loanSchemeViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == false)
                {
                    ModelState["SchemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForDb"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.MaximumFileSizeForGoldPhotoUploadInDb"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.MaximumFileSizeForGoldPhotoUploadInLocalStorage"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.MinimumGoldPhoto"]?.Errors?.Clear();
                    ModelState["SchemeGoldLoanParameterViewModel.MaximumGoldPhoto"]?.Errors?.Clear();
                }
            }
            else
            {
                errorViewModelName = errorViewModelName + ",SchemeGoldLoanParameterViewModel";
            }

            // Home Loan
            if (loanType != StringLiteralValue.HomeLoan)
            {
                errorViewModelName = errorViewModelName + ",SchemeHomeLoanViewModel";
            }

            // Loan Against Property
            if (loanType != StringLiteralValue.LoanAgainstProperty)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanAgainstPropertyViewModel";
            }

            // Business Loan
            if (loanType != StringLiteralValue.ShortTermBusinessLoan)
            {
                errorViewModelName = errorViewModelName + ",SchemeBusinessLoanViewModel";
            }

            //Cash Credit
            if (loanType == StringLiteralValue.CashCreditLoan)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanPrePartPaymentParameterViewModel,SchemeLoanPreFullPaymentParameterViewModel";
               ModelState["SchemeLoanAccountParameterViewModel.SharesRatioWithLoan"]?.Errors?.Clear();

            }
            else
            {
                errorViewModelName = errorViewModelName + ",SchemeCashCreditLoanParameterViewModel";
            }

            // Educational Loan
            if (loanType != StringLiteralValue.EducationalLoan)
            {
                errorViewModelName = errorViewModelName + ",SchemeEducationLoanParameterViewModel";
            }
            else
            {
                ModelState["SchemeLoanAccountParameterViewModel.SharesRatioWithLoan"]?.Errors?.Clear();
            }


            // Check LoanSettlementAccountParameter
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableSettlementAccount == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanSettlementAccountParameterViewModel";
            }

            // Check EnablePrePayment
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnablePrePayment == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanPrePartPaymentParameterViewModel";
            }

            // Check EnableForeClosure
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableForeClosure == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanPreFullPaymentParameterViewModel";
            }

            // Check EnableLoanFineInterest
            if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanFineInterest == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanFineInterestParameterViewModel";
            }

            // Check EnableLoanInterestProvision
            if (_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.EnableLoanInterestProvision == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanInterestProvisionParameterViewModel";
            }

            string interestMethodType = accountDetailRepository.GetSysNameOfInterestMethodTypeById(_loanSchemeViewModel.SchemeLoanInterestParameterViewModel.InterestMethodId);

            if (interestMethodType != "COMPOUND")
            {
                ModelState["SchemeLoanInterestParameterViewModel.InterestCompoundingFrequencyId"]?.Errors?.Clear();
            }

            string interestMethod = accountDetailRepository.GetSysNameOfInterestMethodTypeById(_loanSchemeViewModel.SchemeLoanFineInterestParameterViewModel.InterestMethodId);

            if (interestMethod == "FLAT")
            {
                ModelState["SchemeLoanFineInterestParameterViewModel.InterestRateChargedDurationId"]?.Errors?.Clear();
                ModelState["SchemeLoanFineInterestParameterViewModel.DaysInYearId"]?.Errors?.Clear();
                ModelState["SchemeLoanFineInterestParameterViewModel.LendingRepaymentsInterestCalculationId"]?.Errors?.Clear();
            }

            // Check EnableRebateInterest
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableRebateInterest == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanInterestRebateParameterViewModel";
            }

            //Toggleswitch errors
            //IsApplicablePrePartPaymentForInterestRebate
            if (_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel.IsApplicablePrePartPaymentForInterestRebate == false)
            {
                ModelState["SchemeLoanInterestRebateParameterViewModel.IsApplicablePrePartPaymentForInterestRebate"]?.Errors?.Clear();
            }

            //IsApplicableForeClosureForInterestRebate
            if (_loanSchemeViewModel.SchemeLoanInterestRebateParameterViewModel.IsApplicableForeClosureForInterestRebate == false)
            {
                ModelState["SchemeLoanInterestRebateParameterViewModel.IsApplicableForeClosureForInterestRebate"]?.Errors?.Clear();
            }

            // Check EnableTargetEstimationParameter
            if (loanSchemeParameterViewModel.EnableTargetEstimationParameter == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeEstimateTargetViewModel";
            }

            // EnablePaymentDueReminder
            if (_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel.EnablePaymentDueReminder == false)
            {
                ModelState["SchemeLoanPaymentReminderParameterViewModel.StartDaysBeforePaymentDueDate"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.OccursEveryDayForDueReminder"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.StartDaysAfterPaymentDueDate"]?.Errors?.Clear();
            }

            //EnableMissedPaymentReminder
            if (_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel.EnableMissedPaymentReminder == false)
            {
                ModelState["SchemeLoanPaymentReminderParameterViewModel.OccursEveryDayForMissedPaymentReminder"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.MaximumMissedPaymentReminders"]?.Errors?.Clear();
            }

            //EnableOverduesPaymentReminder
            if (_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel.EnableOverduesPaymentReminder == false)
            {
                ModelState["SchemeLoanPaymentReminderParameterViewModel.StartDaysAfterOverduePaymentDate"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.OccursEveryDayForOverduePaymentReminder"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.MaximumOverduePaymentReminders"]?.Errors?.Clear();
            }

            //EnableNPADeclarationReminder
            if (_loanSchemeViewModel.SchemeLoanPaymentReminderParameterViewModel.EnableNPADeclarationReminder == false)
            {
                ModelState["SchemeLoanPaymentReminderParameterViewModel.StartDaysAfterNPADeclarationDate"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.OccursEveryDayForNPADeclarationReminder"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.MaximumNPADeclarationReminders"]?.Errors?.Clear();
            }


            //EnableLoanDistributorParameter
            if (loanSchemeParameterViewModel.EnableLoanDistributorParameter == false || _loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableDistributor == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanDistributorParameterViewModel";
            }

            //EnableAdvance
            if (_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel.EnableAdvance == false)
            {
                ModelState["SchemeLoanDistributorParameterViewModel.MinimumAdvanceLimit"]?.Errors?.Clear();
                ModelState["SchemeLoanDistributorParameterViewModel.MaximumAdvanceLimit"]?.Errors?.Clear();
            }
            //EnableDistributorInterestRate
            if (_loanSchemeViewModel.SchemeLoanDistributorParameterViewModel.EnableDistributorInterestRate == false)
            {
                ModelState["SchemeLoanDistributorParameterViewModel.MinimumDistributorInterestRate"]?.Errors?.Clear();
                ModelState["SchemeLoanDistributorParameterViewModel.MaximumDistributorInterestRate"]?.Errors?.Clear();
                ModelState["SchemeLoanDistributorParameterViewModel.DefaultDistributorInterestRate"]?.Errors?.Clear();
            }

            // Check EnableFunder
            if (_loanSchemeViewModel.SchemeLoanAccountParameterViewModel.EnableFunder == false)
            {
                errorViewModelName = errorViewModelName + ",SchemeLoanFunderParameterViewModel";
            }

            // Passbook ToggleSwitch Error Clear
            if (_loanSchemeViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber == false)
            {
                ModelState["SchemePassbookViewModel.MaskTypeCharacterForPassbook"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.StartPassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.EndPassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.PassbookNumberIncrementBy"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.IsRandomlyGeneratedPassbookNumber"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.ReGenerateUnusedPassbookNumber"]?.Errors?.Clear();

            }
            ModelState["SchemePassbookViewModel.IsVisiblePassbookNumber"]?.Errors?.Clear();
            ModelState["SchemePassbookViewModel.EnablePassbookVerification"]?.Errors?.Clear();
            ModelState["SchemePassbookViewModel.DuplicatePassbookCharges"]?.Errors?.Clear();
            ModelState["SchemePassbookViewModel.IsRandomlyGeneratedPassbookNumber"]?.Errors?.Clear();
            ModelState["SchemePassbookViewModel.ReGenerateUnusedPassbookNumber"]?.Errors?.Clear();

            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["SchemeApplicationParameterViewModel.SchemeApplicationParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeCustomerAccountNumberViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanAgreementNumberViewModel.SchemeLoanAgreementNumberPrmKey"]?.Errors?.Clear();
                ModelState["SchemeEstimateTargetViewModel.SchemeEstimateTargetPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTenureViewModel.SchemeTenurePrmKey"]?.Errors?.Clear();
                ModelState["SchemePassbookViewModel.SchemePassbookPrmKey"]?.Errors?.Clear();
                ModelState["SchemeReportFormatViewModel.SchemeReportFormatPrmKey"]?.Errors?.Clear();
                ModelState["SchemeAccountBankingChannelParameterViewModel.SchemeAccountBankingChannelParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanRepaymentScheduleParameterViewModel.SchemeLoanRepaymentScheduleParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanSettlementAccountParameterViewModel.SchemeLoanSettlementAccountParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanFunderParameterViewModel.SchemeLoanFunderParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanDistributorParameterViewModel.SchemeLoanDistributorParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanInterestParameterViewModel.SchemeLoanInterestParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanFineInterestParameterViewModel.SchemeLoanFineInterestParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanInterestProvisionParameterViewModel.SchemeLoanInterestProvisionParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanInterestRebateParameterViewModel.SchemeLoanInterestRebateParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanArrearParameterViewModel.SchemeLoanArrearParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanPreFullPaymentParameterViewModel.SchemeLoanPreFullPaymentParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanPrePartPaymentParameterViewModel.SchemeLoanPrePartPaymentParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeConsumerDurableLoanItemViewModel.SchemeConsumerDurableLoanItemPrmKey"]?.Errors?.Clear();
                ModelState["SchemeVehicleTypeLoanParameterViewModel.SchemeVehicleTypeLoanParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemePreownedVehicleLoanParameterViewModel.SchemePreownedVehicleLoanParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeCashCreditLoanParameterViewModel.SchemeCashCreditLoanParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanPaymentReminderParameterViewModel.SchemeLoanPaymentReminderParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeEducationLoanParameterViewModel.SchemeEducationLoanParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeEducationalCourseViewModel.SchemeEducationalCoursePrmKey"]?.Errors?.Clear();
                ModelState["SchemeInstituteViewModel.SchemeInstitutePrmKey"]?.Errors?.Clear();
                ModelState["SchemeGoldLoanParameterViewModel.SchemeGoldLoanParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanSanctionAuthorityViewModel.SchemeLoanSanctionAuthorityPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanAgainstDepositParameterViewModel.SchemeLoanAgainstDepositParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanAgainstDepositGeneralLedgerViewModel.SchemeLoanAgainstDepositGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["SchemeHomeLoanViewModel.SchemeHomeLoanPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanAgainstPropertyViewModel.SchemeLoanAgainstPropertyPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanInstallmentParameterViewModel.SchemeLoanInstallmentParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeAccountParameterViewModel.SchemeAccountParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanAccountParameterViewModel.SchemeLoanAccountParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeTenureListViewModel.SchemeTenureListPrmKey"]?.Errors?.Clear();
                ModelState["SchemeDocumentViewModel.SchemeDocumentPrmKey"]?.Errors?.Clear();
                ModelState["SchemeNoticeScheduleViewModel.SchemeNoticeSchedulePrmKey"]?.Errors?.Clear();
                ModelState["SchemeBusinessOfficeViewModel.SchemeBusinessOfficePrmKey"]?.Errors?.Clear();
                ModelState["SchemeTargetGroupViewModel.SchemeTargetGroupPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanChargesParameterViewModel.SchemeLoanChargesParameterPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanOverduesActionViewModel.SchemeLoanOverduesActionPrmKey"]?.Errors?.Clear();
                ModelState["SchemeGeneralLedgerViewModel.SchemeGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["SchemeLoanRecoveryActionViewModel.SchemeLoanRecoveryActionPrmKey"]?.Errors?.Clear();
                ModelState["SchemeBusinessLoanViewModel.SchemeBusinessLoanPrmKey"]?.Errors?.Clear();
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
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(LoanSchemeViewModel _loanSchemeViewModel)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            ClearModelStateOfDataTableFields(_loanSchemeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                bool result = await loanSchemeRepository.Save(_loanSchemeViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "LoanScheme");
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

            return View(_loanSchemeViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueSchemeName(string NameOfScheme)
        {
            bool data = loanSchemeRepository.GetUniqueSchemeName(NameOfScheme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLoanTypeSysNameByLoanTypeId(Guid LoanTypeId)
        {
            string data = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(LoanTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<LoanSchemeIndexViewModel> loanSchemeViewModels = await loanSchemeRepository.GetLoanSchemeIndex(StringLiteralValue.Reject);

            if (loanSchemeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModels);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables( List<SchemeBusinessOfficeViewModel> _businessOffice,List<SchemeConsumerDurableLoanItemViewModel> _consumerDurableLoan,List<SchemeDocumentViewModel> _document, List<SchemeGeneralLedgerViewModel> _schemeGeneralLedger, List<SchemeLoanChargesParameterViewModel> _loanCharges, List<SchemeLoanInstallmentParameterViewModel> _loanTransactionParameter,  List<SchemeLoanOverduesActionViewModel> _loanOverduesAction,
                                          List<SchemeNoticeScheduleViewModel> _noticeSchedule,List<SchemePreownedVehicleLoanParameterViewModel> _preownedVehicleLoan, 
                                          List<SchemeReportFormatViewModel> _reportFormat,List<SchemeTargetGroupViewModel> _targetGroup, List<SchemeTenureListViewModel> _tenureList ,List<SchemeVehicleTypeLoanParameterViewModel> _vehicleTypeLoan,List<SchemeEducationalCourseViewModel> _educationalCourse,List<SchemeInstituteViewModel> _institute)
        {
            HttpContext.Session.Add("SchemeBusinessOffice", _businessOffice);
            HttpContext.Session.Add("SchemeConsumerDurableLoanItem", _consumerDurableLoan);
            HttpContext.Session.Add("SchemeDocument", _document);
            HttpContext.Session.Add("EducationalCourse", _educationalCourse);
            HttpContext.Session.Add("Institute", _institute);
            HttpContext.Session.Add("SchemeGeneralLedger", _schemeGeneralLedger);
            HttpContext.Session.Add("SchemeLoanChargesParameter", _loanCharges);
            HttpContext.Session.Add("SchemeLoanInstallmentParameter", _loanTransactionParameter);
            HttpContext.Session.Add("SchemeLoanOverduesAction", _loanOverduesAction);
            HttpContext.Session.Add("SchemeNoticeSchedule", _noticeSchedule);
            HttpContext.Session.Add("SchemePreownedVehicleLoanParameter", _preownedVehicleLoan);
            HttpContext.Session.Add("SchemeReportFormat", _reportFormat);
            HttpContext.Session.Add("SchemeTargetGroup", _targetGroup);
            HttpContext.Session.Add("SchemeTenureList", _tenureList);
            HttpContext.Session.Add("SchemeVehicleTypeLoanParameter", _vehicleTypeLoan);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<LoanSchemeIndexViewModel> loanSchemeViewModels = await loanSchemeRepository.GetLoanSchemeIndex(StringLiteralValue.Unverified);

            if (loanSchemeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<LoanSchemeIndexViewModel> loanSchemeViewModels = await loanSchemeRepository.GetLoanSchemeIndex(StringLiteralValue.Verify);

            if (loanSchemeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid SchemeId)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            bool data = await loanSchemeRepository.GetSessionValues(loanSchemeRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Unverified);

            LoanSchemeViewModel loanSchemeViewModel = await loanSchemeRepository.GetLoanSchemeEntry(SchemeId, StringLiteralValue.Unverified);

            if (loanSchemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(LoanSchemeViewModel _loanSchemeViewModel, string Command)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_loanSchemeViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_loanSchemeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _loanSchemeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await loanSchemeRepository.VerifyRejectDelete(_loanSchemeViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "LoanScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _loanSchemeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await loanSchemeRepository.VerifyRejectDelete(_loanSchemeViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "LoanScheme"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "LoanScheme");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_loanSchemeViewModel.SchemeId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid SchemeId)
        {

            await loanSchemeRepository.GetLoanSchemeIndex(StringLiteralValue.Verify);
            bool data = await loanSchemeRepository.GetSessionValues(loanSchemeRepository.GetPrmKeyById(SchemeId), StringLiteralValue.Verify);

            LoanSchemeViewModel loanSchemeViewModel = await loanSchemeRepository.GetLoanSchemeEntry(SchemeId, StringLiteralValue.Verify);

            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;


            if (loanSchemeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeViewModel);
        }

    }
}