using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management.Parameter;
using DemoProject.Services.ViewModel.Management.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Parameter/BoardOfDirectorParameter")]
    public class BoardOfDirectorParameterController : Controller
    {
        private readonly IBoardOfDirectorParameterRepository boardOfDirectorParameterRepository;

        public BoardOfDirectorParameterController(IBoardOfDirectorParameterRepository _boardOfDirectorParameterRepository)
        {
            boardOfDirectorParameterRepository = _boardOfDirectorParameterRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            BoardOfDirectorParameterViewModel boardOfDirectorParameterViewModel = await boardOfDirectorParameterRepository.GetRejectedEntry();

            if (boardOfDirectorParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await boardOfDirectorParameterRepository.Amend(_boardOfDirectorParameterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("Default", "Home");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await boardOfDirectorParameterRepository.Delete(_boardOfDirectorParameterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
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

            return View(_boardOfDirectorParameterViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<BoardOfDirectorParameterViewModel> boardOfDirectorParameterViewModels = await boardOfDirectorParameterRepository.GetBoardOfDirectorParameterIndex();

            if (boardOfDirectorParameterViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorParameterViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await boardOfDirectorParameterRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            BoardOfDirectorParameterViewModel boardOfDirectorParameterViewModel = await boardOfDirectorParameterRepository.GetActiveEntry();

            return View(boardOfDirectorParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await boardOfDirectorParameterRepository.Save(_boardOfDirectorParameterViewModel);

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

            return View(_boardOfDirectorParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            BoardOfDirectorParameterViewModel boardOfDirectorParameterViewModel = await boardOfDirectorParameterRepository.GetUnverifiedEntry();

            if (boardOfDirectorParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandVerify)
                {
                    _boardOfDirectorParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                    bool result = await boardOfDirectorParameterRepository.Verify(_boardOfDirectorParameterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _boardOfDirectorParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await boardOfDirectorParameterRepository.Reject(_boardOfDirectorParameterViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_boardOfDirectorParameterViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> ViewEntry()
        {
            BoardOfDirectorParameterViewModel boardOfDirectorParameterViewModel = await boardOfDirectorParameterRepository.GetActiveEntry();

            if (boardOfDirectorParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(boardOfDirectorParameterViewModel);
        }
    }
}
