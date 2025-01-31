using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionDetailRepository
    {
        bool EnableCashDenomination();
        bool IsTransactionExists(long _customerAccountPrmKey);

        Guid GetBusinessOfficeIdByPrmKey(short _userHomeBranchPrmKey);
        
        decimal GetCurrentYearSharesWithdrawal(short _generalLedgerPrmKey);
        
        string GetTransactionType(short _schemePrmKey);

        Task<TransactionSettingViewModel> GetSharesCapitalTransactionSettingValues(DateTime _transactionDate, Guid _customerAccountId, bool _isCreditTransaction);
        Task<TransactionTypeSettingViewModel> GetTransactionTypeSetting(Guid _transactionTypeId);

        Task<IEnumerable<TransactionCustomerAccountViewModel>> GetTransactionCustomerAccountEntries(long _transactionMasterPrmKey, string _entryType);

        Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetTransactionGeneralLedgerEntries(long _transactionMasterPrmKey, string _entryType);
        
        Task<IEnumerable<TransactionCustomerAccountOtherSubscriptionViewModel>> GetTransactionCustomerAccountOtherSubscriptionEntries(long _transactionCustomerAccountPrmKey, string _entryType);

        Task<IEnumerable<SharesCessationTransactionViewModel>> GetSharesCessationTransactionEntries(long _transactionCustomerAccountPrmKey, string _entryType);

        Task<IEnumerable<SharesCapitalTransactionViewModel>> GetSharesTransactionEntries(long _transactionCustomerAccountPrmKey, string _entryType);

        Task<IEnumerable<TransactionCashDenominationViewModel>> GetTransactionCashDenominationEntries(long _transactionMasterPrmKey, string _entryType);

        //Dropdown List
        List<SelectListItem> DenominationDropdownList { get; }

        List<SelectListItem> GetAccountNumberDropDownListForTransaction(Guid _personId, Guid _businessOfficeId, Guid _generalLedgerId);
       
        List<SelectListItem> GetBusinessOfficeDropDownListForTransaction();
       
        List<SelectListItem> GetGeneralLedgerDropdownListForTransaction(Guid _personId, Guid _businessOfficeId);
       
        List<SelectListItem> GetPersonDropdownListForTransaction(Guid _businessOfficeId);

        List<SelectListItem> GetTransactionTypeDropDownListForTransaction();
    }
}

