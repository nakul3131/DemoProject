using DemoProject.Services.Concrete;
using DemoProject.Services.ViewModel.Configuration;
using DemoProject.Services.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    public class HomeJQController : Controller
    {
        private readonly EFDbContext context;

        public HomeJQController(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }
        // GET: HomeJQ
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Menusearch(string _inputString)
        {
            var searchlist = GetSearchQueryList(_inputString);
            return Json(searchlist, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SearchQueryViewModel> GetSearchQueryList(string _inputString)

        {
            return (from s in context.SearchQueries
                    where s.QueryText.ToLower().Contains(_inputString.ToLower())
                    select new SearchQueryViewModel { QueryText = s.QueryText, SearchQueryId = s.SearchQueryId }).ToList();
        }

    }
}