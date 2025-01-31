using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.AppSupported;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.ViewModel.Report;
using DemoProject.WebUI.Reports;
using System;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    public class FinancialStatementsReportController : Controller
    {
        private readonly IAppSupportedLanguageRepository appSupportedLanguageRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        public FinancialStatementsReportController(IAccountDetailRepository _accountDetailRepository,IAppSupportedLanguageRepository _appSupportedLanguageRepository, ISecurityDetailRepository _securityDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository)
        {
            appSupportedLanguageRepository = _appSupportedLanguageRepository;
            securityDetailRepository = _securityDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult BalanceListReport()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult BalanceListReport(BalanceListViewModel balanceListViewModel)
       {
            BalanceListReport rpt = new BalanceListReport();
            rpt.Parameters["EffectiveDate"].Value = balanceListViewModel.EffectiveDate;
            rpt.Parameters["GeneralLedgerPrmKey"].Value = 47;//accountDetailRepository.GetGeneralLedgerPrmKeyById(balanceListViewModel.GeneralLedgerId); ;// 
            var yy= Convert.ToDateTime(rpt.Parameters["EffectiveDate"].Value);
            var tt=accountDetailRepository.GetGeneralLedgerPrmKeyById(balanceListViewModel.GeneralLedgerId);
            rpt.Parameters["FromBalance"].Value = balanceListViewModel.FromBalance;
            rpt.Parameters["ToBalance"].Value = balanceListViewModel.ToBalance;
            rpt.Parameters["FromAccountOpeningDate"].Value = balanceListViewModel.FromAccountOpeningDate;
            rpt.Parameters["ToAccountOpeningDate"].Value = balanceListViewModel.ToAccountOpeningDate;
            rpt.Parameters["LanguagePrmkey"].Value = appSupportedLanguageRepository.GetPrmKeyById(balanceListViewModel.RegionalLanguageId);
            rpt.Parameters["BranchPrmKey"].Value = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(balanceListViewModel.BusinessOfficeId);
            rpt.Parameters["MemberType"].Value = balanceListViewModel.MemberTypePrmKey;
            rpt.Parameters["SchemePrmKey"].Value = accountDetailRepository.GetSchemePrmKeyById(balanceListViewModel.SchemeId);
            rpt.Parameters["IsAscending"].Value = balanceListViewModel.IsAscending;
            rpt.Parameters["GroupBy"].Value = balanceListViewModel.GroupBy;
            rpt.Parameters["SortBy"].Value = balanceListViewModel.SortBy;
            TempData["Report"] = rpt;
            rpt.CreateDocument();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetAuthorizedGeneralLedger(Guid _businessOfficeId)
        {
            var data = 1; // securityDetailRepository.AuthorizedGeneralLedgerBusinessOfficeDropdownList(_businessOfficeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetSchemeDropdownListByGeneralLedger(Guid _generalLedgerId)
        {
            var data = accountDetailRepository.GetSchemeDropdownListByGeneralLedgerId(_generalLedgerId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}