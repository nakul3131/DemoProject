using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeMemberNumberRepository : IBusinessOfficeMemberNumberRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeMemberNumberRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<BusinessOfficeMemberNumberViewModel> GetRejectedEntry(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeMemberNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeMemberNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeMemberNumberViewModel> GetUnverifiedEntry(short _businessOfficePrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<BusinessOfficeMemberNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeMemberNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeMemberNumberViewModel> GetVerifiedEntry(short _businessOfficePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeMemberNumberViewModel>("SELECT * FROM dbo.GetBusinessOfficeMemberNumberEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
