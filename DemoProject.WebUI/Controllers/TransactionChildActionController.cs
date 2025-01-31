using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Concrete.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class TransactionChildActionController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ICustomerAccountRepository customerAccountRepository;
        private readonly ICustomerDetailRepository customerDetailRepository;
        private readonly ITransactionDetailRepository transactionDetailRepository;
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IPersonStatusRepository personStatusRepository;
        private readonly IPersonAdditionalDetailRepository personAdditionalDetailRepository;
        private readonly ITransactionCustomerAccountRepository transactionCustomerAccountRepository;

        public TransactionChildActionController(IAccountDetailRepository _accountDetailRepository, ICustomerAccountRepository _customerAccountRepository, ICustomerDetailRepository _customerDetailRepository, 
                                             ITransactionDetailRepository _transactionDetailRepository,
                                             IPersonStatusRepository _personStatusRepository,
                                             IGeneralLedgerRepository _generalLedgerRepository,
                                             IPersonAdditionalDetailRepository _personAdditionalDetailRepository,
                                             ITransactionCustomerAccountRepository _transactionCustomerAccountRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            customerAccountRepository = _customerAccountRepository;
            customerDetailRepository = _customerDetailRepository;
            transactionDetailRepository = _transactionDetailRepository;
            personStatusRepository = _personStatusRepository;
            generalLedgerRepository = _generalLedgerRepository;
            personAdditionalDetailRepository = _personAdditionalDetailRepository;
            transactionCustomerAccountRepository = _transactionCustomerAccountRepository;
        }
       
        public JsonResult GetSysNameOfSchemeTypeByGeneralLedgerId(Guid _generalLedgerId)
        {
            var data = accountDetailRepository.GetSysNameOfSchemeTypeByGeneralLedgerId(_generalLedgerId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
       
        public JsonResult GetCustomerWithJointAccount(Guid _generalLedgerId, Guid _personId)
        {
            var data = customerAccountRepository.GetCustomerWithJointAccountDropdownList(_generalLedgerId, _personId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMemberType(Guid _personId)
        {
            var data = personStatusRepository.GetMemberType(_personId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIsVIPCustomer(Guid _personId)
        {
            var data = personAdditionalDetailRepository.IsVIPCustomer(_personId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetClosingBalance(DateTime _balanceDate, Guid _customerAccountId)
        {

            var data = transactionCustomerAccountRepository.GetClosingBalance(_balanceDate, _customerAccountId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLedgerBalance(DateTime _balanceDate, Guid _generalLedgerId)
        {
            var data = transactionCustomerAccountRepository.GetLedgerBalance(_balanceDate, _generalLedgerId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetTotalNumberOfShares(Guid _customerAccountId)
        {
            long _customerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_customerAccountId);
            var data = customerDetailRepository.GetTotalNumberOfShares(_customerAccountPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSharesCertificateNumbers(Guid _customerAccountId)
        {
            long _customerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_customerAccountId);
            var data = customerDetailRepository.GetAllSharesCertificateNumbers(_customerAccountPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSharesCurrentFinancialYearWithradwal(Guid _generalLedgerId)
        {
            short generalLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(_generalLedgerId);
            var data = transactionDetailRepository.GetCurrentYearSharesWithdrawal(generalLedgerPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAllowSharesWithdrawal(Guid _customerAccountId)
        {
            bool result = false;

            long customerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_customerAccountId);

            short membershipAge = customerDetailRepository.GetCustomerAccountAge(customerAccountPrmKey);

            short requiredAgeForWithdrawal = accountDetailRepository.MembershipAgeForResignMembership();

            if (membershipAge >= requiredAgeForWithdrawal)
                result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCrossedAggeregateSharesWithdrawalLimit(Guid _generalLedgerId)
        {
            bool result = false;

            short _generalLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(_generalLedgerId);

            decimal SharesWithdrawal = transactionDetailRepository.GetCurrentYearSharesWithdrawal(_generalLedgerPrmKey);

            decimal aggregateSharesWithdrawalLimit = accountDetailRepository.AggregateSharesWithdrawalLimit();

            if (SharesWithdrawal < aggregateSharesWithdrawalLimit)
                result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public async Task<ActionResult> GetSharesCapitalTransactionSettingValues(DateTime _transactionDate,Guid _customerAccountId,bool _isCreditTransaction)
        {
            TransactionSettingViewModel data = await transactionDetailRepository.GetSharesCapitalTransactionSettingValues(_transactionDate, _customerAccountId, _isCreditTransaction);

            return Json(data, JsonRequestBehavior.AllowGet);
           
        }
        public async Task<ActionResult> GetTransactionTypeSetting(Guid _transactionTypeId)
        {
            TransactionTypeSettingViewModel data = await transactionDetailRepository.GetTransactionTypeSetting(_transactionTypeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
      
        //public JsonResult GetDepositeSettingsValues(Guid _customerAccountId)
        //{
        //    long _customerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_customerAccountId);
        //    short _schemePrmKey = customerDetailRepository.GetSchemePrmKeyOfCustomerAccount(_customerAccountPrmKey);
        //    short _customerAccountage = customerDetailRepository.GetCustomerAccountAge(_customerAccountPrmKey);
        //    TransactionDynamicSettingViewModel data = new TransactionDynamicSettingViewModel()
        //    {
        //        AccountOpeningAmount = accountDetailRepository.GetAccountOpeningAmount(_schemePrmKey),
        //        IsNewCustomer = customerDetailRepository.IsNewCustomer(_customerAccountPrmKey),
        //        IsTransactionExists = transactionDetailRepository.IsTransactionExists(_customerAccountPrmKey),
        //        GetTransactionType = transactionDetailRepository.GetTransactionType(_schemePrmKey),
        //        TotalNumberOfShares = customerDetailRepository.GetTotalNumberOfShares(_customerAccountPrmKey),
        //        AllSharesCertificateNumbers = customerDetailRepository.GetAllSharesCertificateNumbers(_customerAccountPrmKey),
        //       // schemeClosingChargesViewModel = accountDetailRepository.GetSchemeClosingCharges(_schemePrmKey, _customerAccountage),
        //        IsAllowToCloseSharesCapitalAccount = customerDetailRepository.IsAllowToCloseSharesCapitalAccount(_customerAccountPrmKey)
        //    };
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
       

    }
}