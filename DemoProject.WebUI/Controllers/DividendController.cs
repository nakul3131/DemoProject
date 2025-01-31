using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/Dividend")]
    public class DividendController : Controller
    {
        private readonly ITransactionDividendRepository transactionDividendRepository;

        public DividendController(ITransactionDividendRepository _transactionDividendRepository)
        {
            transactionDividendRepository = _transactionDividendRepository;
        }
        // GET: Dividend
        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> GetTransactionDividendIndex(DateTime _transactionDate)
        //{
        //    IEnumerable<TransactionDividendIndexViewModel> transactionDividendIndexViewModel = await transactionDividendRepository.GetTransactionDividendIndex(_transactionDate);
        //    return Json(transactionDividendIndexViewModel, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Route("Index")]
        public async Task<ActionResult> Index(TransactionDividendIndexViewModel transactionDividendIndexViewModel)
        {
            IEnumerable<TransactionDividendIndexViewModel> transactionDividendIndex = await transactionDividendRepository.GetTransactionDividendIndex(transactionDividendIndexViewModel.TransactionDate);
            return View(transactionDividendIndex);
        }

    }
}