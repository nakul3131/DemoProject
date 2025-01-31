using DemoProject.Services.ViewModel.Account.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Transaction
{
    public interface ITransactionDividendRepository
    {
        Task<IEnumerable<TransactionDividendIndexViewModel>> GetTransactionDividendIndex(DateTime transactionDate);
    }
}
