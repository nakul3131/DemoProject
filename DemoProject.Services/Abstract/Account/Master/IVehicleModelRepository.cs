using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IVehicleModelRepository
    {
        // Amend VehicleModel Delete Entry - If Entry Rejected
        Task<bool> Amend(VehicleModelViewModel _vehicleModelViewModel);
        
        // Return Valid List From VehicleModel Table For Modification
        Task<IEnumerable<VehicleModelViewModel>> GetIndex(string _entryType);
        
        // Return Record From VehicleModel Table By Given Parameter (i.e. VehicleModelId)
        Task<VehicleModelViewModel> GetEntry(Guid _vehicleModelId,string _entryType);
        
        // Save VehicleModel New Entry
        Task<bool> Save(VehicleModelViewModel _vehicleModelViewModel);

        // Save VehicleModel Modification New Entry
        Task<bool> Modify(VehicleModelViewModel _vehicleModelViewModel);

        // Authorize VehicleModel Entry
        Task<bool> VerifyRejectDelete(VehicleModelViewModel _vehicleModelViewModel,string _entryType);

    }
}

