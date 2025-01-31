using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeTransactionParameterRepository : IBusinessOfficeTransactionParameterRepository
    {
        private readonly EFDbContext context;

        public EFBusinessOfficeTransactionParameterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<BusinessOfficeTransactionParameterViewModel> GetRejectedEntry(short _businessOfficePrmKey)
        {
            try
            {
                //return await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();

                BusinessOfficeTransactionParameterViewModel businessOfficeTransactionParameterViewModel = await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask = businessOfficeTransactionParameterViewModel.TransactionNumberMask.Split(',');

                return businessOfficeTransactionParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeTransactionParameterViewModel> GetUnverifiedEntry(short _businessOfficePrmKey)
        {
            try
            {
                BusinessOfficeTransactionParameterViewModel businessOfficeTransactionParameterViewModel = await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask = businessOfficeTransactionParameterViewModel.TransactionNumberMask.Split(',');

                return businessOfficeTransactionParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<BusinessOfficeTransactionParameterViewModel> GetVerifiedEntry(short _businessOfficePrmKey)
        {
            try
            {
                //return await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                BusinessOfficeTransactionParameterViewModel businessOfficeTransactionParameterViewModel = await context.Database.SqlQuery<BusinessOfficeTransactionParameterViewModel>("SELECT * FROM dbo.GetBusinessOfficeTransactionParameterEntryByBusinessOfficePrmKey (@BusinessOfficePrmKey, @EntryType)", new SqlParameter("@BusinessOfficePrmKey", _businessOfficePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();

                // Get Multiselect Id's From String (i.e. (Array) TransactionNumberMask From (String) MaskTypeCharacterForTransactionNumberMask)
                businessOfficeTransactionParameterViewModel.MaskTypeCharacterForTransactionNumberMask = businessOfficeTransactionParameterViewModel.TransactionNumberMask.Split(',');

                return businessOfficeTransactionParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
