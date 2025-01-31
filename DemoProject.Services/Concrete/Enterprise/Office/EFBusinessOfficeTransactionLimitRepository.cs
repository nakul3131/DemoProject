using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeTransactionLimitRepository : IBusinessOfficeTransactionLimitRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeTransactionLimitRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public short GetPrmKeyById(Guid _businessOfficeId)
        {
            return context.BusinessOffices
                    .Where(c => c.BusinessOfficeId == _businessOfficeId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> GeneralLedgerDropdownList
        {
            get
            {
                return (from e in context.GeneralLedgers

                        select new SelectListItem
                        {
                            Value = e.GeneralLedgerId.ToString(),
                            Text = e.NameOfGL
                        }).ToList();
            }
        }

        public List<SelectListItem> TransactionTypeDropdownList
        {
            get
            {
                return (from a in context.TransactionTypes

                        select new SelectListItem
                        {
                            Value = a.TransactionTypeId.ToString(),
                            Text = a.NameOfTransactionType
                        }).ToList();
            }
        }

        public async Task<IEnumerable<BusinessOfficeTransactionLimitViewModel>> GetRejectedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeTransactionLimitViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionLimitEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeTransactionLimitViewModel>> GetUnverifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<BusinessOfficeTransactionLimitViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionLimitEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeTransactionLimitViewModel>> GetVerifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeTransactionLimitViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionLimitEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
