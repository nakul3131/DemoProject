using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFTransactionGeneralLedgerRepository : ITransactionGeneralLedgerRepository
    {
        private readonly EFDbContext context;

        public EFTransactionGeneralLedgerRepository(RepositoryConnection _connection) 
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetRejectedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<TransactionGeneralLedgerViewModel>("SELECT * FROM dbo.GetTransactionGeneralLedgerEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetUnverifiedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<TransactionGeneralLedgerViewModel>("SELECT * FROM dbo.GetTransactionGeneralLedgerEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<TransactionGeneralLedgerViewModel>> GetVerifiedEntries(long _transactionMasterPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<TransactionGeneralLedgerViewModel>("SELECT * FROM dbo.GetTransactionGeneralLedgerEntriesByTransactionMasterPrmKey (@UserProfilePrmKey, @TransactionMasterPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@TransactionMasterPrmKey", _transactionMasterPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
