using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Servant
{
    public class EFEmployeeDocumentRepository : IEmployeeDocumentRepository
    {
        private readonly EFDbContext context;

        public EFEmployeeDocumentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<EmployeeDocumentViewModel>> GetRejectedEntries(int _employeePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<EmployeeDocumentViewModel>("SELECT * FROM dbo.GetEmployeeDocumentEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EmployeeDocumentViewModel>> GetUnverifiedEntries(int _employeePrmKey)
        {
            try
            {
                IEnumerable<EmployeeDocumentViewModel> employeeDocument = await context.Database.SqlQuery<EmployeeDocumentViewModel>("SELECT * FROM dbo.GetEmployeeDocumentEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return employeeDocument;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EmployeeDocumentViewModel>> GetVerifiedEntries(int _employeePrmKey)
        {
            try
            {
                IEnumerable<EmployeeDocumentViewModel> employeeDocumentViewModels = await context.Database.SqlQuery<EmployeeDocumentViewModel>("SELECT * FROM dbo.GetEmployeeDocumentEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();

                return employeeDocumentViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
