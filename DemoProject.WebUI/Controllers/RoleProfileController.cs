using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.UserRoles;
using DemoProject.Services.ViewModel.Security.UserRoles;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Security/RoleProfiles")]
    public class RoleProfileController : Controller
    {
        private readonly IRoleProfileRepository roleProfileRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public RoleProfileController(IRoleProfileRepository _roleProfileRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            roleProfileRepository = _roleProfileRepository;
            securityDetailRepository = _securityDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid RoleProfileId)
        {
            bool data = await roleProfileRepository.GetSessionValues(securityDetailRepository.GetRoleProfilePrmKeyById(RoleProfileId), StringLiteralValue.Reject);

            RoleProfileViewModel roleProfileViewModel = await roleProfileRepository.GetRoleProfileEntry(RoleProfileId, StringLiteralValue.Reject);

            if (roleProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(roleProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(RoleProfileViewModel _roleProfileViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_roleProfileViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_roleProfileViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await roleProfileRepository.Amend(_roleProfileViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "RoleProfile");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await roleProfileRepository.VerifyRejectDelete(_roleProfileViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "RoleProfile"), }, JsonRequestBehavior.AllowGet);
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

            return View(_roleProfileViewModel.RoleProfileId);
        }

        private void ClearModelStateOfDataTableFields(RoleProfileViewModel _roleProfileViewModel, string _entryType)
        {
            string errorViewModelName = "RoleProfileGeneralLedgerViewModel,RoleProfileBusinessOfficeViewModel,RoleProfileTransactionLimitViewModel,RoleProfileMenuViewModel";
            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["RoleProfileGeneralLedgerViewModel.RoleProfileGeneralLedgerPrmKey"]?.Errors?.Clear();
                ModelState["RoleProfileBusinessOfficeViewModel.RoleProfileBusinessOfficePrmKey"]?.Errors?.Clear();
                ModelState["RoleProfileTransactionLimitViewModel.RoleProfileTransactionLimitPrmKey"]?.Errors?.Clear();
                ModelState["RoleProfileMenuViewModel.RoleProfileMenuPrmKey"]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(RoleProfileViewModel _roleProfileViewModel)
        {
            ClearModelStateOfDataTableFields(_roleProfileViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await roleProfileRepository.Save(_roleProfileViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "RoleProfile");
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

            return View(_roleProfileViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueRoleProfileName(string nameOfRoleProfile)
        {
            bool data = roleProfileRepository.GetUniqueRoleProfileName(nameOfRoleProfile);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetModel(Guid MenuId)
        {
            var mainMenu = roleProfileRepository.GetModelEntries(MenuId);

            return Json(mainMenu, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<RoleProfileIndexViewModel> roleProfileViewModel = await roleProfileRepository.GetRoleProfileIndex(StringLiteralValue.Reject);

            if (roleProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(roleProfileViewModel);
        }

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<RoleProfileGeneralLedgerViewModel> _roleProfileGeneralLedgerViewModel, List<RoleProfileBusinessOfficeViewModel> _roleProfileBusinessOfficeViewModel, List<RoleProfileTransactionLimitViewModel> _roleProfileTransactionLimitViewModel, List<RoleProfileMenuViewModel> _roleProfileMenuViewModel, List<RoleProfileSpecialPermissionViewModel> _roleProfileSpecialPermissionViewModel)
        {
            HttpContext.Session.Add("RoleProfileGeneralLedger", _roleProfileGeneralLedgerViewModel);

            HttpContext.Session.Add("RoleProfileBusinessOffice", _roleProfileBusinessOfficeViewModel);

            HttpContext.Session.Add("RoleProfileTransactionLimit", _roleProfileTransactionLimitViewModel);

            HttpContext.Session.Add("RoleProfileMenu", _roleProfileMenuViewModel);

            HttpContext.Session.Add("RoleProfileSpecialPermission", _roleProfileSpecialPermissionViewModel);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<RoleProfileIndexViewModel> roleProfileViewModel = await roleProfileRepository.GetRoleProfileIndex(StringLiteralValue.Unverified);

            if (roleProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(roleProfileViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetRoleProfileAllowAllAccess(Guid _roleProfileId)
        {
            var roleProfileViewModel = roleProfileRepository.GetRoleProfileAllowAllAccess(_roleProfileId);
            return Json(roleProfileViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<RoleProfileIndexViewModel> roleProfileViewModel = await roleProfileRepository.GetRoleProfileIndex(StringLiteralValue.Verify);

            if (roleProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(roleProfileViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid RoleProfileId)
        {
            bool data = await roleProfileRepository.GetSessionValues(securityDetailRepository.GetRoleProfilePrmKeyById(RoleProfileId), StringLiteralValue.Unverified);

            RoleProfileViewModel roleProfileViewModel = await roleProfileRepository.GetRoleProfileEntry(RoleProfileId, StringLiteralValue.Unverified);

            if (roleProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(roleProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(RoleProfileViewModel _roleProfileViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_roleProfileViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_roleProfileViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _roleProfileViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await roleProfileRepository.VerifyRejectDelete(_roleProfileViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "RoleProfile"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await roleProfileRepository.VerifyRejectDelete(_roleProfileViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "RoleProfile"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "RoleProfile");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_roleProfileViewModel.RoleProfileId);
        }

    }
}