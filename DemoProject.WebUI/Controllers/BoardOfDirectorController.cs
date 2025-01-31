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
    [RoutePrefix("Employee/DataEntry/Enterprise/BoardOfDirector")]
    public class BoardOfDirectorController : Controller
    {
        private readonly IBoardOfDirectorRepository boardOfDirectorRepository;

        public BoardOfDirectorController(IBoardOfDirectorRepository _boardOfDirectorRepository)
        {
            boardOfDirectorRepository = _boardOfDirectorRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend(Guid BoardOfDirectorId)
        {
            BoardOfDirectorViewModel boardOfDirectorViewModel = await boardOfDirectorRepository.GetRejectedEntry(BoardOfDirectorId);

            if (boardOfDirectorViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(BoardOfDirectorViewModel _boardOfDirectorViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await boardOfDirectorRepository.Amend(_boardOfDirectorViewModel);

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
                    bool result = await boardOfDirectorRepository.Delete(_boardOfDirectorViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("RejectedIndex", "BoardOfDirector");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_boardOfDirectorViewModel);
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
        public async Task<ActionResult> Create(BoardOfDirectorViewModel _boardOfDirectorViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await boardOfDirectorRepository.Save(_boardOfDirectorViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BoardOfDirector");
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

            return View(_boardOfDirectorViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BoardOfDirectorViewModel> boardOfDirectorViewModel = await boardOfDirectorRepository.GetIndexOfRejectedEntries();

            if (boardOfDirectorViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BoardOfDirectorViewModel> boardOfDirectorViewModel = await boardOfDirectorRepository.GetIndexOfUnVerifiedEntries();

            if (boardOfDirectorViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid BoardOfDirectorId)
        {
            BoardOfDirectorViewModel boardOfDirectorViewModel = await boardOfDirectorRepository.GetUnverifiedEntry(BoardOfDirectorId);

            if (boardOfDirectorViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BoardOfDirectorViewModel _boardOfDirectorViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _boardOfDirectorViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await boardOfDirectorRepository.Verify(_boardOfDirectorViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _boardOfDirectorViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await boardOfDirectorRepository.Reject(_boardOfDirectorViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "BoardOfDirector"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_boardOfDirectorViewModel.BoardOfDirectorId);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<BoardOfDirectorViewModel> boardOfDirectorViewModel = await boardOfDirectorRepository.GetIndexOfVerifiedEntries();

            if (boardOfDirectorViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid BoardOfDirectorId)
        {

            BoardOfDirectorViewModel boardOfDirectorViewModel = await boardOfDirectorRepository.GetVerifiedEntry(BoardOfDirectorId);

            if (boardOfDirectorViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorViewModel);
        }

    }
}