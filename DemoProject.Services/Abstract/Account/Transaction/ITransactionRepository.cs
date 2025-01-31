using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionRepository
    {
        List<SelectListItem> GetGLAndAccountNumber(Guid _PersonId);

        // Amend Address Parameter Delete Entry - If Entry Rejected
        Task<bool> Amend(TransactionViewModel _transactionViewModel);

        
        // Return Current Active Entry
        Task<TransactionViewModel> GetActiveEntry();

        Task<bool> GetSessionValues(TransactionViewModel _transactionViewModel, string _entryType);

        Task<IEnumerable<TransactionIndexViewModel>> GetIndexOfUnVerifiedEntries();
        
        // Return Autherize Entry
        Task<IEnumerable<TransactionViewModel>> GetTransactionParameterIndex();

        // Return Rejected Entry
        Task<TransactionViewModel> GetRejectedEntry();

        // Return UnAuthorized Entry
        Task<TransactionViewModel> GetUnVerifiedEntry();
        
       
        // Save Address Parameter New Entry
        Task<bool> Save(TransactionViewModel _transactionViewModel);

        // Authorize Address Parameter Entry
        Task<bool> VerifyRejectDelete(TransactionViewModel _transactionViewModel,string _entryType);
    }
}
