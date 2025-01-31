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
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Vehicle/VehicleMake")]
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeRepository vehicleMakeRepository;

        public VehicleMakeController(IVehicleMakeRepository _vehicleMakeRepository)
        {
            vehicleMakeRepository = _vehicleMakeRepository;
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType)
        {
            string errorViewModelName = "VehicleMakeViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["VehicleMakeViewModel.VehicleMakePrmKey"]?.Errors?.Clear();
                ModelState["VehicleMakeViewModel.CenterPrmKey"]?.Errors?.Clear();
                ModelState["VehicleMakeViewModel.UserProfilePrmKey"]?.Errors?.Clear();
                ModelState["VehicleMakeViewModel.VehicleMakeModificationPrmKey"]?.Errors?.Clear();
                ModelState["VehicleMakeViewModel.PrmKey"]?.Errors?.Clear();

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
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid vehicleMakeId)
        {
            VehicleMakeViewModel vehicleMakeViewModel = await vehicleMakeRepository.GetEntry(vehicleMakeId,StringLiteralValue.Reject);

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(VehicleMakeViewModel _vehicleMakeViewModel, string command)
        {

            if (command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await vehicleMakeRepository.Amend(_vehicleMakeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "VehicleMake");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await vehicleMakeRepository.VerifyRejectDelete(_vehicleMakeViewModel,StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "VehicleMake"), }, JsonRequestBehavior.AllowGet);
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

            return View(_vehicleMakeViewModel);
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
        public async Task<ActionResult> Create(VehicleMakeViewModel _vehicleMakeViewModel)
        {
            ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await vehicleMakeRepository.Save(_vehicleMakeViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "VehicleMake");
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

            return View(_vehicleMakeViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueVehicleMakeName(string nameOfVehicleMake)
        {
            bool data = vehicleMakeRepository.GetUniqueVehicleMakeName(nameOfVehicleMake);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid VehicleMakeId)
        {

            VehicleMakeViewModel vehicleMakeViewModel = await vehicleMakeRepository.GetEntry(VehicleMakeId, StringLiteralValue.Verify);//GetVerifiedEntry

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(VehicleMakeViewModel _vehicleMakeViewModel)
        {
            ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await vehicleMakeRepository.Modify(_vehicleMakeViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_vehicleMakeViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<VehicleMakeViewModel> vehicleMakeViewModel = await vehicleMakeRepository.GetIndex(StringLiteralValue.Reject);

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<VehicleMakeViewModel> vehicleMakeViewModel = await vehicleMakeRepository.GetIndex(StringLiteralValue.Unverified);

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<VehicleMakeViewModel> vehicleMakeViewModel = await vehicleMakeRepository.GetIndex(StringLiteralValue.Verify);

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid VehicleMakeId)
        {
            VehicleMakeViewModel vehicleMakeViewModel = await vehicleMakeRepository.GetEntry(VehicleMakeId, StringLiteralValue.Unverified);

            if (vehicleMakeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleMakeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(VehicleMakeViewModel _vehicleMakeViewModel, string Command)
        {

            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_vehicleMakeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _vehicleMakeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await vehicleMakeRepository.VerifyRejectDelete(_vehicleMakeViewModel,StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleMake"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _vehicleMakeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await vehicleMakeRepository.VerifyRejectDelete(_vehicleMakeViewModel,StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VehicleMake"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "VehicleMake");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_vehicleMakeViewModel.VehicleMakeId);
        }
        
    }
}