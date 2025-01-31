using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionMasterViewModel
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IBusinessOfficeRepository businessOfficeRepository;
        private readonly IPersonRepository personRepository;
        private readonly ISharesApplicationRepository sharesApplicationRepository;

        public TransactionMasterViewModel()
        {
            accountDetailRepository = DependencyResolver.Current.GetService<IAccountDetailRepository>();
            businessOfficeRepository = DependencyResolver.Current.GetService<IBusinessOfficeRepository>();
            personRepository = DependencyResolver.Current.GetService<IPersonRepository>();
            sharesApplicationRepository = DependencyResolver.Current.GetService<ISharesApplicationRepository>();
        }

        public long PrmKey { get; set; }

        public Guid TransactionMasterId { get; set; }

        public short PeriodCodePrmKey { get; set; }

        public DateTime TransactionDate { get; set; }

        public int TransactionTypePrmKey { get; set; }

        [StringLength(25)]
        public string TransactionNumber { get; set; }

        [StringLength(25)]
        public string TokenNumber { get; set; }

        [StringLength(500)]
        public string Narration { get; set; }

        [StringLength(500)]
        public string ByHand { get; set; }

        public bool IsBeginDay { get; set; }

        public bool IsVisibleBalance { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // TransactionMasterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Dropdown

        public Guid BusinessOfficeId { get; set; }

        public Guid PersonId { get; set; }

        public Guid CustomerAccountId { get; set; }

        public Guid TransactionTypeId { get; set; }

        public CreditTransactionViewModel CreditTransactionViewModel { get; set; }

        public DebitTransactionViewModel DebitTransactionViewModel { get; set; }

        public SharesCapitalTransactionViewModel SharesTransactionViewModel { get; set; }

        // DropdownList
        //public List<SelectListItem> SharesApplicationDropdownList
        //{
        //    get
        //    {
        //        return sharesApplicationRepository.SharesApplicationDropdownList;
        //    }
        //}

        //public List<SelectListItem> TransactionTypeDropdownList
        //{
        //    get
        //    {
        //        return accountDetailRepository.SharesApplicationTransactionTypeDropdownList;
        //    }
        //}
    }
}
