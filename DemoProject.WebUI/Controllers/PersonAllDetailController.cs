using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/Person")]
    public class PersonAllDetailController : Controller
    {
        private readonly IPersonAllDetailRepository personAllDetailRepository;

        public PersonAllDetailController(IPersonAllDetailRepository _personAllDetailRepository)
        {
            personAllDetailRepository = _personAllDetailRepository;
        }

        // GET: PersonAllDetail
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(PersonViewModel _personViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await personAllDetailRepository.Save(_personViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_personViewModel);
        }
    }
}