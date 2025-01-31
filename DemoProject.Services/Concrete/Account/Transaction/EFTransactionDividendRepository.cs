using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFTransactionDividendRepository : ITransactionDividendRepository
    {
        private readonly EFDbContext context;

        public EFTransactionDividendRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<TransactionDividendIndexViewModel>> GetTransactionDividendIndex(DateTime transactionDate)
        {
            try
            {
                var a = await context.Database.SqlQuery<TransactionDividendIndexViewModel>("dbo.Usp_CalculateDividend @TransactionDate", new SqlParameter("@TransactionDate", transactionDate)).ToListAsync();
                return a;
            }
            catch(Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
