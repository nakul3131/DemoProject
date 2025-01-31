using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Security/UserRoleProfile")]
    public class UserRoleProfileController : Controller
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserRoleProfileRepository userRoleProfileRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public UserRoleProfileController(IUserProfileRepository _userProfileRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository, IUserRoleProfileRepository _userRoleProfileRepository)
        {
            userProfileRepository = _userProfileRepository;
            personDetailRepository = _personDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            userRoleProfileRepository = _userRoleProfileRepository;
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

        private void ClearModelStateOfDataTableFields(UserRoleProfileViewModel _userRoleProfileRepository)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "UserRoleProfileViewModel";
          
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
                {
                    ModelState[key]?.Errors?.Clear();
                }
            }

            
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(short PrmKey)
        {
            HttpContext.Session["UserRoleProfile"] = await userRoleProfileRepository.GetVerifiedUserRoleProfileEntries(PrmKey);

            //UserProfileViewModel userProfileViewModel = new UserProfileViewModel();
            //userProfileViewModel.PrmKey = PrmKey;
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]   
        public async Task<ActionResult> Modify(UserRoleProfileViewModel _userRoleProfileViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userRoleProfileViewModel);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await userRoleProfileRepository.Modify(_userRoleProfileViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

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

            return View(_userRoleProfileViewModel);
        }
        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<UserRoleProfileIndexViewModel> userProfileViewModel = await userRoleProfileRepository.GetUserRoleProfileUnverifiedIndex(StringLiteralValue.Unverified);

            if (userProfileViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileViewModel);
        }
        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<UserRoleProfileViewModel> _userRoleProfile)
        {

            HttpContext.Session.Add("UserRoleProfile", _userRoleProfile);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(short PrmKey)
        {
            HttpContext.Session["UserRoleProfile"] = await userRoleProfileRepository.GetUnverifiedUserRoleProfileEntry(PrmKey);
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(UserRoleProfileViewModel _userRoleProfileViewModel, string Command)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userRoleProfileViewModel);
            //ModelState.Remove("CurrencyId");
            if (ModelState.IsValid)
            {
                //_userProfileViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await userRoleProfileRepository.Verify(_userRoleProfileViewModel);

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
                    bool result = await userRoleProfileRepository.Reject(_userRoleProfileViewModel);

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
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_userRoleProfileViewModel.PrmKey);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<UserRoleProfileIndexViewModel> userRoleProfileIndexViewModel = await userRoleProfileRepository.GetRejectedUserRoleProfileEntries(StringLiteralValue.Reject);

            if (userRoleProfileIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userRoleProfileIndexViewModel);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(short PrmKey)
        {
            HttpContext.Session["UserRoleProfile"] = await userRoleProfileRepository.GetRejectedUserRoleProfileEntry(PrmKey);
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(UserRoleProfileViewModel _userRoleProfileViewModel, string Command)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userRoleProfileViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await userRoleProfileRepository.Amend(_userRoleProfileViewModel);

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
                    bool result = await userRoleProfileRepository.Delete(_userRoleProfileViewModel);

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

            return View(_userRoleProfileViewModel.PrmKey);
        }

    }
}