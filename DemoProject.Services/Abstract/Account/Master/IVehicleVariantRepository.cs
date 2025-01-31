using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IVehicleVariantRepository
    {
        // Amend VehicleVariant Delete Entry - If Entry Rejected
        Task<bool> Amend(VehicleVariantViewModel _vehicleVariantViewModel);
       
        // Return Valid List From VehicleVariant Table For Modification
        Task<IEnumerable<VehicleVariantViewModel>> GetIndex(string _entryType);
        
        // Return Record From VehicleVariant Table By Given Parameter (i.e. VehicleVariantId)
        Task<VehicleVariantViewModel> GetEntry(Guid _vehicleVariantId,string _entryType);
        
        // Save VehicleVariant New Entry
        Task<bool> Save(VehicleVariantViewModel _vehicleVariantViewModel);

        // Save VehicleVariant Modification New Entry
        Task<bool> Modify(VehicleVariantViewModel _vehicleVariantViewModel);

        // Authorize VehicleVariant Entry
        Task<bool> VerifyRejectDelete(VehicleVariantViewModel _vehicleVariantViewModel,string _entryType);

    }
}
