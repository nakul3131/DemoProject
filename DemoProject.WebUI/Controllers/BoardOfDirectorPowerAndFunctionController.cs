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
    [RoutePrefix("Employee/DataEntry/Enterprise/BoardOfDirectorPowerAndFunction")]
    public class BoardOfDirectorPowerAndFunctionController : Controller
    {
        private readonly IBoardOfDirectorPowerAndFunctionRepository boardOfDirectorPowerAndFunctionRepository;

        public BoardOfDirectorPowerAndFunctionController(IBoardOfDirectorPowerAndFunctionRepository _boardOfDirectorPowerAndFunctionRepository)
        {
            boardOfDirectorPowerAndFunctionRepository = _boardOfDirectorPowerAndFunctionRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BoardOfDirectorPowerAndFunctionId)
        {
            BoardOfDirectorPowerAndFunctionViewModel boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetRejectedEntry(BoardOfDirectorPowerAndFunctionId);

            if (boardOfDirectorPowerAndFunctionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await boardOfDirectorPowerAndFunctionRepository.Amend(_boardOfDirectorPowerAndFunctionViewModel);

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
                    bool result = await boardOfDirectorPowerAndFunctionRepository.Delete(_boardOfDirectorPowerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "F"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirectorPowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                }
                return RedirectToAction("RejectedIndex", "BoardOfDirectorPowerAndFunction");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorPowerAndFunctionId);
        }

        [HttpGet]
        [Route("Change")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Create(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await boardOfDirectorPowerAndFunctionRepository.Save(_boardOfDirectorPowerAndFunctionViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BoardOfDirectorPowerAndFunction");
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

            return View(_boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BoardOfDirectorPowerAndFunctionViewModel> boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetIndexOfRejectedEntries();

            if (boardOfDirectorPowerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BoardOfDirectorPowerAndFunctionViewModel> boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetIndexOfUnVerifiedEntries();

            if (boardOfDirectorPowerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid BoardOfDirectorPowerAndFunctionId)
        {
            BoardOfDirectorPowerAndFunctionViewModel boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetUnverifiedEntry(BoardOfDirectorPowerAndFunctionId);

            if (boardOfDirectorPowerAndFunctionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _boardOfDirectorPowerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await boardOfDirectorPowerAndFunctionRepository.Verify(_boardOfDirectorPowerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BoardOfDirectorPowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirectorPowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _boardOfDirectorPowerAndFunctionViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await boardOfDirectorPowerAndFunctionRepository.Reject(_boardOfDirectorPowerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BoardOfDirectorPowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirectorPowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BoardOfDirectorPowerAndFunction");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_boardOfDirectorPowerAndFunctionViewModel.BoardOfDirectorPowerAndFunctionId);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<BoardOfDirectorPowerAndFunctionViewModel> boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetIndexOfVerifiedEntries();

            if (boardOfDirectorPowerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid BoardOfDirectorPowerAndFunctionId)
        {

            BoardOfDirectorPowerAndFunctionViewModel boardOfDirectorPowerAndFunctionViewModel = await boardOfDirectorPowerAndFunctionRepository.GetUnverifiedEntry(BoardOfDirectorPowerAndFunctionId);

            if (boardOfDirectorPowerAndFunctionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorPowerAndFunctionViewModel);
        }

    }
}