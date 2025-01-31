using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Login/Services/Information")]
    public class AccountServicesController : Controller
    {
        // Login Trouble
        [HttpGet]
        [AllowAnonymous]
        [Route("Trouble Login")]
        public ActionResult TroubleLogin()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _PhoneRecovery()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult _EmailRecovery()
        {
            return View();
        }

        // Safeguard - MENU
        [HttpGet]
        [AllowAnonymous]
        [Route("MultiFactorAuthentication")]
        public ActionResult Authentication()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reCaptcha")]
        public ActionResult GoogleReCaptcha()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Certificate/Digital")]
        public ActionResult DigitalCertificate()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Certificate/SSL/ExtendedValidation")]
        public ActionResult EVSSL()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Log/History")]
        public ActionResult Log()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Explanation")]
        public ActionResult SecuritySolutions()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Team")]
        public ActionResult SecurityTeam()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Session")]
        public ActionResult SessionSecurity()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Monitor/HighRiskTransactions")]
        public ActionResult TransactionMonitor()
        {
            return View();
        }

        // Security Tips - MENU
        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Tips/Password/Safety")]
        public ActionResult SafePassword()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Security/Tips/Computer/Safety")]
        public ActionResult SecurityTips()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Support")]
        public ActionResult Support()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Frequently Asked Questions")]
        public ActionResult Faq()
        {
            return View();
        }

    }
}