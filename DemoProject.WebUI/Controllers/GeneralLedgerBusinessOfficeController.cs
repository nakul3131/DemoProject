using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Account/GL/GeneralLedgerBusinessOffice")]
    public class GeneralLedgerBusinessOfficeController : Controller
    {
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IGeneralLedgerBusinessOfficeRepository generalLedgerBusinessOfficeRepository;

        private readonly IGeneralLedgerDetailRepository generalLedgerDetailRepository;

        public GeneralLedgerBusinessOfficeController(IGeneralLedgerRepository _generalLedgerRepository, IGeneralLedgerDetailRepository _generalLedgerDetailRepository, IGeneralLedgerBusinessOfficeRepository _generalLedgerBusinessOfficeRepository)
        {
            generalLedgerRepository = _generalLedgerRepository;
            generalLedgerBusinessOfficeRepository = _generalLedgerBusinessOfficeRepository;
            generalLedgerDetailRepository = _generalLedgerDetailRepository;
        }
        private void ClearModelStateOfDataTableFields(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "GeneralLedgerBusinessOfficeViewModel";

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

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<GeneralLedgerBusinessOfficeViewModel> _businessOffice)
        {
            HttpContext.Session.Add("GeneralLedgerBusinessOffice", _businessOffice);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: GeneralLedgerBusinessOffice
        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerViewModel = await generalLedgerRepository.GetGeneralLedgerIndex(StringLiteralValue.Verify);

            if (generalLedgerViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerViewModel);
        }
        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(short PrmKey)
        {

            HttpContext.Session["GeneralLedgerBusinessOffice"] = await generalLedgerBusinessOfficeRepository.GetVerifiedGeneralLedgerBusinessOfficeEntries(PrmKey);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_generalLedgerBusinessOfficeViewModel);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await generalLedgerBusinessOfficeRepository.Modify(_generalLedgerBusinessOfficeViewModel);

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

            return View(_generalLedgerBusinessOfficeViewModel);
        }
        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerIndexViewModel = await generalLedgerBusinessOfficeRepository.GetGeneralLedgerBusinessOfficeUnverifiedIndex(StringLiteralValue.Unverified);

            if (generalLedgerIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerIndexViewModel);
        }
        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(short PrmKey)
        {
            HttpContext.Session["GeneralLedgerBusinessOffice"] = await generalLedgerBusinessOfficeRepository.GetUnverifiedGeneralLedgerBusinessOfficeEntries(PrmKey);
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string Command)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_generalLedgerBusinessOfficeViewModel);
            //ModelState.Remove("CurrencyId");
            if (ModelState.IsValid)
            {
                //_userProfileViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await generalLedgerBusinessOfficeRepository.Verify(_generalLedgerBusinessOfficeViewModel);

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
                    bool result = await generalLedgerBusinessOfficeRepository.Reject(_generalLedgerBusinessOfficeViewModel);

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

            return View(_generalLedgerBusinessOfficeViewModel.PrmKey);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<GeneralLedgerIndexViewModel> generalLedgerIndexViewModel = await generalLedgerBusinessOfficeRepository.GetRejectedGeneralLedgerBusinessOfficeIndex(StringLiteralValue.Reject);

            if (generalLedgerIndexViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(generalLedgerIndexViewModel);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(short PrmKey)
        {
            HttpContext.Session["GeneralLedgerBusinessOffice"] = await generalLedgerBusinessOfficeRepository.GetRejectedGeneralLedgerBusinessOfficeEntries(PrmKey);
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string Command)
        {
            //For Clear ModelState Error
            ClearModelStateOfDataTableFields(_generalLedgerBusinessOfficeViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await generalLedgerBusinessOfficeRepository.Amend(_generalLedgerBusinessOfficeViewModel);

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
                    bool result = await generalLedgerBusinessOfficeRepository.Delete(_generalLedgerBusinessOfficeViewModel);

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

            return View(_generalLedgerBusinessOfficeViewModel.PrmKey);
        }


        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(short PrmKey)
        {
            HttpContext.Session["GeneralLedgerBusinessOffice"] = await generalLedgerBusinessOfficeRepository.GetVerifiedGeneralLedgerBusinessOfficeEntries(PrmKey);
            return View();
        }


    }

}