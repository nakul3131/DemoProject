using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeSpecialPermissionRepository : IBusinessOfficeSpecialPermissionRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeSpecialPermissionRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<BusinessOfficeSpecialPermissionViewModel>> GetRejectedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeSpecialPermissionViewModel>("SELECT * FROM dbo.GetBusinessOfficeSpecialPermissionEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeSpecialPermissionViewModel>> GetUnverifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<BusinessOfficeSpecialPermissionViewModel>("SELECT * FROM dbo.GetBusinessOfficeSpecialPermissionEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeSpecialPermissionViewModel>> GetVerifiedEntries(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeSpecialPermissionViewModel>("SELECT * FROM dbo.GetBusinessOfficeSpecialPermissionEntriesByBusinessOfficePrmKey (@BusinessOfficePrmkey, @EntryType)", new SqlParameter("@BusinessOfficePrmkey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
