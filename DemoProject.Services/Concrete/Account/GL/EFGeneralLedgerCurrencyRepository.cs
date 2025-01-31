using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.Wrapper;


namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerCurrencyRepository : IGeneralLedgerCurrencyRepository
    {
        private readonly EFDbContext context;

        public EFGeneralLedgerCurrencyRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetRejectedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerCurrencyViewModel>("SELECT * FROM dbo.GetGeneralLedgerCurrencyEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmkey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetUnverifiedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerCurrencyViewModel>("SELECT * FROM dbo.GetGeneralLedgerCurrencyEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GeneralLedgerCurrencyViewModel>> GetVerifiedGeneralLedgerCurrencyEntries(short _generalLedgerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GeneralLedgerCurrencyViewModel>("SELECT * FROM dbo.GetGeneralLedgerCurrencyEntriesByGeneralLedgerPrmKey (@UserProfilePrmKey, @GeneralLedgerPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
