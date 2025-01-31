using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Security/UserProfile")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public UserProfileController(IUserProfileRepository _userProfileRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            userProfileRepository = _userProfileRepository;
            personDetailRepository = _personDetailRepository;
            securityDetailRepository = _securityDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid UserProfileId)
        {

            bool data = await userProfileRepository.GetSessionValues(securityDetailRepository.GetUserProfilePrmKeyById(UserProfileId), StringLiteralValue.Reject);

            UserProfileViewModel userProfileViewModel = await userProfileRepository.GetUserProfileEntry(UserProfileId, StringLiteralValue.Reject);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(UserProfileViewModel _userProfileViewModel, string Command)
        {
            //For Clear ModelState Error
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_userProfileViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_userProfileViewModel, StringLiteralValue.Delete);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await userProfileRepository.Amend(_userProfileViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "UserProfile");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await userProfileRepository.VerifyRejectDelete(_userProfileViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "UserProfile"), }, JsonRequestBehavior.AllowGet);
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

            return View(_userProfileViewModel.UserProfileId);
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
        public async Task<ActionResult> Create(UserProfileViewModel _userProfileViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userProfileViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await userProfileRepository.Save(_userProfileViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "UserProfile");
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

            return View(_userProfileViewModel);
        }

        private void ClearModelStateOfDataTableFields(UserProfileViewModel _userProfileViewModel, string _entryType)
        {
            string errorViewModelName = "UserProfileBusinessOfficeViewModel, UserProfileCurrencyViewModel,UserProfileGeneralLedgerViewModel,UserProfileMenuViewModel,UserProfileSpecialPermissionViewModel,UserProfileTransactionLimitViewModel,UserRoleProfileViewModel";
            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["UserProfileViewModel.UserProfilePrmKey"]?.Errors?.Clear();
                ModelState["UserProfileBusinessOfficeViewModel.UserProfileBusinessOfficePrmKey"]?.Errors?.Clear();
                ModelState["UserProfileCurrencyViewModel.UserProfileCurrencyPrmKey"]?.Errors?.Clear();
                ModelState["UserProfileGeneralLedgerViewModel.UserProfileGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["UserProfileMenuViewModel.UserProfileMenuPrmKey"]?.Errors?.Clear();
                ModelState["UserProfileSpecialPermissionViewModel.UserProfileSpecialPermissionPrmKey"]?.Errors?.Clear();
                ModelState["UserProfileTransactionLimitViewModel.UserProfileTransactionLimitPrmKey"]?.Errors?.Clear();
                ModelState["UserRoleProfileViewModel.UserRoleProfilePrmKey"]?.Errors?.Clear();

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
        public ActionResult GetPersonDropdownByUserType(Guid _userTypeId)
        {
            var personDropdown = personDetailRepository.PersonDropdownListByUserType(_userTypeId);

            return Json(personDropdown, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetMenuByHomeBranch(Guid homeBranchId)
        {
            var menu = userProfileRepository.GetMenuByHomeBranch(homeBranchId);

            return Json(menu, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetUserPasswordValues(short _userProfilePrmKey, string _inputedPassword)
        {
            var data = userProfileRepository.GetUserProfilePasswords(_userProfilePrmKey, _inputedPassword);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<UserProfileIndexViewModel> userProfileViewModel = await userProfileRepository.GetUserProfileIndex(StringLiteralValue.Reject);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<UserProfileBusinessOfficeViewModel> _userProfileBusinessOffice, List<UserProfileCurrencyViewModel> _userProfileCurrency, List<UserProfileGeneralLedgerViewModel> _userProfileGeneralLedger,
                                           List<UserProfileLoginDeviceViewModel> _userProfileLoginDevice, List<UserProfileMenuViewModel> _userProfileMenu,
                                           List<UserProfileSpecialPermissionViewModel> _userProfileSpecialPermission, List<UserProfileTransactionLimitViewModel> _userProfileTransactionLimit, List<UserRoleProfileViewModel> _userRoleProfile
            )
        {
            HttpContext.Session.Add("UserProfileBusinessOffice", _userProfileBusinessOffice);

            HttpContext.Session.Add("UserProfileCurrency", _userProfileCurrency);

            HttpContext.Session.Add("UserProfileGeneralLedger", _userProfileGeneralLedger);

            HttpContext.Session.Add("UserProfileLoginDevice", _userProfileLoginDevice);

            HttpContext.Session.Add("UserProfileMenu", _userProfileMenu);

            HttpContext.Session.Add("UserProfileSpecialPermission", _userProfileSpecialPermission);

            HttpContext.Session.Add("UserProfileTransactionLimit", _userProfileTransactionLimit);

            HttpContext.Session.Add("UserRoleProfile", _userRoleProfile);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<UserProfileIndexViewModel> userProfileViewModel = await userProfileRepository.GetUserProfileIndex(StringLiteralValue.Unverified);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<UserProfileIndexViewModel> userProfileViewModel = await userProfileRepository.GetUserProfileIndex(StringLiteralValue.Verify);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid UserProfileId)
        {
            bool data = await userProfileRepository.GetSessionValues(securityDetailRepository.GetUserProfilePrmKeyById(UserProfileId), StringLiteralValue.Unverified);

            UserProfileViewModel userProfileViewModel = await userProfileRepository.GetUserProfileEntry(UserProfileId, StringLiteralValue.Unverified);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(UserProfileViewModel _userProfileViewModel, string Command)
        {
            //For Clear ModelState Error
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_userProfileViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_userProfileViewModel, StringLiteralValue.Reject);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await userProfileRepository.VerifyRejectDelete(_userProfileViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "UserProfile"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await userProfileRepository.VerifyRejectDelete(_userProfileViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "UserProfile"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "UserProfile");
            }
            else
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_userProfileViewModel.UserProfileId);
        }
    }
}