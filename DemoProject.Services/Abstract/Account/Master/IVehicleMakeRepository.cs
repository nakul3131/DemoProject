using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IVehicleMakeRepository
    {
        // Amend VehicleMake Delete Entry - If Entry Rejected
        Task<bool> Amend(VehicleMakeViewModel _vehicleMakeViewModel);

        // Return Valid List From VehicleMake Table For Modification
        Task<IEnumerable<VehicleMakeViewModel>> GetIndex(string _entryType);

        bool GetUniqueVehicleMakeName(string _nameOfVehicleMake);

        // Return Record From VehicleMake Table By Given Parameter (i.e. _vehicleMakeId)
        Task<VehicleMakeViewModel> GetEntry(Guid _vehicleMakeId,string _entryType);

        // Save VehicleMake Modification New Entry
        Task<bool> Modify(VehicleMakeViewModel _vehicleMakeViewModel);
       
        // Save VehicleMake New Entry
        Task<bool> Save(VehicleMakeViewModel _vehicleMakeViewModel);

        // Authorize VehicleMake Entry
        Task<bool> VerifyRejectDelete(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType);
    }
}
