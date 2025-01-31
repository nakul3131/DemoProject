using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Customer/Account/Opening/Loan")]
    public class CustomerLoanAccountController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ILoanCustomerAccountRepository loanCustomerAccountRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonRepository personRepository;
        private readonly IPersonInformationDetailRepository personInformationDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;

        public CustomerLoanAccountController(IAccountDetailRepository _accountDetailRepository, ILoanCustomerAccountRepository _loanCustomerAccountRepository, IConfigurationDetailRepository _configurationDetailRepository, IPersonRepository _personRepository, IPersonDetailRepository _personDetailRepository, IPersonInformationDetailRepository _personInformationDetailRepository,
            IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, ISchemeDetailRepository _schemeDetailRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            loanCustomerAccountRepository = _loanCustomerAccountRepository;
            personRepository = _personRepository;
            personDetailRepository = _personDetailRepository;
            personInformationDetailRepository = _personInformationDetailRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CustomerAccountId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            LoanCustomerAccountViewModel loanCustomerAccountViewModel = await loanCustomerAccountRepository.GetLoanCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Reject);

            bool data = await loanCustomerAccountRepository.GetSessionValues(loanCustomerAccountViewModel, StringLiteralValue.Reject);

            if (loanCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }
            return View(loanCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_loanCustomerAccountViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_loanCustomerAccountViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await loanCustomerAccountRepository.Amend(_loanCustomerAccountViewModel);

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
                    bool result = await loanCustomerAccountRepository.VerifyRejectDelete(_loanCustomerAccountViewModel, StringLiteralValue.Delete);


                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CustomerLoanAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "CustomerLoanAccount");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_loanCustomerAccountViewModel);
        }

        [NonAction]
        private async Task ClearModelStateOfDataTableFields(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType)
        {
            LoanCustomerAccountDetailViewModel loanCustomerAccountDetailViewModel = new LoanCustomerAccountDetailViewModel();

            // Assign All DataTable ViewModels
            string errorViewModelName = "CustomerJointAccountHolderViewModel, CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardianViewModel, PersonAddressViewModel, PersonContactDetailViewModel,CustomerLoanAccountGuarantorDetailViewModel,CustomerVehicleLoanPhotoViewModel,CustomerGoldLoanCollateralDetailViewModel,CustomerGoldLoanCollateralPhotoViewModel,CustomerAccountDocumentViewModel,CustomerAccountNoticeScheduleViewModel,PersonBorrowingDetailViewModel" +
                "PersonIncomeTaxDetailViewModel,PersonCourtCaseViewModel,PersonAdditionalIncomeDetailViewModel,CustomerLoanAcquaintanceDetailViewModel,CustomerConsumerLoanCollateralDetailViewModel,CustomerLoanAgainstDepositCollateralDetailViewModel";

            // Get Record Of All Master Table And Transaction Table
            CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

            string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.LoanTypeId);
            string occupation = personDetailRepository.GetSysNameOfOccupationById(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.OccupationId);

            if ((occupation != StringLiteralValue.Salaried && occupation != StringLiteralValue.SelfEmployedBusiness && occupation != StringLiteralValue.SelfEmployedProfessional) || _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee == true)
            {
                errorViewModelName = errorViewModelName + ",PersonEmploymentDetailViewModel";
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableApplication == true ? customerLoanAccountOpeningConfigViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber : true)
            {
                ModelState["CustomerLoanAccountViewModel.ApplicationNumber"]?.Errors?.Clear();
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAgreementNumber == true ? customerLoanAccountOpeningConfigViewModel.SchemeLoanAgreementNumberViewModel.EnableAutoAgreementNumber : true)
            {
                ModelState["CustomerLoanAccountViewModel.AgreementNumber"]?.Errors?.Clear();
            }

            if ((customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false))
            {
                ModelState["AlternateAccountNumber2"]?.Errors?.Clear();
            }
            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableTenure)
            {
                ModelState["CustomerLoanAccountViewModel.TenureListId"]?.Errors?.Clear();
            }
            else
            {
                ModelState["CustomerLoanAccountViewModel.Tenure"]?.Errors?.Clear();
            }

            // Check EnableSWOTAnalysis, If Disable Then Clear Regarding Fields
            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableSWOTAnalysis == false)
            {
                ModelState["CustomerLoanAccountViewModel.StrengthsFactors"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountViewModel.WeaknessesFactors"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountViewModel.OpportunitiesFactors"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountViewModel.ThreatsFactors"]?.Errors?.Clear();
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.StrengthsFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransStrengthsFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.WeaknessesFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransWeaknessesFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.OpportunitiesFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransOpportunitiesFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.ThreatsFactors = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransThreatsFactors = "None";

            }

            // Check EnablePastCreditHistory, If Disable Then Clear Regarding Fields
            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnablePastCreditHistory == false)
            {
                ModelState["CustomerLoanAccountViewModel.PastCreditHistory"]?.Errors?.Clear();
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.PastCreditHistory = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransPastCreditHistory = "None";
            }

            // Check EnableLegalAndRegulatoryCompliance, If Disable Then Clear Regarding Fields
            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableLegalAndRegulatoryCompliance == false)
            {
                ModelState["CustomerLoanAccountViewModel.LegalAndRegulatoryCompliance"]?.Errors?.Clear();
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.LegalAndRegulatoryCompliance = "None";
                _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.TransLegalAndRegulatoryCompliance = "None";
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
            {
                ModelState["AlternateAccountNumber3"]?.Errors?.Clear();
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true ? customerLoanAccountOpeningConfigViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber :true )
            {
                ModelState["PassbookNumber"]?.Errors?.Clear();
            }

            //Clear Model State of Sms
            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == false)
            {
                ModelState["CustomerAccountSmsServiceViewModel.ActivationDate"]?.Errors?.Clear();
                ModelState["CustomerAccountSmsServiceViewModel.ExpiryDate"]?.Errors?.Clear();
            }

            //Clear Model State of Email
            if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == false)
            {
                ModelState["CustomerAccountEmailServiceViewModel.ActivationDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.ExpiryDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.CloseDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.StatementFrequency"]?.Errors?.Clear();
            }

            //Clear Model State of Debt To Income Ratio
            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio == false)
            {
                errorViewModelName = errorViewModelName + ",CustomerLoanAccountDebtToIncomeRatioViewModel";
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors == 0)
            {
                ModelState["CustomerLoanAccountGuarantorDetailViewModel.GuaranteePercentage"]?.Errors?.Clear();
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber)
            {
                ModelState["AccountNumber"]?.Errors?.Clear();
            }

            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation == false)
            {
                errorViewModelName = errorViewModelName + ",CustomerLoanFieldInvestigationViewModel";
            }

            if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == false)
            {
                errorViewModelName = errorViewModelName + ",CustomerAccountStandingInstructionViewModel";
            }

            //Clear Model State of Vehicle Loan
            if (loanType != StringLiteralValue.VehicleLoan)
            {
                errorViewModelName = errorViewModelName + ",CustomerVehicleLoanCollateralDetailViewModel,CustomerPreOwnedVehicleLoanInspectionViewModel,CustomerVehicleLoanInsuranceDetailViewModel ,CustomerVehicleLoanPermitDetailViewModel,CustomerVehicleLoanContractDetailViewModel,";
            }
            else
            {
                if (_loanCustomerAccountViewModel.CustomerVehicleLoanInsuranceDetailViewModel.TypeOfCoverage != "TPR")
                {
                    ModelState["CustomerVehicleLoanInsuranceDetailViewModel.HasAddedZeroDepreciation"]?.Errors?.Clear();
                    ModelState["CustomerVehicleLoanInsuranceDetailViewModel.HasAddedEngineProtection"]?.Errors?.Clear();
                    ModelState["CustomerVehicleLoanInsuranceDetailViewModel.HasAddedReturnToInvoice"]?.Errors?.Clear();
                    ModelState["CustomerVehicleLoanInsuranceDetailViewModel.HasAddedRoadsideAssistance"]?.Errors?.Clear();
                }

                if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == false)
                {
                    errorViewModelName = errorViewModelName + ",CustomerVehicleLoanPermitDetailViewModel,CustomerVehicleLoanContractDetailViewModel";
                }
                else
                {
                    if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.HasContract == false)
                    {
                        errorViewModelName = errorViewModelName + ",CustomerVehicleLoanContractDetailViewModel";
                    }
                    else
                    {
                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel.ContractNature != "OTH")
                        {
                            ModelState["CustomerVehicleLoanContractDetailViewModel.OtherContractNatureDetails"]?.Errors?.Clear();
                            _loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel.OtherContractNatureDetails = "None";
                        }
                    }
                }

                //Clear Error If LoanPurpose Is Purchase New Vehicle 
                if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose == "NEW")
                {
                    errorViewModelName = errorViewModelName + ",CustomerPreOwnedVehicleLoanInspectionViewModel";
                }
                else
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == false)
                    {
                        errorViewModelName = errorViewModelName + ",CustomerPreOwnedVehicleLoanInspectionViewModel";
                    }
                    else
                    {
                        if (_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.OdometerReading < 100000)
                        {
                            ModelState["CustomerPreOwnedVehicleLoanInspectionViewModel.MaintenanceRemark"]?.Errors?.Clear();
                            _loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.MaintenanceRemark = "None";
                        }

                        if (_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.IsUnderAnyHypothecation == false)
                        {
                            ModelState["CustomerPreOwnedVehicleLoanInspectionViewModel.HypothecationInstitutionName"]?.Errors?.Clear();
                            ModelState["CustomerPreOwnedVehicleLoanInspectionViewModel.HypothecationInstitutionOtherDetails"]?.Errors?.Clear();
                            _loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.HypothecationInstitutionName = "None";
                            _loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.HypothecationInstitutionOtherDetails = "None";
                        }

                        if (_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.RCAvailability == true)
                        {
                            ModelState["CustomerPreOwnedVehicleLoanInspectionViewModel.ReasonForUnavailability"]?.Errors?.Clear();
                            _loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.ReasonForUnavailability = "None";
                        }
                    }

                }

                string nameOfColour = accountDetailRepository.GetNameOfColourByColourId(_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.ColourId);
                if (nameOfColour != StringLiteralValue.OtherColour)
                {
                    ModelState["CustomerVehicleLoanCollateralDetailViewModel.OtherColour"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.OtherColour = "None";
                }

            }

            //Clear Model State of Loan Against Property
            if (loanType == StringLiteralValue.LoanAgainstProperty || loanType == StringLiteralValue.HomeLoan)
            {
                if (_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyType != "OTH")
                {
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyTypeOther"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyTypeOther = "None";
                }

                if (_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyOwnershipStatus != "OTH")
                {
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyOwnershipStatusOther"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.PropertyOwnershipStatusOther = "None";
                }

                if (_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.HasExistingPropertyLiabilities == false)
                {
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.OutstandingLoanAmount"]?.Errors?.Clear();
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.LenderName"]?.Errors?.Clear();
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.RemainingTerm"]?.Errors?.Clear();
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.MonthlyRepaymentAmount"]?.Errors?.Clear();
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.AnyAdditionalLiens"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.LenderName = "None";
                    _loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.AnyAdditionalLiens = "None";
                }

                if (_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.IsPropertyFreeOfAnyLegalDisputes == false)
                {
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.LegalDisputeDetails"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.LegalDisputeDetails = "None";
                }

                if (_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel.HasMortgageInsurance == false)
                {
                    ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.MortgageInsuranceAmount"]?.Errors?.Clear();
                }
            }
            else
            {
                errorViewModelName = errorViewModelName + ",CustomerLoanAgainstPropertyCollateralDetailViewModel";
            }

            //Clear Model State of Gold Loan
            if (loanType == StringLiteralValue.GoldLoan)
            {
                ModelState["CustomerLoanAccountViewModel.MinuteOfMeetingAgendaId"]?.Errors?.Clear();

                if (_loanCustomerAccountViewModel.CustomerGoldLoanCollateralDetailViewModel != null)
                {
                    if (_loanCustomerAccountViewModel.CustomerGoldLoanCollateralDetailViewModel.HasAnyDamage == false)
                    {
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.DamageWeight"]?.Errors?.Clear();
                    }

                    if (_loanCustomerAccountViewModel.CustomerGoldLoanCollateralDetailViewModel.HasAnyWestage == false)
                    {
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.WestageWeight"]?.Errors?.Clear();
                    }

                    if (_loanCustomerAccountViewModel.CustomerGoldLoanCollateralDetailViewModel.HasDiamond == false)
                    {
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.NumberOfDiamond"]?.Errors?.Clear();
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.DiamondCarat"]?.Errors?.Clear();
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.ClarityColour"]?.Errors?.Clear();
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.DiamondWeight"]?.Errors?.Clear();
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.DiamondPrice"]?.Errors?.Clear();
                        ModelState["CustomerGoldLoanCollateralDetailViewModel.DiamondValuation"]?.Errors?.Clear();
                    }
                }
            }
            else
            {
                errorViewModelName = errorViewModelName + ",CustomerGoldLoanCollateralDetailViewModel,CustomerGoldLoanCollateralPhotoViewModel";
            }

            //Clear Model State of Cash Credit Loan
            if (loanType == StringLiteralValue.CashCreditLoan)
            {
                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearTurnOver == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousYearTurnOver"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearTurnOver == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousSecondYearTurnOver"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearTurnOver == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousThirdYearTurnOver"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearGrossProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousYearGrossProfitMargin"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearGrossProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousSecondYearGrossProfitMargin"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearGrossProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousThirdYearGrossProfitMargin"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearNetProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousYearNetProfitMargin"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearNetProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousSecondYearNetProfitMargin"]?.Errors?.Clear();
                }

                if (customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearNetProfitMargin == StringLiteralValue.NotRequired)
                {
                    ModelState["CustomerCashCreditLoanAccountViewModel.PreviousThirdYearNetProfitMargin"]?.Errors?.Clear();
                }

            }
            else
            {
                errorViewModelName = errorViewModelName + ",CustomerCashCreditLoanAccountViewModel";
            }

            //Clear Model State of Business Loan
            if (loanType != StringLiteralValue.ShortTermBusinessLoan)
            {
                errorViewModelName = errorViewModelName + ",CustomerBusinessLoanCollateralDetailViewModel";
            }
            else
            {
                byte capturePreviousProfitMakingYears = customerLoanAccountOpeningConfigViewModel.SchemeBusinessLoanViewModel.CapturePreviousProfitMakingYears;
                
                for (int i = capturePreviousProfitMakingYears + 1; i <= 5; i++)
                {
                    string propertyName = $"CustomerBusinessLoanCollateralDetailViewModel.PreviousYearProfit{i}";
                    ModelState[propertyName]?.Errors?.Clear();
                }
            }


            //Clear Model State of Loan Against Fixed Deposit
            if (loanType == StringLiteralValue.LoanAgainstDeposit)
            {
                ModelState["CustomerLoanAccountViewModel.MinuteOfMeetingAgendaId"]?.Errors?.Clear();
            }

            //Clear Model State of Educational Loan
            if (loanType == StringLiteralValue.EducationalLoan)
            {
                string nameOfInstitute = accountDetailRepository.GetNameOfInstituteByInstituteId(_loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.InstituteId);
                
                if (nameOfInstitute != "Other")
                {
                    ModelState["CustomerEducationalLoanDetailViewModel.OtherNameOfInstitute"]?.Errors?.Clear();
                    ModelState["CustomerEducationalLoanDetailViewModel.TransOtherNameOfInstitute"]?.Errors?.Clear();
                    ModelState["CustomerEducationalLoanDetailViewModel.OtherInstituteContactDetails"]?.Errors?.Clear();
                    ModelState["CustomerEducationalLoanDetailViewModel.TransOtherInstituteContactDetails"]?.Errors?.Clear();
                    ModelState["CustomerEducationalLoanDetailViewModel.OtherInstituteAddressDetails"]?.Errors?.Clear();
                    ModelState["CustomerEducationalLoanDetailViewModel.TransOtherInstituteAddressDetails"]?.Errors?.Clear();
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.OtherNameOfInstitute = "None";
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.TransOtherNameOfInstitute = "None";
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.OtherInstituteContactDetails = "None";
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.TransOtherInstituteContactDetails = "None";
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.OtherInstituteAddressDetails = "None";
                    _loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.TransOtherInstituteAddressDetails = "None";
                }
            }
            else
            {
                errorViewModelName = errorViewModelName + ",CustomerEducationalLoanDetailViewModel";
            }


            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["CustomerAccountDetailViewModel.CustomerAccountDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountDocumentViewModel.CustomerAccountDocumentPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.CustomerAccountEmailServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNomineeViewModel.CustomerAccountNomineePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNomineeGuardianViewModel.CustomerAccountNomineeGuardianPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNoticeScheduleViewModel.CustomerAccountNoticeSchedulePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountSmsServiceViewModel.CustomerAccountSmsServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountStandingInstructionViewModel.CustomerAccountStandingInstructionPrmKey"]?.Errors?.Clear();
                ModelState["CustomerCashCreditLoanAccountViewModel.CustomerCashCreditLoanAccountPrmKey"]?.Errors?.Clear();
                ModelState["CustomerConsumerLoanCollateralDetailViewModel.CustomerConsumerLoanCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerBusinessLoanCollateralDetailViewModel.CustomerBusinessLoanCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerEducationalLoanDetailViewModel.CustomerEducationalLoanDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerEducationalLoanDetailViewModel.CustomerEducationalLoanDetailTranslationPrmKey"]?.Errors?.Clear();
                ModelState["CustomerGoldLoanCollateralDetailViewModel.CustomerGoldLoanCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerGoldLoanCollateralPhotoViewModel.CustomerGoldLoanCollateralPhotoPrmKey"]?.Errors?.Clear();
                ModelState["CustomerJointAccountHolderViewModel.CustomerJointAccountHolderPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountGuarantorDetailViewModel.CustomerLoanAccountGuarantorDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAcquaintanceDetailViewModel.CustomerLoanAcquaintanceDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAgainstDepositCollateralDetailViewModel.CustomerLoanAgainstDepositCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAgainstPropertyCollateralDetailViewModel.CustomerLoanAgainstPropertyCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanFieldInvestigationViewModel.CustomerLoanFieldInvestigationPrmKey"]?.Errors?.Clear();
                ModelState["CustomerPreOwnedVehicleLoanInspectionViewModel.CustomerPreOwnedVehicleLoanInspectionPrmKey"]?.Errors?.Clear();
                ModelState["CustomerVehicleLoanInsuranceDetailViewModel.CustomerVehicleLoanInsuranceDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerVehicleLoanPhotoViewModel.CustomerVehicleLoanPhotoPrmKey"]?.Errors?.Clear();
                ModelState["CustomerLoanAccountDebtToIncomeRatioViewModel.CustomerLoanAccountDebtToIncomeRatioPrmKey"]?.Errors?.Clear();
                ModelState["CustomerVehicleLoanPermitDetailViewModel.CustomerVehicleLoanPermitDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerVehicleLoanContractDetailViewModel.CustomerVehicleLoanContractDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerVehicleLoanCollateralDetailViewModel.CustomerVehicleLoanCollateralDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAddressViewModel.PersonAddressPrmKey"]?.Errors?.Clear();
                ModelState["PersonBorrowingDetailViewModel.PersonBorrowingDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonContactDetailViewModel.PersonContactDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonCourtCaseViewModel.PersonCourtCasePrmKey"]?.Errors?.Clear();
                ModelState["PersonEmploymentDetailViewModel.PersonEmploymentDetailTranslationPrmKey"]?.Errors?.Clear();
                ModelState["PersonEmploymentDetailViewModel.PersonEmploymentDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonEmploymentDetailViewModel.CustomerAccountEmploymentDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey"]?.Errors?.Clear();
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
            // For Photo Upload Of Assets Like Income Tax Detail
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(LoanCustomerAccountViewModel _loanCustomerAccountViewModel)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            await ClearModelStateOfDataTableFields(_loanCustomerAccountViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await loanCustomerAccountRepository.Save(_loanCustomerAccountViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CustomerLoanAccount");
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

            return View(_loanCustomerAccountViewModel);
        }

        [HttpPost]
        [Route("LoanCustomerAccountSaveDataTables")]
        public ActionResult LoanCustomerAccountSaveDataTables(List<CustomerJointAccountHolderViewModel> _customerJointAccountHolder, List<CustomerAccountNomineeViewModel> _customerAccountNominee, List<CustomerLoanAccountGuarantorDetailViewModel> _customerAccountGuarantorDetail, List<PersonContactDetailViewModel> _personContactDetail, List<PersonAddressViewModel> _personAddress, List<CustomerGoldLoanCollateralDetailViewModel> _goldLoanCollateralDetail, List<CustomerAccountNoticeScheduleViewModel> _customerAccountNoticeSchedule, List<PersonBorrowingDetailViewModel> _borrowingDetail, List<CustomerLoanAcquaintanceDetailViewModel> _acquaintanceDetail, List<CustomerConsumerLoanCollateralDetailViewModel> _consumerLoanCollateralDetail, List<CustomerLoanAgainstDepositCollateralDetailViewModel> _loanAgainstDepositCollateralDetail, List<PersonCourtCaseViewModel> _personCourtCase, List<PersonAdditionalIncomeDetailViewModel> _personAdditionalIncomeDetail)
        {
            HttpContext.Session.Add("JointAccountHolder", _customerJointAccountHolder);
            HttpContext.Session.Add("Nominee", _customerAccountNominee);
            HttpContext.Session.Add("GuarantorDetail", _customerAccountGuarantorDetail);
            HttpContext.Session.Add("ContactDetail", _personContactDetail);
            HttpContext.Session.Add("BorrowingDetail", _borrowingDetail);
            HttpContext.Session.Add("NoticeSchedule", _customerAccountNoticeSchedule);
            HttpContext.Session.Add("AddressDetail", _personAddress);
            HttpContext.Session.Add("GoldLoanCollateralDetail", _goldLoanCollateralDetail);
            HttpContext.Session.Add("AcquaintanceDetail", _acquaintanceDetail);
            HttpContext.Session.Add("ConsumerLoanCollateralDetail", _consumerLoanCollateralDetail);
            HttpContext.Session.Add("LoanAgainstDepositCollateralDetail", _loanAgainstDepositCollateralDetail);
            HttpContext.Session.Add("PersonCourtCase", _personCourtCase);
            HttpContext.Session.Add("PersonAdditionalIncomeDetail", _personAdditionalIncomeDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Route("CustomerLoanImageDataTables")]
        public ActionResult CustomerLoanImageDataTables(List<CustomerVehicleLoanPhotoViewModel> _vehicleLoanPhoto, List<CustomerGoldLoanCollateralPhotoViewModel> _goldLoanCollateralPhoto, List<CustomerAccountDocumentViewModel> _document, List<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetail)
        {
            HttpContext.Session.Add("Document", _document);
            HttpContext.Session.Add("GoldLoanCollateralPhoto", _goldLoanCollateralPhoto);
            HttpContext.Session.Add("PersonIncomeTaxDetail", _personIncomeTaxDetail);
            HttpContext.Session.Add("VehicleLoanPhoto", _vehicleLoanPhoto);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<LoanCustomerAccountIndexViewModel> loanCustomerAccountViewModel = await loanCustomerAccountRepository.GetLoanCustomerAccountIndex(StringLiteralValue.Reject);

            if (loanCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanCustomerAccountViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<LoanCustomerAccountIndexViewModel> loanCustomerAccountViewModel = await loanCustomerAccountRepository.GetLoanCustomerAccountIndex(StringLiteralValue.Unverified);

            if (loanCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanCustomerAccountViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<LoanCustomerAccountIndexViewModel> loanCustomerAccountViewModel = await loanCustomerAccountRepository.GetLoanCustomerAccountIndex(StringLiteralValue.Verify);

            if (loanCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanCustomerAccountViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CustomerAccountId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            LoanCustomerAccountViewModel loanCustomerAccountViewModel = await loanCustomerAccountRepository.GetLoanCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Unverified);

            bool data = await loanCustomerAccountRepository.GetSessionValues(loanCustomerAccountViewModel, StringLiteralValue.Unverified);

            if (loanCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_loanCustomerAccountViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_loanCustomerAccountViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _loanCustomerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await loanCustomerAccountRepository.VerifyRejectDelete(_loanCustomerAccountViewModel, StringLiteralValue.Verify);
                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerLoanAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _loanCustomerAccountViewModel.UserAction = Services.Constants.StringLiteralValue.Reject;

                    bool result = await loanCustomerAccountRepository.VerifyRejectDelete(_loanCustomerAccountViewModel, StringLiteralValue.Reject);


                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerLoanAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CustomerLoanAccount");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_loanCustomerAccountViewModel.CustomerAccountId);
        }
    }
}