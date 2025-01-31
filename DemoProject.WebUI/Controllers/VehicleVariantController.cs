using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Vehicle/VehicleVariant")]
    public class VehicleVariantController : Controller
    {
        private readonly IVehicleVariantRepository vehicleVariantRepository;

        public VehicleVariantController(IVehicleVariantRepository _vehicleVariantRepository)
        {
            vehicleVariantRepository = _vehicleVariantRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid vehicleVariantId)
        {
            VehicleVariantViewModel vehicleVariantViewModel = await vehicleVariantRepository.GetEntry(vehicleVariantId, StringLiteralValue.Reject);

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(VehicleVariantViewModel _vehicleVariantViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_vehicleVariantViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await vehicleVariantRepository.Amend(_vehicleVariantViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "VehicleVariant");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await vehicleVariantRepository.VerifyRejectDelete(_vehicleVariantViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "VehicleVariant"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required  Valid Information");
            }

            return View(_vehicleVariantViewModel);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType)
        {
            string errorViewModelName = "VehicleVariantViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["VehicleVariantViewModel.PrmKey"]?.Errors?.Clear();
                ModelState["VehicleVariantViewModel.VehicleModelPrmKey"]?.Errors?.Clear();
                ModelState["VehicleVariantViewModel.VehicleBodyTypePrmKey"]?.Errors?.Clear();
                ModelState["VehicleVariantViewModel.VehicleVariantPrmKey"]?.Errors?.Clear();
                ModelState["VehicleVariantViewModel.UserProfilePrmKey"]?.Errors?.Clear();
                ModelState["VehicleVariantViewModel.VehicleVariantModificationPrmKey"]?.Errors?.Clear();

            }
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
        public async Task<ActionResult> Create(VehicleVariantViewModel _vehicleVariantViewModel)
        {
            ClearModelStateOfDataTableFields(_vehicleVariantViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await vehicleVariantRepository.Save(_vehicleVariantViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "VehicleVariant");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_vehicleVariantViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid VehicleVariantId)
        {
            VehicleVariantViewModel vehicleVariantViewModel = await vehicleVariantRepository.GetEntry(VehicleVariantId, StringLiteralValue.Verify);//GetVerifiedEntry

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(VehicleVariantViewModel _vehicleVariantViewModel)
        {
            ClearModelStateOfDataTableFields(_vehicleVariantViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                bool result = await vehicleVariantRepository.Modify(_vehicleVariantViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required  Valid Information");
            }

            return View(_vehicleVariantViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<VehicleVariantViewModel> vehicleVariantViewModel = await vehicleVariantRepository.GetIndex(StringLiteralValue.Reject);

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<VehicleVariantViewModel> vehicleVariantViewModel = await vehicleVariantRepository.GetIndex(StringLiteralValue.Unverified);

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<VehicleVariantViewModel> vehicleVariantViewModel = await vehicleVariantRepository.GetIndex(StringLiteralValue.Verify);

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid vehicleVariantId)
        {
            VehicleVariantViewModel vehicleVariantViewModel = await vehicleVariantRepository.GetEntry(vehicleVariantId, StringLiteralValue.Unverified);

            if (vehicleVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleVariantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(VehicleVariantViewModel _vehicleVariantViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_vehicleVariantViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _vehicleVariantViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await vehicleVariantRepository.VerifyRejectDelete(_vehicleVariantViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleVariant"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _vehicleVariantViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await vehicleVariantRepository.VerifyRejectDelete(_vehicleVariantViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleVariant"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "VehicleVariant");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required  Valid Information");
            }

            return View(_vehicleVariantViewModel.VehicleVariantId);
        }

    }
}