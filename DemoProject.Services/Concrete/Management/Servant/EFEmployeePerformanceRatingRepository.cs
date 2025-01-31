using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Servant
{
    public class EFEmployeePerformanceRatingRepository : IEmployeePerformanceRatingRepository
    {
        private readonly EFDbContext context;

        public EFEmployeePerformanceRatingRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<EmployeePerformanceRatingViewModel> GetRejectedEntries(int _employeePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeePerformanceRatingViewModel>("SELECT * FROM dbo.GetEmployeePerformanceRatingEntryByEmployeePrmKey (@EmployeePrmkey, @EntriesType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeePerformanceRatingViewModel> GetUnVerifiedEntries(int _employeePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeePerformanceRatingViewModel>("SELECT * FROM dbo.GetEmployeePerformanceRatingEntryByEmployeePrmKey (@EmployeePrmkey, @EntriesType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<EmployeePerformanceRatingViewModel> GetVerifiedEntries(int _employeePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeePerformanceRatingViewModel>("SELECT * FROM dbo.GetEmployeePerformanceRatingEntryByEmployeePrmKey (@EmployeePrmkey, @EntriesType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
