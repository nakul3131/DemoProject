using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerPreOwnedVehicleLoanPhotoRepository : ICustomerPreOwnedVehicleLoanPhotoRepository
    {
        private readonly EFDbContext context;

        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;

        public EFCustomerPreOwnedVehicleLoanPhotoRepository(RepositoryConnection _connection, ICryptoAlgorithmRepository _cryptoAlgorithmRepository)
        {
            context = _connection.EFDbContext;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
        }

        public async Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {
                IEnumerable<CustomerVehicleLoanPhotoViewModel> personCommoditiesAssetViewModels;

                personCommoditiesAssetViewModels = await context.Database.SqlQuery<CustomerVehicleLoanPhotoViewModel>("SELECT * FROM dbo.GetCustomerVehicleLoanPhotoEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

                return personCommoditiesAssetViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetUnverifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanPhotoViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanPhotoEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerVehicleLoanPhotoViewModel>("SELECT * FROM dbo.GetCustomerPreOwnedVehicleLoanPhotoEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

    }
}
