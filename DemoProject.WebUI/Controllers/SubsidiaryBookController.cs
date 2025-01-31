using DemoProject.Services.ViewModel.Report;
using DemoProject.WebUI.Reports;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    public class SubsidiaryBookController : Controller
    {
        // GET: AccountBook
        [AllowAnonymous]
        public ActionResult SubsidiaryBook()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SubsidiaryBook(SubsidiaryBookViewModel accountBookViewModel)
        {
            MasterReport rpt = new MasterReport();

            rpt.Parameters[0].Value = 1;
            rpt.Parameters[1].Value = 1;
            rpt.Parameters[2].Value = "2022-01-01";
            rpt.Parameters[3].Value = "2024-01-01";
            rpt.Parameters[4].Value = accountBookViewModel.SortBy;
            rpt.Parameters[5].Value = accountBookViewModel.IsAscending;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

    }
}