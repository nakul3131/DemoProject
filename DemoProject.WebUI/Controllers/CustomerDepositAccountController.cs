using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.WebUI.Utility;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Customer/Account/Opening/Deposit")]
    public class CustomerDepositAccountController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ICustomerDepositAccountRepository customerDepositAccountRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ICustomerAccountDbContextRepository customerAccountDbContextRepository;

        public CustomerDepositAccountController(IAccountDetailRepository _accountDetailRepository, ICustomerDepositAccountRepository _customerDepositAccountRepository, ISchemeDetailRepository _schemeDetailRepository, ICustomerAccountDbContextRepository _customerAccountDbContextRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            customerDepositAccountRepository = _customerDepositAccountRepository;
            schemeDetailRepository = _schemeDetailRepository;
            customerAccountDbContextRepository = _customerAccountDbContextRepository;

        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CustomerAccountId)
        {
            DepositCustomerAccountViewModel depositCustomerAccountViewModel = await customerDepositAccountRepository.GetDepositCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Reject);

            CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

            bool data = await customerDepositAccountRepository.GetSessionValues(depositCustomerAccountViewModel, StringLiteralValue.Reject);

            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel != null)
            {
                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage)
                    depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoCopy = GetCustomerPhotoSignFromPath("Photo", depositCustomerAccountViewModel);

                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage)
                    depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPhotoCopy = GetCustomerPhotoSignFromPath("Sign", depositCustomerAccountViewModel);
            }

            if (depositCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }                

            return View(depositCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                await ClearModelStateOfDataTableFields(_depositCustomerAccountViewModel, StringLiteralValue.Amend);
            else
                await ClearModelStateOfDataTableFields(_depositCustomerAccountViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await customerDepositAccountRepository.Amend(_depositCustomerAccountViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CustomerDepositAccount");
                    }
                    else
                        throw new DatabaseException();
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await customerDepositAccountRepository.VerifyRejectDelete(_depositCustomerAccountViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CustomerDepositAccount"), }, JsonRequestBehavior.AllowGet);
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

            return View(_depositCustomerAccountViewModel);
        }

        [NonAction]
        private async Task ClearModelStateOfDataTableFields(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string _entryType)
        {
            DepositCustomerAccountDetailViewModel depositCustomerAccountDetailViewModel = new DepositCustomerAccountDetailViewModel();

            CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

            // Assign All DataTable ViewModels
            string errorViewModelName = " PersonAddressViewModel, PersonContactDetailViewModel,CustomerJointAccountHolderViewModel,CustomerAccountNomineeViewModel,CustomerAccountNomineeGuardianViewModel,CustomerAccountTurnOverLimitViewModel,CustomerAccountNoticeScheduleViewModel,CustomerDepositAccountAgentViewModel,CustomerAccountDocumentViewModel,CustomerAccountBeneficiaryDetailViewModel,CustomerAccountReferencePersonDetailViewModel";

            // Clear Model State Required Field Error Of Hideen Inputs
            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableApplication == true ? customerDepositAccountOpeningConfigViewModel.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber : true)
            {
                ModelState["ApplicationNumber"]?.Errors?.Clear();
            }

            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == true ? customerDepositAccountOpeningConfigViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber : true)
            {
                ModelState["PassbookNumber"]?.Errors?.Clear();
            }

            if (customerDepositAccountOpeningConfigViewModel.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == true)
            {
                ModelState["AccountNumber"]?.Errors?.Clear();
            }

            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
            {
                ModelState["AlternateAccountNumber2"]?.Errors?.Clear();
            }

            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
            {
                ModelState["AlternateAccountNumber3"]?.Errors?.Clear();
            }

            // Currency
            if (depositCustomerAccountDetailViewModel.HasMultiCurrency == false)
            {
                ModelState["CustomerAccountDetailViewModel.CurrencyId"]?.Errors?.Clear();
                ModelState["CustomerAccountDetailViewModel.CurrencyPrmKey"]?.Errors?.Clear();
            }

            // If No Branch
            if (depositCustomerAccountDetailViewModel.HasBranch == false)
            {
                ModelState["CustomerAccountDetailViewModel.BusinessOfficeId"]?.Errors?.Clear();
                ModelState["CustomerAccountDetailViewModel.BusinessOfficePrmKey"]?.Errors?.Clear();
            }

            // Cheque Detail
            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableChequeBook == false)
                errorViewModelName = errorViewModelName + ",CustomerAccountChequeDetailViewModel";
            else
                ModelState["CustomerAccountChequeDetailViewModel.Status"]?.Errors?.Clear();

            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == false)
                errorViewModelName = errorViewModelName + ",CustomerAccountEmailServiceViewModel";

            if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == false)
                errorViewModelName = errorViewModelName + ",CustomerAccountSmsServiceViewModel";

            if (_depositCustomerAccountViewModel.EnableTurnOverLimit == false)
                errorViewModelName = errorViewModelName + ",CustomerAccountTurnOverLimitViewModel";

            // If Demand Deposit Type
            if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.DemandDeposit)
            {
                errorViewModelName = errorViewModelName + ", CustomerTermDepositAccountDetailViewModel, CustomerAccountStandingInstructionViewModel";

                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSweepOut == false)
                    errorViewModelName = errorViewModelName + ", CustomerAccountSweepDetailViewModel";                
            }
            else
            {
                errorViewModelName = errorViewModelName + ",CustomerAccountPhotoSignViewModel, CustomerAccountSweepDetailViewModel";

                ModelState["CustomerDepositAccountViewModel.AccountOperationModeId"]?.Errors?.Clear();

                ModelState["Year"]?.Errors?.Clear();
                ModelState["Month"]?.Errors?.Clear();
                ModelState["Day"]?.Errors?.Clear();
            }

            // If Fixed / Term Deposit
            if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.FixedDeposit)
            {
                // Clear Number Of Deposit
                ModelState["CustomerTermDepositAccountDetailViewModel.NoOfDeposits"]?.Errors?.Clear();

                // Certificate Number
                if (customerDepositAccountOpeningConfigViewModel.SchemeDepositCertificateParameterViewModel != null)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeDepositCertificateParameterViewModel.EnableAutoCertificateNumber == true)
                        ModelState["CustomerTermDepositAccountDetailViewModel.CertificateNumber"]?.Errors?.Clear();
                }

                // Interest Payout Frequency / Interest Payout Amount
                if (customerDepositAccountOpeningConfigViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.TotalInterestAmount < 1200)
                {
                    ModelState["CustomerTermDepositAccountDetailViewModel.InterestPayoutFrequency"]?.Errors?.Clear();
                    ModelState["CustomerTermDepositAccountDetailViewModel.InterestPayoutAmount"]?.Errors?.Clear();
                    ModelState["CustomerTermDepositAccountDetailViewModel.InterestPayoutDay"]?.Errors?.Clear();
                }

                // Custom Renew Amount
                if (accountDetailRepository.GetRenewTypeSysNameById(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.RenewTypeId) != "CustomAmount")
                    ModelState["CustomerTermDepositAccountDetailViewModel.CustomRenewAmount"]?.Errors?.Clear();

                // Grace Period For Renewal
                if(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.MaturityInstruction == StringLiteralValue.DoNotRenew)
                    ModelState["CustomerTermDepositAccountDetailViewModel.GracePeriodForRenewal"]?.Errors?.Clear();

                // Enable Auto Renew On Maturity
                if (_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity == false)
                {
                    ModelState["CustomerTermDepositAccountDetailViewModel.AutoRenewWaitingTimePeriod"]?.Errors?.Clear();
                    ModelState["CustomerTermDepositAccountDetailViewModel.RenewTypeId"]?.Errors?.Clear();
                    ModelState["CustomerTermDepositAccountDetailViewModel.CustomRenewAmount"]?.Errors?.Clear();
                    ModelState["CustomerTermDepositAccountDetailViewModel.RenewTenure"]?.Errors?.Clear();
                }
            }

            // If Recurring Deposit
            if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.RecurringDeposit)
                errorViewModelName = errorViewModelName + ", CustomerTermDepositAccountDetailViewModel";
            else
            {
                ModelState["CustomerDepositAccountViewModel.DepositInstallmentAmount"]?.Errors?.Clear();
                ModelState["CustomerDepositAccountViewModel.InstallmentFrequencyId"]?.Errors?.Clear();
            }

            // Standing Instruction
            if (_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == false && _depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == false && accountDetailRepository.GetRenewTypeSysNameById(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.RenewTypeId) != "Principal")
                errorViewModelName = errorViewModelName + ", CustomerAccountStandingInstructionViewModel";
            else
            {
                if(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == false)
                {
                    ModelState["CustomerAccountStandingInstructionViewModel.CreditCustomerAccountNumberId"]?.Errors?.Clear();
                    ModelState["CustomerAccountStandingInstructionViewModel.InterestCustomerAccountNumberId"]?.Errors?.Clear();
                }

                if(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == false)
                    ModelState["CustomerAccountStandingInstructionViewModel.DebitCustomerAccountNumberId"]?.Errors?.Clear();

                if (accountDetailRepository.GetRenewTypeSysNameById(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.RenewTypeId) != "Principal")
                    ModelState["CustomerAccountStandingInstructionViewModel.InterestCustomerAccountNumberId"]?.Errors?.Clear();
            }

            // On Create Following Inputs Are Disabled And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonAddressViewModel.PersonAddressPrmKey"]?.Errors?.Clear();
                ModelState["PersonContactDetailViewModel.PersonContactDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerJointAccountHolderViewModel.CustomerJointAccountHolderPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNomineeViewModel.CustomerAccountNomineePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountTurnOverLimitViewModel.CustomerAccountTurnOverLimitPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountSmsServiceViewModel.CustomerAccountSmsServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountEmailServiceViewModel.CustomerAccountEmailServicePrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNoticeScheduleViewModel.CustomerAccountNoticeSchedulePrmKey"]?.Errors?.Clear();
                ModelState["CustomerTermDepositAccountDetailViewModel.CustomerTermDepositAccountDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountStandingInstructionViewModel.CustomerAccountStandingInstructionPrmKey"]?.Errors?.Clear();
                ModelState["CustomerDepositAccountAgentViewModel.CustomerDepositAccountAgentPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountDocumentViewModel.CustomerAccountDocumentPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountSweepDetailViewModel.CustomerAccountSweepDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountPhotoSignViewModel.CustomerAccountPhotoSignPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountChequeDetailViewModel.CustomerAccountChequeDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountBeneficiaryDetailViewModel.CustomerAccountBeneficiaryDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountReferencePersonDetailViewModel.CustomerAccountReferencePersonDetailPrmKey"]?.Errors?.Clear();
                ModelState["CustomerAccountNomineeGuardianViewModel.CustomerAccountNomineeGuardianPrmKey"]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(DepositCustomerAccountViewModel _depositCustomerAccountViewModel)
        {
            await ClearModelStateOfDataTableFields(_depositCustomerAccountViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {

                bool result = await customerDepositAccountRepository.Save(_depositCustomerAccountViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CustomerDepositAccount");
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

            return View(_depositCustomerAccountViewModel);
        }

        [HttpGet]
        [Route("RejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<DepositCustomerAccountIndexViewModel> customerDepositAccountViewModel = await customerDepositAccountRepository.GetDepositCustomerAccountIndex(StringLiteralValue.Reject);

            if (customerDepositAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerDepositAccountViewModel);
        }

        [HttpPost]
        public ActionResult SaveDataTables(List<CustomerAccountBeneficiaryDetailViewModel> _customerAccountBeneficiaryDetail,  List<CustomerAccountNomineeViewModel> _customerAccountNominee, List<CustomerAccountNoticeScheduleViewModel> _customerAccountNoticeSchedule, List<CustomerAccountReferencePersonDetailViewModel> _referencePersonDetail,  List<CustomerAccountTurnOverLimitViewModel> _customerAccountTurnOverLimit,
                                           List<CustomerDepositAccountAgentViewModel> _customerDepositAccountAgent, List<CustomerJointAccountHolderViewModel> _customerJointAccountHolder, List<PersonAddressViewModel> _personAddress, List<PersonContactDetailViewModel> _personContactDetail)

        {
            HttpContext.Session.Add("CustomerAccountBeneficiaryDetail", _customerAccountBeneficiaryDetail);
            HttpContext.Session.Add("CustomerAccountNominee", _customerAccountNominee);
            HttpContext.Session.Add("CustomerAccountNoticeSchedule", _customerAccountNoticeSchedule);
            HttpContext.Session.Add("CustomerAccountReferencePersonDetail", _referencePersonDetail);
            HttpContext.Session.Add("CustomerAccountTurnOverLimit", _customerAccountTurnOverLimit);
            HttpContext.Session.Add("CustomerDepositAccountAgent", _customerDepositAccountAgent);
            HttpContext.Session.Add("CustomerJointAccountHolder", _customerJointAccountHolder);
            HttpContext.Session.Add("PersonAddress", _personAddress);
            HttpContext.Session.Add("PersonContactDetail", _personContactDetail);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DepositCustomerAccountImageSaveDataTables(List<CustomerAccountDocumentViewModel> _customerAccountDocument)
        {
            HttpContext.Session.Add("CustomerAccountDocument", _customerAccountDocument);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnVerifiedIndex()
        {
            IEnumerable<DepositCustomerAccountIndexViewModel> depositCustomerAccountIndexViewModel = await customerDepositAccountRepository.GetDepositCustomerAccountIndex(StringLiteralValue.Unverified);

            if (depositCustomerAccountIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(depositCustomerAccountIndexViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<DepositCustomerAccountIndexViewModel> customerDepositAccountViewModel = await customerDepositAccountRepository.GetDepositCustomerAccountIndex(StringLiteralValue.Verify);

            if (customerDepositAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(customerDepositAccountViewModel);
        }

        [HttpGet]
        [Route("Verify")]
        public async Task<ActionResult> Verify(Guid CustomerAccountId)
        {
            DepositCustomerAccountViewModel depositCustomerAccountViewModel = await customerDepositAccountRepository.GetDepositCustomerAccountEntry(CustomerAccountId, StringLiteralValue.Unverified);

            CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

            bool data = await customerDepositAccountRepository.GetSessionValues(depositCustomerAccountViewModel, StringLiteralValue.Unverified);

            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel != null)
            {
                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage)
                    depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoCopy = GetCustomerPhotoSignFromPath("Photo", depositCustomerAccountViewModel);

                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage)
                    depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPhotoCopy = GetCustomerPhotoSignFromPath("Sign", depositCustomerAccountViewModel);
            }

            if (depositCustomerAccountViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(depositCustomerAccountViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Verify")]
        public async Task<ActionResult> Verify(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                await ClearModelStateOfDataTableFields(_depositCustomerAccountViewModel, StringLiteralValue.Verify);
            else
                await ClearModelStateOfDataTableFields(_depositCustomerAccountViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _depositCustomerAccountViewModel.CustomerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await customerDepositAccountRepository.VerifyRejectDelete(_depositCustomerAccountViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnVerifiedIndex", "CustomerDepositAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _depositCustomerAccountViewModel.CustomerDepositAccountViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await customerDepositAccountRepository.VerifyRejectDelete(_depositCustomerAccountViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnVerifiedIndex", "CustomerDepositAccount"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnVerifiedIndex", "CustomerDepositAccount");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View();
        }


        [NonAction]
        private byte[] GetCustomerPhotoSignFromPath(string _photoSign, DepositCustomerAccountViewModel _depositCustomerAccountViewModel)
        {
            string filePath;
            string fileName;
            byte[] imagecode=null;
            byte[] fileBytes;


            if (_photoSign == "Photo")
            {
                filePath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath;

                //Get Full Destination Path
                string fullFilePath = customerAccountDbContextRepository.GetFullFilePath(filePath, _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoNameOfFile);

                //Check  Image is Existing Or Not
                bool fileExistance = customerAccountDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath;
                    // Find The Last Index Of '/' to Replace File Name
                    int lastSlashIndex = originalPath.LastIndexOf('/');

                    string path = originalPath.Substring(0, lastSlashIndex);

                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;

                    //Get Full Destination Path
                    fullFilePath = customerAccountDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
            }

            else
            {
                filePath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath;
                //Get Full Destination Path
                string fullFilePath = customerAccountDbContextRepository.GetFullFilePath(filePath, _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignNameOfFile);

                //Check  Image is Existing Or Not
                bool fileExistance = customerAccountDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath;
                    // Find The Last Index Of '/' to Replace File Name
                    int lastSlashIndex = originalPath.LastIndexOf('/');

                    string path = originalPath.Substring(0, lastSlashIndex);

                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;

                    //Get Full Destination Path
                    fullFilePath = customerAccountDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
            }
                    return imagecode;
        }

    }
}