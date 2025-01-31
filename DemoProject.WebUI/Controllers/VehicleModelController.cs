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
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Vehicle/VehicleModel")]
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelRepository vehicleModelRepository;

        public VehicleModelController(IVehicleModelRepository _vehicleModelRepository)
        {
            vehicleModelRepository = _vehicleModelRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid vehicleModelId)
        {
            VehicleModelViewModel vehicleModelViewModel = await vehicleModelRepository.GetEntry(vehicleModelId, StringLiteralValue.Reject);

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(VehicleModelViewModel _vehicleModelViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await vehicleModelRepository.Amend(_vehicleModelViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "VehicleModel");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await vehicleModelRepository.VerifyRejectDelete(_vehicleModelViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "VehicleModel"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_vehicleModelViewModel);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(VehicleModelViewModel _vehicleModelViewModel, string _entryType)
        {
            string errorViewModelName = "VehicleModelViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["VehicleModelViewModel.PrmKey"]?.Errors?.Clear();
                ModelState["VehicleModelViewModel.VehicleMakePrmKey"]?.Errors?.Clear();
                ModelState["VehicleModelViewModel.VehicleModelPrmKey"]?.Errors?.Clear();
                ModelState["VehicleModelViewModel.VehicleModelModificationPrmKey"]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(VehicleModelViewModel _vehicleModelViewModel)
        {

            ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await vehicleModelRepository.Save(_vehicleModelViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "VehicleModel");
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

            return View(_vehicleModelViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid vehicleModelId)
        {
            VehicleModelViewModel vehicleModelViewModel = await vehicleModelRepository.GetEntry(vehicleModelId, StringLiteralValue.Verify);//GetVerifiedEntry

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(VehicleModelViewModel _vehicleModelViewModel)
        {
            ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                bool result = await vehicleModelRepository.Modify(_vehicleModelViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_vehicleModelViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<VehicleModelViewModel> vehicleModelViewModel = await vehicleModelRepository.GetIndex(StringLiteralValue.Reject);

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<VehicleModelViewModel> vehicleModelViewModel = await vehicleModelRepository.GetIndex(StringLiteralValue.Unverified);

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<VehicleModelViewModel> vehicleModelViewModel = await vehicleModelRepository.GetIndex(StringLiteralValue.Verify);

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }
            return View(vehicleModelViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid vehicleModelId)
        {
            VehicleModelViewModel vehicleModelViewModel = await vehicleModelRepository.GetEntry(vehicleModelId, StringLiteralValue.Unverified);

            if (vehicleModelViewModel is null)
            {
                throw new DatabaseException();
            }
            return View(vehicleModelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(VehicleModelViewModel _vehicleModelViewModel, string command)
        {

            if (command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_vehicleModelViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _vehicleModelViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await vehicleModelRepository.VerifyRejectDelete(_vehicleModelViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleModel"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _vehicleModelViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await vehicleModelRepository.VerifyRejectDelete(_vehicleModelViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleModel"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("UnverifiedIndex", "VehicleModel");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_vehicleModelViewModel.VehicleModelId);
        }
    }
}