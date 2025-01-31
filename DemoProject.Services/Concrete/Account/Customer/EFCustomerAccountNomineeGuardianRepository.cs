using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountNomineeGuardianRepository : ICustomerAccountNomineeGuardianRepository
    {
        private readonly EFDbContext context;

        public EFCustomerAccountNomineeGuardianRepository(RepositoryConnection _connection) 
        {
            context = _connection.EFDbContext;
        }

        //public long GetPrmKeyById(Guid _customerAccountId) 
        //{
        //    var a= context.CustomerAccountNominees
        //            .Where(c => c.CustomerAccountNomineeId == _customerAccountId)
        //            .Select(c => c.PrmKey).FirstOrDefault();
        //    return a;
        //}

        public IEnumerable<CustomerAccountNomineeGuardianViewModel> GetRejectedEntries(long _customerAccountNomineePrmKey)
        {
            try
            {
                var a = context.Database.SqlQuery<CustomerAccountNomineeGuardianViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeGuardianEntriesByCustomerPrmKey (@CustomerAccountNomineePrmKey, @EntriesType)", new SqlParameter("@CustomerAccountNomineePrmKey", _customerAccountNomineePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToList();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public IEnumerable<CustomerAccountNomineeGuardianViewModel> GetUnverifiedEntries(long _customerAccountNomineePrmKey)
        {
            try
            {
                var a = context.Database.SqlQuery<CustomerAccountNomineeGuardianViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeGuardianEntriesByCustomerPrmKey (@CustomerAccountNomineePrmKey, @EntriesType)", new SqlParameter("@CustomerAccountNomineePrmKey", _customerAccountNomineePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToList();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public IEnumerable<CustomerAccountNomineeGuardianViewModel> GetVerifiedEntries(long _customerAccountNomineePrmKey)
        {
            try
            {
                var a = context.Database.SqlQuery<CustomerAccountNomineeGuardianViewModel>("SELECT * FROM dbo.GetCustomerAccountNomineeGuardianEntriesByCustomerPrmKey (@CustomerAccountNomineePrmKey, @EntryType)", new SqlParameter("@CustomerAccountNomineePrmKey", _customerAccountNomineePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).ToList();
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
