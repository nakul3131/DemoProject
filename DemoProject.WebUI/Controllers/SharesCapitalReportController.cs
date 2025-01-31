using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.ViewModel.Report;
using DemoProject.WebUI.Reports;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Report")]
    public class SharesCapitalReportController : Controller
    {
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ISharesApplicationRepository sharesApplicationRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public SharesCapitalReportController(IConfigurationDetailRepository _configurationDetailRepository, ISharesApplicationRepository _sharesApplicationRepository, IAccountDetailRepository _accountDetailRepository, IPersonDetailRepository _personDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository)
        {
            configurationDetailRepository = _configurationDetailRepository;
            sharesApplicationRepository = _sharesApplicationRepository;
            accountDetailRepository = _accountDetailRepository;
            personDetailRepository = _personDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("SharesApplicationReport")]
        public ActionResult SharesApplicationReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SharesApplicationReport")]
        public ActionResult SharesApplicationReport(SharesApplicationReportViewModel _sharesApplicationReportViewModel)
        {
            SharesApplicationReport rpt = new SharesApplicationReport();
            rpt.Parameters["SharesApplicationPrmKey"].Value = sharesApplicationRepository.GetPrmKeyByNumber(_sharesApplicationReportViewModel.ApplicationNumber);
            rpt.Parameters["SharesApplicationPrmKey"].Visible = false;
            rpt.Parameters["UserProfilePrmKey"].Value = 4;
            rpt.Parameters["UserProfilePrmKey"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_sharesApplicationReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult MemberRegisterReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult MemberRegisterReport(MemberRegisterReportViewModel _memberRegisterReportViewModel)
        {
            MemberRegisterReport rpt = new MemberRegisterReport();
            rpt.Parameters["CustomerAccountPrmKey"].Value = accountDetailRepository.GetCustomerAccountPrmKeyById(_memberRegisterReportViewModel.CustomerAccountId);
            rpt.Parameters["CustomerAccountPrmKey"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_memberRegisterReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult JReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult JReport(MemberRegisterReportViewModel _memberRegisterReportViewModel)
        {
            JReport rpt = new JReport();
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_memberRegisterReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult J1Report()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult J1Report(MemberRegisterReportViewModel _memberRegisterReportViewModel)
        {
            J1Report rpt = new J1Report();
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_memberRegisterReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult J2Report()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult J2Report(MemberRegisterReportViewModel _memberRegisterReportViewModel)
        {
            J2Report rpt = new J2Report();
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_memberRegisterReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult KReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult KReport(KReportViewModel _kReportViewModel)
        {
            KReport rpt = new KReport();
            rpt.Parameters["PersonPrmKey"].Value = personDetailRepository.GetPersonPrmKeyById(_kReportViewModel.PersonId);
            rpt.Parameters["PersonPrmKey"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_kReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult WReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult WReport(KReportViewModel _kReportViewModel)
        {
            WReport rpt = new WReport();
            rpt.Parameters["PersonPrmKey"].Value = personDetailRepository.GetPersonPrmKeyById(_kReportViewModel.PersonId);
            rpt.Parameters["PersonPrmKey"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_kReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }

        //private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        //{
        //    if (e.SeriesPoint.Values[0] > 1)
        //    {
        //        e.LabelText = "Critical value";
        //    }
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult MemberIncrementReport()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult MemberIncrementReport(KReportViewModel _kReportViewModel)
        {
            WReport rpt = new WReport();
            rpt.Parameters["BusinessOfficePrmKey"].Value = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_kReportViewModel.BusinessOfficeId);
            rpt.Parameters["BusinessOfficePrmKey"].Visible = false;
            rpt.Parameters["FromYear"].Value = _kReportViewModel.FromYear;
            rpt.Parameters["FromYear"].Visible = false;
            rpt.Parameters["ToYear"].Value = _kReportViewModel.ToYear;
            rpt.Parameters["ToYear"].Visible = false;
            rpt.Parameters["LanguagePrmkey"].Value = configurationDetailRepository.GetLanguagePrmKeyById(_kReportViewModel.RegionalLanguageId);
            rpt.Parameters["LanguagePrmkey"].Visible = false;
            rpt.Parameters["IsBranchWise"].Value = _kReportViewModel.IsBranchWise;
            rpt.Parameters["IsBranchWise"].Visible = false;
            TempData["Report"] = rpt;
            rpt.CreateDocument();
            return View();
        }
    }
}