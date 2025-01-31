using DemoProject.Services.ViewModel.Account.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerPreOwnedVehicleLoanPhotoRepository
    {
        // Return Rejected CustomerPreOwnedVehicleLoanPhoto Entries
        Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetRejectedEntries(long _customerAccountPrmKey);

        // Return UnVerified CustomerPreOwnedVehicleLoanPhoto Entries
        Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetUnverifiedEntries(long _customerAccountPrmKey);

        // Return Verified CustomerPreOwnedVehicleLoanPhoto Entries
        Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetVerifiedEntries(long _customerAccountPrmKey);

    }
}
