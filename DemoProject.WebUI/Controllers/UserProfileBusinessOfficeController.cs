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
    [RoutePrefix("Employee/Security/UserProfileBusinessOffice")]
    public class UserProfileBusinessOfficeController : Controller
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IUserProfileBusinessOfficeRepository userProfileBusinessOfficeRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public UserProfileBusinessOfficeController(IUserProfileRepository _userProfileRepository, IPersonDetailRepository _personDetailRepository, ISecurityDetailRepository _securityDetailRepository, IUserProfileBusinessOfficeRepository _userProfileBusinessOfficeRepository)
        {
            userProfileRepository = _userProfileRepository;
            personDetailRepository = _personDetailRepository;
            securityDetailRepository = _securityDetailRepository;
            userProfileBusinessOfficeRepository = _userProfileBusinessOfficeRepository;
        }

        private  void ClearModelStateOfDataTableFields(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            string errorViewModelName = "UserProfileBusinessOfficeViewModel";

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

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<UserProfileBusinessOfficeViewModel> _userProfileBusinessOffice)
        {
            HttpContext.Session.Add("UserProfileBusinessOffice", _userProfileBusinessOffice);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(short PrmKey)
        {

            HttpContext.Session["UserProfileBusinessOffice"] = await userProfileBusinessOfficeRepository.GetVerifiedUserProfileBusinessOfficeEntries(PrmKey);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userProfileBusinessOfficeViewModel);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await userProfileBusinessOfficeRepository.Modify(_userProfileBusinessOfficeViewModel);

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

            return View(_userProfileBusinessOfficeViewModel);
        }


        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<UserProfileBusinessOfficeIndexViewModel> userProfileBusinessOfficeIndexViewModel = await userProfileBusinessOfficeRepository.GetUnverifiedUserProfileBusinessOfficeEntries(StringLiteralValue.Unverified);

            if (userProfileBusinessOfficeIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileBusinessOfficeIndexViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(short PrmKey)
        {

            HttpContext.Session["UserProfileBusinessOffice"] = await userProfileBusinessOfficeRepository.GetUnverifiedUserProfileBusinessOfficeEntry(PrmKey,StringLiteralValue.Unverified);

           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string Command)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userProfileBusinessOfficeViewModel);
            //ModelState.Remove("CurrencyId");
            if (ModelState.IsValid)
            {
                _userProfileBusinessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await userProfileBusinessOfficeRepository.Verify(_userProfileBusinessOfficeViewModel);

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
                    bool result = await userProfileBusinessOfficeRepository.Reject(_userProfileBusinessOfficeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "UserProfileBusinessOffice"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "UserProfileBusinessOffice");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_userProfileBusinessOfficeViewModel.BusinessOfficeId);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<UserProfileBusinessOfficeIndexViewModel> userProfileBusinessOfficeIndexViewModel = await userProfileBusinessOfficeRepository.GetRejectedUserProfileBusinessOfficeEntries(StringLiteralValue.Reject);

            if (userProfileBusinessOfficeIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userProfileBusinessOfficeIndexViewModel);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(short PrmKey)
        {
            HttpContext.Session["UserProfileBusinessOffice"] = await userProfileBusinessOfficeRepository.GetRejectedUserProfileBusinessOfficeEntry(PrmKey, StringLiteralValue.Unverified);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel, string Command)
               {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_userProfileBusinessOfficeViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await userProfileBusinessOfficeRepository.Amend(_userProfileBusinessOfficeViewModel);

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
                    bool result = await userProfileBusinessOfficeRepository.Delete(_userProfileBusinessOfficeViewModel);

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

            return View(_userProfileBusinessOfficeViewModel.PrmKey);
        }

    }
}