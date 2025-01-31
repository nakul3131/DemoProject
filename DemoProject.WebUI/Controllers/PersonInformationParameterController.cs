using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Constants;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Parameter/Person/PersonInformationParameter")]
    public class PersonInformationParameterController : Controller
    {
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;

        public PersonInformationParameterController(IPersonInformationParameterRepository _personInformationParameterRepository)
        {
            personInformationParameterRepository = _personInformationParameterRepository;
        }

        //method replaced by Rahul date 27.10.2024
        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            bool result = true;

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Reject);
            if (personInformationParameterViewModel != null && result == true)
            {
                // Check Whether Session Data Required Or Not?
                if (personInformationParameterViewModel.EnableSMSAlert || personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
                {
                    result = await personInformationParameterRepository.GetSessionValues(StringLiteralValue.Reject);
                }
            }
            else
            {
                throw new DatabaseException();
            }

            return View(personInformationParameterViewModel);
        }
        //-------------------------------------//

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(PersonInformationParameterViewModel _personInformationParameterViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personInformationParameterRepository.Amend(_personInformationParameterViewModel);

                    if (result)
                    {
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
                    bool result = await personInformationParameterRepository.Delete(_personInformationParameterViewModel);

                    if (result)
                    {
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

            return View(_personInformationParameterViewModel);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonInformationParameterViewModel _personInformationParameterViewModel, string _entryType)
        {

            // Mobile OTP 
            if (_personInformationParameterViewModel.EnableMobileOTPForVerification)
            {
                ModelState["VerificationMobileOTPDataType"]?.Errors?.Clear();
                ModelState["VerificationMobileOTPLength"]?.Errors?.Clear();
                ModelState["PrefixStringForVerificationMobileOTP"]?.Errors?.Clear();
                ModelState["PostfixStringForVerificationMobileOTP"]?.Errors?.Clear();
                ModelState["IncludedCharactersForVerificationMobileOTP"]?.Errors?.Clear();
                ModelState["ExcludedCharactersForVerificationMobileOTP"]?.Errors?.Clear();
                ModelState["VerificationMobileOTPExpiryTime"]?.Errors?.Clear();
                ModelState["MaximumResendForVerificationMobileOTP"]?.Errors?.Clear();
            }

            // Photo Doucment Upload 
            if (_personInformationParameterViewModel.PhotoDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnablePhotoDocumentUploadInDb == false)
                {
                    ModelState["PhotoDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["PhotoDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.PhotoDocumentLocalStoragePath = "None";
                    ModelState["PhotoDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForPhotoDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["PhotoDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["PhotoDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.PhotoDocumentLocalStoragePath = "None";
                ModelState["PhotoDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForPhotoDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // Sign Doucment Upload 
            if (_personInformationParameterViewModel.SignDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableSignDocumentUploadInDb == false)
                {
                    ModelState["SignDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForSignDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.SignDocumentLocalStoragePath = "None";
                    ModelState["SignDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForSignDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["SignDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForSignDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.SignDocumentLocalStoragePath = "None";
                ModelState["SignDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForSignDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // KYC Doucment Upload 
            if (_personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableKYCDocumentUploadInDb == false)
                {
                    ModelState["KYCDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForKYCDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.KYCDocumentLocalStoragePath = "None";
                    ModelState["KYCDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForKYCDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["KYCDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForKYCDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.KYCDocumentLocalStoragePath = "None";
                ModelState["KYCDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForKYCDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // Bank Doucment Upload 
            if (_personInformationParameterViewModel.BankStatementDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableBankStatementDocumentUploadInDb == false)
                {
                    ModelState["BankStatementDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForBankStatementDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.BankStatementDocumentLocalStoragePath = "None";
                    ModelState["BankStatementDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForBankStatementDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["BankStatementDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForBankStatementDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.BankStatementDocumentLocalStoragePath = "None";
                ModelState["BankStatementDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForBankStatementDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // GST Doucment Upload 
            if (_personInformationParameterViewModel.GSTDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableGSTDocumentUploadInDb == false)
                {
                    ModelState["GSTDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForGSTDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.GSTDocumentLocalStoragePath = "None";
                    ModelState["GSTDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForGSTDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["GSTDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForGSTDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.GSTDocumentLocalStoragePath = "None";
                ModelState["GSTDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForGSTDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // IncomeTax Doucment Upload 
            if (_personInformationParameterViewModel.IncomeTaxDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInDb == false)
                {
                    ModelState["IncomeTaxDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForIncomeTaxDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath = "None";
                    ModelState["IncomeTaxDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForIncomeTaxDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["IncomeTaxDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForIncomeTaxDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath = "None";
                ModelState["IncomeTaxDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForIncomeTaxDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }


            // FinancialAsset Doucment Upload 
            if (_personInformationParameterViewModel.FinancialAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableFinancialAssetDocumentUploadInDb == false)
                {
                    ModelState["FinancialAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForFinancialAssetDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath = "None";
                    ModelState["FinancialAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForFinancialAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["FinancialAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForFinancialAssetDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.FinancialAssetDocumentLocalStoragePath = "None";
                ModelState["FinancialAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForFinancialAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // AgricultureAsset Doucment Upload 
            if (_personInformationParameterViewModel.AgricultureAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableAgricultureAssetDocumentUploadInDb == false)
                {
                    ModelState["AgricultureAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForAgricultureAssetDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath = "None";
                    ModelState["AgricultureAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForAgricultureAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["AgricultureAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForAgricultureAssetDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.AgricultureAssetDocumentLocalStoragePath = "None";
                ModelState["AgricultureAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForAgricultureAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // ImmovableAsset Doucment Upload 
            if (_personInformationParameterViewModel.ImmovableAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableImmovableAssetDocumentUploadInDb == false)
                {
                    ModelState["ImmovableAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForImmovableAssetDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.ImmovableAssetDocumentLocalStoragePath = "None";
                    ModelState["ImmovableAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForImmovableAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }

            // MovableAsset Doucment Upload 
            if (_personInformationParameterViewModel.MovableAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableMovableAssetDocumentUploadInDb == false)
                {
                    ModelState["MovableAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForMovableAssetDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath = "None";
                    ModelState["MovableAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForMovableAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["MovableAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForMovableAssetDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.MovableAssetDocumentLocalStoragePath = "None";
                ModelState["MovableAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForMovableAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // MachineryAsset Doucment Upload 
            if (_personInformationParameterViewModel.MachineryAssetDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableMachineryAssetDocumentUploadInDb == false)
                {
                    ModelState["MachineryAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForMachineryAssetDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath = "None";
                    ModelState["MachineryAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForMachineryAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["MachineryAssetDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForMachineryAssetDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.MachineryAssetDocumentLocalStoragePath = "None";
                ModelState["MachineryAssetDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForMachineryAssetDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }

            // Death Doucment Upload 
            if (_personInformationParameterViewModel.DeathDocumentUpload != StringLiteralValue.Disable)
            {
                if (_personInformationParameterViewModel.EnableDeathDocumentUploadInDb == false)
                {
                    ModelState["DeathDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForDeathDocumentUploadInDb"]?.Errors?.Clear();
                }
                else
                {
                    _personInformationParameterViewModel.DeathDocumentLocalStoragePath = "None";
                    ModelState["DeathDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                    ModelState["MaximumFileSizeForDeathDocumentUploadInLocalStorage"]?.Errors?.Clear();
                }
            }
            else
            {
                ModelState["DeathDocumentAllowedFileFormatTypeIdForDb"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForDeathDocumentUploadInDb"]?.Errors?.Clear();

                _personInformationParameterViewModel.DeathDocumentLocalStoragePath = "None";
                ModelState["DeathDocumentAllowedFileFormatTypeIdForLocalStorage"]?.Errors?.Clear();
                ModelState["MaximumFileSizeForDeathDocumentUploadInLocalStorage"]?.Errors?.Clear();
            }
            //PersonInformationParameterDocumentTypeViewModel And PersonInformationParameterNoticeTypeViewModel
            string errorViewModelName = "PersonInformationParameterViewModel,PersonInformationParameterDocumentTypeViewModel,PersonInformationParameterNoticeTypeViewModel";
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonInformationParameterViewModel.PersonInformationParameterPrmKey"]?.Errors?.Clear();
                ModelState["PersonInformationParameterDocumentTypeViewModel.PersonInformationParameterDocumentTypePrmKey"]?.Errors?.Clear();
                ModelState["PersonInformationParameterNoticeTypeViewModel.PersonInformationParameterNoticeTypePrmKey"]?.Errors?.Clear();

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

            /*// PersonInformationParameterDocumentTypeViewModel
            ModelState["PersonInformationParameterDocumentTypeViewModel.IsMandatory"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.ActivationDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.ExpiryDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.CloseDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.Note"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.ReasonForModification"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.PersonInformationParameterDocumentTypePrmKey"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.DocumentTypeId"]?.Errors?.Clear();
            ModelState["PersonInformationParameterDocumentTypeViewModel.NameOfDocumentType"]?.Errors?.Clear();

            // PersonInformationParameterNoticeTypeViewModel
            ModelState["PersonInformationParameterNoticeTypeViewModel.EnableNoticeInRegionalLanguage"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.MaximumResendsOnFailure"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.ActivationDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.ExpiryDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.CloseDate"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.Note"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.ReasonForModification"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.PersonInformationParameterNoticeTypePrmKey"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.NoticeTypeId"]?.Errors?.Clear();
            ModelState["PersonInformationParameterNoticeTypeViewModel.NameOfNoticeType"]?.Errors?.Clear();*/
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<PersonInformationParameterViewModel> personInformationParameterViewModels = await personInformationParameterRepository.GetPersonInformationParameterIndex();

            return View(personInformationParameterViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify()
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            bool data = await personInformationParameterRepository.GetSessionValues(StringLiteralValue.Verify);

            if (await personInformationParameterRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            return View(personInformationParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await personInformationParameterRepository.Save(_personInformationParameterViewModel);

                if (result)
                {
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

            return View(_personInformationParameterViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<PersonInformationParameterDocumentTypeViewModel> _personInformationParameterDocumentType, List<PersonInformationParameterNoticeTypeViewModel> _personInformationParameterNoticeType)
        {
            HttpContext.Session.Add("PersonInformationParameterDocumentType", _personInformationParameterDocumentType);

            HttpContext.Session.Add("PersonInformationParameterNoticeType", _personInformationParameterNoticeType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Unverified);
            bool data = await personInformationParameterRepository.GetSessionValues(StringLiteralValue.Unverified);

            if (personInformationParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(personInformationParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonInformationParameterViewModel _personInformationParameterViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Reject);

            //ClearModelStateOfDataTableFields(_personInformationParameterViewModel, StringLiteralValue.Verify);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personInformationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personInformationParameterRepository.Verify(_personInformationParameterViewModel);

                    if (result)
                    {
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
                    _personInformationParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personInformationParameterRepository.Reject(_personInformationParameterViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
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

            return View(_personInformationParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            bool data = await personInformationParameterRepository.GetSessionValues(StringLiteralValue.Verify);

            if (personInformationParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personInformationParameterViewModel);
        }
    }
}