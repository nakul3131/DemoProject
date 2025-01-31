using System;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class TransactionDynamicDropdownListController : Controller
    {
        private readonly ITransactionDetailRepository transactionDetailRepository;

        public TransactionDynamicDropdownListController( ITransactionDetailRepository _transactionDetailRepository)
        {
            transactionDetailRepository = _transactionDetailRepository;
        }

        public JsonResult GetGeneralLedgerDropdownListForTransaction( Guid _personId, Guid _businessOfficeId)
        {
            var data = transactionDetailRepository.GetGeneralLedgerDropdownListForTransaction(_personId,_businessOfficeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonDropdownListForTransaction(Guid _businessOfficeId)
        {
            var data = transactionDetailRepository.GetPersonDropdownListForTransaction(_businessOfficeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

         public JsonResult GetAccountNumberDropDownListForTransaction(Guid _personId, Guid _businessOfficeId, Guid _generalLedgerId)
        {
            var data = transactionDetailRepository.GetAccountNumberDropDownListForTransaction(_personId, _businessOfficeId,_generalLedgerId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}