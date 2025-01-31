using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.MachineLearning;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Concrete.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionDetailViewModel
    {
        private readonly IMLDetailRepository mlDetailRepository;
        private readonly ITransactionDetailRepository transactionDetailRepository;

        public TransactionDetailViewModel()
        {
            mlDetailRepository = DependencyResolver.Current.GetService<IMLDetailRepository>();
            transactionDetailRepository = DependencyResolver.Current.GetService<ITransactionDetailRepository>();
        }
        public Guid HomeBranchBusinessOfficeId => transactionDetailRepository.GetBusinessOfficeIdByPrmKey((short)HttpContext.Current.Session["UserHomeBranchPrmKey"]);

        public List<SelectListItem> BusinessOfficeDropdownList => transactionDetailRepository.GetBusinessOfficeDropDownListForTransaction();

        public List<SelectListItem> DenominationDropdownList => transactionDetailRepository.DenominationDropdownList;

        // public List<SelectListItem> GeneralLedgerDropdownList => generalLedgerRepository.GLParentDropdownList;

        public List<SelectListItem> PaymentModeDropdownList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Cash"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("Cash"),
                        Value = "CAS"
                    },
                    new SelectListItem
                    {
                        Text = "Cheque"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("Cheque"),
                        Value = "CHQ"
                    },
                    new SelectListItem
                    {
                        Text = "IMPS"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("IMPS"),
                        Value = "IMP"
                    },
                   new SelectListItem
                    {
                        Text = "NEFT"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("NEFT"),
                        Value = "NFT"
                    },
                     new SelectListItem
                    {
                        Text = "Online Transaction"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("Online Transaction"),
                        Value = "ONT"
                    },
                    new SelectListItem
                    {
                        Text = "RTGS"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("RTGS"),
                        Value = "RTG"
                    },
                   new SelectListItem
                    {
                        Text = "Transfer"+ " ---> " + mlDetailRepository.TranslateInRegionalLanguage("Transfer"),
                        Value = "TRF"
                    },
                    new SelectListItem
                    {
                        Text = "UPI/QR"+ " --->" + mlDetailRepository.TranslateInRegionalLanguage("UPI/QR"),
                        Value = "UPI"
                    }

                };
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList => transactionDetailRepository.GetTransactionTypeDropDownListForTransaction();

    }
}
