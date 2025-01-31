using DemoProject.Services.Abstract.Enterprise;
using System;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    public class EnterpriseChildActionController : Controller
    {
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

    public EnterpriseChildActionController(IEnterpriseDetailRepository _enterpriseDetailRepository)
    {
            enterpriseDetailRepository = _enterpriseDetailRepository;
    }
    public ActionResult GetCoOperativeRegistrationDate()
    {
            DateTime data = enterpriseDetailRepository.GetCoOperativeRegistrationDate();
            return Json(data, JsonRequestBehavior.AllowGet);
    }


    }

}