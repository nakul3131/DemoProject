using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}