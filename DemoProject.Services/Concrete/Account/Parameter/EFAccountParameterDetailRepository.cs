using System;
using System.Data.SqlClient;
using System.Linq;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFAccountParameterDetailRepository : IAccountParameterDetailRepository
    {
        private readonly EFDbContext context;

        public EFAccountParameterDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        // ByLawsLoanScheduleParameterViewModel

        public ByLawsLoanScheduleParameterViewModel GetByLawsLoanScheduleParameterEntry(short _loanTypePrmKey, string _entryType)
        {
            try
            {
                return context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@LoanTypePrmKey, @EntriesType)", new SqlParameter("@LoanTypePrmKey", _loanTypePrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
