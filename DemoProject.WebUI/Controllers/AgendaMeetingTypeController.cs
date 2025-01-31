using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    public class AgendaMeetingTypeController : Controller
    {
        private readonly IAgendaMeetingTypeRepository agendaMeetingTypeRepository;
        private readonly IAgendaRepository agendaRepository;

        public AgendaMeetingTypeController(IAgendaMeetingTypeRepository _agendaMeetingTypeRepository, IAgendaRepository _agendaRepository)
        {
            agendaMeetingTypeRepository = _agendaMeetingTypeRepository;
            agendaRepository = _agendaRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid AgendaId)
        {
            HttpContext.Session["AgendaMeetingType"] = await agendaMeetingTypeRepository.GetRejectedEntries(agendaRepository.GetPrmKeyById(AgendaId));

            AgendaMeetingTypeViewModel agendaMeetingTypeViewModel = await agendaMeetingTypeRepository.GetViewModelForReject(agendaRepository.GetPrmKeyById(AgendaId));

            if (agendaMeetingTypeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaMeetingTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_agendaMeetingTypeViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await agendaMeetingTypeRepository.Amend(_agendaMeetingTypeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "AgendaMeetingType");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await agendaMeetingTypeRepository.Delete(_agendaMeetingTypeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "AgendaMeetingType"), }, JsonRequestBehavior.AllowGet);
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

            return View(_agendaMeetingTypeViewModel);
        }

        private void ClearModelStateOfDataTableFields(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            ModelState[nameof(_agendaMeetingTypeViewModel.AgendaId)]?.Errors?.Clear();
            ModelState[nameof(_agendaMeetingTypeViewModel.MeetingTypeId)]?.Errors?.Clear();
            ModelState[nameof(_agendaMeetingTypeViewModel.ActivationDate)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid AgendaId)
        {
            HttpContext.Session["AgendaMeetingType"] = await agendaMeetingTypeRepository.GetVerifiedEntries(agendaRepository.GetPrmKeyById(AgendaId));

            AgendaMeetingTypeViewModel vehicleModelVariantViewModel = await agendaMeetingTypeRepository.GetViewModelForCreate(agendaRepository.GetPrmKeyById(AgendaId));

            if (vehicleModelVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelVariantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            ClearModelStateOfDataTableFields(_agendaMeetingTypeViewModel);

            if (ModelState.IsValid)
            {
                bool result = await agendaMeetingTypeRepository.Save(_agendaMeetingTypeViewModel);

                if (result)
                {
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "AgendaMeetingType");
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

            return View(_agendaMeetingTypeViewModel);
        }

        [HttpGet]
        [Route("Closed")]
        public async Task<ActionResult> Closed(Guid AgendaId)
        {
            HttpContext.Session["AgendaMeetingType"] = await agendaMeetingTypeRepository.GetVerifiedEntries(agendaRepository.GetPrmKeyById(AgendaId));

            AgendaMeetingTypeViewModel vehicleModelVariantViewModel = await agendaMeetingTypeRepository.GetViewModelForCreate(agendaRepository.GetPrmKeyById(AgendaId));

            if (vehicleModelVariantViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(vehicleModelVariantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Closed")]
        public async Task<ActionResult> Closed(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel)
        {
            ClearModelStateOfDataTableFields(_agendaMeetingTypeViewModel);

            if (ModelState.IsValid)
            {
                bool result = await agendaMeetingTypeRepository.Closed(_agendaMeetingTypeViewModel);

                if (result)
                {
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_agendaMeetingTypeViewModel);
        }

        [HttpPost]
        [Route("DropDownGetModel")]
        public ActionResult GetModel(Guid AgendaId)
        {
            var AgendaMeetingType = agendaMeetingTypeRepository.GetModelEntries(AgendaId);

            return Json(AgendaMeetingType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModels = await agendaMeetingTypeRepository.GetIndexWithCreateModifyOperationStatus();

            if (agendaMeetingTypeViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(agendaMeetingTypeViewModels);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<AgendaMeetingTypeViewModel> designationViewModel = await agendaMeetingTypeRepository.GetIndexOfRejectedEntries();

            if (designationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(designationViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModel = await agendaMeetingTypeRepository.GetIndexOfUnVerifiedEntries();

            if (agendaMeetingTypeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaMeetingTypeViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<AgendaMeetingTypeViewModel> agendaMeetingTypeViewModel = await agendaMeetingTypeRepository.GetIndexOfVerifiedEntries();

            if (agendaMeetingTypeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaMeetingTypeViewModel);
        }

        [HttpPost]
        [Route("SaveAgendaMeetingTypeDataTables")]
        public ActionResult SaveAgendaMeetingTypeDataTables(List<AgendaMeetingTypeViewModel> _AgendaMeetingType)
        {
            HttpContext.Session.Add("AgendaMeetingType", _AgendaMeetingType);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid AgendaId)
        {
            HttpContext.Session["AgendaMeetingType"] = await agendaMeetingTypeRepository.GetUnverifiedEntries(agendaRepository.GetPrmKeyById(AgendaId));
            
            AgendaMeetingTypeViewModel agendaMeetingTypeViewModel = await agendaMeetingTypeRepository.GetViewModelForUnverified(agendaRepository.GetPrmKeyById(AgendaId));

            if (agendaMeetingTypeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaMeetingTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _agendaMeetingTypeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await agendaMeetingTypeRepository.Verify(_agendaMeetingTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "AgendaMeetingType"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _agendaMeetingTypeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await agendaMeetingTypeRepository.Reject(_agendaMeetingTypeViewModel);

                    if (result)
                    {
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "AgendaMeetingType"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "AgendaMeetingType");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_agendaMeetingTypeViewModel.AgendaId);
        }
    }
}