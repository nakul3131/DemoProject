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
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Agenda")]
    public class AgendaController : Controller
    {
        private readonly IAgendaRepository agendaRepository;

        public AgendaController(IAgendaRepository _agendaRepository)
        {
            agendaRepository = _agendaRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid AgendaId)
        {
            AgendaViewModel agendaViewModel = await agendaRepository.GetRejectedEntry(AgendaId);

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(AgendaViewModel _agendaViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await agendaRepository.Amend(_agendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await agendaRepository.Delete(_agendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("RejectedIndex", "Agenda");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_agendaViewModel.AgendaId);
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
        public async Task<ActionResult> Create(AgendaViewModel _agendaViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await agendaRepository.Save(_agendaViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Agenda");
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

            return View(_agendaViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueAgendaName(string nameOfAgenda)
        {
            bool data = agendaRepository.GetUniqueAgendaName(nameOfAgenda);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid AgendaId)
        {
            AgendaViewModel agendaViewModel = await agendaRepository.GetVerifiedEntry(AgendaId);

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(AgendaViewModel _agendaViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await agendaRepository.Modify(_agendaViewModel);

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

            return View(_agendaViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<AgendaViewModel> agendaViewModel = await agendaRepository.GetIndexOfRejectedEntries();

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<AgendaViewModel> agendaViewModel = await agendaRepository.GetIndexOfUnVerifiedEntries();

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<AgendaViewModel> agendaViewModel = await agendaRepository.GetIndexOfVerifiedEntries();

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid AgendaId)
        {
            AgendaViewModel agendaViewModel = await agendaRepository.GetUnVerifiedEntry(AgendaId);

            if (agendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(agendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(AgendaViewModel _agendaViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await agendaRepository.Verify(_agendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _agendaViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await agendaRepository.Reject(_agendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "Agenda"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Agenda");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_agendaViewModel.AgendaId);
        }

    }
}