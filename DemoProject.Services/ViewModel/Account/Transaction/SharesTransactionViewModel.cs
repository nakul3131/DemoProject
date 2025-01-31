using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class SharesCapitalTransactionViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;

        public SharesCapitalTransactionViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
        }

        public long PrmKey { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public decimal SharesFaceValue { get; set; }

        public short NumberOfShares { get; set; }

        public int StartSharesCertificateNumber { get; set; }      // Manage In Transaction Procedure

        public int EndSharesCertificateNumber { get; set; } 

        public bool IsPrinted { get; set; }

        public bool IsSharesCertificateIssued { get; set; }

        public bool IsReturned { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public int PreviousCertificateNumber { get; set; }

        public decimal AdmissionFees { get; set; }

        public decimal Charges1 { get; set; }

        public decimal SharesAmount { get; set; }

        public decimal SharesTransferCharges { get; set; }

        public decimal SharesClosingCharges { get; set; }

        //SharesCapitalTransactionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long SharesCapitalTransactionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]

        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public decimal MaximumIndividualSharesHoldingLimit => accountDetailRepository.GetMaximumSharesHolidingLimitAmount(); //confirm and delete

    }


}
