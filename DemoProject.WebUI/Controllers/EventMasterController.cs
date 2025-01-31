using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/EventMaster")]
    public class EventMasterController : Controller
    {
        private readonly IEventMasterRepository eventMasterRepository;

        public EventMasterController(IEventMasterRepository _eventMasterRepository)
        {
            eventMasterRepository = _eventMasterRepository;
        }

        [AllowAnonymous]
        public JsonResult GetEventMasters()
        {
            var events = eventMasterRepository.GetEventMasterList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [AllowAnonymous]
        public JsonResult GetEventTypeDropDown()
        {
            var eventTypeDropDown = eventMasterRepository.GetEventTypeDropDown();
            return new JsonResult { Data = eventTypeDropDown, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(EventMasterViewModel _eventMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await eventMasterRepository.Save(_eventMasterViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "EventMaster");
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

            return View(_eventMasterViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(Int16 prmKey)
        {
            if (ModelState.IsValid)
            {
                bool result = await eventMasterRepository.Delete(prmKey);
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return RedirectToAction("Default", "Home");
        }

    }
}