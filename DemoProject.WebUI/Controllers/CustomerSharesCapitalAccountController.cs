using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Customer/Account/Opening/SharesCapital")]
    public class CustomerSharesCapitalAccountController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ISharesCapitalCustomerAccountRepository sharesCapitalCustomerAccountRepository;

        public CustomerSharesCapitalAccountController(IAccountDetailRepository _accountDetailRepository, ISchemeDetailRepository _schemeDetailRepository, ISharesCapitalCustomerAccountRepository _sharesCapitalCustomerAccountRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
            sharesCapitalCustomerAccountRepository = _sharesCapitalCustomerAccountRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CustomerAccountId)
        {
            // CustomerAccount
            SharesCapitalCustomerAccountViewModel sharesCapitalCustomerAccountViewModel = await sharesCapitalCustomerAccountRepository.GetSharesCapitalCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Reject);

            bool data = await sharesCapitalCustomerAccountRepository.GetSessionValues(sharesCapitalCustomerAccountViewModel, StringLiteralValue.Reject);

            if (sharesCapitalCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await sharesCapitalCustomerAccountRepository.Amend(_sharesCapitalCustomerAccountViewModel);

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
                    bool result = await sharesCapitalCustomerAccountRepository.VerifyRejectDelete(_sharesCapitalCustomerAccountViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CustomerSharesCapitalAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "CustomerSharesCapitalAccount");
            }
            else
            {
                ModelState.AddModelError("InputValidationError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_sharesCapitalCustomerAccountViewModel);
        }

        private async Task ClearModelStateOfDataTableFields(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType)
        {
            SharesCapitalCustomerAccountDetailViewModel sharesCapitalCustomerAccountDetailViewModel = new SharesCapitalCustomerAccountDetailViewModel();

            // Assign All DataTable ViewModels
            string errorViewModelName = "CustomerJointAccountHolderViewModel, CustomerAccountNomineeViewModel, CustomerAccountNomineeGuardianViewModel, PersonAddressViewModel, PersonContactDetailViewModel, CustomerAccountTurnOverLimitViewModel, CustomerAccountNoticeScheduleViewModel";

            // ******** Change ViewModel
            errorViewModelName = errorViewModelName + ", SchemeNoticeScheduleViewModel";

            // If MultiCurrency Hide
            if(sharesCapitalCustomerAccountDetailViewModel.HasMultiCurrency == false)
            {
                ModelState["CustomerAccountDetailViewModel.CurrencyId"]?.Errors?.Clear();
                ModelState["CustomerAccountDetailViewModel.CurrencyPrmKey"]?.Errors?.Clear();
            }

            // If No Branch
            if (sharesCapitalCustomerAccountDetailViewModel.HasBranch == false)
            {
                ModelState["CustomerAccountDetailViewModel.BusinessOfficeId"]?.Errors?.Clear();
                ModelState["CustomerAccountDetailViewModel.BusinessOfficePrmKey"]?.Errors?.Clear();
            }

            // Get Record Of All Master Table And Transaction Table
            CustomerSharesAccountOpeningConfigViewModel customerSharesAccountOpeningConfigViewModel = await schemeDetailRepository.GetSharesCapitalSchemeConfigDetail(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

            //Clear Model State of ApplicationNumber
            if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableApplication == true ? customerSharesAccountOpeningConfigViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber : true)
            {
                ModelState["ApplicationNumber"]?.Errors?.Clear();
            }

            //Clear Model State of Sms
            if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == false)
            {
                ModelState["CustomerAccountSmsServiceViewModel.ActivationDate"]?.Errors?.Clear();
                ModelState["CustomerAccountSmsServiceViewModel.ExpiryDate"]?.Errors?.Clear();
            }

            //Clear Model State of Email
            if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == false)
            {
                ModelState["CustomerAccountEmailServiceViewModel.ActivationDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.ExpiryDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.CloseDate"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.StatementFrequency"]?.Errors?.Clear();
            }
            // Cleara Model State Required Field Error Of Hideen Inputs
            if (customerSharesAccountOpeningConfigViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == true)
            {
                ModelState["AccountNumber"]?.Errors?.Clear();
            }

            if (customerSharesAccountOpeningConfigViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == true)
                errorViewModelName = errorViewModelName + ",CustomerSharesCapitalAccountViewModel";

            if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
                ModelState["AlternateAccountNumber2"]?.Errors?.Clear();

            if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
                ModelState["AlternateAccountNumber3"]?.Errors?.Clear();
            
            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {

                ModelState["CustomerJointAccountHolderViewModel.CustomerJointAccountHolderPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNomineeViewModel.CustomerAccountNomineePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountTurnOverLimitViewModel.CustomerAccountTurnOverLimitPrmKey"]?.Errors?.Clear();
                ModelState["PersonContactDetailViewModel.PersonContactDetailPrmKey"]?.Errors?.Clear();
                ModelState["PersonAddressViewModel.PersonAddressPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.CustomerAccountEmailServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountSmsServiceViewModel.CustomerAccountSmsServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNoticeScheduleViewModel.CustomerAccountNoticeSchedulePrmKey"]?.Errors?.Clear();

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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel)
        {
            await ClearModelStateOfDataTableFields(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await sharesCapitalCustomerAccountRepository.Save(_sharesCapitalCustomerAccountViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CustomerSharesCapitalAccount");
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
                ModelState.AddModelError("InputValidationError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_sharesCapitalCustomerAccountViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<PersonAddressViewModel> _personAddress, List<PersonContactDetailViewModel> _personContactDetail, List<CustomerAccountNomineeViewModel> _customerAccountNominee, List<CustomerAccountNoticeScheduleViewModel> _customerAccountNoticeSchedule, List<CustomerAccountTurnOverLimitViewModel> _customerAccountTurnOverLimit, List<CustomerJointAccountHolderViewModel> _customerJointAccountHolder)
        {
            HttpContext.Session.Add("CustomerAccountAddressDetail", _personAddress);

            HttpContext.Session.Add("CustomerAccountContactDetail", _personContactDetail);

            HttpContext.Session.Add("CustomerAccountNominee", _customerAccountNominee);

            HttpContext.Session.Add("CustomerAccountNoticeSchedule", _customerAccountNoticeSchedule);

            HttpContext.Session.Add("CustomerAccountTurnOverLimit", _customerAccountTurnOverLimit);

            HttpContext.Session.Add("CustomerJointAccountHolder", _customerJointAccountHolder);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("RejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<SharesCapitalCustomerAccountIndexViewModel> sharesCapitalCustomerAccountIndexViewModels = await sharesCapitalCustomerAccountRepository.GetSharesCapitalCustomerAccountIndex(StringLiteralValue.Reject);

            if (sharesCapitalCustomerAccountIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalCustomerAccountIndexViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<SharesCapitalCustomerAccountIndexViewModel> sharesCapitalCustomerAccountIndexViewModels = await sharesCapitalCustomerAccountRepository.GetSharesCapitalCustomerAccountIndex(StringLiteralValue.Unverified);

            if (sharesCapitalCustomerAccountIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalCustomerAccountIndexViewModels);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<SharesCapitalCustomerAccountIndexViewModel> sharesCapitalCustomerAccountIndexViewModels = await sharesCapitalCustomerAccountRepository.GetSharesCapitalCustomerAccountIndex(StringLiteralValue.Verify);

            if (sharesCapitalCustomerAccountIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalCustomerAccountIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CustomerAccountId)
        {
            // CustomerAccount
            SharesCapitalCustomerAccountViewModel sharesCapitalCustomerAccountViewModel = await sharesCapitalCustomerAccountRepository.GetSharesCapitalCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Unverified);

            bool data = await sharesCapitalCustomerAccountRepository.GetSessionValues(sharesCapitalCustomerAccountViewModel, StringLiteralValue.Unverified);

            if (sharesCapitalCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _sharesCapitalCustomerAccountViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await sharesCapitalCustomerAccountRepository.VerifyRejectDelete(_sharesCapitalCustomerAccountViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerSharesCapitalAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _sharesCapitalCustomerAccountViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await sharesCapitalCustomerAccountRepository.VerifyRejectDelete(_sharesCapitalCustomerAccountViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CustomerSharesCapitalAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CustomerSharesCapitalAccount");
            }
            else
            {
                ModelState.AddModelError("InputValidationError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_sharesCapitalCustomerAccountViewModel.CustomerAccountId);
        }
    }
}