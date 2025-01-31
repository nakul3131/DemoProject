using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFTransactionCustomerAccountRepository : ITransactionCustomerAccountRepository
    {
        private readonly EFDbContext context;

        public readonly ICustomerAccountRepository customerAccountRepository;
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        public EFTransactionCustomerAccountRepository(RepositoryConnection _connection, ICustomerAccountRepository _customerAccountRepository, IGeneralLedgerRepository _generalLedgerRepository)
        {
            context = _connection.EFDbContext;
            customerAccountRepository = _customerAccountRepository;
            generalLedgerRepository = _generalLedgerRepository;
        }


        public decimal GetClosingBalance(DateTime _balanceDate, Guid _customerAccountId)
        {
            long customerAccountPrmKey = customerAccountRepository.GetPrmKeyById(_customerAccountId);

            return (from m in context.TransactionMasters
                    join t in context.TransactionCustomerAccounts.Where(m => m.EntryStatus == StringLiteralValue.Verify) on m.PrmKey equals t.TransactionMasterPrmKey into mt
                    from t in mt.DefaultIfEmpty()
                    orderby m.TransactionDate, m.PrmKey descending
                    where (m.TransactionDate <= _balanceDate && t.CustomerAccountPrmKey == customerAccountPrmKey)
                    select (t.Balance)).FirstOrDefault();
        }

        public decimal GetLedgerBalance(DateTime _balanceDate, Guid _generalLedgerId)
        {
            short generalLedgerPrmKey = generalLedgerRepository.GetPrmKeyById(_generalLedgerId);

            return (from m in context.TransactionMasters
                    join g in context.TransactionGeneralLedgers.Where(m => m.EntryStatus == StringLiteralValue.Verify) on m.PrmKey equals g.TransactionMasterPrmKey into mt
                    from g in mt.DefaultIfEmpty()
                    orderby m.TransactionDate, m.PrmKey descending
                    where (m.TransactionDate <= _balanceDate && g.GeneralLedgerPrmKey == generalLedgerPrmKey)
                    select (g.Amount)).FirstOrDefault();
        }
        public async Task<IEnumerable<TransactionCustomerAccountViewModel>> GetRejectedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<TransactionCustomerAccountViewModel>("SELECT * FROM dbo.GetTransactionCustomerAccountEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionCustomerAccountViewModel>> GetUnverifiedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<TransactionCustomerAccountViewModel>("SELECT * FROM dbo.GetTransactionCustomerAccountEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionCustomerAccountViewModel>> GetVerifiedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionCustomerAccountViewModel>("SELECT * FROM dbo.GetTransactionCustomerAccountEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
