using DemoProject.Services.Abstract.Account.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class OpeningBalanceViewModel
    {
        private readonly IOpeningBalanceRepository openingBalanceRepository;

        public OpeningBalanceViewModel()
        {
            openingBalanceRepository = DependencyResolver.Current.GetService<IOpeningBalanceRepository>();
        }
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal Amount { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int OpeningBalancePrmKey { get; set; }

        public int UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //OpeningBalanceDeposit
        public int OpeningBalanceDepositPrmKey { get; set; }

        public decimal ProductMinBalance { get; set; }

        public decimal ProvisionAmountOfDeposit { get; set; }

        public DateTime LastProvisionDateOfDeposit { get; set; }

        public decimal PreviousInterestAmountOfDeposit { get; set; }

        //OpeningBalanceLoan
        public int OpeningBalanceLoanPrmKey { get; set; }

        public decimal ProvisionAmountOfLoan { get; set; }

        public DateTime LastProvisionDateOfLoan { get; set; }

        public decimal DuesInterestOfLoan { get; set; }

        public decimal PreviousInterestAmountOfLoan { get; set; }

        public DateTime PreviousInstallmentDateOfLoan { get; set; }

        public DateTime PreviousInterestDateOfLoan { get; set; }

        //OpeningBalanceShares
        public int OpeningBalanceSharesPrmKey { get; set; }

        public decimal PreviousYearBalanceOfShares { get; set; }

        public decimal FaceValueOfShares { get; set; }

        public decimal TotalShares { get; set; }

        //OpeningBalanceInvestment
        public int OpeningBalanceInvestmentPrmKey { get; set; }

        public decimal ProvisionAmountOfInvestment { get; set; }

        public DateTime LastProvisionDateOfInvestment { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public Guid GeneralLedgerId { get; set; }

        public Guid CustomerAccountId { get; set; }

        public long PersonPrmKey { get; set; }

        public Guid PersonId { get; set; }

        public string FullName { get; set; }

        public byte SchemeTypePrmKey { get; set; }

        public string DepositType { get; set; }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return openingBalanceRepository.GeneralLedgerDropdownList;
            }
        }

        //public List<SelectListItem> CustomerAccountDropdownList
        //{
        //    get
        //    {
        //        return openingBalanceRepository.CustomerAccountDropdownList;
        //    }
        //}
    }


}
