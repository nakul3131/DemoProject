using DemoProject.Services.ViewModel.Account.Master;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IVehicleDbContextRepository
    {
        bool AttachVehicleMakeData(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType);

        bool AttachVehicleMakeModificationData(VehicleMakeViewModel _vehicleMakeViewModel, string _entryType);

        bool AttachVehicleModelData(VehicleModelViewModel _vehicleModelViewModel, string _entryType);

        bool AttachVehicleModelModificationData(VehicleModelViewModel _vehicleModelViewModel, string _entryType);

        bool AttachVehicleVariantData(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType);

        bool AttachVehicleVariantModificationData(VehicleVariantViewModel _vehicleVariantViewModel, string _entryType);


        Task<bool> SaveData();
    }
}
