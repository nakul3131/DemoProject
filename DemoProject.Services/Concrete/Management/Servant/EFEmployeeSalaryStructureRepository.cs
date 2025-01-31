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
    public class EFEmployeeSalaryStructureRepository : IEmployeeSalaryStructureRepository
    {
        private readonly EFDbContext context;

        public EFEmployeeSalaryStructureRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetRejectedEntries(int _employeePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<EmployeeSalaryStructureViewModel>("SELECT * FROM dbo.GetEmployeeSalaryStructureEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetUnverifiedEntries(int _employeePrmKey)
        {
            try
            {
                IEnumerable<EmployeeSalaryStructureViewModel> employeeSalaryStructure = await context.Database.SqlQuery<EmployeeSalaryStructureViewModel>("SELECT * FROM dbo.GetEmployeeSalaryStructureEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return employeeSalaryStructure;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<EmployeeSalaryStructureViewModel>> GetVerifiedEntries(int _employeePrmKey)
        {
            try
            {
                IEnumerable<EmployeeSalaryStructureViewModel> employeeSalaryStructureViewModels = await context.Database.SqlQuery<EmployeeSalaryStructureViewModel>("SELECT * FROM dbo.GetEmployeeSalaryStructureEntriesByEmployeePrmKey (@EmployeePrmkey, @EntryType)", new SqlParameter("@EmployeePrmkey", _employeePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToListAsync();

                return employeeSalaryStructureViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
